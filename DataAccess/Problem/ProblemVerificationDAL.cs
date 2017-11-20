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
    public class ProblemVerificationDAL : NewSqlHelper
    {
        private const string tableName = "T_PROBLEM_VERIFICATION";

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(ProblemVerificationModel model)
        {
            var sql = @"INSERT INTO " + tableName +
                @" ([PVWhat]
                   ,[PVWhoNo]
                   ,[PVWho]
                   ,[PVPlanDate]
                   ,[PVActualDate]
                   ,[PVWhere]
                   ,[PVAttachment]
                   ,[PVAttachmentUrl]
                   ,[PVStatus]
                   ,[PVComment]
                   ,[PVIsValid]
                   ,[PVCreateUserNo]
                   ,[PVCreateUserName]
                   ,[PVCreateTime]
                   ,[PVOperateUserNo]
                   ,[PVOperateUserName]
                   ,[PVOperateTime]
                   ,[PVProblemId])" +
                @" VALUES
                   (@PVWhat 
                   ,@PVWhoNo 
                   ,@PVWho 
                   ,@PVPlanDate 
                   ,@PVActualDate 
                   ,@PVWhere 
                   ,@PVAttachment 
                   ,@PVAttachmentUrl 
                   ,@PVStatus 
                   ,@PVComment 
                   ,@PVIsValid 
                   ,@PVCreateUserNo 
                   ,@PVCreateUserName
                   ,@PVCreateTime 
                   ,@PVOperateUserNo 
                   ,@PVOperateUserName 
                   ,@PVOperateTime 
                   ,@PVProblemId )" +
                "  select id = scope_identity()";
            SqlParameter[] para = {
                new SqlParameter("@PVWhat",  string.IsNullOrEmpty(model.PVWhat)?string.Empty:model.PVWhat),
                new SqlParameter("@PVWhoNo ",string.IsNullOrEmpty(model.PVWhoNo)?string.Empty:model.PVWhoNo),
                new SqlParameter("@PVWho ",string.IsNullOrEmpty(model.PVWho)?string.Empty:model.PVWho),
                new SqlParameter("@PVPlanDate", model.PVPlanDate ?? Convert.ToDateTime("1900-1-1")),
                new SqlParameter("@PVActualDate", model.PVActualDate ?? Convert.ToDateTime("1900-1-1")),
                new SqlParameter("@PVWhere", string.IsNullOrEmpty(model.PVWhere)?string.Empty:model.PVWhere),
                new SqlParameter("@PVAttachment", string.IsNullOrEmpty(model.PVAttachment)?string.Empty: (model.PVAttachment.Equals("D")? string.Empty:model.PVAttachment)),
                new SqlParameter("@PVAttachmentUrl",string.IsNullOrEmpty(model.PVAttachmentUrl)?string.Empty: (model.PVAttachmentUrl.Equals("D")? string.Empty:model.PVAttachmentUrl)),
                new SqlParameter("@PVStatus",string.IsNullOrEmpty(model.PVStatus)?string.Empty:model.PVStatus),
                new SqlParameter("@PVComment",string.IsNullOrEmpty(model.PVComment)?string.Empty:model.PVComment),
                new SqlParameter("@PVIsValid",model.PVIsValid),
                new SqlParameter("@PVCreateUserNo",string.IsNullOrEmpty(model.PVCreateUserNo)?string.Empty:model.PVCreateUserNo),
                new SqlParameter("@PVCreateUserName",string.IsNullOrEmpty(model.PVCreateUserName)?string.Empty:model.PVCreateUserName),
                new SqlParameter("@PVCreateTime",model.PVCreateTime),
                new SqlParameter("@PVOperateUserNo", string.IsNullOrEmpty(model.PVOperateUserNo)?string.Empty:model.PVOperateUserNo),
                new SqlParameter("@PVOperateUserName", string.IsNullOrEmpty(model.PVOperateUserName)?string.Empty:model.PVOperateUserName),
                new SqlParameter("@PVOperateTime", model.PVOperateTime),
                new SqlParameter("@PVProblemId", model.PVProblemId)
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
        public bool Update(ProblemVerificationModel model)
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
            if (!string.IsNullOrEmpty(model.PVWhat))
            {
                paramsql.Append(" [PVWhat] = @PVWhat  ,");
                param.Add(new SqlParameter("@PVWhat ", model.PVWhat));
            }
            if (!string.IsNullOrEmpty(model.PVWho))
            {
                paramsql.Append(" [PVWho] = @PVWho  ,");
                param.Add(new SqlParameter("@PVWho ", model.PVWho));
            }
            if (!string.IsNullOrEmpty(model.PVWhoNo))
            {
                paramsql.Append(" [PVWhoNo ] = @PVWhoNo  ,");
                param.Add(new SqlParameter("@PVWhoNo ", model.PVWhoNo));
            }
            if (model.PVPlanDate != null && model.PVPlanDate > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                paramsql.Append(" [PVPlanDate] = @PVPlanDate ,");
                param.Add(new SqlParameter("@PVPlanDate", model.PVPlanDate));
            }
            if (model.PVActualDate != null && model.PVActualDate > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                paramsql.Append(" [PVActualDate] = @PVActualDate ,");
                param.Add(new SqlParameter("@PVActualDate", model.PVActualDate));
            }

            if (!string.IsNullOrEmpty(model.PVWhere))
            {
                paramsql.Append(" [PVWhere] = @PVWhere ,");
                param.Add(new SqlParameter("@PVWhere", model.PVWhere));
            }
            if (!string.IsNullOrEmpty(model.PVAttachment))
            {
                if (model.PVAttachment.Equals("D"))
                {
                    paramsql.Append(" [PVAttachment] = '' ,");
                }
                else
                {
                    paramsql.Append(" [PVAttachment] = @PVAttachment ,");
                    param.Add(new SqlParameter("@PVAttachment", model.PVAttachment));
                }
            }
            if (!string.IsNullOrEmpty(model.PVAttachmentUrl))
            {
                if (model.PVAttachmentUrl.Equals("D"))
                {
                    paramsql.Append(" [PVAttachmentUrl] = '' ,");
                }
                else
                {
                    paramsql.Append(" [PVAttachmentUrl] = @PVAttachmentUrl ,");
                    param.Add(new SqlParameter("@PVAttachmentUrl", model.PVAttachmentUrl));
                }
            }
            if (!string.IsNullOrEmpty(model.PVStatus))
            {
                paramsql.Append(" [PVStatus] = @PVStatus ,");
                param.Add(new SqlParameter("@PVStatus", model.PVStatus));
            }

            if (!string.IsNullOrEmpty(model.PVComment))
            {
                paramsql.Append(" [PVComment] = @PVComment ,");
                param.Add(new SqlParameter("@PVComment", model.PVComment));
            }
            if (model.PVIsValid != null)
            {
                paramsql.Append(" [PVIsValid] = @PVIsValid ,");
                param.Add(new SqlParameter("@PVIsValid", model.PVIsValid));
            }
            if (!string.IsNullOrEmpty(model.PVCreateUserNo))
            {
                paramsql.Append(" [PVCreateUserNo] = @PVCreateUserNo ,");
                param.Add(new SqlParameter("@PVCreateUserNo", model.PVCreateUserNo));
            }
            if (!string.IsNullOrEmpty(model.PVCreateUserName))
            {
                paramsql.Append(" [PVCreateUserName] = @PVCreateUserName ,");
                param.Add(new SqlParameter("@PVCreateUserName", model.PVCreateUserName));
            }
            if (model.PVCreateTime != null && model.PVCreateTime > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                paramsql.Append(" [PVCreateTime] = @PVCreateTime ,");
                param.Add(new SqlParameter("@PVCreateTime", model.PVCreateTime));
            }

            if (!string.IsNullOrEmpty(model.PVOperateUserNo))
            {
                paramsql.Append(" [PVOperateUserNo] = @PVOperateUserNo ,");
                param.Add(new SqlParameter("@PVOperateUserNo", model.PVOperateUserNo));
            }

            if (!string.IsNullOrEmpty(model.PVOperateUserName))
            {
                paramsql.Append(" [PVOperateUserName] = @PVOperateUserName ,");
                param.Add(new SqlParameter("@PVOperateUserName", model.PVOperateUserName));
            }
            if (model.PVOperateTime != null && model.PVOperateTime > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                paramsql.Append(" [PVOperateTime] = @PVOperateTime ,");
                param.Add(new SqlParameter("@PVOperateTime", model.PVOperateTime));
            }

            if (model.PVProblemId > 0)
            {
                paramsql.Append(" [PVProblemId] = @PVProblemId ,");
                param.Add(new SqlParameter("@PVProblemId", model.PVProblemId));
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
            upsql.Append(" [PVIsValid] = @PVIsValid ,");
            upsql.Append(" [PVOperateUserNo] = @PVOperateUserNo ,");
            upsql.Append(" [PVOperateUserName] = @PVOperateUserName ,");
            upsql.Append(" [PVOperateTime] = @PVOperateTime ");
            upsql.Append(" WHERE Id = @Id ");
            var value = 0;
            sqlparam.Add(new SqlParameter("@PVIsValid", value));
            sqlparam.Add(new SqlParameter("@PVOperateUserNo", param.JobNum));
            sqlparam.Add(new SqlParameter("@PVOperateUserName", param.Name));
            sqlparam.Add(new SqlParameter("@PVOperateTime", param.InvalidTime));
            sqlparam.Add(new SqlParameter("@Id", param.Id));
            return ExecteNonQuery(CommandType.Text, upsql.ToString(), null, sqlparam) > 0;
        }
    }
}
