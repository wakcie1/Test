using Business;
using Common.Costant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoechlingEquipment.Interface
{
    /// <summary>
    /// InitialAdminAccount 的摘要说明
    /// </summary>
    public class InitialAdminAccount : IHttpHandler,System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["dbTyPe"]))
                {
                    var dbtype= HttpContext.Current.Request.QueryString["dbTyPe"];
                    //HttpContext.Current.Session[SessionKey.SESSION_KEY_DBINFO] = dbtype;
                    context.Session[SessionKey.SESSION_KEY_DBINFO] = dbtype;
                }
                else
                {
                    context.Response.Write("Plese Check dbType");
                    return;
                }
                HomeBusiness.InitialManager();
                context.Response.Write("Initial Success");
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
            }
            //初始化管理员账号
         
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}