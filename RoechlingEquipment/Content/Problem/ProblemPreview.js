(function (window, jQuery, undefined) {
    var ProblemPreview = {
        initialdata: function (data) {
            $("#problemnoPre").html(data.problem.PIProblemNo);
            $("#problemprocessPre").html(data.problem.PIProcess);//selected
            $("#machinesPre").html(data.problem.PIMachine);
            $("#toolsPre").html(data.problem.PITool);
            $("#sappnPre").html(data.problem.PISapPN);
            $("#workorderPre").html(data.problem.PIWorkOrder);
            $("#partnamePre").html(data.problem.PIProductName);
            $("#customerPre").html(data.problem.PICustomer);
            $("#problemdatePre").html(data.problem.PIProblemDateDesc);
            $("#problemsourcePre").html(data.problem.PIProblemSource);//selected
            $("#problemdefecttypePre").html(data.problem.PIDefectType);
            $("#problemdefectcodePre").html(data.problem.PIDefectCode); //selected
            $("#problemdefectqtyPre").html(data.problem.PIDefectQty);
            $("#problemshifttypePre").html(data.problem.PIShiftType);//selected
            $("#problemisrepeatPre").attr("checked", data.problem.PIIsRepeated == 1);
            $("#problemdescriptionPre").html(data.problem.PIProblemDesc);
            $("#problemseverityPre").html(data.problem.PISeverityDesc);//selected
            if (data.problem.PIPicture1Url) {
                $("#selectImg1Pre").attr("src", data.problem.PIPicture1Url)
            }
            if (data.problem.PIPicture2Url) {
                $("#selectImg2Pre").attr("src", data.problem.PIPicture2Url)
            }
            if (data.problem.PIPicture3Url) {
                $("#selectImg3Pre").attr("src", data.problem.PIPicture3Url)
            }
            if (data.problem.PIPicture4Url) {
                $("#selectImg4Pre").attr("src", data.problem.PIPicture4Url)
            }
            if (data.problem.PIPicture5Url) {
                $("#selectImg5Pre").attr("src", data.problem.PIPicture5Url)
            }
            if (data.problem.PIPicture6Url) {
                $("#selectImg6Pre").attr("src", data.problem.PIPicture6Url)
            }
            $("#problestatusPre").html(data.problem.PIStatusDesc);
            if (data.problem.PIActionPlan) {
                $("#problemactionplanPre").html(data.problem.PIActionPlan);
                $("#pvactionplan").removeClass("hidden");
            }
            $("#problemrootcausePre").val(data.problem.PIRootCause);
            ProblemPreview.initialSolvingTeam(data.solvingteam);
            ProblemPreview.initialQualityalert(data.qualityalert);
            ProblemPreview.initialSortingactivity(data.sortingactivity);
            ProblemPreview.initialActioncontainment(data.actioncontainment);
            ProblemPreview.initialActionFactoranalysis(data.actionfactoranalysis);
            ProblemPreview.initialActionwhyanalysisi1(data.actionwhyanalysisi);
            ProblemPreview.initialActionwhyanalysisi2(data.actionwhyanalysisi);
            ProblemPreview.initialActioncorrective(data.actioncorrective);
            ProblemPreview.initialActionpreventive(data.actionpreventive);
            ProblemPreview.initialLayeredaudit(data.layeredaudit);
            ProblemPreview.initialVerification(data.verification);
            ProblemPreview.initialStandardization(data.standardization);
            setTimeout(function () {
                //draw canvas
                var canvas = $('#pvcanvas')[0];
                if (!canvas) return false;
                var canheight = 400;
                var canwidth = $(".panel-body").width() - 77;
                canvas.height = canheight;
                canvas.width = canwidth;
                var context = canvas.getContext('2d');
                context.moveTo(canwidth * 0, canheight * 0.5);
                context.lineTo(canwidth * 0.8, canheight * 0.5);
                context.moveTo(canwidth * 0, canheight * 0.125);
                context.lineTo(canwidth * 0.3, canheight * 0.5);
                context.moveTo(canwidth * 0, canheight * 0.875);
                context.lineTo(canwidth * 0.3, canheight * 0.5);
                context.moveTo(canwidth * 0.4, canheight * 0.875);
                context.lineTo(canwidth * 0.7, canheight * 0.5);
                context.moveTo(canwidth * 0.4, canheight * 0.125);
                context.lineTo(canwidth * 0.7, canheight * 0.5);
                context.moveTo(canwidth * 0.8, canheight * 0.5);
                context.lineTo(canwidth * 0.78, canheight * 0.48);
                context.moveTo(canwidth * 0.8, canheight * 0.5);
                context.lineTo(canwidth * 0.78, canheight * 0.52);
                context.lineWidth = 1;
                context.strokeStyle = "#000";
                context.stroke();
                $("#pvaddA1").css("top", 0 - canheight * 0.8);
                $("#pvaddA2").css("top", 0 - canheight * 0.8).css("left", canwidth * 0.12 - 54);
                $("#pvaddA3").css("top", 0 - canheight * 0.7).css("left", canwidth * 0.08 - 108);
                $("#pvaddA4").css("top", 0 - canheight * 0.7).css("left", canwidth * 0.2 - 162);
                $("#pvaddB1").css("top", 0 - canheight * 0.8 - 24).css("left", canwidth * 0.4);
                $("#pvaddB2").css("top", 0 - canheight * 0.8 - 24).css("left", canwidth * 0.52 - 54);
                $("#pvaddB3").css("top", 0 - canheight * 0.7 - 24).css("left", canwidth * 0.48 - 108);
                $("#pvaddB4").css("top", 0 - canheight * 0.7 - 24).css("left", canwidth * 0.6 - 162);
                $("#pvaddC1").css("top", 0 - canheight * 0.48).css("left", canwidth * 0.08);
                $("#pvaddC2").css("top", 0 - canheight * 0.48).css("left", canwidth * 0.2 - 54);
                $("#pvaddC3").css("top", 0 - canheight * 0.38).css("left", 0 - 108);
                $("#pvaddC4").css("top", 0 - canheight * 0.38).css("left", canwidth * 0.12 - 162);
                $("#pvaddD1").css("top", 0 - canheight * 0.48 - 24).css("left", canwidth * 0.48);
                $("#pvaddD2").css("top", 0 - canheight * 0.48 - 24).css("left", canwidth * 0.6 - 54);
                $("#pvaddD3").css("top", 0 - canheight * 0.38 - 24).css("left", canwidth * 0.4 - 108);
                $("#pvaddD4").css("top", 0 - canheight * 0.38 - 24).css("left", canwidth * 0.52 - 162);
            }, 2000);
        },
        initialSolvingTeam: function (data) {
            var datahtml = "";
            $("#solvingteambodyPre").html("");
            if (data.length == 0) {
                datahtml += "<tr><td colspan=\"10\"  style=\"text-align:center\">  NoResult </td></tr>";
            }
            else {
                $("#pvsolvingteam").removeClass("hidden");
                $.each(data, function (index, item) {
                    var isleader = item.PSIsLeader == 1 ? "Yes" : "No";
                    datahtml += "<tr role=\"row\" class=\"odd\">" +
                        "<td class=\"membername\"><label class=\"control-label stuserinfo\" data-id=\"" + item.Id + "\" data-jobno=\"" + item.PSUserNo + "\">" + item.PSUserName + "</label></td >" +
                        "<td class=\"deskext\"><label class=\"control-label\">" + item.PSDeskEXT + "</label></td>" +
                        "<td class=\"department\"><label class=\"control-label\">" + item.PSDeptName + "</label></td>" +
                        "<td class=\"title\"><label class=\"control-label\">" + item.PSUserTitle + "</label></td>" +
                        "<td class=\"isleader\">" + isleader + "</td></tr > ";
                });
            }
            $("#solvingteambodyPre").append(datahtml);
        },
        initialQualityalert: function (data) {
            var datahtml = "";
            $("#qualityalertbodyPre").html("");
            if (data.length == 0) {
                datahtml += "<tr><td colspan=\"10\" style=\"text-align:center\">noResult</td></tr>";
            }
            else {
                $("#pvqualityalert").removeClass("hidden");
                $.each(data, function (index, item) {
                    datahtml += "<tr role=\"row\" class=\"odd\">" +
                        "<td class=\"what\"><label class=\"control-label\" data-id=\"" + data.Id + "\">" + item.PQWhat + "</label ></td >" +
                        "<td class=\"who\"><label class=\"control-label\" data-jobno=\"" + item.PQWhoNo + "\">" + item.PQWho + "</label ></td>" +
                        "<td class=\"plandate\">" + item.PQPlanDateDesc + "</td>" +
                        "<td class=\"actualdate\">" + item.PQActualDateDesc + "</td>" +
                        "<td class=\"attachment\"></td></tr>";
                });
            }
            $("#qualityalertbodyPre").append(datahtml);
        },
        initialSortingactivity: function (data) {
            var datahtml = "";
            $("#sortingactivitybodyPre").html("");
            if (data.length == 0) {
                datahtml += "<tr><td colspan=\"10\" style=\"text-align:center\">noResult</td></tr>";
            }
            else {
                $("#pvsortingactivity").removeClass("hidden");
                $.each(data, function (index, item) {
                    datahtml += "<tr class=\"gradeA odd\" role=\"row\">"
                        + "<td class=\"valuestream\" > <label class=\"control-label\" data-id=\"0\" data-no=\"1\">"
                        + item.PSAValueStream
                        + "</label></td>"
                        + "<td>" + item.PSADefectQty + "</td><td>" + item.PSASortedQty + "</td></tr >"

                });
            }
            $("#sortingactivitybodyPre").append(datahtml);

        },
        initialActioncontainment: function (data) {
            var datahtml = "";
            $("#containmentactionbodyPre").html("");
            if (data.length == 0) {
                datahtml += "<tr><td colspan=\"10\" style=\"text-align:center\">noResult</td></tr>";
            }
            else {
                $("#pvcontainmentaction").removeClass("hidden");
                $.each(data, function (index, item) {
                    datahtml += "<tr role=\"row\" class=\"odd\"><td class=\"what\">" +
                        "<label class=\"control-label\" data-id=\"" + item.Id + "\">" + item.PACWhat + "</label></td>" +
                        "<td class=\"troublespots\">" + item.PACCheck + "</td>" +
                        "<td class=\"who\"><label class=\"control-label\" data-jobno=\"" + item.PACWhoNo + "\">" + item.PACWho + "</label ></td>" +
                        "<td class=\"plandate\">" + item.PACPlanDateDesc + "</td>" +
                        "<td class=\"varificationdate\">" + item.PACVarificationDateDesc + "</td>" +
                        "<td class=\"where\">" + item.PACWhere + "</td>" +
                        "<td class=\"attachment\"></td>" +
                        "<td class=\"effeective\">" + item.PACEffeective + "</td>" +
                        "<td class=\"comment\">" + item.PACComment + "</td></tr>";
                });
            }
            $("#containmentactionbodyPre").append(datahtml);
        },
        initialActionFactoranalysis: function (data) {
            var datahtml = "";
            $("#pvfactoranalysisbody").html(datahtml);
            if (data.length == 0) {
                datahtml += "<tr><td colspan=\"6\" style=\"text-align:center\">noResult</td></tr>";
            }
            else {
                $("#pvfactoranalysis").removeClass("hidden");
                $.each(data, function (index, item) {
                    datahtml += "<tr role=\"row\" class=\"odd\">" +
                        "<td class=\"name\"><label class=\"control-label\" data-id=\"" + item.Id + "\">" + item.PAFType + "</label></td>" +
                        "<td class=\"possiblecause\">" + item.PAFPossibleCause + "</td>" +
                        "<td class=\"what\">" + item.PAFWhat + "</td>" +
                        "<td class=\"who\">" + item.PAFWho + "</td>" +
                        "<td class=\"when\">" + item.PAFValidatedDateDesc + "</td>" +
                        "<td class=\"result\">" + item.PAFPotentialCause + "</td></tr>";
                });
            }
            $("#pvfactoranalysisbody").append(datahtml);
        },
        drawPvCanvas: function () {
            //draw canvas
            var canvas = $('#pvcanvas')[0];
            if (!canvas) return false;
            var canheight = 400;
            var canwidth = $(".panel-body").width() - 77;
            canvas.clearRect(0, 0, canwidth, canheight);
            canvas.height = canheight;
            canvas.width = canwidth;
            var context = canvas.getContext('2d');
            context.moveTo(canwidth * 0, canheight * 0.5);
            context.lineTo(canwidth * 0.8, canheight * 0.5);
            context.moveTo(canwidth * 0, canheight * 0.125);
            context.lineTo(canwidth * 0.3, canheight * 0.5);
            context.moveTo(canwidth * 0, canheight * 0.875);
            context.lineTo(canwidth * 0.3, canheight * 0.5);
            context.moveTo(canwidth * 0.4, canheight * 0.875);
            context.lineTo(canwidth * 0.7, canheight * 0.5);
            context.moveTo(canwidth * 0.4, canheight * 0.125);
            context.lineTo(canwidth * 0.7, canheight * 0.5);
            context.moveTo(canwidth * 0.8, canheight * 0.5);
            context.lineTo(canwidth * 0.78, canheight * 0.48);
            context.moveTo(canwidth * 0.8, canheight * 0.5);
            context.lineTo(canwidth * 0.78, canheight * 0.52);
            context.lineWidth = 1;
            context.strokeStyle = "#000";
            context.stroke();
            $("#pvaddA1").css("top", 0 - canheight * 0.8);
            $("#pvaddA2").css("top", 0 - canheight * 0.8).css("left", canwidth * 0.12 - 54);
            $("#pvaddA3").css("top", 0 - canheight * 0.7).css("left", canwidth * 0.08 - 108);
            $("#pvaddA4").css("top", 0 - canheight * 0.7).css("left", canwidth * 0.2 - 162);
            $("#pvaddB1").css("top", 0 - canheight * 0.8 - 24).css("left", canwidth * 0.4);
            $("#pvaddB2").css("top", 0 - canheight * 0.8 - 24).css("left", canwidth * 0.52 - 54);
            $("#pvaddB3").css("top", 0 - canheight * 0.7 - 24).css("left", canwidth * 0.48 - 108);
            $("#pvaddB4").css("top", 0 - canheight * 0.7 - 24).css("left", canwidth * 0.6 - 162);
            $("#pvaddC1").css("top", 0 - canheight * 0.48).css("left", canwidth * 0.08);
            $("#pvaddC2").css("top", 0 - canheight * 0.48).css("left", canwidth * 0.2 - 54);
            $("#pvaddC3").css("top", 0 - canheight * 0.38).css("left", 0 - 108);
            $("#pvaddC4").css("top", 0 - canheight * 0.38).css("left", canwidth * 0.12 - 162);
            $("#pvaddD1").css("top", 0 - canheight * 0.48 - 24).css("left", canwidth * 0.48);
            $("#pvaddD2").css("top", 0 - canheight * 0.48 - 24).css("left", canwidth * 0.6 - 54);
            $("#pvaddD3").css("top", 0 - canheight * 0.38 - 24).css("left", canwidth * 0.4 - 108);
            $("#pvaddD4").css("top", 0 - canheight * 0.38 - 24).css("left", canwidth * 0.52 - 162);
        },
        initialActionwhyanalysisi1: function (data) {
            var _self = this;
            var datahtml = "";
            var q1 = "1) Why did it happen?";
            $("#whyanalysisbody1Pre").html(datahtml);
            if (data.length == 0) {
                datahtml += "<tr><td colspan=\"10\" style=\"text-align:center\">noResult</td></tr>";
            }
            else {
                $("#pvwhyanalysis").removeClass("hidden");
                $.each(data, function (index, item) {
                    if (item.PAWWhyForm == q1) {
                        datahtml += "<tr role=\"row\" class=\"odd\">" +
                            "<td class=\"whyform\"><label class=\"control-label\" data-id=\"" + item.Id + "\">" + item.PAWWhyForm + "</label></td>" +
                            "<td class=\"questionchain\">" + item.PAWWhyQuestionChain + "</td>" +
                            "<td class=\"why1\">" + item.PAWWhy1 + "</td>" +
                            "<td class=\"why2\">" + item.PAWWhy2 + "</td>" +
                            "<td class=\"why3\">" + item.PAWWhy3 + "</td>" +
                            "<td class=\"why4\">" + item.PAWWhy4 + "</td>" +
                            "<td class=\"why5\">" + item.PAWWhy5 + "</td></tr>";
                    }
                });
            }
            $("#whyanalysisbody1Pre").append(datahtml);
        },
        initialActionwhyanalysisi2: function (data) {
            var _self = this;
            var datahtml = "";
            var q2 = "2) Why wasn't it detected?";
            $("#whyanalysisbody2Pre").html(datahtml);
            if (data.length == 0) {
                datahtml += "<tr><td colspan=\"10\" style=\"text-align:center\">noResult</td></tr>";
            }
            else {
                $.each(data, function (index, item) {
                    if (item.PAWWhyForm == q2) {
                        datahtml += "<tr role=\"row\" class=\"odd\">" +
                            "<td class=\"whyform\"><label class=\"control-label\" data-id=\"" + item.Id + "\">" + item.PAWWhyForm + "</label></td>" +
                            "<td class=\"questionchain\">" + item.PAWWhyQuestionChain + "</td>" +
                            "<td class=\"why1\">" + item.PAWWhy1 + "</td>" +
                            "<td class=\"why2\">" + item.PAWWhy2 + "</td>" +
                            "<td class=\"why3\">" + item.PAWWhy3 + "</td>" +
                            "<td class=\"why4\">" + item.PAWWhy4 + "</td>" +
                            "<td class=\"why5\">" + item.PAWWhy5 + "</td></tr>";
                    }
                });
            }
            $("#whyanalysisbody2Pre").append(datahtml);
        },
        initialActioncorrective: function (data) {
            var datahtml = "";
            $("#correctivecctionbodyPre").html(datahtml);
            if (data.length == 0) {
                datahtml += "<tr><td colspan=\"10\" style=\"text-align:center\">noResult</td></tr>";
            }
            else {
                $("#pvcorrectiveaction").removeClass("hidden");
                $.each(data, function (index, item) {
                    datahtml += "<tr role=\"row\" class=\"odd\">" +
                        "<td class=\"what\"><label class=\"control-label\" data-id=\"" + item.Id + "\">" + item.PACWhat + "</label></td>" +
                        "<td class=\"who\"><label class=\"control-label\" data-jobno=\"" + item.PACWhoNo + "\">" + item.PACWho + "</label ></td>" +
                        "<td class=\"plandate\">" + item.PACPlanDateDesc + "</td>" +
                        "<td class=\"actualdate\">" + item.PACActualDateDesc + "</td>" +
                        "<td class=\"where\">" + item.PACWhere + "</td>" +
                        "<td class=\"attachment\"></td>" +
                        "<td class=\"status\"></td>" +
                        "<td class=\"comment\">" + item.PACComment + "</td></tr>";

                });
            }
            $("#correctivecctionbodyPre").append(datahtml);
        },
        initialActionpreventive: function (data) {
            var datahtml = "";
            $("#preventivemeasuresbodyPre").html(datahtml);
            if (data.length == 0) {
                datahtml += "<tr><td colspan=\"10\" style=\"text-align:center\">noResult</td></tr>";
            }
            else {
                $("#pvpreventivemeasures").removeClass("hidden");
                $.each(data, function (index, item) {
                    datahtml += "<tr role=\"row\" class=\"odd\">" +
                        "<td class=\"what\"><label class=\"control-label\" data-id=\"" + item.Id + "\">" + item.PAPWhat + "</label></td>" +
                        "<td class=\"who\"><label class=\"control-label\" data-jobno=\"" + item.PAPWhoNo + "\">" + item.PAPWho + "</label ></td>" +
                        "<td class=\"plandate\">" + item.PAPPlanDateDesc + "</td>" +
                        "<td class=\"actualdate\">" + item.PAPActualDateDesc + "</td>" +
                        "<td class=\"where\">" + item.PAPWhere + "</td>" +
                        "<td class=\"attachment\"></td>" +
                        "<td class=\"status\"></td>" +
                        "<td class=\"comment\">" + item.PAPComment + "</td>" +
                        "<td class=\"actions\"><a href=\"#\" class=\"hidden on-editing save-row\"><i class=\"fa fa-save\"></i></a>" +
                        " <a href=\"#\" class=\"hidden on-editing cancel-row\"><i class=\"fa fa-times\"></i></a>" +
                        " <a href=\"#\" class=\"on-default edit-row\"><i class=\"fa fa-pencil\"></i></a>" +
                        " <a href=\"#\" class=\"on-default remove-row\"><i class=\"fa fa-trash-o\"></i></a></td></tr>";;

                });
            }
            $("#preventivemeasuresbodyPre").append(datahtml);
        },
        initialLayeredaudit: function (data) {
            var datahtml = "";
            $("#layeredauditbodyPre").html(datahtml);
            if (data.length == 0) {
                datahtml += "<tr><td colspan=\"10\" style=\"text-align:center\">noResult</td></tr>";
            }
            else {
                $("#pvlayeredaudit").removeClass("hidden");
                $.each(data, function (index, item) {
                    datahtml += "<tr role=\"row\" class=\"odd\">" +
                        "<td class=\"what\"><label class=\"control-label\" data-id=\"" + item.Id + "\">" + item.PLWhat + "</label></td>" +
                        "<td class=\"who\"><label class=\"control-label\" data-jobno=\"" + item.PLWhoNo + "\">" + item.PLWho + "</label ></td>" +
                        "<td class=\"plandate\">" + item.PLPlanDateDesc + "</td>" +
                        "<td class=\"actualdate\">" + item.PLActualDateDesc + "</td>" +
                        "<td class=\"where\">" + item.PLWhere + "</td>" +
                        "<td class=\"attachment\"></td>" +
                        "<td class=\"status\"></td>" +
                        "<td class=\"comment\">" + item.PLComment + "</td></tr>";

                });
            }
            $("#layeredauditbodyPre").append(datahtml);
        },
        initialVerification: function (data) {
            var datahtml = "";
            $("#verificationbodyPre").html(datahtml);
            if (data.length == 0) {
                datahtml += "<tr><td colspan=\"10\" style=\"text-align:center\">noResult</td></tr>";
            }
            else {
                $("#pvverification").removeClass("hidden");
                $.each(data, function (index, item) {
                    datahtml += "<tr role=\"row\" class=\"odd\">" +
                        "<td class=\"what\"><label class=\"control-label\" data-id=\"" + item.Id + "\">" + item.PVWhat + "</label></td>" +
                        "<td class=\"who\"><label class=\"control-label\" data-jobno=\"" + item.PVWhoNo + "\">" + item.PVWho + "</label ></td>" +
                        "<td class=\"plandate\">" + item.PVPlanDateDesc + "</td>" +
                        "<td class=\"actualdate\">" + item.PVActualDateDesc + "</td>" +
                        "<td class=\"where\">" + item.PVWhere + "</td>" +
                        "<td class=\"attachment\"></td>" +
                        "<td class=\"status\"></td>" +
                        "<td class=\"comment\">" + item.PVComment + "</td> </tr>";
                });
            }
            $("#verificationbodyPre").append(datahtml);
        },
        initialStandardization: function (data) {
            var datahtml = "";
            $("#standardizationbodyPre").html(datahtml);
            if (data.length == 0) {
                datahtml += "<tr><td colspan=\"10\"  style=\"text-align:center\">noResult</td></tr>";
            }
            else {
                $("#pvstandardization").removeClass("hidden");
                $.each(data, function (index, item) {
                    var needupdate = item.PSNeedUpdate == 1 ? "Yes" : "No";
                    datahtml += "<tr class=\"gradeA odd\" role=\"row\">" +
                        "<td class=\"itemname\" > <label class=\"control-label\" data-id=\"0\" data-no=\"1\">D/P-FMEA</label></td >" +
                        "<td class=\"needupdate\">" + needupdate + "</td>" +
                        "<td>" + item.PSWho + "</td>" +
                        "<td class=\"plandate\" >" + item.PSPlanDateDesc + "</td > " +
                        "<td class=\"actualdate\" >" + item.PSActualDateDesc + "</td > " +
                        "<td >" + item.PSDocNo + "</td > " +
                        "<td >" + item.PSChangeContent + "</td > " +
                        "<td >" + item.PSOldVersion + "</td > " +
                        "<td >" + item.PSNewVersion + "</td >" +
                        "<td >" + item.PSAttachment + "</td ></tr >";
                });
            }
            $("#standardizationbodyPre").append(datahtml);
        },
        getEmuneById: function (type, key, isEnum) {
            var parms = {
                Type: type,
                Key: key,
                isEnum: isEnum
            }
            $.ajax({
                type: "GET",
                url: "GetEmuneById",
                async: false,
                data: JSON.stringify(params),
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (data) {
                    return data;
                }
            });
        }

    }
    window.InitProblemPreviewData = ProblemPreview.initialdata;
    window.DrawPvCanvas = ProblemPreview.drawPvCanvas;
})(window, jQuery)