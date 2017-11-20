using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.TableModel
{
    public class RoleModel
    {
        /// <summary>
        /// 权限Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        public string BRCode { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string BRName { get; set; }

        /// <summary>
        /// 权限种类（1.页面 2.功能）
        /// </summary>
        public int BRType { get; set; }

        /// <summary>
        /// 有效性（0.无效 1.有效）
        /// </summary>
        public int BRIsValid { get; set; }
    }
}
