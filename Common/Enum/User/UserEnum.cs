using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Common.Enum.User
{
    public enum UserEnum
    {
        /// <summary>
        /// 管理员
        /// </summary>
        [Description("管理员")]
        Manager = 1,

        /// <summary>
        /// 员工
        /// </summary>
        [Description("员工")]
        Employee = 2,
    }
}
