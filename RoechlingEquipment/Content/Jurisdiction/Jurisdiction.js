//闭包（避免全局污染）
//加上分号（防止压缩出错）
;
(function (window, jQuery, undefined) {
    var config = {};
    var pageIndex = 1;

    var events = {
        SearchResult: function (index) {
            index = index || pageIndex;
            var params = {
                CurrentPage: index,
                JobNumber: $("#txtjobNumber").val(),
                Name: $("#txtName").val(),
                Position: $("#txtPosition").val()
            }
            var url = basePath + culture + '/Jurisdiction/JurisdictionSearch';
            $.post(url, params, function (data) {
                $("#dataResult").empty().append(data);
            })
        },
        EditJurisdiction: function (userId) {
            $("#UserId").val(userId);
            $("#editJurisdiction").trigger("click");
        },
        Save: function (event) {
            event.preventDefault();
            var userId = $("#UserId").val();
            var roleGroupId = [];
            $(".tag").each(function () {
                var roleId = $(this).children().attr("data-groupid");
                roleGroupId.push(roleId);
            });
            $("#JurisdictionSave").attr("disabled", "disabled");
            var params = {
                UserId: userId,
                RoleGroupIds: roleGroupId
            }
            //$.ajax({
            //    type: 'post',
            //    url: basePath + culture + '/Jurisdiction/Save',
            //    dataType: "json",
            //    data: {
            //        paraStr: JSON.stringify(params)
            //    },
            //    sucess: function (data) {
            //        $("#JurisdictionSave").removeAttr("disabled");
            //        $.magnificPopup.close();
            //        if (data.IsSuccess == true) {
            //            alert("Save Success");
            //        } else
            //        {
            //            alert(data.Message);
            //        }
            //    }
            //});
            var url = basePath + culture + '/Jurisdiction/Save';
            var paraStr = { paraStr: JSON.stringify(params) };
            $.post(url, paraStr, function (data) {
                $("#JurisdictionSave").removeAttr("disabled");
                $("#UserId").val("");
                if (data.IsSuccess) {
                    alert("Save Success");
                    window.location.reload();
                } else {
                    alert(data.Message);
                }
                $.magnificPopup.close();
            });
        },
    };

    var page = {
        config: {},

        Init: function (fig) {
            config = fig;
            page.InitEvents();
        },

        InitEvents: function () {
            events.SearchResult(1);

            $("#editJurisdiction").magnificPopup({
                type: 'inline',
                preloader: false,
                focus: '#name',
                modal: true,
                callbacks: {
                    beforeOpen: function () {
                        if ($(window).width() < 700) {
                            this.st.focus = false;
                        } else {
                            this.st.focus = '#name';
                        }

                        $("#JurisdictionReset").trigger("click");
                        $(".bootstrap-tagsinput").empty();
                    },
                    open: function () {
                        var userId = $("#UserId").val();
                        $.ajax({
                            type: 'post',
                            url: basePath + culture + '/Jurisdiction/InitJurisdiction',
                            dataType: "json",
                            data: {
                                userId: userId
                            },
                            success: function (data) {
                                $("#txtName").val(data.GroupName);
                                if (data.UserList != null)
                                {
                                    for (var i in data.UserList)
                                    {
                                        var html = "<span class='tag label label-primary'>" + data.UserList[i].UserName + "(" + data.UserList[i].JobNumber + ")<span data-role='remove' data-groupId=" + data.UserList[i].GroupId + "></span></span>";
                                        $(".bootstrap-tagsinput").append(html);
                                        //$("#roleGroup").find("option[value='" + data.RoleGroupList[i].RoleGroupId +"']").remove();
                                    }
                                }
                            }
                        });
                    },
                }
            });

            $("#JurisdictionSave").on('click', events.Save);

            $("#btnsearch").on('click', function () {
                events.SearchResult(1);
            });
        },
    };

    window.PageInit = page.Init;
    window.SearchResult = events.SearchResult;
    window.EditJurisdiction = events.EditJurisdiction
})(window, jQuery)