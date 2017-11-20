using Common;
using Model.TableModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class CommonDAL : NewSqlHelper
    {
        private const string tableName = "T_BASE_CODE";

        public List<CodeModel> GetCodeList(string catrgory)
        {
            var list = new List<CodeModel>();
            var selectSql = new StringBuilder();
            selectSql.AppendFormat(@"SELECT [Id]
                                          ,[BCCode]
                                          ,[BCCodeDesc]
                                          ,[BCCategory]
                                          ,[BCCodeOrder]
                                          ,[BCIsValid]
                                          ,[BCCreateUserNo]
                                          ,[BCCreateUserName]
                                          ,[BCCreateTime]
                                          ,[BCOperateUserNo]
                                          ,[BCOperateUserName]
                                          ,[BCOperateTime]
                                      FROM {0} with(NOLOCK) ", tableName);
            selectSql.AppendFormat(" WHERE [BCIsValid] = 1 ");

            selectSql.AppendFormat(" AND BCCategory= '{0}' ", catrgory);
            selectSql.AppendFormat(" ORDER BY BCCodeOrder ASC ");

            var ds = ExecuteDataSet(CommandType.Text, selectSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<CodeModel>(dt);
            }
            return list;
        }
    }
}
