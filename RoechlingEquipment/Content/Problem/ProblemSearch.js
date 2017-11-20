$(function () {
    $(".form_datetime").datetimepicker({
        format: "yyyy-mm-dd",
        minView: "month",
        autoclose: true,
        todayBtn: true,
        startDate: "2013-02-14 " 
    });
    InitControl();
    Search(1);
});

function InitControl() {
    //Process 
    var processListUrl = $("#processUrl").val() || '';
    if (processListUrl != '') {
        $.getJSON(processListUrl, null, function (myData) {
            if (myData != null) {
                var process = $("#process");
                process.html("");
                for (var i = 0; i < myData.length; i++) {
                    var item = "<option value='" + myData[i].Value + "'>" + myData[i].Text + "</option>";
                    process.append(item);
                }
            }
        });
    }
    //ProblemSeverity
    var problemSeverityUrl = $("#problemSeverityUrl").val() || '';
    if (problemSeverityUrl != '') {
        $.getJSON(problemSeverityUrl, null, function (myData) {
            if (myData != null) {
                var problemSeverity = $("#problemSeverity");
                problemSeverity.html("");
                for (var i = 0; i < myData.length; i++) {
                    var item = "<option value='" + myData[i].Value + "'>" + myData[i].Text + "</option>";
                    problemSeverity.append(item);
                }
            }
        });
    }
    //Status
    var statusUrl = $("#statusUrl").val() || '';
    if (statusUrl != '') {
        $.getJSON(statusUrl, null, function (myData) {
            if (myData != null) {
                var status = $("#status");
                status.html("");
                for (var i = 0; i < myData.length; i++) {
                    var item = "<option value='" + myData[i].Value + "'>" + myData[i].Text + "</option>";
                    status.append(item);
                }
            }
        });
    }
    //Repeatable
    var problemRepeatableUrl = $("#problemRepeatableUrl").val() || '';
    if (problemRepeatableUrl != '') {
        $.getJSON(problemRepeatableUrl, null, function (myData) {
            if (myData != null) {
                var problemRepeatable = $("#repeatable");
                problemRepeatable.html("");
                for (var i = 0; i < myData.length; i++) {
                    var item = "<option value='" + myData[i].Value + "'>" + myData[i].Text + "</option>";
                    problemRepeatable.append(item);
                }
            }
        });
    }

    //Source
    var sourceUrl = $("#sourceUrl").val() || '';
    if (sourceUrl != '') {
        $.getJSON(sourceUrl, null, function (myData) {
            if (myData != null) {
                var source = $("#source");
                source.html("");
                for (var i = 0; i < myData.length; i++) {
                    var item = "<option value='" + myData[i].Value + "'>" + myData[i].Text + "</option>";
                    source.append(item);
                }
            }
        });
    }

    var defectTypeUrl = $("#defectTypeUrl").val() || '';
    if (defectTypeUrl != '') {
        $.getJSON(defectTypeUrl, null, function (myData) {
            if (myData != null) {
                var defectType = $("#defectType");
                defectType.html("");
                for (var i = 0; i < myData.length; i++) {
                    var item = "<option value='" + myData[i].Value + "'>" + myData[i].Text + "</option>";
                    defectType.append(item);
                }
            }
        });
    }
}

$("#Search").click(function () { 

    var startDate = $("#From").val();
    var start = new Date(startDate.replace("-", "/").replace("-", "/"));
    var endDate = $("#To").val();
    var end = new Date(endDate.replace("-", "/").replace("-", "/"));

    if (end < start) {
        alert("开始时间应该大于结束时间！");
        return false;
    }
    Search(1);
});

function Search(pageIndex) {
    var params = {
        DateForm: $("#From").val(),
        DateTo: $("#To").val(),
        Process: $("#process").val(),
        PlantNo: $("#plantNo").val(),
        ReportDate: $("#reportDate").val(),
        WorkOrderNo: $("#workorder").val(),
        ToolingNo: $("#toolingNo").val(),
        MachineNo: $("#machineNo").val(),
        ProblemSeverity: $("#problemSeverity").val(),
        Status: $("#status").val(),
        KeyWords: $("#keyWords").val(),
        Repeatable: $("#repeatable").val(),
        NextProblemDateFrom: $("#nextReportDateFrom").val(),
        NextProblemDateTo: $("#nextReportDateTo").val(),
        SapNo: $("#sapNo").val(),
        PartName: $("#partname").val(),
        Customer: $("#customer").val(),
        Source: $("#source").val(),
        DefectType: $("#defectType").val(),
        CurrentPage: pageIndex
    };
    $.ajax({
        type: "Post",
        url: "ProblemSearchResult",
        async: false,
        data: JSON.stringify(params),
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        success: function (data) {
            if (data != null) {
                $("#searchResult").html(data);
            }

        }
    });
}

$("#btnExport").click(function () {
    var url = basePath + culture + '/Problem/ProblemExcel?DateForm=' + $.trim($("#From").val())
        + "&DateTo=" + $.trim($("#To").val()) + "&Process=" + $.trim($("#process").val())
        + "&Status=" + $.trim($("#status").val()) + "&WorkOrderNo=" + $.trim($("#workorder").val())
        + "&ToolingNo=" + $.trim($("#toolingNo").val()) + "&MachineNo=" + $.trim($("#machineNo").val())
        + "&ProblemSeverity=" + $.trim($("#problemSeverity").val()) + "&Repeatable=" + $.trim($("#repeatable").val())
        + "&SapNo=" + $.trim($("#sapNo").val()) + "&PartName=" + $.trim($("#partname").val())
        + "&Customer=" + $.trim($("#customer").val()) + "&Source=" + $.trim($("#source").val())
        + "&NextProblemDateFrom=" + $.trim($("#nextReportDateFrom").val()) + "&NextProblemDateTo=" + $.trim($("#nextReportDateTo").val())
        + "&DefectType=" + $.trim($("#defectType").val())  
      
    window.open(url); 
});