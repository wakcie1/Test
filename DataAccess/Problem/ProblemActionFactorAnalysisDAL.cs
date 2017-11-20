using Model.CommonModel;
using Model.Problem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class ProblemActionFactorAnalysisDAL : NewSqlHelper
    {
        private const string tableName = "T_PROBLEM_ACTION_FACTORANALYSIS";

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(ProblemActionFactorAnalysisModel model)
        {
            var sql = @"INSERT INTO " + tableName +
                @" ([PAFType]
                   ,[PAFPossibleCause]
                   ,[PAFWhat]
                   ,[PAFWhoNo]
                   ,[PAFWho]
                   ,[PAFValidatedDate]
                   ,[PAFPotentialCause]
                   ,[PAFIsValid]
                   ,[PAFCreateUserNo]
                   ,[PAFCreateUserName]
                   ,[PAFCreateTime]
                   ,[PAFOperateUserNo]
                   ,[PAFOperateUserName]
                   ,[PAFOperateTime]
                   ,[PAFProblemId])" +
                @" VALUES (@PAFType
                   ,@PAFPossibleCause
                   ,@PAFWhat
                   ,@PAFWhoNo
                   ,@PAFWho
                   ,@PAFValidatedDate
                   ,@PAFPotentialCause
                   ,@PAFIsValid
                   ,@PAFCreateUserNo
                   ,@PAFCreateUserName
                   ,@PAFCreateTime
                   ,@PAFOperateUserNo
                   ,@PAFOperateUserName
                   ,@PAFOperateTime
                   ,@PAFProblemId) " +
                "  select id = scope_identity()";
            SqlParameter[] para = {
                new SqlParameter("@PAFType", model.PAFType),
                new SqlParameter("@PAFPossibleCause", model.PAFPossibleCause),
                new SqlParameter("@PAFWhat", string.IsNullOrEmpty(model.PAFWhat)?string.Empty:model.PAFWhat),
                new SqlParameter("@PAFWhoNo", string.IsNullOrEmpty(model.PAFWhoNo)?string.Empty:model.PAFWhoNo),
                new SqlParameter("@PAFWho", string.IsNullOrEmpty(model.PAFWho)?string.Empty:model.PAFWho),
                new SqlParameter("@PAFValidatedDate",  model.PAFValidatedDate ?? Convert.ToDateTime("1900-1-1")),
                new SqlParameter("@PAFPotentialCause", string.IsNullOrEmpty(model.PAFPotentialCause)?string.Empty:model.PAFPotentialCause),
                new SqlParameter("@PAFIsValid",model.PAFIsValid),
                new SqlParameter("@PAFCreateUserNo",model.PAFCreateUserNo),
                new SqlParameter("@PAFCreateUserName",model.PAFCreateUserName),
                new SqlParameter("@PAFCreateTime",model.PAFCreateTime),
                new SqlParameter("@PAFOperateUserNo",model.PAFOperateUserNo),
                new SqlParameter("@PAFOperateUserName",model.PAFOperateUserName),
                new SqlParameter("@PAFOperateTime",model.PAFOperateTime),
                new SqlParameter("@PAFProblemId",model.PAFProblemId)
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

        public bool Update(ProblemActionFactorAnalysisModel model)
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

            if (!string.IsNullOrEmpty(model.PAFType))
            {
                paramsql.Append(" [PAFType] = @PAFType ,");
                param.Add(new SqlParameter("@PAFType", model.PAFType));
            }
            if (!string.IsNullOrEmpty(model.PAFPossibleCause))
            {
                paramsql.Append(" [PAFPossibleCause] = @PAFPossibleCause ,");
                param.Add(new SqlParameter("@PAFPossibleCause", model.PAFPossibleCause));
            }
            if (!string.IsNullOrEmpty(model.PAFWhat))
            {
                paramsql.Append(" [PAFWhat] = @PAFWhat ,");
                param.Add(new SqlParameter("@PAFWhat", model.PAFWhat));
            }
            if (!string.IsNullOrEmpty(model.PAFWhoNo))
            {
                paramsql.Append(" [PAFWhoNo] = @PAFWhoNo ,");
                param.Add(new SqlParameter("@PAFWhoNo", model.PAFWhoNo));
            }
            if (!string.IsNullOrEmpty(model.PAFWho))
            {
                paramsql.Append(" [PAFWho] = @PAFWho ,");
                param.Add(new SqlParameter("@PAFWho", model.PAFWho));
            }
            if (model.PAFValidatedDate != null && model.PAFValidatedDate > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                paramsql.Append(" [PAFValidatedDate] = @PAFValidatedDate ,");
                param.Add(new SqlParameter("@PAFValidatedDate", model.PAFValidatedDate));
            }

            if (!string.IsNullOrEmpty(model.PAFPotentialCause))
            {
                paramsql.Append(" [PAFPotentialCause] = @PAFPotentialCause ,");
                param.Add(new SqlParameter("@PAFPotentialCause", model.PAFPotentialCause));
            }
            paramsql.Append(" [PAFIsValid] = @PAFIsValid ,");
            param.Add(new SqlParameter("@PAFIsValid", model.PAFIsValid));

            if (!string.IsNullOrEmpty(model.PAFCreateUserNo))
            {
                paramsql.Append(" [PAFCreateUserNo] = @PAFCreateUserNo ,");
                param.Add(new SqlParameter("@PAFCreateUserNo", model.PAFCreateUserNo));
            }
            if (!string.IsNullOrEmpty(model.PAFCreateUserName))
            {
                paramsql.Append(" [PAFCreateUserName] = @PAFCreateUserName ,");
                param.Add(new SqlParameter("@PAFCreateUserName", model.PAFCreateUserName));
            }
            if (model.PAFCreateTime != null && model.PAFCreateTime > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                paramsql.Append(" [PAFCreateTime] = @PAFCreateTime ,");
                param.Add(new SqlParameter("@PAFCreateTime", model.PAFCreateTime));
            }
            if (!string.IsNullOrEmpty(model.PAFOperateUserNo))
            {
                paramsql.Append(" [PAFOperateUserNo] = @PAFOperateUserNo ,");
                param.Add(new SqlParameter("@PAFOperateUserNo", model.PAFOperateUserNo));
            }
            if (!string.IsNullOrEmpty(model.PAFOperateUserName))
            {
                paramsql.Append(" [PAFOperateUserName] = @PAFOperateUserName ,");
                param.Add(new SqlParameter("@PAFOperateUserName", model.PAFOperateUserName));
            }
            if (model.PAFOperateTime != null && model.PAFOperateTime > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                paramsql.Append(" [PAFOperateTime] = @PAFOperateTime ,");
                param.Add(new SqlParameter("@PAFOperateTime", model.PAFOperateTime));
            }
            if (model.PAFProblemId > 0)
            {
                paramsql.Append(" [PAFProblemId] = @PAFProblemId ,");
                param.Add(new SqlParameter("@PAFProblemId", model.PAFProblemId));
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
            upsql.Append(" [PAFIsValid] = @PAFIsValid ,");
            upsql.Append(" [PAFOperateUserNo] = @PAFOperateUserNo ,");
            upsql.Append(" [PAFOperateUserName] = @PAFOperateUserName ,");
            upsql.Append(" [PAFOperateTime] = @PAFOperateTime ");
            upsql.Append(" WHERE Id = @Id ");
            var value = 0;
            sqlparam.Add(new SqlParameter("@PAFIsValid", value));
            sqlparam.Add(new SqlParameter("@PAFOperateUserNo", param.JobNum));
            sqlparam.Add(new SqlParameter("@PAFOperateUserName", param.Name));
            sqlparam.Add(new SqlParameter("@PAFOperateTime", param.InvalidTime));
            sqlparam.Add(new SqlParameter("@Id", param.Id));
            return ExecteNonQuery(CommandType.Text, upsql.ToString(), null, sqlparam) > 0;
        }

    }
}
