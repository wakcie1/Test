using Common;
using Common.Enum;
using Model.TableModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class RoleDAL : NewSqlHelper
    {
        private const string tableName = "T_BASE_ROLE";

        public RoleModel GetRoleByName(string roleName)
        {
            var roleModel = new RoleModel();
            SqlParameter[] para = {
                new SqlParameter("@BRName", roleName),
                new SqlParameter("@BRIsValid",EnabledEnum.Enabled.GetHashCode())
            };

            var sql = "SELECT Id,BRCode,BRName,BRType,BRIsValid FROM " + tableName + " WITH(NOLOCK) WHERE BRName=@BRName AND BRIsValid=@BRIsValid";
            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                roleModel = DataConvertHelper.DataTableToList<RoleModel>(dt)[0];
            }
            else
            {
                return null;
            }
            //using (var dr = ExecuteReader(CommandType.Text, sql, null, para))
            //{
            //    if (dr.Read())
            //    {
            //        roleModel.Id = long.Parse(dr["Id"].ToString());
            //        roleModel.BRCode = dr["BRCode"].ToString();
            //        roleModel.BRName = dr["BRName"].ToString();
            //        roleModel.BRType = Convert.ToInt32(dr["BRType"]);
            //        roleModel.BRIsValid = Convert.ToInt32(dr["BRIsValid"]);
            //        dr.Close();
            //    }
            //    else
            //    {
            //        roleModel = null;
            //    }
            //}
            return roleModel;
        }

        public RoleModel GetRoleById(long roleId)
        {
            var roleModel = new RoleModel();
            SqlParameter[] para = {
                new SqlParameter("@Id", roleId),
                new SqlParameter("@BRIsValid",EnabledEnum.Enabled.GetHashCode())
            };

            var sql = "SELECT Id,BRCode,BRName,BRType,BRIsValid FROM " + tableName + " WITH(NOLOCK) WHERE Id=@Id AND BRIsValid=@BRIsValid";

            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                roleModel = DataConvertHelper.DataTableToList<RoleModel>(dt)[0];
            }
            else
            {
                return null;
            }

            //using (var dr = ExecuteReader(CommandType.Text, sql, null, para))
            //{
            //    if (dr.Read())
            //    {
            //        roleModel.Id = long.Parse(dr["Id"].ToString());
            //        roleModel.BRCode = dr["BRCode"].ToString();
            //        roleModel.BRName = dr["BRName"].ToString();
            //        roleModel.BRType = Convert.ToInt32(dr["BRType"]);
            //        roleModel.BRIsValid = Convert.ToInt32(dr["BRIsValid"]);
            //        dr.Close();
            //    }
            //    else
            //    {
            //        roleModel = null;
            //    }
            //}
            return roleModel;
        }

        public bool InsertRole(RoleModel model, SqlTransaction tran)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"INSERT INTO {0} (BRCode,BRName,BRType,BRIsValid) VALUES (@BRCode,@BRName,@BRType,@BRIsValid)", tableName);

            SqlParameter[] para = {
                new SqlParameter("@BRCode", model.BRCode),
                new SqlParameter("@BRName",model.BRName),
                new SqlParameter("@BRType",model.BRType),
                new SqlParameter("@BRIsValid",model.BRIsValid),
            };
            int cmdresult = Convert.ToInt32(ExecuteScalar(CommandType.Text, sql.ToString(), tran, para));
            return cmdresult > 0;
        }
    }
}
