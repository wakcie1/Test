
using Business;
using Common;
using Common.Costant;
using Common.Enum;
using Model.CommonModel;
using Model.Home;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace RoechlingEquipment.Controllers
{
    public class LoginController : BaseController
    {
        //TODO:CPF
        public ActionResult LoginPage()
        {
            // Validate input
            var workNo = string.Empty;
            //HomeBusiness.UserLogin("", "", "", out workNo);
            ViewBag.BasePath = BasePath;
            ViewBag.CurrentCulture = CultureHelper.GetCurrentCulture();

            var dbtype = new List<SelectListItem>();
            //var dbtypeCollection = (ConnectionStringsSection)ConfigurationManager.GetSection("connectionStrings");
            var dbtypeStr = ConfigurationManager.AppSettings["DBType"].ToString();
            var dbtypeCollection = dbtypeStr.Split(',');

            foreach (var item in dbtypeCollection)
            {
                var listitem = new SelectListItem();
                listitem.Text = item;
                listitem.Value = item;
                dbtype.Add(listitem);
            }
            ViewBag.DBType = dbtype;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string account, string password, string dbtype)
        {
            var msg = string.Empty;
            var success = false;

            try
            {
                if (!string.IsNullOrEmpty(dbtype))
                {
                    Session.Timeout = 1440;
                    Session[SessionKey.SESSION_KEY_DBINFO] = dbtype;
                }
                else
                {
                    return RedirectToAction("LoginPage");
                }
                var result = HomeBusiness.Login(account, password);

                //写入cookie
                string key = CommonHelper.Md5(CookieKey.COOKIE_KEY_USERINFO);
                string data = JsonHelper.Serializer<UserLoginInfo>(result);
                CookieHelper.SetCookie(
                    key,
                    CommonHelper.DesEncrypt(data, HomeContent.CookieKeyEncrypt),
                    DateTime.Now.AddDays(1).Date,
                    ServerInfo.GetTopDomain);

                //写入权限信息
                var list = JurisdictionBusiness.GetAllRoleCodeByUserid(LoginUser);
                string roleKey = CommonHelper.Md5(CookieKey.COOKIE_KEY_ROLEINFO);
                string roleData = JsonHelper.Serializer<List<string>>(list);
                CookieHelper.SetCookie(roleKey, CommonHelper.DesEncrypt(roleData, CookieKey.COOKIE_KEY_ENCRYPT),
                    DateTime.Now.AddDays(1).Date, ServerInfo.GetTopDomain);

                success = true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Json(new { Success = success, Message = msg });
        }

        [HttpPost]
        public ActionResult ContactAdmin(string username)
        {
            var send = new EmailSendModel()
            {
                MailTitle = "reset the password",
                MailContent = string.Format("Account:{0} need to reset the password", username)
            };
            var result = EmailHelper.SendToSystemAdmin(send);
            return Json(result);
        }
    }
}
