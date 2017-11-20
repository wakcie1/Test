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
    public class CodeDAL : NewSqlHelper
    {
        private const string tableName = "T_BASE_CODE";

        /// <summary>
        /// Search Mode by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<CodeModel> SearchModelById(int id = 0)
        {
            List<CodeModel> list = new List<CodeModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@" SELECT [Id]
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
            sql.Append(" WHERE 1=1 AND BCIsValid=1 ");
            if (!id.Equals(0))
            {
                sql.AppendFormat("AND Id={0}", id);
            }
            var ds = ExecuteDataSet(CommandType.Text, sql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<CodeModel>(dt);
            }

            return list.AsEnumerable();
        }
        /// <summary>
        /// insert Table
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertModel(CodeModel model)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@" 
                INSERT INTO {0}
                               ([BCCode]
                               ,[BCCodeDesc]
                               ,[BCCategory]
                               ,[BCCodeOrder]
                               ,[BCIsValid]
                               ,[BCCreateUserNo]
                               ,[BCCreateUserName]
                               ,[BCCreateTime]
                               ,[BCOperateUserNo]
                               ,[BCOperateUserName]
                               ,[BCOperateTime])
                         VALUES
                               (
		                         @BCCode
                               , @BCCodeDesc
                               , @BCCategory
                               , @BCCodeOrder
                               , @BCIsValid
                               , @BCCreateUserNo
                               , @BCCreateUserName
                               , @BCCreateTime
                               , @BCOperateUserNo
                               , @BCOperateUserName
                               , @BCOperateTime) 
                select id = scope_identity() ", tableName);

            SqlParameter[] para = {
                new SqlParameter("@BCCode",model.BCCode),
                new SqlParameter("@BCCodeDesc",model.BCCodeDesc),
                new SqlParameter("@BCCategory",model.BCCategory),
                new SqlParameter("@BCCodeOrder",model.BCCodeOrder),
                new SqlParameter("@BCIsValid",model.BCIsValid),
                new SqlParameter("@BCCreateUserNo",model.BCCreateUserNo),
                new SqlParameter("@BCCreateUserName",model.BCCreateUserName),
                new SqlParameter("@BCCreateTime",model.BCCreateTime),
                new SqlParameter("@BCOperateUserNo",model.BCOperateUserNo),
                new SqlParameter("@BCOperateUserName",model.BCOperateUserName),
                new SqlParameter("@BCOperateTime",model.BCOperateTime)
            };

            var result = 0;
            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                var Idstring = ds.Tables[0].Rows[0][0].ToString();
                result = string.IsNullOrEmpty(Idstring) ? 0 : Convert.ToInt32(Idstring);
            }
            return result;
        }

        public IEnumerable<CodeModel> SearchModelByCategory(string key)
        {
            List<CodeModel> list = new List<CodeModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@" SELECT [Id]
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
            sql.Append(" WHERE 1=1 AND BCIsValid=1 ");
            if (!string.IsNullOrEmpty(key))
            {
                sql.AppendFormat("AND BCCategory= '{0}' ", key);
            }
            var ds = ExecuteDataSet(CommandType.Text, sql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<CodeModel>(dt);
            }

            return list.AsEnumerable();

        }
        public IEnumerable<CodeModel> SearchModelByCode(string key, string category)
        {
            List<CodeModel> list = new List<CodeModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@" SELECT [Id]
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
            sql.Append(" WHERE 1=1 ");
            if (!string.IsNullOrEmpty(key))
            {
                sql.AppendFormat("AND BCCode= '{0}' ", key);
            }
            if (!string.IsNullOrWhiteSpace(category))
            {
                sql.AppendFormat("AND BCCategory= '{0}' ", key);
            }
            var ds = ExecuteDataSet(CommandType.Text, sql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<CodeModel>(dt);
            }

            return list.AsEnumerable();

        }
        /// <summary>
        /// update model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(CodeModel model)
        {
            var sql = @"UPDATE  " + tableName +
                 @" SET [BCCode] = @BCCode
                      ,[BCCodeDesc] = @BCCodeDesc
                      ,[BCCategory] = @BCCategory
                      ,[BCCodeOrder] = @BCCodeOrder
                      ,[BCIsValid] = @BCIsValid
                      ,[BCCreateUserNo] = @BCCreateUserNo
                      ,[BCCreateUserName] = @BCCreateUserName
                      ,[BCCreateTime] = @BCCreateTime
                      ,[BCOperateUserNo] = @BCOperateUserNo
                      ,[BCOperateUserName] = @BCOperateUserName
                      ,[BCOperateTime] = @BCOperateTime WHERE Id = @Id";
            SqlParameter[] para = {
                new SqlParameter("@Id",model.Id),
               new SqlParameter("@BCCode",model.BCCode),
                new SqlParameter("@BCCodeDesc",model.BCCodeDesc),
                new SqlParameter("@BCCategory",model.BCCategory),
                new SqlParameter("@BCCodeOrder",model.BCCodeOrder),
                new SqlParameter("@BCIsValid",model.BCIsValid),
                new SqlParameter("@BCCreateUserNo",model.BCCreateUserNo),
                new SqlParameter("@BCCreateUserName",model.BCCreateUserName),
                new SqlParameter("@BCCreateTime",model.BCCreateTime),
                new SqlParameter("@BCOperateUserNo",model.BCOperateUserNo),
                new SqlParameter("@BCOperateUserName",model.BCOperateUserName),
                new SqlParameter("@BCOperateTime",model.BCOperateTime)
            };
            return ExecteNonQuery(CommandType.Text, sql, null, para) > 0;
        }
    }
}
