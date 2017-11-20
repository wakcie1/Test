
using Business;
using Common;
using Common.Costant;
using Common.Enum;
using Model.CommonModel;
using Model.Material;
using Model.TableModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace RoechlingEquipment.Controllers
{
    [ServerAuthorize]
    public class ManagementReportController : BaseController
    {
        public ActionResult ManagementReportIndex()
        {
            return View();
        }

        public FileResult ProblemCloseExport()
        {
            var helper = new ExcelHelper();
            var time = DateTime.Now;
            var data = ManagementReportBusiness.GetProblemByStartAndEnd(time);
            if (data != null)
            {
                var rowInt = 24;
                helper.Open(Request.MapPath("/ReportTemplate/ProblemClosedTrendC.xlsx"));
                helper.ws = helper.GetSheet("问题关闭率Problem Closed Trend C ");

                var startTime = time.AddYears(-1).AddDays((-1) * time.Day + 1);
                for (int i = 0; i < 12; i++)
                {
                    var endTime = startTime.AddMonths(1);
                    helper.SetCellValue(helper.ws, rowInt, i + 2, startTime.ToString("yyyy-MM"));
                    helper.SetCellValue(helper.ws, rowInt+1, i + 2, 1);
                    var openCount = data.Where(t => t.PIProblemDate >= startTime && t.PIProblemDate < endTime).ToList().Count;
                    var CompleteCount = data.Where(t => t.PIProblemDate >= startTime && t.PIProblemDate < endTime
                        && t.PIProcessStatus == ProblemProcessStatusEnum.Authorized.GetHashCode()).ToList().Count;
                    helper.SetCellValue(helper.ws, rowInt + 2, i + 2, openCount);
                    helper.SetCellValue(helper.ws, rowInt + 3, i + 2, CompleteCount);
                    startTime = endTime;
                }

                var excelurl = Request.MapPath("/App_Data/uploads/ProblemClosedTrendC" + DateTime.Now.ToString(CommonConstant.DateTimeFormatDaySecondsOnly) + ".xlsx");
                helper.SaveAs(excelurl);
                helper.Close();
                return File(excelurl, "application/vnd.ms-excel", "ProblemClosedTrendC" + DateTime.Now.ToString(CommonConstant.DateTimeFormatDaySecondsOnly) + ".xlsx");
            }
            return File(Request.MapPath("/ReportTemplate/ProblemClosedTrendC.xlsx"), "application/vnd.ms-excel", "ProblemClosedTrendC" + DateTime.Now.ToString(CommonConstant.DateTimeFormatDaySecondsOnly) + ".xlsx");
        }
    }
}
