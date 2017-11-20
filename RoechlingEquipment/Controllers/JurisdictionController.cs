using Business;
using Common;
using Model.CommonModel;
using Model.ViewModel.Jurisdiction;
using Model.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoechlingEquipment.Controllers
{
    [ServerAuthorize]
    public class JurisdictionController : BaseController
    {
        public ActionResult JurisdictionIndex()
        {
            return View();
        }

        public ActionResult JurisdictionRolePackageIndex()
        {
            return View();
        }

        public ActionResult JurisdictionSearch(UserSearchViewModel usersearch)
        {
            //分页获取所有的用户信息
            var total = 0;
            var result = HomeBusiness.SearchUser(usersearch, out total);
            var page = new Page(total, usersearch.CurrentPage);
            var list = new List<JurisdictionIndexViewModel>();

            foreach (var item in result)
            {
                var viewModel = new JurisdictionIndexViewModel
                {
                    UserId = item.UserId,
                    Username = item.BUName,
                    Jobnumber = item.BUJobNumber,
                    Position = item.BUTitle,
                };
                //根据用户id去资源权限分类表查询资源信息
                var relationinfo = JurisdictionBusiness.GetUserRoleRelationByUserId(long.Parse(EncryptHelper.DesDecrypt(item.UserId.ToString())));
                if (relationinfo != null)
                {
                    foreach (var roloGroup in relationinfo)
                    {
                        var roloGroupInfo = JurisdictionBusiness.GetGroupById(roloGroup.BURGroupId);
                        if (roloGroupInfo != null)
                        {
                            viewModel.RoleGroup += roloGroupInfo.BGName + ",";
                        }
                    }
                    if (viewModel.RoleGroup != null && viewModel.RoleGroup.Length > 0)
                    {
                        viewModel.RoleGroup = viewModel.RoleGroup.Substring(0, viewModel.RoleGroup.Length - 1);
                    }
                }
                list.Add(viewModel);
            }

            var resultModel = new JurisdictionSearchModel()
            {
                Models = list,
                Page = page
            };
            return View(resultModel);
        }

        public ActionResult JurisdictionRolePackageSearch(RoleSearchViewModel rolesearch)
        {
            //分页获取所有的角色信息
            var total = 0;
            var result = JurisdictionBusiness.SearchRole(rolesearch, out total);
            var page = new Page(total, rolesearch.CurrentPage);
            var list = new List<JurisdictionRoleGroupIndexViewModel>();

            foreach (var item in result)
            {
                var viewModel = new JurisdictionRoleGroupIndexViewModel
                {
                    GroupId = item.RoleId,
                    GroupName = item.Rolename,
                    GroupCode = item.RoleCode,
                };
                //根据用户id去资源权限分类表查询资源信息
                var relationinfo = JurisdictionBusiness.GetUserRoleRelationByGroupId(long.Parse(EncryptHelper.DesDecrypt(item.RoleId.ToString())));
                if (relationinfo != null)
                {
                    foreach (var roloGroup in relationinfo)
                    {
                        var userInfo = HomeBusiness.GetUserById(roloGroup.BURUserId);
                        if (userInfo != null)
                        {
                            viewModel.UserInfo += userInfo.BUName + "(" + userInfo.BUJobNumber + ")" + ",";
                        }
                    }
                    if (viewModel.UserInfo != null && viewModel.UserInfo.Length > 0)
                    {
                        viewModel.UserInfo = viewModel.UserInfo.Substring(0, viewModel.UserInfo.Length - 1);
                    }
                }
                list.Add(viewModel);
            }

            var resultModel = new JurisdictionRoleGroupSearchModel()
            {
                Models = list,
                Page = page
            };
            return View(resultModel);
        }

        public ActionResult JurisdictionEdit()
        {
            var rgList = new List<SelectListItem>();
            var defaultItem = new SelectListItem { Text = "请选择", Value = EncryptHelper.DesEncrypt("0") };
            rgList.Add(defaultItem);
            var list = JurisdictionBusiness.GetAllGroup();
            if (list != null && list.Count > 0)
            {
                foreach (var rg in list)
                {
                    var item = new SelectListItem();
                    item.Text = rg.BGName;
                    item.Value = EncryptHelper.DesEncrypt(rg.Id.ToString());
                    rgList.Add(item);
                }
            }
            ViewBag.RoleGroup = rgList;
            return View();
        }

        public ActionResult JurisdictionRolePackageEdit()
        {
            //var userLis = new List<SelectListItem>();
            //var defaultItem = new SelectListItem { Text = "请选择", Value = EncryptHelper.DesEncrypt("0") };
            //userLis.Add(defaultItem);
            //var list = HomeBusiness.GetAllUser();
            //if (list != null && list.Count > 0)
            //{
            //    foreach (var rg in list)
            //    {
            //        var item = new SelectListItem();
            //        item.Text = rg.BUName + "(" + rg.BUJobNumber + ")";
            //        item.Value = EncryptHelper.DesEncrypt(rg.Id.ToString());
            //        userLis.Add(item);
            //    }
            //}
            //ViewBag.UserList = userLis;
            return View();
        }

        public ActionResult Save(string paraStr)
        {
            var model = JsonHelper.Deserialize<SaveModel>(paraStr);
            var result = new ResultInfoModel { IsSuccess = false };
            if (model != null)
            {
                result = JurisdictionBusiness.SaveRoleGroupRelation(model, this.LoginUser);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveByGroup(string paraStr)
        {
            var model = JsonHelper.Deserialize<SaveByGroupIdModel>(paraStr);
            var result = new ResultInfoModel { IsSuccess = false };
            if (model != null)
            {
                result = JurisdictionBusiness.SaveRoleGroupRelationByGroup(model, this.LoginUser);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InitJurisdiction(string userId)
        {
            var model = new JurisdictionEditViewModel
            {
                RoleGroupList = new List<RoleGroupDic>(),
            };

            var userinfo = HomeBusiness.GetUserById(long.Parse(EncryptHelper.DesDecrypt(userId)));
            if (userinfo != null)
            {
                model.UserId = userId;
                model.Username = userinfo.BUName;
                model.Jobnumber = userinfo.BUJobNumber;
                var relationinfo = JurisdictionBusiness.GetUserRoleRelationByUserId(long.Parse(EncryptHelper.DesDecrypt(userId)));
                if (relationinfo != null)
                {
                    foreach (var roloGroup in relationinfo)
                    {
                        var roloGroupInfo = JurisdictionBusiness.GetGroupById(roloGroup.BURGroupId);
                        if (roloGroupInfo != null)
                        {
                            var dic = new RoleGroupDic();
                            dic.RoleGroupId = EncryptHelper.DesEncrypt(roloGroup.BURGroupId.ToString());
                            dic.RoleGroupName = roloGroupInfo.BGName;
                            model.RoleGroupList.Add(dic);
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InitInitJurisdictionRoleGroup(string groupId)
        {
            var model = new JurisdictionByRoleGroupEditViewModel
            {
                UserList = new List<UserDic>(),
            };
            var groupInfo = JurisdictionBusiness.GetGroupById(long.Parse(EncryptHelper.DesDecrypt(groupId)));
            if (groupInfo != null)
            {
                model.GroupId = groupId;
                model.GroupName = groupInfo.BGName;
                var relationinfo = JurisdictionBusiness.GetUserRoleRelationByGroupId(long.Parse(EncryptHelper.DesDecrypt(groupId)));
                if (relationinfo != null)
                {
                    foreach (var rolegroup in relationinfo)
                    {
                        var userInfo = HomeBusiness.GetUserById(rolegroup.BURUserId);
                        if (userInfo != null)
                        {
                            var dic = new UserDic();
                            dic.UserName = userInfo.BUName;
                            dic.userId = EncryptHelper.DesEncrypt(userInfo.Id.ToString());
                            dic.JobNumber = userInfo.BUJobNumber;
                            model.UserList.Add(dic);
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRoleCode()
        {
            //判断是否是管理员
            if (HomeBusiness.IsAdmin(LoginUser.UserId))
            {
                return Json("isadmin", JsonRequestBehavior.AllowGet);
            }
            var list = JurisdictionBusiness.GetAllRoleCodeByUserid(LoginUser);
            if (list != null)
            {
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(string.Empty, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetGroupCode()
        {
            var list = JurisdictionBusiness.GetAllGroupCodeByUserid(LoginUser);
            if (list != null)
            {
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(string.Empty, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult JurisdictionUser(UserSearchViewModel search)
        {
            var total = 0;
            var result = HomeBusiness.SearchUser(search, out total);

            var page = new Page(total, search.CurrentPage);

            var resultModel = new UserSearchRsultModel
            {
                Models = result,
                Page = page
            };
            return View(resultModel);
        }

        /// <summary>
        /// 描述：判断当前登录人是否有某权限
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        //public ActionResult IfHasRole(string roleCode)
        //{
        //    var ifhasrole = false;
        //    var allRole = LoginHelper.GetAllRoleCode();
        //    ifhasrole= allRole.Contains(roleCode);
        //    return Json(ifhasrole);
        //}
    }
}