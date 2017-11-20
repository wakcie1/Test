
//闭包（避免全局污染）
//加上分号（防止压缩出错）
;
(function (window, jQuery, undefined) {
    var config = {};
    var _instance;

    var events = {
        getDepartList: function () {
            var that = this;
            $.ajax({
                url: basePath + culture+ '/Home/DepartmentManagerList',
                type: 'post',
                success: function (data) {
                    $('#firstUL').html(data);
                    $('#btnAdd_0').show();
                    $("#tableWrapper li").not("li:eq(0)").hide();
                }
            });
        },

        //节点点击事件：显示或隐藏下级节点
        InputEvent: function (i) {
            var children = $("li[name='li_" + i + "']");
            var span = $("span[id='span_" + i+"']");
            $("input[type=button]").hide();
            $("a[id^='btnUpdateValid_'").hide();
            $("a[id^='btnAdd_'").hide();
            $("a[id^='btnsib_'").hide();
            $("a[id^='btn_'").hide();
            //先隐藏所有按钮，然后显示该行的按钮
            $("a[id='btnsib_"+ i+"']").show(); //新增平级子模块按钮     
            $("a[id='btnAdd_" + i+"']").show(); //新增下级子模块按钮    
            $("a[id='btn_" + i+"']").show(); //编辑按钮      
            $("a[id='btnUpdateValid_" + i + "']").show(); //无效/有效按钮
            var modal = $("#modal-template").html();
            $(".btn-secondary-setunvalid,.btn-secondary-setvalid").magnificPopup({  //有效无效弹框
                items: {
                    src: modal,
                    type: 'inline'
                }
            });
            //初始化新建下级部门弹框
            $("a[id='btnAdd_" + i + "']").magnificPopup({
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
                        $("#departmenRest").trigger("click");
                    },
                    open: function () {
                        var o = $("a[id='btnAdd_" + i + "']");
                        var index = o.attr('id').indexOf('_') + 1;
                        var id = o.attr('id').substr(index, o.attr('id').length - index);
                        $.ajax({
                            type: 'post',
                            url: basePath + culture + '/Home/DepartmentCreate',
                            dataType: "json",
                            data: {
                                departmentId: id,
                                level: 0
                            },
                            success: function (data) {
                                $("#ParentName").val(data.ParentName);
                                $("#ParentId").val(data.ParentId);
                            },
                        });
                    },

                }
            });

            //初始化新建平级部门弹框
            $("a[id='btnsib_" + i + "']").magnificPopup({
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
                        $("#departmenRest").trigger("click");
                    },
                    open: function () {
                        var o = $("a[id='btnsib_" + i + "']");
                        var index = o.attr('id').indexOf('_') + 1;
                        var id = o.attr('id').substr(index, o.attr('id').length - index);
                        $.ajax({
                            type: 'post',
                            url: basePath + culture + '/Home/DepartmentCreate',
                            dataType: "json",
                            data: {
                                departmentId: id,
                                level: 1
                            },
                            success: function (data) {
                                $("#ParentName").val(data.ParentName);
                                $("#ParentId").val(data.ParentId);
                            },
                        });
                    },

                }
            });

            //初始化编辑
            $("a[id='btn_" + i + "']").magnificPopup({
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
                        $("#departmenRest").trigger("click");
                    },
                    open: function () {
                        var o = $("a[id='btn_" + i + "']");
                        var index = o.attr('id').indexOf('_') + 1;
                        var id = o.attr('id').substr(index, o.attr('id').length - index);
                        $.ajax({
                            type: 'post',
                            url: basePath + culture + '/Home/DepartmentEdit',
                            dataType: "json",
                            data: {
                                id: id,
                            },
                            success: function (data) {
                                $("#departmentId").val(data.Id);
                                $("#departmentName").val(data.Name);
                                $("#ParentName").val(data.ParentName);
                                $("#ParentId").val(data.ParentId);
                                $("#DepartDesc").val(data.Description);
                            },
                        });
                    },

                }
            });
            if (children.is(":visible")) {
                children.hide('fast');
                span.attr('title', '展开子指标').find(' > i').addClass('icon-plus-sign').removeClass('icon-minus-sign');
            } else {
                children.show('fast');
                span.attr('title', '折叠子指标').find(' > i').addClass('icon-minus-sign').removeClass('icon-plus-sign');
            }
        },
        //展开节点
        ExpandParent: function (i) {
            var span = $("span[id='span_" + i + "']");
            var spanParent = span.parent().parent().siblings("span");
            var liParent = spanParent.parent();
            liParent.show('fast');
            spanParent.attr('title', '折叠子指标').find(' > i').addClass('icon-minus-sign').removeClass('icon-plus-sign');

            var index = $(spanParent).attr('id').indexOf('_') + 1;
            var parentid = spanParent.attr('id').substr(index, spanParent.attr('id').length - index);

            if (parentid != 0) {
                events.ExpandParent(parentid);
            }
        },
        //添加节点
        AddNode:function(Code, parentId, DepartName) {
            var rethtml = ` <li class="parent_li" name="li_` + parentId + `">
                                         <span title="展开子部门" id="span_`+ Code + `" data-id="` + Code + `" style="padding:0px 8px;"><font>` + DepartName + `</font></span>
                                            <a class="btn bk-margin-5 btn-secondary-current  btn-primary" href="#departmentModal" id="btnsib_`+ Code + `"  style="display:none">New flat</a>
                                            <a class="btn bk-margin-5 btn-secondary-next  btn-success" href="#departmentModal" id="btnAdd_`+ Code + `" data-id="` + parentId + `" style="display:none">New subordinate</a>
                                            <a class="btn bk-margin-5 btn-secondary-edit btn-info" href="#departmentModal"  id="btn_`+ Code + `"  data-id="` + Code + `" style="display:none">Edit</a>
                                                <a class="btn bk-margin-5 btn-secondary-setunvalid btn-warning"  id="btnUpdateValid_`+ Code + `" style="display:none">Disable</a>
                                        <ul style="float: none">
                                       </ul>
                                    </li>`;
            $("span[id='span_" + parentId + "']").nextAll("ul").append(rethtml);
            $("#DIName").append("<option value='" + Code + "'>" + DepartName + "</option>");
        },
        AddNewNodeFalse: function (data) {
            $.magnificPopup.close();
            alert(data);
        },
        
        EditSuccess: function (departmentId,departName) {
            $.magnificPopup.close();
            alert("Department:" + departName + "Edit success!");
            window.location.reload();
        },
        AddSuccess: function (code, parentId, departmentName) {
            $.magnificPopup.close();
            alert("Add Depaartment success");
            window.location.reload();
        },
        //启用
        UpdateDepartValid: function () {
            var o = $(this);
            var index = o.attr('id').indexOf('_') + 1;
            var id = o.attr('id').substr(index, o.attr('id').length - index);
            var name = $("span[id='span_" + id + "'] font").html();
            
            $("#modal-conform").on("click", function () {
                var params = {
                    Id: id,
                };
                var url = basePath + culture + '/Home/UpdateDepartmentValid';
                $.post(url, params, function (data) {
                    if (data.IsSuccess == true) {
                        $.magnificPopup.close();
                        $("span[id='span_" + id + "']").parent().remove();
                        var rethtml = ` <li class="parent_li" name="li_` + data.Code + `">
                                         <span title="展开子部门" id="span_`+ params.Id + `" data-id="` + params.Id + `" style="padding:0px 8px;"><font>` + name + `</font></span>
                                            <a class="btn bk-margin-5 btn-secondary-current  btn-primary" href="#departmentModal" id="btnsib_`+ params.Id + `"  style="display:none">New flat</a>
                                            <a class="btn bk-margin-5 btn-secondary-next  btn-success" href="#departmentModal" id="btnAdd_`+ params.Id + `" data-id="` + data.Code + `" style="display:none">New subordinate</a>
                                            <a class="btn bk-margin-5 btn-secondary-edit btn-info" href="#departmentModal"  id="btn_`+ params.Id + `"  data-id="` + data.Code + `" style="display:none">Edit</a>
                                                <a type="button" class="btn bk-margin-5 btn-secondary-setunvalid btn-warning"  id="btnUpdateValid_`+ params.Id + `" style="display:none">Disable</a>
                                        <ul style="float: none">
                                       </ul>
                                    </li>`;
                        
                        if (data.Code == $("span[id^=span_]:first").attr("data-id")) {
                            $("span[id='span_" + data.Code + "']").nextAll("ul").children("li:last").before(rethtml);
                        } else {
                            $("span[id='span_" + data.Code + "']").nextAll("ul").append(rethtml);
                        }
                        alert("Enable Department Success");
                    }
                    else
                    {
                        $.magnificPopup.close();
                        alert(data.Message);
                    }
                });
            });
        },
        //停用
        UpdateDepartUnValid: function () {
            var o = $(this);
            var index = o.attr('id').indexOf('_') + 1;
            var id = o.attr('id').substr(index, o.attr('id').length - index);
            var name = $("span[id='span_" + id + "'] font").html();
            
            $("#modal-conform").on("click", function () {
                var params = {
                    Id: id,
                };
                var url = basePath + culture + '/Home/UpdateDepartmentValid';
                $.post(url, params, function (data) {
                    if (data.IsSuccess == true) {
                        $.magnificPopup.close();
                        $("span[id='span_" + id + "']").parent().remove();
                        var rethtml = ` <li class="parent_li" name="li_` + $(".inValid:eq(0)").attr('data-id') + `">
                                           <span class="inValid" title="无效部门" id="span_` + id + `" data-id="` + id + `" data-isdmmanager="0"><font>` + name + `</font></span>
                                  <a class="btn btnnew btn-secondary-setvalid btn-primary " id="btnUpdateValid_`+ id + `" style="display:none">Enable</a>
                                 <ul style="float: none"></ul>
                                </li>`;
                        $(".inValid:eq(0)").nextAll('ul').append(rethtml);
                        alert("UnEnable Department Success");
                    }
                    else {
                        $.magnificPopup.close();
                        alert(data.Message);
                    }
                });
            });
        },
    };

    var page = {
        Init: function (fig) {
            config = fig;
            page.InitEvents();
        },

        InitEvents: function () {
            events.getDepartList();

            //根目录单击
            $("#tableWrapper ul:eq(0) li").on('click', 'span', function () {
                var o = $(this);
                var id = o.attr('id');
                var valueId = id.substr(5, id.length - 4);
                events.InputEvent(valueId);
            });

            //初始化新建一级部门弹框
            $('.btn-secondary-add').magnificPopup({
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
                        $("#departmenRest").trigger("click");
                    },
                    open: function () { 
                        var o = $(".btn-secondary-add");
                        var index = o.attr('id').indexOf('_') + 1;
                        var id = o.attr('id').substr(index, o.attr('id').length - index);
                        $.ajax({
                            type: 'post',
                            url: basePath + culture + '/Home/DepartmentCreate',
                            dataType: "json",
                            data: {
                                departmentId: id,
                                level:0
                            },
                            success: function (data) {
                                $("#ParentName").val(data.ParentName);
                                $("#ParentId").val(data.ParentId);
                            },
                        });
                    },
                    close: function () {
                    }

                }
            });
            //启用部门
            $('#tableWrapper').on('click', '.btn-secondary-setvalid', events.UpdateDepartValid);
            //停用部门
            $('#tableWrapper').on('click', '.btn-secondary-setunvalid', events.UpdateDepartUnValid);
            
        },
    }

    window.PageInit = page.Init;
    window.AddSuccess = events.AddSuccess;
    window.ClosePopWin = events.closePopWin;
    window.AddNewNodeFalse = events.AddNewNodeFalse;
    window.EditSuccess = events.EditSuccess;
})(window, jQuery)