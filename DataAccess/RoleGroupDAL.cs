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
    public class RoleGroupDAL : NewSqlHelper
    {
        private const string tableName = "T_BASE_ROLEGROUP";

        /// <summary>
        /// 描述：根据角色包id获取角色分配信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public List<RoleGroupModel> GetRoleGroupByGroupId(long groupId)
        {
            var list = new List<RoleGroupModel>();
            SqlParameter[] para = {
                new SqlParameter("@BRGGroupId", groupId),
                new SqlParameter("@BRGIsValid",EnabledEnum.Enabled.GetHashCode())
            };

            var sql = "SELECT Id,BRGRoleId,BRGGroupId,BRGIsValid FROM " + tableName + " WITH(NOLOCK) WHERE BRGGroupId=@BRGGroupId AND BRGIsValid=@BRGIsValid";
            var ds = ExecuteDataSet(CommandType.Text, sql, null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<RoleGroupModel>(dt);
            }
            return list;
        }

        public bool InsertRoleGroup(RoleGroupModel model)
        {
            var sql = "INSERT INTO " + tableName + " (BRGRoleId,BRGGroupId,BRGIsValid) VALUES (@BRGRoleId,@BRGGroupId,@BRGIsValid)";

            SqlParameter[] para = {
                new SqlParameter("@BRGRoleId", model.BRGRoleId),
                new SqlParameter("@BRGGroupId",model.BRGGroupId),
                new SqlParameter("@BRGIsValid",model.BRGIsValid)
            };
            int cmdresult = Convert.ToInt32(ExecuteScalar(CommandType.Text, sql.ToString(), null, para));
            return cmdresult > 0;
        }
    }
}
