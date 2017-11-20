$(function () {
    var roleurl = basePath + culture + '/Jurisdiction/GetRoleCode';
    var groupurl = basePath + culture + '/Jurisdiction/GetGroupCode';
    $.ajax({
        type: 'post',
        url: roleurl,
        async: false,
        dataType: 'json',
        success: function (data) {
            allRole = data;
            if (data != "isadmin") {
                if ($.inArray("HOME", data) < 0) {
                    $("#homePage").prop("href", basePath + culture + '/PublicPage/Error404')
                }
                if ($.inArray("CREATE", data) < 0) {
                    $("#createPage").prop("href", basePath + culture + '/PublicPage/Error404')
                }
                if ($.inArray("SEARCH", data) < 0) {
                    $("#searchPage").prop("href", basePath + culture + '/PublicPage/Error404')
                }
                if ($.inArray("TIME_TRACKING", data) < 0) {
                    $("#timeTrackingPage").prop("href", basePath + culture + '/PublicPage/Error404')
                }
                if ($.inArray("SUGGESTIONS", data) < 0) {
                    $("#suggestionsPage").prop("href", basePath + culture + '/PublicPage/Error404')
                }
                if ($.inArray("REPORT", data) < 0) {
                    $("#reportPage").prop("href", basePath + culture + '/PublicPage/Error404')
                }
                if ($.inArray("MANAGEMENTREPORT", data) < 0) {
                    $("#managementReportPage").prop("href", basePath + culture + '/PublicPage/Error404')
                }
                if ($.inArray("SET_USER", data) < 0) {
                    $("#userPage").prop("href", basePath + culture + '/PublicPage/Error404')
                }
                if ($.inArray("SET_DEPARTMENT", data) < 0) {
                    $("#departmentPage").prop("href", basePath + culture + '/PublicPage/Error404')
                }
                if ($.inArray("SET_SAP", data) < 0) {
                    $("#sapPage").prop("href", basePath + culture + '/PublicPage/Error404')
                }
                if ($.inArray("SET_MATOOL", data) < 0) {
                    $("#toolPage").prop("href", basePath + culture + '/PublicPage/Error404')
                }
                //if ($.inArray("SET_ROOL", data) < 0) {
                //    $("#jurisdictionPage").prop("href", basePath + culture + '/PublicPage/Error404')
                //}
                if ($.inArray("SET_CODE", data) < 0) {
                    $("#codePage").prop("href", basePath + culture + '/PublicPage/Error404')
                }
            }
        }
    });
    $.ajax({
        type: 'post',
        url: groupurl,
        async: false,
        dataType: 'json',
        success: function (data) {
            allGroup = data;
        }
    });
    $("#aProfile").magnificPopup({
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
        }
    });

    $("#btnCancel").on("click", function () {
        $.magnificPopup.close();
    });
});