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
    public class LogDAL : NewSqlHelper
    {
        private const string tableName = "T_BASE_LOG";

        /// <summary>
        /// Search Mode by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<LogModel> SearchModelById(int id = 0)
        {
            List<LogModel> list = new List<LogModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@" SELECT [Id]
                                      ,[BLCode]
                                      ,[BLLogDesc]
                                      ,[BLLogType]
                                      ,[BLFilterValue1]
                                      ,[BLFilterValue2]
                                      ,[BLFilterId1]
                                      ,[BLFilterId2]
                                      ,[BLIsValid] 
                                      ,[BLCreateUserNo]
                                      ,[BLCreateUserName]
                                      ,[BLCreateTime]
                                  FROM {0} with(NOLOCK)  ", tableName);
            sql.Append(" WHERE 1=1 ");
            if (!id.Equals(0))
            {
                sql.AppendFormat("AND Id={0}", id);
            }
            var ds = ExecuteDataSet(CommandType.Text, sql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<LogModel>(dt);
            }

            return list.AsEnumerable();
        }
        /// <summary>
        /// insert Table
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertModel(LogModel model)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@" 
                INSERT INTO {0}
                               ([BLCode]
                               ,[BLLogDesc]
                               ,[BLLogType]
                               ,[BLFilterValue1]
                               ,[BLFilterValue2]
                               ,[BLFilterId1]
                               ,[BLFilterId2]
                               ,[BLIsValid] 
                               ,[BLCreateUserNo]
                               ,[BLCreateUserName]
                               ,[BLCreateTime])
                         VALUES
                               (
		                         @BLCode
                               , @BLLogDesc
                               , @BLLogType
                               , @BLFilterValue1
                               , @BLFilterValue2
                               , @BLFilterId1
                               , @BLFilterId2
                               , @BLIsValid 
                               , @BLCreateUserNo
                               , @BLCreateUserName
                               , @BLCreateTime)
                ", tableName);

            SqlParameter[] para = {
                new SqlParameter("@BLCode",model.BLCode),
                new SqlParameter("@BLLogDesc",model.BLLogDesc),
                new SqlParameter("@BLLogType",model.BLLogType),
                new SqlParameter("@BLFilterValue1",model.BLFilterValue1),
                new SqlParameter("@BLFilterValue2",model.BLFilterValue2),
                new SqlParameter("@BLFilterId1",model.BLFilterId1),
                new SqlParameter("@BLFilterId2",model.BLFilterId2),
                new SqlParameter("@BLIsValid",model.BLIsValid),
                new SqlParameter("@BLCreateUserNo",model.BLCreateUserNo),
                new SqlParameter("@BLCreateUserName",model.BLCreateUserName),
                new SqlParameter("@BLCreateTime",model.BLCreateTime)
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

        /// <summary>
        /// update model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(LogModel model)
        {
            var sql = @"UPDATE  " + tableName +
                 @" SET [BLCode] = @BLCode
                      ,[BLLogDesc] = @BLLogDesc
                      ,[BLLogType] = @BLLogType
                      ,[BLFilterValue1] = @BLFilterValue1
                      ,[BLFilterValue2] = @BLFilterValue2
                      ,[BLFilterId1] = @BLFilterId1
                      ,[BLFilterId2] = @BLFilterId2
                      ,[BLIsValid] = @BLIsValid 
                      ,[BLCreateUserNo] = @BLCreateUserNo
                      ,[BLCreateUserName] = @BLCreateUserName
                      ,[BLCreateTime] = @BLCreateTime WHERE Id = @Id";
            SqlParameter[] para = {
                new SqlParameter("@Id",model.Id),
               new SqlParameter("@BLCode",model.BLCode),
                new SqlParameter("@BLLogDesc",model.BLLogDesc),
                new SqlParameter("@BLLogType",model.BLLogType),
                new SqlParameter("@BLFilterValue1",model.BLFilterValue1),
                new SqlParameter("@BLFilterValue2",model.BLFilterValue2),
                new SqlParameter("@BLFilterId1",model.BLFilterId1),
                new SqlParameter("@BLFilterId2",model.BLFilterId2),
                new SqlParameter("@BLIsValid",model.BLIsValid),
                new SqlParameter("@BLCreateUserNo",model.BLCreateUserNo),
                new SqlParameter("@BLCreateUserName",model.BLCreateUserName),
                new SqlParameter("@BLCreateTime",model.BLCreateTime)
            };
            return ExecteNonQuery(CommandType.Text, sql, null, para) > 0;
        }

        public List<LogModel> SearchProblemFollew(string code = "")
        {
            List<LogModel> list = new List<LogModel>();
            StringBuilder sql = new StringBuilder();
            SqlParameter[] para = {
                new SqlParameter("@BLLogType", LogTypeEnum.ProblemLog.GetHashCode()),
            };

            sql.AppendFormat(@" SELECT [Id]
                                      ,[BLCode]
                                      ,[BLLogDesc]
                                      ,[BLLogType]
                                      ,[BLFilterValue1]
                                      ,[BLFilterValue2]
                                      ,[BLFilterId1]
                                      ,[BLFilterId2]
                                      ,[BLIsValid] 
                                      ,[BLCreateUserNo]
                                      ,[BLCreateUserName]
                                      ,[BLCreateTime]
                                  FROM {0} with(NOLOCK) ", tableName);
            sql.Append(" WHERE 1=1 ");
            if (!string.IsNullOrEmpty(code))
            {
                sql.AppendFormat("AND BLCode= '{0}' ", code);
            }
            sql.Append(" ORDER BY BLCreateTime ASC");
            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<LogModel>(dt);
            }

            return list;
        }
    }
}
