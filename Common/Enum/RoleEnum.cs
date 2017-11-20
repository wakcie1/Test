using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Common.Enum
{
    public enum RoleEnum
    {
        /// <summary>
        /// 页面
        /// </summary>
        [Description("页面")]
        Page = 1,

        /// <summary>
        /// 功能
        /// </summary>
        [Description("功能")]
        Function = 2,
    }
}
