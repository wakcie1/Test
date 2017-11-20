using Model.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.ViewModel.Jurisdiction
{
    public class JurisdictionIndexViewModel
    {
        public string UserId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string Jobnumber { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 角色包名称
        /// </summary>
        public string RoleGroup { get; set; }
    }

    public class JurisdictionSearchModel
    {
        public IEnumerable<JurisdictionIndexViewModel> Models { get; set; }
        public Page Page { get; set; }
    }

    public class JurisdictionRoleGroupIndexViewModel
    {
        public string GroupId { get; set; }
        public string GroupName { get; set; }

        public string GroupCode { get; set; }

        public string UserInfo { get; set; }
    }

    public class JurisdictionRoleGroupSearchModel
    {
        public IEnumerable<JurisdictionRoleGroupIndexViewModel> Models { get; set; }
        public Page Page { get; set; }
    }

    public class JurisdictionEditViewModel
    {
        public string UserId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string Jobnumber { get; set; }

        /// <summary>
        /// 权限包信息
        /// </summary>
        public List<RoleGroupDic> RoleGroupList { get; set; }
    }

    public class JurisdictionByRoleGroupEditViewModel
    {
        /// <summary>
        /// 角色包Id
        /// </summary>
        public string GroupId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 权限包信息
        /// </summary>
        public List<UserDic> UserList { get; set; }
    }

    public class UserDic
    {
        public string userId { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户工号
        /// </summary>
        public string JobNumber { get; set; }
    }

    public class RoleGroupDic
    {
        public string RoleGroupId { get; set; }

        public string RoleGroupName { get; set; }
    }

    public class SaveModel
    {
        public string UserId { get; set; }

        public List<string> RoleGroupIds { get; set; }
    }

    public class SaveByGroupIdModel
    {
        public string GroupId { get; set; }

        public List<string> UserIds { get; set; }
    }

    public class RoleSearchViewModel
    {
        private int _pageSize = 10;

        private int _currentPage = 1;

        public string GroupName { get; set; }

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

    public class RoleView
    {
        public string RoleId { get; set; }

        public string Rolename { get; set; }

        public string RoleCode { get; set; }
    }


    public class UserGroupModel
    {
        public long UserId { get; set; }

        public long GroupId { get; set; }

        public string GroupCode { get; set; }
    }
}
