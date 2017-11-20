
//闭包（避免全局污染）
//加上分号（防止压缩出错）
;
(function (window, jQuery, undefined) {
    var config = {};
    var pageIndex = 1;
    var _instance;

    var events = {
        Validate: function () {
            ////验证消息扩展 Todo
            //$.extend($.validator.error, {
            //    required: "required"
            //});
            // validation summary
            var $summaryForm = $("#userAdd");
            $summaryForm.validate({
                errorPlacement: function (error, element) {
                    $(element).parent().append(error);
                },
                wrapper: "div",
                showErrors: function (errorMap, errorList) {
                    // 遍历错误列表
                    for (var obj in errorMap) {
                        // 自定义错误提示效果
                        $('#' + obj).parent().addClass('has-error');
                    }
                    // 此处注意，一定要调用默认方法，这样保证提示消息的默认效果
                    this.defaultShowErrors();
                },
                success: function (label) {
                    $(label).parent().removeClass('has-error');
                    $(label).remove();
                }
            });
        },
        SearchResult: function (index) {
            index = index || pageIndex;
            var params = {
                CurrentPage: index,
                Account: $("#account").val(),
                Name: $("#name").val(),
                DepartName: $("#department").val(),
                Position: $("#position").val(),
            }
            var url = basePath + culture + '/Home/UserSearch';
            $.post(url, params, function (data) {
                $("#dateResult").empty().append(data);
            })
        },
        Export: function () {
            var url = basePath + culture + '/Home/UserExcel?Account=' + $.trim($("#account").val())
                + "&Name=" + $("#name").val() + "&DepartName=" + $("#department").val()
                + "&Position=" + $("#position").val();
            window.open(url);
        },
        EditUser: function (userId) {
            $("#id").val(userId);
            $("#editUser").trigger("click");
        },
        ResetUserPass: function (userId) {
            $("#id").val(userId);
            $("#btnResetPass").trigger("click");
        },
        DeleteUser: function (userId) {
            $.ajax({
                type: 'post',
                url: 'Home/DeleteUser',
                dataType: "json",
                data: {
                    UserId: userId
                },
                success: function (data) {
                    if (data) {
                        alert("Delete success!");
                        events.SearchResult(1);
                    }
                    else {
                        alert("Delete fail!");
                    }
                }
            });
        },
        EditFalse: function (data) {
            events.closePopWin();
            new PNotify({
                title: 'Error!',
                text: data,
                type: 'error'
            });
        },
        Save: function (event) {
            event.preventDefault();
            if (!($("#userAdd").valid())) {
                return;
            }
            var params = {
                UserId: $("#id").val() || "",
                //BUName: $.trim($("#surname").val()),
                BUName: $.trim($("#chineseName").val()),
                //BUJobNumber: $.trim($("#jobNumber").val()),
                BUEnglishName: $.trim($("#englishName").val()),
                Account: $.trim($("#account").val()),
                BUSex: $("input[name='sex-radios']:checked").val(),
                BUAvatars: $.trim($("#avatars").val()),
                BUPhoneNum: $.trim($("#phoneNum").val()),
                BUExtensionPhone: $.trim($("#extensionPhone").val()),
                BUMobilePhone: $.trim($("#mobilePhone").val()),
                BUEmail: $.trim($("#email").val()),
                //DepartId: $("#depart option:selected").val(),
                BUDepartName: $.trim($("#depart").val()),
                BUPosition: $.trim($("#position").val()),
                BUTitle: $.trim($("#title").val()),
                BUIsValid: $("input[name='valid-radios']:checked").val(),
                IsExistAccount: $("#isExistAccount").val()
            };
            var url = basePath + culture + '/Home/UsrSave';
            $("#userSubmit").attr("disabled", "disabled");
            $.post(url, params, function (data) {
                $("#userSubmit").removeAttr("disabled");
                if (data.IsSuccess) {
                    if (data.Code != null) {
                        alert("Add Success");
                        $("#ResetUser").trigger("click");
                        $.magnificPopup.close();
                    } else {
                        $("#id").val(0);
                        alert("Update Succes");
                        $.magnificPopup.close();
                    }
                } else {
                    alert("operation failed");
                }
            });
        },
        ResetImg: function () {
            $(".previewImg").remove();
            $(".selectImg").children().each(function (index, item) {
                $(item).removeClass('none');
            });
            $(".imgClose").addClass('none');
            $(".loading").addClass('none');
            $("#imgUpload").val("");
            $("#avatars").val("");
            page.InitImgUpload();
        },
        Upload: function () {
            var url = basePath + culture + '/Home/UpLoadFile';
            $.ajaxFileUpload({
                url: url,
                secureuri: false,
                fileElementId: "defectfile",
                //dataType: 'json',
                data: { 'type': 1 },
                success: function (result) {
                    var data = JSON.parse(result.body.innerHTML);
                    if (data.IsSuccess) {
                        alert("upload successful!"); 
                    }
                    else {
                        alert(data.Message);
                    }
                    $.magnificPopup.close();
                },
                error: function (data, status, e) { 
                }
            });
            return false;
        }
    };

    var page = {

        config: {},

        imgfile: {},

        Init: function (fig) {
            page.InitEvents();
            page.InitImgUpload();
        },

        InitEvents: function () {
            var _self = this;
            events.SearchResult(1);
            events.Validate();
            $("#search").on('click', function () {
                events.SearchResult(1);
            });
            $("#btnExport").on('click', function () {
                events.Export();
            });

            $("#addToTable").magnificPopup({
                type: 'inline',
                preloader: false,
                focus: '#name',
                modal: true,
                // When elemened is focused, some mobile browsers in some cases zoom in
                // It looks not nice, so we disable it:
                callbacks: {
                    beforeOpen: function () {
                        if ($(window).width() < 700) {
                            this.st.focus = false;
                        } else {
                            this.st.focus = '#name';
                        }
                        $("#ResetUser").trigger("click");
                        events.ResetImg();
              
                    },
                    open: function () {
                        $("#id").val(0);
                    },
                    close: function () {
                        events.SearchResult(1);
                    }

                }
            });

            $("#modalForm").on("click", ".modal-dismiss", function () {
                $.magnificPopup.close();
            });

            $("#editUser").magnificPopup({
                type: 'inline',
                preloader: false,
                focus: '#name',
                modal: true,
                // When elemened is focused, some mobile browsers in some cases zoom in
                // It looks not nice, so we disable it:
                callbacks: {
                    beforeOpen: function () {
                        if ($(window).width() < 700) {
                            this.st.focus = false;
                        } else {
                            this.st.focus = '#name';
                        }
                        $("div.dz-success").remove();
                        events.ResetImg();
                    },
                    open: function () {
                        var userId = $("#id").val()
                        $.ajax({
                            type: 'post',
                            url: basePath + culture + '/Home/InitAdUser',
                            dataType: "json",
                            data: {
                                userId: userId
                            },
                            success: function (data) {
                                $(".panel-title").html('EditUser')
                                $("#isExistAccount").val(data.IsExistAccount);
                                $("#chineseName").val(data.BUName);
                                $("#englishName").val(data.BUEnglishName);
                                $("#account").val(data.Account);
                                if (data.Account != null) {
                                    $("#account").attr("readonly", true)
                                }
                                else {
                                    $("#account").attr("readonly", false)
                                }
                                $("#jobNumber").val(data.BUJobNumber);
                                $("input[name='sex-radios'][value='" + data.BUSex + "']").prop('checked', true);
                                $("input[name='valid-radios'][value='" + data.BUIsValid + "']").prop('checked', true);
                                $("#avatars").val(data.BUAvatars);
                                $("#phoneNum").val(data.BUPhoneNum);
                                $("#extensionPhone").val(data.BUExtensionPhone);
                                $("#mobilePhone").val(data.BUMobilePhone);
                                $("#email").val(data.BUEmail);
                                //$("#depart option[value='" + data.DepartId + "']").prop('selected', true);
                                $("#depart").val(data.BUDepartName);
                                $("#position").val(data.BUPosition)
                                $("#title").val(data.BUTitle);

                                if (data.BUAvatars) {
                                    $(".selectImg").children().each(function (index, item) {
                                        $(item).addClass('none');
                                    });
                                    $(".selectImg").append('<img src="' + data.AvatarsUrl + '" class="previewImg"/>');
                                    $(".imgClose").removeClass('none');
                                    $(".loading").addClass('none');
                                }
                            }
                        });
                    },
                    close: function () {
                        events.SearchResult(1);
                    }

                }
            });

            $("#btnResetPass").magnificPopup({
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
                    },
                }
            });

            $("#userSubmit").on('click', events.Save);

            $("#ResetPassCon").on('click', function () {
                var userId = $("#id").val()
                $.ajax({
                    type: 'post',
                    url: basePath + culture + '/Home/RestPassByUserid',
                    dataType: "json",
                    data: {
                        userId: userId
                    },
                    success: function (data) {
                        $.magnificPopup.close();
                        if (data.IsSuccess) {
                            alert("Reset Password Success");
                        }
                        else {
                            alert(data.Message);
                        }
                    }
                });
            });

            $("#importUser").magnificPopup({
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
                    },
                    close: function () {
                        events.SearchResult(1);
                    }
                },
            });

            $("#btnTempDown").click(function () {
                var url = $("#btnTempDown").attr("data-url");
                window.open(url);
            });

            $("#upload").click(function (data) {
                if ($("#file").val() == "") {
                    alert("please choose file");
                    return false;
                }
                else {
                    events.Upload()
                }
            });

            $("#cancel").click(function () {
                $("#defectfile").val('');
                $.magnificPopup.close();
                return false;
            });

            $("#imgClose").on('click', function () {
                events.ResetImg();
      
            });
        },

        InitImgUpload: function () {
            $("#imgUpload").off('change');
            $("#imgUpload").on('change', function () {
                var file = this;
                if (file.files && file.files[0]) {
                    var arr = file.value.split('.');
                    var fileType = arr[arr.length - 1];
                    var fileSize = file.files[0].size;
                    var fileName = file.files[0].name;
                    if ("jpg|png|gif".indexOf(fileType) == -1 || fileSize > 5242880) {
                        alert("仅支持jpg、png、gif格式,大小小于5M");
                        return;
                    }

                    var url = basePath + culture + '/Home/ImgUpload';
                    $(".selectImg").children().each(function (index, item) {
                        $(item).addClass('none');
                    });
                    $(".loading").removeClass('none');
                    $.ajaxFileUpload({
                        url: url,
                        secureuri: false,
                        fileElementId: "imgUpload",
                        //dataType: 'json',
                        data: { 'type': 1 },
                        success: function (result) {
                            var data = JSON.parse(result.body.innerHTML);
                            if (data.data && data.data.length > 0) {
                                $(".selectImg").append('<img src="' + data.data[0].ImgUrl + '" class="previewImg"/>');
                                $(".imgClose").removeClass('none');
                                $(".loading").addClass('none');
                                $("#avatars").val(data.data[0].ImgLocalPath);
                            }
                        },
                        error: function (data) {
                        }
                    });
                } else {
                    $(".selectImg").append('<div class="img" style="filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale,src=\'' + file.value + '\'"></div>');
                }
            });
        },
    }
    window.PageInit = page.Init;
    window.SearchResult = events.SearchResult;
    window.EditUser = events.EditUser;
    window.ResetUserPass = events.ResetUserPass;
    window.DeleteUser = events.DeleteUser;
})(window, jQuery)