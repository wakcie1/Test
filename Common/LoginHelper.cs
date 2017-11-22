using Model.Home;
using Common.Costant;
using System.Collections.Generic;

namespace Common
{
    /// <summary>
    /// Helper
    /// </summary>
    public class LoginHelper
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public static UserLoginInfo GetUser()
        {
            UserLoginInfo userInfo = new UserLoginInfo();
            var key = CommonHelper.Md5(CookieKey.COOKIE_KEY_USERINFO);
            var data = CookieHelper.GetCookieValue(key);
            if (!string.IsNullOrEmpty(data))
            {
                data = CommonHelper.DesDecrypt(data, HomeContent.CookieKeyEncrypt);
                userInfo = JsonHelper.Deserialize<UserLoginInfo>(data);
            }
            return userInfo;
        }

        /// <summary>
        /// 描述：从cookie获取权限的code
        /// </summary>
        /// <returns></returns>
        //public static string GetAllRoleCode()
        //{
        //    var rolecode = string.Empty;
        //    if (GetUser().SuperAdmin)
        //    {
        //        rolecode += "\"isadmin\",";
        //    }

        //    var list = new List<string>();
        //    var key = CommonHelper.Md5(CookieKey.COOKIE_KEY_ROLEINFO);
        //    var data = CookieHelper.GetCookieValue(key);
        //    if (!string.IsNullOrEmpty(data))
        //    {
        //        data = CommonHelper.DesDecrypt(data, CookieKey.COOKIE_KEY_ENCRYPT);
        //        list = JsonHelper.Deserialize<List<string>>(data);
        //    }
         
        //    return rolecode;
        //}
    }
}
