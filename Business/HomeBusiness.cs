using System;
using Common.Enum;
using DataAccess;
using Model.Home;
using Common;
using Common.Costant;
using Model.TableModel;
using System.Collections.Generic;
using System.Linq;
using Model.ViewModel.Department;
using Model.CommonModel;
using System.Data.SqlClient;
using Common.Enum.User;
using Model.ViewModel.User;
using System.Configuration;
using Model.ViewModel.Jurisdiction;

namespace Business
{
    public class HomeBusiness
    {
        private static HomeDAL _homeDal = new HomeDAL();
        private static AccountDAL _accountDal = new AccountDAL();
        private static UserDAL _userdal = new UserDAL();
        private static DepartmentDAL _departmentDal = new DepartmentDAL();

        public static bool UserLogin(string loginName, string password, string isAdmin, out string workNo)
        {
            return _homeDal.UserLogin(loginName, password, isAdmin, out workNo);
        }

        public static UserLoginInfo Login(string account, string password)
        {
            var userInfo = new UserLoginInfo();
            //根据账号密码获取用户id
            //密码需要加密？？
            var accountInfo = _accountDal.GetAccountByAccount(account);
            if (accountInfo == null)
            {
                throw new Exception("The account you entered does not exist. Please register first!");
            }
            //验证密码是否正确
            if (EncryptHelper.DesDecrypt(accountInfo.BAPassword) != password)
            {
                throw new Exception("Username or password incorrect");
            }
            //if (accountInfo.BAPassword != password)  //注册完成之后使用上面的代码解密密码
            //{
            //    throw new Exception("Username or password incorrect");
            //}
            if (accountInfo.BAIsValid == EnabledEnum.UnEnabled.GetHashCode())
            {
                throw new Exception("The account is invalid");
            }
            var user = _userdal.GetUserById(accountInfo.BAUserId);
            if (user != null)
            {
                if (user.BUIsValid == EnabledEnum.UnEnabled.GetHashCode())
                {
                    throw new Exception("The account is invalid");
                }
                userInfo.UserId = accountInfo.BAUserId;
                userInfo.Avatars = user.BUAvatars;
                if (!string.IsNullOrEmpty(userInfo.Avatars))
                { userInfo.AvatarsUrl = UploadHelper.GetDownLoadUrl(userInfo.Avatars); }
                userInfo.PhoneNum = user.BUPhoneNum;
                userInfo.DepartId = user.BUDepartId;
                userInfo.AccountType = accountInfo.BAType;
                userInfo.Account = accountInfo.BAAccount;
                userInfo.UserName = user.BUName;
                userInfo.JobNum = user.BUJobNumber;
                userInfo.Position = user.BUTitle;
                userInfo.Email = user.BUEmail;
                userInfo.SuperAdmin = IsAdmin(user.Id);
                if (user.BUDepartId > 0)
                {
                    var departmentInfo = _departmentDal.GetDpById(user.BUDepartId);
                    if (departmentInfo != null)
                    {
                        userInfo.DepartName = departmentInfo.BDDeptName;
                    }
                }
            }
            return userInfo;
        }

        #region 部门相关
        /// <summary>
        /// 描述：获取根部门
        /// 创建标识：cpf
        /// 创建时间：2017-9-19 16:08:35
        /// </summary>
        /// <returns></returns>
        public static DepartView GetRootDepartment()
        {
            var dpView = new DepartView();
            var depar = _departmentDal.GetRootDepartment();
            if (depar != null)
            {
                dpView.DesId = EncryptHelper.DesEncrypt(depar.Id.ToString());
                dpView.BDDeptName = depar.BDDeptName;
                dpView.DesParentId = EncryptHelper.DesEncrypt(depar.BDParentId.ToString());
            }
            return dpView;
        }

        /// <summary>
        /// 描述：获取组织架构信息
        /// 创建标识：cpf
        /// 创建时间：2017-9-19 20:31:07
        /// </summary>
        /// <returns></returns>
        public static OrganizationEntity GetAllDepartmentList()
        {
            var entity = new OrganizationEntity();

            var deplist = _departmentDal.GetAllDepartMent(); //所有的部门列表（有效无效的都包含）
            var firstdepart = deplist.FirstOrDefault(x => x.BDParentId == 0 & x.BDIsValid == EnabledEnum.Enabled.GetHashCode());
            var userList = _userdal.GetAllIsValidUser(); //所有有效的人员信息
            if (deplist.Count > 0 && userList.Count > 0)  //获取有效的部门梯队
            {
                entity = GetOrganizationTree(new OrganizationEntity(), userList, deplist, firstdepart.Id);
                entity.DepartmentId = EncryptHelper.DesEncrypt(firstdepart.Id.ToString());
                entity.DepartmentIsValid = (byte)firstdepart.BDIsValid;
                entity.DepartmentName = firstdepart.BDDeptName;
                entity.DepartmentUserNum = userList.Count;
                entity.ParentId = EncryptHelper.DesEncrypt("0");
            }
            var unValidList = deplist.Where(x => x.BDIsValid == EnabledEnum.UnEnabled.GetHashCode()).ToList();
            if (entity.DepartmentChildList == null)
            {
                entity.DepartmentChildList = new List<OrganizationEntity>();
            }
            if (firstdepart != null && unValidList != null)
            {
                var unValidEntity = new List<OrganizationEntity>();
                if (unValidList.Count > 0)
                {
                    unValidEntity.AddRange(unValidList.Select(item => new OrganizationEntity()
                    {
                        DepartmentId = EncryptHelper.DesEncrypt(item.Id.ToString()),
                        DepartmentIsValid = (byte)item.BDIsValid,
                        DepartmentName = item.BDDeptName,
                        DepartmentUserNum = 0,
                        ParentId = EncryptHelper.DesEncrypt("-1")
                    }));
                }
                entity.DepartmentChildList.Add(new OrganizationEntity
                {
                    ParentId = EncryptHelper.DesEncrypt(firstdepart.Id.ToString()),
                    DepartmentId = EncryptHelper.DesEncrypt("-1"), //避免无效部门id和有效部门id冲突
                    DepartmentName = "Envalid Department",
                    DepartmentUserNum = 0,
                    DepartmentChildList = unValidEntity
                });
            }

            return entity;
        }

        /// <summary>
        /// 描述：递归获取组织架构树
        /// 创建标识：cpf
        /// 创建时间：2017-9-20 10:56:01
        /// </summary>
        /// <param name="organization">组织架构树</param>
        /// <param name="userlist">所有的有效用户</param>
        /// <param name="departList">所有的部门</param>
        /// <param name="parentId">上级部门Id</param>
        /// <returns></returns>
        private static OrganizationEntity GetOrganizationTree(OrganizationEntity organization, List<UserModel> userlist, List<DepartmentModel> departList, long parentId)
        {
            var departmentinterim = departList.Where(x => x.BDParentId == parentId & x.BDIsValid == EnabledEnum.Enabled.GetHashCode()).ToList();

            if (departmentinterim.Any())
            {
                foreach (var item in departmentinterim)
                {
                    var userCount = userlist.Where(x => x.BUDepartId == item.Id).ToList().Count;
                    var temOrganizationEntity = new OrganizationEntity
                    {
                        DepartmentId = EncryptHelper.DesEncrypt(item.Id.ToString()),
                        ParentId = EncryptHelper.DesEncrypt(parentId.ToString()),
                        DepartmentName = item.BDDeptName,
                        DepartmentUserNum = userCount,
                        DepartmentIsValid = (byte)item.BDIsValid
                    };
                    if (organization.DepartmentChildList == null)
                    {
                        organization.DepartmentChildList = new List<OrganizationEntity>();
                    }
                    organization.DepartmentChildList.Add(temOrganizationEntity);
                    GetOrganizationTree(temOrganizationEntity, userlist, departList, item.Id);
                }
            }
            return organization;
        }

        /// <summary>
        /// 描述：获取所有的有效部门的下拉数据
        /// 创建标识：cpf
        /// 创建时间：13点38分
        /// </summary>
        /// <returns></returns>
        public static List<OrganizationSearch> GetAllDepartmentName()
        {
            var list = new List<OrganizationSearch>();
            var deplist = _departmentDal.GetAllDepartMent();
            if (deplist != null && deplist.Count > 0)
            {
                deplist = deplist.Where(x => x.BDIsValid == EnabledEnum.Enabled.GetHashCode()).ToList();
                foreach (var item in deplist)
                {
                    var search = new OrganizationSearch();
                    search.id = EncryptHelper.DesEncrypt(item.Id.ToString());
                    search.text = item.BDDeptName;
                    list.Add(search);
                }
            }
            return list;
        }

        /// <summary>
        /// 描述：根据id获取部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DepartmentInfo GetDepartById(int id)
        {
            var departmetInfo = _departmentDal.GetDepartById(id);
            if (departmetInfo != null)
            {
                departmetInfo.Id = EncryptHelper.DesEncrypt(departmetInfo.Id);
                departmetInfo.ParentId = EncryptHelper.DesEncrypt(departmetInfo.ParentId);
            }
            return departmetInfo;
        }

        /// <summary>
        /// 描述:保存新部门
        /// 创建标识：cpf
        /// 创建时间：2017-9-21 21:56:37
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static ResultInfoModel SaveNewDepartment(DepartmentSaveModel model, UserLoginInfo loginUser)
        {
            var result = new ResultInfoModel() { IsSuccess = true };
            var flag = CheckDepartment(model.DepartName);
            try
            {
                if (!flag)
                {
                    var entity = new DepartmentModel()
                    {
                        BDParentId = long.Parse(model.ParentId),
                        BDDeptName = model.DepartName,
                        BDDeptDesc = model.DepartDesc ?? string.Empty,
                        BDIsValid = model.IsValid,
                        BDCreateUserNo = loginUser.JobNum,
                        BDCreateUserName = loginUser.UserName,
                        BDCreateTime = DateTime.Now,
                        BDOperateUserNo = loginUser.JobNum,
                        BDOperateUserName = loginUser.UserName,
                        BDOperateTime = DateTime.Now
                    };
                    var ids = _departmentDal.Insert(entity);
                    result.Code = EncryptHelper.DesEncrypt(ids.ToString());
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "A department with the same name already exists";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public static ResultInfoModel SaveDepartment(DepartmentSaveModel model, UserLoginInfo loginUser)
        {
            var result = new ResultInfoModel() { IsSuccess = true };
            try
            {
                var entity = new DepartmentModel()
                {
                    Id = long.Parse(model.DepartId),
                    BDParentId = long.Parse(model.ParentId),
                    BDDeptName = model.DepartName,
                    BDDeptDesc = model.DepartDesc,
                    BDIsValid = model.IsValid,
                    BDOperateUserNo = loginUser.JobNum,
                    BDOperateUserName = loginUser.UserName,
                    BDOperateTime = DateTime.Now
                };
                _departmentDal.Update(entity);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }
         
        /// <summary>
        /// 描述：根据部门名称判断是否有相同的部门true表示已经重名
        /// 创建标识：cpf
        /// 创建时间：2017-9-21 21:56:54
        /// </summary>
        /// <param name="departName"></param>
        /// <returns></returns>
        private static bool CheckDepartment(string departName)
        {
            return _departmentDal.CheckDepartment(departName);
        }

        /// <summary>
        /// 描述：启用或停用部门
        /// 创建标识：cpf
        /// 创建时间：2017-9-24 18:17:48
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="loginuser"></param>
        /// <returns></returns>
        public static ResultInfoModel UpdateDepartmentValid(string Id, UserLoginInfo loginuser)
        {
            var result = new ResultInfoModel();
            var model = _departmentDal.GetDpById(Convert.ToInt32(Id));
            if (model != null)
            {
                if (model.BDIsValid == EnabledEnum.Enabled.GetHashCode())
                {
                    var deptlist = _departmentDal.GetDepartmentByParentId(long.Parse(Id));
                    if (deptlist != null && deptlist.Count > 0)
                    {
                        result.IsSuccess = false;
                        result.Message = "There is an effective sub department under the Department and cannot be deactivated!";
                        return result;
                    }
                    var userList = _userdal.GetUserListByDeptId(long.Parse(Id));
                    if (userList != null && userList.Count > 0)
                    {
                        result.IsSuccess = false;
                        result.Message = "You can't disable this department because there are some in-service staff employees under the department!";
                        return result;
                    }
                    model.BDIsValid = EnabledEnum.UnEnabled.GetHashCode();
                    model.BDOperateUserNo = loginuser.JobNum;
                    model.BDOperateUserName = loginuser.UserName;
                    model.BDOperateTime = DateTime.Now;
                    result.IsSuccess = _departmentDal.UpdateIsValid(model);
                }
                else
                {
                    var dpInfo = _departmentDal.GetDepartById(Convert.ToInt32(Id));
                    if (!CheckDepartment(dpInfo.Name))
                    {
                        var parentDept = _departmentDal.GetDpById(int.Parse(model.BDParentId.ToString()));
                        if (parentDept != null && parentDept.BDIsValid == EnabledEnum.UnEnabled.GetHashCode())
                        {
                            result.IsSuccess = false;
                            result.Message = "The higher level has been disabled, so it cannot be enabled";
                            return result;
                        }
                        model.BDIsValid = EnabledEnum.Enabled.GetHashCode();
                        model.BDOperateUserNo = loginuser.JobNum;
                        model.BDOperateUserName = loginuser.UserName;
                        model.BDOperateTime = DateTime.Now;
                        result.IsSuccess = _departmentDal.UpdateIsValid(model);
                        result.Code = EncryptHelper.DesEncrypt(model.BDParentId.ToString());
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = dpInfo.Name + "already exists!";
                        return result;
                    }
                }
                result.Message = "Edit Success";
            }
            return result;
        }

        public static DepartmentModel GetDepartByName(string departmenName)
        {
            return _departmentDal.GetDepartByName(departmenName);
        }
        #endregion

        #region 用户相关
        public static UserModel GetUserAndAccountById(long userId)
        {
            var userinfo = _userdal.GetUserAndAccountById(Convert.ToInt32(userId));
            if (!string.IsNullOrEmpty(userinfo.BUAvatars))
            {
                userinfo.AvatarsUrl = UploadHelper.GetDownLoadUrl(userinfo.BUAvatars);
            }
            return userinfo;
        }
        public static UserModel GetUserById(long userId)
        {
            var userinfo = _userdal.GetUserById(Convert.ToInt32(userId));
            if (!string.IsNullOrEmpty(userinfo.BUAvatars))
            {
                userinfo.AvatarsUrl = UploadHelper.GetDownLoadUrl(userinfo.BUAvatars);
            }
            return userinfo;
        }
        public static ResultInfoModel SaveNewUser(UserModel model)
        {
            var result = new ResultInfoModel { IsSuccess = true };
       
            try
            {
                if (!string.IsNullOrWhiteSpace(model.Account))
                {
                    var isOrNotExistAccount = _accountDal.GetAccountByAccount(model.Account);
                    if (isOrNotExistAccount == null)
                    {
                        var userId = _userdal.InsertUser(model);
                        var account = new AccountModel();
                        account.BAAccount = model.Account;
                        account.BAPassword = EncryptHelper.DesEncrypt("123456");
                        account.BAUserId = int.Parse(userId.ToString());
                        account.BAType = UserEnum.Employee.GetHashCode();
                        account.BAIsValid = EnabledEnum.Enabled.GetHashCode();
                        account.BACreateUserNo = model.BUCreateUserNo;
                        account.BACreateUserName = model.BUCreateUserName;
                        account.BACreateTime = DateTime.Now;
                        account.BAOperateUserNo = model.BUOperateUserNo;
                        account.BAOperateUserName = model.BUOperateUserName;
                        account.BAOperateTime = DateTime.Now;
                        _accountDal.InsertAccount(account);
                        result.Code = EncryptHelper.DesEncrypt(userId.ToString());
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "Account has exist!";
                    }
                }
                else
                {
                    var userId = _userdal.InsertUser(model);
                    result.Code = EncryptHelper.DesEncrypt(userId.ToString());
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;

            }
            return result;
        }
        public static bool DeleteUser(int userId, UserLoginInfo loginUser)
        {
            try
            {
                var model = _userdal.GetUserById(userId);
                model.BUIsValid = 0;
                model.BUOperateUserNo = loginUser.JobNum;
                model.BUOperateUserName = loginUser.UserName;
                model.BUOperateTime = DateTime.Now;
                return _userdal.UpdateUser(model);
            }
            catch
            {
                return false;
            }
        }
        public static string GenerateNewJobNumber(string jobNumber)
        {
            if (string.IsNullOrEmpty(jobNumber))
            {
                jobNumber = UserContent.DefaultJobNumber;
                var maxJobNumberUser = GetMaxJobNumber();
                if (maxJobNumberUser != null)
                {
                    jobNumber = (long.Parse(maxJobNumberUser.BUJobNumber) + 1).ToString().PadLeft(5, '0');
                }
            }
            else
            {
                jobNumber = (long.Parse(jobNumber) + 1).ToString().PadLeft(5, '0');
            }
            return jobNumber;
        }

        public static ResultInfoModel SaveUser(UserModel model, bool isExistAccount)
        {
            var result = new ResultInfoModel();
            result.IsSuccess = _userdal.UpdateUser(model);

            if (!string.IsNullOrWhiteSpace(model.Account) && !isExistAccount)
            {
                var isOrNotExistAccount = _accountDal.GetAccountByAccount(model.Account);
                if (isOrNotExistAccount == null)
                {
                    var account = new AccountModel();
                    account.BAAccount = model.Account;
                    account.BAPassword = EncryptHelper.DesEncrypt("123456");
                    account.BAUserId = model.Id;
                    account.BAType = UserEnum.Employee.GetHashCode();
                    account.BAIsValid = EnabledEnum.Enabled.GetHashCode();
                    account.BACreateUserNo = model.BUOperateUserNo;
                    account.BACreateUserName = model.BUOperateUserName;
                    account.BACreateTime = DateTime.Now;
                    account.BAOperateUserNo = model.BUOperateUserNo;
                    account.BAOperateUserName = model.BUOperateUserName;
                    account.BAOperateTime = DateTime.Now;
                    _accountDal.InsertAccount(account);
                    result.Code = EncryptHelper.DesEncrypt(model.Id.ToString());
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Account has exist!";
                }
            }

            return result;
        }

        /// <summary>
        /// 描述：验证是否已存在手机号true表示有false表示没有
        /// 创建标识；cpf
        /// </summary>
        /// <param name="phoneNum"></param>
        /// <returns></returns>
        public static bool IfHaveSamePhone(string phoneNum)
        {
            var dt = _userdal.GetUserByPhone(phoneNum);
            return dt != null && dt.Count > 0;
        }

        private static UserModel GetMaxJobNumber()
        {
            var model = _userdal.GetMaxJobNumber();
            return model;
        }

        public static UserModel GetUserByJobNumber(string jobNumber)
        {
            var result = new UserModel { Id = 0 };
            var dt = _userdal.GetUserByJobNumber(jobNumber);
            if (dt != null && dt.Count > 0)
            {
                result = dt[0];
            }
            return result;
        }

        /// <summary>
        /// 描述：验证工号是否存在true表示有false表示没有
        /// 创建标识；cpf
        /// </summary>
        /// <param name="JobNumber"></param>
        /// <returns></returns>
        public static bool IfHaveSameJobNumber(string jobNumber)
        {
            var dt = _userdal.GetUserByJobNumber(jobNumber);
            return dt != null && dt.Count > 0;
        }

        public static List<UserView> SearchUser(UserSearchViewModel search, out int totalCount)
        {
            var result = _userdal.SearchUser(search, out totalCount);
            if (result != null && result.Count > 0)
            {
                foreach (var item in result)
                {
                    item.UserId = EncryptHelper.DesEncrypt(item.UserId);
                    //item.DepartId = EncryptHelper.DesEncrypt(item.DepartId);
                    item.SexStr = EnumHelper.GetDescriptionByValue<Sex>(item.BUSex);
                }
            }
            return result;
        }

        /// <summary>
        /// 通过Key查询用户
        /// </summary>
        /// <returns></returns>
        public static List<UserModel> UserAutoComplete(string key)
        {
            var last = new List<UserModel>();
            if (!string.IsNullOrEmpty(key))
            {
                last = _userdal.GetSelectUserInfo(key);
            }
            return last;
        }

        #endregion

        #region 账号相关
        public static ResultInfoModel RestAccount(string account, string password, UserLoginInfo loguser)
        {
            var result = new ResultInfoModel { IsSuccess = false, };
            //验证账户名是否存在
            var accountInfo = _accountDal.GetAccountByAccount(account);
            if (accountInfo != null && accountInfo.BAPassword == password)
            {
                result.Message = "The password is same with the original password";
                return result;
            }
            if (accountInfo != null)
            {
                accountInfo.BAPassword = EncryptHelper.DesEncrypt(password);
                accountInfo.BAOperateUserNo = loguser.JobNum;
                accountInfo.BAOperateUserName = loguser.UserName;
                accountInfo.BAOperateTime = DateTime.Now;
                _accountDal.UpdataAccount(accountInfo);
                result.IsSuccess = true;
                result.Message = "Update Success";
            }
            return result;
        }

        /// <summary>
        /// 描述：根据用户id初始化用户密码
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static ResultInfoModel RestAccountByUserid(string userId, UserLoginInfo loguser)
        {
            var result = new ResultInfoModel { IsSuccess = false, };
            var accountInfo = _accountDal.GetAccountByUserId(long.Parse(EncryptHelper.DesDecrypt(userId)));
            if (accountInfo != null)
            {
                accountInfo.BAPassword = EncryptHelper.DesEncrypt(ConfigurationManager.AppSettings["defaultPassword"].ToString());
                accountInfo.BAOperateUserNo = loguser.JobNum;
                accountInfo.BAOperateUserName = loguser.UserName;
                accountInfo.BAOperateTime = DateTime.Now;
                _accountDal.UpdataAccount(accountInfo);
                result.IsSuccess = true;
                result.Message = "Update Success";
            }
            return result;
        }

        public static bool IsAdmin(long userId)
        {
            bool isadmin = false;
            var accountInfo = _accountDal.GetAccountByUserId(userId);
            if (accountInfo != null)
            {
                isadmin = accountInfo.BAType == UserEnum.Manager.GetHashCode() ? true : false;
            }
            return isadmin;
        }

        public static List<UserModel> GetAllUser()
        {
            return _userdal.GetAllIsValidUser();
        }

        public static void InitialManager()
        {
            var managerModel = new UserModel
            {
                BUName = "管理员",
                BUJobNumber = "00000",
                BUSex = Sex.Male.GetHashCode(),
                BUAvatars = string.Empty,
                BUPhoneNum = string.Empty,
                BUEmail = string.Empty,
                BUDepartId = 0,
                BUDepartName = "DefaultDepart",
                BUTitle = "Manager",
                BUCreateUserNo = "0",
                BUCreateUserName = "初始化",
                BUCreateTime = DateTime.Now,
                BUOperateUserNo = "0",
                BUOperateUserName = "初始化",
                BUOperateTime = DateTime.Now,
                BUEnglishName = "Manager",
                BUPosition = "Manager",
                BUExtensionPhone = string.Empty,
                BUMobilePhone = string.Empty,
                BUIsValid = EnabledEnum.Enabled.GetHashCode()
            };

            try
            {
                var userId = _userdal.InsertUser(managerModel);
                var account = new AccountModel();
                account.BAAccount = "Admin";
                account.BAPassword = EncryptHelper.DesEncrypt("Admin");
                account.BAUserId = int.Parse(userId.ToString());
                account.BAType = UserEnum.Manager.GetHashCode();
                account.BAIsValid = EnabledEnum.Enabled.GetHashCode();
                account.BACreateUserNo = managerModel.BUCreateUserNo;
                account.BACreateUserName = managerModel.BUCreateUserName;
                account.BACreateTime = DateTime.Now;
                account.BAOperateUserNo = managerModel.BUOperateUserNo;
                account.BAOperateUserName = managerModel.BUOperateUserName;
                account.BAOperateTime = DateTime.Now;
                _accountDal.InsertAccount(account);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
    }
}
