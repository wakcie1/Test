using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.TableModel
{
    public class RoleGroupModel
    {
        /// <summary>
        /// 权限分配Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 权限Id [T_BASE_ROLE.Id]
        /// </summary>
        public long BRGRoleId { get; set; }

        /// <summary>
        /// 资源Id [T_BASE_GROUP.Id]
        /// </summary>
        public long BRGGroupId { get; set; }

        /// <summary>
        /// 有效性（0.无效 1.有效）
        /// </summary>
        public int BRGIsValid { get; set; }
    }
}
