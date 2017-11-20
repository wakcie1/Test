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
    public class ProblemLayeredAuditDAL : NewSqlHelper
    {
        private const string tableName = "T_PROBLEM_LAYEREDAUDIT";
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(ProblemLayeredAuditModel model)
        {
            var sql = @"INSERT INTO " + tableName +
                @" ([PLWhat]
                   ,[PLWhoNo]
                   ,[PLWho]
                   ,[PLPlanDate]
                   ,[PLActualDate]
                   ,[PLWhere]
                   ,[PLAttachment]
                   ,[PLAttachmentUrl]
                   ,[PLStatus]
                   ,[PLComment]
                   ,[PLIsValid]
                   ,[PLCreateUserNo]
                   ,[PLCreateUserName]
                   ,[PLCreateTime]
                   ,[PLOperateUserNo]
                   ,[PLOperateUserName]
                   ,[PLOperateTime]
                   ,[PLProblemId])" +
                @" VALUES 
                   (@PLWhat 
                   ,@PLWhoNo 
                   ,@PLWho 
                   ,@PLPlanDate 
                   ,@PLActualDate 
                   ,@PLWhere 
                   ,@PLAttachment 
                   ,@PLAttachmentUrl 
                   ,@PLStatus 
                   ,@PLComment 
                   ,@PLIsValid 
                   ,@PLCreateUserNo 
                   ,@PLCreateUserName
                   ,@PLCreateTime 
                   ,@PLOperateUserNo 
                   ,@PLOperateUserName 
                   ,@PLOperateTime 
                   ,@PLProblemId )" +
                "  select id = scope_identity()";
            SqlParameter[] para = {
                new SqlParameter("@PLWhat",  string.IsNullOrEmpty(model.PLWhat)?string.Empty:model.PLWhat),
                new SqlParameter("@PLWhoNo ",string.IsNullOrEmpty(model.PLWhoNo)?string.Empty:model.PLWhoNo),
                new SqlParameter("@PLWho ",string.IsNullOrEmpty(model.PLWho)?string.Empty:model.PLWho),
                new SqlParameter("@PLPlanDate", model.PLPlanDate ?? Convert.ToDateTime("1900-1-1")),
                new SqlParameter("@PLActualDate", model.PLActualDate ?? Convert.ToDateTime("1900-1-1")),
                new SqlParameter("@PLWhere", string.IsNullOrEmpty(model.PLWhere)?string.Empty:model.PLWhere),
                new SqlParameter("@PLAttachment", string.IsNullOrEmpty(model.PLAttachment)?string.Empty: (model.PLAttachment.Equals("D")? string.Empty:model.PLAttachment)),
                new SqlParameter("@PLAttachmentUrl",string.IsNullOrEmpty(model.PLAttachmentUrl)?string.Empty: (model.PLAttachmentUrl.Equals("D")? string.Empty:model.PLAttachmentUrl)),
                new SqlParameter("@PLStatus", string.IsNullOrEmpty(model.PLStatus)?string.Empty:model.PLStatus),
                new SqlParameter("@PLComment",string.IsNullOrEmpty(model.PLComment)?string.Empty:model.PLComment),
                new SqlParameter("@PLIsValid",model.PLIsValid),
                new SqlParameter("@PLCreateUserNo",string.IsNullOrEmpty(model.PLCreateUserNo)?string.Empty:model.PLCreateUserNo),
                new SqlParameter("@PLCreateUserName",string.IsNullOrEmpty(model.PLCreateUserName)?string.Empty:model.PLCreateUserName),
                new SqlParameter("@PLCreateTime",model.PLCreateTime),
                new SqlParameter("@PLOperateUserNo", string.IsNullOrEmpty(model.PLOperateUserNo)?string.Empty:model.PLOperateUserNo),
                new SqlParameter("@PLOperateUserName", string.IsNullOrEmpty(model.PLOperateUserName)?string.Empty:model.PLOperateUserName),
                new SqlParameter("@PLOperateTime", model.PLOperateTime),
                new SqlParameter("@PLProblemId", model.PLProblemId)
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
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(ProblemLayeredAuditModel model)
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
            if (!string.IsNullOrEmpty(model.PLWhat))
            {
                paramsql.Append(" [PLWhat] = @PLWhat  ,");
                param.Add(new SqlParameter("@PLWhat ", model.PLWhat));
            }
            if (!string.IsNullOrEmpty(model.PLWho))
            {
                paramsql.Append(" [PLWho] = @PLWho  ,");
                param.Add(new SqlParameter("@PLWho ", model.PLWho));
            }
            if (!string.IsNullOrEmpty(model.PLWhoNo))
            {
                paramsql.Append(" [PLWhoNo ] = @PLWhoNo  ,");
                param.Add(new SqlParameter("@PLWhoNo ", model.PLWhoNo));
            }
            if (model.PLPlanDate != null && model.PLPlanDate > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                paramsql.Append(" [PLPlanDate] = @PLPlanDate ,");
                param.Add(new SqlParameter("@PLPlanDate", model.PLPlanDate));
            }
            if (model.PLActualDate != null && model.PLActualDate > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                paramsql.Append(" [PLActualDate] = @PLActualDate ,");
                param.Add(new SqlParameter("@PLActualDate", model.PLActualDate));
            }

            if (!string.IsNullOrEmpty(model.PLWhere))
            {
                paramsql.Append(" [PLWhere] = @PLWhere ,");
                param.Add(new SqlParameter("@PLWhere", model.PLWhere));
            }
            if (!string.IsNullOrEmpty(model.PLAttachment))
            {
                if (model.PLAttachment.Equals("D"))
                {
                    paramsql.Append(" [PLAttachment] = '' ,");
                }
                else
                {
                    paramsql.Append(" [PLAttachment] = @PLAttachment ,");
                    param.Add(new SqlParameter("@PLAttachment", model.PLAttachment));
                }
            }
            if (!string.IsNullOrEmpty(model.PLAttachmentUrl))
            {
                if (model.PLAttachmentUrl.Equals("D"))
                {
                    paramsql.Append(" [PLAttachmentUrl] = '' ,");
                }
                else
                {
                    paramsql.Append(" [PLAttachmentUrl] = @PLAttachmentUrl ,");
                    param.Add(new SqlParameter("@PLAttachmentUrl", model.PLAttachmentUrl));
                }
            }
            if (!string.IsNullOrEmpty(model.PLStatus))
            {
                paramsql.Append(" [PLStatus] = @PLStatus ,");
                param.Add(new SqlParameter("@PLStatus", model.PLStatus));
            }
            if (!string.IsNullOrEmpty(model.PLComment))
            {
                paramsql.Append(" [PLComment] = @PLComment ,");
                param.Add(new SqlParameter("@PLComment", model.PLComment));
            }
            if (model.PLIsValid != null)
            {
                paramsql.Append(" [PLIsValid] = @PLIsValid ,");
                param.Add(new SqlParameter("@PLIsValid", model.PLIsValid));
            }
            if (!string.IsNullOrEmpty(model.PLCreateUserNo))
            {
                paramsql.Append(" [PLCreateUserNo] = @PLCreateUserNo ,");
                param.Add(new SqlParameter("@PLCreateUserNo", model.PLCreateUserNo));
            }
            if (!string.IsNullOrEmpty(model.PLCreateUserName))
            {
                paramsql.Append(" [PLCreateUserName] = @PLCreateUserName ,");
                param.Add(new SqlParameter("@PLCreateUserName", model.PLCreateUserName));
            }
            if (model.PLCreateTime != null && model.PLCreateTime > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                paramsql.Append(" [PLCreateTime] = @PLCreateTime ,");
                param.Add(new SqlParameter("@PLCreateTime", model.PLCreateTime));
            }

            if (!string.IsNullOrEmpty(model.PLOperateUserNo))
            {
                paramsql.Append(" [PLOperateUserNo] = @PLOperateUserNo ,");
                param.Add(new SqlParameter("@PLOperateUserNo", model.PLOperateUserNo));
            }

            if (!string.IsNullOrEmpty(model.PLOperateUserName))
            {
                paramsql.Append(" [PLOperateUserName] = @PLOperateUserName ,");
                param.Add(new SqlParameter("@PLOperateUserName", model.PLOperateUserName));
            }
            if (model.PLOperateTime != null && model.PLOperateTime > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                paramsql.Append(" [PLOperateTime] = @PLOperateTime ,");
                param.Add(new SqlParameter("@PLOperateTime", model.PLOperateTime));
            }

            if (model.PLProblemId > 0)
            {
                paramsql.Append(" [PLProblemId] = @PLProblemId ,");
                param.Add(new SqlParameter("@PLProblemId", model.PLProblemId));
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
            upsql.Append(" [PLIsValid] = @PLIsValid ,");
            upsql.Append(" [PLOperateUserNo] = @PLOperateUserNo ,");
            upsql.Append(" [PLOperateUserName] = @PLOperateUserName ,");
            upsql.Append(" [PLOperateTime] = @PLOperateTime ");
            upsql.Append(" WHERE Id = @Id ");
            var value = 0;
            sqlparam.Add(new SqlParameter("@PLIsValid", value));
            sqlparam.Add(new SqlParameter("@PLOperateUserNo", param.JobNum));
            sqlparam.Add(new SqlParameter("@PLOperateUserName", param.Name));
            sqlparam.Add(new SqlParameter("@PLOperateTime", param.InvalidTime));
            sqlparam.Add(new SqlParameter("@Id", param.Id));
            return ExecteNonQuery(CommandType.Text, upsql.ToString(), null, sqlparam) > 0;
        }
    }
}
