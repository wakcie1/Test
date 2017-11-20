;
(function (window, jQuery, undefined) {

    var WhyAnalysis1 = {
        options: {
            addButton1: '#addwhyanalysis1',
            addButton2: '#addwhyanalysis2',
            table: '#datatable-editable-whyanalysis1',
            problemId: "#proId",
            problemNo: "#problemno",
            approveButton: '#approveWhyanalysis',
            rejectButton: '#rejectWhyanalysis',
            whyQuestion: '1) Why did it happen?',
            dialog: {
                wrapper: '#dialog-whyanalysis1',
                cancelButton: '#dialogCancel-whyanalysis1',
                confirmButton: '#dialogConfirm-whyanalysis1',
            }
        },


        initialize: function () {
            WhyAnalysis1.setVars()
                .build()
                .events();
        },

        setVars: function () {
            this.$table1 = $(this.options.table);
            this.$addButton1 = $(this.options.addButton1);
            this.$addButton2 = $(this.options.addButton2);
            this.$problemId = $(this.options.problemId);
            this.$problemNo = $(this.options.problemNo);
            this.$approveButton = $(this.options.approveButton);
            this.$rejectButton = $(this.options.rejectButton);
            // dialog
            this.dialog = {};
            this.dialog.$wrapper = $(this.options.dialog.wrapper);
            this.dialog.$cancel = $(this.options.dialog.cancelButton);
            this.dialog.$confirm = $(this.options.dialog.confirmButton);

            return this;
        },

        build: function () {
            this.datatable = this.$table1.DataTable({
                aoColumns: [
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    { "bSortable": false }
                ],
                bPaginate: false,
                iDisplayLength: 1000, //默认显示的记录数 
                bInfo: false,
                bFilter: false,
            });

            window.dt = this.datatable;

            return this;
        },

        events: function () {
            var _self = this;

            this.$table1
                .on('click', 'a.save-row', function (e) {
                    e.preventDefault();
                    var _event = this;
                    var data = $(_event).closest('tr').find('td').map(function () {
                        var $this = $(this);
                        if ($this.hasClass('actions')) {
                            return _self.datatable.cell(this).data();
                        }
                        else if ($this.hasClass('whyform')) {
                            var WhyAnalysis1 = {
                                Id: $this.find('label').attr("data-id") || 0,
                                WhyForm: $this.find('label').html() || ''
                            }
                            return WhyAnalysis1;
                        }
                        else {
                            return $.trim($this.find('input').val());
                        }
                    });
                    var params = {
                        Id: data[0].Id || 0,
                        PAWProblemId: _self.$problemId.val() || 0,
                        PAWProblemNo: _self.$problemNo.html() || '',
                        PAWWhyForm: data[0].WhyForm || '',
                        PAWWhyQuestionChain: data[1] || '',
                        PAWWhy1: data[2] || '',
                        PAWWhy2: data[3] || '',
                        PAWWhy3: data[4] || '',
                        PAWWhy4: data[5] || '',
                        PAWWhy5: data[6] || '',
                    };

                    $.ajax({
                        type: "Post",
                        url: "SaveActionWhyanalysis",
                        async: true,
                        data: JSON.stringify(params),
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            if (data != null) {
                                if (data.IsSuccess == true) {
                                    var whyAnalysisInfo = {
                                        Id: data.data.Id || 0,
                                        WhyFrom: data.data.PAWWhyForm || ''
                                    };
                                    WhyAnalysis1.rowSave($(_event).closest('tr'), whyAnalysisInfo);
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
                .on('click', 'a.remove-row', function (e) {
                    e.preventDefault();
                    var $row = $(this).closest('tr');

                    var dataId = $row.find('.whyform').find('label').attr('data-id') || 0;

                    $.magnificPopup.open({
                        items: {
                            src: '#dialog-whyanalysis1',
                            type: 'inline'
                        },
                        preloader: false,
                        modal: true,
                        callbacks: {
                            change: function () {
                                _self.dialog.$confirm.on('click', function (e) {
                                    e.preventDefault();
                                    _self.rowRecordDelete(dataId);
                                    _self.rowRemove($row);
                                    $.magnificPopup.close();
                                });
                            },
                            close: function () {
                                _self.dialog.$confirm.off('click');
                            }
                        }
                    });
                });

            this.$addButton1.on('click', function (e) {
                e.preventDefault();
                _self.rowAdd();
            });

            this.dialog.$cancel.on('click', function (e) {
                e.preventDefault();
                $.magnificPopup.close();
            });

            this.$approveButton.on('click', function () {
                _self.$addButton1.addClass("hidden");
                _self.$addButton2.addClass("hidden");
                _self.$approveButton.addClass("hidden");
                $("#rejectContainmentaction").addClass("hidden");
                $("#submitRootCause").addClass("hidden");
                $("#editRootCause").addClass("hidden");
                if (IfHasRole("BTN_WHYANALY_REJECT")) {
                    _self.$rejectButton.removeClass("hidden");
                }
                var params = {
                    Id: $("#proId").val() || 0,
                    PIProblemNo: $("#problemno").html() || '',
                    PIProcessStatus: 651,
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
                                $("#proProcessStatus").val(651);
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
                if (IfHasRole("BTN_WHYANALY_APPROVE")) {
                    _self.$approveButton.removeClass("hidden");
                }
                if (IfHasRole("BTN_WHYANALY_ADD")) {
                    _self.$addButton1.removeClass("hidden");
                    _self.$addButton2.removeClass("hidden");
                }
                var params = {
                    Id: $("#proId").val() || 0,
                    PIProblemNo: $("#problemno").html() || '',
                    PIProcessStatus: 501,
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
                                $("#proProcessStatus").val(501);
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
            this.$addButton1.attr({ 'disabled': 'disabled' });

            var actions,
                whyform,
                questionchain,
                why1,
                why2,
                why3,
                why4,
                why5,
                data,
                $row;

            actions = [
                '<a href="#" class="hidden on-editing save-row"><i class="fa fa-save"></i></a>',
                '<a href="#" class="hidden on-editing cancel-row"><i class="fa fa-times"></i></a>',
                '<a href="#" class="on-default edit-row"><i class="fa fa-pencil"></i></a>',
                '<a href="#" class="on-default remove-row"><i class="fa fa-trash-o"></i></a>',
            ].join(' ');
            whyform = [
                '<label class="control-label" data-id="0">' + this.options.whyQuestion + '</label>',
            ].join(' ');
            questionchain = [
            ].join(' ');
            why1 = [
            ].join(' ');
            why2 = [
            ].join(' ');
            why3 = [
            ].join(' ');
            why4 = [
            ].join(' ');
            why5 = [
            ].join(' ');
            data = this.datatable.row.add([whyform, questionchain, why1, why2, why3, why4, why5, actions]);
            $row = this.datatable.row(data[0]).nodes().to$();

            $row
                .addClass('adding')
                .find('td:last')
                .addClass('actions');
            $row.find('td').eq(0)
                .addClass('whyform');
            $row.find('td').eq(1)
                .addClass('questionchain');
            $row.find('td').eq(2)
                .addClass('why1');
            $row.find('td').eq(3)
                .addClass('why2');
            $row.find('td').eq(4)
                .addClass('why3');
            $row.find('td').eq(5)
                .addClass('why4');
            $row.find('td').eq(6)
                .addClass('why5');

            this.rowEdit($row);

            this.datatable.draw(); // always show fields
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
                else if ($this.hasClass('whyform')) {
                    var id = $(data[i]).attr("data-id") || 0;
                    var whatform = $(data[i]).html() || '';
                    $this.html("<label class=\"control-label\" data-id=\"" + id + "\">" + whatform + "</label>");
                }
                else {
                    $this.html('<input type="text" class="form-control input-block" value="' + data[i] + '"/>');
                }
            });
        },

        rowSave: function ($row, whyAnalysisInfo) {
            var _self = this,
                $actions,
                values = [];

            if ($row.hasClass('adding')) {
                this.$addButton1.removeAttr('disabled');
                $row.removeClass('adding');
            }

            values = $row.find('td').map(function () {
                var $this = $(this);

                if ($this.hasClass('actions')) {
                    _self.rowSetActionsDefault($row);
                    return _self.datatable.cell(this).data();
                }
                else if ($this.hasClass('whyform')) {
                    return $.trim("<label class=\"control-label\" data-id=\"" + whyAnalysisInfo.Id + "\">" + whyAnalysisInfo.WhyFrom + "</label >");
                }
                else {
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
        rowRecordDelete: function (id) {
            if (!id) {
                return false;
            }
            var params = {
                Id: id,
            };
            $.ajax({
                type: "Post",
                url: "InvalidActionWhyanalysis",
                async: true,
                data: JSON.stringify(params),
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (data) {
                }
            });
        },
        rowRemove: function ($row) {
            if ($row.hasClass('adding')) {
                this.$addButton1.removeAttr('disabled');
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

        setActionBtn: function (flag) {
            if (flag == 1) {
                $("#whyanalysisbody1").find("tr").each(function (index, item) {
                    //$(item).find(".on-editing").each(function (index2, item2) {
                    //    $(item2).removeClass("hidden");
                    //});
                    $(item).find(".on-default").each(function (index2, item2) {
                        $(item2).removeClass("hidden");
                    });
                });
            }
            else if (flag == 0) {
                $("#whyanalysisbody1").find("tr").each(function (index, item) {
                    $(item).find(".on-editing").each(function (index2, item2) {
                        $(item2).addClass("hidden");
                    });
                    $(item).find(".on-default").each(function (index2, item2) {
                        $(item2).addClass("hidden");
                    });
                });
            }
        },

        initialdata: function (actionwhyanalysisidata) {
            var _self = this;
            var datahtml = "";
            WhyAnalysis1.datatable.clear();
            $("#whyanalysisbody").html(datahtml);
            for (var i = 0; i < actionwhyanalysisidata.length; i++) {
                if (actionwhyanalysisidata[i].PAWWhyForm == WhyAnalysis1.options.whyQuestion) {
                    var tr = "<tr role=\"row\" class=\"odd\">" +
                        "<td class=\"whyform\"><label class=\"control-label\" data-id=\"" + actionwhyanalysisidata[i].Id + "\">" + actionwhyanalysisidata[i].PAWWhyForm + "</label></td>" +
                        "<td class=\"questionchain\">" + actionwhyanalysisidata[i].PAWWhyQuestionChain + "</td>" +
                        "<td class=\"why1\">" + actionwhyanalysisidata[i].PAWWhy1 + "</td>" +
                        "<td class=\"why2\">" + actionwhyanalysisidata[i].PAWWhy2 + "</td>" +
                        "<td class=\"why3\">" + actionwhyanalysisidata[i].PAWWhy3 + "</td>" +
                        "<td class=\"why4\">" + actionwhyanalysisidata[i].PAWWhy4 + "</td>" +
                        "<td class=\"why5\">" + actionwhyanalysisidata[i].PAWWhy5 + "</td>" +
                        "<td class=\"actions\"><a href=\"#\" class=\"hidden on-editing save-row\"><i class=\"fa fa-save\"></i></a>" +
                        " <a href=\"#\" class=\"hidden on-editing cancel-row\"><i class=\"fa fa-times\"></i></a>" +
                        " <a href=\"#\" class=\"on-default edit-row\"><i class=\"fa fa-pencil\"></i></a>" +
                        " <a href=\"#\" class=\"on-default remove-row\"><i class=\"fa fa-trash-o\"></i></a></td></tr>";
                    WhyAnalysis1.datatable.row.add($(tr));
                }
            }
            WhyAnalysis1.datatable.draw();
        }
    };


    var WhyAnalysis2 = {
        options: {
            addButton: '#addwhyanalysis2',
            table: '#datatable-editable-whyanalysis2',
            problemId: "#proId",
            problemNo: "#problemno",
            whyQuestion: "2) Why wasn't it detected?",
            dialog: {
                wrapper: '#dialog-whyanalysis2',
                cancelButton: '#dialogCancel-whyanalysis2',
                confirmButton: '#dialogConfirm-whyanalysis2',
            }
        },


        initialize: function () {
            WhyAnalysis2.setVars()
                .build()
                .events();
        },

        setVars: function () {
            this.$table2 = $(this.options.table);
            this.$addButton2 = $(this.options.addButton);
            this.$problemId = $(this.options.problemId);
            this.$problemNo = $(this.options.problemNo);

            // dialog
            this.dialog = {};
            this.dialog.$wrapper = $(this.options.dialog.wrapper);
            this.dialog.$cancel = $(this.options.dialog.cancelButton);
            this.dialog.$confirm = $(this.options.dialog.confirmButton);

            return this;
        },

        build: function () {
            this.datatable = this.$table2.DataTable({
                aoColumns: [
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    { "bSortable": false }
                ],
                bPaginate: false,
                iDisplayLength: 1000, //默认显示的记录数 
                bInfo: false,
                bFilter: false,
            });

            window.dt = this.datatable;

            return this;
        },

        events: function () {
            var _self = this;

            this.$table2
                .on('click', 'a.save-row', function (e) {
                    e.preventDefault();
                    var _event = this;
                    var data = $(_event).closest('tr').find('td').map(function () {
                        var $this = $(this);
                        if ($this.hasClass('actions')) {
                            return _self.datatable.cell(this).data();
                        }
                        else if ($this.hasClass('whyform')) {
                            var WhyAnalysis2 = {
                                Id: $this.find('label').attr("data-id") || 0,
                                WhyForm: $this.find('label').html() || ''
                            }
                            return WhyAnalysis2;
                        }
                        else {
                            return $.trim($this.find('input').val());
                        }
                    });
                    var params = {
                        Id: data[0].Id || 0,
                        PAWProblemId: _self.$problemId.val() || 0,
                        PAWProblemNo: _self.$problemNo.html() || '',
                        PAWWhyForm: data[0].WhyForm || '',
                        PAWWhyQuestionChain: data[1] || '',
                        PAWWhy1: data[2] || '',
                        PAWWhy2: data[3] || '',
                        PAWWhy3: data[4] || '',
                        PAWWhy4: data[5] || '',
                        PAWWhy5: data[6] || '',
                    };

                    $.ajax({
                        type: "Post",
                        url: "SaveActionWhyanalysis",
                        async: true,
                        data: JSON.stringify(params),
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            if (data != null) {
                                if (data.IsSuccess == true) {
                                    var whyAnalysisInfo = {
                                        Id: data.data.Id || 0,
                                        WhyFrom: data.data.PAWWhyForm || ''
                                    };
                                    WhyAnalysis2.rowSave($(_event).closest('tr'), whyAnalysisInfo);
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
                .on('click', 'a.remove-row', function (e) {
                    e.preventDefault();
                    var $row = $(this).closest('tr');

                    var dataId = $row.find('.whyform').find('label').attr('data-id') || 0;

                    $.magnificPopup.open({
                        items: {
                            src: '#dialog-whyanalysis2',
                            type: 'inline'
                        },
                        preloader: false,
                        modal: true,
                        callbacks: {
                            change: function () {
                                _self.dialog.$confirm.on('click', function (e) {
                                    e.preventDefault();
                                    _self.rowRecordDelete(dataId);
                                    _self.rowRemove($row);
                                    $.magnificPopup.close();
                                });
                            },
                            close: function () {
                                _self.dialog.$confirm.off('click');
                            }
                        }
                    });
                });

            this.$addButton2.on('click', function (e) {
                e.preventDefault();
                _self.rowAdd();
            });

            this.dialog.$cancel.on('click', function (e) {
                e.preventDefault();
                $.magnificPopup.close();
            });
            return this;
        },

        // ==========================================================================================
        // ROW FUNCTIONS
        // ==========================================================================================
        rowAdd: function () {
            this.$addButton2.attr({ 'disabled': 'disabled' });

            var actions,
                whyform,
                questionchain,
                why1,
                why2,
                why3,
                why4,
                why5,
                data,
                $row;

            actions = [
                '<a href="#" class="hidden on-editing save-row"><i class="fa fa-save"></i></a>',
                '<a href="#" class="hidden on-editing cancel-row"><i class="fa fa-times"></i></a>',
                '<a href="#" class="on-default edit-row"><i class="fa fa-pencil"></i></a>',
                '<a href="#" class="on-default remove-row"><i class="fa fa-trash-o"></i></a>',
            ].join(' ');
            whyform = [
                '<label class="control-label" data-id="0">' + this.options.whyQuestion + '</label>',
            ].join(' ');
            questionchain = [
            ].join(' ');
            why1 = [
            ].join(' ');
            why2 = [
            ].join(' ');
            why3 = [
            ].join(' ');
            why4 = [
            ].join(' ');
            why5 = [
            ].join(' ');
            data = this.datatable.row.add([whyform, questionchain, why1, why2, why3, why4, why5, actions]);
            $row = this.datatable.row(data[0]).nodes().to$();

            $row
                .addClass('adding')
                .find('td:last')
                .addClass('actions');
            $row.find('td').eq(0)
                .addClass('whyform');
            $row.find('td').eq(1)
                .addClass('questionchain');
            $row.find('td').eq(2)
                .addClass('why1');
            $row.find('td').eq(3)
                .addClass('why2');
            $row.find('td').eq(4)
                .addClass('why3');
            $row.find('td').eq(5)
                .addClass('why4');
            $row.find('td').eq(6)
                .addClass('why5');

            this.rowEdit($row);

            this.datatable.draw(); // always show fields
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
                else if ($this.hasClass('whyform')) {
                    var id = $(data[i]).attr("data-id") || 0;
                    var whatform = $(data[i]).html() || '';
                    $this.html("<label class=\"control-label\" data-id=\"" + id + "\">" + whatform + "</label>");
                }
                else {
                    $this.html('<input type="text" class="form-control input-block" value="' + data[i] + '"/>');
                }
            });
        },

        rowSave: function ($row, whyAnalysisInfo) {
            var _self = this,
                $actions,
                values = [];

            if ($row.hasClass('adding')) {
                this.$addButton2.removeAttr('disabled');
                $row.removeClass('adding');
            }

            values = $row.find('td').map(function () {
                var $this = $(this);

                if ($this.hasClass('actions')) {
                    _self.rowSetActionsDefault($row);
                    return _self.datatable.cell(this).data();
                }
                else if ($this.hasClass('whyform')) {
                    return $.trim("<label class=\"control-label\" data-id=\"" + whyAnalysisInfo.Id + "\">" + whyAnalysisInfo.WhyFrom + "</label >");
                }
                else {
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
        rowRecordDelete: function (id) {
            if (!id) {
                return false;
            }
            var params = {
                Id: id,
            };
            $.ajax({
                type: "Post",
                url: "InvalidActionWhyanalysis",
                async: true,
                data: JSON.stringify(params),
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (data) {
                }
            });
        },
        rowRemove: function ($row) {
            if ($row.hasClass('adding')) {
                this.$addButton2.removeAttr('disabled');
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

        setActionBtn: function (flag) {
            if (flag == 1) {
                $("#whyanalysisbody2").find("tr").each(function (index, item) {
                    //$(item).find(".on-editing").each(function (index2, item2) {
                    //    $(item2).removeClass("hidden");
                    //});
                    $(item).find(".on-default").each(function (index2, item2) {
                        $(item2).removeClass("hidden");
                    });
                });
            }
            else if (flag == 0) {
                $("#whyanalysisbody2").find("tr").each(function (index, item) {
                    $(item).find(".on-editing").each(function (index2, item2) {
                        $(item2).addClass("hidden");
                    });
                    $(item).find(".on-default").each(function (index2, item2) {
                        $(item2).addClass("hidden");
                    });
                });
            }
        },

        initialdata: function (actionwhyanalysisidata) {
            var _self = this;
            var datahtml = "";
            WhyAnalysis2.datatable.clear();
            $("#whyanalysisbody").html(datahtml);
            for (var i = 0; i < actionwhyanalysisidata.length; i++) {
                if (actionwhyanalysisidata[i].PAWWhyForm == WhyAnalysis2.options.whyQuestion) {
                    var tr = "<tr role=\"row\" class=\"odd\">" +
                        "<td class=\"whyform\"><label class=\"control-label\" data-id=\"" + actionwhyanalysisidata[i].Id + "\">" + actionwhyanalysisidata[i].PAWWhyForm + "</label></td>" +
                        "<td class=\"questionchain\">" + actionwhyanalysisidata[i].PAWWhyQuestionChain + "</td>" +
                        "<td class=\"why1\">" + actionwhyanalysisidata[i].PAWWhy1 + "</td>" +
                        "<td class=\"why2\">" + actionwhyanalysisidata[i].PAWWhy2 + "</td>" +
                        "<td class=\"why3\">" + actionwhyanalysisidata[i].PAWWhy3 + "</td>" +
                        "<td class=\"why4\">" + actionwhyanalysisidata[i].PAWWhy4 + "</td>" +
                        "<td class=\"why5\">" + actionwhyanalysisidata[i].PAWWhy5 + "</td>" +
                        "<td class=\"actions\"><a href=\"#\" class=\"hidden on-editing save-row\"><i class=\"fa fa-save\"></i></a>" +
                        " <a href=\"#\" class=\"hidden on-editing cancel-row\"><i class=\"fa fa-times\"></i></a>" +
                        " <a href=\"#\" class=\"on-default edit-row\"><i class=\"fa fa-pencil\"></i></a>" +
                        " <a href=\"#\" class=\"on-default remove-row\"><i class=\"fa fa-trash-o\"></i></a></td></tr>";
                    WhyAnalysis2.datatable.row.add($(tr));
                }
            }
            WhyAnalysis2.datatable.draw();
        }
    };
    window.InitActionWhyAnalysis1 = WhyAnalysis1.initialize;
    window.InitActionWhyAnalysisData1 = WhyAnalysis1.initialdata;
    window.InitActionWhyAnalysis2 = WhyAnalysis2.initialize;
    window.InitActionWhyAnalysisData2 = WhyAnalysis2.initialdata;
    window.SetActionWhyAnalysis1Btn = WhyAnalysis1.setActionBtn;
    window.SetActionWhyAnalysis2Btn = WhyAnalysis2.setActionBtn;
})(window, jQuery)


