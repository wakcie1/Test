using Business;
using Common.Costant;
using Common.Enum;
using Model.TableModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoechlingEquipment.Interface
{
    /// <summary>
    /// InitialRole 的摘要说明
    /// </summary>
    public class InitialRole : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["dbTyPe"]))
                {
                    HttpContext.Current.Session.Timeout = 1440;
                    HttpContext.Current.Session[SessionKey.SESSION_KEY_DBINFO] = HttpContext.Current.Request.QueryString["dbTyPe"];
                }
                else
                {
                    context.Response.Write("Plese Check dbType");
                    return;
                }
                //初始化页面权限
                JurisdictionBusiness.InititalPageRole();
                JurisdictionBusiness.InititalProblemRole();
                JurisdictionBusiness.InititalSolvingteamRole();
                JurisdictionBusiness.InititalQualityalertRole();
                JurisdictionBusiness.InititalSortingactivityRole();
                JurisdictionBusiness.InititalContainmentactionRole();
                JurisdictionBusiness.InititalFactanalyRole();
                JurisdictionBusiness.InititalWhyAnalyRole();
                JurisdictionBusiness.InititalCorrectiveActionRole();
                JurisdictionBusiness.InititalPreventiveMeasuresRole();
                JurisdictionBusiness.InititalLayeredAuditRole();
                JurisdictionBusiness.InititalVerificationRole();
                JurisdictionBusiness.InititalStandardizationRole();

                //初始化资源包
                JurisdictionBusiness.InitialPageGroup();
                context.Response.Write("Initial Success");

            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
            }
            
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