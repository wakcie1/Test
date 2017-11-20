$(function () {
    Search(1);
    DefectCodeSearch(1);
    $("#otherTab").trigger("click");
});

$("#Search").click(function () {
    Search(1);
});

$("#DefectSearch").click(function () {
    DefectCodeSearch(1);
});

$("#DefectCodeExport").click(function () {
    var url = basePath + culture + '/Code/DefectCodeExcel?codetype=' + $.trim($("#scodetype").val())
        + "&code=" + $.trim($("#scode").val())  
    window.open(url);
});
$("#btnDefectCodeTempDown").click(function () {
    var url = $("#btnDefectCodeTempDown").attr("data-url");
    window.open(url);
});

function Search(pageIndex) {
    var params = {
        Key: $("#key").val()
    };
    $.ajax({
        type: "Post",
        url: "CodeSearchResult",
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

function DefectCodeSearch(pageIndex) {
    var params = {
        codetype: $.trim($("#scodetype").val()) || '',
        code: $.trim($("#scode").val()) || '',
        CurrentPage: pageIndex
    };
    $.ajax({
        type: "Post",
        url: "DefectCodeSearchResult",
        async: false,
        data: JSON.stringify(params),
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        success: function (data) {
            if (data != null) {
                $("#defectcodesearchResult").html(data);
            }
        }
    });
}

$("#addCode").magnificPopup({
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
            $("#ResetCodeBtn").trigger("click");
        },
        close: function () {
            Search(1);
        }
    }
});

$("#addDefectCode").magnificPopup({
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
            $("#ResetDefectCodeBtn").trigger("click");
        },
        open: function () {
            $("#DCID").val(0);
        },
        close: function () {
            DefectCodeSearch(1);
        }
    }
});

function DeletePage(category) {
    $("#categoryDel").val(category)
    $("#deleteCode").trigger("click");
}

$("#deleteCode").magnificPopup({
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
        },
        open: function () {

            var deleteCategory = $("#categoryDel").val();

            CodeDeletePageInit(deleteCategory);
        },
        close: function () {
            Search(1);
        }

    }
});

function CodeDeletePageInit(deleteCategory) {
    $.ajax({
        type: 'post',
        url: basePath + culture + '/Code/InitCodeDeletePage',
        dataType: "json",
        data: {
            key: deleteCategory
        },
        success: function (data) {
            var table = $("#codeDeleteTable");
            table.html("");
            var html = "<thead>"
                + "<tr>"
                + "<th class='sorting' tabindex='0' rowspan='1' colspan='1' style='width:30%;text-align:center'>Key</th>"
                + "<th class='sorting' tabindex='0' rowspan='1' colspan='1' style='width:30%;text-align:center'>Value</th>"
                + "<th class='sorting' tabindex='0' rowspan='1' colspan='1' style='width:30%;text-align:center'>Option</th>"
                + "</tr >"
                + "</thead>"
                + "<tbody>"
            if (data != null && data.length != 0) {
                $.each(data, function (index, item) {
                    html += "<tr><td>" + item.Category + "</td><td>" + item.Value + "</td><td>" +
                        "<a class='on-default remove-row' onclick='Delete("
                        + item.Id
                        + ")'><i class='fa fa-trash-o'></i></a>"
                        + "</td></tr>";
                });
            }
            else {
                html += "<tr><td colspan='3' style='text-align:center'>noResult</td></tr>"
            }
            html += "</tbody>";
            table.append(html);
        }
    });
}

$(".fa,.fa-times").click(function () {
    $.magnificPopup.close();
});

function Delete(id) {
    $.ajax({
        type: 'post',
        url: basePath + culture + '/Code/DeleteCode ',
        dataType: "json",
        data: {
            Id: id
        },
        success: function (data) {
            if (data) {
                alert("Delete success!");
                CodeDeletePageInit($("#categoryDel").val());
            }
            else {
                alert("Delete fail!");
            }
        }
    });
}

function EditDefectCode(Id) {
    $("#DCID").val(Id);
    $("#editDefectCode").trigger("click");
}

$("#editDefectCode").magnificPopup({
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
        },
        open: function () {
            var Id = $("#DCID").val();
            $.ajax({
                type: "GET",
                url: "GetOneDefectCode",
                data: { defectId: Id },
                dataType: "json",
                success: function (data) {
                    if (data) {
                        $("#DCID").val(data.Id);
                        $("#bdcodetype").val(data.BDCodeType);
                        $("#bdcodeno").val(data.BDCodeNo);
                        $("#bdcode").val(data.BDCode);
                        $("#bccodenameen").val(data.BDCodeNameEn);
                        $("#bccodenamecn").val(data.BDCodeNameCn);
                    }
                }
            });
        },
        beforeClose: function () {
        },
        close: function () {
            DefectCodeSearch(1);
        }
    }
});

$("#importDefectCode").magnificPopup({
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
        },
        close: function () {
            DefectCodeSearch(1);
        }
    }
});


$("#defectcodeupload").click(function () { 
    var url = basePath + culture + '/Code/UploadDefectCodeFiles';
    $.ajaxFileUpload({
        url: url,
        secureuri: false,
        fileElementId: "defectfile",
        //dataType: 'json',
        data: { 'type': 1 },
        success: function (result) {
            var data = JSON.parse(result.body.innerHTML);
            if (data.IsSuccess) {
                alert("upload successful"); 
            }
            else {
                alert(data.Message);
            }
            $.magnificPopup.close();
        },
        error: function (data) {
        }
    });
    return false;
});

$("#defectcodeCancel").click(function () {
    $("#defectfile").val('');
    $.magnificPopup.close();
    return false;
});

//function setImportProcess() {
//    var p = $.cookie('Import_Process');
//    if (p) {
//        var width = p + '%';
//        $("#defectcodeimportProcessbar").width(width).attr('aria-valuenow', p);
//    }
//}


