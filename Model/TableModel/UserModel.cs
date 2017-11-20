using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.TableModel
{
    public class UserModel
    {
        /// <summary>
        /// 人员表Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string BUName { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string BUJobNumber { get; set; }

        /// <summary>
        /// 性别(1-男，2-女，0-其他)
        /// </summary>
        public int? BUSex { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string BUAvatars { get; set; }

        /// <summary>
        /// 头像URL
        /// </summary>
        public string AvatarsUrl { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string BUPhoneNum { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string BUEmail { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public int BUDepartId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string BUDepartName { get; set; }

        /// <summary>
        /// 岗位头衔
        /// </summary>
        public string BUTitle { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public string BUCreateUserNo { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string BUCreateUserName { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime BUCreateTime { get; set; }

        /// <summary>
        /// 操作人Id
        /// </summary>
        public string BUOperateUserNo { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string BUOperateUserName { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime BUOperateTime { get; set; }

        /// <summary>
        /// 有效性（0.无效 1.有效）
        /// </summary>
        public int? BUIsValid { get; set; }


        public string BUEnglishName { get; set; }
        public string BUPosition { get; set; }
        public string BUExtensionPhone { get; set; }
        public string BUMobilePhone { get; set; }

        public string Account { get; set; }
    }
}
