;
(function (window, jQuery, undefined) {

    'use strict';

    var SortingActivityTable = {
        options: {
            addButton: '#addToTable',
            problemId: '#proId',
            problemNo: '#problemno',
            table: '#datatable-editable-sortingactivity',
            approveButton: '#approveSortingactivity',
            rejectButton: '#rejectSortingactivity',
        },


        initialize: function () {
            SortingActivityTable.setVars()
                .build()
                .events();
        },

        setVars: function () {
            this.$table = $(this.options.table);
            this.$addButton = $(this.options.addButton);
            this.$problemId = $(this.options.problemId);
            this.$problemNo = $(this.options.problemNo);
            this.$approveButton = $(this.options.approveButton);
            this.$rejectButton = $(this.options.rejectButton);
            return this;
        },

        build: function () {
            this.datatable = this.$table.DataTable({
                aoColumns: [
                    null,
                    null,
                    null,
                    null,
                    { "bSortable": false }
                ],
                bPaginate: false,
                iDisplayLength: 1000,
                bInfo: false,
                bFilter: false,
                bSort: false,
            });

            window.dt = this.datatable;

            return this;
        },

        events: function () {
            var _self = this;

            this.$table
                .on('click', 'a.save-row', function (e) {
                    e.preventDefault();
                    var _event = this;
                    var data = $(_event).closest('tr').find('td').map(function () {
                        var $this = $(this);
                        if ($this.hasClass('actions')) {
                            return _self.datatable.cell(this).data();
                        }
                        else if ($this.hasClass('valuestream')) {
                            var actibityInfo = {
                                Id: $this.find('label').attr("data-id") || 0,
                                ValueStreamNo: $this.find('label').attr("data-no") || 0,
                                ValueStream: $this.find('label').html() || '',
                            }
                            return actibityInfo;
                        }
                        else {
                            return $.trim($this.find('input').val());
                        }
                    });

                    var params = {
                        Id: data[0].Id || 0,
                        PSAProblemId: _self.$problemId.val() || 0,
                        PSAProblemNo: _self.$problemNo.html() || '',
                        PSAValueStream: data[0].ValueStream || '',
                        PSAValueStreamNo: data[0].ValueStreamNo || 0,
                        PSADefectQty: data[1] || '',
                        PSASortedQty: data[2] || '',
                        PSADeadLine: data[3] || '',
                    };

                    $.ajax({
                        type: "Post",
                        url: "SaveSortingActivity",
                        async: true,
                        data: JSON.stringify(params),
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            if (data != null) {
                                if (data.IsSuccess == true) {
                                    var actibityInfo = {
                                        Id: data.data.Id || 0,
                                        ValueStreamNo: data.data.PSAValueStreamNo || 0,
                                        ValueStream: data.data.PSAValueStream || '',
                                    };
                                    SortingActivityTable.rowSave($(_event).closest('tr'), actibityInfo);
                                }
                                else {
                                    alert("error");
                                }
                            }
                            else {
                                alert("error");
                            }
                        }
                    });
                })
                .on('click', 'a.cancel-row', function (e) {
                    e.preventDefault();

                    _self.rowCancel($(this).closest('tr'));
                })
                .on('click', 'a.edit-row', function (e) {
                    e.preventDefault();

                    _self.rowEdit($(this).closest('tr'));
                })

            this.$approveButton.on('click', function () {
                _self.$addButton.addClass("hidden");
                _self.$approveButton.addClass("hidden");
                $("#rejectQualityalert").addClass("hidden");
                if (IfHasRole("BTN_SORTACT_REJECT")) {
                    _self.$rejectButton.removeClass("hidden");
                }
                var params = {
                    Id: $("#proId").val() || 0,
                    PIProblemNo: $("#problemno").html() || '',
                    PIProcessStatus: 401,
                };
                $.ajax({
                    type: "Post",
                    url: "UpdateProblemStatus",
                    async: true,
                    data: JSON.stringify(params),
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data != null) {
                            if (data.IsSuccess == true) {
                                $("#proProcessStatus").val(401);
                                InitTabs();
                            }
                            else {
                                alert("error");
                            }
                        }
                        else {
                            alert("error");
                        }
                    }
                });
                return false;
            });
            this.$rejectButton.on('click', function () {
                _self.$rejectButton.addClass("hidden");
                if (IfHasRole("BTN_SORTACT_APPROVE")) {
                    _self.$approveButton.removeClass("hidden");
                }
                var params = {
                    Id: $("#proId").val() || 0,
                    PIProblemNo: $("#problemno").html() || '',
                    PIProcessStatus: 301,
                };
                $.ajax({
                    type: "Post",
                    url: "UpdateProblemStatus",
                    async: true,
                    data: JSON.stringify(params),
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data != null) {
                            if (data.IsSuccess == true) {
                                $("#proProcessStatus").val(301);
                                InitTabs();
                            }
                            else {
                                alert("error");
                            }
                        }
                        else {
                            alert("error");
                        }
                    }
                });
                return false;
            });
            return this;
        },

        // ==========================================================================================
        // ROW FUNCTIONS
        // ==========================================================================================
        rowAdd: function () {
            this.$addButton.attr({ 'disabled': 'disabled' });

            var actions,

                data,
                $row;

            actions = [
                '<a href="#" class="hidden on-editing save-row"><i class="fa fa-save"></i></a>',
                '<a href="#" class="hidden on-editing cancel-row"><i class="fa fa-times"></i></a>',
                '<a href="#" class="on-default edit-row"><i class="fa fa-pencil"></i></a>',
            ].join(' ');


            data = this.datatable.row.add(['', '', '', '', actions]);
            $row = this.datatable.row(data[0]).nodes().to$();

            $row
                .addClass('adding')
                .find('td:last')
                .addClass('actions');
            this.rowEdit($row);

            this.datatable.draw();
        },

        rowCancel: function ($row) {
            var _self = this,
                $actions,
                i,
                data;

            if ($row.hasClass('adding')) {
                this.rowRemove($row);
            } else {

                data = this.datatable.row($row.get(0)).data();
                this.datatable.row($row.get(0)).data(data);

                $actions = $row.find('td.actions');
                if ($actions.get(0)) {
                    this.rowSetActionsDefault($row);
                }

                this.datatable.draw();
            }
        },

        rowEdit: function ($row) {
            var _self = this,
                data;

            data = this.datatable.row($row.get(0)).data();

            $row.children('td').each(function (i) {
                var $this = $(this);

                if ($this.hasClass('actions')) {
                    _self.rowSetActionsEditing($row);
                }
                else if ($this.hasClass('valuestream')) {
                    $this.html(data[i]);
                }
                else if ($this.hasClass('deadline')) {
                    $this.html('<input type="text" class="col-xs-2 form-control width140 dateinput" name="deadline" value="' + data[i] + '"/>');
                }
                else {
                    $this.html('<input type="text" class="form-control input-block" value="' + data[i] + '"/>');
                }
            });
            this.datePicker($row);
        },

        rowSave: function ($row, actibityInfo) {
            var _self = this,
                $actions,
                values = [];

            if ($row.hasClass('adding')) {
                this.$addButton.removeAttr('disabled');
                $row.removeClass('adding');
            }

            values = $row.find('td').map(function () {
                var $this = $(this);

                if ($this.hasClass('actions')) {
                    _self.rowSetActionsDefault($row);
                    return _self.datatable.cell(this).data();
                }
                else if ($this.hasClass('valuestream')) {
                    return $.trim("<label class=\"control-label\" data-id=\"" + actibityInfo.Id + "\" data-no=\"" + actibityInfo.ValueStreamNo + "\">" + actibityInfo.ValueStream + "</label >");
                } else {
                    return $.trim($this.find('input').val());
                }
            });

            this.datatable.row($row.get(0)).data(values);

            $actions = $row.find('td.actions');
            if ($actions.get(0)) {
                this.rowSetActionsDefault($row);
            }

            this.datatable.draw();
        },

        rowRemove: function ($row) {
            if ($row.hasClass('adding')) {
                this.$addButton.removeAttr('disabled');
            }

            this.datatable.row($row.get(0)).remove().draw();
        },

        rowSetActionsEditing: function ($row) {
            $row.find('.on-editing').removeClass('hidden');
            $row.find('.on-default').addClass('hidden');
        },

        rowSetActionsDefault: function ($row) {
            $row.find('.on-editing').addClass('hidden');
            $row.find('.on-default').removeClass('hidden');
        },
        datePicker: function ($row) {
            $row.find(".dateinput").each(function (index, element) {
                $(this).datetimepicker({
                    format: "yyyy-mm-dd hh:ii",
                    autoclose: true,
                    todayBtn: true,
                    startDate: "2017-10-01 10:00",
                    minuteStep: 10,
                    minView: 0
                });
            });;
        },
        setActionBtn: function (flag) {
            if (flag == 1) {
                $("#sortingactivitybody").find("tr").each(function (index, item) {
                    //$(item).find(".on-editing").each(function (index2, item2) {
                    //    $(item2).removeClass("hidden");
                    //});
                    $(item).find(".on-default").each(function (index2, item2) {
                        $(item2).removeClass("hidden");
                    });
                });
            }
            else if (flag == 0) {
                $("#sortingactivitybody").find("tr").each(function (index, item) {
                    $(item).find(".on-editing").each(function (index2, item2) {
                        $(item2).addClass("hidden");
                    });
                    $(item).find(".on-default").each(function (index2, item2) {
                        $(item2).addClass("hidden");
                    });
                });
            }
        },
        initialdata: function (sortingactivitydata) {
            for (var i = 0; i < sortingactivitydata.length; i++) {
                var no = sortingactivitydata[i].PSAValueStreamNo;
                var td = $("#sortingactivitybody").find("[data-no='" + no + "']").parent();
                td.next().html(sortingactivitydata[i].PSADefectQty);
                td.next().next().html(sortingactivitydata[i].PSASortedQty);
                td.next().next().next().html(sortingactivitydata[i].PSADeadLineDesc);
                var tr = td.parent();
                var trstring = tr.prop("outerHTML");

                SortingActivityTable.datatable.row(tr).remove();
                SortingActivityTable.datatable.row.add($(trstring));
                SortingActivityTable.datatable.draw();
            }
        }

    };
    window.InitSortingActivity = SortingActivityTable.initialize;
    window.InitSortingActivityData = SortingActivityTable.initialdata;
    window.SetSortingActivityBtn = SortingActivityTable.setActionBtn;
})(window, jQuery)


