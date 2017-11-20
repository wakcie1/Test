$(function () {
    
    var $summaryForm = $("#suggestEdit-form");
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
    //Status
    var phraseUrl = $("#statusUrl").val() || '';
    if (phraseUrl != '') {
        $.getJSON(phraseUrl, null, function (myData) {
            if (myData != null) {
                var requireType = $("#status");
                requireType.html("");
                for (var i = 0; i < myData.length; i++) {
                    var item = "<option value='" + myData[i].Value + "'>" + myData[i].Text + "</option>";
                    requireType.append(item);
                }
            }
        });
    }
});

$("#EditSuggestBtn").click(function () {

    if (!($("#suggestEdit-form").valid())) return;

    var params = { 
        Id:$("#id").val(),
        BFFeedBackComment: $("#feedBack").val(),
        BFStatus: $("#status").val()
    };
    $.ajax({
        type: "Post",
        url: "SaveSuggest",
        async: false,
        data: JSON.stringify(params),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) { 
            if (data.IsSuccess) {
                alert("update sucess " + data.model.Id);
                $.magnificPopup.close();
            }
            else {
                alert("update fail " + data.model.Id);
            }
        }
    });
})

$("#cancelSuggestEditBtn").click(function () {
    $.magnificPopup.close();
});