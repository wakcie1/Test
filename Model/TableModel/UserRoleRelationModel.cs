using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.TableModel
{
    public class UserRoleRelationModel
    {
        /// <summary>
        /// 权限分配Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 用户Id [T_BASE_USER.Id]
        /// </summary>
        public long BURUserId { get; set; }

        /// <summary>
        /// 资源Id [T_BASE_GROUP.Id]
        /// </summary>
        public long BURGroupId { get; set; }

        /// <summary>
        /// 有效性（0.无效 1.有效）
        /// </summary>
        public int BURIsValid { get; set; }

        /// <summary>
        /// 创建人工号
        /// </summary>
        public string BURCreateUserId { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string BURCreateUserName { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime BURCreateTime { get; set; }

        /// <summary>
        /// 操作人工号
        /// </summary>
        public string BUROperateUserId { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string BUROperateUserName { get; set; }

        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime BUROperateTime { get; set; }
    }
}
