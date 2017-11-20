using Common;
using Common.Enum;
using Model.Problem;
using Model.TableModel;
using Model.ViewModel.Department;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess
{
    public class ProblemSortingActivityDAL : NewSqlHelper
    {
        private const string tableName = "T_PROBLEM_SORTINGACTIVITY";

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(ProblemSortingActivityModel model)
        {
            var sql = @"INSERT INTO " + tableName +
                    @" ([PSAProblemId]
                    ,[PSAValueStreamNo]
                    ,[PSAValueStream]
                    ,[PSADefectQty]
                    ,[PSASortedQty]
                    ,[PSADeadLine]
                    ,[PSAIsValid]
                    ,[PSACreateUserNo]
                    ,[PSACreateUserName]
                    ,[PSACreateTime]
                    ,[PSAOperateUserNo]
                    ,[PSAOperateUserName]
                    ,[PSAOperateTime])
                    VALUES
                    (@PSAProblemId
                    ,@PSAValueStreamNo
                    ,@PSAValueStream
                    ,@PSADefectQty
                    ,@PSASortedQty
                    ,@PSADeadLine
                    ,@PSAIsValid
                    ,@PSACreateUserNo
                    ,@PSACreateUserName
                    ,@PSACreateTime
                    ,@PSAOperateUserNo
                    ,@PSAOperateUserName
                    ,@PSAOperateTime) " +
                    "  select id = scope_identity()";
            SqlParameter[] para = {
                new SqlParameter("@PSAProblemId",model.PSAProblemId),
                new SqlParameter("@PSAValueStreamNo",model.PSAValueStreamNo),
                new SqlParameter("@PSAValueStream",model.PSAValueStream),
                new SqlParameter("@PSADefectQty",model.PSADefectQty ?? 0),
                new SqlParameter("@PSASortedQty",model.PSASortedQty ?? 0),
                new SqlParameter("@PSADeadLine",model.PSADeadLine ?? Convert.ToDateTime("1900-1-1")),
                new SqlParameter("@PSAIsValid",model.PSAIsValid),
                new SqlParameter("@PSACreateUserNo",model.PSACreateUserNo),
                new SqlParameter("@PSACreateUserName",model.PSACreateUserName),
                new SqlParameter("@PSACreateTime",model.PSACreateTime),
                new SqlParameter("@PSAOperateUserNo",model.PSAOperateUserNo),
                new SqlParameter("@PSAOperateUserName",model.PSAOperateUserName),
                new SqlParameter("@PSAOperateTime",model.PSAOperateTime),
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

        public bool Update(ProblemSortingActivityModel model)
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

            if (!string.IsNullOrEmpty(model.PSAValueStream))
            {
                paramsql.Append(" [PSAValueStream] = @PSAValueStream ,");
                param.Add(new SqlParameter("@PSAValueStream", model.PSAValueStream));
            }
            if (model.PSAValueStreamNo > 0)
            {
                paramsql.Append(" [PSAValueStreamNo] = @PSAValueStreamNo ,");
                param.Add(new SqlParameter("@PSAValueStreamNo", model.PSAValueStreamNo));
            }
            if (model.PSADefectQty != null)
            {
                paramsql.Append(" [PSADefectQty] = @PSADefectQty ,");
                param.Add(new SqlParameter("@PSADefectQty", model.PSADefectQty));
            }
            if (model.PSASortedQty != null)
            {
                paramsql.Append(" [PSASortedQty] = @PSASortedQty ,");
                param.Add(new SqlParameter("@PSASortedQty", model.PSASortedQty));
            }
            if (model.PSADeadLine != null && model.PSADeadLine > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                paramsql.Append(" [PSADeadLine] = @PSADeadLine ,");
                param.Add(new SqlParameter("@PSADeadLine", model.PSADeadLine));
            }
            if (model.PSAIsValid != null)
            {
                paramsql.Append(" [PSAIsValid] = @PSAIsValid ,");
                param.Add(new SqlParameter("@PSAIsValid", model.PSAIsValid));
            }
            if (!string.IsNullOrEmpty(model.PSACreateUserNo))
            {
                paramsql.Append(" [PSACreateUserNo] = @PSACreateUserNo ,");
                param.Add(new SqlParameter("@PSACreateUserNo", model.PSACreateUserNo));
            }
            if (!string.IsNullOrEmpty(model.PSACreateUserName))
            {
                paramsql.Append(" [PSACreateUserName] = @PSACreateUserName ,");
                param.Add(new SqlParameter("@PSACreateUserName", model.PSACreateUserName));
            }
            if (model.PSACreateTime != null)
            {
                paramsql.Append(" [PSACreateTime] = @PSACreateTime ,");
                param.Add(new SqlParameter("@PSACreateTime", model.PSACreateTime));
            }
            if (!string.IsNullOrEmpty(model.PSAOperateUserNo))
            {
                paramsql.Append(" [PSAOperateUserNo] = @PSAOperateUserNo ,");
                param.Add(new SqlParameter("@PSAOperateUserNo", model.PSAOperateUserNo));
            }
            if (!string.IsNullOrEmpty(model.PSAOperateUserName))
            {
                paramsql.Append(" [PSAOperateUserName] = @PSAOperateUserName ,");
                param.Add(new SqlParameter("@PSAOperateUserName", model.PSAOperateUserName));
            }
            if (model.PSAOperateTime != null)
            {
                paramsql.Append(" [PSAOperateTime] = @PSAOperateTime ,");
                param.Add(new SqlParameter("@PSAOperateTime", model.PSAOperateTime));
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

        /// <summary>
        /// 描述：
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public ProblemSortingActivityModel GetSolvingActivityById(int id)
        {
            var material = new ProblemSortingActivityModel();
            SqlParameter[] para = {
                new SqlParameter("@Id", id)
            };

            var sql = @"SELECT TOP 1 [Id]
                          ,[PSAProblemId]
                          ,[PSAValueStreamNo]
                          ,[PSAValueStream]
                          ,[PSADefectQty]
                          ,[PSASortedQty]
                          ,[PSADeadLine]
                          ,[PSAIsValid]
                          ,[PSACreateUserNo]
                          ,[PSACreateUserName]
                          ,[PSACreateTime]
                          ,[PSAOperateUserNo]
                          ,[PSAOperateUserName]
                          ,[PSAOperateTime]
                        FROM " + tableName + " with(NOLOCK) WHERE Id=@Id";
            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                material = DataConvertHelper.DataTableToList<ProblemSortingActivityModel>(dt)[0];
            }
            else
            {
                return null;
            }
            return material;
        }
    }
}
