using Model.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.ViewModel.User
{
    public class UserView
    {
        /// <summary>
        /// 人员表Id
        /// </summary>
        public string UserId { get; set; }

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
        public int BUSex { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string BUAvatars { get; set; }

        /// <summary>
        /// 头像Url
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
        /// 部门Id
        /// </summary>
        public string  DepartId { get; set; }

        /// <summary>
        /// 岗位头衔
        /// </summary>
        public string BUTitle { get; set; }
        
        /// <summary>
        /// 有效性（0.无效 1.有效）
        /// </summary>
        public int? BUIsValid { get; set; }

        public string SexStr { get; set; }

        public string BUDepartName { get; set; }
        public string BUExtensionPhone { get; set; }
        public string BUEnglishName { get; set; }
        public string BUPosition { get; set; }
        public string BUMobilePhone { get; set; }

        public string Account { get; set; }
        public bool IsExistAccount { get; set; }
    }

    public class UserSearchRsultModel
    {
        public IEnumerable<UserView> Models { get; set; }
        public Page Page { get; set; }
    }

    public class UserSearchViewModel
    {
        private int _pageSize = 10;

        private int _currentPage = 1;

        public string Account { get; set; }

        public string Name { get; set; }

        public string DepartName { get; set; }

        public string Position { get; set; }

        public int CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
            }
        }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }
    }
}
