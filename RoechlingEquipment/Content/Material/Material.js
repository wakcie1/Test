(function () {

    'use strict';

    //验证消息扩展
    $.extend($.validator.messages, {
        required: "required"
    });
    // validation summary
    var $summaryForm = $("#material-form");
    $summaryForm.validate({
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

    //更新物料
    $("#OperateMaterial").on("click", function () {
        if (!($("#material-form").valid())) return;
        var id = $("#MIId").val() || 0;
        var params = {
            Id: $("#MIId").val() || 0,
            MIProcessType: $.trim($("#processType").val())||'',
            MICustomerPN: $("#customerpn").val() || '',
            MIProductName: $("#productname").val() || '',
            MICustomer: $("#customer").val() || '',
            MIPicture: '',
            MIPictureUrl: '',
            MIMaterialPN: $("#materialpn").val() || '',
            MIMoldNo: $("#moldno").val() || '',
            MISapPN: $("#sappn").val() || '',
            MIInjectionMC: $("#injectionmc").val() || '',
            MICavity: $("#cavity").val() || 0,
            MICycletime: $("#cycletime").val() || 0,
            MICycletimeCav: $("#cycletimecav").val() || 0,
            MIStandardHeadcount: $("#standardheadcount").val() || 0,
            MTStandardScrap: $.trim($("#standardScrap").val()) || '',
            MICavityG: $("#cavityg").val() || 0,
            MIWorkOrder: $("#workorder").val() || '',
            MIAssAC: $.trim($("#assAC").val()) || ''
        }
        $.ajax({
            type: "Post",
            url: "MaterialOperate",
            async: false,
            data: JSON.stringify(params),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    if (id > 0) {
                        $("#MIId").val(0);
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
    });
    $("#CancelMaterial").on("click", function () {
        $.magnificPopup.close();
    });

    $("#Search").on("click", function () {
        SearchMaterialList(1);
    });

    $("#btnSAPTempDown").click(function () {
        var url = $("#btnSAPTempDown").attr("data-url");
        window.open(url);
    });

    $("#addMaterial").magnificPopup({
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
                $("#ResetMaterial").trigger("click");

            },
            open: function () {
                $("#MIId").val(0);
            },
            close: function () {
                SearchMaterialList(1);
            }

        }
    });

    $("#importMaterial").magnificPopup({
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
                SearchMaterialList(1);
            },
        }
    });

    $("#editMaterial").magnificPopup({
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
                var Id = $("#MIId").val();
                $.ajax({
                    type: "GET",
                    url: "GetOneMaterial",
                    data: { materialId: Id },
                    dataType: "json",
                    success: function (data) {
                        $("#processType").val(data.MIProcessType);
                        $("#MIId").val(data.Id);
                        $("#sappn").val(data.MISapPN);
                        $("#productname").val(data.MIProductName);
                        $("#customerpn").val(data.MICustomerPN);
                        $("#customer").val(data.MICustomer);
                        $("#moldno").val(data.MIMoldNo);
                        $("#materialpn").val(data.MIMaterialPN);
                        $("#injectionmc").val(data.MIInjectionMC);
                        $("#standardheadcount").val(data.MIStandardHeadcount);
                        $("#standardScrap").val(data.MTStandardScrap);
                        $("#cavity").val(data.MICavity);
                        $("#cycletime").val(data.MICycletime);
                        $("#cycletimecav").val(data.MICycletimeCav);
                        $("#cavityg").val(data.MICavityG);
                        $("#workorder").val(data.MIWorkOrder);
                        $("#sappn").attr("disabled", "disable");
                        $("#assAC").val(data.MIAssAC);
                    }
                });
            },
            beforeClose: function () {
                $("#sappn").removeAttr('disabled');
            },
            close: function () {
                SearchMaterialList(1);
            }
        }
    });

    //Tab
    $("#btnwo1Tab").on("click", function () {
        $("#WorkOrderName1").val('');
        $("#SapNo1").val('');
        $("#WorkOrderType").val(1);
        SearchMaterialOtherList(1, 1, null, null);
    });

    $("#btnwo2Tab").on("click", function () {
        $("#WorkOrderName2").val('');
        $("#SapNo2").val('');
        $("#WorkOrderType").val(2);
        SearchMaterialOtherList(1, 2, null, null);
    });

    $("#btnwo3Tab").on("click", function () {
        $("#WorkOrderName3").val('');
        $("#SapNo3").val('');
        $("#WorkOrderType").val(3);
        SearchMaterialOtherList(1, 3, null, null);
    });

    $("#btnwo4Tab").on("click", function () {
        $("#WorkOrderName4").val('');
        $("#SapNo4").val('');
        $("#WorkOrderType").val(4);
        SearchMaterialOtherList(1, 4, null, null);
    });

    //Tab End
    SearchMaterialList(1);
    $("#btnbasicinfoTab").trigger("click");

    $("#workOrderTempDown").click(function () {
        var url = $("#workOrderTempDown").attr("data-url");
        var type = $("#WorkOrderType").val() || 1;
        url += "?type=" + type;
        window.open(url);
    });

    $("#workOrderUpload").click(function (data) {
        if ($("#workOrderFile").val() == "") {
            alert("please choose file");
            return false;
        }
        else {
            var type = $("#WorkOrderType").val() || 0;
            var url = basePath + culture + '/Material/ImportWorkOrder';
            $.ajaxFileUpload({
                url: url,
                secureuri: false,
                fileElementId: "workOrderFile",
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

    $("#WorkOrderSubmmitBtn").on("click", function () {
        if (!($("#workOrder-form").valid())) return;
        var id = $("#WorkOrderId").val() || 0;
        var params = {
            Id: $("#WorkOrderId").val() || 0,
            WIWorkOrder: $("#orderNo").val() || '',
            WISapPN: $("#sapNo").val() || '',
            WIProductName: $("#product").val() || '', 
            WIReceiptTime: $("#receiptTime").val() || '',
            WIReceiptBy: $("#receiptBy").val() || '', 
            WIType: $("#WorkOrderType").val()
        }
        $.ajax({
            type: "Post",
            url: "WorkOrderSubmmit",
            async: false,
            data: JSON.stringify(params),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null) {
                    if (id > 0) {
                        $("#id").val(0);
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
    });

    $("#WorkOrderCancelBtn").on("click", function () {
        $.magnificPopup.close(); 
    });
}).apply(this, [jQuery]);

function SearchMaterialList(pageIndex) {
    var search = {
        Customer: $("#customerIndex").val() || "",
        SapPN: $("#sappnIndex").val() || "",
        ProductName: $("#productnameIndex").val() || "",
        CurrentPage: pageIndex
    }
    $.ajax({
        type: "Post",
        url: "MaterialSearch",
        async: false,
        data: JSON.stringify(search),
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        success: function (data) {
            $("#searchResult").html(data);
        }
    });
}

function EditMaterial(Id) {
    $("#MIId").val(Id);
    $("#editMaterial").trigger("click");
}
function DeleteMaterial(id) {
    $.ajax({
        type: 'post',
        url: 'DeleteMaterial ',
        dataType: "json",
        data: {
            Id: id
        },
        success: function (data) {
            if (data) {
                alert("Delete success!");
                SearchMaterialList(1);
            }
            else {
                alert("Delete fail!");
            }
        }
    });
}


$("#btnExport").click(function () {
    var url = basePath + culture + '/Material/MaterialExcel?Customer=' + $.trim($("#customerIndex").val())
        + "&SapPN=" + $.trim($("#sappnIndex").val()) + "&ProductName=" + $("#productnameIndex").val();
    window.open(url);
})

$("#SAPupload").click(function () {
    if ($("#defectfile").val() == "") {
        alert("please choose file");
        return false;
    }
    else {
        var url = basePath + culture + '/Material/UploadMaterialFiles';
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
    }
    return false;
});

$("#SAPuploadCancel").click(function () {
    $("#defectfile").val('');
    $.magnificPopup.close();
    return false;
});

//workOrderSearch Todo Add Name to search
function SearchMaterialOtherList(pageIndex, type, WorkOrderName,SapNo) {
    var search = {
        Type: type,
        WorkOrderName: $.trim(WorkOrderName),
        SapNo: $.trim(SapNo),
        CurrentPage: pageIndex
    }
    $.ajax({
        type: "Post",
        url: "MaterialOtherSearchResult",
        async: false,
        data: JSON.stringify(search),
        contentType: "application/json;charset=utf-8",
        dataType: "html",
        success: function (data) {
            if (type == '1')
                $("#material1Result").html(data);
            else if (type == '2')
                $("#material2Result").html(data);
            else if (type == '3')
                $("#material3Result").html(data);
            else if (type == '4')
                $("#material4Result").html(data);
        }
    });
} 
//WorkOrderExport
function ExportWorkOrder(type, workOrderName, sapNo) {
    var url = basePath + culture + '/Material/WorkOrderExport?Type=' + type + '&WorkOrderName=' + $.trim(workOrderName) + '&SapNo=' + $.trim(sapNo)
    window.open(url);
}
//Edit get WorkOrder
function FillWorkOrder() {
    var id = $("#WorkOrderId").val();
    $.ajax({
        type: "GET",
        url: "GetWorkOrderById",
        data: { workOrderId: id },
        dataType: "json",
        success: function (data) {
            if (data && data.Id > 0) {
                $("#orderNo").val(data.WIWorkOrder);
                $("#sapNo").val(data.WISapPN);
                $("#product").val(data.WIProductName);
                $("#receiptTime").val(data.WIReceiptTime);
                $("#receiptBy").val(data.WIReceiptBy); 
            }
        }
    });
} 
function EditWorkOrder(Id) {
    $("#WorkOrderId").val(Id);
    var type = $("#WorkOrderType").val();
    if (type == 1) {

        $("#material1EditBtn").trigger("click");
    }
    else if (type == 2) {

        $("#material2EditBtn").trigger("click");
    }
    else if (type == 3) {

        $("#material3EditBtn").trigger("click");

    }
    else if (type == 4) {

        $("#material4EditBtn").trigger("click");
    } 
}
function DeleteWorkOrder(id) {
    $.ajax({
        type: 'post',
        url:  'DeleteWorkOrder',
        dataType: "json",
        data: {
            Id: id
        },
        success: function (data) {
            if (data) {
                alert("Delete success!"); 
                SearchMaterialOtherList(1, $("#WorkOrderType").val(), null, null)
            }
            else {
                alert("Delete fail!");
            }
        }
    });
}
function SearchMaterialOtherWithOutType(pageIndex) {
    var type = $("#WorkOrderType").val() || 0;
    var name = null;
    var sapNo = null;
    if (type == 1) {
        name = $("#WorkOrderName1").val();
        sapNo=$("#SapNo1").val()
    }
    else if (type == 2) {
        name = $("#WorkOrderName2").val();
        sapNo = $("#SapNo2").val()
    }
    else if (type == 3) {
        name = $("#WorkOrderName3").val();
        sapNo = $("#SapNo3").val()
    }
    else if (type == 4) {
        name = $("#WorkOrderName4").val();
        sapNo = $("#SapNo4").val()
    }

    SearchMaterialOtherList(pageIndex, type, name, sapNo);
}
 //molding
$("#material1SearchBtn").on("click", function () {
    SearchMaterialOtherList(1, 1, $("#WorkOrderName1").val(), $("#SapNo1").val());
}); 
$("#material1AddBtn").magnificPopup({
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
            $("#WorkOrderResetBtn").trigger("click");
        },
        open: function () {
            $("#WorkOrderId").val(0);
            $("#WorkOrderType").val(1);
        },
        close: function () {
            SearchMaterialOtherList(1, 1,null,null);
        }
    },
    close: function () {
        SearchMaterialOtherList(1, 1, null,null);
    }
}); 
$("#material1EditBtn").magnificPopup({
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
            $("#WorkOrderResetBtn").trigger("click");
        },
        open: function () {
            $("#WorkOrderType").val(1);
            FillWorkOrder();
        }, 
        close: function () {
            SearchMaterialOtherList(1, 1, null,null);
        }
    },
    close: function () {
        SearchMaterialOtherList(1, 1, null,null);
    }
}); 
$("#material1ImportBtn").magnificPopup({
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
            $("#WorkOrderType").val(1);
        },
        close: function () {
            SearchMaterialOtherList(1, 1, null,null);
        }
    },
    close: function () {
        SearchMaterialOtherList(1, 1, null,null);
    }
}); 
$("#material1ExportBtn").on("click", function () {
    ExportWorkOrder(1, $("#WorkOrderName1").val(),$("#SapNo1").val());
}); 
// Assy line
$("#material2SearchBtn").on("click", function () {
    SearchMaterialOtherList(1, 2, $("#WorkOrderName2").val(), $("#SapNo2").val());
});
$("#material2AddBtn").magnificPopup({
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
            $("#WorkOrderResetBtn").trigger("click");
        },
        open: function () {
            $("#WorkOrderId").val(0);
            $("#WorkOrderType").val(2);
        },
        close: function () {
            SearchMaterialOtherList(1, 2, null,null);
        }
    },
    close: function () {
        SearchMaterialOtherList(1, 2, null,null);
    }
});   
$("#material2EditBtn").magnificPopup({
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
            $("#WorkOrderResetBtn").trigger("click");
        },
        open: function () {
            $("#WorkOrderType").val(2);
            FillWorkOrder();
        },
        close: function () {
            SearchMaterialOtherList(1, 2  , null,null);
        }
    },
    close: function () {
        SearchMaterialOtherList(1, 2, null,null);
    }
}); 
$("#material2ImportBtn").magnificPopup({
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
            $("#WorkOrderType").val(2);
        },
        close: function () {
            SearchMaterialOtherList(1, 2, null,null);
        }
    },
    close: function () {
        SearchMaterialOtherList(1, 2, null,null);
    }
}); 
$("#material2ExportBtn").on("click", function () {
    ExportWorkOrder(2, $("WorkOrderName2").val(), $("#SapNo2").val());
});
// 3D blow molding
$("#material3SearchBtn").on("click", function () {
    SearchMaterialOtherList(1, 3, $("#WorkOrderName3").val(), $("#SapNo3").val());
});
$("#material3AddBtn").magnificPopup({
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
            $("#WorkOrderResetBtn").trigger("click");
        },
        open: function () {
            $("#WorkOrderId").val(0);
            $("#WorkOrderType").val(3);
        },
        close: function () {
            SearchMaterialOtherList(1, 3, null,null);
        }
    },
    close: function () {
        SearchMaterialOtherList(1, 3, null,null);
    }
});
$("#material3EditBtn").magnificPopup({
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
            $("#WorkOrderResetBtn").trigger("click");
        },
        open: function () {
            $("#WorkOrderType").val(3);
            FillWorkOrder();
        },
        close: function () {
            SearchMaterialOtherList(1, 3, null,null);
        }
    },
    close: function () {
        SearchMaterialOtherList(1, 3, null,null);
    }
});
$("#material3ImportBtn").magnificPopup({
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
            $("#WorkOrderType").val(3);
        },
        close: function () {
            SearchMaterialOtherList(1, 3, null,null);
        }
    },
    close: function () {
        SearchMaterialOtherList(1, 3, null,null);
    }
});
$("#material3ExportBtn").on("click", function () {
    ExportWorkOrder(3, $("WorkOrderName3").val(), $("#SapNo3").val());
});
// Foaming
$("#material4SearchBtn").on("click", function () {
    SearchMaterialOtherList(1, 4, $("#WorkOrderName4").val(),$("#SapNo4").val());
});
$("#material4AddBtn").magnificPopup({
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
            $("#WorkOrderResetBtn").trigger("click");
        },
        open: function () {
            $("#WorkOrderId").val(0);
            $("#WorkOrderType").val(4);
        },
        close: function () {
            SearchMaterialOtherList(1, 4  , null,null);
        }
    },
    close: function () {
        SearchMaterialOtherList(1, 4 , null,null);
    }
});
$("#material4EditBtn").magnificPopup({
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
            $("#WorkOrderResetBtn").trigger("click");
        },
        open: function () {
            $("#WorkOrderType").val(4);
            FillWorkOrder();
        },
        close: function () {
            SearchMaterialOtherList(1, 4, null,null);
        }
    },
    close: function () {
        SearchMaterialOtherList(1, 4, null,null);
    }
});
$("#material4ImportBtn").magnificPopup({
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
            $("#WorkOrderType").val(4);
        },
        close: function () {
            SearchMaterialOtherList(1, 4, null,null);
        }
    },
    close: function () {
        SearchMaterialOtherList(1, 4, null,null);
    }
});
$("#material4ExportBtn").on("click", function () {
    ExportWorkOrder(4, $("WorkOrderName4").val(), $("#SapNo4").val());
});

//For WorkOrderImport
$("#workOrderCancel").on("click", function (event) {
    $("#workOrderFile").val('');
    event.preventDefault();
    $.magnificPopup.close();
});
