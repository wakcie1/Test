(function () {

    //验证消息扩展
    $.extend($.validator.messages, {
        required: "required"
    });
    //表单验证初始化
    var $problemForm = $("#problem-form");
    $problemForm.validate({
        errorPlacement: function (error, element) {
            $(element).parent().append(error);
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

    var $problemRootCauseForm = $("#problem-rootcause");
    $problemRootCauseForm.validate({
        errorPlacement: function (error, element) {
            $(element).parent().append(error);
        },
        wrapper: "div",
        showErrors: function (errorMap, errorList) {
            for (var obj in errorMap) {
                $('#' + obj).parent().addClass('has-error');
            }
            this.defaultShowErrors();
        },
        success: function (label) {
            $(label).parent().prev().removeClass('has-error');
        }
    });

    var $problemActionPlanForm = $("#problem-actionplan");
    $problemActionPlanForm.validate({
        errorPlacement: function (error, element) {
            $(element).parent().append(error);
        },
        wrapper: "div",
        showErrors: function (errorMap, errorList) {
            for (var obj in errorMap) {
                $('#' + obj).parent().addClass('has-error');
            }
            this.defaultShowErrors();
        },
        success: function (label) {
            $(label).parent().prev().removeClass('has-error');
        }
    });

    var $problemExtendporjectsForm = $("#problem-extendporjects");
    $problemExtendporjectsForm.validate({
        errorPlacement: function (error, element) {
            $(element).parent().append(error);
        },
        wrapper: "div",
        showErrors: function (errorMap, errorList) {
            for (var obj in errorMap) {
                $('#' + obj).parent().addClass('has-error');
            }
            this.defaultShowErrors();
        },
        success: function (label) {
            $(label).parent().prev().removeClass('has-error');
        }
    });

    var $problemNextdateForm = $("#problem-nextdate");
    $problemNextdateForm.validate({
        errorPlacement: function (error, element) {
            $(element).parent().append(error);
        },
        wrapper: "div",
        showErrors: function (errorMap, errorList) {
            for (var obj in errorMap) {
                $('#' + obj).parent().addClass('has-error');
            }
            this.defaultShowErrors();
        },
        success: function (label) {
            $(label).parent().prev().removeClass('has-error');
        }
    });

    var $problemDescForm = $("#problem-description");
    $problemDescForm.validate({
        errorPlacement: function (error, element) {
            $(element).parent().append(error);
        },
        wrapper: "div",
        showErrors: function (errorMap, errorList) {
            for (var obj in errorMap) {
                $('#' + obj).parent().addClass('has-error');
            }
            this.defaultShowErrors();
        },
        success: function (label) {
            $(label).parent().prev().removeClass('has-error');
        }
    });

    //提交问题
    $("#submitProblem").on("click", function () {
        if (!($("#problem-form").valid())) return false;
        problemProcess.saveProblem();
        return false;
    });

    //开启问题编辑
    $("#editProblem").on("click", function () {
        DisableForm("problem-form", null);
        if (IfHasRole("BTN_PROBLEM_SUBMIT")) {
            $("#submitProblem").removeClass("hidden");
        }
        $("#editProblem").addClass("hidden");
        $("#resetProblem").removeClass("hidden");
        //$("#approveProblem").addClass("hidden");
        return false;
    });

    //提交problemdesc
    $("#submitproblemdesc").on("click", function () {
        if (!($("#problem-description").valid())) return false;
        problemProcess.saveProblemDesc();
        return false;
    });

    //编辑problemdesc
    $("#editproblemdesc").on("click", function () {
        DisableForm("problem-description", null);
        $("#submitproblemdesc").removeClass("hidden");
        $("#editproblemdesc").addClass("hidden");
        return false;
    });

    //提交nextdate
    $("#submitNextdate").on("click", function () {
        if (!($("#problem-nextdate").valid())) return false;
        problemProcess.saveNextdate();
        return false;
    });

    //编辑nextdate
    $("#editNextdate").on("click", function () {
        DisableForm("problem-nextdate", null);
        $("#submitNextdate").removeClass("hidden");
        $("#editNextdate").addClass("hidden");
        return false;
    });

    //提交ActionPlan
    $("#submitActionPlan").on("click", function () {
        if (!($("#problem-actionplan").valid())) return false;
        problemProcess.saveActionPlan();
        return false;
    });

    //编辑ActionPlan
    $("#editActionPlan").on("click", function () {
        DisableForm("problem-actionplan", null);
        $("#submitActionPlan").removeClass("hidden");
        $("#editActionPlan").addClass("hidden");
        return false;
    });

    //提交RootCause
    $("#submitRootCause").on("click", function () {
        if (!($("#problem-rootcause").valid())) return false;
        problemProcess.saveRootCause();
        return false;
    });

    //编辑RootCause
    $("#editRootCause").on("click", function () {
        DisableForm("problem-rootcause", null);
        $("#submitRootCause").removeClass("hidden");
        $("#editRootCause").addClass("hidden");
        return false;
    });

    //提交Extendporjects
    $("#submitExtendporjects").on("click", function () {
        if (!($("#problem-extendporjects").valid())) return false;
        problemProcess.saveExtendporjects();
        return false;
    });

    //编辑Extendporjects
    $("#editExtendporjects").on("click", function () {
        DisableForm("problem-extendporjects", null);
        $("#submitExtendporjects").removeClass("hidden");
        $("#editExtendporjects").addClass("hidden");
        return false;
    });

    //关闭问题
    $("#closeProblem1").on("click", function () {
        problemProcess.closeProblem();
        return false;
    });

    //关闭问题
    $("#closeProblem2").on("click", function () {
        problemProcess.closeProblem();
        return false;
    });

    //关闭问题
    $("#closeProblem3").on("click", function () {
        problemProcess.closeProblem();
        return false;
    });

    $("#problemNext1").on("click", function () {
        $("#proActionTab").trigger("click");
        return false;
    });

    $("#problemNext2").on("click", function () {
        $("#proStandTab").trigger("click");
        return false;
    });

    $("#problemNext3").on("click", function () {
        $("#proPreiewTab").trigger("click");
        return false;
    });

    $("#imgClose1").on('click', function () {
        problemProcess.ResetImg(1);
    });
    $("#imgClose2").on('click', function () {
        problemProcess.ResetImg(2);
    });
    $("#imgClose3").on('click', function () {
        problemProcess.ResetImg(3);
    });
    $("#imgClose4").on('click', function () {
        problemProcess.ResetImg(4);
    });
    $("#imgClose5").on('click', function () {
        problemProcess.ResetImg(5);
    });
    $("#imgClose6").on('click', function () {
        problemProcess.ResetImg(6);
    });

    $("#proInfoTab").on("click", function () {
        pageScroll();
    });

    $("#proActionTab").on("click", function () {
        pageScroll();
    });

    $("#proStandTab").on("click", function () {
        pageScroll();
    });

    $("#proPreiewTab").on("click", function () {
        pageScroll();
        InitProblem();
    });

    $("#timeTrackingTab").on("click", function () {
        pageScroll();
        InitTimeTracking();
    });

    $("#problemdefecttype").on("change", function () {
        problemProcess.InitDefectCode();
    });

    $("input[name='extendporjects-radios']").on("change", function () {
        if ($("input[name='extendporjects-radios']:checked").val() == 0) {
            $("#extendporjectsTab").addClass("hidden");
        }
        else {
            $("#extendporjectsTab").removeClass("hidden")
        }
    });
    $("#previewexport").on('click', function () {
        var id = $("#proId").val();
        var url = $("#previewexport").attr("data-url") + "?proId=" + id;
        window.open(url);
    });

    //问题处理
    var problemProcess = {
        //保存问题
        saveProblem: function () {
            var _self = this;
            var params = {
                Id: $("#proId").val() || 0,
                PIProblemNo: $("#problemno").html() || '',
                PIProcess: $("#problemprocess").val() || '',
                PIMachine: $("#machines").val() || '',
                PITool: $("#tools").val() || '',
                PIMaterialId: $("#materialId").val() || '',
                PISapPN: $("#sappn").val() || '',
                PICustomer: $("#customer").val() || '',
                PIProductName: $("#partname").val() || '',
                PIWorkOrder: $("#workorder").val() || '',
                PIProblemDate: $("#problemdate").val() || '',
                PIProblemSource: $("#problemsource").val() || '',
                PIDefectType: $("#problemdefecttype").val() || '',
                PIDefectCode: $("#problemdefectcode").val() || '',
                PIDefectQty: $("#problemdefectqty").val() || 0,
                PIShiftType: $("#problemshifttype").val() || '',
                PIIsRepeated: $("#problemisrepeat").is(':checked') ? 1 : 0,
                PIProblemDesc: $("#problemdescription").val() || '',
                PIPicture1: $("#problempic1").val() || '',
                PIPicture2: $("#problempic2").val() || '',
                PIPicture3: $("#problempic3").val() || '',
                PIPicture4: $("#problempic4").val() || '',
                PIPicture5: $("#problempic5").val() || '',
                PIPicture6: $("#problempic6").val() || '',
                PISeverity: $("#problemseverity").val() || 0,
                PIStatus: $("#problestatus").val() || 0,
            };
            $.ajax({
                type: "Post",
                url: "SaveNewProblem",
                async: true,
                data: JSON.stringify(params),
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data != null) {
                        if (data.IsSuccess == true) {
                            $("#proId").val(data.data.Id);
                            $("#problemno").html(data.data.PIProblemNo);
                            InitBtnRole();
                            InitProblem();
                        }
                        else {
                            alert("error");
                        }
                    }
                    else {
                        alert("error");
                    }
                }
            });
        },

        saveNextdate: function () {
            var _self = this;
            var params = {
                Id: $("#proId").val() || 0,
                PINextProblemDate: $("#problemnextdate").val() || '',
            };
            $.ajax({
                type: "Post",
                url: "SaveNewProblem",
                async: true,
                data: JSON.stringify(params),
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data != null) {
                        if (data.IsSuccess == true) {
                            DisableForm("problem-nextdate", 1);
                            $("#submitNextdate").addClass("hidden");
                            $("#editNextdate").removeClass("hidden");
                        }
                        else {
                            alert("error");
                        }
                    }
                    else {
                        alert("error");
                    }
                }
            });
        },

        saveProblemDesc: function () {
            var _self = this;
            var params = {
                Id: $("#proId").val() || 0,
                PIProblemDesc: $("#problemactiondescription").val() || '',
            };
            $.ajax({
                type: "Post",
                url: "SaveNewProblem",
                async: true,
                data: JSON.stringify(params),
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data != null) {
                        if (data.IsSuccess == true) {
                            $("#problemdescription").val(params.PIProblemDesc);
                            DisableForm("problem-description", 1);
                            $("#submitproblemdesc").addClass("hidden");
                            $("#editproblemdesc").removeClass("hidden");
                        }
                        else {
                            alert("error");
                        }
                    }
                    else {
                        alert("error");
                    }
                }
            });
        },

        saveActionPlan: function () {
            var _self = this;
            var params = {
                Id: $("#proId").val() || 0,
                PIActionPlan: $("#problemactionplan").val() || '',
            };
            $.ajax({
                type: "Post",
                url: "SaveNewProblem",
                async: true,
                data: JSON.stringify(params),
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data != null) {
                        if (data.IsSuccess == true) {
                            DisableForm("problem-actionplan", 1);
                            $("#submitActionPlan").addClass("hidden");
                            $("#editActionPlan").removeClass("hidden");
                        }
                        else {
                            alert("error");
                        }
                    }
                    else {
                        alert("error");
                    }
                }
            });
        },

        saveExtendporjects: function () {
            var _self = this;
            var params = {
                Id: $("#proId").val() || 0,
                PIExtendComment: $("#problemextendcomment").val() || '',
                PIExtendApproveComment: $("#problemextendapprovecomment").val() || '',
                PIExtendPorjects: $("input[name='extendporjects-radios']:checked").val() || 0,
            };
            $.ajax({
                type: "Post",
                url: "SaveNewProblem",
                async: true,
                data: JSON.stringify(params),
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data != null) {
                        if (data.IsSuccess == true) {
                            DisableForm("problem-extendporjects", 1);
                            $("#submitExtendporjects").addClass("hidden");
                            $("#editExtendporjects").removeClass("hidden");
                        }
                        else {
                            alert("error");
                        }
                    }
                    else {
                        alert("error");
                    }
                }
            });
        },

        closeProblem: function () {
            var _self = this;
            var params = {
                Id: $("#proId").val() || 0,
                PIStatus: 2,
            };
            $.ajax({
                type: "Post",
                url: "SaveNewProblem",
                async: true,
                data: JSON.stringify(params),
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data != null) {
                        if (data.IsSuccess == true) {
                            SetClose(2);
                        }
                        else {
                            alert("error");
                        }
                    }
                    else {
                        alert("error");
                    }
                }
            });
        },

        saveRootCause: function () {
            var _self = this;
            var params = {
                Id: $("#proId").val() || 0,
                PIRootCause: $("#problemrootcause").val() || '',
            };
            $.ajax({
                type: "Post",
                url: "SaveNewProblem",
                async: true,
                data: JSON.stringify(params),
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data != null) {
                        if (data.IsSuccess == true) {
                            DisableForm("problem-rootcause", 1);
                            $("#submitRootCause").addClass("hidden");
                            $("#editRootCause").removeClass("hidden");
                        }
                        else {
                            alert("error");
                        }
                    }
                    else {
                        alert("error");
                    }
                }
            });
        },

        ResetImg: function (num) {

            $("#selectImg" + num).find(".previewImg").remove();
            $("#selectImg" + num).children().each(function (index, item) {
                $(item).removeClass('none');
            });
            $("#selectImg" + num).find(".imgClose").addClass('none');
            $("#selectImg" + num).find(".loading").addClass('none');
            $("#imgUpload" + num).val("");
            $("#problempic" + num).val("");
            problemProcess.InitImgUpload(num);
        },

        InitImgUpload: function (num) {
            $("#imgUpload" + num).off('change');
            $("#imgUpload" + num).on('change', function () {
                var file = this;
                if (file.files && file.files[0]) {
                    var arr = file.value.split('.');
                    var fileType = arr[arr.length - 1];
                    var fileSize = file.files[0].size;
                    var fileName = file.files[0].name;
                    if ("jpg|png|gif".indexOf(fileType) == -1 || fileSize > 5242880) {
                        alert("仅支持jpg、png、gif格式,大小小于5M");
                        return;
                    }
                    var url = basePath + culture + '/Home/ImgUpload';
                    $("#selectImg" + num).children().each(function (index, item) {
                        $(item).addClass('none');
                    });
                    $("#selectImg" + num).find(".loading").removeClass('none');
                    $.ajaxFileUpload({
                        url: url,
                        secureuri: false,
                        fileElementId: "imgUpload" + num,
                        //dataType: 'json',
                        data: { 'type': 1 },
                        success: function (result) {
                            var data = JSON.parse(result.body.innerHTML);
                            if (data.data && data.data.length > 0) {
                                var img = "<a href=\"" + data.data[0].ImgUrl + "\" class=\"image-popup-no-margins\">"
                                    + "<img class=\"img-responsive\" src=\"" + data.data[0].ImgUrl + "\" width=\"160\" ></a >"
                                //$("#selectImg" + num).append('<img src="' + data.data[0].ImgUrl + '" class="previewImg"/>');
                                $("#selectImg" + num).append(img);
                                $('.image-popup-no-margins').magnificPopup({
                                    type: 'image',
                                    closeOnContentClick: true,
                                    closeBtnInside: false,
                                    fixedContentPos: true,
                                    mainClass: 'mfp-no-margins mfp-with-zoom',
                                    image: {
                                        verticalFit: true
                                    },
                                    zoom: {
                                        enabled: true,
                                        duration: 300
                                    }
                                });
                                $("#selectImg" + num).find(".imgClose").removeClass('none');
                                $("#selectImg" + num).find(".loading").addClass('none');
                                $("#problempic" + num).val(data.data[0].ImgLocalPath);
                            }
                        },
                        error: function (data) {
                        }
                    });
                } else {

                }
            });
        },

        InitSapInfo: function () {
            //sap info
            var materiallisturl = $("#materiallisturl").val() || '';
            if (materiallisturl != '') {
                var options = {
                    minLength: 0,
                    source: function (request, response) {
                        $.ajax({
                            url: materiallisturl,
                            type: "post",
                            dataType: "json",
                            data: { "key": $("#sappn").val() || '', "workorder": '' },
                            success: function (data) {
                                response($.map(data, function (item) {
                                    return {
                                        label: item.MISapPN,
                                        value: item.MISapPN,
                                        Id: item.Id,
                                        sappn: item.MISapPN,
                                        Productname: item.MIProductName,
                                        Customer: item.MICustomer
                                    }
                                }));
                            }
                        });
                    },
                    select: function (event, ui) {
                        $("#sappn").val(ui.item.sappn);
                        $("#materialId").val(ui.item.Id);
                        $("#partname").val(ui.item.Productname);
                        $("#customer").val(ui.item.Customer);
                        return false;
                    },
                };
                $("#sappn").autocomplete(options);
            }
        },

        InitWorkOrder: function () {
            var workorderlisturl = $("#workorderlisturl").val() || '';
            if (workorderlisturl != '') {
                var options = {
                    minLength: 0,
                    source: function (request, response) {
                        $.ajax({
                            url: workorderlisturl,
                            type: "post",
                            dataType: "json",
                            data: { "key": $("#workorder").val() || '' },
                            success: function (data) {
                                response($.map(data, function (item) {
                                    return {
                                        label: item.WIWorkOrder,
                                        value: item.WISapPN || ''
                                    }
                                }));
                            }
                        });
                    },
                    select: function (event, ui) {
                        $("#workorder").val(ui.item.label);
                        $("#sappn").val(ui.item.value);
                        if (ui.item.value == '') return false;
                        var materiallisturl = $("#materiallisturl").val() || '';
                        if (materiallisturl != '') {
                            $.ajax({
                                url: materiallisturl,
                                type: "post",
                                dataType: "json",
                                data: { "key": $("#sappn").val() || '', "workorder": '' },
                                success: function (data) {
                                    if (data && data.length > 0) {
                                        $("#sappn").val(data[0].MISapPN);
                                        $("#materialId").val(data[0].Id);
                                        $("#partname").val(data[0].MIProductName);
                                        $("#customer").val(data[0].MICustomer);
                                    }
                                }
                            });
                        }
                        return false;
                    },
                };
                $("#workorder").autocomplete(options);
            }
        },

        InitMachineTool: function () {
            //tool autocomplete
            var toollistUrl = $("#toollisturl").val() || '';
            if (toollistUrl != '') {
                var options = {
                    minLength: 0,
                    source: function (request, response) {
                        $.ajax({
                            url: toollistUrl,
                            type: "post",
                            dataType: "json",
                            data: { "key": $("#tools").val() },
                            success: function (data) {
                                response($.map(data, function (item) {
                                    return {
                                        label: item.MTToolNo,
                                        value: item.MTToolNo
                                    }
                                }));
                            }
                        });
                    },
                    select: function (event, ui) {
                        $("#tools").val(ui.item.label);
                        return false;
                    },
                };
                $("#tools").autocomplete(options);
            }
            //machine autocomplete
            var machinelisturl = $("#machinelisturl").val() || '';
            if (machinelisturl != '') {
                var options = {
                    minLength: 0,
                    source: function (request, response) {
                        $.ajax({
                            url: machinelisturl,
                            type: "post",
                            dataType: "json",
                            data: { "key": $("#machines").val() },
                            success: function (data) {
                                response($.map(data, function (item) {
                                    return {
                                        label: item.BMEquipmentNo,
                                        value: item.BMEquipmentNo
                                    }
                                }));
                            }
                        });
                    },
                    select: function (event, ui) {
                        $("#machines").val(ui.item.label);
                        return false;
                    },
                };
                $("#machines").autocomplete(options);
            }
        },

        InitProcess: function () {
            //process select
            var processlisturl = $("#processlisturl").val() || '';
            if (processlisturl != '') {
                $.getJSON(processlisturl, null, function (myData) {
                    if (myData != null) {
                        var processlist = $("#problemprocess");
                        processlist.html("");
                        for (var i = 0; i < myData.length; i++) {
                            var item = "<option value='" + myData[i].Value + "'>" + myData[i].Text + "</option>";
                            processlist.append(item);
                        }
                    }
                });
            }
        },

        InitSource: function () {
            //source select
            var sourcelisturl = $("#sourcelisturl").val() || '';
            if (sourcelisturl != '') {
                $.getJSON(sourcelisturl, null, function (myData) {
                    if (myData != null) {
                        var sourcelist = $("#problemsource");
                        sourcelist.html("");
                        for (var i = 0; i < myData.length; i++) {
                            var item = "<option value='" + myData[i].Value + "'>" + myData[i].Text + "</option>";
                            sourcelist.append(item);
                        }
                    }
                });
            }
        },

        InitSeverity: function () {
            //severity select
            var severitylisturl = $("#severitylisturl").val() || '';
            if (severitylisturl != '') {
                $.getJSON(severitylisturl, null, function (myData) {
                    if (myData != null) {
                        var severitylist = $("#problemseverity");
                        severitylist.html("");
                        for (var i = 0; i < myData.length; i++) {
                            var item = "<option value='" + myData[i].Value + "'>" + myData[i].Text + "</option>";
                            severitylist.append(item);
                        }
                    }
                });
            }
        },

        InitOpenStatus: function () {
            //severity select
            var statuslisturl = $("#statuslisturl").val() || '';
            if (statuslisturl != '') {
                $.getJSON(statuslisturl, null, function (myData) {
                    if (myData != null) {
                        var severitylist = $("#problestatus");
                        severitylist.html("");
                        for (var i = 0; i < myData.length; i++) {
                            var item = "<option value='" + myData[i].Value + "'>" + myData[i].Text + "</option>";
                            severitylist.append(item);
                        }
                    }
                });
            }
        },

        InitShiftType: function () {
            //shifttype select
            var itemX = "<option value=\"X\">X</option>";
            var itemY = "<option value=\"Y\">Y</option>";
            var itemZ = "<option value=\"Z\">Z</option>";
            var shifttype = $("#problemshifttype");
            shifttype.html("");
            shifttype.append(itemX);
            shifttype.append(itemY);
            shifttype.append(itemZ);
        },

        InitDatePicker: function () {
            $("#problemdate").datetimepicker({
                format: "yyyy-mm-dd hh:ii",
                autoclose: true,
                todayBtn: true,
                startDate: "2013-02-14 10:00",
                minuteStep: 10
            });
            $("#problemnextdate").datetimepicker({
                format: "yyyy-mm-dd hh:ii",
                autoclose: true,
                todayBtn: true,
                startDate: "2013-02-14 10:00",
                minuteStep: 10
            });
        },

        InitDefectCode: function () {
            //defectcode select
            var defectcodelisturl = $("#defectcodelisturl").val() || '';
            if (defectcodelisturl != '') {
                //if ($("#problemdefecttype").val()) {
                //    defectcodelisturl += "&type=" + $("#problemdefecttype").val();
                //}
                var params = {
                    type: $("#problemdefecttype").val() || '',
                    isNeedDefalut: false
                };
                $.ajax({
                    type: "Post",
                    url: defectcodelisturl,
                    async: false,
                    data: JSON.stringify(params),
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (myData) {
                        if (myData != null) {
                            var defectcodelist = $("#problemdefectcode");
                            defectcodelist.html("");
                            for (var i = 0; i < myData.length; i++) {
                                var item = "<option value='" + myData[i].Value + "'>" + myData[i].Text + "</option>";
                                defectcodelist.append(item);
                            }
                        }

                    }
                });
                //$.getJSON(defectcodelisturl, null, function (myData) {
                //    if (myData != null) {
                //        var defectcodelist = $("#problemdefectcode");
                //        defectcodelist.html("");
                //        for (var i = 0; i < myData.length; i++) {
                //            var item = "<option value=" + myData[i].Value + ">" + myData[i].Text + "</option>";
                //            defectcodelist.append(item);
                //        }
                //    }
                //});
            }
        },

        InitDefectType: function () {
            //defecttype select
            var defectcodetypeurl = $("#defectcodetypeurl").val() || '';
            if (defectcodetypeurl != '') {
                $.getJSON(defectcodetypeurl, null, function (myData) {
                    if (myData != null) {
                        var problemdefecttype = $("#problemdefecttype");
                        problemdefecttype.html("");
                        for (var i = 0; i < myData.length; i++) {
                            var item = "<option value='" + myData[i].Value + "'>" + myData[i].Text + "</option>";
                            problemdefecttype.append(item);
                        }
                    }
                });
            }
        },

        InitControl: function () {
            problemProcess.InitSapInfo();
            problemProcess.InitWorkOrder();
            problemProcess.InitMachineTool();
            problemProcess.InitProcess();
            problemProcess.InitSource();
            problemProcess.InitSeverity();
            problemProcess.InitOpenStatus();
            problemProcess.InitShiftType();
            problemProcess.InitDatePicker();
            problemProcess.InitDefectType();
            problemProcess.InitDefectCode();

        },
    };

    //审核问题
    $("#approvePreventivemeasures2").on("click", function () {
        $("#approvePreventivemeasures2").addClass("hidden");
        if (IfHasRole("BTN_PREMEASURE_REJECT")) {
            $("#rejectPreventivemeasures2").removeClass("hidden");
        }
        var params = {
            Id: $("#proId").val() || 0,
            PIProblemNo: $("#problemno").html() || '',
            PIProcessStatus: 801,
        };
        $.ajax({
            type: "Post",
            url: "UpdateProblemStatus",
            async: true,
            data: JSON.stringify(params),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    if (data.IsSuccess == true) {
                        $("#proProcessStatus").val(801);
                        InitBtnRole();
                        InitProblem();
                    }
                    else {
                        alert("error");
                    }
                }
                else {
                    alert("error");
                }
            }
        });
        return false;
    });

    //驳回问题
    $("#rejectPreventivemeasures2").on("click", function () {
        $("#rejectPreventivemeasures2").addClass("hidden");
        if (IfHasRole("BTN_PREMEASURE_APPROVE")) {
            $("#approvePreventivemeasures2").removeClass("hidden");
        }
        var params = {
            Id: $("#proId").val() || 0,
            PIProblemNo: $("#problemno").html() || '',
            PIProcessStatus: 0,
        };
        $.ajax({
            type: "Post",
            url: "UpdateProblemStatus",
            async: true,
            data: JSON.stringify(params),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    if (data.IsSuccess == true) {
                        $("#proProcessStatus").val(0);
                        InitBtnRole();
                        InitProblem();
                        SetSolvingTeamBtn(1);
                        SetQualityAlertBtn(1);
                        SetSortingActivityBtn(1);
                        SetActionContainmentBtn(1);
                        SetActionFactoranalysisBtn(1);
                        SetActionWhyAnalysis1Btn(1);
                        SetActionWhyAnalysis2Btn(1);
                        SetActionnCorrectiveBtn(1);
                        SetActionnPreventiveMeasuresBtn(1);
                    }
                    else {
                        alert("error");
                    }
                }
                else {
                    alert("error");
                }
            }
        });
        return false;
    });

    $("#approveEnd").on("click", function () {
        $("#approveEnd").addClass("hidden");
        var params = {
            Id: $("#proId").val() || 0,
            PIProblemNo: $("#problemno").html() || '',
            PIProcessStatus: 1300,
        };
        $.ajax({
            type: "Post",
            url: "UpdateProblemStatus",
            async: true,
            data: JSON.stringify(params),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    if (data.IsSuccess == true) {
                        $("#proProcessStatus").val(1300);
                        InitTabs();
                    }
                    else {
                        alert("error");
                    }
                }
                else {
                    alert("error");
                }
            }
        });
        return false;
    });

    window.InitDefectCode = problemProcess.InitDefectCode;
    $(function () {
        problemProcess.InitControl();
        problemProcess.InitImgUpload("1");
        problemProcess.InitImgUpload("2");
        problemProcess.InitImgUpload("3");
        problemProcess.InitImgUpload("4");
        problemProcess.InitImgUpload("5");
        problemProcess.InitImgUpload("6");
        InitBtnRole();
        InitSolvingTeam();
        InitQualityAlert();
        InitSortingActivity();
        InitActionContainment();
        InitActionFactoranalysis();
        InitActionWhyAnalysis1();
        InitActionWhyAnalysis2();
        InitActionCorrective();
        InitActionPreventiveMeasures();
        InitStandLayeredAudit();
        InitStandVerification();
        InitStandardization();
        InitProblem();
        var id = $("#proId").val();
        if (id == 0) {
            $("#proActionTab").addClass("hidden");
            $("#proPreiewTab").addClass("hidden");
            $("#editProblem").addClass("hidden");
            if (IfHasRole("BTN_PROBLEM_SUBMIT")) {
                $("#submitProblem").removeClass("hidden");
                $("#resetProblem").removeClass("hidden");
            }

        }
        $("#proInfoTab").trigger("click");
        $(window).resize(function () {
            DrawPvCanvas();
        });
    });

}).apply(this, [jQuery]);


//禁用表单
function DisableForm(formId, isDisabled) {

    var attr = "disable";
    if (!isDisabled) {
        attr = "enable";
    }
    $("form[id='" + formId + "']").find("[type='number']").attr("disabled", isDisabled);
    $("form[id='" + formId + "'] :text").attr("disabled", isDisabled);
    $("form[id='" + formId + "'] textarea").attr("disabled", isDisabled);
    $("form[id='" + formId + "'] select").attr("disabled", isDisabled);
    $("form[id='" + formId + "'] :radio").attr("disabled", isDisabled);
    $("form[id='" + formId + "'] :checkbox").attr("disabled", isDisabled);

    //禁用jquery easyui中的下拉选（使用input生成的combox）    

    $("#" + formId + " input[class='combobox-f combo-f']").each(function () {
        if (this.id) {
            $("#" + this.id).combobox(attr);
        }
    });

    //禁用jquery easyui中的下拉选（使用select生成的combox）    
    $("#" + formId + " select[class='combobox-f combo-f']").each(function () {
        if (this.id) {
            $("#" + this.id).combobox(attr);
        }
    });

    //禁用jquery easyui中的日期组件dataBox    
    $("#" + formId + " input[class='datebox-f combo-f']").each(function () {
        if (this.id) {
            $("#" + this.id).datebox(attr);
        }
    });
}

function InitProblem() {
    var proId = $("#proId").val() || 0;
    if (proId == 0) {
        $.ajax({
            type: "Post",
            url: "GenerateProblemNo",
            async: true,
            //data: JSON.stringify(params),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data) {
                    $("#problemno").html(data);
                }
            }
        });

        return;
    }
    else {
        var problemunionurl = $("#problemunionurl").val() || '';
        if (problemunionurl != '') {
            problemunionurl += "?proId=" + proId;
            $.getJSON(problemunionurl, null, function (problemData) {
                if (problemData && problemData.data && problemData.data.problem) {
                    var proInfo = problemData.data.problem;
                    InitActionProblemDetail(proInfo);
                    $("proOwner").val(proInfo.PICreateUserNo);
                    $("#problemno").html(proInfo.PIProblemNo);
                    $("#problemprocess").find("option[value = '" + proInfo.PIProcess + "']").attr("selected", "selected");
                    $("#machines").val(proInfo.PIMachine);
                    $("#tools").val(proInfo.PITool);
                    $("#sappn").val(proInfo.PISapPN);
                    $("#workorder").val(proInfo.PIWorkOrder);
                    $("#partname").val(proInfo.PIProductName);
                    $("#customer").val(proInfo.PICustomer);
                    $("#problemdate").val(proInfo.PIProblemDateDesc);
                    $("#problemsource").find("option[value = '" + proInfo.PIProblemSource + "']").attr("selected", "selected");
                    $("#problemdefecttype").find("option[value = '" + proInfo.PIDefectType + "']").attr("selected", "selected");
                    InitDefectCode();
                    $("#problemdefectcode").find("option[value = '" + proInfo.PIDefectCode + "']").attr("selected", "selected");
                    $("#problemdefectqty").val(proInfo.PIDefectQty);
                    $("#problemshifttype").find("option[value = '" + proInfo.PIShiftType + "']").attr("selected", "selected");
                    $("#problemisrepeat").attr("checked", proInfo.PIIsRepeated == 1);
                    $("#problemdescription").val(proInfo.PIProblemDesc);

                    $("#problemseverity").find("option[value = '" + proInfo.PISeverity + "']").attr("selected", "selected");
                    $("#problestatus").find("option[value = '" + proInfo.PIStatus + "']").attr("selected", "selected");
                    $("#proProcessStatus").val(proInfo.PIProcessStatus);

                    $("input[name='extendporjects-radios'][value='" + proInfo.PIExtendPorjects + "']").prop('checked', true);
                    if (proInfo.PIExtendPorjects == 1) {
                        $("#extendporjectsTab").removeClass("hidden");
                    }
                    else {
                        $("#extendporjectsTab").addClass("hidden");
                    }

                    if (proInfo.PIPicture1Url) {
                        $("#selectImg1").children().each(function (index, item) {
                            $(item).addClass('none');
                        });

                        var img = "<a href=\"" + proInfo.PIPicture1Url + "\" class=\"image-popup-no-margins\">"
                            + "<img class=\"img-responsive\" src=\"" + proInfo.PIPicture1Url + "\" width=\"160\" ></a >"
                        $("#selectImg1").append(img);
                        $("#selectImg1").find(".imgClose").removeClass('none');
                        $("#selectImg1").find(".loading").addClass('none');
                        $("#problempic1").val(proInfo.PIPicture1);
                    }
                    if (proInfo.PIPicture2Url) {
                        $("#selectImg2").children().each(function (index, item) {
                            $(item).addClass('none');
                        });
                        var img = "<a href=\"" + proInfo.PIPicture2Url + "\" class=\"image-popup-no-margins\">"
                            + "<img class=\"img-responsive\" src=\"" + proInfo.PIPicture2Url + "\" width=\"160\" ></a >"
                        $("#selectImg2").append(img);
                        $("#selectImg2").find(".imgClose").removeClass('none');
                        $("#selectImg2").find(".loading").addClass('none');
                        $("#problempic2").val(proInfo.PIPicture2);
                    }
                    if (proInfo.PIPicture3Url) {
                        $("#selectImg3").children().each(function (index, item) {
                            $(item).addClass('none');
                        });
                        var img = "<a href=\"" + proInfo.PIPicture3Url + "\" class=\"image-popup-no-margins\">"
                            + "<img class=\"img-responsive\" src=\"" + proInfo.PIPicture3Url + "\" width=\"160\" ></a >"
                        $("#selectImg3").append(img);
                        $("#selectImg3").find(".imgClose").removeClass('none');
                        $("#selectImg3").find(".loading").addClass('none');
                        $("#problempic3").val(proInfo.PIPicture3);
                    }

                    if (proInfo.PIPicture4Url) {
                        $("#selectImg4").children().each(function (index, item) {
                            $(item).addClass('none');
                        });
                        var img = "<a href=\"" + proInfo.PIPicture4Url + "\" class=\"image-popup-no-margins\">"
                            + "<img class=\"img-responsive\" src=\"" + proInfo.PIPicture4Url + "\" width=\"160\" ></a >"
                        $("#selectImg4").append(img);
                        $("#selectImg4").find(".imgClose").removeClass('none');
                        $("#selectImg4").find(".loading").addClass('none');
                        $("#problempic4").val(proInfo.PIPicture4);
                    }
                    if (proInfo.PIPicture5Url) {
                        $("#selectImg5").children().each(function (index, item) {
                            $(item).addClass('none');
                        });
                        var img = "<a href=\"" + proInfo.PIPicture5Url + "\" class=\"image-popup-no-margins\">"
                            + "<img class=\"img-responsive\" src=\"" + proInfo.PIPicture5Url + "\" width=\"160\" ></a >"
                        $("#selectImg5").append(img);
                        $("#selectImg5").find(".imgClose").removeClass('none');
                        $("#selectImg5").find(".loading").addClass('none');
                        $("#problempic5").val(proInfo.PIPicture5);
                    }
                    if (proInfo.PIPicture6Url) {
                        $("#selectImg6").children().each(function (index, item) {
                            $(item).addClass('none');
                        });
                        var img = "<a href=\"" + proInfo.PIPicture6Url + "\" class=\"image-popup-no-margins\">"
                            + "<img class=\"img-responsive\" src=\"" + proInfo.PIPicture6Url + "\" width=\"160\" ></a >"
                        $("#selectImg6").append(img);
                        $("#selectImg6").find(".imgClose").removeClass('none');
                        $("#selectImg6").find(".loading").addClass('none');
                        $("#problempic6").val(proInfo.PIPicture6);
                    }

                    $('.image-popup-no-margins').magnificPopup({
                        type: 'image',
                        closeOnContentClick: true,
                        closeBtnInside: false,
                        fixedContentPos: true,
                        mainClass: 'mfp-no-margins mfp-with-zoom',
                        image: {
                            verticalFit: true
                        },
                        zoom: {
                            enabled: true,
                            duration: 300
                        }
                    });
                    DisableForm("problem-form", 1);

                    //action porblemdesc
                    if (proInfo.PIProblemDesc) {
                        DisableForm("problem-description", 1);
                        $("#problemactiondescription").val(proInfo.PIProblemDesc);
                        if (IfHasRole("BTN_PROBLEM_SUBMIT") && (IfHasGroup("QE") || IsOwner())) {
                            $("#editproblemdesc").removeClass("hidden");
                        }
                        $("#submitproblemdesc").addClass("hidden");
                    }
                    else {
                        if (IfHasRole("BTN_PROBLEM_SUBMIT") && (IfHasGroup("QE") || IsOwner())) {
                            $("#submitproblemdesc").removeClass("hidden");
                        }
                        $("#editproblemdesc").addClass("hidden");
                    }
                    //next date
                    if (proInfo.PINextProblemDateDesc) {
                        DisableForm("problem-nextdate", 1);
                        $("#problemnextdate").val(proInfo.PINextProblemDateDesc);
                        if (IfHasRole("BTN_PROBLEM_SUBMIT") && (IfHasGroup("QE") || IsOwner())) {
                            $("#editNextdate").removeClass("hidden");
                        }
                        $("#submitNextdate").addClass("hidden");
                    }
                    else {
                        if (IfHasRole("BTN_PROBLEM_SUBMIT") && (IfHasGroup("QE") || IsOwner())) {
                            $("#submitNextdate").removeClass("hidden");
                        }
                        $("#editNextdate").addClass("hidden");
                    }

                    //rootcouse
                    if (proInfo.PIRootCause) {
                        DisableForm("problem-rootcause", 1);
                        $("#problemrootcause").val(proInfo.PIRootCause);
                        if (IfHasRole("BTN_WHYANALY_ADD") && (IfHasGroup("QE") || IsOwner())) {
                            $("#editRootCause").removeClass("hidden");
                        }
                        $("#submitRootCause").addClass("hidden");
                    }
                    else {
                        if (IfHasRole("BTN_WHYANALY_ADD") && (IfHasGroup("QE") || IsOwner())) {
                            $("#submitRootCause").removeClass("hidden");
                        }
                        $("#editRootCause").addClass("hidden");
                    }

                    //actionplan
                    if (proInfo.PIActionPlan) {
                        DisableForm("problem-actionplan", 1);
                        $("#problemactionplan").val(proInfo.PIActionPlan);

                        if (IfHasRole("BTN_PREMEASURE_ADD") && (IfHasGroup("QE") || IsOwner())) {
                            $("#editActionPlan").removeClass("hidden");
                        }
                        $("#submitActionPlan").addClass("hidden");
                    }
                    else {
                        if (IfHasRole("BTN_PREMEASURE_ADD") && (IfHasGroup("QE") || IsOwner())) {
                            $("#submitActionPlan").removeClass("hidden");
                        }
                        $("#editActionPlan").addClass("hidden");
                    }

                    //extendporjects
                    if (proInfo.PIExtendComment) {
                        DisableForm("problem-extendporjects", 1);
                        $("#problemextendcomment").val(proInfo.PIExtendComment);
                        $("#problemextendapprovecomment").val(proInfo.PIExtendApproveComment);
                        if (IfHasRole("BTN_STANDA_ADD")) {
                            $("#editExtendporjects").removeClass("hidden");
                        }
                        $("#submitExtendporjects").addClass("hidden");
                    }
                    else {
                        if (IfHasRole("BTN_STANDA_ADD")) {
                            $("#submitExtendporjects").removeClass("hidden");
                        }
                        $("#editExtendporjects").addClass("hidden");
                    }
                    $("#approveLayeredAuditflag").val(proInfo.PIApproveLayeredAudit);
                    $("#approveVerificationflag").val(proInfo.PIApproveVerification);
                    $("#approveStandardizationflag").val(proInfo.PIApproveStandardization);

                    var assignlist = $("#problemassign");
                    assignlist.html("");
                    for (var i = 0; i < problemData.data.solvingteam.length; i++) {
                        var assigners = problemData.data.solvingteam[i];
                        var item = "<option value='" + assigners.PSUserNo + "'>" + assigners.PSUserName + "</option>";
                        assignlist.append(item);
                    }
                    $("#problemassigenhidden").val(proInfo.PIRootCauseAssignNo);
                    var assignop = $("#problemassign").find("option[value = '" + proInfo.PIRootCauseAssignNo + "']");
                    if (assignop) {
                        assignop.attr("selected", "selected");
                    }

                    InitProblemPreviewData(problemData.data);
                    if (proInfo.PIStatus == 2) {
                        $("#proInfoTab").addClass("hidden");
                        $("#proActionTab").addClass("hidden");
                        $("#proStandTab").addClass("hidden");
                        $("#approveEnd").addClass("hidden");
                        if ($("#proPreiewTab").parent().hasClass("active")) {
                            return false;
                        }
                        $("#proPreiewTab").trigger("click");
                    }

                    if (problemData.data.solvingteam) {
                        var solvingteamdata = problemData.data.solvingteam;
                        InitSolvingTeamData(solvingteamdata);
                    }
                    if (problemData.data.qualityalert) {
                        var qualityalertdata = problemData.data.qualityalert;
                        InitQualityAlertData(qualityalertdata);
                    }
                    if (problemData.data.sortingactivity) {
                        var sortingactivitydata = problemData.data.sortingactivity;
                        InitSortingActivityData(sortingactivitydata);
                    }
                    if (problemData.data.actioncontainment) {
                        var actioncontainmentdata = problemData.data.actioncontainment;
                        InitActionContainmentData(actioncontainmentdata);
                    }
                    if (problemData.data.actionfactoranalysis) {
                        var actionfactoranalysis = problemData.data.actionfactoranalysis;
                        InitActionFactoranalysisData(actionfactoranalysis);
                    }
                    if (problemData.data.actionwhyanalysisi) {
                        var actionwhyanalysisidata = problemData.data.actionwhyanalysisi;
                        InitActionWhyAnalysisData1(actionwhyanalysisidata);
                        InitActionWhyAnalysisData2(actionwhyanalysisidata);
                    }
                    if (problemData.data.actioncorrective) {
                        var actioncorrectivedata = problemData.data.actioncorrective;
                        InitActionCorrectiveData(actioncorrectivedata);
                    }
                    if (problemData.data.actionpreventive) {
                        var actionpreventivedata = problemData.data.actionpreventive;
                        InitActionPreventiveMeasuresData(actionpreventivedata);
                    }
                    if (problemData.data.layeredaudit) {
                        var layeredauditdata = problemData.data.layeredaudit;
                        InitStandLayeredAuditData(layeredauditdata);
                    }
                    if (problemData.data.verification) {
                        var verificationdata = problemData.data.verification;
                        InitStandVerificationData(verificationdata);
                    }
                    if (problemData.data.standardization) {
                        var standardizationdata = problemData.data.standardization;
                        InitStandardizationData(standardizationdata);
                    }
                    InitTabs();
                }
            });
        }
    }
}

function InitActionProblemDetail(data) {
    if (data) {
        var prictureimg = "<td></td>";
        if (data.PIPicture1Url) {
            prictureimg = "<td><a href=\"" + data.PIPicture1Url + "\" class=\"image-popup-no-margins\">"
                + "<img class=\"img-responsive\" src=\"" + data.PIPicture1Url + "\" width=\"100\" ></a ></td>";
        }
        var actionproblemdetails = "<tr><td>" + data.PIProblemNo + "</td>"
            + "<td>" + data.PIProcess + "</td>"
            + "<td>" + data.PIProductName + "</td>"
            + "<td>" + data.PIProblemDateDesc + "</td>"
            + "<td>" + data.PIDefectQty + "</td>"
            + "<td>" + data.PIDefectCode + "</td>"
            + "<td>" + data.PIShiftType + "</td>"
            + prictureimg
            + "</tr > ";
        $("#problemdetailTable").html(actionproblemdetails);
    }
}

function InitAssign() {
    var proId = $("#proId").val() || 0;
    if (proId == 0) {
        return;
    }
    else {
        var problemunionurl = $("#problemunionurl").val() || '';
        if (problemunionurl != '') {
            problemunionurl += "?proId=" + proId;
            $.getJSON(problemunionurl, null, function (problemData) {
                if (problemData && problemData.data && problemData.data.solvingteam) {
                    var assignlist = $("#problemassign");
                    var selectAssign = assignlist.val() || '';
                    assignlist.html("");
                    for (var i = 0; i < problemData.data.solvingteam.length; i++) {
                        var assigners = problemData.data.solvingteam[i];
                        var item = "<option value='" + assigners.PSUserNo + "'>" + assigners.PSUserName + "</option>";
                        assignlist.append(item);
                    }
                    if (selectAssign != '') {
                        var assignop = $("#problemassign").find("option[value = '" + selectAssign + "']");
                        if (assignop) {
                            assignop.attr("selected", "selected");
                        }
                    }
                }
            });
        }
    }
}

function InitTimeTracking() {
    var proId = $("#proId").val() || 0;
    if (proId == 0) return;
    var params = {
        code: $("#problemno").text()
    }
    var url = basePath + culture + '/Log/LogSearch';
    $.post(url, params, function (data) {
        $("#logList").empty().append(data);
    })
}

function InitBtnRole() {
    if (IfHasRole("BTN_PROBLEM_SUBMIT")) {
        $("#submitProblem").removeClass("hidden");
        $("#resetProblem").removeClass("hidden");
        $("#submitNextdate").removeClass("hidden");
        $("#submitproblemdesc").removeClass("hidden");
        $("#editProblem").removeClass("hidden");
        $("#editNextdate").removeClass("hidden");
        $("#editproblemdesc").removeClass("hidden");
    }
    if (IfHasRole("BTN_SOLVTEAM_ADD")) {
        $("#addsolvingteam").removeClass("hidden");
    }
    if (IfHasRole("BTN_QUALALERT_ADD")) {
        $("#addToQualityalert").removeClass("hidden");
    }
    if (IfHasRole("BTN_CONTAIN_ADD")) {
        $("#addContainmentAction").removeClass("hidden");
    }
    if (IfHasRole("BTN_FACTANALY_ADD")) {
        $("#addA1").removeAttr('disabled');
        $("#addA2").removeAttr('disabled');
        $("#addA3").removeAttr('disabled');
        $("#addA4").removeAttr('disabled');
        $("#addB1").removeAttr('disabled');
        $("#addB2").removeAttr('disabled');
        $("#addB3").removeAttr('disabled');
        $("#addB4").removeAttr('disabled');
        $("#addC1").removeAttr('disabled');
        $("#addC2").removeAttr('disabled');
        $("#addC3").removeAttr('disabled');
        $("#addC4").removeAttr('disabled');
        $("#addD1").removeAttr('disabled');
        $("#addD2").removeAttr('disabled');
        $("#addD3").removeAttr('disabled');
        $("#addD4").removeAttr('disabled');
    }
    if (IfHasRole("BTN_WHYANALY_ADD")) {
        $("#addwhyanalysis1").removeClass("hidden");
        $("#addwhyanalysis2").removeClass("hidden");
        $("#submitRootCause").removeClass("hidden");
        $("#editRootCause").removeClass("hidden");
    }
    if (IfHasRole("BTN_CORRECT_ADD")) {
        $("#addcorrectivecction").removeClass("hidden");
    }
    if (IfHasRole("BTN_PREMEASURE_ADD")) {
        $("#addpreventivemeasures").removeClass("hidden");
        $("#submitActionPlan").removeClass("hidden");
        $("#editActionPlan").removeClass("hidden");
    }
    if (IfHasRole("BTN_PREMEASURE_APPROVE")) {
        $("#approvePreventivemeasures2").removeClass("hidden");
        $("#rejectPreventivemeasures2").removeClass("hidden");
    }
    if (IfHasRole("BTN_LAYERDAUDIT_ADD")) {
        $("#addlayeredaudit").removeClass("hidden");
    }
    if (IfHasRole("BTN_LAYERDAUDIT_APPROVE")) {
        $("#approveLayeredaudit").removeClass("hidden");
        $("#rejectLayeredaudit").removeClass("hidden");
    }
    if (IfHasRole("BTN_VERIFICATION_ADD")) {
        $("#addverification").removeClass("hidden");
    }
    if (IfHasRole("BTN_VERIFICATION_APPROVE")) {
        $("#approveVerification").removeClass("hidden");
        $("#rejectVerification").removeClass("hidden");
    }
    if (IfHasRole("BTN_STANDA_APPROVE")) {
        $("#approveStandardization").removeClass("hidden");
        $("#rejectStandardization").removeClass("hidden");
    }
    if (IfHasRole("BTN_STANDA_ADD")) {
        $("#submitExtendporjects").removeClass("hidden");
        $("#editExtendporjects").removeClass("hidden");
    }
    if (IfHasRole("BTN_END_APPROVE")) {
        $("#approveEnd").removeClass("hidden");
    }
    if (IfHasRole("BTN_PROBLEM_CLOSE2")) {
        $("#closeProblem2").removeClass("hidden");
    }
    if (IfHasRole("BTN_PROBLEM_CLOSE3")) {
        $("#closeProblem3").removeClass("hidden");
    }
}

function InitTabs() {
    var id = $("#proId").val();
    if (id == 0) {
        $("#editProblem").addClass("hidden");
        return false;
    }

    $("#proActionTab").removeClass("hidden");
    $("#proPreiewTab").removeClass("hidden");
    var status = $("#proProcessStatus").val();
    var severity = $("#problemseverity").val();

    $("#submitProblem").addClass("hidden");
    $("#resetProblem").addClass("hidden");
    $("#timeTrackingTab").removeClass("hidden");
    $("#solvingteamTab").removeClass("hidden");
    $("#qualityalertTab").removeClass("hidden");
    $("#sortingactivityTab").removeClass("hidden");
    $("#problemNext1").removeClass("hidden");
    $("#problemactionTab").removeClass("hidden");
    $("#containmentactionTab").removeClass("hidden");
    $("#factoranalysisTab").removeClass("hidden");
    $("#whyanalysisTab").removeClass("hidden");
    $("#correctiveActionTab").removeClass("hidden");
    $("#preventivemeasuresTab").removeClass("hidden");

    var approveLayeredAudit = $("#approveLayeredAuditflag").val() || 0;
    if (approveLayeredAudit == 0) {
        $("#rejectLayeredaudit").addClass("hidden");
        if (IfHasRole("BTN_LAYERDAUDIT_APPROVE")) {
            $("#approveLayeredaudit").removeClass("hidden");
        }
    }
    else {
        $("#addlayeredaudit").addClass("hidden");
        $("#approveLayeredaudit").addClass("hidden");
        if (IfHasRole("BTN_LAYERDAUDIT_APPROVE")) {
            $("#rejectLayeredaudit").removeClass("hidden");
        }
        SetStandLayeredAuditBtn(0);
    }
    var approveVerification = $("#approveVerificationflag").val() || 0;
    if (approveVerification == 0) {
        $("#rejectVerification").addClass("hidden");
        if (IfHasRole("BTN_VERIFICATION_APPROVE")) {
            $("#approveVerification").removeClass("hidden");
        }
    }
    else {
        $("#addverification").addClass("hidden");
        $("#approveVerification").addClass("hidden");
        if (IfHasRole("BTN_VERIFICATION_APPROVE")) {
            $("#rejectVerification").removeClass("hidden");
        }
        SetStandVerificationBtn(0);
    }
    var approveStandardization = $("#approveStandardizationflag").val() || 0;
    if (approveStandardization == 0) {
        $("#rejectStandardization").addClass("hidden");
        if (IfHasRole("BTN_STANDA_APPROVE")) {
            $("#approveStandardization").removeClass("hidden");
        }
    }
    else {
        $("#approveStandardization").addClass("hidden");
        if (IfHasRole("BTN_STANDA_APPROVE")) {
            $("#rejectStandardization").removeClass("hidden");
        }
        SetStandardizationBtn(0);
    }
    if (status < 801) {
        $("#proStandTab").addClass("hidden");
        $("#rejectPreventivemeasures2").addClass("hidden");
        $("#approveEnd").addClass("hidden");
        $("#problemNext2").addClass("hidden");
    }
    if (status >= 801) {
        $("#submitProblem").addClass("hidden");
        $("#editProblem").addClass("hidden");
        $("#resetProblem").addClass("hidden");
        $("#submitNextdate").addClass("hidden");
        $("#submitproblemdesc").addClass("hidden");
        $("#editNextdate").addClass("hidden");
        $("#editproblemdesc").addClass("hidden");
        $("#addsolvingteam").addClass("hidden");
        //隐藏小按钮solvingteam
        SetSolvingTeamBtn(0);
        $("#addToQualityalert").addClass("hidden");
        //隐藏小按钮Qualityalert
        SetQualityAlertBtn(0);
        SetSortingActivityBtn(0);
        $("#addContainmentAction").addClass("hidden");
        //隐藏小按钮ContainmentAction
        SetActionContainmentBtn(0);
        $("#addA1").attr({ 'disabled': 'disabled' });
        $("#addA2").attr({ 'disabled': 'disabled' });
        $("#addA3").attr({ 'disabled': 'disabled' });
        $("#addA4").attr({ 'disabled': 'disabled' });
        $("#addB1").attr({ 'disabled': 'disabled' });
        $("#addB2").attr({ 'disabled': 'disabled' });
        $("#addB3").attr({ 'disabled': 'disabled' });
        $("#addB4").attr({ 'disabled': 'disabled' });
        $("#addC1").attr({ 'disabled': 'disabled' });
        $("#addC2").attr({ 'disabled': 'disabled' });
        $("#addC3").attr({ 'disabled': 'disabled' });
        $("#addC4").attr({ 'disabled': 'disabled' });
        $("#addD1").attr({ 'disabled': 'disabled' });
        $("#addD2").attr({ 'disabled': 'disabled' });
        $("#addD3").attr({ 'disabled': 'disabled' });
        $("#addD4").attr({ 'disabled': 'disabled' });
        //隐藏小按钮Factanaly
        SetActionFactoranalysisBtn(0);
        $("#addwhyanalysis1").addClass("hidden");
        $("#addwhyanalysis2").addClass("hidden");
        $("#submitRootCause").addClass("hidden");
        $("#editRootCause").addClass("hidden");
        //隐藏小按钮whyanalysis
        $("#addcorrectivecction").addClass("hidden");
        SetActionWhyAnalysis1Btn(0);
        SetActionWhyAnalysis2Btn(0);
        //隐藏小按钮correctivecction
        SetActionnCorrectiveBtn(0);
        $("#addpreventivemeasures").addClass("hidden");
        $("#submitActionPlan").addClass("hidden");
        $("#editActionPlan").addClass("hidden");
        //隐藏小按钮preventivemeasures
        SetActionnPreventiveMeasuresBtn(0);
        $("#approvePreventivemeasures2").addClass("hidden");
        $("#rejectPreventivemeasures2").addClass("hidden");
        $("#closeProblem2").addClass("hidden");
        
        $("#problemNext2").removeClass("hidden");
        $("#problemNext3").removeClass("hidden");
        $("#proStandTab").removeClass("hidden");
        $("#layeredauditTab").removeClass("hidden");
        $("#verificationTab").removeClass("hidden");
        $("#standardizationTab").removeClass("hidden");
        if (status < 1200) {

        }
    }
    if (status >= 1200) {
        $("#closeProblem3").addClass("hidden");

        $("#addlayeredaudit").addClass("hidden");
        $("#approveLayeredaudit").addClass("hidden");
        $("#rejectLayeredaudit").addClass("hidden");
        //隐藏小按钮layeredaudit
        SetStandLayeredAuditBtn(0);
        $("#addverification").addClass("hidden");
        $("#approveVerification").addClass("hidden");
        $("#rejectVerification").addClass("hidden");
        //隐藏小按钮Verification
        SetStandVerificationBtn(0);
        $("#approveStandardization").addClass("hidden");
        $("#rejectStandardization").addClass("hidden");
        $("#submitExtendporjects").addClass("hidden");
        $("#editExtendporjects").addClass("hidden");
        //隐藏小按钮Standardization
        SetStandardizationBtn(0);
    }
    if (status == 1300) {
        $("#approveEnd").addClass("hidden");
    }
}

function SetClose(status) {
    if (status == 2) {
        $("#proInfoTab").addClass("hidden");
        $("#proActionTab").addClass("hidden");
        $("#proStandTab").addClass("hidden");
        $("#approveEnd").addClass("hidden");
        if ($("#proPreiewTab").parent().hasClass("active")) {
            return false;
        }
        $("#proPreiewTab").trigger("click");
    }
}

function IsOwner() {
    var jobnum = $("#userJobnum").val() || '';
    if (jobnum == '') {
        return false;
    }
    var problemowner = $("#proOwner").val() || '';
    if (problemowner == '') {
        return false;
    }
    if (jobnum == problemowner) {
        return true;
    }
    return false;
}

function pageScroll() {
    window.scrollTo(0, -10);
}

