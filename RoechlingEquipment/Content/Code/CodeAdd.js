$(function () {

    // validation summary
    var $summaryForm = $("#code-form");
    $summaryForm.validate({
        errorPlacement: function (error, element) {
            $(element).parent().after(error);
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
            $(label).parent().prev().removeClass('has-error');
        }
    });
    //Gategory
    var categoryUrl = $("#categoryUrl").val() || '';
    if (categoryUrl != '') {
        $.getJSON(categoryUrl, null, function (myData) {
            if (myData != null) {
                var category = $("#category");
                category.html("");
                for (var i = 0; i < myData.length; i++) {
                    var item = "<option value='" + myData[i].Value + "'>" + myData[i].Text + "</option>";
                    category.append(item);
                }
            }
        });
    }
});


$("#addCodeBtn").click(function () {

    if (!($("#code-form").valid())) return;

    var params = {
        BCCategory: $("#category").val(),
        BCCode: $("#code").val(),
        BCCodeDesc: $("#code").val(),
        BCCodeOrder: $("#codeOrder").val()
    };
    $.ajax({
        type: "Post",
        url: "SaveCode",
        async: false,
        data: JSON.stringify(params),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.IsSuccess) {
                alert("successful");
                $.magnificPopup.close();
            }
        }
    });
    return false;
})
$("#cancelCodeBtn").click(function () {
    $.magnificPopup.close();
    return false;
});
