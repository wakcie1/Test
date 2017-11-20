//闭包（避免全局污染）
//加上分号（防止压缩出错）
;
(function (window, jQuery, undefined) {
    var config = {};

    //事件类
    var events = {
        save: function (event) {
            event.preventDefault();
            if (!($("#dpCreate").valid())) {
                return;
            }
            var parms = {
                DepartName: $("#departmentName").val(),
                ParentId: $("#ParentId").val(),
                IsValid: $("input[name='isvalid-radios']:checked").val(),
                DepartDesc: $("#DepartDesc").val()||"",
                DepartId: $("#departmentId").val()
            }
            var url = basePath + culture + '/Home/DepartmentSave';
            $.post(url, parms, function (data) {
                if (data.IsSuccess) {
                    if (data.Code != null) {
                        window.top.AddSuccess(data.Code, parms.ParentId, parms.DepartName);
                    } else
                    {
                        window.top.EditSuccess(parms.DepartId,parms.DepartName);
                    }
                } else {
                    window.top.AddNewNodeFalse(data.Message);
                }
            });
        },
    };

    //初始化类
    var page = {
        //初始化
        Init: function (fig) {
            config = fig;
            page.InitEvents();
        },
        //初始化事件
        InitEvents: function () {
            $("#dmConfirm").on('click', events.save);
        },
    };

    window.PageInit = page.Init;
})(window, jQuery)