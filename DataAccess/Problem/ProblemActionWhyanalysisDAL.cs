using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Common;
using Model.Problem;
using Model.CommonModel;

namespace DataAccess
{
    public class ProblemActionWhyanalysisDAL : NewSqlHelper
    {
        private const string tableName = "T_PROBLEM_ACTION_WHYANALYSIS";

        /// <summary>
        /// insert Table
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(ProblemActionWhyanalysisModel model)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@" 
                INSERT INTO {0}
                    ([PAWWhyForm]
                    ,[PAWWhyQuestionChain]
                    ,[PAWWhy1]
                    ,[PAWWhy2]
                    ,[PAWWhy3]
                    ,[PAWWhy4]
                    ,[PAWWhy5]
                    ,[PAWIsValid]
                    ,[PAWCreateUserNo]
                    ,[PAWCreateUserName]
                    ,[PAWCreateTime]
                    ,[PAWOperateUserNo]
                    ,[PAWOperateUserName]
                    ,[PAWOperateTime]
                    ,[PAWProblemId])
                    VALUES
                    (@PAWWhyForm
                    ,@PAWWhyQuestionChain
                    ,@PAWWhy1
                    ,@PAWWhy2 
                    ,@PAWWhy3
                    ,@PAWWhy4 
                    ,@PAWWhy5
                    ,@PAWIsValid
                    ,@PAWCreateUserNo 
                    ,@PAWCreateUserName 
                    ,@PAWCreateTime
                    ,@PAWOperateUserNo 
                    ,@PAWOperateUserName 
                    ,@PAWOperateTime
                    ,@PAWProblemId);
                select id = scope_identity() ", tableName);

            SqlParameter[] para = {
                new SqlParameter("@PAWWhyForm", string.IsNullOrEmpty(model.PAWWhyForm)?string.Empty:model.PAWWhyForm),
                new SqlParameter("@PAWWhyQuestionChain", string.IsNullOrEmpty(model.PAWWhyQuestionChain) ? string.Empty : model.PAWWhyQuestionChain),
                new SqlParameter("@PAWWhy1", string.IsNullOrEmpty(model.PAWWhy1) ? string.Empty : model.PAWWhy1),
                new SqlParameter("@PAWWhy2", string.IsNullOrEmpty(model.PAWWhy2) ? string.Empty : model.PAWWhy2),
                new SqlParameter("@PAWWhy3", string.IsNullOrEmpty(model.PAWWhy3) ? string.Empty : model.PAWWhy3),
                new SqlParameter("@PAWWhy4", string.IsNullOrEmpty(model.PAWWhy4) ? string.Empty : model.PAWWhy4),
                new SqlParameter("@PAWWhy5", string.IsNullOrEmpty(model.PAWWhy5) ? string.Empty : model.PAWWhy5),
                new SqlParameter("@PAWIsValid",model.PAWIsValid),
                new SqlParameter("@PAWCreateUserNo",model.PAWCreateUserNo),
                new SqlParameter("@PAWCreateUserName",model.PAWCreateUserName),
                new SqlParameter("@PAWCreateTime",model.PAWCreateTime),
                new SqlParameter("@PAWOperateUserNo",model.PAWOperateUserNo),
                new SqlParameter("@PAWOperateUserName",model.PAWOperateUserName),
                new SqlParameter("@PAWOperateTime",model.PAWOperateTime),
                new SqlParameter("@PAWProblemId",model.PAWProblemId)
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
        public bool Update(ProblemActionWhyanalysisModel model)
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
            if (!string.IsNullOrEmpty(model.PAWWhyForm))
            {
                paramsql.Append(" [PAWWhyForm] = @PAWWhyForm ,");
                param.Add(new SqlParameter("@PAWWhyForm", model.PAWWhyForm));
            }
            if (!string.IsNullOrEmpty(model.PAWWhyQuestionChain))
            {
                paramsql.Append(" [PAWWhyQuestionChain] = @PAWWhyQuestionChain ,");
                param.Add(new SqlParameter("@PAWWhyQuestionChain", model.PAWWhyQuestionChain));
            }
            if (!string.IsNullOrEmpty(model.PAWWhy1))
            {
                paramsql.Append(" [PAWWhy1] = @PAWWhy1 ,");
                param.Add(new SqlParameter("@PAWWhy1", model.PAWWhy1));
            }
            if (!string.IsNullOrEmpty(model.PAWWhy2))
            {
                paramsql.Append(" [PAWWhy2] = @PAWWhy2 ,");
                param.Add(new SqlParameter("@PAWWhy2", model.PAWWhy2));
            }
            if (!string.IsNullOrEmpty(model.PAWWhy3))
            {
                paramsql.Append(" [PAWWhy3] = @PAWWhy3 ,");
                param.Add(new SqlParameter("@PAWWhy3", model.PAWWhy3));
            }
            if (!string.IsNullOrEmpty(model.PAWWhy4))
            {
                paramsql.Append(" [PAWWhy4] = @PAWWhy4 ,");
                param.Add(new SqlParameter("@PAWWhy4", model.PAWWhy4));
            }
            if (!string.IsNullOrEmpty(model.PAWWhy5))
            {
                paramsql.Append(" [PAWWhy5] = @PAWWhy5 ,");
                param.Add(new SqlParameter("@PAWWhy5", model.PAWWhy5));
            }
            if (model.PAWIsValid != null)
            {
                paramsql.Append(" [PAWIsValid] = @PAWIsValid ,");
                param.Add(new SqlParameter("@PAWIsValid", model.PAWIsValid));
            }
            if (!string.IsNullOrEmpty(model.PAWCreateUserNo))
            {
                paramsql.Append(" [PAWCreateUserNo] = @PAWCreateUserNo ,");
                param.Add(new SqlParameter("@PAWCreateUserNo", model.PAWCreateUserNo));
            }
            if (!string.IsNullOrEmpty(model.PAWCreateUserName))
            {
                paramsql.Append(" [PAWCreateUserName] = @PAWCreateUserName ,");
                param.Add(new SqlParameter("@PAWCreateUserName", model.PAWCreateUserName));
            }
            if (model.PAWCreateTime != null)
            {
                paramsql.Append(" [PAWCreateTime] = @PAWCreateTime ,");
                param.Add(new SqlParameter("@PAWCreateTime", model.PAWCreateTime));
            }
            if (!string.IsNullOrEmpty(model.PAWOperateUserNo))
            {
                paramsql.Append(" [PAWOperateUserNo] = @PAWOperateUserNo ,");
                param.Add(new SqlParameter("@PAWOperateUserNo", model.PAWOperateUserNo));
            }
            if (!string.IsNullOrEmpty(model.PAWOperateUserName))
            {
                paramsql.Append(" [PAWOperateUserName] = @PAWOperateUserName ,");
                param.Add(new SqlParameter("@PAWOperateUserName", model.PAWOperateUserName));
            }
            if (model.PAWOperateTime != null)
            {
                paramsql.Append(" [PAWOperateTime] = @PAWOperateTime ,");
                param.Add(new SqlParameter("@PAWOperateTime", model.PAWOperateTime));
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

        public bool Invalid(InvalidParam param)
        {
            if (param.Id == 0)
            {
                return false;
            }
            var upsql = new StringBuilder();
            var sqlparam = new List<SqlParameter>();
            upsql.Append(string.Format("UPDATE {0} SET ", tableName));
            upsql.Append(" [PAWIsValid] = @PAWIsValid ,");
            upsql.Append(" [PAWOperateUserNo] = @PAWOperateUserNo ,");
            upsql.Append(" [PAWOperateUserName] = @PAWOperateUserName ,");
            upsql.Append(" [PAWOperateTime] = @PAWOperateTime ");
            upsql.Append(" WHERE Id = @Id ");
            var value = 0;
            sqlparam.Add(new SqlParameter("@PAWIsValid", value));
            sqlparam.Add(new SqlParameter("@PAWOperateUserNo", param.JobNum));
            sqlparam.Add(new SqlParameter("@PAWOperateUserName", param.Name));
            sqlparam.Add(new SqlParameter("@PAWOperateTime", param.InvalidTime));
            sqlparam.Add(new SqlParameter("@Id", param.Id));
            return ExecteNonQuery(CommandType.Text, upsql.ToString(), null, sqlparam) > 0;
        }
    }
}
