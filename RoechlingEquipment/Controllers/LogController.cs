using Business;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoechlingEquipment.Controllers
{
    [ServerAuthorize]
    public class LogController:BaseController
    {
        public ActionResult LogIndex()
        {
            //todo 获取第一条日志信息
            //var logList = LogBusiness.LogSearch(string.Empty);
            // var IsDisplay = false;
            //if (logList != null&& logList.Count>0)
            //{
            //    var dateTime = DateTime.Now - logList.First().BLCreateTime;
            //    ViewBag.FirstlogTime = logList.First().BLCreateTime.ToString(Common.Costant.CommonConstant.DateTimeFormatDayMinutesOnly) 
            //        + "-" + dateTime.Days + "天" + dateTime.Hours + "小时" + dateTime.Minutes + "分钟" + dateTime.Seconds + "秒";
            //    IsDisplay = true;
            //}
            //ViewBag.IsDisplay = IsDisplay;
            return View();
        }
        public ActionResult LogSearch(string code="")
        {
            var IsDisplay = false;
            var logList = LogBusiness.LogSearch(code);
            if (logList != null && logList.Count > 0)
            {
                var dateTime = DateTime.Now - logList.First().BLCreateTime;
                //ViewBag.FirstlogTime = logList.First().BLCreateTime.ToString(Common.Costant.CommonConstant.DateTimeFormatDayMinutesOnly)
                //    + "-" + dateTime.Days + "天" + dateTime.Hours + "小时" + dateTime.Minutes + "分钟" + dateTime.Seconds + "秒";
                ViewBag.FirstlogTime = code + "-" + dateTime.Days + "天" + dateTime.Hours + "小时" + dateTime.Minutes + "分钟" + dateTime.Seconds + "秒";
                IsDisplay = true;
            }
            ViewBag.IsDisplay = IsDisplay;
            return View(logList);
        }
    }
}