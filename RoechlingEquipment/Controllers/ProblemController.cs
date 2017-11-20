using Business;
using Common;
using Common.Costant;
using Common.Enum;
using Model.CommonModel;
using Model.Material;
using Model.Problem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RoechlingEquipment.Controllers
{
    [ServerAuthorize]
    public class ProblemController : BaseController
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult ProblemIndex(long proId = 0)
        {
            ViewBag.ProId = proId;
            return View();
        }

        public ActionResult ProblemSearchIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProblemSearchResult(ProblemSearchModel searchModel)
        {
            var totalCount = 0;
            var result = ProblemBusiness.ProblemSearchResult(searchModel, out totalCount);
            var page = new Page(totalCount, searchModel.CurrentPage);

            var model = new ProblemSearchResultModel
            {
                Models = result,
                Page = page
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult GetMyToDoProblemResult()
        {
            var result = ProblemBusiness.GetMyToDoProblemList(this.LoginUser);
            return View(result);
        }

        /// <summary>
        /// 新增问题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveNewProblem(ProblemInfoModel model)
        {
            var result = ProblemBusiness.SaveNewProblem(model, this.LoginUser);
            return Json(result);
        }

        /// <summary>
        /// 新增问题处理人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveSolvingTeam(ProblemSolvingTeamModel model)
        {
            var result = ProblemBusiness.SaveSolvingTeam(model, this.LoginUser);
            return Json(result);
        }

        /// <summary>
        /// 新增问题质量报警
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveQualityAlert(ProblemQualityAlertModel model)
        {
            var result = ProblemBusiness.SaveQualityAlert(model, this.LoginUser);
            return Json(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveSortingActivity(ProblemSortingActivityModel model)
        {
            var result = ProblemBusiness.SaveSortingActivity(model, this.LoginUser);
            return Json(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveFactorAnalysis(ProblemActionFactorAnalysisModel model)
        {
            var result = ProblemBusiness.SaveFactorAnalysis(model, this.LoginUser);
            return Json(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveActionContainment(ProblemActionContainmentModel model)
        {
            var result = ProblemBusiness.SaveActionContainment(model, this.LoginUser);
            return Json(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveActionWhyanalysis(ProblemActionWhyanalysisModel model)
        {
            var result = ProblemBusiness.SaveActionWhyanalysis(model, this.LoginUser);
            return Json(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveActionCorrective(ProblemActionCorrectiveModel model)
        {
            var result = ProblemBusiness.SaveActionCorrective(model, this.LoginUser);
            return Json(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveActionPreventive(ProblemActionPreventiveModel model)
        {
            var result = ProblemBusiness.SaveActionPreventive(model, this.LoginUser);
            return Json(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveStandLayeredAudit(ProblemLayeredAuditModel model)
        {
            var result = ProblemBusiness.SaveStandLayeredAudit(model, this.LoginUser);
            return Json(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveStandVerification(ProblemVerificationModel model)
        {
            var result = ProblemBusiness.SaveStandVerification(model, this.LoginUser);
            return Json(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveStandardization(ProblemStandardizationModel model)
        {
            var result = ProblemBusiness.SaveStandardization(model, this.LoginUser);
            return Json(result);
        }

        /// <summary>
        /// 更新问题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateProblemStatus(ProblemInfoModel model)
        {
            var result = ProblemBusiness.UpdateProblemStatus(model, this.LoginUser);
            return Json(result);
        }

        [HttpPost]
        public ActionResult InvalidActionCorrective(long id)
        {
            var result = ProblemBusiness.InvalidActionCorrective(id, this.LoginUser);
            return Json(result);
        }

        [HttpPost]
        public ActionResult InvalidActionContainment(long id)
        {
            var result = ProblemBusiness.InvalidActionContainment(id, this.LoginUser);
            return Json(result);
        }

        [HttpPost]
        public ActionResult InvalidFactorAnalysis(long id)
        {
            var result = ProblemBusiness.InvalidFactorAnalysis(id, this.LoginUser);
            return Json(result);
        }

        [HttpPost]
        public ActionResult InvalidActionPreventive(long id)
        {
            var result = ProblemBusiness.InvalidActionPreventive(id, this.LoginUser);
            return Json(result);
        }

        [HttpPost]
        public ActionResult InvalidActionWhyanalysis(long id)
        {
            var result = ProblemBusiness.InvalidActionWhyanalysis(id, this.LoginUser);
            return Json(result);
        }

        [HttpPost]
        public ActionResult InvalidStandLayeredAudit(long id)
        {
            var result = ProblemBusiness.InvalidStandLayeredAudit(id, this.LoginUser);
            return Json(result);
        }

        [HttpPost]
        public ActionResult InvalidQualityAlert(long id)
        {
            var result = ProblemBusiness.InvalidQualityAlert(id, this.LoginUser);
            return Json(result);
        }

        [HttpPost]
        public ActionResult InvalidSolvingTeam(long id)
        {
            var result = ProblemBusiness.InvalidSolvingTeam(id, this.LoginUser);
            return Json(result);
        }

        [HttpPost]
        public ActionResult InvalidStandVerification(long id)
        {
            var result = ProblemBusiness.InvalidStandVerification(id, this.LoginUser);
            return Json(result);
        }

        public ActionResult GetProblemUnionInfo(long proId)
        {
            var result = ProblemBusiness.GetProblemUnionInfo(proId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取新增问题编号
        /// </summary>
        /// <returns></returns>
        public ActionResult GenerateProblemNo()
        {
            var result = ProblemBusiness.GenerateProblemNo();
            return Content(result);
        }

        //[HttpGet]
        //public ActionResult GetEmuneById(string Name, string Key, bool isEnum=false)
        //{
        //    var data = string.Empty;
        //    if (isEnum) {
        //        EnumHelper.GetDescriptionByValue<Name>
        //    }
        //}
        #region 控件数据源

        /// <summary>
        /// 获取物料信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ActionResult SapAutoComplete(string key, string workorder)
        {
            var userlist = MaterialBusiness.SapAutoComplete(key, workorder);
            return Json(userlist, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取物料信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ActionResult WorkOrderAutoComplete(string key)
        {
            var userlist = MaterialBusiness.WorkOrderAutoComplete(key);
            return Json(userlist, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// ToolList
        /// </summary>
        /// <returns></returns>
        public ActionResult GetToolList(string key)
        {

            var toollist = MaterialBusiness.GetToolList(key);
            return Json(toollist, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// MachineList
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMachineList(string key)
        {
            var toollist = MaterialBusiness.GetMachineList(key);
            return Json(toollist, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// ProcessList
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProcessList(bool isNeedDefalut)
        {
            var list = CommonBusiness.GetProcessList();
            var processList = list.Select(i => new SelectListItem
            {
                Text = i.BCCodeDesc,
                Value = i.BCCode
            });
            var result = new List<SelectListItem>();
            if (isNeedDefalut)
            {
                var value1 = new SelectListItem() { Text = "Please Select", Value = "-1", Selected = true };
                result.Add(value1);
            }
            if (processList.Count() > 0)
            {
                result.AddRange(processList);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// ProcessList
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSourceList(bool isNeedDefalut)
        {
            var list = CommonBusiness.GetSourceList();
            var sourcelist = list.Select(i => new SelectListItem
            {
                Text = i.BCCodeDesc,
                Value = i.BCCode
            });
            var result = new List<SelectListItem>();
            if (isNeedDefalut)
            {
                var value1 = new SelectListItem() { Text = "Please Select", Value = "-1", Selected = true };
                result.Add(value1);
            }
            if (sourcelist.Count() > 0)
            {
                result.AddRange(sourcelist);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// ProcessList
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDefectCodeTypeList(bool isNeedDefalut)
        {
            var list = CodeBusiness.GetDefectCodeTypeList();
            var sourcelist = list.Select(i => new SelectListItem
            {
                Text = i.BDCodeType,
                Value = i.BDCodeType
            });
            var result = new List<SelectListItem>();
            if (isNeedDefalut)
            {
                var value1 = new SelectListItem() { Text = "Please Select", Value = "-1", Selected = true };
                result.Add(value1);
            }
            if (sourcelist.Count() > 0)
            {
                result.AddRange(sourcelist);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// ProcessList
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDefectCodeList(string type, bool isNeedDefalut)
        {
            var list = CodeBusiness.GetDefectCodeByType(type);
            var sourcelist = list.Select(i => new SelectListItem
            {
                Text = i.BDCodeNameEn,
                Value = i.BDCode
            });
            var result = new List<SelectListItem>();
            if (isNeedDefalut)
            {
                var value1 = new SelectListItem() { Text = "Please Select", Value = "-1", Selected = true };
                result.Add(value1);
            }
            if (sourcelist.Count() > 0)
            {
                result.AddRange(sourcelist);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// SeverityList
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSeverityList()
        {
            var severitylist = EnumHelper.SelectListEnum<ProblemSeverityEnum>(null, false);
            return Json(severitylist, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// SeverityList
        /// </summary>
        /// <returns></returns>
        public ActionResult GetStatusList()
        {
            var severitylist = EnumHelper.SelectListEnum<ProblemStatusEnum>(null, false);
            return Json(severitylist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProblemAotoComplete(string key)
        {
            var problemList = ProblemBusiness.ProblemNoAuto(key);
            return Json(problemList, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region 导出
        public FileResult ProblemExcel(ProblemSearchModel searchModel)
        {
            searchModel.PageSize = 1000;
            var totalCount = 0;
            var result = ProblemBusiness.ProblemSearchResult(searchModel, out totalCount).ToList();

            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");

            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("ProblemNo");
            row1.CreateCell(1).SetCellValue("IssueFoundOwner");
            row1.CreateCell(2).SetCellValue("Date");
            row1.CreateCell(3).SetCellValue("Shift");
            row1.CreateCell(4).SetCellValue("Repeated");
            row1.CreateCell(5).SetCellValue("Status");
            row1.CreateCell(6).SetCellValue("Process");
            row1.CreateCell(7).SetCellValue("WorkOrder");
            row1.CreateCell(8).SetCellValue("Tooling");
            row1.CreateCell(9).SetCellValue("Machine");
            row1.CreateCell(10).SetCellValue("Severity");
            row1.CreateCell(11).SetCellValue("Sap#");
            row1.CreateCell(12).SetCellValue("Part Name");
            row1.CreateCell(13).SetCellValue("Customer");
            row1.CreateCell(14).SetCellValue("Next Report Date");
            row1.CreateCell(15).SetCellValue("Source");
            row1.CreateCell(16).SetCellValue("SourceDefect Type");
            row1.CreateCell(17).SetCellValue("Root Causel");

            for (int i = 0; i < result.Count(); i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(result[i].PIProblemNo);
                rowtemp.CreateCell(1).SetCellValue(result[i].PICreateUserName);
                rowtemp.CreateCell(2).SetCellValue(result[i].PIProblemDateDesc);
                rowtemp.CreateCell(3).SetCellValue(result[i].PIShiftType);
                rowtemp.CreateCell(4).SetCellValue(EnumHelper.GetDescriptionByValue<YesOrNoEnum>((int)(result[i].PIIsRepeated)));
                rowtemp.CreateCell(5).SetCellValue(EnumHelper.GetDescriptionByValue<ProblemStatusEnum>((int)result[i].PIStatus));
                rowtemp.CreateCell(6).SetCellValue(result[i].PIProcess);
                rowtemp.CreateCell(7).SetCellValue(result[i].PIWorkOrder);
                rowtemp.CreateCell(8).SetCellValue(result[i].PITool);
                rowtemp.CreateCell(9).SetCellValue(result[i].PIMachine);
                rowtemp.CreateCell(10).SetCellValue(EnumHelper.GetDescriptionByValue<ProblemSeverityEnum>((int)result[i].PISeverity));
                rowtemp.CreateCell(11).SetCellValue(result[i].PICustomerPN);
                rowtemp.CreateCell(12).SetCellValue(result[i].PIProductName);
                rowtemp.CreateCell(13).SetCellValue(result[i].PICustomer);
                rowtemp.CreateCell(14).SetCellValue(result[i].PINextProblemDateDesc);
                rowtemp.CreateCell(15).SetCellValue(result[i].PIProblemSource);
                rowtemp.CreateCell(16).SetCellValue(result[i].PIDefectType);
                rowtemp.CreateCell(17).SetCellValue(result[i].PIRootCause);
                rowtemp.CreateCell(18).SetCellValue(result[i].PIRootCause);
            }

            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            var exportFileName = string.Format("{0}{1}.xls", "ProblemInfo", DateTime.Now.ToString("yyyyMMddHHmmss"));
            return File(ms, "application/vnd.ms-excel", exportFileName);
        }

        #endregion

        #region 控制ProblemSearch数据源 

        /// <summary>
        /// ProcessList
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProblemSeverityList(bool isNeedDefalut)
        {
            var result = EnumHelper.SelectListEnum<ProblemSeverityEnum>(null, isNeedDefalut);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProblemStatusList(bool isNeedDefalut)
        {
            var sourcelist = EnumHelper.SelectListEnum<ProblemStatusEnum>(null, isNeedDefalut);

            return Json(sourcelist, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// RepeatableList
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRepeatableList(bool isNeedDefalut)
        {
            var sourcelist = EnumHelper.SelectListEnum<YesOrNoEnum>(null, isNeedDefalut);
            return Json(sourcelist, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 导出EXCEL
        public FileResult PreviewExport(long proId)
        {
            var helper = new ExcelHelper();
            var data = ProblemBusiness.GetProblemUnionInfo(proId);
            var rowInt = 3;

            //helper.Open("../ReportTemplate/ProblemPreviewReort.xlsx");
            //helper.GetSheet("8D report Format");
            //helper.Create();
            helper.Open(Request.MapPath("/ReportTemplate/ProblemPreviewReport.xlsx"));
            //helper.AddSheet("8D report Format");
            helper.ws = helper.GetSheet("8D report Format");

            //todo 添加表头图片
            rowInt += 4;

            //problem基础信息
            if (data.data.problem != null)
            {
                helper.SetCellValue(helper.ws, rowInt, 6, data.data.problem.PIProblemNo); //编号

                helper.SetCellValue(helper.ws, rowInt, 19, data.data.problem.PICreateTime); //创建日期

                helper.SetCellValue(helper.ws, rowInt + 1, 6, data.data.problem.PIProductName); //零件名称

                helper.SetCellValue(helper.ws, rowInt + 1, 19, data.data.problem.PICustomerPN); //零件号

                helper.SetCellValue(helper.ws, rowInt + 2, 6, data.data.problem.PIProblemSource); //问题来源

                helper.SetCellValue(helper.ws, rowInt + 2, 19, data.data.problem.PIMachine + " " + data.data.problem.PITool); //设备&模具编号

                helper.SetCellValue(helper.ws, rowInt + 3, 6, data.data.problem.PICustomer); //客户名称

                helper.SetCellValue(helper.ws, rowInt + 3, 19, data.data.problem.PICreateUserName + "(" + data.data.problem.PICreateUserNo + ")"); //创建人
                rowInt += 4;
            }

            //问题解决小组
            rowInt += 2;

            if (data.data.solvingteam != null && data.data.solvingteam.Count > 0)
            {
                var leader = data.data.solvingteam.Where(p => p.PSIsLeader == EnabledEnum.Enabled.GetHashCode()).FirstOrDefault(); //组长信息
                if (leader != null)
                {
                    helper.SetCellValue(helper.ws, rowInt, 5, leader.PSUserName + "(" + leader.PSUserNo + ")");

                    helper.SetCellValue(helper.ws, rowInt, 10, leader.PSDeptName);

                    helper.SetCellValue(helper.ws, rowInt, 16, leader.PSDeskEXT);

                    helper.SetCellValue(helper.ws, rowInt, 22, leader.PSUserTitle);
                }
                rowInt += 1;

                var member = data.data.solvingteam.Where(p => p.PSIsLeader == EnabledEnum.UnEnabled.GetHashCode()).ToList(); //组员信息
                if (member != null && member.Count > 0)
                {
                    if (member.Count <= 3)
                    {
                        for (int i = 0; i < member.Count; i++)
                        {
                            helper.SetCellValue(helper.ws, rowInt + i, 5, member[i].PSUserName + "(" + member[i].PSUserNo + ")");
                            helper.SetCellValue(helper.ws, rowInt, 10, member[i].PSDeptName);
                            helper.SetCellValue(helper.ws, rowInt + i, 16, member[i].PSDeskEXT);
                            helper.SetCellValue(helper.ws, rowInt + i, 22, member[i].PSUserTitle);
                        }
                        rowInt += 3;
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            helper.SetCellValue(helper.ws, rowInt, 5, member[i].PSUserName + "(" + member[i].PSUserNo + ")");
                            helper.SetCellValue(helper.ws, rowInt, 10, member[i].PSDeptName);
                            helper.SetCellValue(helper.ws, rowInt, 16, member[i].PSDeskEXT);
                            helper.SetCellValue(helper.ws, rowInt, 22, member[i].PSUserTitle);
                            rowInt += 1;
                        }
                        for (int i = 3; i < member.Count; i++)
                        {
                            helper.AddRow(helper.ws, rowInt);
                            helper.UniteCells(helper.ws, rowInt, 2, rowInt, 4);
                            helper.SetCellValue(helper.ws, rowInt, 2, "Team Member/小组成员");
                            helper.SetCellValue(helper.ws, rowInt, 5, member[i].PSUserName + "(" + member[i].PSUserNo + ")");
                            helper.UniteCells(helper.ws, rowInt, 8, rowInt, 9);
                            helper.SetCellValue(helper.ws, rowInt, 8, " Department/部门");
                            helper.UniteCells(helper.ws, rowInt, 10, rowInt, 13);
                            helper.UniteCells(helper.ws, rowInt, 14, rowInt, 15);
                            helper.SetCellValue(helper.ws, rowInt, 14, "Phone/电话");
                            helper.UniteCells(helper.ws, rowInt, 16, rowInt, 18);
                            helper.SetCellValue(helper.ws, rowInt, 16, member[i].PSDeskEXT);
                            helper.UniteCells(helper.ws, rowInt, 19, rowInt, 21);
                            helper.SetCellValue(helper.ws, rowInt, 19, "Function&Department/职务");
                            helper.UniteCells(helper.ws, rowInt, 22, rowInt, 26);
                            helper.SetCellValue(helper.ws, rowInt, 22, member[i].PSUserTitle);
                            rowInt += 1;
                        }
                    }
                }
            }
            else
            {
                rowInt += 4;
            }
            rowInt += 1;

            //问题描述
            rowInt += 2;

            //失效项目视化
            //helper.UniteCells(helper.ws, rowInt, 2, rowInt + 19, 26);
            if (!string.IsNullOrEmpty(data.data.problem.PIPicture1))
            {
                helper.InsertPictures(Request.MapPath(data.data.problem.PIPicture1), "8D report Format", 180, rowInt * 16, 140, 140);
            }
            if (!string.IsNullOrEmpty(data.data.problem.PIPicture2))
            {
                helper.InsertPictures(Request.MapPath(data.data.problem.PIPicture2), "8D report Format", 330, rowInt * 16, 140, 140);
            }
            if (!string.IsNullOrEmpty(data.data.problem.PIPicture3))
            {
                helper.InsertPictures(Request.MapPath(data.data.problem.PIPicture3), "8D report Format", 480, rowInt * 16, 140, 140);
            }
            if (!string.IsNullOrEmpty(data.data.problem.PIPicture4))
            {
                helper.InsertPictures(Request.MapPath(data.data.problem.PIPicture4), "8D report Format", 650, rowInt * 16, 140, 140);
            }
            if (!string.IsNullOrEmpty(data.data.problem.PIPicture5))
            {
                helper.InsertPictures(Request.MapPath(data.data.problem.PIPicture5), "8D report Format", 810, rowInt * 16, 140, 140);
            }
            if (!string.IsNullOrEmpty(data.data.problem.PIPicture6))
            {
                helper.InsertPictures(Request.MapPath(data.data.problem.PIPicture6), "8D report Format", 960, rowInt * 16, 140, 140);
            }

            rowInt += 20;

            if (data.data.problem != null)
            {
                helper.SetCellValue(helper.ws, rowInt, 5, data.data.problem.PIWorkOrder);

                helper.SetCellValue(helper.ws, rowInt, 19, data.data.problem.PIDefectCode);


                helper.SetCellValue(helper.ws, rowInt + 1, 6, data.data.problem.PIDefectQty);

                helper.SetCellValue(helper.ws, rowInt + 1, 19, data.data.problem.PIShiftType);

                helper.SetCellValue(helper.ws, rowInt + 2, 6, data.data.problem.PIProblemDesc);

            }
            rowInt += 4;

            //24H围堵措施
            rowInt += 4;
            if (data.data.actioncontainment != null && data.data.actioncontainment.Count > 0)
            {
                if (data.data.actioncontainment.Count <= 5)
                {
                    for (int i = 0; i < data.data.actioncontainment.Count; i++)
                    {
                        helper.SetCellValue(helper.ws, rowInt + i, 2, (i + 1).ToString());

                        helper.SetCellValue(helper.ws, rowInt + i, 3, data.data.actioncontainment[i].PACWhat);

                        helper.SetCellValue(helper.ws, rowInt + i, 12, data.data.actioncontainment[i].PACWho);

                        helper.SetCellValue(helper.ws, rowInt + i, 14, data.data.actioncontainment[i].PACPlanDateDesc);

                        helper.SetCellValue(helper.ws, rowInt + i, 16, data.data.actioncontainment[i].PACVarificationDateDesc);

                        helper.SetCellValue(helper.ws, rowInt + i, 18, data.data.actioncontainment[i].PACWhere);

                        helper.SetCellValue(helper.ws, rowInt + i, 20, data.data.actioncontainment[i].PACAttachmentDownloadUrl);

                        helper.SetCellValue(helper.ws, rowInt + i, 22, data.data.actioncontainment[i].PACEffeective);

                    }
                    rowInt += 5;
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        helper.SetCellValue(helper.ws, rowInt, 2, i + 1);

                        helper.SetCellValue(helper.ws, rowInt, 3, data.data.actioncontainment[i].PACWhat);

                        helper.SetCellValue(helper.ws, rowInt, 12, data.data.actioncontainment[i].PACWho);

                        helper.SetCellValue(helper.ws, rowInt, 14, data.data.actioncontainment[i].PACPlanDateDesc);

                        helper.SetCellValue(helper.ws, rowInt, 16, data.data.actioncontainment[i].PACVarificationDateDesc);

                        helper.SetCellValue(helper.ws, rowInt, 18, data.data.actioncontainment[i].PACWhere);

                        helper.SetCellValue(helper.ws, rowInt, 20, data.data.actioncontainment[i].PACAttachment);

                        helper.SetCellValue(helper.ws, rowInt, 22, data.data.actioncontainment[i].PACEffeective);

                        rowInt += 1;
                    }
                    for (int i = 5; i < data.data.actioncontainment.Count; i++)
                    {
                        helper.AddRow(helper.ws, rowInt);
                        helper.SetCellValue(helper.ws, rowInt, 2, i + 1);
                        helper.UniteCells(helper.ws, rowInt, 3, rowInt, 11);
                        helper.SetCellValue(helper.ws, rowInt, 3, data.data.actioncontainment[i].PACWhat);
                        helper.UniteCells(helper.ws, rowInt, 12, rowInt, 13);
                        helper.SetCellValue(helper.ws, rowInt, 12, data.data.actioncontainment[i].PACWho);
                        helper.UniteCells(helper.ws, rowInt, 14, rowInt, 15);
                        helper.SetCellValue(helper.ws, rowInt, 14, data.data.actioncontainment[i].PACPlanDateDesc);
                        helper.UniteCells(helper.ws, rowInt, 16, rowInt, 17);
                        helper.SetCellValue(helper.ws, rowInt, 16, data.data.actioncontainment[i].PACVarificationDateDesc);
                        helper.UniteCells(helper.ws, rowInt, 18, rowInt, 19);
                        helper.SetCellValue(helper.ws, rowInt, 18, data.data.actioncontainment[i].PACWhere);
                        helper.UniteCells(helper.ws, rowInt, 20, rowInt, 21);
                        helper.SetCellValue(helper.ws, rowInt, 20, data.data.actioncontainment[i].PACAttachment);
                        helper.UniteCells(helper.ws, rowInt, 22, rowInt, 23);
                        helper.SetCellValue(helper.ws, rowInt, 22, data.data.actioncontainment[i].PACEffeective);
                        rowInt += 1;
                    }
                }


            }
            else
            {
                rowInt += 5;
            }

            //sorting Activity
            rowInt += 2;

            if (data.data.sortingactivity != null && data.data.sortingactivity.Count > 0)
            {
                if (data.data.sortingactivity.Count <= 6)
                {
                    for (int i = 0; i < data.data.sortingactivity.Count; i++)
                    {
                        helper.SetCellValue(helper.ws, rowInt + i, 3, data.data.sortingactivity[i].PSAValueStream);
                        helper.SetCellValue(helper.ws, rowInt + i, 12, data.data.sortingactivity[i].PSADefectQty);
                        helper.SetCellValue(helper.ws, rowInt + i, 14, data.data.sortingactivity[i].PSASortedQty);
                    }
                    rowInt += 6;
                }
                else
                {
                    for (int i = 0; i < 6; i++)
                    {
                        helper.SetCellValue(helper.ws, rowInt, 2, i + 1);
                        helper.SetCellValue(helper.ws, rowInt, 3, data.data.sortingactivity[i].PSAValueStream);
                        helper.SetCellValue(helper.ws, rowInt, 12, data.data.sortingactivity[i].PSADefectQty);
                        helper.SetCellValue(helper.ws, rowInt, 14, data.data.sortingactivity[i].PSASortedQty);
                        rowInt += 1;
                    }
                    for (int i = 6; i < data.data.sortingactivity.Count; i++)
                    {
                        helper.AddRow(helper.ws, rowInt);
                        helper.SetCellValue(helper.ws, rowInt, 2, i + 1);
                        helper.UniteCells(helper.ws, rowInt, 3, rowInt, 11);
                        helper.SetCellValue(helper.ws, rowInt, 3, data.data.sortingactivity[i].PSAValueStream);
                        helper.UniteCells(helper.ws, rowInt, 12, rowInt, 13);
                        helper.SetCellValue(helper.ws, rowInt, 12, data.data.sortingactivity[i].PSADefectQty);
                        helper.UniteCells(helper.ws, rowInt, 14, rowInt, 15);
                        helper.SetCellValue(helper.ws, rowInt, 14, data.data.sortingactivity[i].PSASortedQty);
                        rowInt += 1;
                    }
                }
            }
            else
            {
                rowInt += 6;
            }

            //7D根本原因分析
            rowInt += 2;
            rowInt += 24;
            if (data.data.actionfactoranalysis != null && data.data.actionfactoranalysis.Count > 0)
            {
                for (int i = 0; i < data.data.actionfactoranalysis.Count; i++)
                {
                    switch (data.data.actionfactoranalysis[i].PAFType)
                    {
                        case "A1":
                            helper.SetCellValue(helper.ws, rowInt + 2, 3, data.data.actionfactoranalysis[i].PAFPossibleCause);
                            helper.SetCellValue(helper.ws, rowInt + 2, 9, data.data.actionfactoranalysis[i].PAFWhat);
                            helper.SetCellValue(helper.ws, rowInt + 2, 13, data.data.actionfactoranalysis[i].PAFWho);
                            helper.SetCellValue(helper.ws, rowInt + 2, 17, data.data.actionfactoranalysis[i].PAFValidatedDateDesc);
                            helper.SetCellValue(helper.ws, rowInt + 2, 21, data.data.actionfactoranalysis[i].PAFPotentialCause);
                            break;
                        case "A2":
                            helper.SetCellValue(helper.ws, rowInt + 3, 3, data.data.actionfactoranalysis[i].PAFPossibleCause);
                            helper.SetCellValue(helper.ws, rowInt + 3, 9, data.data.actionfactoranalysis[i].PAFWhat);
                            helper.SetCellValue(helper.ws, rowInt + 3, 13, data.data.actionfactoranalysis[i].PAFWho);
                            helper.SetCellValue(helper.ws, rowInt + 3, 17, data.data.actionfactoranalysis[i].PAFValidatedDateDesc);
                            helper.SetCellValue(helper.ws, rowInt + 3, 21, data.data.actionfactoranalysis[i].PAFPotentialCause);
                            break;
                        case "A3":
                            helper.SetCellValue(helper.ws, rowInt + 4, 3, data.data.actionfactoranalysis[i].PAFPossibleCause);
                            helper.SetCellValue(helper.ws, rowInt + 4, 9, data.data.actionfactoranalysis[i].PAFWhat);
                            helper.SetCellValue(helper.ws, rowInt + 4, 13, data.data.actionfactoranalysis[i].PAFWho);
                            helper.SetCellValue(helper.ws, rowInt + 4, 17, data.data.actionfactoranalysis[i].PAFValidatedDateDesc);
                            helper.SetCellValue(helper.ws, rowInt + 4, 21, data.data.actionfactoranalysis[i].PAFPotentialCause);
                            break;
                        case "A4":
                            helper.SetCellValue(helper.ws, rowInt + 5, 3, data.data.actionfactoranalysis[i].PAFPossibleCause);
                            helper.SetCellValue(helper.ws, rowInt + 5, 9, data.data.actionfactoranalysis[i].PAFWhat);
                            helper.SetCellValue(helper.ws, rowInt + 5, 13, data.data.actionfactoranalysis[i].PAFWho);
                            helper.SetCellValue(helper.ws, rowInt + 5, 17, data.data.actionfactoranalysis[i].PAFValidatedDateDesc);
                            helper.SetCellValue(helper.ws, rowInt + 5, 21, data.data.actionfactoranalysis[i].PAFPotentialCause);
                            break;
                        case "B1":
                            helper.SetCellValue(helper.ws, rowInt + 6, 3, data.data.actionfactoranalysis[i].PAFPossibleCause);
                            helper.SetCellValue(helper.ws, rowInt + 6, 9, data.data.actionfactoranalysis[i].PAFWhat);
                            helper.SetCellValue(helper.ws, rowInt + 6, 13, data.data.actionfactoranalysis[i].PAFWho);
                            helper.SetCellValue(helper.ws, rowInt + 6, 17, data.data.actionfactoranalysis[i].PAFValidatedDateDesc);
                            helper.SetCellValue(helper.ws, rowInt + 6, 21, data.data.actionfactoranalysis[i].PAFPotentialCause);
                            break;
                        case "B2":
                            helper.SetCellValue(helper.ws, rowInt + 7, 3, data.data.actionfactoranalysis[i].PAFPossibleCause);
                            helper.SetCellValue(helper.ws, rowInt + 7, 9, data.data.actionfactoranalysis[i].PAFWhat);
                            helper.SetCellValue(helper.ws, rowInt + 7, 13, data.data.actionfactoranalysis[i].PAFWho);
                            helper.SetCellValue(helper.ws, rowInt + 7, 17, data.data.actionfactoranalysis[i].PAFValidatedDateDesc);
                            helper.SetCellValue(helper.ws, rowInt + 7, 21, data.data.actionfactoranalysis[i].PAFPotentialCause);
                            break;
                        case "B3":
                            helper.SetCellValue(helper.ws, rowInt + 8, 3, data.data.actionfactoranalysis[i].PAFPossibleCause);
                            helper.SetCellValue(helper.ws, rowInt + 8, 9, data.data.actionfactoranalysis[i].PAFWhat);
                            helper.SetCellValue(helper.ws, rowInt + 8, 13, data.data.actionfactoranalysis[i].PAFWho);
                            helper.SetCellValue(helper.ws, rowInt + 8, 17, data.data.actionfactoranalysis[i].PAFValidatedDateDesc);
                            helper.SetCellValue(helper.ws, rowInt + 8, 21, data.data.actionfactoranalysis[i].PAFPotentialCause);
                            break;
                        case "B4":
                            helper.SetCellValue(helper.ws, rowInt + 9, 3, data.data.actionfactoranalysis[i].PAFPossibleCause);
                            helper.SetCellValue(helper.ws, rowInt + 9, 9, data.data.actionfactoranalysis[i].PAFWhat);
                            helper.SetCellValue(helper.ws, rowInt + 9, 13, data.data.actionfactoranalysis[i].PAFWho);
                            helper.SetCellValue(helper.ws, rowInt + 9, 17, data.data.actionfactoranalysis[i].PAFValidatedDateDesc);
                            helper.SetCellValue(helper.ws, rowInt + 9, 21, data.data.actionfactoranalysis[i].PAFPotentialCause);
                            break;
                        case "C1":
                            helper.SetCellValue(helper.ws, rowInt + 10, 3, data.data.actionfactoranalysis[i].PAFPossibleCause);
                            helper.SetCellValue(helper.ws, rowInt + 10, 9, data.data.actionfactoranalysis[i].PAFWhat);
                            helper.SetCellValue(helper.ws, rowInt + 10, 13, data.data.actionfactoranalysis[i].PAFWho);
                            helper.SetCellValue(helper.ws, rowInt + 10, 17, data.data.actionfactoranalysis[i].PAFValidatedDateDesc);
                            helper.SetCellValue(helper.ws, rowInt + 10, 21, data.data.actionfactoranalysis[i].PAFPotentialCause);
                            break;
                        case "C2":
                            helper.SetCellValue(helper.ws, rowInt + 11, 3, data.data.actionfactoranalysis[i].PAFPossibleCause);
                            helper.SetCellValue(helper.ws, rowInt + 11, 9, data.data.actionfactoranalysis[i].PAFWhat);
                            helper.SetCellValue(helper.ws, rowInt + 11, 13, data.data.actionfactoranalysis[i].PAFWho);
                            helper.SetCellValue(helper.ws, rowInt + 11, 17, data.data.actionfactoranalysis[i].PAFValidatedDateDesc);
                            helper.SetCellValue(helper.ws, rowInt + 11, 21, data.data.actionfactoranalysis[i].PAFPotentialCause);
                            break;
                        case "C3":
                            helper.SetCellValue(helper.ws, rowInt + 12, 3, data.data.actionfactoranalysis[i].PAFPossibleCause);
                            helper.SetCellValue(helper.ws, rowInt + 12, 9, data.data.actionfactoranalysis[i].PAFWhat);
                            helper.SetCellValue(helper.ws, rowInt + 12, 13, data.data.actionfactoranalysis[i].PAFWho);
                            helper.SetCellValue(helper.ws, rowInt + 12, 17, data.data.actionfactoranalysis[i].PAFValidatedDateDesc);
                            helper.SetCellValue(helper.ws, rowInt + 12, 21, data.data.actionfactoranalysis[i].PAFPotentialCause);
                            break;
                        case "C4":
                            helper.SetCellValue(helper.ws, rowInt + 13, 3, data.data.actionfactoranalysis[i].PAFPossibleCause);
                            helper.SetCellValue(helper.ws, rowInt + 13, 9, data.data.actionfactoranalysis[i].PAFWhat);
                            helper.SetCellValue(helper.ws, rowInt + 13, 13, data.data.actionfactoranalysis[i].PAFWho);
                            helper.SetCellValue(helper.ws, rowInt + 13, 17, data.data.actionfactoranalysis[i].PAFValidatedDateDesc);
                            helper.SetCellValue(helper.ws, rowInt + 13, 21, data.data.actionfactoranalysis[i].PAFPotentialCause);
                            break;
                        case "D1":
                            helper.SetCellValue(helper.ws, rowInt + 14, 3, data.data.actionfactoranalysis[i].PAFPossibleCause);
                            helper.SetCellValue(helper.ws, rowInt + 14, 9, data.data.actionfactoranalysis[i].PAFWhat);
                            helper.SetCellValue(helper.ws, rowInt + 14, 13, data.data.actionfactoranalysis[i].PAFWho);
                            helper.SetCellValue(helper.ws, rowInt + 14, 17, data.data.actionfactoranalysis[i].PAFValidatedDateDesc);
                            helper.SetCellValue(helper.ws, rowInt + 14, 21, data.data.actionfactoranalysis[i].PAFPotentialCause);
                            break;
                        case "D2":
                            helper.SetCellValue(helper.ws, rowInt + 15, 3, data.data.actionfactoranalysis[i].PAFPossibleCause);
                            helper.SetCellValue(helper.ws, rowInt + 15, 9, data.data.actionfactoranalysis[i].PAFWhat);
                            helper.SetCellValue(helper.ws, rowInt + 15, 13, data.data.actionfactoranalysis[i].PAFWho);
                            helper.SetCellValue(helper.ws, rowInt + 15, 17, data.data.actionfactoranalysis[i].PAFValidatedDateDesc);
                            helper.SetCellValue(helper.ws, rowInt + 15, 21, data.data.actionfactoranalysis[i].PAFPotentialCause);
                            break;
                        case "D3":
                            helper.SetCellValue(helper.ws, rowInt + 16, 3, data.data.actionfactoranalysis[i].PAFPossibleCause);
                            helper.SetCellValue(helper.ws, rowInt + 16, 9, data.data.actionfactoranalysis[i].PAFWhat);
                            helper.SetCellValue(helper.ws, rowInt + 16, 13, data.data.actionfactoranalysis[i].PAFWho);
                            helper.SetCellValue(helper.ws, rowInt + 16, 17, data.data.actionfactoranalysis[i].PAFValidatedDateDesc);
                            helper.SetCellValue(helper.ws, rowInt + 16, 21, data.data.actionfactoranalysis[i].PAFPotentialCause);
                            break;
                        case "D4":
                            helper.SetCellValue(helper.ws, rowInt + 17, 3, data.data.actionfactoranalysis[i].PAFPossibleCause);
                            helper.SetCellValue(helper.ws, rowInt + 17, 9, data.data.actionfactoranalysis[i].PAFWhat);
                            helper.SetCellValue(helper.ws, rowInt + 17, 13, data.data.actionfactoranalysis[i].PAFWho);
                            helper.SetCellValue(helper.ws, rowInt + 17, 17, data.data.actionfactoranalysis[i].PAFValidatedDateDesc);
                            helper.SetCellValue(helper.ws, rowInt + 17, 21, data.data.actionfactoranalysis[i].PAFPotentialCause);
                            break;
                    }
                }
            }
            rowInt += 18;
            rowInt += 2;
            if (data.data.actionwhyanalysisi != null && data.data.actionwhyanalysisi.Count > 0)
            {
                if (data.data.actionwhyanalysisi.Count <= 2)
                {
                    for (int i = 0; i < data.data.actionwhyanalysisi.Count; i++)
                    {
                        helper.SetCellValue(helper.ws, rowInt + i, 2, data.data.actionwhyanalysisi[i].PAWWhyForm);
                        helper.SetCellValue(helper.ws, rowInt + i, 5, data.data.actionwhyanalysisi[i].PAWWhyQuestionChain);
                        helper.SetCellValue(helper.ws, rowInt + i, 11, "Q" + data.data.actionwhyanalysisi[i].PAWWhy1);
                        helper.SetCellValue(helper.ws, rowInt + i, 14, "Q" + data.data.actionwhyanalysisi[i].PAWWhy2);
                        helper.SetCellValue(helper.ws, rowInt + i, 17, "Q" + data.data.actionwhyanalysisi[i].PAWWhy3);
                        helper.SetCellValue(helper.ws, rowInt + i, 20, "Q" + data.data.actionwhyanalysisi[i].PAWWhy4);
                        helper.SetCellValue(helper.ws, rowInt + i, 23, "Q" + data.data.actionwhyanalysisi[i].PAWWhy5);
                    }
                    rowInt += 2;
                }
                else
                {
                    for (int i = 0; i < 2; i++)
                    {
                        helper.SetCellValue(helper.ws, rowInt, 2, data.data.actionwhyanalysisi[i].PAWWhyForm);
                        helper.SetCellValue(helper.ws, rowInt, 5, data.data.actionwhyanalysisi[i].PAWWhyQuestionChain);
                        helper.SetCellValue(helper.ws, rowInt, 11, "Q" + data.data.actionwhyanalysisi[i].PAWWhy1);
                        helper.SetCellValue(helper.ws, rowInt, 14, "Q" + data.data.actionwhyanalysisi[i].PAWWhy2);
                        helper.SetCellValue(helper.ws, rowInt, 17, "Q" + data.data.actionwhyanalysisi[i].PAWWhy3);
                        helper.SetCellValue(helper.ws, rowInt, 20, "Q" + data.data.actionwhyanalysisi[i].PAWWhy4);
                        helper.SetCellValue(helper.ws, rowInt, 23, "Q" + data.data.actionwhyanalysisi[i].PAWWhy5);
                        rowInt += 1;
                    }
                    for (int i = 2; i < data.data.actionwhyanalysisi.Count; i++)
                    {
                        helper.AddRow(helper.ws, rowInt);
                        helper.UniteCells(helper.ws, rowInt + i, 2, rowInt, 4);
                        helper.SetCellValue(helper.ws, rowInt + i, 2, data.data.actionwhyanalysisi[i].PAWWhyForm);
                        helper.UniteCells(helper.ws, rowInt + i, 5, rowInt, 10);
                        helper.SetCellValue(helper.ws, rowInt + i, 5, data.data.actionwhyanalysisi[i].PAWWhyQuestionChain);
                        helper.UniteCells(helper.ws, rowInt + i, 11, rowInt, 13);
                        helper.SetCellValue(helper.ws, rowInt + i, 11, "Q" + data.data.actionwhyanalysisi[i].PAWWhy1);
                        helper.UniteCells(helper.ws, rowInt + i, 14, rowInt, 16);
                        helper.SetCellValue(helper.ws, rowInt + i, 14, "Q" + data.data.actionwhyanalysisi[i].PAWWhy2);
                        helper.UniteCells(helper.ws, rowInt + i, 17, rowInt, 19);
                        helper.SetCellValue(helper.ws, rowInt + i, 17, "Q" + data.data.actionwhyanalysisi[i].PAWWhy3);
                        helper.UniteCells(helper.ws, rowInt + i, 20, rowInt, 22);
                        helper.SetCellValue(helper.ws, rowInt + i, 20, "Q" + data.data.actionwhyanalysisi[i].PAWWhy4);
                        helper.UniteCells(helper.ws, rowInt + i, 23, rowInt, 26);
                        helper.SetCellValue(helper.ws, rowInt + i, 23, "Q" + data.data.actionwhyanalysisi[i].PAWWhy5);
                        rowInt += 1;
                    }
                }
            }
            else
            {
                rowInt += 2;
            }
            rowInt += 1;

            //发生原因and流出原因 没找着 todo
            rowInt += 8;

            //Corrective Actions/纠正措施:
            rowInt += 3;
            if (data.data.actioncorrective != null && data.data.actioncorrective.Count > 0)
            {
                if (data.data.actioncorrective.Count <= 5)
                {
                    for (int i = 0; i < data.data.actioncorrective.Count; i++)
                    {
                        helper.SetCellValue(helper.ws, rowInt + i, 2, i + 1);
                        helper.SetCellValue(helper.ws, rowInt + i, 3, data.data.actioncorrective[i].PACWhat);
                        helper.SetCellValue(helper.ws, rowInt + i, 12, data.data.actioncorrective[i].PACWho);
                        helper.SetCellValue(helper.ws, rowInt + i, 14, data.data.actioncorrective[i].PACPlanDateDesc);
                        helper.SetCellValue(helper.ws, rowInt + i, 16, data.data.actioncorrective[i].PACActualDateDesc);
                        helper.SetCellValue(helper.ws, rowInt + i, 18, data.data.actioncorrective[i].PACWhere);
                        helper.SetCellValue(helper.ws, rowInt + i, 20, data.data.actioncorrective[i].PACAttachmentDownloadUrl);
                        helper.SetCellValue(helper.ws, rowInt + i, 22, data.data.actioncorrective[i].PACStatus);
                        helper.SetCellValue(helper.ws, rowInt + i, 24, data.data.actioncorrective[i].PACComment);
                    }
                    rowInt += 5;
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        helper.SetCellValue(helper.ws, rowInt, 2, i + 1);
                        helper.SetCellValue(helper.ws, rowInt, 3, data.data.actioncorrective[i].PACWhat);
                        helper.SetCellValue(helper.ws, rowInt, 12, data.data.actioncorrective[i].PACWho);
                        helper.SetCellValue(helper.ws, rowInt, 14, data.data.actioncorrective[i].PACPlanDateDesc);
                        helper.SetCellValue(helper.ws, rowInt, 16, data.data.actioncorrective[i].PACActualDateDesc);
                        helper.SetCellValue(helper.ws, rowInt, 18, data.data.actioncorrective[i].PACWhere);
                        helper.SetCellValue(helper.ws, rowInt, 20, data.data.actioncorrective[i].PACAttachmentDownloadUrl);
                        helper.SetCellValue(helper.ws, rowInt, 22, data.data.actioncorrective[i].PACStatus);
                        helper.SetCellValue(helper.ws, rowInt, 24, data.data.actioncorrective[i].PACComment);
                        rowInt += 1;
                    }
                    for (int i = 5; i < data.data.actioncorrective.Count; i++)
                    {
                        helper.AddRow(helper.ws, rowInt);
                        helper.SetCellValue(helper.ws, rowInt, 2, i + 1);
                        helper.UniteCells(helper.ws, rowInt, 3, rowInt, 11);
                        helper.SetCellValue(helper.ws, rowInt, 3, data.data.actioncorrective[i].PACWhat);
                        helper.UniteCells(helper.ws, rowInt, 12, rowInt, 13);
                        helper.SetCellValue(helper.ws, rowInt, 12, data.data.actioncorrective[i].PACWho);
                        helper.UniteCells(helper.ws, rowInt, 14, rowInt, 15);
                        helper.SetCellValue(helper.ws, rowInt, 14, data.data.actioncorrective[i].PACPlanDateDesc);
                        helper.UniteCells(helper.ws, rowInt, 16, rowInt, 17);
                        helper.SetCellValue(helper.ws, rowInt, 16, data.data.actioncorrective[i].PACActualDateDesc);
                        helper.UniteCells(helper.ws, rowInt, 18, rowInt, 19);
                        helper.SetCellValue(helper.ws, rowInt, 18, data.data.actioncorrective[i].PACWhere);
                        helper.UniteCells(helper.ws, rowInt, 20, rowInt, 21);
                        helper.SetCellValue(helper.ws, rowInt, 20, data.data.actioncorrective[i].PACAttachmentDownloadUrl);
                        helper.UniteCells(helper.ws, rowInt, 22, rowInt, 23);
                        helper.SetCellValue(helper.ws, rowInt, 22, data.data.actioncorrective[i].PACStatus);
                        helper.UniteCells(helper.ws, rowInt, 24, rowInt, 26);
                        helper.SetCellValue(helper.ws, rowInt, 243, data.data.actioncorrective[i].PACComment);
                    }
                }
            }
            else
            {
                rowInt += 5;
            }


            //Preventive Measure/预防再发措施:
            rowInt += 3;
            if (data.data.actionpreventive != null && data.data.actionpreventive.Count > 0)
            {
                if (data.data.actionpreventive.Count <= 5)
                {
                    for (int i = 0; i < data.data.actionpreventive.Count; i++)
                    {
                        helper.SetCellValue(helper.ws, rowInt + i, 2, i + 1);
                        helper.SetCellValue(helper.ws, rowInt + i, 3, data.data.actionpreventive[i].PAPWhat);
                        helper.SetCellValue(helper.ws, rowInt + i, 12, data.data.actionpreventive[i].PAPWho);
                        helper.SetCellValue(helper.ws, rowInt + i, 14, data.data.actionpreventive[i].PAPPlanDateDesc);
                        helper.SetCellValue(helper.ws, rowInt + i, 16, data.data.actionpreventive[i].PAPActualDateDesc);
                        helper.SetCellValue(helper.ws, rowInt + i, 18, data.data.actionpreventive[i].PAPWhere);
                        helper.SetCellValue(helper.ws, rowInt + i, 20, data.data.actionpreventive[i].PAPAttachmentDownloadUrl);
                        helper.SetCellValue(helper.ws, rowInt + i, 22, data.data.actionpreventive[i].PAPStatus);
                        helper.SetCellValue(helper.ws, rowInt + i, 24, data.data.actionpreventive[i].PAPComment);
                    }
                    rowInt += 5;
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        helper.SetCellValue(helper.ws, rowInt, 2, i + 1);
                        helper.SetCellValue(helper.ws, rowInt, 3, data.data.actionpreventive[i].PAPWhat);
                        helper.SetCellValue(helper.ws, rowInt, 12, data.data.actionpreventive[i].PAPWho);
                        helper.SetCellValue(helper.ws, rowInt, 14, data.data.actionpreventive[i].PAPPlanDateDesc);
                        helper.SetCellValue(helper.ws, rowInt, 16, data.data.actionpreventive[i].PAPActualDateDesc);
                        helper.SetCellValue(helper.ws, rowInt, 18, data.data.actionpreventive[i].PAPWhere);
                        helper.SetCellValue(helper.ws, rowInt, 20, data.data.actionpreventive[i].PAPAttachmentDownloadUrl);
                        helper.SetCellValue(helper.ws, rowInt, 22, data.data.actionpreventive[i].PAPStatus);
                        helper.SetCellValue(helper.ws, rowInt, 24, data.data.actionpreventive[i].PAPComment);
                        rowInt += 1;
                    }
                    for (int i = 5; i < data.data.actionpreventive.Count; i++)
                    {
                        helper.AddRow(helper.ws, rowInt);
                        helper.SetCellValue(helper.ws, rowInt, 2, i + 1);
                        helper.UniteCells(helper.ws, rowInt, 3, rowInt, 11);
                        helper.SetCellValue(helper.ws, rowInt, 3, data.data.actionpreventive[i].PAPWhat);
                        helper.UniteCells(helper.ws, rowInt, 12, rowInt, 13);
                        helper.SetCellValue(helper.ws, rowInt, 12, data.data.actionpreventive[i].PAPWho);
                        helper.UniteCells(helper.ws, rowInt, 14, rowInt, 15);
                        helper.SetCellValue(helper.ws, rowInt, 14, data.data.actionpreventive[i].PAPPlanDateDesc);
                        helper.UniteCells(helper.ws, rowInt, 16, rowInt, 17);
                        helper.SetCellValue(helper.ws, rowInt, 16, data.data.actionpreventive[i].PAPActualDateDesc);
                        helper.UniteCells(helper.ws, rowInt, 18, rowInt, 19);
                        helper.SetCellValue(helper.ws, rowInt, 18, data.data.actionpreventive[i].PAPWhere);
                        helper.UniteCells(helper.ws, rowInt, 20, rowInt, 21);
                        helper.SetCellValue(helper.ws, rowInt, 20, data.data.actionpreventive[i].PAPAttachmentDownloadUrl);
                        helper.UniteCells(helper.ws, rowInt, 22, rowInt, 23);
                        helper.SetCellValue(helper.ws, rowInt, 22, data.data.actionpreventive[i].PAPStatus);
                        helper.UniteCells(helper.ws, rowInt, 24, rowInt, 26);
                        helper.SetCellValue(helper.ws, rowInt, 24, data.data.actionpreventive[i].PAPComment);
                        rowInt += 1;
                    }
                }
            }
            else
            {
                rowInt += 5;
            }

            //Layered Audit/分层审核:
            rowInt += 3;
            if (data.data.layeredaudit != null && data.data.layeredaudit.Count > 0)
            {
                if (data.data.layeredaudit.Count <= 5)
                {
                    for (int i = 0; i < data.data.layeredaudit.Count; i++)
                    {
                        helper.SetCellValue(helper.ws, rowInt + i, 2, i + 1);
                        helper.SetCellValue(helper.ws, rowInt + i, 3, data.data.layeredaudit[i].PLWhat);
                        helper.SetCellValue(helper.ws, rowInt + i, 12, data.data.layeredaudit[i].PLWho);
                        helper.SetCellValue(helper.ws, rowInt + i, 14, data.data.layeredaudit[i].PLPlanDateDesc);
                        helper.SetCellValue(helper.ws, rowInt + i, 16, data.data.layeredaudit[i].PLActualDateDesc);
                        helper.SetCellValue(helper.ws, rowInt + i, 18, data.data.layeredaudit[i].PLWhere);
                        helper.SetCellValue(helper.ws, rowInt + i, 20, data.data.layeredaudit[i].PLAttachment);
                        helper.SetCellValue(helper.ws, rowInt + i, 22, data.data.layeredaudit[i].PLStatus);
                        helper.SetCellValue(helper.ws, rowInt + i, 24, data.data.layeredaudit[i].PLComment);
                    }
                    rowInt += 5;
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        helper.SetCellValue(helper.ws, rowInt, 2, i + 1);
                        helper.SetCellValue(helper.ws, rowInt, 3, data.data.layeredaudit[i].PLWhat);
                        helper.SetCellValue(helper.ws, rowInt, 12, data.data.layeredaudit[i].PLWho);
                        helper.SetCellValue(helper.ws, rowInt, 14, data.data.layeredaudit[i].PLPlanDateDesc);
                        helper.SetCellValue(helper.ws, rowInt, 16, data.data.layeredaudit[i].PLActualDateDesc);
                        helper.SetCellValue(helper.ws, rowInt, 18, data.data.layeredaudit[i].PLWhere);
                        helper.SetCellValue(helper.ws, rowInt, 20, data.data.layeredaudit[i].PLAttachment);
                        helper.SetCellValue(helper.ws, rowInt, 22, data.data.layeredaudit[i].PLStatus);
                        helper.SetCellValue(helper.ws, rowInt, 24, data.data.layeredaudit[i].PLComment);
                        rowInt += 1;
                    }
                    for (int i = 5; i < data.data.layeredaudit.Count; i++)
                    {
                        helper.AddRow(helper.ws, rowInt);
                        helper.SetCellValue(helper.ws, rowInt, 2, i + 1);
                        helper.UniteCells(helper.ws, rowInt, 3, rowInt + i, 11);
                        helper.SetCellValue(helper.ws, rowInt, 3, data.data.layeredaudit[i].PLWhat);
                        helper.UniteCells(helper.ws, rowInt, 12, rowInt + i, 13);
                        helper.SetCellValue(helper.ws, rowInt, 12, data.data.layeredaudit[i].PLWho);
                        helper.UniteCells(helper.ws, rowInt, 14, rowInt + i, 15);
                        helper.SetCellValue(helper.ws, rowInt, 14, data.data.layeredaudit[i].PLPlanDateDesc);
                        helper.UniteCells(helper.ws, rowInt, 16, rowInt + i, 17);
                        helper.SetCellValue(helper.ws, rowInt, 16, data.data.layeredaudit[i].PLActualDateDesc);
                        helper.UniteCells(helper.ws, rowInt, 18, rowInt + i, 19);
                        helper.SetCellValue(helper.ws, rowInt, 18, data.data.layeredaudit[i].PLWhere);
                        helper.UniteCells(helper.ws, rowInt, 20, rowInt + i, 21);
                        helper.SetCellValue(helper.ws, rowInt, 20, data.data.layeredaudit[i].PLAttachment);
                        helper.UniteCells(helper.ws, rowInt, 22, rowInt + i, 23);
                        helper.SetCellValue(helper.ws, rowInt, 22, data.data.layeredaudit[i].PLStatus);
                        helper.UniteCells(helper.ws, rowInt, 24, rowInt + i, 26);
                        helper.SetCellValue(helper.ws, rowInt, 24, data.data.layeredaudit[i].PLComment);
                        rowInt += 1;
                    }
                }
            }
            else
            {
                rowInt += 5;
            }

            //Corrective Verfication/纠正措施验证：
            rowInt += 3;
            if (data.data.verification != null && data.data.verification.Count > 0)
            {
                if (data.data.verification.Count <= 5)
                {
                    for (int i = 0; i < data.data.verification.Count; i++)
                    {
                        helper.SetCellValue(helper.ws, rowInt + i, 2, i + 1);
                        helper.SetCellValue(helper.ws, rowInt + i, 3, data.data.verification[i].PVWhat);
                        helper.SetCellValue(helper.ws, rowInt + i, 12, data.data.verification[i].PVWho);
                        helper.SetCellValue(helper.ws, rowInt + i, 14, data.data.verification[i].PVPlanDateDesc);
                        helper.SetCellValue(helper.ws, rowInt + i, 16, data.data.verification[i].PVActualDateDesc);
                        helper.SetCellValue(helper.ws, rowInt + i, 18, data.data.verification[i].PVWhere);
                        helper.SetCellValue(helper.ws, rowInt + i, 20, data.data.verification[i].PVAttachmentDownloadUrl);
                        helper.SetCellValue(helper.ws, rowInt + i, 22, data.data.verification[i].PVStatus);
                        helper.SetCellValue(helper.ws, rowInt + i, 24, data.data.verification[i].PVComment);
                    }
                    rowInt += 5;
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        helper.SetCellValue(helper.ws, rowInt, 2, i + 1);
                        helper.SetCellValue(helper.ws, rowInt, 3, data.data.verification[i].PVWhat);
                        helper.SetCellValue(helper.ws, rowInt, 12, data.data.verification[i].PVWho);
                        helper.SetCellValue(helper.ws, rowInt, 14, data.data.verification[i].PVPlanDateDesc);
                        helper.SetCellValue(helper.ws, rowInt, 16, data.data.verification[i].PVActualDateDesc);
                        helper.SetCellValue(helper.ws, rowInt, 18, data.data.verification[i].PVWhere);
                        helper.SetCellValue(helper.ws, rowInt, 20, data.data.verification[i].PVAttachmentDownloadUrl);
                        helper.SetCellValue(helper.ws, rowInt, 22, data.data.verification[i].PVStatus);
                        helper.SetCellValue(helper.ws, rowInt, 24, data.data.verification[i].PVComment);
                        rowInt += 1;
                    }
                    for (int i = 5; i < data.data.verification.Count; i++)
                    {
                        helper.AddRow(helper.ws, rowInt);
                        helper.SetCellValue(helper.ws, rowInt, 2, i + 1);
                        helper.UniteCells(helper.ws, rowInt, 3, rowInt, 11);
                        helper.SetCellValue(helper.ws, rowInt, 3, data.data.verification[i].PVWhat);
                        helper.UniteCells(helper.ws, rowInt, 12, rowInt, 13);
                        helper.SetCellValue(helper.ws, rowInt, 12, data.data.verification[i].PVWho);
                        helper.UniteCells(helper.ws, rowInt, 14, rowInt, 15);
                        helper.SetCellValue(helper.ws, rowInt, 14, data.data.verification[i].PVPlanDateDesc);
                        helper.UniteCells(helper.ws, rowInt, 16, rowInt, 17);
                        helper.SetCellValue(helper.ws, rowInt, 16, data.data.verification[i].PVActualDateDesc);
                        helper.UniteCells(helper.ws, rowInt, 18, rowInt, 19);
                        helper.SetCellValue(helper.ws, rowInt, 18, data.data.verification[i].PVWhere);
                        helper.UniteCells(helper.ws, rowInt, 20, rowInt, 21);
                        helper.SetCellValue(helper.ws, rowInt, 20, data.data.verification[i].PVAttachmentDownloadUrl);
                        helper.UniteCells(helper.ws, rowInt, 22, rowInt, 23);
                        helper.SetCellValue(helper.ws, rowInt, 22, data.data.verification[i].PVStatus);
                        helper.UniteCells(helper.ws, rowInt, 24, rowInt, 26);
                        helper.SetCellValue(helper.ws, rowInt, 24, data.data.verification[i].PVComment);
                        rowInt += 1;
                    }
                }
            }
            else
            {
                rowInt += 5;
            }

            //Standardlize/标准化：
            rowInt += 3;
            if (data.data.standardization != null && data.data.standardization.Count > 0)
            {
                if (data.data.standardization.Count <= 5)
                {
                    for (int i = 0; i < data.data.standardization.Count; i++)
                    {
                        helper.SetCellValue(helper.ws, rowInt + i, 2, data.data.standardization[i].PSItemName);
                        helper.SetCellValue(helper.ws, rowInt + i, 6, data.data.standardization[i].PSNeedUpdate == 0 ? "N" : "Y");
                        helper.SetCellValue(helper.ws, rowInt + i, 8, data.data.standardization[i].PSItemNameNo);
                        helper.SetCellValue(helper.ws, rowInt + i, 12, data.data.standardization[i].PSOldVersion);
                        helper.SetCellValue(helper.ws, rowInt + i, 14, data.data.standardization[i].PSNewVersion);
                        helper.SetCellValue(helper.ws, rowInt + i, 16, data.data.standardization[i].PSWho);
                        helper.SetCellValue(helper.ws, rowInt + i, 19, data.data.standardization[i].PSPlanDateDesc);
                        helper.SetCellValue(helper.ws, rowInt + i, 21, data.data.standardization[i].PSActualDateDesc);
                        helper.SetCellValue(helper.ws, rowInt + i, 23, data.data.standardization[i].PSAttachmentDownloadUrl);
                    }
                    rowInt += 5;
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        helper.SetCellValue(helper.ws, rowInt, 2, data.data.standardization[i].PSItemName);
                        helper.SetCellValue(helper.ws, rowInt, 6, data.data.standardization[i].PSNeedUpdate == 0 ? "N" : "Y");
                        helper.SetCellValue(helper.ws, rowInt, 8, data.data.standardization[i].PSItemNameNo);
                        helper.SetCellValue(helper.ws, rowInt, 12, data.data.standardization[i].PSOldVersion);
                        helper.SetCellValue(helper.ws, rowInt, 14, data.data.standardization[i].PSNewVersion);
                        helper.SetCellValue(helper.ws, rowInt, 16, data.data.standardization[i].PSWho);
                        helper.SetCellValue(helper.ws, rowInt, 19, data.data.standardization[i].PSPlanDateDesc);
                        helper.SetCellValue(helper.ws, rowInt, 21, data.data.standardization[i].PSActualDateDesc);
                        helper.SetCellValue(helper.ws, rowInt, 23, data.data.standardization[i].PSAttachmentDownloadUrl);
                        rowInt += 1;
                    }
                    for (int i = 5; i < data.data.standardization.Count; i++)
                    {
                        helper.UniteCells(helper.ws, rowInt, 2, rowInt, 5);
                        helper.SetCellValue(helper.ws, rowInt, 2, data.data.standardization[i].PSItemName);
                        helper.UniteCells(helper.ws, rowInt, 6, rowInt, 7);
                        helper.SetCellValue(helper.ws, rowInt, 6, data.data.standardization[i].PSNeedUpdate == 0 ? "N" : "Y");
                        helper.UniteCells(helper.ws, rowInt, 8, rowInt, 11);
                        helper.SetCellValue(helper.ws, rowInt, 8, data.data.standardization[i].PSItemNameNo);
                        helper.UniteCells(helper.ws, rowInt, 12, rowInt, 13);
                        helper.SetCellValue(helper.ws, rowInt, 12, data.data.standardization[i].PSOldVersion);
                        helper.UniteCells(helper.ws, rowInt, 14, rowInt, 15);
                        helper.SetCellValue(helper.ws, rowInt, 14, data.data.standardization[i].PSNewVersion);
                        helper.UniteCells(helper.ws, rowInt, 16, rowInt, 18);
                        helper.SetCellValue(helper.ws, rowInt, 16, data.data.standardization[i].PSWho);
                        helper.UniteCells(helper.ws, rowInt, 19, rowInt, 20);
                        helper.SetCellValue(helper.ws, rowInt, 19, data.data.standardization[i].PSPlanDateDesc);
                        helper.UniteCells(helper.ws, rowInt, 21, rowInt, 22);
                        helper.SetCellValue(helper.ws, rowInt, 21, data.data.standardization[i].PSActualDateDesc);
                        helper.UniteCells(helper.ws, rowInt, 23, rowInt, 26);
                        helper.SetCellValue(helper.ws, rowInt, 23, data.data.standardization[i].PSAttachmentDownloadUrl);
                        rowInt += 1;
                    }
                }
            }

            var excelurl = Request.MapPath("/App_Data/uploads/ProblemPreviewReort" + DateTime.Now.ToString(CommonConstant.DateTimeFormatDaySecondsOnly) + ".xlsx");
            helper.SaveAs(excelurl);
            helper.Close();
            return File(excelurl, "application/vnd.ms-excel", "ProblemPreviewReort" + DateTime.Now.ToString(CommonConstant.DateTimeFormatDaySecondsOnly) + ".xlsx");
        }
        #endregion
    }
}
