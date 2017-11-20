using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoechlingEquipment.Interface
{
    /// <summary>
    /// Test 的摘要说明
    /// </summary>
    public class Test : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var date = HttpContext.Current.Request.QueryString["date"];
            var reult = DataConvertHelper.IsDateTime(date);
            context.Response.ContentType = "text/plain";
            context.Response.Write(reult.ToString());
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