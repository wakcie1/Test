using Common;
using Common.Enum;
using Model.TableModel;
using Model.ViewModel.Jurisdiction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class UserRoleRelationDAL : NewSqlHelper
    {
        private const string tableName = "T_BASE_USERROLE_RELATION";
        private const string groupTableName = "T_BASE_GROUP";
        /// <summary>
        /// 描述;根据用户id获取权限分配情况
        /// 创建标识：曹鹏飞
        /// 创建时间：16点50分
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<UserRoleRelationModel> GetUserRoleRelationByUserId(long userid)
        {
            var list = new List<UserRoleRelationModel>();
            var sql = "SELECT Id,BURUserId,BURGroupId,BURIsValid,BURCreateUserId,BURCreateUserName,BURCreateTime," +
                "BUROperateUserId,BUROperateUserName,BUROperateTime FROM " + tableName +
                " WITH(NOLOCK) WHERE  BURUserId=@BURUserId AND BURIsValid=1";
            SqlParameter[] para = {
                new SqlParameter("@BURUserId", userid),
            };
            var ds = ExecuteDataSet(CommandType.Text, sql, null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<UserRoleRelationModel>(dt);
            }
            return list;
        }

        public List<UserRoleRelationModel> GetUserRoleRelationByGroupId(long groupid)
        {
            var list = new List<UserRoleRelationModel>();
            var sql = "SELECT Id,BURUserId,BURGroupId,BURIsValid,BURCreateUserId,BURCreateUserName,BURCreateTime," +
                "BUROperateUserId,BUROperateUserName,BUROperateTime FROM " + tableName +
                " WITH(NOLOCK) WHERE  BURGroupId=@BURGroupId AND BURIsValid=1";
            SqlParameter[] para = {
                new SqlParameter("@BURGroupId", groupid),
            };
            var ds = ExecuteDataSet(CommandType.Text, sql, null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<UserRoleRelationModel>(dt);
            }
            return list;
        }

        public List<UserGroupModel> GetUserGroupListByUserId(long userid)
        {
            var list = new List<UserGroupModel>();
            var sql = string.Format(@"SELECT  
                R.BURUserId AS 'UserId' ,
                R.BURGroupId AS 'GroupId' ,
                G.BGCode AS 'GroupCode'
                FROM    {0} AS R WITH ( NOLOCK )
                INNER JOIN {1} AS G WITH ( NOLOCK ) ON G.Id = R.BURGroupId
                WHERE   R.BURIsValid = 1 AND R.BURUserId = @BURUserId",
                tableName,
                groupTableName);
            SqlParameter[] para = {
                new SqlParameter("@BURUserId", userid),
            };
            var ds = ExecuteDataSet(CommandType.Text, sql, null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<UserGroupModel>(dt);
            }
            return list;
        }
        /// <summary>
        /// 描述:根据userId删除relation
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public bool DeleteRelationByUserid(long userId, SqlTransaction tran = null)
        {
            SqlParameter[] para = {
                new SqlParameter("@BURUserId", userId),
                new SqlParameter("@BURIsValid",EnabledEnum.Enabled.GetHashCode())
            };
            var sql = "DELETE FROM " + tableName + " WHERE BURUserId = @BURUserId AND BURIsValid=@BURIsValid";

            int cmdresult = Convert.ToInt32(ExecuteScalar(CommandType.Text, sql.ToString(), tran, para));
            return cmdresult > 0;
        }

        public bool DeleteRelationByGroupid(long groupId, SqlTransaction tran = null)
        {
            SqlParameter[] para = {
                new SqlParameter("@BURGroupId", groupId),
                new SqlParameter("@BURIsValid",EnabledEnum.Enabled.GetHashCode())
            };
            var sql = "DELETE FROM " + tableName + " WHERE BURGroupId = @BURGroupId AND BURIsValid=@BURIsValid";

            int cmdresult = Convert.ToInt32(ExecuteScalar(CommandType.Text, sql.ToString(), tran, para));
            return cmdresult > 0;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public bool InsertRelation(UserRoleRelationModel model, SqlTransaction tran)
        {
            var sql = "INSERT INTO " + tableName +
                " (BURUserId,BURGroupId,BURIsValid,BURCreateUserId,BURCreateUserName,BURCreateTime,BUROperateUserId,BUROperateUserName,BUROperateTime)" +
                " VALUES (@BURUserId,@BURGroupId,@BURIsValid,@BURCreateUserId,@BURCreateUserName,@BURCreateTime,@BUROperateUserId,@BUROperateUserName,@BUROperateTime)";

            SqlParameter[] para = {
                new SqlParameter("@BURUserId",model.BURUserId),
                new SqlParameter("@BURGroupId",model.BURGroupId),
                new SqlParameter("@BURIsValid",model.BURIsValid),
                new SqlParameter("@BURCreateUserId",model.BURCreateUserId),
                new SqlParameter("@BURCreateUserName",model.BURCreateUserName),
                new SqlParameter("@BURCreateTime",model.BURCreateTime),
                new SqlParameter("@BUROperateUserId",model.BUROperateUserId),
                new SqlParameter("@BUROperateUserName",model.BUROperateUserName),
                new SqlParameter("@BUROperateTime",model.BUROperateTime),
            };
            int cmdresult = Convert.ToInt32(ExecuteScalar(CommandType.Text, sql.ToString(), tran, para));
            return cmdresult > 0;
        }
    }
}
