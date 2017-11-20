
(function (window, jQuery, undefined) {

    var SolvingTeamTable = {
        options: {
            addButton: '#addsolvingteam',
            approveButton: '#approveSolvingTeam',
            rejectButton: '#rejectSolvingTeam',
            table: '#datatable-solvingteam',
            jobNum: "#jobnum",
            problemId: "#proId",
            problemNo: "#problemno",
            dialog: {
                wrapper: '#dialog-solvingteam',
                cancelButton: '#dialogCancel-solvingteam',
                confirmButton: '#dialogConfirm-solvingteam',
            }
        },

        userInfo: {
            jobNum: "",
            phone: "",
            department: "",
            title: "",
            userName: "",
            isleader: 0,
        },

        initialize: function () {
            SolvingTeamTable.setVars()
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
                        } else if ($this.hasClass('isleader')) {
                            return $this.find('.isleader').is(':checked') ? 1 : 0;
                        }
                        else if ($this.hasClass('membername')) {
                            var userinfo = {
                                stId: $this.find('label').attr('data-id') || 0,
                                jobnum: $this.find('label').attr('data-jobno') || '',
                                name: $this.find('label').html() || '',
                            };
                            return userinfo;
                        }
                        else {
                            return $.trim($this.find('label').html());
                        }
                    });

                    var params = {
                        Id: data[0].stId,
                        PSProblemId: _self.$problemId.val() || 0,
                        PSProblemNo: _self.$problemNo.html() || '',
                        PSUserNo: data[0].jobnum,
                        PSUserName: data[0].name,
                        PSDeskEXT: data[1] || '',
                        PSDeptId: data[2] || 0,
                        PSDeptName: data[2] || '',
                        PSUserTitle: data[3] || '',
                        PSIsLeader: data[4],
                    };

                    $.ajax({
                        type: "Post",
                        url: "SaveSolvingTeam",
                        async: true,
                        data: JSON.stringify(params),
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            if (data != null) {
                                if (data.IsSuccess == true) {
                                    var userinfo = {
                                        Id: data.data.Id || 0,
                                    };
                                    SolvingTeamTable.rowSave($(_event).closest('tr'), userinfo);
                                    InitAssign();
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

                    var dataId = $row.find('.membername').find('label').attr('data-id') || 0;

                    $.magnificPopup.open({
                        items: {
                            src: '#dialog-solvingteam',
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
                $("#solvingteamuserSearch").val('');
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
                                _self.userInfo.phone = data.BUPhoneNum || "";
                                _self.userInfo.department = data.BUDepartName || "";
                                _self.userInfo.title = data.BUTitle || "";
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
                //$("#rejectProblem").addClass("hidden");
                if (IfHasRole("BTN_SOLVTEAM_REJECT")) {
                    _self.$rejectButton.removeClass("hidden");
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
            this.$rejectButton.on('click', function () {
                _self.$rejectButton.addClass("hidden");
                if (IfHasRole("BTN_SOLVTEAM_ADD")) {
                    _self.$addButton.removeClass("hidden");
                }
                if (IfHasRole("BTN_SOLVTEAM_APPROVE")) {
                    _self.$approveButton.removeClass("hidden");
                }
                var params = {
                    Id: $("#proId").val() || 0,
                    PIProblemNo: $("#problemno").html() || '',
                    PIProcessStatus: 101,
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
                                $("#proProcessStatus").val(101);
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
                isleader,
                membername,
                deskext,
                department,
                title,
                data,
                $row;

            actions = [
                '<a href="#" class="hidden on-editing save-row"><i class="fa fa-save"></i></a>',
                '<a href="#" class="hidden on-editing cancel-row"><i class="fa fa-times"></i></a>',
                '<a href="#" class="on-default edit-row"><i class="fa fa-pencil"></i></a>',
                '<a href="#" class="on-default remove-row"><i class="fa fa-trash-o"></i></a>'
            ].join(' ');

            membername = [
                '<label class="control-label stuserinfo" data-id="0" data-jobno="' + this.userInfo.jobNum + '">' + this.userInfo.userName + '</label>',
            ].join(' ');

            deskext = [
                this.userInfo.phone,
            ].join(' ');

            department = [
                this.userInfo.department,
            ].join(' ');

            title = [
                this.userInfo.title,
            ].join(' ');

            isleader = [
                '<div class="checkbox-custom checkbox-inline"><input type="checkbox" name="isleader" class="isleader"></div>',
            ].join(' ');

            data = this.datatable.row.add([membername, deskext, department, title, isleader, actions]);
            $row = this.datatable.row(data[0]).nodes().to$();

            $row
                .addClass('adding')
                .find('td:last')
                .addClass('actions');
            $row.find('td').eq(0)
                .addClass('membername');
            $row.find('td').eq(1)
                .addClass('deskext');
            $row.find('td').eq(2)
                .addClass('department');
            $row.find('td').eq(3)
                .addClass('title');
            $row.find('td').eq(4)
                .addClass('isleader');

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
                else if ($this.hasClass('isleader')) {
                    var check = data[i] == "Yes" ? "checked=\"checked\"" : "";
                    $this.html('<div class="checkbox-custom checkbox-inline"><input type="checkbox" name="isleader" class="isleader" ' + check + '><label for="isleader"> </label></div >');
                }
                else if ($this.hasClass('membername')) {
                    $this.html(data[i]);
                }
                else if ($this.hasClass('deskext') ||
                    $this.hasClass('department') ||
                    $this.hasClass('title')) {
                    $this.html('<label class="control-label">' + data[i] + '</label>');
                }
                else {
                    $this.html('<input type="text" class="form-control input-block" value="' + data[i] + '"/>');
                }
            });
        },

        rowSave: function ($row, userinfo) {
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
                } else if ($this.hasClass('isleader')) {
                    if ($this.find(".isleader").attr('checked'))
                        return "Yes";
                    else
                        return "No";
                }
                else if ($this.hasClass('membername')) {
                    var rowmembername = $this.find('label');
                    rowmembername.attr("data-id", userinfo.Id);
                    return $.trim($this.find('label').prop("outerHTML"));
                }
                else {
                    return $.trim($this.find('label').html());
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
                url: "InvalidSolvingTeam",
                async: true,
                data: JSON.stringify(params),
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (data) {
                    InitAssign();
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
        // ==========================================================================================
        // OTHER FUNCTIONS
        // ==========================================================================================
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
                            data: { "key": $("#solvingteamuserSearch").val() },
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
                        $("#solvingteamuserSearch").val(ui.item.label);
                        $("#jobnum").html(ui.item.value);
                        return false;
                    },
                };
                $("#solvingteamuserSearch").autocomplete(options);
            }
        },

        setActionBtn: function (flag) {
            if (flag == 1)
            {
                $("#solvingteambody").find("tr").each(function (index, item) {
                    //$(item).find(".on-editing").each(function (index2, item2) {
                    //    $(item2).removeClass("hidden");
                    //});
                    $(item).find(".on-default").each(function (index2, item2) {
                        $(item2).removeClass("hidden");
                    });
                });
            }
            else if (flag == 0) {
                $("#solvingteambody").find("tr").each(function (index, item) {
                    $(item).find(".on-editing").each(function (index2, item2) {
                        $(item2).addClass("hidden");
                    });
                    $(item).find(".on-default").each(function (index2, item2) {
                        $(item2).addClass("hidden");
                    });
                });
            }
        },

        initialdata: function (solvingteamdata) {
            var datahtml = "";
            SolvingTeamTable.datatable.clear();
            $("#solvingteambody").html(datahtml);
            for (var i = 0; i < solvingteamdata.length; i++) {
                var isleader = solvingteamdata[i].PSIsLeader == 1 ? "Yes" : "No";
                var tr = "<tr role=\"row\" class=\"odd\">" +
                    "<td class=\"membername\"><label class=\"control-label stuserinfo\" data-id=\"" + solvingteamdata[i].Id + "\" data-jobno=\"" + solvingteamdata[i].PSUserNo + "\">" + solvingteamdata[i].PSUserName + "</label></td >" +
                    "<td class=\"deskext\">" + solvingteamdata[i].PSDeskEXT + "</td>" +
                    "<td class=\"department\">" + solvingteamdata[i].PSDeptName + "</td>" +
                    "<td class=\"title\">" + solvingteamdata[i].PSUserTitle + "</td>" +
                    "<td class=\"isleader\">" + isleader + "</td>" +
                    "<td class=\"actions\"><a href=\"#\" class=\"on-editing save-row hidden\"><i class=\"fa fa-save\"></i></a>" +
                    " <a href=\"#\" class=\"on-editing cancel-row hidden\"><i class=\"fa fa-times\"></i></a>" +
                    " <a href=\"#\" class=\"on-default edit-row\"><i class=\"fa fa-pencil\"></i></a>" +
                    " <a href=\"#\" class=\"on-default remove-row\"><i class=\"fa fa-trash-o\"></i></a></td></tr>";
                SolvingTeamTable.datatable.row.add($(tr));
            }
            SolvingTeamTable.datatable.draw();
        },
    };

    window.InitSolvingTeam = SolvingTeamTable.initialize;
    window.InitSolvingTeamData = SolvingTeamTable.initialdata;
    window.SetSolvingTeamBtn = SolvingTeamTable.setActionBtn;
})(window, jQuery)


