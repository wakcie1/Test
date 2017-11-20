var id = 0;
$(function () {
    $(".form_datetime").datetimepicker({
        format: "yyyy-mm-dd hh:ii",
        autoclose: true,
        todayBtn: true,
        startDate: "2013-02-14 10:00",
        minuteStep: 5
    });

    Search(1);
})


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
        CurrentPage: pageIndex
    };
    $.ajax({
        type: "Post",
        url: "SuggestSearchResult",
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

$("#addSuggest").magnificPopup({
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
            $("#ResetSuggest").trigger("click");
        },
        close: function () {
            Search(1);
        }

    }
});
$("#editSuggest").magnificPopup({
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
            //$("div.dz-success").remove();
            //myDropzone.reset();
            //myDropzone.removeAllFiles(true);
            //var file = _self.imgfile
            //if (file) {
            //    myDropzone.emit("removedfile", file);
            //}
        },
        open: function () {
            $("#id").val(id)
            var editid = id;
            $.ajax({
                type: 'post',
                url: basePath + culture + '/Suggest/InitEditSuggestPage',
                dataType: "json",
                data: {
                    userId: editid
                },
                success: function (data) {
                    if (data != null) {
                        $("#feedBack").val(data.BFFeedBackComment);
                        $("#status option[value='" + data.BFStatus + "']").prop('selected', true);
                    } 
                }
            });
        },
        close: function () { 
            Search(1);
        }

    }
});

function EditPage(editid)
{
    id = editid;
    $("#editSuggest").trigger("click");
}
 

