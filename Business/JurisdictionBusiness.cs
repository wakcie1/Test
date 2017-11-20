using Common;
using Common.Costant;
using Common.Enum;
using DataAccess;
using Model.CommonModel;
using Model.Home;
using Model.TableModel;
using Model.ViewModel.Jurisdiction;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Business
{
    public class JurisdictionBusiness
    {
        private static UserDAL _userDal = new UserDAL();
        private static UserRoleRelationDAL _userRoleRelationDal = new UserRoleRelationDAL();
        private static GroupDAL _groupDal = new GroupDAL();
        private static RoleGroupDAL _roleGroupDal = new RoleGroupDAL();
        private static RoleDAL _roleDal = new RoleDAL();

        /// <summary>
        /// 根据用户id获取权限分配情况
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static List<UserRoleRelationModel> GetUserRoleRelationByUserId(long userid)
        {
            return _userRoleRelationDal.GetUserRoleRelationByUserId(userid);
        }

        public static List<UserRoleRelationModel> GetUserRoleRelationByGroupId(long groupId)
        {
            return _userRoleRelationDal.GetUserRoleRelationByGroupId(groupId);
        }

        public static List<UserGroupModel> GetUserGroupListByUserId(long userid)
        {
            return _userRoleRelationDal.GetUserGroupListByUserId(userid);
        }

        /// <summary>
        /// 根据Id获取角色包信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static GroupModel GetGroupById(long id)
        {
            return _groupDal.GetGroupById(id);
        }

        /// <summary>
        /// 描述：根据用户Id获取所有的用户权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<long> GetAllRoleByUserid(long userId)
        {
            var list = new List<long>();
            var relation = GetUserRoleRelationByUserId(userId); //获取权限包
            if (relation != null && relation.Count > 0)
            {
                foreach (var rolegroup in relation)
                {
                    //根据权限包获取权限Id
                    var roleGroupInfo = _roleGroupDal.GetRoleGroupByGroupId(rolegroup.BURGroupId);
                    if (roleGroupInfo != null)
                    {
                        list.AddRange(roleGroupInfo.Select(x => x.BRGRoleId));
                    }
                }
            }
            return list.Distinct().ToList();
        }

        public static List<string> GetAllRoleCodeByUserid(UserLoginInfo loguser)
        {
            var list = new List<string>();
            var roleList = GetAllRoleByUserid(loguser.UserId);
            if (roleList != null)
            {
                foreach (var item in roleList)
                {
                    var roleInfo = _roleDal.GetRoleById(item);
                    if (roleInfo != null)
                    {
                        list.Add(roleInfo.BRCode);
                    }
                }
            }
            return list;
        }

        public static List<string> GetAllGroupCodeByUserid(UserLoginInfo loguser)
        {
            var list = new List<string>();
            var groupList = GetUserGroupListByUserId(loguser.UserId);
            if (groupList != null)
            {
                foreach (var item in groupList)
                {
                    list.Add(item.GroupCode);
                }
            }
            return list;
        }

        public static List<long> GetAllRoleByJobNumber(string jobNumber)
        {
            var list = new List<long>();
            var userinfo = _userDal.GetUserByJobNumber(jobNumber);
            if (userinfo != null && userinfo.Count > 0)
            {
                list = GetAllRoleByUserid(userinfo[0].Id);
            }
            return list;
        }

        /// <summary>
        /// 判断某人是否拥有某权限
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public bool IfHasRole(long userid, long roleid)
        {
            var allRole = GetAllRoleByUserid(userid);
            return allRole.Contains(roleid);
        }

        /// <summary>
        /// 描述：获取所有的角色包
        /// </summary>
        /// <returns></returns>
        public static List<GroupModel> GetAllGroup()
        {
            return _groupDal.GetAllGroup();
        }

        public static ResultInfoModel SaveRoleGroupRelation(SaveModel model, UserLoginInfo loguser)
        {
            var result = new ResultInfoModel { IsSuccess = false };
            var userid = long.Parse(EncryptHelper.DesDecrypt(model.UserId));
            using (var con = new SqlConnection(NewSqlHelper.ConnectionString))
            {
                con.Open();
                var tran = con.BeginTransaction();
                try
                {
                    //todo 根据userId清空所有权限
                    _userRoleRelationDal.DeleteRelationByUserid(userid, tran);
                    for (int i = 0; i < model.RoleGroupIds.Count; i++)
                    {
                        var relationModel = new UserRoleRelationModel
                        {
                            BURUserId = userid,
                            BURGroupId = long.Parse(EncryptHelper.DesDecrypt(model.RoleGroupIds[i])),
                            BURIsValid = EnabledEnum.Enabled.GetHashCode(),
                            BURCreateUserId = loguser.JobNum,
                            BURCreateUserName = loguser.UserName,
                            BURCreateTime = DateTime.Now,
                            BUROperateUserId = loguser.JobNum,
                            BUROperateUserName = loguser.UserName,
                            BUROperateTime = DateTime.Now
                        };
                        //插入数据
                        _userRoleRelationDal.InsertRelation(relationModel, tran);
                    }

                    result.IsSuccess = true;
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    tran.Rollback();
                }
                return result;
            }
        }

        public static ResultInfoModel SaveRoleGroupRelationByGroup(SaveByGroupIdModel model, UserLoginInfo loguser)
        {
            var result = new ResultInfoModel { IsSuccess = false };
            var groupId = long.Parse(EncryptHelper.DesDecrypt(model.GroupId));
            using (var con = new SqlConnection(NewSqlHelper.ConnectionString))
            {
                con.Open();
                var tran = con.BeginTransaction();
                try
                {
                    //todo 根据userId清空所有权限
                    _userRoleRelationDal.DeleteRelationByGroupid(groupId, tran);
                    for (int i = 0; i < model.UserIds.Count; i++)
                    {
                        var relationModel = new UserRoleRelationModel
                        {
                            BURUserId = long.Parse(EncryptHelper.DesDecrypt(model.UserIds[i])),
                            BURGroupId = groupId,
                            BURIsValid = EnabledEnum.Enabled.GetHashCode(),
                            BURCreateUserId = loguser.JobNum,
                            BURCreateUserName = loguser.UserName,
                            BURCreateTime = DateTime.Now,
                            BUROperateUserId = loguser.JobNum,
                            BUROperateUserName = loguser.UserName,
                            BUROperateTime = DateTime.Now
                        };
                        //插入数据
                        _userRoleRelationDal.InsertRelation(relationModel, tran);
                    }

                    result.IsSuccess = true;
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    tran.Rollback();
                }

                return result;
            }
        }

        public static RoleModel GetRoleByRoleName(string rolename)
        {
            return _roleDal.GetRoleByName(rolename);
        }

        public static GroupModel GetGroupByGroupName(string groupname)
        {
            return _groupDal.GetGroupByGroupName(groupname);
        }

        public static ResultInfoModel DiRoleToGroup(long roleId, long groupId)
        {
            var result = new ResultInfoModel { IsSuccess = false };
            try
            {
                var model = new RoleGroupModel();
                model.BRGRoleId = roleId;
                model.BRGGroupId = groupId;
                model.BRGIsValid = EnabledEnum.Enabled.GetHashCode();
                _roleGroupDal.InsertRoleGroup(model);
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public static List<RoleView> SearchRole(RoleSearchViewModel search, out int totalCount)
        {
            var result = _groupDal.SearchRole(search, out totalCount);
            if (result != null && result.Count > 0)
            {
                foreach (var item in result)
                {
                    item.RoleId = EncryptHelper.DesEncrypt(item.RoleId);
                }
            }
            return result;
        }

        public static ResultInfoModel InititalPageRole()
        {
            var result = new ResultInfoModel { IsSuccess = false };
            using (var con = new SqlConnection(NewSqlHelper.ConnectionString))
            {
                con.Open();
                var tran = con.BeginTransaction();
                try
                {
                    var suModel = new RoleModel
                    {
                        BRCode = "SET_USER",
                        BRName = "SET_USER",
                        BRType = RoleEnum.Page.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(suModel, tran); //SET_USER

                    var sdModel = new RoleModel
                    {
                        BRCode = "SET_DEPARTMENT",
                        BRName = "SET_DEPARTMENT",
                        BRType = RoleEnum.Page.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(sdModel, tran); //SET_DEPARTMENT

                    var ssModel = new RoleModel
                    {
                        BRCode = "SET_SAP",
                        BRName = "SET_SAP",
                        BRType = RoleEnum.Page.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(ssModel, tran); //SET_SAP

                    var smModel = new RoleModel
                    {
                        BRCode = "SET_MATOOL",
                        BRName = "SET_MATOOL",
                        BRType = RoleEnum.Page.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(smModel, tran); //SET_MATOOL

                    var srModel = new RoleModel
                    {
                        BRCode = "SET_ROOL",
                        BRName = "SET_ROOL",
                        BRType = RoleEnum.Page.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(srModel, tran); //SET_ROOL

                    var scdModel = new RoleModel
                    {
                        BRCode = "SET_CODE",
                        BRName = "SET_CODE",
                        BRType = RoleEnum.Page.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(scdModel, tran); //SET_CODE

                    var homeModel = new RoleModel
                    {
                        BRCode = "HOME",
                        BRName = "HOME",
                        BRType = RoleEnum.Page.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(homeModel, tran); //HOME

                    var createModel = new RoleModel
                    {
                        BRCode = "CREATE",
                        BRName = "CREATE",
                        BRType = RoleEnum.Page.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(createModel, tran); //CREATE

                    var searchModel = new RoleModel
                    {
                        BRCode = "SEARCH",
                        BRName = "SEARCH",
                        BRType = RoleEnum.Page.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(searchModel, tran); //SEARCH

                    var tcModel = new RoleModel
                    {
                        BRCode = "TIME_TRACKING",
                        BRName = "TIME_TRACKING",
                        BRType = RoleEnum.Page.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(tcModel, tran); //TIME_TRACKING

                    var suggestModel = new RoleModel
                    {
                        BRCode = "SUGGESTIONS",
                        BRName = "SUGGESTIONS",
                        BRType = RoleEnum.Page.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(suggestModel, tran); //SUGGESTIONS

                    var reportModel = new RoleModel
                    {
                        BRCode = "REPORT",
                        BRName = "REPORT",
                        BRType = RoleEnum.Page.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(reportModel, tran); //reportModel

                    var managementReport = new RoleModel
                    {
                        BRCode = "MANAGEMENTREPORT",
                        BRName = "MANAGEMENTREPORT",
                        BRType = RoleEnum.Page.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(managementReport, tran); //ManagementReport
                    result.IsSuccess = true;
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    tran.Rollback();
                }
                return result;
            }
        }

        public static ResultInfoModel InititalProblemRole()
        {
            var result = new ResultInfoModel { IsSuccess = false };
            using (var con = new SqlConnection(NewSqlHelper.ConnectionString))
            {
                con.Open();
                var tran = con.BeginTransaction();
                try
                {
                    var BTN_PROBLEM_SUBMIT = new RoleModel
                    {
                        BRCode = "BTN_PROBLEM_SUBMIT",
                        BRName = "BTN_PROBLEM_SUBMIT",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_PROBLEM_SUBMIT, tran);

                    var BTN_PROBLEM_APPROVE = new RoleModel
                    {
                        BRCode = "BTN_PROBLEM_APPROVE",
                        BRName = "BTN_PROBLEM_APPROVE",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_PROBLEM_APPROVE, tran);

                    var BTN_PROBLEM_REJECT = new RoleModel
                    {
                        BRCode = "BTN_PROBLEM_REJECT",
                        BRName = "BTN_PROBLEM_REJECT",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_PROBLEM_REJECT, tran);

                    var BTN_PROBLEM_CLOSE2 = new RoleModel
                    {
                        BRCode = "BTN_PROBLEM_CLOSE2",
                        BRName = "BTN_PROBLEM_CLOSE2",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_PROBLEM_CLOSE2, tran);

                    var BTN_PROBLEM_CLOSE3 = new RoleModel
                    {
                        BRCode = "BTN_PROBLEM_CLOSE3",
                        BRName = "BTN_PROBLEM_CLOSE3",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_PROBLEM_CLOSE3, tran);

                    var BTN_END_APPROVE = new RoleModel
                    {
                        BRCode = "BTN_END_APPROVE",
                        BRName = "BTN_END_APPROVE",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_END_APPROVE, tran);

                    result.IsSuccess = true;
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    tran.Rollback();
                }
                return result;
            }
        }

        public static ResultInfoModel InititalSolvingteamRole()
        {
            var result = new ResultInfoModel { IsSuccess = false };
            using (var con = new SqlConnection(NewSqlHelper.ConnectionString))
            {
                con.Open();
                var tran = con.BeginTransaction();
                try
                {
                    var BTN_SOLVTEAM_ADD = new RoleModel
                    {
                        BRCode = "BTN_SOLVTEAM_ADD",
                        BRName = "BTN_SOLVTEAM_ADD",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_SOLVTEAM_ADD, tran);

                    var BTN_SOLVTEAM_APPROVE = new RoleModel
                    {
                        BRCode = "BTN_SOLVTEAM_APPROVE",
                        BRName = "BTN_SOLVTEAM_APPROVE",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_SOLVTEAM_APPROVE, tran);

                    var BTN_SOLVTEAM_REJECT = new RoleModel
                    {
                        BRCode = "BTN_SOLVTEAM_REJECT",
                        BRName = "BTN_SOLVTEAM_REJECT",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_SOLVTEAM_REJECT, tran);

                    result.IsSuccess = true;
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    tran.Rollback();
                }
                return result;
            }
        }

        public static ResultInfoModel InititalQualityalertRole()
        {
            var result = new ResultInfoModel { IsSuccess = false };
            using (var con = new SqlConnection(NewSqlHelper.ConnectionString))
            {
                con.Open();
                var tran = con.BeginTransaction();
                try
                {
                    var BTN_QUALALERT_ADD = new RoleModel
                    {
                        BRCode = "BTN_QUALALERT_ADD",
                        BRName = "BTN_QUALALERT_ADD",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_QUALALERT_ADD, tran);

                    var BTN_QUALALERT_APPROVE = new RoleModel
                    {
                        BRCode = "BTN_QUALALERT_APPROVE",
                        BRName = "BTN_QUALALERT_APPROVE",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_QUALALERT_APPROVE, tran);

                    var BTN_QUALALERT_REJECT = new RoleModel
                    {
                        BRCode = "BTN_QUALALERT_REJECT",
                        BRName = "BTN_QUALALERT_REJECT",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_QUALALERT_REJECT, tran);

                    result.IsSuccess = true;
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    tran.Rollback();
                }
                return result;
            }
        }

        public static ResultInfoModel InititalSortingactivityRole()
        {
            var result = new ResultInfoModel { IsSuccess = false };
            using (var con = new SqlConnection(NewSqlHelper.ConnectionString))
            {
                con.Open();
                var tran = con.BeginTransaction();
                try
                {
                    var BTN_SORTACT_EDIT = new RoleModel
                    {
                        BRCode = "BTN_SORTACT_EDIT",
                        BRName = "BTN_SORTACT_EDIT",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_SORTACT_EDIT, tran);

                    var BTN_SORTACT_APPROVE = new RoleModel
                    {
                        BRCode = "BTN_SORTACT_APPROVE",
                        BRName = "BTN_SORTACT_APPROVE",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_SORTACT_APPROVE, tran);

                    var BTN_SORTACT_REJECT = new RoleModel
                    {
                        BRCode = "BTN_SORTACT_REJECT",
                        BRName = "BTN_SORTACT_REJECT",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_SORTACT_REJECT, tran);

                    result.IsSuccess = true;
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    tran.Rollback();
                }
                return result;
            }
        }

        public static ResultInfoModel InititalContainmentactionRole()
        {
            var result = new ResultInfoModel { IsSuccess = false };
            using (var con = new SqlConnection(NewSqlHelper.ConnectionString))
            {
                con.Open();
                var tran = con.BeginTransaction();
                try
                {
                    var BTN_CONTAIN_ADD = new RoleModel
                    {
                        BRCode = "BTN_CONTAIN_ADD",
                        BRName = "BTN_CONTAIN_ADD",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_CONTAIN_ADD, tran);

                    var BTN_CONTAIN_APPROVE1 = new RoleModel
                    {
                        BRCode = "BTN_CONTAIN_APPROVE1",
                        BRName = "BTN_CONTAIN_APPROVE1",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_CONTAIN_APPROVE1, tran);

                    var BTN_CONTAIN_APPROVE2 = new RoleModel
                    {
                        BRCode = "BTN_CONTAIN_APPROVE2",
                        BRName = "BTN_CONTAIN_APPROVE2",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_CONTAIN_APPROVE2, tran);

                    var BTN_CONTAIN_REJECT = new RoleModel
                    {
                        BRCode = "BTN_CONTAIN_REJECT",
                        BRName = "BTN_CONTAIN_REJECT",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_CONTAIN_REJECT, tran);

                    result.IsSuccess = true;
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    tran.Rollback();
                }
                return result;
            }
        }

        public static ResultInfoModel InititalFactanalyRole()
        {
            var result = new ResultInfoModel { IsSuccess = false };
            using (var con = new SqlConnection(NewSqlHelper.ConnectionString))
            {
                con.Open();
                var tran = con.BeginTransaction();
                try
                {
                    var BTN_FACTANALY_ADD = new RoleModel
                    {
                        BRCode = "BTN_FACTANALY_ADD",
                        BRName = "BTN_FACTANALY_ADD",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_FACTANALY_ADD, tran);

                    result.IsSuccess = true;
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    tran.Rollback();
                }
                return result;
            }
        }

        public static ResultInfoModel InititalWhyAnalyRole()
        {
            var result = new ResultInfoModel { IsSuccess = false };
            using (var con = new SqlConnection(NewSqlHelper.ConnectionString))
            {
                con.Open();
                var tran = con.BeginTransaction();
                try
                {
                    var BTN_WHYANALY_ADD = new RoleModel
                    {
                        BRCode = "BTN_WHYANALY_ADD",
                        BRName = "BTN_WHYANALY_ADD",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_WHYANALY_ADD, tran);

                    var BTN_WHYANALY_APPROVE = new RoleModel
                    {
                        BRCode = "BTN_WHYANALY_APPROVE",
                        BRName = "BTN_WHYANALY_APPROVE",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_WHYANALY_APPROVE, tran);

                    var BTN_WHYANALY_REJECT = new RoleModel
                    {
                        BRCode = "BTN_WHYANALY_REJECT",
                        BRName = "BTN_WHYANALY_REJECT",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_WHYANALY_REJECT, tran);

                    result.IsSuccess = true;
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    tran.Rollback();
                }
                return result;
            }
        }

        public static ResultInfoModel InititalCorrectiveActionRole()
        {
            var result = new ResultInfoModel { IsSuccess = false };
            using (var con = new SqlConnection(NewSqlHelper.ConnectionString))
            {
                con.Open();
                var tran = con.BeginTransaction();
                try
                {
                    var BTN_CORRECT_ADD = new RoleModel
                    {
                        BRCode = "BTN_CORRECT_ADD",
                        BRName = "BTN_CORRECT_ADD",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_CORRECT_ADD, tran);

                    var BTN_CORRECT_APPROVE = new RoleModel
                    {
                        BRCode = "BTN_CORRECT_APPROVE",
                        BRName = "BTN_CORRECT_APPROVE",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_CORRECT_APPROVE, tran);

                    var BTN_CORRECT_REJECT = new RoleModel
                    {
                        BRCode = "BTN_CORRECT_REJECT",
                        BRName = "BTN_CORRECT_REJECT",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_CORRECT_REJECT, tran);

                    result.IsSuccess = true;
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    tran.Rollback();
                }
                return result;
            }
        }

        public static ResultInfoModel InititalPreventiveMeasuresRole()
        {
            var result = new ResultInfoModel { IsSuccess = false };
            using (var con = new SqlConnection(NewSqlHelper.ConnectionString))
            {
                con.Open();
                var tran = con.BeginTransaction();
                try
                {
                    var BTN_PREMEASURE_ADD = new RoleModel
                    {
                        BRCode = "BTN_PREMEASURE_ADD",
                        BRName = "BTN_PREMEASURE_ADD",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_PREMEASURE_ADD, tran);

                    var BTN_PREMEASURE_APPROVE = new RoleModel
                    {
                        BRCode = "BTN_PREMEASURE_APPROVE",
                        BRName = "BTN_PREMEASURE_APPROVE",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_PREMEASURE_APPROVE, tran);

                    var BTN_PREMEASURE_REJECT = new RoleModel
                    {
                        BRCode = "BTN_PREMEASURE_REJECT",
                        BRName = "BTN_PREMEASURE_REJECT",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_PREMEASURE_REJECT, tran);

                    result.IsSuccess = true;
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    tran.Rollback();
                }
                return result;
            }
        }

        public static ResultInfoModel InititalLayeredAuditRole()
        {
            var result = new ResultInfoModel { IsSuccess = false };
            using (var con = new SqlConnection(NewSqlHelper.ConnectionString))
            {
                con.Open();
                var tran = con.BeginTransaction();
                try
                {
                    var BTN_LAYERDAUDIT_ADD = new RoleModel
                    {
                        BRCode = "BTN_LAYERDAUDIT_ADD",
                        BRName = "BTN_LAYERDAUDIT_ADD",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_LAYERDAUDIT_ADD, tran);

                    var BTN_LAYERDAUDIT_APPROVE = new RoleModel
                    {
                        BRCode = "BTN_LAYERDAUDIT_APPROVE",
                        BRName = "BTN_LAYERDAUDIT_APPROVE",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_LAYERDAUDIT_APPROVE, tran);

                    var BTN_LAYERDAUDIT_REJECT = new RoleModel
                    {
                        BRCode = "BTN_LAYERDAUDIT_REJECT",
                        BRName = "BTN_LAYERDAUDIT_REJECT",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_LAYERDAUDIT_REJECT, tran);

                    result.IsSuccess = true;
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    tran.Rollback();
                }
                return result;
            }
        }

        public static ResultInfoModel InititalVerificationRole()
        {
            var result = new ResultInfoModel { IsSuccess = false };
            using (var con = new SqlConnection(NewSqlHelper.ConnectionString))
            {
                con.Open();
                var tran = con.BeginTransaction();
                try
                {
                    var BTN_VERIFICATION_ADD = new RoleModel
                    {
                        BRCode = "BTN_VERIFICATION_ADD",
                        BRName = "BTN_VERIFICATION_ADD",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_VERIFICATION_ADD, tran);

                    var BTN_VERIFICATION_APPROVE = new RoleModel
                    {
                        BRCode = "BTN_VERIFICATION_APPROVE",
                        BRName = "BTN_VERIFICATION_APPROVE",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_VERIFICATION_APPROVE, tran);

                    var BTN_VERIFICATION_REJECT = new RoleModel
                    {
                        BRCode = "BTN_VERIFICATION_REJECT",
                        BRName = "BTN_VERIFICATION_REJECT",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_VERIFICATION_REJECT, tran);

                    result.IsSuccess = true;
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    tran.Rollback();
                }
                return result;
            }
        }

        public static ResultInfoModel InititalStandardizationRole()
        {
            var result = new ResultInfoModel { IsSuccess = false };
            using (var con = new SqlConnection(NewSqlHelper.ConnectionString))
            {
                con.Open();
                var tran = con.BeginTransaction();
                try
                {
                    var BTN_STANDA_EDIT = new RoleModel
                    {
                        BRCode = "BTN_STANDA_EDIT",
                        BRName = "BTN_STANDA_EDIT",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_STANDA_EDIT, tran);

                    var BTN_STANDA_APPROVE = new RoleModel
                    {
                        BRCode = "BTN_STANDA_APPROVE",
                        BRName = "BTN_STANDA_APPROVE",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_STANDA_APPROVE, tran);

                    var BTN_STANDA_REJECT = new RoleModel
                    {
                        BRCode = "BTN_STANDA_REJECT",
                        BRName = "BTN_STANDA_REJECT",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_STANDA_REJECT, tran);

                    var BTN_STANDA_ADD = new RoleModel
                    {
                        BRCode = "BTN_STANDA_ADD",
                        BRName = "BTN_STANDA_ADD",
                        BRType = RoleEnum.Function.GetHashCode(),
                        BRIsValid = EnabledEnum.Enabled.GetHashCode()
                    };
                    _roleDal.InsertRole(BTN_STANDA_ADD, tran);

                    result.IsSuccess = true;
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    tran.Rollback();
                }
                return result;
            }
        }

        public static ResultInfoModel InitialPageGroup()
        {
            var result = new ResultInfoModel { IsSuccess = false };
            using (var con = new SqlConnection(NewSqlHelper.ConnectionString))
            {
                con.Open();
                var tran = con.BeginTransaction();
                try
                {
                    var shiftModel = new GroupModel
                    {
                        BGCode = "Shift leader",
                        BGName = "Shift leader",
                        BGIsValid = EnabledEnum.Enabled.GetHashCode(),
                    };
                    var shifId = _groupDal.InsertGroup(shiftModel, tran);


                    var qeModel = new GroupModel
                    {
                        BGCode = "QE",
                        BGName = "QE",
                        BGIsValid = EnabledEnum.Enabled.GetHashCode(),
                    };
                    var qeId = _groupDal.InsertGroup(qeModel, tran);

                    var responsibleModel = new GroupModel
                    {
                        BGCode = "Responsible",
                        BGName = "Responsible",
                        BGIsValid = EnabledEnum.Enabled.GetHashCode(),
                    };
                    var responsibleId = _groupDal.InsertGroup(responsibleModel, tran);

                    var mlModel = new GroupModel
                    {
                        BGCode = "Management level",
                        BGName = "Management level",
                        BGIsValid = EnabledEnum.Enabled.GetHashCode(),
                    };
                    var mlId = _groupDal.InsertGroup(mlModel, tran);

                    var setModel = new GroupModel
                    {
                        BGCode = "Admin",
                        BGName = "Admin",
                        BGIsValid = EnabledEnum.Enabled.GetHashCode(),
                    };
                    var setId = _groupDal.InsertGroup(setModel, tran);
                    tran.Commit();

                    var homeInfo = JurisdictionBusiness.GetRoleByRoleName("HOME");
                    var createInfo = JurisdictionBusiness.GetRoleByRoleName("CREATE");
                    var searchInfo = JurisdictionBusiness.GetRoleByRoleName("SEARCH");
                    var timeInfo = JurisdictionBusiness.GetRoleByRoleName("TIME_TRACKING");
                    var suggestInfo = JurisdictionBusiness.GetRoleByRoleName("SUGGESTIONS");
                    var reportInfo = JurisdictionBusiness.GetRoleByRoleName("REPORT");
                    var managementReportInfo = JurisdictionBusiness.GetRoleByRoleName("MANAGEMENTREPORT");
                    var suInfo = JurisdictionBusiness.GetRoleByRoleName("SET_USER");
                    var sdInfo = JurisdictionBusiness.GetRoleByRoleName("SET_DEPARTMENT");
                    var ssInfo = JurisdictionBusiness.GetRoleByRoleName("SET_SAP");
                    var smInfo = JurisdictionBusiness.GetRoleByRoleName("SET_MATOOL");
                    var srInfo = JurisdictionBusiness.GetRoleByRoleName("SET_ROOL");
                    var scdInfo = JurisdictionBusiness.GetRoleByRoleName("SET_CODE");
                    if (homeInfo != null && createInfo != null && searchInfo != null && timeInfo != null && suggestInfo != null && reportInfo != null && managementReportInfo != null)
                    {
                        DiRoleToGroup(homeInfo.Id, shifId);
                        DiRoleToGroup(createInfo.Id, shifId);
                        DiRoleToGroup(searchInfo.Id, shifId);
                        DiRoleToGroup(timeInfo.Id, shifId);
                        DiRoleToGroup(suggestInfo.Id, shifId);

                        DiRoleToGroup(homeInfo.Id, qeId);
                        DiRoleToGroup(createInfo.Id, qeId);
                        DiRoleToGroup(searchInfo.Id, qeId);
                        DiRoleToGroup(timeInfo.Id, qeId);
                        DiRoleToGroup(suggestInfo.Id, qeId);
                        DiRoleToGroup(reportInfo.Id, qeId);
                        DiRoleToGroup(managementReportInfo.Id, qeId);

                        DiRoleToGroup(homeInfo.Id, responsibleId);
                        DiRoleToGroup(createInfo.Id, responsibleId);
                        DiRoleToGroup(searchInfo.Id, responsibleId);
                        DiRoleToGroup(timeInfo.Id, responsibleId);
                        DiRoleToGroup(suggestInfo.Id, responsibleId);

                        DiRoleToGroup(homeInfo.Id, mlId);
                        DiRoleToGroup(createInfo.Id, mlId);
                        DiRoleToGroup(searchInfo.Id, mlId);
                        DiRoleToGroup(timeInfo.Id, mlId);
                        DiRoleToGroup(suggestInfo.Id, mlId);
                        DiRoleToGroup(reportInfo.Id, mlId);
                        DiRoleToGroup(managementReportInfo.Id, mlId);

                        DiRoleToGroup(homeInfo.Id, setId);
                        DiRoleToGroup(createInfo.Id, setId);
                        DiRoleToGroup(searchInfo.Id, setId);
                        DiRoleToGroup(timeInfo.Id, setId);
                        DiRoleToGroup(suggestInfo.Id, setId);
                    }

                    if (suInfo != null && sdInfo != null && ssInfo != null && smInfo != null && srInfo != null && scdInfo != null)
                    {
                        DiRoleToGroup(suInfo.Id, setId);
                        DiRoleToGroup(sdInfo.Id, setId);
                        DiRoleToGroup(ssInfo.Id, setId);
                        DiRoleToGroup(smInfo.Id, setId);
                        DiRoleToGroup(srInfo.Id, setId);
                        DiRoleToGroup(scdInfo.Id, setId);
                    }

                    var BTN_PROBLEM_SUBMIT = JurisdictionBusiness.GetRoleByRoleName("BTN_PROBLEM_SUBMIT");
                    if (BTN_PROBLEM_SUBMIT != null)
                    {
                        DiRoleToGroup(BTN_PROBLEM_SUBMIT.Id, shifId);
                        DiRoleToGroup(BTN_PROBLEM_SUBMIT.Id, qeId);
                    }
                    //var BTN_PROBLEM_APPROVE = JurisdictionBusiness.GetRoleByRoleName("BTN_PROBLEM_APPROVE");
                    //if (BTN_PROBLEM_APPROVE != null)
                    //{
                    //    DiRoleToGroup(BTN_PROBLEM_APPROVE.Id, qeId);
                    //}
                    //var BTN_PROBLEM_REJECT = JurisdictionBusiness.GetRoleByRoleName("BTN_PROBLEM_REJECT");
                    //if (BTN_PROBLEM_REJECT != null)
                    //{
                    //    DiRoleToGroup(BTN_PROBLEM_REJECT.Id, qeId);
                    //}
                    var BTN_PROBLEM_CLOSE2 = JurisdictionBusiness.GetRoleByRoleName("BTN_PROBLEM_CLOSE2");
                    if (BTN_PROBLEM_CLOSE2 != null)
                    {
                        DiRoleToGroup(BTN_PROBLEM_CLOSE2.Id, qeId);
                        DiRoleToGroup(BTN_PROBLEM_CLOSE2.Id, shifId);
                        DiRoleToGroup(BTN_PROBLEM_CLOSE2.Id, mlId);
                    }
                    var BTN_PROBLEM_CLOSE3 = JurisdictionBusiness.GetRoleByRoleName("BTN_PROBLEM_CLOSE3");
                    if (BTN_PROBLEM_CLOSE3 != null)
                    {
                        DiRoleToGroup(BTN_PROBLEM_CLOSE3.Id, qeId);
                        DiRoleToGroup(BTN_PROBLEM_CLOSE3.Id, mlId);
                    }

                    var BTN_SOLVTEAM_ADD = JurisdictionBusiness.GetRoleByRoleName("BTN_SOLVTEAM_ADD");
                    if (BTN_SOLVTEAM_ADD != null)
                    {
                        DiRoleToGroup(BTN_SOLVTEAM_ADD.Id, shifId);
                        DiRoleToGroup(BTN_SOLVTEAM_ADD.Id, qeId);
                    }
                    //var BTN_SOLVTEAM_APPROVE = JurisdictionBusiness.GetRoleByRoleName("BTN_SOLVTEAM_APPROVE");
                    //if (BTN_SOLVTEAM_APPROVE != null)
                    //{
                    //    DiRoleToGroup(BTN_SOLVTEAM_APPROVE.Id, qeId);
                    //}
                    //var BTN_SOLVTEAM_REJECT = JurisdictionBusiness.GetRoleByRoleName("BTN_SOLVTEAM_REJECT");
                    //if (BTN_SOLVTEAM_REJECT != null)
                    //{
                    //    DiRoleToGroup(BTN_SOLVTEAM_REJECT.Id, qeId);
                    //}

                    var BTN_QUALALERT_ADD = JurisdictionBusiness.GetRoleByRoleName("BTN_QUALALERT_ADD");
                    if (BTN_QUALALERT_ADD != null)
                    {
                        DiRoleToGroup(BTN_QUALALERT_ADD.Id, shifId);
                        DiRoleToGroup(BTN_QUALALERT_ADD.Id, qeId);
                    }
                    //var BTN_QUALALERT_APPROVE = JurisdictionBusiness.GetRoleByRoleName("BTN_QUALALERT_APPROVE");
                    //if (BTN_QUALALERT_APPROVE != null)
                    //{
                    //    DiRoleToGroup(BTN_QUALALERT_APPROVE.Id, qeId);
                    //}
                    //var BTN_QUALALERT_REJECT = JurisdictionBusiness.GetRoleByRoleName("BTN_QUALALERT_REJECT");
                    //if (BTN_QUALALERT_REJECT != null)
                    //{
                    //    DiRoleToGroup(BTN_QUALALERT_REJECT.Id, qeId);
                    //}

                    //var BTN_SORTACT_EDIT = JurisdictionBusiness.GetRoleByRoleName("BTN_SORTACT_EDIT");
                    //if (BTN_SORTACT_EDIT != null)
                    //{
                    //    DiRoleToGroup(BTN_SORTACT_EDIT.Id, shifId);
                    //}
                    //var BTN_SORTACT_APPROVE = JurisdictionBusiness.GetRoleByRoleName("BTN_SORTACT_APPROVE");
                    //if (BTN_SORTACT_APPROVE != null)
                    //{
                    //    DiRoleToGroup(BTN_SORTACT_APPROVE.Id, qeId);
                    //}
                    //var BTN_SORTACT_REJECT = JurisdictionBusiness.GetRoleByRoleName("BTN_SORTACT_REJECT");
                    //if (BTN_SORTACT_REJECT != null)
                    //{
                    //    DiRoleToGroup(BTN_SORTACT_REJECT.Id, qeId);
                    //}


                    var BTN_CONTAIN_ADD = JurisdictionBusiness.GetRoleByRoleName("BTN_CONTAIN_ADD");
                    if (BTN_CONTAIN_ADD != null)
                    {
                        DiRoleToGroup(BTN_CONTAIN_ADD.Id, shifId);
                        DiRoleToGroup(BTN_CONTAIN_ADD.Id, qeId);
                    }
                    var BTN_FACTANALY_ADD = JurisdictionBusiness.GetRoleByRoleName("BTN_FACTANALY_ADD");
                    if (BTN_FACTANALY_ADD != null)
                    {
                        DiRoleToGroup(BTN_FACTANALY_ADD.Id, shifId);
                        DiRoleToGroup(BTN_FACTANALY_ADD.Id, qeId);
                    }
                    var BTN_WHYANALY_ADD = JurisdictionBusiness.GetRoleByRoleName("BTN_WHYANALY_ADD");
                    if (BTN_WHYANALY_ADD != null)
                    {
                        DiRoleToGroup(BTN_WHYANALY_ADD.Id, shifId);
                        DiRoleToGroup(BTN_WHYANALY_ADD.Id, qeId);
                    }
                    //var BTN_WHYANALY_APPROVE = JurisdictionBusiness.GetRoleByRoleName("BTN_WHYANALY_APPROVE");
                    //if (BTN_WHYANALY_APPROVE != null)
                    //{
                    //    DiRoleToGroup(BTN_WHYANALY_APPROVE.Id, qeId);
                    //}
                    //var BTN_WHYANALY_REJECT = JurisdictionBusiness.GetRoleByRoleName("BTN_WHYANALY_REJECT");
                    //if (BTN_WHYANALY_REJECT != null)
                    //{
                    //    DiRoleToGroup(BTN_WHYANALY_REJECT.Id, qeId);
                    //}

                    var BTN_CORRECT_ADD = JurisdictionBusiness.GetRoleByRoleName("BTN_CORRECT_ADD");
                    if (BTN_CORRECT_ADD != null)
                    {
                        DiRoleToGroup(BTN_CORRECT_ADD.Id, shifId);
                        DiRoleToGroup(BTN_CORRECT_ADD.Id, qeId);
                    }
                    //var BTN_CORRECT_APPROVE = JurisdictionBusiness.GetRoleByRoleName("BTN_CORRECT_APPROVE");
                    //if (BTN_CORRECT_APPROVE != null)
                    //{
                    //    DiRoleToGroup(BTN_CORRECT_APPROVE.Id, qeId);
                    //}
                    //var BTN_CORRECT_REJECT = JurisdictionBusiness.GetRoleByRoleName("BTN_CORRECT_REJECT");
                    //if (BTN_CORRECT_REJECT != null)
                    //{
                    //    DiRoleToGroup(BTN_CORRECT_REJECT.Id, qeId);
                    //}

                    var BTN_PREMEASURE_ADD = JurisdictionBusiness.GetRoleByRoleName("BTN_PREMEASURE_ADD");
                    if (BTN_PREMEASURE_ADD != null)
                    {
                        DiRoleToGroup(BTN_PREMEASURE_ADD.Id, shifId);
                        DiRoleToGroup(BTN_PREMEASURE_ADD.Id, qeId);
                    }
                    var BTN_PREMEASURE_APPROVE = JurisdictionBusiness.GetRoleByRoleName("BTN_PREMEASURE_APPROVE");
                    if (BTN_PREMEASURE_APPROVE != null)
                    {
                        DiRoleToGroup(BTN_PREMEASURE_APPROVE.Id, qeId);
                    }
                    var BTN_PREMEASURE_REJECT = JurisdictionBusiness.GetRoleByRoleName("BTN_PREMEASURE_REJECT");
                    if (BTN_PREMEASURE_REJECT != null)
                    {
                        DiRoleToGroup(BTN_PREMEASURE_REJECT.Id, qeId);
                    }

                    var BTN_LAYERDAUDIT_ADD = JurisdictionBusiness.GetRoleByRoleName("BTN_LAYERDAUDIT_ADD");
                    if (BTN_LAYERDAUDIT_ADD != null)
                    {
                        DiRoleToGroup(BTN_LAYERDAUDIT_ADD.Id, qeId);
                    }
                    var BTN_LAYERDAUDIT_APPROVE = JurisdictionBusiness.GetRoleByRoleName("BTN_LAYERDAUDIT_APPROVE");
                    if (BTN_LAYERDAUDIT_APPROVE != null)
                    {
                        DiRoleToGroup(BTN_LAYERDAUDIT_APPROVE.Id, qeId);
                        DiRoleToGroup(BTN_LAYERDAUDIT_APPROVE.Id, mlId);
                    }
                    var BTN_LAYERDAUDIT_REJECT = JurisdictionBusiness.GetRoleByRoleName("BTN_LAYERDAUDIT_REJECT");
                    if (BTN_LAYERDAUDIT_REJECT != null)
                    {
                        DiRoleToGroup(BTN_LAYERDAUDIT_REJECT.Id, qeId);
                        DiRoleToGroup(BTN_LAYERDAUDIT_REJECT.Id, mlId);
                    }

                    var BTN_VERIFICATION_ADD = JurisdictionBusiness.GetRoleByRoleName("BTN_VERIFICATION_ADD");
                    if (BTN_VERIFICATION_ADD != null)
                    {
                        DiRoleToGroup(BTN_VERIFICATION_ADD.Id, qeId);
                        DiRoleToGroup(BTN_VERIFICATION_ADD.Id, mlId);
                    }
                    var BTN_VERIFICATION_APPROVE = JurisdictionBusiness.GetRoleByRoleName("BTN_VERIFICATION_APPROVE");
                    if (BTN_VERIFICATION_APPROVE != null)
                    {
                        DiRoleToGroup(BTN_VERIFICATION_APPROVE.Id, qeId);
                        DiRoleToGroup(BTN_VERIFICATION_APPROVE.Id, mlId);
                    }
                    var BTN_VERIFICATION_REJECT = JurisdictionBusiness.GetRoleByRoleName("BTN_VERIFICATION_REJECT");
                    if (BTN_VERIFICATION_REJECT != null)
                    {
                        DiRoleToGroup(BTN_VERIFICATION_REJECT.Id, qeId);
                        DiRoleToGroup(BTN_VERIFICATION_REJECT.Id, mlId);
                    }

                    var BTN_STANDA_ADD = JurisdictionBusiness.GetRoleByRoleName("BTN_STANDA_ADD");
                    if (BTN_STANDA_ADD != null)
                    {
                        DiRoleToGroup(BTN_STANDA_ADD.Id, qeId);
                    }
                    var BTN_STANDA_EDIT = JurisdictionBusiness.GetRoleByRoleName("BTN_STANDA_EDIT");
                    if (BTN_STANDA_EDIT != null)
                    {
                        DiRoleToGroup(BTN_STANDA_EDIT.Id, qeId);
                    }
                    var BTN_STANDA_APPROVE = JurisdictionBusiness.GetRoleByRoleName("BTN_STANDA_APPROVE");
                    if (BTN_STANDA_APPROVE != null)
                    {
                        DiRoleToGroup(BTN_STANDA_APPROVE.Id, qeId);
                        DiRoleToGroup(BTN_STANDA_APPROVE.Id, mlId);
                    }
                    var BTN_STANDA_REJECT = JurisdictionBusiness.GetRoleByRoleName("BTN_STANDA_REJECT");
                    if (BTN_STANDA_REJECT != null)
                    {
                        DiRoleToGroup(BTN_STANDA_REJECT.Id, qeId);
                        DiRoleToGroup(BTN_STANDA_REJECT.Id, mlId);
                    }
                    var BTN_END_APPROVE = JurisdictionBusiness.GetRoleByRoleName("BTN_END_APPROVE");
                    if (BTN_END_APPROVE != null)
                    {
                        DiRoleToGroup(BTN_END_APPROVE.Id, qeId);
                        DiRoleToGroup(BTN_END_APPROVE.Id, mlId);
                    }

                    result.IsSuccess = true;
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    tran.Rollback();
                }
            }
            return result;
        }
    }
}
