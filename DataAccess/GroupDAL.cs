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
    public class GroupDAL : NewSqlHelper
    {
        private const string tableName = "T_BASE_GROUP";

        public GroupModel GetGroupById(long id)
        {
            var roloGroupInfo = new GroupModel();
            SqlParameter[] para = {
                new SqlParameter("@Id", id)
            };

            var sql = "SELECT Id,BGCode,BGName,BGIsValid FROM " + tableName + " WITH(NOLOCK) WHERE Id=@Id";

            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                roloGroupInfo = DataConvertHelper.DataTableToList<GroupModel>(dt)[0];
            }
            else
            {
                return null;
            }
            //using (var dr = ExecuteReader(CommandType.Text, sql, null, para))
            //{
            //    if (dr.Read())
            //    {
            //        roloGroupInfo.Id = long.Parse(dr["Id"].ToString());
            //        roloGroupInfo.BGCode = dr["BGCode"].ToString();
            //        roloGroupInfo.BGName = dr["BGName"].ToString();
            //        roloGroupInfo.BGIsValid = Convert.ToInt32(dr["BGIsValid"]);
            //        dr.Close();
            //    }
            //    else
            //    {
            //        roloGroupInfo = null;
            //    }
            //}
            return roloGroupInfo;
        }

        public GroupModel GetGroupByGroupName(string groupName)
        {
            var roloGroupInfo = new GroupModel();
            SqlParameter[] para = {
                new SqlParameter("@BGName", groupName),
                new SqlParameter("@BGIsValid",EnabledEnum.Enabled.GetHashCode())
            };

            var sql = "SELECT Id,BGCode,BGName,BGIsValid FROM " + tableName + " WITH(NOLOCK) WHERE BGName=@BGName AND BGIsValid=@BGIsValid";

            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                roloGroupInfo = DataConvertHelper.DataTableToList<GroupModel>(dt)[0];
            }
            else
            {
                return null;
            }
            //using (var dr = ExecuteReader(CommandType.Text, sql, null, para))
            //{
            //    if (dr.Read())
            //    {
            //        roloGroupInfo.Id = long.Parse(dr["Id"].ToString());
            //        roloGroupInfo.BGCode = dr["BGCode"].ToString();
            //        roloGroupInfo.BGName = dr["BGName"].ToString();
            //        roloGroupInfo.BGIsValid = Convert.ToInt32(dr["BGIsValid"]);
            //        dr.Close();
            //    }
            //    else
            //    {
            //        roloGroupInfo = null;
            //    }
            //}
            return roloGroupInfo;
        }

        /// <summary>
        /// 描述：获取所有的权限包
        /// </summary>
        /// <returns></returns>
        public List<GroupModel> GetAllGroup()
        {
            var list = new List<GroupModel>();
            SqlParameter[] para = {
                new SqlParameter("@BGIsValid",EnabledEnum.Enabled.GetHashCode())
            };
            var sql = "SELECT Id,BGCode,BGName,BGIsValid FROM " + tableName + " WITH(NOLOCK) WHERE BGIsValid=@BGIsValid";
            var ds = ExecuteDataSet(CommandType.Text, sql, null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = Common.DataConvertHelper.DataTableToList<GroupModel>(dt);
            }

            return list;
        }

        public long InsertGroup(GroupModel model, SqlTransaction tran)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"INSERT INTO {0} (BGCode,BGName,BGIsValid) VALUES (@BGCode,@BGName,@BGIsValid) select id = scope_identity()", tableName);

            SqlParameter[] para = {
                new SqlParameter("@BGCode", model.BGCode),
                new SqlParameter("@BGName",model.BGName),
                new SqlParameter("@BGIsValid",model.BGIsValid),
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

        public List<RoleView> SearchRole(RoleSearchViewModel search, out int totalCount)
        {
            var list = new List<RoleView>();
            var sql = new StringBuilder();
            var countSql = new StringBuilder();
            var whereSql = new StringBuilder();
            whereSql.Append(" WHERE 1=1 ");
            if (!string.IsNullOrEmpty(search.GroupName))
            {
                whereSql.Append(string.Format(" AND BGName LIKE '%{0}%'", search.GroupName));
            }

            sql.Append(string.Format(@"
                SELECT newTable.*
                FROM    (
                        SELECT TOP ( {0} * {1} )
                        ROW_NUMBER() OVER (ORDER BY Id DESC) RowNum
                        ,Id AS RoleId
                        ,BGCode AS RoleCode
                        ,BGName AS Rolename
                        FROM {2} WITH(NOLOCK) {3}
                        Order BY Id DESC ) newTable
                WHERE newTable.RowNum>(({0}-1)*{1})
            ", search.CurrentPage, search.PageSize, tableName, whereSql.ToString()));

            countSql.Append(string.Format(@"SELECT COUNT(1) FROM {0} WITH(NOLOCK) {1} ", tableName, whereSql.ToString()));

            var ds = ExecuteDataSet(CommandType.Text, sql.ToString());
            totalCount = ExecuteCount(CommandType.Text, countSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<RoleView>(dt);
            }
            return list;
        }
    }
}
