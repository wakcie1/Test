$(function () {

    // validation summary
    var $summaryForm = $("#defectcode-form");
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
});


$("#addDefectCodeBtn").click(function () {
    if (!($("#defectcode-form").valid())) return;
    var id = $("#DCID").val() || 0;
    var params = {
        Id: id,
        BDCodeType: $("#bdcodetype").val() || '',
        BDCodeNo: $("#bdcodeno").val() || '',
        BDCode: $("#bdcode").val() || '',
        BDCodeNameEn: $("#bccodenameen").val() || '',
        BDCodeNameCn: $("#bccodenamecn").val() || '',
    };
    $.ajax({
        type: "Post",
        url: "SaveDefectCode",
        async: false,
        data: JSON.stringify(params),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.IsSuccess) {
                if (id > 0) {
                    $("#DCID").val(0);
                    alert("update successful");
                    $.magnificPopup.close();
                }
                else {
                    alert("add successful");
                    $.magnificPopup.close();
                }
            }
        }
    });
    return false;
})
$("#cancelDefectCodeBtn").click(function () {
    $.magnificPopup.close();
    return false;
});

