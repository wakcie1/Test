using Common;
using Model.Code;
using Model.TableModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class CodeDefectDAL : NewSqlHelper
    {
        private const string tableName = "T_BASE_DEFECT_CODE";

        /// <summary>
        /// Search Mode by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CodeDefectModel GetOneDefectCode(int id = 0)
        {
            var result = new CodeDefectModel();
            SqlParameter[] para = {
                new SqlParameter("@Id", id)
            };

            var sql = @"SELECT TOP 1 [Id]
                                  ,[BDCodeNameEn]
                                  ,[BDCodeNameCn]
                                  ,[BDCode]
                                  ,[BDCodeNo]
                                  ,[BDCodeType]
                                  ,[BDIsValid]
                                  ,[BDCreateUserNo]
                                  ,[BDCreateUserName]
                                  ,[BDCreateTime]
                                  ,[BDOperateUserNo]
                                  ,[BDOperateUserName]
                                  ,[BDOperateTime]
                        FROM " + tableName + " with(NOLOCK) WHERE Id=@Id";

            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    result = DataConvertHelper.DataTableToList<CodeDefectModel>(dt)[0];
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
            return result;
        }
        /// <summary>
        /// insert Table
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertModel(CodeDefectModel model)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@" 
                            INSERT INTO {0}
                            ([BDCodeNameEn]
                            ,[BDCodeNameCn]
                            ,[BDCode]
                            ,[BDCodeType]
                            ,[BDCodeNo]
                            ,[BDIsValid]
                            ,[BDCreateUserNo]
                            ,[BDCreateUserName]
                            ,[BDCreateTime]
                            ,[BDOperateUserNo]
                            ,[BDOperateUserName]
                            ,[BDOperateTime])
                            VALUES
                            (@BDCodeNameEn
                            ,@BDCodeNameCn
                            ,@BDCode
                            ,@BDCodeType
                            ,@BDCodeNo
                            ,@BDIsValid
                            ,@BDCreateUserNo
                            ,@BDCreateUserName
                            ,@BDCreateTime
                            ,@BDOperateUserNo
                            ,@BDOperateUserName
                            ,@BDOperateTime)
                select id = scope_identity() ", tableName);

            SqlParameter[] para = {
                new SqlParameter("@BDCodeNameEn",model.BDCodeNameEn),
                new SqlParameter("@BDCodeNameCn",model.BDCodeNameCn),
                new SqlParameter("@BDCode",model.BDCode),
                new SqlParameter("@BDCodeType",model.BDCodeType),
                new SqlParameter("@BDCodeNo",model.BDCodeNo),
                new SqlParameter("@BDIsValid",model.BDIsValid),
                new SqlParameter("@BDCreateUserNo",model.BDCreateUserNo),
                new SqlParameter("@BDCreateUserName",model.BDCreateUserName),
                new SqlParameter("@BDCreateTime",model.BDCreateTime),
                new SqlParameter("@BDOperateUserNo",model.BDOperateUserNo),
                new SqlParameter("@BDOperateUserName",model.BDOperateUserName),
                new SqlParameter("@BDOperateTime",model.BDOperateTime)
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

        public IEnumerable<CodeDefectModel> SearchCodeDefectPageList(DefectCodeSearchModel param, out int totalCount)
        {
            var list = new List<CodeDefectModel>();
            var selectSql = new StringBuilder();
            var countSql = new StringBuilder();
            var whereSql = new StringBuilder();
            whereSql.Append(" WHERE 1 = 1 ");
            if (!string.IsNullOrEmpty(param.code))
            {
                whereSql.Append(string.Format(" AND (BDCodeNameEn like '%{0}%' OR BDCodeNameCn like N'%{0}%')", param.code));
            }
            if (!string.IsNullOrEmpty(param.codetype))
            {
                whereSql.Append(string.Format(" AND BDCodeType like '%{0}%'", param.codetype));
            }

            selectSql.Append(string.Format(@"
                SELECT  newTable.*
                FROM    ( 
                        SELECT TOP ( {0} * {1} )
                                ROW_NUMBER() OVER ( ORDER BY BDCodeType , BDCodeNo desc) RowNum
                                    ,[Id]
                                    ,[BDCodeNameEn]
                                    ,[BDCodeNameCn]
                                    ,[BDCodeType]
                                    ,[BDCode]
                                    ,[BDCodeNo]
                                    ,[BDIsValid]
                                    ,[BDCreateUserNo]
                                    ,[BDCreateUserName]
                                    ,[BDCreateTime]
                                    ,[BDOperateUserNo]
                                    ,[BDOperateUserName]
                                    ,[BDOperateTime]
                            FROM {2} with(NOLOCK) {3} 
                            ORDER BY BDCodeType , BDCodeNo desc) newTable
                WHERE   newTable.RowNum > ( ( {0} - 1 ) * {1} )  
            ", param.CurrentPage, param.PageSize, tableName, whereSql.ToString()));
            countSql.Append(string.Format(@"SELECT COUNT(1) FROM {0} with(NOLOCK) {1} ", tableName, whereSql.ToString()));

            var ds = ExecuteDataSet(CommandType.Text, selectSql.ToString());
            totalCount = ExecuteCount(CommandType.Text, countSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<CodeDefectModel>(dt);
            }
            return list;
        }


        public List<CodeDefectModel> GetDefectCodeTypeList()
        {
            List<CodeDefectModel> list = new List<CodeDefectModel>();
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" SELECT DISTINCT [BDCodeType]
                                FROM {0} with(NOLOCK) ", tableName);
            sql.Append(" WHERE 1=1 AND BDIsValid=1 ");
            var ds = ExecuteDataSet(CommandType.Text, sql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<CodeDefectModel>(dt);
            }
            return list;
        }

        public List<CodeDefectModel> GetDefectCodeByType(string type)
        {
            List<CodeDefectModel> list = new List<CodeDefectModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@" SELECT [Id]
                                  ,[BDCodeNameEn]
                                  ,[BDCodeNameCn]
                                  ,[BDCode]
                                  ,[BDCodeType]
                                  ,[BDCodeNo]
                                  ,[BDIsValid]
                                  ,[BDCreateUserNo]
                                  ,[BDCreateUserName]
                                  ,[BDCreateTime]
                                  ,[BDOperateUserNo]
                                  ,[BDOperateUserName]
                                  ,[BDOperateTime]
                                  FROM {0} with(NOLOCK) ", tableName);
            sql.Append(" WHERE 1=1 AND BDIsValid=1 ");
            if (!string.IsNullOrEmpty(type))
            {
                sql.AppendFormat("AND BDCodeType= '{0}' ", type);
            }
            var ds = ExecuteDataSet(CommandType.Text, sql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<CodeDefectModel>(dt);
            }
            return list;
        }

        /// <summary>
        /// update model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(CodeDefectModel model)
        {
            if (model.Id == 0)
            {
                return false;
            }
            var upsql = new StringBuilder();
            var paramsql = new StringBuilder();
            var param = new List<SqlParameter>();
            upsql.Append(string.Format("UPDATE {0} SET ", tableName));

            #region param

            if (!string.IsNullOrEmpty(model.BDCodeNameEn))
            {
                paramsql.Append(" [BDCodeNameEn] = @BDCodeNameEn  ,");
                param.Add(new SqlParameter("@BDCodeNameEn", model.BDCodeNameEn));
            }
            if (!string.IsNullOrEmpty(model.BDCodeNameCn))
            {
                paramsql.Append(" [BDCodeNameCn] = @BDCodeNameCn ,");
                param.Add(new SqlParameter("@BDCodeNameCn", model.BDCodeNameCn));
            }
            if (!string.IsNullOrEmpty(model.BDCodeType))
            {
                paramsql.Append(" [BDCodeType] = @BDCodeType ,");
                param.Add(new SqlParameter("@BDCodeType", model.BDCodeType));
            }
            if (!string.IsNullOrEmpty(model.BDCodeType))
            {
                paramsql.Append(" [BDCode] = @BDCode ,");
                param.Add(new SqlParameter("@BDCode", model.BDCode));
            }
            if (model.BDCodeNo > 0)
            {
                paramsql.Append(" [BDCodeNo] = @BDCodeNo,");
                param.Add(new SqlParameter("@BDCodeNo", model.BDCodeNo));
            }
            if (model.BDIsValid != null)
            {
                paramsql.Append(" [BDIsValid] = @BDIsValid ,");
                param.Add(new SqlParameter("@BDIsValid", model.BDIsValid));
            }
            if (!string.IsNullOrEmpty(model.BDCreateUserNo))
            {
                paramsql.Append(" [BDCreateUserNo] = @BDCreateUserNo ,");
                param.Add(new SqlParameter("@BDCreateUserNo", model.BDCreateUserNo));
            }
            if (!string.IsNullOrEmpty(model.BDCreateUserName))
            {
                paramsql.Append(" [BDCreateUserName] = @BDCreateUserName ,");
                param.Add(new SqlParameter("@BDCreateUserName", model.BDCreateUserName));
            }
            if (model.BDCreateTime != null)
            {
                paramsql.Append(" [BDCreateTime] = @BDCreateTime ,");
                param.Add(new SqlParameter("@BDCreateTime", model.BDCreateTime));
            }
            if (!string.IsNullOrEmpty(model.BDOperateUserNo))
            {
                paramsql.Append(" [BDOperateUserNo] = @BDOperateUserNo ,");
                param.Add(new SqlParameter("@BDOperateUserNo", model.BDOperateUserNo));
            }
            if (!string.IsNullOrEmpty(model.BDOperateUserName))
            {
                paramsql.Append(" [BDOperateUserName] = @BDOperateUserName ,");
                param.Add(new SqlParameter("@BDOperateUserName", model.BDOperateUserName));
            }
            if (model.BDOperateTime != null)
            {
                paramsql.Append(" [BDOperateTime] = @BDOperateTime ,");
                param.Add(new SqlParameter("@BDOperateTime", model.BDOperateTime));
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
    }
}
