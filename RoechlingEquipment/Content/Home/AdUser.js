//闭包（避免全局污染）
//加上分号（防止压缩出错）
;
(function (window, jQuery, undefined) {
    var config = {};
    var userId = "";

    var events = {
        CheckData: function () {
            if ($.trim($("#surname").val()) == '') {
                $("#surname").css('border-color', 'red');
                return false;
            }
            else {
                $('#surname').css('border-color', '#e3e6f3');
            }
            if ($.trim($("#givenname").val()) == '') {
                $("#givenname").css('border-color', 'red');
                return false;
            }
            else {
                $('#givenname').css('border-color', '#e3e6f3');
            }
            if ($.trim($("#jobNumber").val()) == '') {
                $("#jobNumber").css('border-color', 'red');
                return false;
            }
            else {
                $('#jobNumber').css('border-color', '#e3e6f3');
            }
            if ($.trim($("#phoneNum").val()) == '') {
                $("#phoneNum").css('border-color', 'red');
                return false;
            }
            else {
                $('#phoneNum').css('border-color', '#e3e6f3');
            }
            if ($.trim($("#title").val()) == '') {
                $("#title").css('border-color', 'red');
                return false;
            }
            else {
                $('#title').css('border-color', '#e3e6f3');
            }
            if ($.trim($("#phoneNum").val())){
                var myreg = /^[0-9]{11}$/;
                if (!myreg.test($.trim($("#phoneNum").val()))) {
                    new PNotify({
                        title: '',
                        text: 'The cell phone number is incorrect',
                        type: 'info',
                    });
                    return;
                }
            }
            if ($.trim($("#email").val())) {
                var emailreg = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+/;
                if (!emailreg.test($.trim($("#email").val()))) {
                    new PNotify({
                        title: '',
                        text: 'Please enter a valid mailbox',
                        type: 'info',
                    });
                    return;
                }
            }
            return true;
        },
        Save: function () {
            var flag = events.CheckData();
            if (!flag) {
                return;
            }
            var params = {
                UserId: userId,
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
            $(".modal-confirm").attr("disabled", "disabled");
            $.post(url, params, function (data) {
                if (data.IsSuccess) {
                    if (data.Code != null) {
                        window.top.EditSuccess();   
                    } else {
                        window.top.EditSuccess(); 
                    }
                } else {
                    window.top.EditFalse(); 
                }
            });
        },
        Cancel: function () {
            window.top.ClosePopWin();
        },
    };

    var page = {
        config: {},
        Init: function (fig) {
            config = fig;
            userId = fig.userId;
            page.InitEvents();
        },
        InitEvents: function () {
            $(".modal-confirm").on('click', events.Save);
            $(".modal-dismiss").on('click', events.Cancel)
        }
    }

    window.PageInit = page.Init;
})(window, jQuery)