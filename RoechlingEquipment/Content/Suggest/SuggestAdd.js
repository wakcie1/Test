;
(function (window, jQuery, undefined) {
    var config = {};

    var page = {

        config: {},

        imgfile: {},

        Init: function () {
            page.InitControl();
            page.InitEvents();
            page.ResetImg();
            page.userAutoComplete();
        },

        InitControl: function () {
            // validation summary
            var $summaryForm = $("#suggestAdd-form");
            $summaryForm.validate({
                errorPlacement: function (error, element) {
                    $(element).parent().after(error);
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

            //Require Type
            var requireTypeUrl = $("#requireTypeUrl").val() || '';
            if (requireTypeUrl != '') {
                $.getJSON(requireTypeUrl, null, function (myData) {
                    if (myData != null) {
                        var requireType = $("#requireType");
                        requireType.html("");
                        for (var i = 0; i < myData.length; i++) {
                            var item = "<option value='" + myData[i].Value + "'>" + myData[i].Text + "</option>";
                            requireType.append(item);
                        }
                    }
                });
            }
            //Phrase
            var phraseUrl = $("#phraseUrl").val() || '';
            if (phraseUrl != '') {
                $.getJSON(phraseUrl, null, function (myData) {
                    if (myData != null) {
                        var requireType = $("#phrase");
                        requireType.html("");
                        for (var i = 0; i < myData.length; i++) {
                            var item = "<option value='" + myData[i].Value + "'>" + myData[i].Text + "</option>";
                            requireType.append(item);
                        }
                    }
                });
            }
        },

        InitEvents: function () {
            $("#addSuggestBtn").on('click', function () {
                if (!($("#suggestAdd-form").valid())) return;
                var params = {
                    BFType: $("#requireType").val(),
                    BFPhase: $("#phrase").val(),
                    BFDesc: $("#description").val(),
                    BFRespUserNo: $("assignToNo").val(),
                    BFRespUser: $("assignTo").val(),
                    BFPictureUrl: $("#uploadPicture").prop("src"),
                    BFPicture: $("#uploadPicture").prop("alt")
                    //FeedBackComment: $().val(),
                    //IsValid:$().val()
                };
                $.ajax({
                    type: "Post",
                    url: "SaveSuggest",
                    async: false,
                    data: JSON.stringify(params),
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.IsSuccess) {
                            alert("新增成功" + data.model.Id);
                            $.magnificPopup.close();
                        }
                    }
                });
                return false; 
            });

            $("#cancelSuggestAddBtn").on('click', function () {
                $.magnificPopup.close();
            });

            $("#imgClose").on('click', function () {
                page.ResetImg();
            });
        },

        InitImgUpload: function () {
            $("#imgUpload").off('change');
            $("#imgUpload").on('change', function () {
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
                    $(".selectImg").children().each(function (index, item) {
                        $(item).addClass('none');
                    });
                    $(".loading").removeClass('none');
                    $.ajaxFileUpload({
                        url: url,
                        secureuri: false,
                        fileElementId: "imgUpload",
                        //dataType: 'json',
                        data: { 'type': 1 },
                        success: function (result) {
                            var data = JSON.parse(result.body.innerHTML);
                            if (data.data && data.data.length > 0) {
                                $(".selectImg").append('<img src="' + data.data[0].ImgUrl + '" class="previewImg"/>');
                                $(".imgClose").removeClass('none');
                                $(".loading").addClass('none');
                                $("#uploadPicture").val(data.data[0].ImgLocalPath);
                            }
                        },
                        error: function (data) {
                        }
                    });
                } else {
                    $(".selectImg").append('<div class="img" style="filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale,src=\'' + file.value + '\'"></div>');
                }
            });
        },

        ResetImg: function () {
            $(".previewImg").remove();
            $(".selectImg").children().each(function (index, item) {
                $(item).removeClass('none');
            });
            $(".imgClose").addClass('none');
            $(".loading").addClass('none');
            $("#imgUpload").val("");
            $("#uploadPicture").val("");
            page.InitImgUpload();
        },

        userAutoComplete: function () {
            var userlisturl = $("#assignToUrl").val() || '';
            if (userlisturl != '') {
                var options = {
                    minLength: 0,
                    source: function (request, response) {
                        $.ajax({
                            url: userlisturl,
                            type: "post",
                            dataType: "json",
                            data: { "key": $("#assignTo").val() },
                            success: function (data) {
                                response($.map(data, function (item) {
                                    return {
                                        label: item.BUName + "[" + item.BUEnglishName + "](" + item.BUJobNumber + ")",
                                        value: item.BUJobNumber
                                    }
                                }));
                            }
                        });
                    },
                    select: function (event, ui) {
                        $("#assignTo").val(ui.item.label);
                        $("#assignToNo").html(ui.item.value);
                        return false;
                    },
                };
                $("#assignTo").autocomplete(options);
            }
        },
    }
    window.SuggestAddPageInit = page.Init;
})(window, jQuery)



