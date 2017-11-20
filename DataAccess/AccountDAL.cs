using Common;
using Model.TableModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class AccountDAL : NewSqlHelper
    {
        private const string tableName = "T_BASE_ACCOUNT";
        /// <summary>
        /// 描述：根据登录账号获取账号信息
        /// 创建标识;cpf23568
        /// 创建时间：2017-9-10 21:36:38
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public AccountModel GetAccountByAccount(string account)
        {
            var accountInfo = new AccountModel();
            SqlParameter[] para = {
                new SqlParameter("@Account", account)
            };

            var sql = @"SELECT Id,BAAccount,BAPassword,BAUserId,BAType,BAIsValid FROM " + tableName
                + " WITH(NOLOCK) WHERE BAAccount=@Account AND BAIsValid=1";
            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    accountInfo = DataConvertHelper.DataTableToList<AccountModel>(dt)[0];
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
            return accountInfo;
        }

        public AccountModel GetAccountByAccountAndPass(string account, string password)
        {
            var accountInfo = new AccountModel();
            SqlParameter[] para = {
                new SqlParameter("@Account", account),
                new SqlParameter("@Password", password)
            };

            var sql = @"SELECT Id,BAAccount,BAPassword,BAUserId,BAType,BAIsValid FROM " + tableName
                + " WITH(NOLOCK) WHERE BAAccount=@Account AND BAPassword=@Password";

            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                accountInfo = DataConvertHelper.DataTableToList<AccountModel>(dt)[0];
            }
            else
            {
                return null;
            }
            //using (SqlDataReader dr = ExecuteReader(CommandType.Text, sql, null, para))
            //{
            //    if (dr.Read())
            //    {
            //        accountInfo.Id = Convert.ToInt32(dr["Id"]);
            //        accountInfo.BAPassword = dr["BAPassword"].ToString();
            //        accountInfo.BAAccount = dr["BAAccount"].ToString();
            //        accountInfo.BAUserId = Convert.ToInt32(dr["BAUserId"]);
            //        accountInfo.BAType = Convert.ToInt32(dr["BAType"]);
            //        accountInfo.BAIsValid = Convert.ToInt32(dr["BAIsValid"]);
            //        dr.Close();
            //    }
            //    else
            //    {
            //        accountInfo = null;
            //    }
            //}

            return accountInfo;
        }

        public AccountModel GetAccountByUserId(long userId)
        {
            var accountInfo = new AccountModel();
            SqlParameter[] para = {
                new SqlParameter("@BAUserId", userId)
            };

            var sql = @"SELECT Id,BAAccount,BAPassword,BAUserId,BAType,BAIsValid FROM " + tableName
                + " WITH(NOLOCK) WHERE BAUserId=@BAUserId";

            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                accountInfo = DataConvertHelper.DataTableToList<AccountModel>(dt)[0];
            }
            else
            {
                return null;
            }
            //using (SqlDataReader dr = ExecuteReader(CommandType.Text, sql, null, para))
            //{
            //    if (dr.Read())
            //    {
            //        accountInfo.Id = Convert.ToInt32(dr["Id"]);
            //        accountInfo.BAPassword = dr["BAPassword"].ToString();
            //        accountInfo.BAAccount = dr["BAAccount"].ToString();
            //        accountInfo.BAUserId = Convert.ToInt32(dr["BAUserId"]);
            //        accountInfo.BAType = Convert.ToInt32(dr["BAType"]);
            //        accountInfo.BAIsValid = Convert.ToInt32(dr["BAIsValid"]);
            //        dr.Close();
            //    }
            //    else
            //    {
            //        accountInfo = null;
            //    }
            //}

            return accountInfo;
        }

        /// <summary>
        /// 描述更新账号信息
        /// 创建标识：cpf
        /// 创建时间：14点10分
        /// </summary>
        /// <param name="model">更新model</param>
        /// <returns></returns>
        public bool UpdataAccount(AccountModel model)
        {
            var sql = @"UPDATE " + tableName +
                " SET BAPassword=@BAPassword,BAOperateUserNo=@BAOperateUserNo,BAOperateUserName=@BAOperateUserName,BAOperateTime=@BAOperateTime " +
                " WHERE Id=@Id";
            SqlParameter[] para = {
                new SqlParameter("@BAAccount",model.BAAccount),
                new SqlParameter("@BAPassword",model.BAPassword),
                new SqlParameter("@BAOperateUserNo",model.BAOperateUserNo),
                new SqlParameter("@BAOperateUserName",model.BAOperateUserName),
                new SqlParameter("@BAOperateTime",model.BAOperateTime),
                new SqlParameter("@Id",model.Id),
            };
            return ExecteNonQuery(CommandType.Text, sql, null, para) > 0;
        }

        public bool InsertAccount(AccountModel model, SqlTransaction tran = null)
        {
            var sql = @"INSERT INTO " + tableName +
                " (BAAccount,BAPassword,BAUserId,BAType,BAIsValid,BACreateUserNo,BACreateUserName,BACreateTime,BAOperateUserNo,BAOperateUserName,BAOperateTime )" +
                " VALUES (@BAAccount,@BAPassword,@BAUserId,@BAType,@BAIsValid,@BACreateUserNo,@BACreateUserName,@BACreateTime,@BAOperateUserNo,@BAOperateUserName,@BAOperateTime)";
            SqlParameter[] para = {
                new SqlParameter("@BAAccount",  string.IsNullOrEmpty(model.BAAccount)? string.Empty :model.BAAccount ),
                new SqlParameter("@BAPassword",string.IsNullOrEmpty(model.BAPassword)? string.Empty :model.BAPassword),
                new SqlParameter("@BAUserId",model.BAUserId),
                new SqlParameter("@BAType",model.BAType),
                new SqlParameter("@BAIsValid",model.BAIsValid),
                new SqlParameter("@BACreateUserNo",string.IsNullOrEmpty(model.BACreateUserNo)?string.Empty :model.BACreateUserNo),
                new SqlParameter("@BACreateUserName",string.IsNullOrEmpty(model.BACreateUserName)?string.Empty :model.BACreateUserName),
                new SqlParameter("@BACreateTime",model.BACreateTime),
                new SqlParameter("@BAOperateUserNo",string.IsNullOrEmpty(model.BAOperateUserNo)?string.Empty :model.BAOperateUserNo),
                new SqlParameter("@BAOperateUserName",string.IsNullOrEmpty(model.BAOperateUserName)?string.Empty :model.BAOperateUserName),
                new SqlParameter("@BAOperateTime",model.BAOperateTime)
            };
            return ExecuteDataSet(CommandType.Text, sql, tran, para).Tables.Count > 0;
        }
    }
}
