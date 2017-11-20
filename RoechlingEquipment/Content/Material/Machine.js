(function () {

    'use strict';

    //验证消息扩展
    $.extend($.validator.messages, {
        required: "required"
    });
    // validation machine
    var $machineForm = $("#machine-form");
    $machineForm.validate({
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
            $(label).parent().removeClass('has-error');
            $(label).remove();
        }
    });

    var $toolForm = $("#tool-form");
    $toolForm.validate({
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

    var $materialToolForm = $("#materialtool-form");
    $materialToolForm.validate({
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
            $(label).parent().removeClass('has-error');
            $(label).remove();
        }
    });
    //Tab
    $("#btnma1Tab").on("click", function () {
        $("#MachineClassification").val(1);
        $("#machineSearch1").trigger("click");
    });

    $("#btnma2Tab").on("click", function () {
        $("#MachineClassification").val(2);
        $("#machineSearch2").trigger("click");
    });

    $("#btnma3Tab").on("click", function () {
        $("#MachineClassification").val(3);
        $("#machineSearch3").trigger("click");
    });
    $("#btnma4Tab").on("click", function () {
        $("#MachineClassification").val(4);
        $("#machineToolSearch").trigger("click");
    });
    //Tab End

    //PageInfo Button
    $("#addMachine1").magnificPopup({
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
                $("#resetMachine").trigger("click");
            },
            open: function () {
                $("#MachineId").val(0);
                $("#MachineClassification").val(1);
            },
            close: function () {
                SearchToolList(1, 1,null,null,null,null);
            }
        }
    });

    $("#editMachine1").magnificPopup({
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
                $("#MachineClassification").val(1);
                fillMaTool(1);
            },
            beforeClose: function () {
                $("#bmfixtureno").removeAttr('disabled');
            },
            close: function () {
                SearchToolList(1, 1, null, null, null, null);
            }
        }
    });

    $("#importMachine1").magnificPopup({
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
                $("#MachineClassification").val(1);
            },
            close: function () {
                SearchToolList(1, 1, null, null, null, null);
            }
        },
        close: function () {
            SearchToolList(1, 1, null, null, null, null);
        }
    });

    $("#machineSearch1").on("click", function () {
        SearchToolList(1, 1, $("#MachineName1").val(), $("#EquipmentNo1").val(), $("#Type1").val(), $("#FixtureNo1").val());
    });

    $("#exportMachine1").on("click", function () {
        ExportTool(1, $("#MachineName1").val(), $("#EquipmentNo1").val(),$("#Type1").val(), $("#FixtureNo1").val());
    });

    $("#addMachine2").magnificPopup({
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
                $("#resetMachine").trigger("click");
            },
            open: function () {
                $("#MachineId").val(0);
                $("#MachineClassification").val(2);
            },
            close: function () {
                SearchToolList(2, 1, null, null, null, null);
            }
        }
    });

    $("#editMachine2").magnificPopup({
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
            beforeClose: function () {
                $("#bmfixtureno").removeAttr('disabled');
            },
            open: function () {
                $("#MachineClassification").val(2);
                fillMaTool(2);
            },
            close: function () {
                SearchToolList(2, 1, null, null, null, null);
            }
        }
    });

    $("#importMachine2").magnificPopup({
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
                $("#MachineClassification").val(2);
            },
            close: function () {
                SearchToolList(2, 1, null, null, null, null);
            }
        }
    });

    $("#machineSearch2").on("click", function () {
        SearchToolList(2, 1, $("#MachineName2").val(), $("#EquipmentNo2").val(), $("#Type2").val(), $("#FixtureNo2").val());
    });

    $("#exportMachine2").on("click", function () {
        ExportTool(2, $("#MachineName2").val(), $("#EquipmentNo2").val(), $("#Type2").val(), $("#FixtureNo2").val());
    });

    $("#addMachine3").magnificPopup({
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
                $("#resetMachine").trigger("click");
            },
            open: function () {
                $("#MachineId").val(0);
                $("#MachineClassification").val(3);
            },
            close: function () {
                SearchToolList(3, 1, null, null, null, null);
            }
        }
    });

    $("#editMachine3").magnificPopup({
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
            beforeClose: function () {
                $("#bmfixtureno").removeAttr('disabled');
            },
            open: function () {
                $("#MachineClassification").val(3);
                fillMaTool(3);
            },
            close: function () {
                SearchToolList(3, 1,null);
            }
        }
    });

    $("#importMachine3").magnificPopup({
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
                $("#MachineClassification").val(3);
            },
            close: function () {
                SearchToolList(3, 1, null);
            }

        }
    });

    $("#machineSearch3").on("click", function () {
        SearchToolList(3, 1, $("#MachineName3").val(), $("#EquipmentNo3").val(), $("#Type3").val(), $("#FixtureNo3").val());
    });

    $("#exportMachine3").on("click", function () {
        ExportTool(3, $("#MachineName3").val(), $("#EquipmentNo3").val(), $("#Type3").val(), $("#FixtureNo3").val());
    });

    $("#importMachineTool").magnificPopup({
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
                $("#MachineClassification").val(4);
            },
            close: function () {
                SearchMaterialToolList(1);
            }
        },
        close: function () {
            SearchMaterialToolList(1);
        }
    });

    $("#machineToolSearch").on("click", function () {
        SearchMaterialToolList(1);
    });
    $("#addMachineTool").magnificPopup({
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
                $("#materialToolReset").trigger("click");
            },
            open: function () {
                $("#MachineId").val(0);
                $("#MachineClassification").val(4);
            },
            close: function () { 
                SearchMaterialToolList(1)
            }
        }
    });
    $("#editMachineTool").magnificPopup({
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
                $("#MachineClassification").val(4);
                fillMachineTool();
            },
            close: function () {
                SearchMaterialToolList(1)
            }
        }
    });
    //PageInfo Button End

    //Operate page Button
    $("#operateMachine").on("click", function () {
        operateMaTool();
        return false;
    });

    $("#cancelMachine").on("click", function () {
        $.magnificPopup.close();
    });

    $("#materialTool").on("click", function () { 
        if (!($("#materialtool-form").valid())) return;
        var id = $("#MachineId").val() || 0;

        var params = {
            Id: id,
            MTToolNo: $("#toolNo").val(),
            MTSapPN: $.trim($("#sapPN").val()),
            MTSapQuantity: $.trim($("#sapQuantity").val()),
            MTSapLibrary: $.trim($("#sapLibrary").val()),
            MTSapProductName: $.trim($("#sapProductName").val()),
            MTToolLibrary: $.trim($("#toolLibrary").val()),
            MTProductName: $.trim($("#productName").val()),
            MTStatus: $.trim($("#status").val()),
            MTQuality: $.trim($("#quality").val()),
            MTCustomerPN: $.trim($("#customerPN").val()),
            MTCustomerNo: $.trim($("#customerNo").val()),
            MTOutlineDimension: $.trim($("#outlineDimension").val()),
            MTBelong: $.trim($("#belong").val()),
            MTToolSupplier: $.trim($("#toolSupplier").val()),
            MTToolSupplierNo: $.trim($("#toolSupplierNo").val()),
            MTProductDate: $.trim($("#productDate").val()),
            MTCavity: $.trim($("#cavity").val())
        }
        $.ajax({
            type: "Post",
            url: "MaterialToolOperate",
            async: true,
            data: JSON.stringify(params),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) { 
                if (data != null) {
                    if (id > 0) {
                        $("#MachineId").val(0);
                        alert("successful");
                        $.magnificPopup.close();
                    }
                    else {
                        alert("successful");
                        $.magnificPopup.close();
                    }
                }
            }
        });

        return false;
    });
    $("#materialToolCancel").on("click", function () {
        $.magnificPopup.close();
    });
    //Operate page Button End

    //Import page Button
    $("#Machineupload").click(function (data) {
        if ($("#machinefile").val() == "") {
            alert("please choose file");
            return false;
        }
        else {
            var type = $("#MachineClassification").val() || 0;
            var url;
            if (type != 4) {
                url = basePath + culture + '/Material/ImportMachine';
            }
            else {
                url = basePath + culture + '/Material/ImportMaterialTool';
            }
            $.ajaxFileUpload({
                url: url,
                secureuri: false,
                fileElementId: "machinefile",
                //dataType: 'json',
                data: { 'type': type },
                success: function (result) {
                    var data = JSON.parse(result.body.innerHTML);
                    if (data.IsSuccess) {
                        var msg = " upload successful";
                        if (data.invalidData && data.invalidData.length > 0) {
                            msg += "</br> the date fromat should be yyyy-MM-dd yyyy/MM/dd yyyy.MM.dd:"
                            for (var i = 0; i < data.invalidData.length; i++) {
                                var item = data.invalidData[i];
                                msg += "</br> Equipment No/Fixture No:<span class='error'>" + item.Key + "</span> Manufactured Date:<span class='error'>" + item.Value1 + "</span> Incoming Date:<span class='error'>" + item.Value2 + "</span>";
                            }
                        }
                        Confirm({
                            content: msg, determine: "O K",
                            callback: function (res) {
                            }
                        });
                        //alert(msg);
                    }
                    else {
                        alert(data.Message);
                    }
                    $.magnificPopup.close();
                },
                error: function (data) {
                }
            });
        }
        return false;
    }); 
    $("#MachineuploadCancel").on("click", function (event) {
        $("#machinefile").val('');
        event.preventDefault();
        $.magnificPopup.close();
    });

    //To Delete
    //$("#Toolupload").click(function (data) {
    //    if ($("#importToolfile").val() == "") {
    //        alert("please choose file");
    //        return false;
    //    }
    //    else { 
    //        var url = basePath + culture + '/Material/Importool';
    //        $.ajaxFileUpload({
    //            url: url,
    //            secureuri: false,
    //            fileElementId: "importToolfile",
    //            //dataType: 'json',
    //            data: { 'type': 1 },
    //            success: function (result) {
    //                var data = JSON.parse(result.body.innerHTML);
    //                if (data.IsSuccess) {
    //                    alert("upload successful");
    //                }
    //                else {
    //                    alert(data.Message);
    //                }
    //                $.magnificPopup.close();
    //            },
    //            error: function (data) {
    //            }
    //        });
    //    }
    //    return false;
    //});
    //$("#TooluploadCancel").click(function () {
    //    event.preventDefault();
    //    $.magnificPopup.close();
    //});
    $("#btnMachineTempDown").click(function () {
        var url = $("#btnMachineTempDown").attr("data-url");
        var classification = $("#MachineClassification").val() || 1;
        url += "?classification=" + classification;
        window.open(url);
    });
    //Import page Button End


    $("#btnma1Tab").trigger("click");
    //SearchToolList(1, 1,null);
    //SearchToolList(2, 1,null);
    //SearchToolList(3, 1,null);
    //SearchMaterialToolList(1);
}).apply(this, [jQuery]);

function SearchMaterialToolList(pageIndex) {
    var search = { 
        ToolNo: $.trim($("#toolNo").val()),
        ProductName: $.trim($("#productName").val()),
        ToolSupplier: $.trim($("#toolSupplier").val()),
        CurrentPage: pageIndex
    }
    $.ajax({
        type: "Post",
        url: "MaterialToolSearchResult",
        async: false,
        data: JSON.stringify(search),
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        success: function (data) {
            if (data != null) {
                $("#machineToolResult").html(data); 
            }
        }
    });
}
function SearchToolListWithOutType(pageIndex) {
    var type = $("#MachineClassification").val() || 0;
    var name = null;
    var equipmentNo = null;
    var pageType = null;
    var fixtureNo = null;
    if (type == 1) {
        name = $("#MachineName1").val();
        equipmentNo = $("#EquipmentNo1").val();
        pageType = $("#Type1").val();
        fixtureNo = $("#FixtureNo1").val();
    }
    else if (type == 2) {
        name = $("#MachineName2").val();
        equipmentNo = $("#EquipmentNo2").val();
        pageType = $("#Type2").val();
        fixtureNo = $("#FixtureNo2").val();
    }
    else if (type == 3)
    {
        name = $("#MachineName3").val();
        equipmentNo = $("#EquipmentNo3").val();
        pageType = $("#Type3").val();
        fixtureNo = $("#FixtureNo3").val();
    }
    
    SearchToolList(type, pageIndex, name, equipmentNo, pageType, fixtureNo);
}

function SearchToolList(type, pageIndex, machineName, equipmentNo, pageType, fixtureNo) {
    if (type <= 0 || type > 3) {
        return;
    }
    var search = { 
        MachineName: $.trim(machineName),
        EquipmentNo: $.trim(equipmentNo),
        Type: $.trim(pageType),
        FixtureNo: $.trim(fixtureNo),
        Classification: type,
        CurrentPage: pageIndex
    }
    $.ajax({
        type: "Post",
        url: "MachineToolSearch",
        async: false,
        data: JSON.stringify(search),
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        success: function (data) {
            if (data != null) {
                if (type == 1) {
                    $("#machineResult1").html(data);
                }
                else if (type == 2) {
                    $("#machineResult2").html(data);
                }
                else if (type == 3) {
                    $("#machineResult3").html(data);
                }
            }
        }
    });
}

function ExportTool(type, machineName, equipmentNo, pageType, fixtureNo) {
    var url = basePath + culture + '/Material/MachineToolExcel?Classification=' + type
        + '&MachineName=' + $.trim(machineName)
        + '&EquipmentNo=' + $.trim(equipmentNo)
        + '&Type=' + $.trim(pageType)
        + '&FixtureNo=' + $.trim(fixtureNo)
    window.open(url);
}
function operateMaTool() {
    if (!($("#machine-form").valid())) return;
    var id = $("#MachineId").val() || 0;

    var params = {
        Id: id,
        BMEquipmentName: $("#bmequipmentname").val() || '',
        BMClassification: $("#MachineClassification").val() || 1,
        BMEquipmentNo: $("#bmequipmentno").val() || '',
        BMFixtureNo: $("#bmfixtureno").val() || '',
        BMType: $("#bmtype").val() || '',
        BMSerialNumber: $("#bmserialnumber").val() || '',
        BMQuantity: $("#bmquantity").val() || '',
        BMManufacturedDate: $("#bmmanufactureddate").val() || '',
        BMPower: $("#bmpower").val() || '',
        BMOutlineDimension: $("#bmoutlinedimension").val() || '',
        BMAbility: $("#bmability").val() || '',
        BMNeedPressureAir: $("input[name='bmneedpressureair-radios']:checked").val() || 0,
        BMNeedCoolingWater: $("input[name='bmneedcoolingwater-radios']:checked").val() || 0,
        BMIncomingDate: $("#bmincomingdate").val() || '',
        BMRemarks: $("#bmremarks").val() || '',
    }
    $.ajax({
        type: "Post",
        url: "MaToolOperate",
        async: true,
        data: JSON.stringify(params),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (id > 0) {
                    $("#MachineId").val(0);
                    alert("successful");
                    $.magnificPopup.close();
                }
                else {
                    alert("successful");
                    $.magnificPopup.close();
                }
            }
        }
    });
}

function fillMaTool(type) {
    var id = $("#MachineId").val();
    $.ajax({
        type: "GET",
        url: "GetOneMaTool",
        data: { matoolId: id },
        dataType: "json",
        success: function (data) {
            if (data && data.Id > 0) {
                $("#bmequipmentname").val(data.BMEquipmentName);
                $("#bmequipmentno").val(data.BMEquipmentNo);
                $("#bmfixtureno").val(data.BMFixtureNo);
                $("#bmtype").val(data.BMType);
                $("#bmserialnumber").val(data.BMSerialNumber);
                $("#bmquantity").val(data.BMQuantity);
                $("#bmmanufactureddate").val(data.BMManufacturedDate);
                $("#bmpower").val(data.BMPower);
                $("#bmoutlinedimension").val(data.BMOutlineDimension);
                $("#bmability").val(data.BMAbility);
                $("input[name='bmneedpressureair-radios'][value='" + data.BMNeedPressureAir + "']").prop('checked', true);
                $("input[name='bmneedcoolingwater-radios'][value='" + data.BMNeedCoolingWater + "']").prop('checked', true);
                $("#bmincomingdate").val(data.BMIncomingDate);
                $("#bmremarks").val(data.BMRemarks);
                $("#bmfixtureno").attr("disabled", "disable");
            }
        }
    });
}
function fillMachineTool() {
    var id = $("#MachineId").val();
    $.ajax({
        type: "GET",
        url: "GetOneMachineTool",
        data: { materialToolId: id },
        dataType: "json",
        success: function (data) {
            if (data && data.Id > 0) {
                $("#toolNo").val(data.MTToolNo);
                $("#sapPN").val(data.MTSapPN);
                $("#sapQuantity").val(data.MTSapQuantity);
                $("#sapLibrary").val(data.MTSapLibrary);
                $("#sapProductName").val(data.MTSapProductName);
                $("#toolLibrary").val(data.MTToolLibrary);
                $("#productName").val(data.MTProductName);
                $("#status").val(data.MTStatus);
                $("#quality").val(data.MTQuality);
                $("#customerPN").val(data.MTCustomerPN);
                $("#customerNo").val(data.MTCustomerNo);
                $("#outlineDimension").val(data.MTOutlineDimension);
                $("#belong").val(data.MTBelong);
                $("#toolSupplier").val(data.MTToolSupplier);
                $("#toolSupplierNo").val(data.MTToolSupplierNo);
                $("#productDate").val(data.MTProductDate);
                $("#cavity").val(data.MTCavity);
            }
        }
    });
}
function EditMachine(Id) {
    $("#MachineId").val(Id);
    var classification = $("#MachineClassification").val() || 1;
    $("#editMachine" + classification).trigger("click");
}
function EditMachineTool(Id) {
    $("#MachineId").val(Id); 
    $("#editMachineTool").trigger("click");
}
function DeleteMachine(id) {
    $.ajax({
        type: 'post',
        url: 'DeleteMachine',
        dataType: "json",
        data: {
            Id: id
        },
        success: function (data) {
            if (data) {
                alert("Delete success!");
                SearchToolList($("#MachineClassification").val(),1);
            }
            else {
                alert("Delete fail!");
            }
        }
    });
}
function DeleteMaterialTool(id) {
    $.ajax({
        type: 'post',
        url: 'DeleteMaterialTool',
        dataType: "json",
        data: {
            Id: id
        },
        success: function (data) {
            if (data) {
                alert("Delete success!");
                SearchMaterialToolList(1);
            }
            else {
                alert("Delete fail!");
            }
        }
    });
}

