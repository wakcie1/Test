﻿;
(function (window, jQuery, undefined) {

    var QualityAlertTable = {
        options: {
            addButton: '#addToQualityalert',
            table: '#datatable-editable-qualityalert',
            jobNum: "#qajobnum",
            problemId: "#proId",
            problemNo: "#problemno",
            approveButton: '#approveQualityalert',
            rejectButton: '#rejectQualityalert',
            dialog: {
                wrapper: '#dialog-qualityalert',
                cancelButton: '#dialogConfirm-qualityalert',
                confirmButton: '#dialogCancel-qualityalert',
            }
        },

        userInfo: {
            jobNum: "",
            userName: "",
        },

        initialize: function () {
            QualityAlertTable.setVars()
                .build()
                .events()
                .userAutoComplete();
        },

        setVars: function () {
            this.$table = $(this.options.table);
            this.$addButton = $(this.options.addButton);
            this.$jobNum = $(this.options.jobNum);
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
            this.datatable = this.$table.DataTable({
                aoColumns: [
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
                        else if ($this.hasClass('what')) {
                            var what = {
                                Id: $this.find('input').attr("data-id") || 0,
                                Whatvalue: $this.find('input').val() || ''
                            }
                            return what;
                        }
                        else if ($this.hasClass('who')) {
                            var who = {
                                jobnum: $this.find('label').attr('data-jobno') || '',
                                name: $this.find('label').html() || '',
                            }
                            return who;
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
                        PQProblemId: _self.$problemId.val() || 0,
                        PQProblemNo: _self.$problemNo.html() || '',
                        PQWhat: data[0].Whatvalue || '',
                        PQWhoNo: data[1].jobnum,
                        PQWho: data[1].name,
                        PQPlanDate: data[2] || '',
                        PQActualDate: data[3] || '',
                        PQAttachment: data[4].name || 'D',
                        PQAttachmentUrl: data[4].url || 'D',
                    };

                    $.ajax({
                        type: "Post",
                        url: "SaveQualityAlert",
                        async: true,
                        data: JSON.stringify(params),
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            if (data != null) {
                                if (data.IsSuccess == true) {
                                    var userinfo = {
                                        Id: data.data.Id || 0,
                                        What: data.data.PQWhat || '',
                                        JobNo: data.data.PQWhoNo || '',
                                        Name: data.data.PQWho || '',
                                    };
                                    var attachinfo = {
                                        Attachname: (data.data.PQAttachment == "D" ? '' : data.data.PQAttachment) || '',
                                        Attachurl: (data.data.PQAttachmentUrl == "D" ? '' : data.data.PQAttachmentUrl) || '',
                                        Attachdownurl: data.data.PQAttachmentDownloadUrl || '',
                                    };
                                    QualityAlertTable.rowSave($(_event).closest('tr'), userinfo, attachinfo);
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
                    var dataId = $row.find('.what').find('label').attr('data-id') || 0;
                    $.magnificPopup.open({
                        items: {
                            src: '#dialog-qualityalert',
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

            this.$addButton.on('click', function (e) {
                var jobnum = _self.$jobNum.html() || '';
                if (jobnum == '') {
                    alert("please select the member name!");
                    return false;
                }
                _self.userInfo.jobNum = jobnum;
                $("#qauserSearch").val('');
                var userinfourl = $("#userinfourl").val() || '';
                if (userinfourl != '') {
                    $.ajax({
                        url: userinfourl,
                        type: "post",
                        async: false,
                        dataType: "json",
                        data: { "jobNumber": jobnum },
                        success: function (data) {
                            if (data && data.Id > 0) {
                                _self.userInfo.userName = data.BUName || "";
                            }
                            _self.$jobNum.html('');
                        }
                    });
                };
                e.preventDefault();
                _self.rowAdd();
            });

            this.dialog.$cancel.on('click', function (e) {
                e.preventDefault();
                $.magnificPopup.close();
            });
            this.$approveButton.on('click', function () {
                _self.$addButton.addClass("hidden");
                _self.$approveButton.addClass("hidden");
                $("#rejectSolvingTeam").addClass("hidden");
                if (IfHasRole("BTN_QUALALERT_REJECT")) {
                    _self.$rejectButton.removeClass("hidden");
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

            this.$rejectButton.on('click', function () {
                _self.$rejectButton.addClass("hidden");
                if (IfHasRole("BTN_QUALALERT_ADD")) {
                    _self.$addButton.removeClass("hidden");
                }
                if (IfHasRole("BTN_QUALALERT_APPROVE")) {
                    _self.$approveButton.removeClass("hidden");
                }
                var params = {
                    Id: $("#proId").val() || 0,
                    PIProblemNo: $("#problemno").html() || '',
                    PIProcessStatus: 201,
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
                                $("#proProcessStatus").val(201);
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
                what,
                who,
                plandate,
                actualdate,
                attachment,
                data,
                $row;

            actions = [
                '<a href="#" class="hidden on-editing save-row"><i class="fa fa-save"></i></a>',
                '<a href="#" class="hidden on-editing cancel-row"><i class="fa fa-times"></i></a>',
                '<a href="#" class="on-default edit-row"><i class="fa fa-pencil"></i></a>',
                '<a href="#" class="on-default remove-row"><i class="fa fa-trash-o"></i></a>'
            ].join(' ');

            what = [
                '<input type="text" class="col-xs-2 form-control width140" name="plandate"/>'
            ].join(' ');

            who = [
                '<label class="control-label" data-jobno="' + this.userInfo.jobNum + '">' + this.userInfo.userName + '</label>',
            ].join(' ');

            plandate = [

            ].join(' ');

            actualdate = [

            ].join(' ');

            attachment = [
                '<a href="#" data-value="" data-download="" class="fileinfo"></a >'
            ].join(' ');

            data = this.datatable.row.add([what, who, plandate, actualdate, attachment, actions]);
            $row = this.datatable.row(data[0]).nodes().to$();

            $row
                .addClass('adding')
                .find('td:last')
                .addClass('actions');
            $row.find('td').eq(0)
                .addClass('what');
            $row.find('td').eq(1)
                .addClass('who');
            $row.find('td').eq(2)
                .addClass('plandate');
            $row.find('td').eq(3)
                .addClass('actualdate');
            $row.find('td').eq(4)
                .addClass('attachment');

            this.rowEdit($row);

            this.datatable.draw(); // always show fields
            this.datePicker($row);
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
                else if ($this.hasClass('what')) {
                    var id = $(data[i]).attr("data-id") || 0;
                    var whatvalue = $(data[i]).html() || '';
                    $this.html('<input type="text" class="col-xs-2 form-control width140" name="plandate" data-id="' + id + '" value="' + whatvalue + '"/>');
                }
                else if ($this.hasClass('who')) {
                    var jobno = $(data[i]).attr("data-jobno") || '';
                    var name = $(data[i]).html() || '';
                    $this.html('<label class="control-label" data-jobno="' + jobno + '">' + name + '</label>');
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

        rowSave: function ($row, qualityInfo, attachinfo) {
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
                else if ($this.hasClass('what')) {
                    return $.trim("<label class=\"control-label\" data-id=\"" + qualityInfo.Id + "\">" + qualityInfo.What + "</label >");
                }
                else if ($this.hasClass('who')) {
                    return $.trim("<label class=\"control-label\" data-jobno=\"" + qualityInfo.JobNo + "\">" + qualityInfo.Name + "</label >");
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
        rowRecordDelete: function (id) {
            if (!id) {
                return false;
            }
            var params = {
                Id: id,
            };
            $.ajax({
                type: "Post",
                url: "InvalidQualityAlert",
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
                                    QualityAlertTable.attachmentUpload();
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
                    QualityAlertTable.attachmentUpload();
                    return false;
                });
            });
        },

        userAutoComplete: function () {
            var userlisturl = $("#userlisturl").val() || '';
            if (userlisturl != '') {
                var options = {
                    minLength: 0,
                    source: function (request, response) {
                        $.ajax({
                            url: userlisturl,
                            type: "post",
                            dataType: "json",
                            data: { "key": $("#qauserSearch").val() },
                            success: function (data) {
                                response($.map(data, function (item) {
                                    return {
                                        label: item.BUName + "[" + item.BUEnglishName + "](" + item.BUJobNumber + ")",
                                        value: item.BUJobNumber
                                    }
                                }));
                            }
                        });
                    },
                    select: function (event, ui) {
                        $("#qauserSearch").val(ui.item.label);
                        $("#qajobnum").html(ui.item.value);
                        return false;
                    },
                };
                $("#qauserSearch").autocomplete(options);
            }
        },
        setActionBtn: function (flag) {
            if (flag == 1) {
                $("#qualityalertbody").find("tr").each(function (index, item) {
                    //$(item).find(".on-editing").each(function (index2, item2) {
                    //    $(item2).removeClass("hidden");
                    //});
                    $(item).find(".on-default").each(function (index2, item2) {
                        $(item2).removeClass("hidden");
                    });
                });
            }
            else if (flag == 0) {
                $("#qualityalertbody").find("tr").each(function (index, item) {
                    $(item).find(".on-editing").each(function (index2, item2) {
                        $(item2).addClass("hidden");
                    });
                    $(item).find(".on-default").each(function (index2, item2) {
                        $(item2).addClass("hidden");
                    });
                });
            }
        },

        initialdata: function (qualityalert) {
            var datahtml = "";
            QualityAlertTable.datatable.clear();
            $("#qualityalertbody").html(datahtml);
            for (var i = 0; i < qualityalert.length; i++) {
                var tr = "<tr role=\"row\" class=\"odd\">" +
                    "<td class=\"what\"><label class=\"control-label\" data-id=\"" + qualityalert[i].Id + "\">" + qualityalert[i].PQWhat + "</label ></td >" +
                    "<td class=\"who\"><label class=\"control-label\" data-jobno=\"" + qualityalert[i].PQWhoNo + "\">" + qualityalert[i].PQWho + "</label ></td>" +
                    "<td class=\"plandate\">" + qualityalert[i].PQPlanDateDesc + "</td>" +
                    "<td class=\"actualdate\">" + qualityalert[i].PQActualDateDesc + "</td>" +
                    "<td class=\"attachment\"><a href=\"" + qualityalert[i].PQAttachmentDownloadUrl + "\" data-value=\"" + qualityalert[i].PQAttachmentUrl + "\" data-download=\"" + qualityalert[i].PQAttachmentDownloadUrl + "\" class=\"fileinfo\">" + qualityalert[i].PQAttachment + "</a ></td>" +
                    "<td class=\"actions\"><a href=\"#\" class=\"on-editing save-row hidden\"><i class=\"fa fa-save\"></i></a>" +
                    " <a href=\"#\" class=\"on-editing cancel-row hidden\"><i class=\"fa fa-times\"></i></a>" +
                    " <a href=\"#\" class=\"on-default edit-row\"><i class=\"fa fa-pencil\"></i></a>" +
                    " <a href=\"#\" class=\"on-default remove-row \"><i class=\"fa fa-trash-o\"></i></a></td></tr>";
                QualityAlertTable.datatable.row.add($(tr));
            }
            QualityAlertTable.datatable.draw();
        }
    }

    window.InitQualityAlert = QualityAlertTable.initialize;
    window.InitQualityAlertData = QualityAlertTable.initialdata;
    window.SetQualityAlertBtn = QualityAlertTable.setActionBtn;

})(window, jQuery)


