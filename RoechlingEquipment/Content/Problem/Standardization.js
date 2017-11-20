;
(function (window, jQuery, undefined) {

    var StandardizationTable = {
        options: {
            addButton: '#addToTable',
            table: '#datatable-editable-standardization',
            problemId: "#proId",
            problemNo: "#problemno",
            approveButton: '#approveStandardization',
            rejectButton: '#rejectStandardization',
        },

        initialize: function () {
            StandardizationTable.setVars()
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

            this.$table
                .on('click', 'a.save-row', function (e) {
                    e.preventDefault();
                    var _event = this;
                    var data = $(_event).closest('tr').find('td').map(function () {
                        var $this = $(this);
                        if ($this.hasClass('actions')) {
                            return _self.datatable.cell(this).data();
                        }
                        else if ($this.hasClass('itemname')) {
                            var actibityInfo = {
                                Id: $this.find('label').attr("data-id") || 0,
                                ItemNameNo: $this.find('label').attr("data-no") || 0,
                                ItemName: $this.find('label').html() || '',
                            }
                            return actibityInfo;
                        }
                        else if ($this.hasClass('needupdate')) {
                            return $this.find('.needupdate').is(':checked') ? 1 : 0;
                        }
                        else if ($this.hasClass('attachment')) {
                            var attachdiv = $this.find('.uploadAttachment');
                            var attach = {
                                name: attachdiv.find('.fileinfo').html() || '',
                                url: attachdiv.find('.fileinfo').attr('data-value') || '',
                            }
                            return attach;
                        }
                        else {
                            return $.trim($this.find('input').val());
                        }
                    });

                    var params = {
                        Id: data[0].Id || 0,
                        PSProblemId: _self.$problemId.val() || 0,
                        PSProblemNo: _self.$problemNo.html() || '',
                        PSItemName: data[0].ItemName || '',
                        PSItemNameNo: data[0].ItemNameNo || 0,
                        PSNeedUpdate: data[1] || 0,
                        PSWhoNo: data[2] || '',
                        PSWho: data[2] || '',
                        PSPlanDate: data[3],
                        PSActualDate: data[4],
                        PSDocNo: data[5],
                        PSChangeContent: data[6],
                        PSOldVersion: data[7],
                        PSNewVersion: data[8],
                        PSAttachment: data[9].name || 'D',
                        PSAttachmentUrl: data[9].url || 'D',
                    };

                    $.ajax({
                        type: "Post",
                        url: "SaveStandardization",
                        async: true,
                        data: JSON.stringify(params),
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            if (data != null) {
                                if (data.IsSuccess == true) {
                                    var standardizationInfo = {
                                        Id: data.data.Id || 0,
                                        ItemName: data.data.PSItemName || '',
                                        ItemNameNo: data.data.PSItemNameNo || 0,
                                        NeedUpdate: data.data.PSNeedUpdate || 0,
                                    };
                                    var attachinfo = {
                                        Attachname: (data.data.PSAttachment == "D" ? '' : data.data.PSAttachment) || '',
                                        Attachurl: (data.data.PSAttachmentUrl == "D" ? '' : data.data.PSAttachmentUrl) || '',
                                        Attachdownurl: data.data.PSAttachmentDownloadUrl || '',
                                    };
                                    StandardizationTable.rowSave($(_event).closest('tr'), standardizationInfo, attachinfo);
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

                if (IfHasRole("BTN_STANDA_REJECT")) {
                    _self.$rejectButton.removeClass("hidden");
                }

                var params = {
                    Id: $("#proId").val() || 0,
                    PIProblemNo: $("#problemno").html() || '',
                    PIApproveStandardization: 1,
                };
                $.ajax({
                    type: "Post",
                    url: "SaveNewProblem",
                    async: true,
                    data: JSON.stringify(params),
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data != null) {
                            if (data.IsSuccess == true) {
                                $("#approveStandardizationflag").val(1);
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
                if (IfHasRole("BTN_STANDA_APPROVE")) {
                    _self.$approveButton.removeClass("hidden");
                }

                var params = {
                    Id: $("#proId").val() || 0,
                    PIProblemNo: $("#problemno").html() || '',
                    PIApproveStandardization: 0,
                };
                $.ajax({
                    type: "Post",
                    url: "SaveNewProblem",
                    async: true,
                    data: JSON.stringify(params),
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data != null) {
                            if (data.IsSuccess == true) {
                                $("#approveStandardizationflag").val(0);
                                SetStandardizationBtn(1);
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


            data = this.datatable.row.add(['', '', '', actions]);
            $row = this.datatable.row(data[0]).nodes().to$();

            $row
                .addClass('adding')
                .find('td:last')
                .addClass('actions');


            this.rowEdit($row);

            this.datatable.order([0, 'asc']).draw(); // always show fields
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
                else if ($this.hasClass('itemname')) {
                    $this.html(data[i]);
                }
                else if ($this.hasClass('needupdate')) {
                    var check = data[i] == "Yes" ? "checked=\"checked\"" : "";
                    $this.html('<div class="checkbox-custom checkbox-inline"><input type="checkbox" name="needupdate" class="needupdate" ' + check + '><label for="isleader"> </label></div >');
                }
                else if ($this.hasClass('plandate')) {
                    $this.html('<input type="text" class="col-xs-2 form-control width140 dateinput" name="plandate" value="' + data[i] + '"/>');
                }
                else if ($this.hasClass('actualdate')) {
                    $this.html('<input type="text" class="col-xs-2 form-control width140 dateinput" name="actualdate" value="' + data[i] + '"/>');
                }
                else if ($this.hasClass('attachment')) {
                    var attachname = $(data[i]).html() || '';
                    var attachurl = $(data[i]).attr("data-value") || '';
                    var attachdownurl = $(data[i]).attr("data-download") || '';
                    var noneclass = ' none';
                    if (attachname == '') {
                        noneclass = '';
                    }
                    var attachhtml = '<div class="uploadAttachment">' +
                        '<button class="bk-margin-5 btn btn-default' + noneclass + '" type= "button"><i class="fa fa-paperclip"></i> upload</button>' +
                        '<input type="file" class="uploadfileButton' + noneclass + '" />';
                    if (attachname != '') {
                        attachhtml += "<a href=\"javascript:\"><i class=\"dropattachment glyphicon glyphicon-remove\"></i></a><a href=\"" + attachdownurl + "\" data-value=\"" + attachurl + "\" data-download=\"" + attachdownurl + "\" class=\"fileinfo\">" + attachname + "</a >";
                    }
                    attachhtml += '</div>';
                    $this.html(attachhtml);
                }
                else {
                    $this.html('<input type="text" class="form-control input-block" value="' + data[i] + '"/>');
                }
            });
            this.datePicker($row);
            this.attachmentUpload($row);
        },

        rowSave: function ($row, standardizationInfo, attachinfo) {
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
                else if ($this.hasClass('itemname')) {
                    return $.trim("<label class=\"control-label\" data-id=\"" + standardizationInfo.Id + "\" data-no=\"" + standardizationInfo.ItemNameNo + "\">" + standardizationInfo.ItemName + "</label >");
                }
                else if ($this.hasClass('needupdate')) {
                    if ($this.find(".needupdate").attr('checked'))
                        return "Yes";
                    else
                        return "No";
                }
                else if ($this.hasClass('attachment')) {
                    return $.trim("<a href=\"" + attachinfo.Attachdownurl + "\" data-value=\"" + attachinfo.Attachurl + "\" data-download=\"" + attachinfo.Attachdownurl + "\" class=\"fileinfo\">" + attachinfo.Attachname + "</a >");
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

        attachmentUpload: function ($row) {
            $row.find(".uploadAttachment").each(function (index, element) {
                var _attachment = this;
                $(_attachment).off('change');
                $(_attachment).on('change', function () {
                    var _self = this;
                    var file = $(this).find(".uploadfileButton")[0];
                    if (file.files && file.files[0]) {
                        var arr = file.value.split('.');
                        var fileType = arr[arr.length - 1];
                        var fileSize = file.files[0].size;
                        var fileName = file.files[0].name;
                        if ("exe|bat".indexOf(fileType) > -1 || fileSize > 5242880) {
                            alert("不支持exe、bat格式,大小小于5M");
                            return;
                        }
                        var url = basePath + culture + '/Home/AttachmentUpload';
                        $(_attachment).children().each(function (index, item) {
                            $(item).addClass('none');
                        });
                        var fileObj = file.files[0];
                        var form = new FormData();
                        form.append("file", fileObj);
                        var xhr = new XMLHttpRequest();
                        xhr.open("post", url, true);
                        xhr.onload = function (result) {
                            var data = JSON.parse(result.currentTarget.response);
                            if (data.data && data.data.length > 0) {
                                var filedom = "<a href=\"javascript:\"><i class=\"dropattachment glyphicon glyphicon-remove\"></i></a><a href=\"" + data.data[0].FileUrl + "\" data-value=\"" + data.data[0].FileLocalPath + "\" data-download=\"" + data.data[0].FileUrl + "\" class=\"fileinfo\">" + data.data[0].FileName + "</a >";
                                $(_self).append(filedom);
                                $(".dropattachment").off('click');
                                $(".dropattachment").on('click', function () {
                                    var drop = this;
                                    $(drop).parent().prev("a").remove();
                                    $(drop).parent().prev("input").removeClass('none');
                                    $(drop).parent().prev().prev("button").removeClass('none');
                                    $(drop).parent().remove();
                                    StandardizationTable.attachmentUpload();
                                    return false;
                                });
                            }
                        };
                        xhr.send(form);
                    }
                });

                $(_attachment).find(".dropattachment").off('click');
                $(_attachment).find(".dropattachment").on('click', function () {
                    var drop = this;
                    $(drop).parent().prev("a").remove();
                    $(drop).parent().prev("input").removeClass('none');
                    $(drop).parent().prev().prev("button").removeClass('none');
                    $(drop).parent().remove();
                    StandardizationTable.attachmentUpload();
                    return false;
                });
            });
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
                $("#standardizationbody").find("tr").each(function (index, item) {
                    //$(item).find(".on-editing").each(function (index2, item2) {
                    //    $(item2).removeClass("hidden");
                    //});
                    $(item).find(".on-default").each(function (index2, item2) {
                        $(item2).removeClass("hidden");
                    });
                });
            }
            else if (flag == 0) {
                $("#standardizationbody").find("tr").each(function (index, item) {
                    $(item).find(".on-editing").each(function (index2, item2) {
                        $(item2).addClass("hidden");
                    });
                    $(item).find(".on-default").each(function (index2, item2) {
                        $(item2).addClass("hidden");
                    });
                });
            }
        },

        initialdata: function (standardizationdata) {
            for (var i = 0; i < standardizationdata.length; i++) {
                var no = standardizationdata[i].PSItemNameNo;
                var needupdate = standardizationdata[i].PSNeedUpdate == 1 ? "Yes" : "No";
                var td = $("#standardizationbody").find("[data-no='" + no + "']").parent();
                td.next().html(needupdate);

                td.next().next().html(standardizationdata[i].PSWho);
                td.next().next().next().html(standardizationdata[i].PSPlanDateDesc);
                td.next().next().next().next().html(standardizationdata[i].PSActualDateDesc);
                td.next().next().next().next().next().html(standardizationdata[i].PSDocNo);
                td.next().next().next().next().next().next().html(standardizationdata[i].PSChangeContent);
                td.next().next().next().next().next().next().next().html(standardizationdata[i].PSOldVersion);
                td.next().next().next().next().next().next().next().next().html(standardizationdata[i].PSNewVersion);

                var attchmenttd = "<a href=\"" + standardizationdata[i].PSAttachmentDownloadUrl + "\" data-value=\"" + standardizationdata[i].PSAttachmentUrl + "\" data-download=\"" + standardizationdata[i].PSAttachmentDownloadUrl + "\" class=\"fileinfo\">" + standardizationdata[i].PSAttachment + "</a >";

                td.next().next().next().next().next().next().next().next().next().append(attchmenttd);

                var tr = td.parent();
                var trstring = tr.prop("outerHTML");

                StandardizationTable.datatable.row(tr).remove();
                StandardizationTable.datatable.row.add($(trstring));
                StandardizationTable.datatable.draw();
            }
        }
    };

    window.InitStandardization = StandardizationTable.initialize;
    window.InitStandardizationData = StandardizationTable.initialdata;
    window.SetStandardizationBtn = StandardizationTable.setActionBtn;
})(window, jQuery)


