using Common;
using Common.Enum;
using Model.TableModel;
using Model.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class UserDAL : NewSqlHelper
    {
        private const string userTableName = "T_BASE_USER";
        private const string depatrTableName = "T_BASE_DEPARTMENT";
        private const string accountTableName = "T_BASE_ACCOUNT";

        public UserModel GetUserById(int id)
        {
            var user = new UserModel();
            string sql = @"SELECT Id
            ,[BUName]
            ,[BUJobNumber]
            ,[BUSex]
            ,[BUAvatars]
            ,[BUPhoneNum]
            ,[BUEmail]
            ,[BUDepartId]
            ,[BUTitle]
            ,[BUCreateUserNo]
            ,[BUCreateUserName]
            ,[BUCreateTime]
            ,[BUOperateUserNo]
            ,[BUOperateUserName]
            ,[BUOperateTime]
            ,[BUIsValid] 
            ,[BUDepartName]
            ,[BUEnglishName]
            ,[BUPosition]
            ,[BUExtensionPhone]
            ,[BUMobilePhone]  FROM " + userTableName
            + " WITH(NOLOCK) WHERE Id=@Id";
            SqlParameter[] para = { new SqlParameter("@Id", id) };
            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                user = DataConvertHelper.DataTableToList<UserModel>(dt)[0];
            }
            else
            {
                return null;
            }
            return user;
        }

        /// <summary>
        /// 描述：获取所有的有效人员
        /// </summary>
        /// <returns></returns>
        public List<UserModel> GetAllIsValidUser()
        {
            var list = new List<UserModel>();
            string sql = @"SELECT Id,BUName,BUJobNumber,BUSex,BUAvatars,BUPhoneNum,BUEmail,BUDepartId,BUIsValid, 
                           BUTitle,BUCreateUserNo,BUCreateUserName,BUCreateTime,BUOperateUserNo,BUOperateUserName,BUOperateTime FROM " + userTableName + " WITH(NOLOCK)";
            var ds = ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                var dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<UserModel>(dt);
            }
            return list;
        }

        /// <summary>
        /// 描述:根据部门Id获取该部门下的所有有效的员工
        /// </summary>
        /// <param name="departId"></param>
        /// <returns></returns>
        public List<UserModel> GetUserByDepartId(long departId)
        {
            var list = new List<UserModel>();
            var sql = @"SELECT Id,BUName,BUJobNumber,BUSex,BUAvatars,BUPhoneNum,BUEmail,BUDepartId,BUIsValid, 
                           BUTitle,BUCreateUserNo,BUCreateUserName,BUCreateTime,BUOperateUserNo,BUOperateUserName,BUOperateTime FROM " + userTableName
                           + " WITH(NOLOCK) WHERE BUIsValid=@BUIsValid AND BUDepartId=@BUDepartId";
            SqlParameter[] para = {
                new SqlParameter("@BUIsValid",EnabledEnum.Enabled.GetHashCode()),
                new SqlParameter("@BUDepartId",departId)
            };
            var ds = ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                var dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<UserModel>(dt);
            }
            return list;
        }

        /// <summary>
        /// 描述：获取某部门下的员工信息
        /// 创建标识：cpf
        /// 创建时间：2017-9-24 17:40:52
        /// </summary>
        /// <param name="dpId"></param>
        /// <returns></returns>
        public List<UserModel> GetUserListByDeptId(long dpId)
        {
            var list = new List<UserModel>();
            var user = new UserModel();
            string sql = @"SELECT Id,BUName,BUJobNumber,BUSex,BUAvatars,BUPhoneNum,BUEmail,BUDepartId,BUIsValid,BUTitle FROM " + userTableName
                + " WITH(NOLOCK) WHERE BUDepartId=@BUDepartId AND " +
                " BUIsValid=@BUIsValid";
            SqlParameter[] para = {
                new SqlParameter("@BUIsValid",EnabledEnum.Enabled.GetHashCode()),
                new SqlParameter("@BUDepartId", dpId)
            };
            var ds = ExecuteDataSet(CommandType.Text, sql, null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                var dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<UserModel>(dt);
            }
            return list;
        }

        /// <summary>
        /// 描述：插入员工并返回员工id
        /// 创建标识：cpf
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long InsertUser(UserModel model, SqlTransaction tran = null)
        {
            var sql = @"INSERT INTO " + userTableName +
                " ([BUName],[BUJobNumber] ,[BUSex],[BUAvatars],[BUPhoneNum],[BUEmail],[BUDepartId],[BUTitle] ,[BUCreateUserNo],[BUCreateUserName] ,[BUCreateTime] ,[BUOperateUserNo] ,[BUOperateUserName] ,[BUOperateTime],[BUIsValid] ,[BUDepartName],[BUEnglishName],[BUPosition],[BUExtensionPhone] ,[BUMobilePhone])" +
                " VALUES (@BUName,@BUJobNumber,@BUSex,@BUAvatars,@BUPhoneNum,@BUEmail,@BUDepartId,@BUTitle,@BUCreateUserNo,@BUCreateUserName,@BUCreateTime,@BUOperateUserNo,@BUOperateUserName,@BUOperateTime,@BUIsValid,@BUDepartName,@BUEnglishName,@BUPosition,@BUExtensionPhone,@BUMobilePhone)" +
                " select id = scope_identity()";
            SqlParameter[] para = {
                new SqlParameter("@BUName",string.IsNullOrEmpty(model.BUName)?string.Empty:model.BUName),
                //new SqlParameter("@BUGivenname",model.BUGivenname),
                new SqlParameter("@BUJobNumber",string.IsNullOrEmpty(model.BUJobNumber)?string.Empty:model.BUJobNumber),
                new SqlParameter("@BUSex", model.BUSex),
                new SqlParameter("@BUAvatars",string.IsNullOrEmpty(model.BUAvatars)?string.Empty:model.BUAvatars),
                new SqlParameter("@BUPhoneNum",string.IsNullOrEmpty(model.BUPhoneNum)?string.Empty:model.BUPhoneNum),
                new SqlParameter("@BUEmail",string.IsNullOrEmpty(model.BUEmail)?string.Empty:model.BUEmail),
                new SqlParameter("@BUDepartId",model.BUDepartId),
                new SqlParameter("@BUTitle",string.IsNullOrEmpty(model.BUTitle)?string.Empty:model.BUTitle),
                new SqlParameter("@BUCreateUserNo",string.IsNullOrEmpty(model.BUCreateUserNo)?string.Empty:model.BUCreateUserNo),
                new SqlParameter("@BUCreateUserName",string.IsNullOrEmpty(model.BUCreateUserName)?string.Empty:model.BUCreateUserName),
                new SqlParameter("@BUCreateTime",model.BUCreateTime),
                new SqlParameter("@BUOperateUserNo",string.IsNullOrEmpty(model.BUOperateUserNo)?string.Empty:model.BUOperateUserNo),
                new SqlParameter("@BUOperateUserName",string.IsNullOrEmpty(model.BUOperateUserName)?string.Empty:model.BUOperateUserName),
                new SqlParameter("@BUOperateTime",model.BUOperateTime),
                new SqlParameter("@BUIsValid",model.BUIsValid),
                new SqlParameter("@BUDepartName",string.IsNullOrEmpty(model.BUDepartName)?string.Empty:model.BUDepartName),
                new SqlParameter("@BUEnglishName",string.IsNullOrEmpty(model.BUEnglishName)?string.Empty:model.BUEnglishName),
                new SqlParameter("@BUPosition",string.IsNullOrEmpty(model.BUPosition)?string.Empty:model.BUPosition),
                new SqlParameter("@BUExtensionPhone",string.IsNullOrEmpty(model.BUExtensionPhone)?string.Empty:model.BUExtensionPhone),
                new SqlParameter("@BUMobilePhone",string.IsNullOrEmpty(model.BUMobilePhone)?string.Empty:model.BUMobilePhone),
            };
            long result = 0;
            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), tran, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                var IdString = ds.Tables[0].Rows[0][0].ToString();
                result = string.IsNullOrEmpty(IdString) ? 0 : long.Parse(IdString);
            }
            return result;
        }

        public List<UserModel> GetUserByPhone(string phoneNum)
        {
            var list = new List<UserModel>();
            var user = new UserModel();
            string sql = @"SELECT Id,BUName,BUJobNumber,BUSex,BUAvatars,BUPhoneNum,BUEmail,BUDepartId,BUIsValid,BUTitle FROM " + userTableName
                + " WITH(NOLOCK) WHERE BUPhoneNum=@BUPhoneNum AND " +
                " BUIsValid=@BUIsValid";
            SqlParameter[] para = {
                new SqlParameter("@BUIsValid",EnabledEnum.Enabled.GetHashCode()),
                new SqlParameter("@BUPhoneNum", phoneNum)
            };
            var ds = ExecuteDataSet(CommandType.Text, sql, null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                var dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<UserModel>(dt);
            }
            return list;
        }

        public UserModel GetMaxJobNumber()
        {
            var model = new UserModel();
            var user = new UserModel();
            string sql = @"SELECT Top 1 Id,BUName,BUJobNumber,BUSex,BUAvatars,BUPhoneNum,BUEmail,BUDepartId,BUIsValid,BUTitle FROM " + userTableName
                + " WITH(NOLOCK) ORDER BY BUJobNumber DESC ";
            var ds = ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                var dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    model = DataConvertHelper.DataTableToList<UserModel>(dt)[0];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
            return model;
        }

        /// <summary>
        /// 描述：根据工号获取用户
        /// </summary>
        /// <param name="jobnumber"></param>
        /// <returns></returns>
        public List<UserModel> GetUserByJobNumber(string jobnumber)
        {
            var list = new List<UserModel>();
            var user = new UserModel();
            string sql = @"SELECT U.Id,BUName,U.BUJobNumber,U.BUSex,U.BUAvatars,U.BUPhoneNum,U.BUEmail,U.BUDepartId,D.BDDeptName AS BUDeptName, U.BUIsValid,U.BUTitle FROM " + userTableName
                + " U WITH(NOLOCK) LEFT JOIN " + depatrTableName + " D WITH(NOLOCK) ON U.BUDepartId = D.Id " +
                " WHERE U.BUJobNumber=@BUJobNumber AND " +
                " U.BUIsValid=@BUIsValid";
            SqlParameter[] para = {
                new SqlParameter("@BUIsValid",EnabledEnum.Enabled.GetHashCode()),
                new SqlParameter("@BUJobNumber", jobnumber)
            };
            var ds = ExecuteDataSet(CommandType.Text, sql, null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                var dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<UserModel>(dt);
            }
            return list;
        }

        /// <summary>
        /// 描述：模糊获取有效用户
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<UserModel> GetSelectUserInfo(string key)
        {
            var list = new List<UserModel>();
            var user = new UserModel();
            string sql = string.Format(@"SELECT TOP 5 Id,BUName,BUEnglishName,BUJobNumber FROM {0} WITH(NOLOCK) 
                 WHERE (BUJobNumber like '%{1}%' OR BUName like N'%{1}%' OR BUEnglishName like '%{1}%') AND BUIsValid =1",
                 userTableName, key);

            var ds = ExecuteDataSet(CommandType.Text, sql, null);
            if (ds != null && ds.Tables.Count > 0)
            {
                var dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<UserModel>(dt);
            }
            return list;
        }

        public bool UpdateUser(UserModel model)
        {
            if (model.Id == 0)
            {
                return false;
            }
            var upsql = new StringBuilder();
            var paramsql = new StringBuilder();
            var param = new List<SqlParameter>();
            upsql.Append(string.Format("UPDATE {0} SET ", userTableName));
            #region param

            if (!string.IsNullOrEmpty(model.BUName))
            {
                paramsql.Append(" BUName=@BUName ,");
                param.Add(new SqlParameter("@BUName", model.BUName));
            }
            if (!string.IsNullOrEmpty(model.BUJobNumber))
            {
                paramsql.Append(" BUJobNumber=@BUJobNumber ,");
                param.Add(new SqlParameter("@BUJobNumber", model.BUJobNumber));
            }
            if (model.BUSex != null)
            {
                paramsql.Append(" BUSex=@BUSex ,");
                param.Add(new SqlParameter("@BUSex", model.BUSex));
            }
            if (!string.IsNullOrEmpty(model.BUAvatars))
            {
                paramsql.Append(" BUAvatars=@BUAvatars ,");
                param.Add(new SqlParameter("@BUAvatars", model.BUAvatars));
            }
            if (!string.IsNullOrEmpty(model.BUPhoneNum))
            {
                paramsql.Append(" BUPhoneNum=@BUPhoneNum ,");
                param.Add(new SqlParameter("@BUPhoneNum", model.BUPhoneNum));
            }
            if (!string.IsNullOrEmpty(model.BUEmail))
            {
                paramsql.Append(" BUEmail=@BUEmail ,");
                param.Add(new SqlParameter("@BUEmail", model.BUEmail));
            }
            if (model.BUDepartId > 0)
            {
                paramsql.Append(" BUDepartId=@BUDepartId ,");
                param.Add(new SqlParameter("@BUDepartId", model.BUDepartId));
            }
            if (!string.IsNullOrEmpty(model.BUTitle))
            {
                paramsql.Append(" BUTitle=@BUTitle ,");
                param.Add(new SqlParameter("@BUTitle", model.BUTitle));
            }
            if (model.BUIsValid != null)
            {
                paramsql.Append(" [BUIsValid] = @BUIsValid ,");
                param.Add(new SqlParameter("@BUIsValid", model.BUIsValid));
            }
            if (!string.IsNullOrEmpty(model.BUCreateUserNo))
            {
                paramsql.Append(" [BUCreateUserNo] = @BUCreateUserNo ,");
                param.Add(new SqlParameter("@BUCreateUserNo", model.BUCreateUserNo));
            }
            if (!string.IsNullOrEmpty(model.BUCreateUserName))
            {
                paramsql.Append(" [BUCreateUserName] = @BUCreateUserName ,");
                param.Add(new SqlParameter("@BUCreateUserName", model.BUCreateUserName));
            }
            if (model.BUCreateTime != null)
            {
                paramsql.Append(" [BUCreateTime] = @BUCreateTime ,");
                param.Add(new SqlParameter("@BUCreateTime", model.BUCreateTime));
            }
            if (!string.IsNullOrEmpty(model.BUOperateUserNo))
            {
                paramsql.Append(" [BUOperateUserNo] = @BUOperateUserNo ,");
                param.Add(new SqlParameter("@BUOperateUserNo", model.BUOperateUserNo));
            }
            if (!string.IsNullOrEmpty(model.BUOperateUserName))
            {
                paramsql.Append(" [BUOperateUserName] = @BUOperateUserName ,");
                param.Add(new SqlParameter("@BUOperateUserName", model.BUOperateUserName));
            }
            if (model.BUOperateTime != null)
            {
                paramsql.Append(" [BUOperateTime] = @BUOperateTime ,");
                param.Add(new SqlParameter("@BUOperateTime", model.BUOperateTime));
            }
            #endregion
            if (param.Count == 0)
            {
                return false;
            }

            var paramsqlresult = paramsql.ToString();
            paramsqlresult = paramsqlresult.Remove(paramsqlresult.Length - 1, 1);
            upsql.Append(string.Format("{0} WHERE Id = @Id ", paramsqlresult));
            param.Add(new SqlParameter("@Id", model.Id));

            return ExecteNonQuery(CommandType.Text, upsql.ToString(), null, param) > 0;
        }

        public List<UserView> SearchUser(UserSearchViewModel search, out int totalCount)
        {
            var list = new List<UserView>();
            var sql = new StringBuilder();
            var countSql = new StringBuilder();
            var whereSql = new StringBuilder();
            whereSql.Append(" WHERE 1=1 AND A.BUIsValid=1");
            if (!string.IsNullOrEmpty(search.Name))
            {
                whereSql.Append(string.Format(" AND A.BUName LIKE N'%{0}%' OR A.BUEnglishName LIKE '%{0}%'", search.Name));
            }
            if (!string.IsNullOrEmpty(search.DepartName))
            {
                whereSql.Append(string.Format(" AND A.BUDepartName LIKE '%{0}%'", search.DepartName));
            }
            if (!string.IsNullOrEmpty(search.Position))
            {
                whereSql.Append(string.Format(" AND A.BUPosition LIKE '%{0}%'", search.Position));
            }
            if (!string.IsNullOrEmpty(search.Account))
            {
                whereSql.Append(string.Format(" AND B.BAAccount LIKE '%{0}%'", search.Account));
            }

            sql.Append(string.Format(@"
                SELECT  newTable.*
                FROM    (
                        SELECT TOP ( {0} * {1} )
                        ROW_NUMBER() OVER (ORDER BY A.Id DESC) RowNum 
                      ,A.[Id] AS UserId
                      ,A.[BUName]
                      ,A.[BUJobNumber]
                      ,A.[BUSex]
                      ,A.[BUAvatars]
                      ,A.[BUPhoneNum]
                      ,A.[BUEmail]
                      ,A.[BUDepartId]
                      ,A.[BUTitle]
                      ,A.[BUCreateUserNo]
                      ,A.[BUCreateUserName]
                      ,A.[BUCreateTime]
                      ,A.[BUOperateUserNo]
                      ,A.[BUOperateUserName]
                      ,A.[BUOperateTime]
                      ,A.[BUIsValid]
                      ,A.[BUDepartName]
                      ,A.[BUEnglishName]
                      ,A.[BUPosition]
                      ,A.[BUExtensionPhone]
                      ,A.[BUMobilePhone]
	                  ,B.BAAccount  AS Account 
                    FROM {2} A WiTH (NOLOCK)
                      LEFT JOIN {3} B WITH (NOLOCK)  
                            ON A.Id=B.BAUserId AND B.BAIsValid=1 {4}
                    ORDER BY A.BUIsValid  DESC , A.BUCreateTime DESC) newTable
                WHERE newTable.RowNum>(({0}-1)*{1})
            ", search.CurrentPage, search.PageSize, userTableName, accountTableName, whereSql.ToString()));
            countSql.Append(string.Format(@"SELECT COUNT(1) FROM (
                                                            SELECT A.[Id]
                                                                  ,A.[BUName]
                                                                  ,A.[BUJobNumber]
                                                                  ,A.[BUSex]
                                                                  ,A.[BUAvatars]
                                                                  ,A.[BUPhoneNum]
                                                                  ,A.[BUEmail]
                                                                  ,A.[BUDepartId]
                                                                  ,A.[BUTitle]
                                                                  ,A.[BUCreateUserNo]
                                                                  ,A.[BUCreateUserName]
                                                                  ,A.[BUCreateTime]
                                                                  ,A.[BUOperateUserNo]
                                                                  ,A.[BUOperateUserName]
                                                                  ,A.[BUOperateTime]
                                                                  ,A.[BUIsValid]
                                                                  ,A.[BUDepartName]
                                                                  ,A.[BUEnglishName]
                                                                  ,A.[BUPosition]
                                                                  ,A.[BUExtensionPhone]
                                                                  ,A.[BUMobilePhone]
	                                                              ,B.BAAccount  AS Account
                                                              FROM {0} A WiTH (NOLOCK)
                                                              LEFT JOIN {1} B WITH (NOLOCK)  ON A.Id=B.BAUserId AND B.BAIsValid=1 {2}
                                                            )TEMP ", userTableName, accountTableName, whereSql.ToString()));

            var ds = ExecuteDataSet(CommandType.Text, sql.ToString());
            totalCount = ExecuteCount(CommandType.Text, countSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<UserView>(dt);
            }
            return list;
        }

        public UserModel GetUserAndAccountById(int id)
        {
            var user = new UserModel();
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" SELECT A.[Id]
                                      ,A.[BUName]
                                      ,A.[BUJobNumber]
                                      ,A.[BUSex]
                                      ,A.[BUAvatars]
                                      ,A.[BUPhoneNum]
                                      ,A.[BUEmail]
                                      ,A.[BUDepartId]
                                      ,A.[BUTitle]
                                      ,A.[BUCreateUserNo]
                                      ,A.[BUCreateUserName]
                                      ,A.[BUCreateTime]
                                      ,A.[BUOperateUserNo]
                                      ,A.[BUOperateUserName]
                                      ,A.[BUOperateTime]
                                      ,A.[BUIsValid]
                                      ,A.[BUDepartName]
                                      ,A.[BUEnglishName]
                                      ,A.[BUPosition]
                                      ,A.[BUExtensionPhone]
                                      ,A.[BUMobilePhone]
	                                  ,B.BAAccount  AS Account
                                  FROM {0} A WiTH (NOLOCK)
                                  LEFT JOIN {1} B WITH (NOLOCK)  
                                ON A.Id=B.BAUserId AND B.BAIsValid=1 ", userTableName, accountTableName);
            sql.AppendFormat(" WHERE A.Id =@Id AND A.BUIsValid=1 ");
            SqlParameter[] para = { new SqlParameter("@Id", id) };
            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                user = DataConvertHelper.DataTableToList<UserModel>(dt)[0];
            }
            else
            {
                return null;
            }
            return user;
        }
    }
}
