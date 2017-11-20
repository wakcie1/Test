$(function () {
    Search(1)
});


function Search(pageIndex) {
    var params = {
        DateForm: null,
        DateTo: null,
        Process: null,
        PlantNo: null,
        ReportDate: null,
        WorkOrderNo: null,
        ToolingNo: null,
        MachineNo: null,
        ProblemSeverity: null,
        Status: null,
        KeyWords: null,
        Repeatable: null,
        CurrentPage: pageIndex
    };
    var url = basePath + culture + '/Home/HomeSearchResult';
    $.ajax({
        type: "Post",
        url: url,
        async: false,
        data: JSON.stringify(params),
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        success: function (data) {
            if (data != null) {
                $("#result").html(data);
            }

        }
    });
}