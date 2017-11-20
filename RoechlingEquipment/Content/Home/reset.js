//闭包（避免全局污染）
//加上分号（防止压缩出错）
;
(function (window, jQuery, undefined) {
    var config = {};
    var basepath = '';
    var culture = 'en-US';
    //自定义封装方法
    var custMethods = {
        //查询
        SearchResult: function (index) {

        }
    };

    //事件类
    var events = {
        submitEvent: function () {
            var url = $(".reset").attr("action");
            var ajaxObj = $.ajax({
                url: url,
                type: "POST",
                data: { account: $("#username").val(), password: $("#password").val() },
                dataType: "json",
                timeout: 20000,
                async: true,
                success: function (data) {
                    if (!data.IsSuccess) {
                        $("#loginHelp").html(data.Message);
                    }
                    else {
                        window.location.href = basepath + "/" + culture + "/Home/Index";
                    }
                }
            });
        },

    };

    //初始化类
    var page = {
        //初始化
        Init: function (fig) {
            config = fig;
            basepath = config.basepath;
            culture = config.culture;
            page.InitEvents();
        },
        //初始化事件
        InitEvents: function () {
            //   $("#btnSearch").click(events.SearchEvent);

            $(".reset :submit").on("click", function (event) {
                event.preventDefault();
                if ($("#username").val().length == 0) {
                    $("#usernameHelp").html("The account is required");
                    return;
                }
                if ($("#password").val().length == 0) {
                    $("#passwordHelp").html("The password is required");
                    return;
                }

                if ($("#confirmPassword").val().length == 0) {
                    $("#cpHelp").html("The password is required");
                    return;
                }
                if ($("#confirmPassword").val() != $("#password").val()) {
                    $("#cpHelp").html("Passwords does not match");
                    return;
                }
                events.submitEvent();
            });

            $("#username").blur(function () {
                if ($("#username").val().length == 0) {
                    $("#usernameHelp").html("The account is required");
                }
            });

            $("#password").blur(function () {
                if ($("#password").val().length == 0) {
                    $("#passwordHelp").html("The password is required");
                }
            });

            $("#confirmPassword").blur(function () {
                if ($("#confirmPassword").val().length == 0) {
                    $("#cpHelp").html("The password is required");
                }
                if ($("#confirmPassword").val() != $("#password").val())
                {
                    $("#cpHelp").html("Passwords does not match");
                }
            });
        }
    };

    //全局变量，请勿重新定义（多个JS文件需要区分）
    window.PageInit = page.Init;

})(window, jQuery)