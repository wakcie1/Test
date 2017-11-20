using Business;
using Model.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoechlingEquipment.Interface
{
    /// <summary>
    /// DistributionRoleToGroup 的摘要说明(已作废)
    /// </summary>
    public class DistributionRoleToGroup : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var roleName = context.Request["roleName"];
            var groupName = context.Request["groupName"];

            var roleInfo = JurisdictionBusiness.GetRoleByRoleName(roleName);
            var groupInfo = JurisdictionBusiness.GetGroupByGroupName(groupName);
            var result = new ResultInfoModel {IsSuccess=false };
            if (roleInfo != null && groupInfo != null)
            {
                result = JurisdictionBusiness.DiRoleToGroup(roleInfo.Id, groupInfo.Id);
            }
            if (result.IsSuccess)
            {
                context.Response.Write("Success");
            }
            else
            {
                context.Response.Write(result.Message);
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