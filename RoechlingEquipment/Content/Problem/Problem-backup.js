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

    //提交问题
    $("#submitProblem").on("click", function () {
        if (!($("#problem-form").valid())) return;
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
        $("#approveProblem").addClass("hidden");
        return false;
    });

    //提交ActionPlan
    $("#submitActionPlan").on("click", function () {
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

    $("#proPreiewTab").on("click", function () {
        InitProblem();
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

    InitUploadAttach();
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
                PICustomerPN: $("#customerpart").val() || '',
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
                            DisableForm("problem-form", 1);
                            $("#submitProblem").addClass("hidden");
                            if (IfHasRole("BTN_PROBLEM_SUBMIT")) {
                                $("#editProblem").removeClass("hidden");
                            }
                            $("#resetProblem").addClass("hidden");
                            if (IfHasRole("BTN_PROBLEM_APPROVE")) {
                                $("#approveProblem").removeClass("hidden");
                            }
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

        InitAttachmentUpload: function () {
            //TODO
            $("#attupload1").on('change', function () {
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
                        fileElementId: "imgUpload",
                        //dataType: 'json',
                        data: { 'type': 1 },
                        success: function (result) {
                            var data = JSON.parse(result.body.innerHTML);
                            if (data.data && data.data.length > 0) {
                                var file = "  <span class='attachment'><a href='javascript:void(0);' class='attachFile' data-attachName=\'" + name + "." + style + "\' data-attachUrl=\'" + url + "\'>" + name + "." + style + "</a><i class='icon-paper-clip'></i><a href=\"javascript:void(0);\" class=\"deleteBtn\">(删除)</a> </span>";
                                $("#UploadFileList").append(file);
                                $('#UploadFileList').on('click', '.deleteBtn', problemProcess.UploadDelete);
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
                            data: { "key": $("#customerpart").val() || '', "workorder": $("#workorder").val() || '' },
                            success: function (data) {
                                response($.map(data, function (item) {
                                    return {
                                        label: item.MICustomerPN,
                                        value: item.MICustomerPN,
                                        Id: item.Id,
                                        Customerpart: item.MICustomerPN,
                                        Productname: item.MIProductName,
                                        Customer: item.MICustomer
                                    }
                                }));
                            }
                        });
                    },
                    select: function (event, ui) {
                        $("#customerpart").val(ui.item.Customerpart);
                        $("#materialId").val(ui.item.Id);
                        $("#partname").val(ui.item.Productname);
                        $("#customer").val(ui.item.Customer);
                        return false;
                    },
                };
                $("#customerpart").autocomplete(options);
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
                                        label: item.MIWorkOrder,
                                        value: item.MIWorkOrder
                                    }
                                }));
                            }
                        });
                    },
                    select: function (event, ui) {
                        $("#workorder").val(ui.item.label);
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
                                        label: item.BMFixtureNo,
                                        value: item.BMFixtureNo
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
                            url: toollistUrl,
                            type: "post",
                            dataType: "json",
                            data: { "key": $("#machines").val() },
                            success: function (data) {
                                response($.map(data, function (item) {
                                    return {
                                        label: item.BMFixtureNo,
                                        value: item.BMFixtureNo
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
            if ($("#proId").val() == 0) {
                if (IfHasRole("BTN_PROBLEM_SUBMIT")) {
                    $("#submitProblem").removeClass("hidden");
                    $("#resetProblem").removeClass("hidden");
                }
            }
        },

        UploadDelete: function () {
            $(this).parent().remove();
        },
    };

    //审核问题
    $("#approveProblem").on("click", function () {
        $("#submitProblem").addClass("hidden");
        $("#editProblem").addClass("hidden");
        $("#resetProblem").addClass("hidden");
        $("#approveProblem").addClass("hidden");
        if (IfHasRole("BTN_PROBLEM_REJECT")) {
            $("#rejectProblem").removeClass("hidden");
        }
        var params = {
            Id: $("#proId").val() || 0,
            PIProblemNo: $("#problemno").html() || '',
            PIProcessStatus: 101,
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
                        $("#proProcessStatus").val(101);
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

    //审核问题
    $("#rejectProblem").on("click", function () {
        $("#rejectProblem").addClass("hidden");
        if (IfHasRole("BTN_PROBLEM_APPROVE")) {
            $("#approveProblem").removeClass("hidden");
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
        $("#proInfoTab").trigger("click");
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

                    $("#problemno").html(proInfo.PIProblemNo);
                    $("#problemprocess").find("option[value = '" + proInfo.PIProcess + "']").attr("selected", "selected");
                    $("#machines").val(proInfo.PIMachine);
                    $("#tools").val(proInfo.PITool);
                    $("#customerpart").val(proInfo.PICustomerPN);
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

                    $("#problemactionplan").val(proInfo.PIActionPlan);
                    $("#problemrootcause").val(proInfo.PIRootCause);
                    if (proInfo.PIRootCause) {
                        DisableForm("problem-rootcause", 1);
                    }

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
                    SetClose(proInfo.PIStatus);

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


function InitTabs() {
    var id = $("#proId").val();
    if (id == 0) {
        if (IfHasRole("BTN_PROBLEM_SUBMIT")) {
            $("#submitProblem").removeClass("hidden");
            $("#resetProblem").removeClass("hidden");
        }
        return false;
    }
    var status = $("#proProcessStatus").val();
    var severity = $("#problemseverity").val();
    HiddenTabs();
    if (status == 0) {
        if (IfHasRole("BTN_PROBLEM_SUBMIT")) {
            $("#editProblem").removeClass("hidden");
        }
        if (IfHasRole("BTN_PROBLEM_APPROVE")) {
            $("#approveProblem").removeClass("hidden");
        }
    }
    if (status >= 101) {
        if (IfHasRole("BTN_PROBLEM_CLOSE")) {
            $("#closeProblem1").removeClass("hidden");
        }
        $("#solvingteamTab").removeClass("hidden");
        if (status < 201) {
            if (IfHasRole("BTN_PROBLEM_REJECT")) {
                $("#rejectProblem").removeClass("hidden");
            }
            if (IfHasRole("BTN_SOLVTEAM_ADD")) {
                $("#addsolvingteam").removeClass("hidden");
            }
            if (IfHasRole("BTN_SOLVTEAM_APPROVE")) {
                $("#approveSolvingTeam").removeClass("hidden");
            }
        }
    }
    if (status >= 201) {
        $("#qualityalertTab").removeClass("hidden");
        if (status < 301) {
            if (IfHasRole("BTN_SOLVTEAM_REJECT")) {
                $("#rejectSolvingTeam").removeClass("hidden");
            }
            if (IfHasRole("BTN_QUALALERT_ADD")) {
                $("#addToQualityalert").removeClass("hidden");
            }
            if (IfHasRole("BTN_QUALALERT_APPROVE")) {
                $("#approveQualityalert").removeClass("hidden");
            }
        }
    }
    if (status >= 301) {
        $("#sortingactivityTab").removeClass("hidden");
        if (status < 401) {
            if (IfHasRole("BTN_QUALALERT_REJECT")) {
                $("#rejectQualityalert").removeClass("hidden");
            }
            if (IfHasRole("BTN_SORTACT_APPROVE")) {
                $("#approveSortingactivity").removeClass("hidden");
            }
        }
    }
    if (status >= 401) {
        $("#problemNext1").removeClass("hidden");
        if (IfHasRole("BTN_PROBLEM_CLOSE")) {
            $("#closeProblem2").removeClass("hidden");
        }
        $("#containmentactionTab").removeClass("hidden");
        if (status < 501) {
            if (IfHasRole("BTN_SORTACT_REJECT")) {
                $("#rejectSortingactivity").removeClass("hidden");
            }
            if (IfHasRole("BTN_CONTAIN_ADD")) {
                $("#addContainmentAction").removeClass("hidden");
            }
            if (IfHasRole("BTN_CONTAIN_APPROVE1")) {
                $("#approve1Containmentaction").removeClass("hidden");
            }
        }
    }
    if (status >= 501) {
        if (severity == 1) {
            $("#whyanalysisTab").removeClass("hidden");
            if (status < 651) {
                if (IfHasRole("BTN_CONTAIN_REJECT")) {
                    $("#rejectContainmentaction").removeClass("hidden");
                }
                if (IfHasRole("BTN_WHYANALY_ADD")) {
                    $("#addwhyanalysis1").removeClass("hidden");
                    $("#addwhyanalysis2").removeClass("hidden");
                    $("#editRootCause").removeClass("hidden");
                }
                if (IfHasRole("BTN_WHYANALY_APPROVE")) {
                    $("#approveWhyanalysis").removeClass("hidden");
                }
            }
        }
        else {
            if (status < 502) {
                if (IfHasRole("BTN_CONTAIN_APPROVE2")) {
                    $("#approve2Containmentaction").removeClass("hidden");
                }
            }
        }
    }
    if (status >= 502 && severity == 2) {
        $("#whyanalysisTab").removeClass("hidden");
        if (status < 651) {
            if (IfHasRole("BTN_CONTAIN_REJECT")) {
                $("#rejectContainmentaction").removeClass("hidden");
            }
            if (IfHasRole("BTN_WHYANALY_ADD")) {
                $("#addwhyanalysis1").removeClass("hidden");
                $("#addwhyanalysis2").removeClass("hidden");
                $("#editRootCause").removeClass("hidden");
            }
            if (IfHasRole("BTN_WHYANALY_APPROVE")) {
                $("#approveWhyanalysis").removeClass("hidden");
            }
        }
    }
    if (status >= 651) {
        $("#correctiveActionTab").removeClass("hidden");
        if (status < 701) {
            if (IfHasRole("BTN_WHYANALY_REJECT")) {
                $("#rejectWhyanalysis").removeClass("hidden");
            }
            if (IfHasRole("BTN_CORRECT_ADD")) {
                $("#addcorrectivecction").removeClass("hidden");
            }
            if (IfHasRole("BTN_CORRECT_APPROVE")) {
                $("#approveCorrectiveaction").removeClass("hidden");
            }
        }
    }
    if (status >= 701) {
        $("#preventivemeasuresTab").removeClass("hidden");
        if (status < 801) {
            if (IfHasRole("BTN_CORRECT_REJECT")) {
                $("#rejectCorrectiveaction").removeClass("hidden");
            }
            if (IfHasRole("BTN_PREMEASURE_ADD")) {
                $("#addpreventivemeasures").removeClass("hidden");
            }
            if (IfHasRole("BTN_PREMEASURE_APPROVE")) {
                $("#approvePreventivemeasures").removeClass("hidden");
            }
        }
    }
    if (status >= 801) {
        $("#problemNext2").removeClass("hidden");
        $("#layeredauditTab").removeClass("hidden");
        if (status < 901) {
            if (IfHasRole("BTN_PREMEASURE_REJECT")) {
                $("#rejectPreventivemeasures").removeClass("hidden");
            }
            if (IfHasRole("BTN_LAYERDAUDIT_ADD")) {
                $("#addlayeredaudit").removeClass("hidden");
            }
            if (IfHasRole("BTN_LAYERDAUDIT_APPROVE")) {
                $("#approveLayeredaudit").removeClass("hidden");
            }
        }
    }
    if (status >= 901) {
        $("#verificationTab").removeClass("hidden");
        if (status < 1001) {
            if (IfHasRole("BTN_LAYERDAUDIT_REJECT")) {
                $("#rejectLayeredaudit").removeClass("hidden");
            }
            if (IfHasRole("BTN_VERIFICATION_ADD")) {
                $("#addverification").removeClass("hidden");
            }
            if (IfHasRole("BTN_VERIFICATION_APPROVE")) {
                $("#approveVerification").removeClass("hidden");
            }
        }
    }
    if (status >= 1001) {
        $("#standardizationTab").removeClass("hidden");
        if (status < 1101) {
            if (IfHasRole("BTN_VERIFICATION_REJECT")) {
                $("#rejectVerification").removeClass("hidden");
            }
            if (IfHasRole("BTN_STANDA_APPROVE")) {
                $("#approveStandardization").removeClass("hidden");
            }
        }
    }
    if (status >= 1101) {
        $("#problemNext3").removeClass("hidden");
        if (IfHasRole("BTN_PROBLEM_CLOSE")) {
            $("#closeProblem3").removeClass("hidden");
        }
        if (status < 1200) {
            if (IfHasRole("BTN_STANDA_REJECT")) {
                $("#rejectStandardization").removeClass("hidden");
            }
        }
    }
}
function HiddenTabs() {

    var status = $("#proProcessStatus").val();
    var severity = $("#problemseverity").val();
    if (status < 1101) {
        $("#closeProblem3").addClass("hidden");
    }
    if (status < 1001) {
        $("#standardizationTab").addClass("hidden");
    }
    if (status < 901) {
        $("#verificationTab").addClass("hidden");
    }
    if (status < 801) {
        $("#problemNext2").addClass("hidden");
        $("#layeredauditTab").addClass("hidden");
    }
    if (status < 701) {
        $("#preventivemeasuresTab").addClass("hidden");
    }
    if (status < 651) {
        $("#correctiveActionTab").addClass("hidden");
    }
    if (status < 502 && severity == 2) {
        $("#whyanalysisTab").addClass("hidden");
    }
    if (status < 501) {
        $("#whyanalysisTab").addClass("hidden");
        $("#submitRootCause").addClass("hidden");
        $("#editRootCause").addClass("hidden");
    }
    if (status < 401) {
        $("#problemNext1").addClass("hidden");
        $("#containmentactionTab").addClass("hidden");
        $("#closeProblem2").addClass("hidden");
    }
    if (status < 301) {
        $("#sortingactivityTab").addClass("hidden");
    }
    if (status < 201) {
        $("#qualityalertTab").addClass("hidden");
    }
    if (status < 101) {
        $("#solvingteamTab").addClass("hidden");
    }
}

function SetClose(status) {
    if (status == 2) {
        $("#proInfoTab").addClass("hidden");
        $("#proActionTab").addClass("hidden");
        $("#proStandTab").addClass("hidden");
        if ($("#proPreiewTab").parent().hasClass("active")) {
            return false;
        }
        $("#proPreiewTab").trigger("click");
    }
}

function InitUploadAttach() {
    $(".uploadAttachment").off('change');
    $(".uploadAttachment").on('change', function () {
        var _self = this;
        var file = $(this).find(".uploadfileButton")[0];
        if (file.files && file.files[0]) {
            var arr = file.value.split('.');
            var fileType = arr[arr.length - 1];
            var fileSize = file.files[0].size;
            var fileName = file.files[0].name;
            if ("exe|bat".indexOf(fileType) > -1 || fileSize > 5242880) {
                alert("不支持exe、bat格式,大小小于5M");
                return;
            }
            var url = basePath + culture + '/Home/AttachmentUpload';
            $(".uploadAttachment").children().each(function (index, item) {
                $(item).addClass('none');
            });
            var fileObj = file.files[0];
            var form = new FormData();
            form.append("file", fileObj);
            var xhr = new XMLHttpRequest();
            xhr.open("post", url, true);
            xhr.onload = function (result) {
                var data = JSON.parse(result.currentTarget.response);
                if (data.data && data.data.length > 0) {
                    var filedom = "<a href=\"" + data.data[0].FileUrl + "\" data-value=\"" + data.data[0].FileLocalPath + "\"  class=\"fileinfo\">" + data.data[0].FileName + "</a ><a href=\"#\"><i class=\"dropattachment glyphicon glyphicon-remove\"></i></a>";
                    $(_self).append(filedom);
                    $(".dropattachment").off('click');
                    $(".dropattachment").on('click', function () {
                        var drop = this;
                        $(this).parent().prev("a").remove();
                        $(this).parent().prev("input").removeClass('none');
                        $(this).parent().prev().prev("button").removeClass('none');
                        $(this).parent().remove();
                        InitUploadAttach();
                        return false;
                    });
                }
            };
            xhr.send(form);
        }
    });
}

