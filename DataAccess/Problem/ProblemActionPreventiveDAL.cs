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
    public class ProblemActionPreventiveDAL : NewSqlHelper
    {
        private const string tableName = "T_PROBLEM_ACTION_PREVENTIVE";

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(ProblemActionPreventiveModel model)
        {
            var sql = @"INSERT INTO " + tableName +
                @" ([PAPWhat]
                   ,[PAPWhoNo]
                   ,[PAPWho]
                   ,[PAPPlanDate]
                   ,[PAPActualDate]
                   ,[PAPWhere]
                   ,[PAPAttachment]
                   ,[PAPAttachmentUrl]
                   ,[PAPStatus]
                   ,[PAPComment]
                   ,[PAPIsValid]
                   ,[PAPCreateUserNo]
                   ,[PAPCreateUserName]
                   ,[PAPCreateTime]
                   ,[PAPOperateUserNo]
                   ,[PAPOperateUserName]
                   ,[PAPOperateTime]
                   ,[PAPProblemId])" +
                @" VALUES 
                   (@PAPWhat 
                   ,@PAPWhoNo 
                   ,@PAPWho 
                   ,@PAPPlanDate 
                   ,@PAPActualDate 
                   ,@PAPWhere 
                   ,@PAPAttachment 
                   ,@PAPAttachmentUrl 
                   ,@PAPStatus 
                   ,@PAPComment 
                   ,@PAPIsValid 
                   ,@PAPCreateUserNo 
                   ,@PAPCreateUserName
                   ,@PAPCreateTime 
                   ,@PAPOperateUserNo 
                   ,@PAPOperateUserName 
                   ,@PAPOperateTime 
                   ,@PAPProblemId )" +
                "  select id = scope_identity()";
            SqlParameter[] para = {
                new SqlParameter("@PAPWhat",  string.IsNullOrEmpty(model.PAPWhat)?string.Empty:model.PAPWhat),
                new SqlParameter("@PAPWhoNo ",string.IsNullOrEmpty(model.PAPWhoNo)?string.Empty:model.PAPWhoNo ),
                new SqlParameter("@PAPWho ",string.IsNullOrEmpty(model.PAPWho)?string.Empty:model.PAPWho ),
                new SqlParameter("@PAPPlanDate",  model.PAPPlanDate ?? Convert.ToDateTime("1900-1-1")),
                new SqlParameter("@PAPActualDate", model.PAPActualDate ?? Convert.ToDateTime("1900-1-1")),
                new SqlParameter("@PAPWhere", string.IsNullOrEmpty(model.PAPWhere)?string.Empty:model.PAPWhere),
                new SqlParameter("@PAPAttachment", string.IsNullOrEmpty(model.PAPAttachment)?string.Empty: (model.PAPAttachment.Equals("D")? string.Empty:model.PAPAttachment)),
                new SqlParameter("@PAPAttachmentUrl",string.IsNullOrEmpty(model.PAPAttachmentUrl)?string.Empty: (model.PAPAttachmentUrl.Equals("D")? string.Empty:model.PAPAttachmentUrl)),
                new SqlParameter("@PAPStatus", string.IsNullOrEmpty(model.PAPStatus)?string.Empty:model.PAPStatus),
                new SqlParameter("@PAPComment",string.IsNullOrEmpty(model.PAPComment)?string.Empty:model.PAPComment),
                new SqlParameter("@PAPIsValid",model.PAPIsValid),
                new SqlParameter("@PAPCreateUserNo",string.IsNullOrEmpty(model.PAPCreateUserNo)?string.Empty:model.PAPCreateUserNo),
                new SqlParameter("@PAPCreateUserName",string.IsNullOrEmpty(model.PAPCreateUserName)?string.Empty:model.PAPCreateUserName),
                new SqlParameter("@PAPCreateTime",model.PAPCreateTime),
                new SqlParameter("@PAPOperateUserNo", string.IsNullOrEmpty(model.PAPOperateUserNo)?string.Empty:model.PAPOperateUserNo),
                new SqlParameter("@PAPOperateUserName", string.IsNullOrEmpty(model.PAPOperateUserName)?string.Empty:model.PAPOperateUserName),
                new SqlParameter("@PAPOperateTime", model.PAPOperateTime),
                new SqlParameter("@PAPProblemId", model.PAPProblemId)
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
        public bool Update(ProblemActionPreventiveModel model)
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
            if (!string.IsNullOrEmpty(model.PAPWhat))
            {
                paramsql.Append(" [PAPWhat] = @PAPWhat  ,");
                param.Add(new SqlParameter("@PAPWhat ", model.PAPWhat));
            }
            if (!string.IsNullOrEmpty(model.PAPWho))
            {
                paramsql.Append(" [PAPWho] = @PAPWho  ,");
                param.Add(new SqlParameter("@PAPWho ", model.PAPWho));
            }
            if (!string.IsNullOrEmpty(model.PAPWhoNo))
            {
                paramsql.Append(" [PAPWhoNo ] = @PAPWhoNo  ,");
                param.Add(new SqlParameter("@PAPWhoNo ", model.PAPWhoNo));
            }
            if (model.PAPPlanDate != null && model.PAPPlanDate > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                paramsql.Append(" [PAPPlanDate] = @PAPPlanDate ,");
                param.Add(new SqlParameter("@PAPPlanDate", model.PAPPlanDate));
            }
            if (model.PAPActualDate != null && model.PAPActualDate > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                paramsql.Append(" [PAPActualDate] = @PAPActualDate ,");
                param.Add(new SqlParameter("@PAPActualDate", model.PAPActualDate));
            }

            if (!string.IsNullOrEmpty(model.PAPWhere))
            {
                paramsql.Append(" [PAPWhere] = @PAPWhere ,");
                param.Add(new SqlParameter("@PAPWhere", model.PAPWhere));
            }
            if (!string.IsNullOrEmpty(model.PAPAttachment))
            {
                if (model.PAPAttachment.Equals("D"))
                {
                    paramsql.Append(" [PAPAttachment] = '' ,");
                }
                else
                {
                    paramsql.Append(" [PAPAttachment] = @PAPAttachment ,");
                    param.Add(new SqlParameter("@PAPAttachment", model.PAPAttachment));
                }
            }
            if (!string.IsNullOrEmpty(model.PAPAttachmentUrl))
            {
                if (model.PAPAttachmentUrl.Equals("D"))
                {
                    paramsql.Append(" [PAPAttachmentUrl] = '' ,");
                }
                else
                {
                    paramsql.Append(" [PAPAttachmentUrl] = @PAPAttachmentUrl ,");
                    param.Add(new SqlParameter("@PAPAttachmentUrl", model.PAPAttachmentUrl));
                }
            }
            if (!string.IsNullOrEmpty(model.PAPStatus))
            {
                paramsql.Append(" [PAPStatus] = @PAPStatus ,");
                param.Add(new SqlParameter("@PAPStatus", model.PAPStatus));
            }

            if (!string.IsNullOrEmpty(model.PAPComment))
            {
                paramsql.Append(" [PAPComment] = @PAPComment ,");
                param.Add(new SqlParameter("@PAPComment", model.PAPComment));
            }

            paramsql.Append(" [PAPIsValid] = @PAPIsValid ,");
            param.Add(new SqlParameter("@PAPIsValid", model.PAPIsValid));

            if (!string.IsNullOrEmpty(model.PAPCreateUserNo))
            {
                paramsql.Append(" [PAPCreateUserNo] = @PAPCreateUserNo ,");
                param.Add(new SqlParameter("@PAPCreateUserNo", model.PAPCreateUserNo));
            }
            if (!string.IsNullOrEmpty(model.PAPCreateUserName))
            {
                paramsql.Append(" [PAPCreateUserName] = @PAPCreateUserName ,");
                param.Add(new SqlParameter("@PAPCreateUserName", model.PAPCreateUserName));
            }
            if (model.PAPCreateTime != null && model.PAPCreateTime > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                paramsql.Append(" [PAPCreateTime] = @PAPCreateTime ,");
                param.Add(new SqlParameter("@PAPCreateTime", model.PAPCreateTime));
            }

            if (!string.IsNullOrEmpty(model.PAPOperateUserNo))
            {
                paramsql.Append(" [PAPOperateUserNo] = @PAPOperateUserNo ,");
                param.Add(new SqlParameter("@PAPOperateUserNo", model.PAPOperateUserNo));
            }

            if (!string.IsNullOrEmpty(model.PAPOperateUserName))
            {
                paramsql.Append(" [PAPOperateUserName] = @PAPOperateUserName ,");
                param.Add(new SqlParameter("@PAPOperateUserName", model.PAPOperateUserName));
            }
            if (model.PAPOperateTime != null && model.PAPOperateTime > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                paramsql.Append(" [PAPOperateTime] = @PAPOperateTime ,");
                param.Add(new SqlParameter("@PAPOperateTime", model.PAPOperateTime));
            }

            if (model.PAPProblemId > 0)
            {
                paramsql.Append(" [PAPProblemId] = @PAPProblemId ,");
                param.Add(new SqlParameter("@PAPProblemId", model.PAPProblemId));
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
            upsql.Append(" [PAPIsValid] = @PAPIsValid ,");
            upsql.Append(" [PAPOperateUserNo] = @PAPOperateUserNo ,");
            upsql.Append(" [PAPOperateUserName] = @PAPOperateUserName ,");
            upsql.Append(" [PAPOperateTime] = @PAPOperateTime ");
            upsql.Append(" WHERE Id = @Id ");
            var value = 0;
            sqlparam.Add(new SqlParameter("@PAPIsValid", value));
            sqlparam.Add(new SqlParameter("@PAPOperateUserNo", param.JobNum));
            sqlparam.Add(new SqlParameter("@PAPOperateUserName", param.Name));
            sqlparam.Add(new SqlParameter("@PAPOperateTime", param.InvalidTime));
            sqlparam.Add(new SqlParameter("@Id", param.Id));
            return ExecteNonQuery(CommandType.Text, upsql.ToString(), null, sqlparam) > 0;
        }
    }
}
