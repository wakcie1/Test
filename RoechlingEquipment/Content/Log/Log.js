//闭包（避免全局污染）
//加上分号（防止压缩出错）
;
(function (window, jQuery, undefined) {
    var config = {};

    var events = {
        SearchResult: function () {
            var params = {
                code: $("#txtlogSearch").val()
            }

            var url = basePath + culture + '/Log/LogSearch';
            $.post(url, params, function (data) {
                $("#logList").empty().append(data);
            })
        },
        ProblemAutoComplete: function () {
            var problemIdUrl = basePath + culture + '/Problem/ProblemAotoComplete';

            var options = {
                minLength: 0,
                source: function (request, response) {
                    $.ajax({
                        url: problemIdUrl,
                        type: "post",
                        dataType: "json",
                        data: { "key": $("#txtlogSearch").val() },
                        success: function (data) {
                            response($.map(data, function (item) {
                                return {
                                    label: item.PIProblemNo,
                                    value: item.Id
                                }
                            }));
                        }
                    });
                },
                select: function (event, ui) {
                    $("#txtlogSearch").val(ui.item.label);
                    return false;
                },
            };
            $("#txtlogSearch").autocomplete(options);
        },
    };

    var page = {
        config: {},

        Init: function (fig) {
            page.InitEvents();
        },

        InitEvents: function () {
            //events.SearchResult();
            events.ProblemAutoComplete();
            $("#btnSearch").on("click", function () {
                events.SearchResult();
            });
            
        },
    }

    window.PageInit = page.Init;
})(window, jQuery)