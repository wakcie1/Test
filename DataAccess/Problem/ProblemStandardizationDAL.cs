using Model.Problem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class ProblemStandardizationDAL : NewSqlHelper
    {
        private const string tableName = "T_PROBLEM_STANDARDIZATION";

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(ProblemStandardizationModel model)
        {
            var sql = @"INSERT INTO " + tableName +
                @" ( [PSItemName]
                   ,[PSItemNameNo]
                   ,[PSNeedUpdate]
                   ,[PSWhoNo]
                   ,[PSWho]
                   ,[PSPlanDate]
                   ,[PSActualDate]
                   ,[PSDocNo]
                   ,[PSChangeContent]
                   ,[PSOldVersion]
                   ,[PSNewVersion]
                   ,[PSAttachment]
                   ,[PSAttachmentUrl]
                   ,[PSIsValid]
                   ,[PSCreateUserNo]
                   ,[PSCreateUserName]
                   ,[PSCreateTime]
                   ,[PSOperateUserNo]
                   ,[PSOperateUserName]
                   ,[PSOperateTime]
                   ,[PSProblemId])" +
                @" VALUES
                   (@PSItemName
                   ,@PSItemNameNo
                   ,@PSNeedUpdate
                   ,@PSWhoNo 
                   ,@PSWho 
                   ,@PSPlanDate 
                   ,@PSActualDate 
                   ,@PSDocNo 
                   ,@PSChangeContent 
                   ,@PSOldVersion 
                   ,@PSNewVersion
                   ,@PSAttachment 
                   ,@PSAttachmentUrl 
                   ,@PSIsValid 
                   ,@PSCreateUserNo 
                   ,@PSCreateUserName
                   ,@PSCreateTime 
                   ,@PSOperateUserNo 
                   ,@PSOperateUserName 
                   ,@PSOperateTime 
                   ,@PSProblemId )" +
                "  select id = scope_identity()";
            SqlParameter[] para = {
                new SqlParameter("@PSItemName",  string.IsNullOrEmpty(model.PSItemName)?string.Empty:model.PSItemName),
                new SqlParameter("@PSItemNameNo", model.PSItemNameNo),
                new SqlParameter("@PSNeedUpdate", model.PSNeedUpdate ?? 0),
                new SqlParameter("@PSWhoNo ",string.IsNullOrEmpty(model.PSWhoNo)?string.Empty:model.PSWhoNo ),
                new SqlParameter("@PSWho ",string.IsNullOrEmpty(model.PSWho)?string.Empty:model.PSWho ),
                new SqlParameter("@PSPlanDate", model.PSPlanDate ?? Convert.ToDateTime("1900-1-1")),
                new SqlParameter("@PSActualDate", model.PSActualDate ?? Convert.ToDateTime("1900-1-1")),
                new SqlParameter("@PSDocNo", string.IsNullOrEmpty(model.PSDocNo)?string.Empty:model.PSDocNo),
                new SqlParameter("@PSChangeContent", string.IsNullOrEmpty(model.PSChangeContent)?string.Empty:model.PSChangeContent),
                new SqlParameter("@PSOldVersion",string.IsNullOrEmpty(model.PSOldVersion)?string.Empty:model.PSOldVersion),
                new SqlParameter("@PSNewVersion",string.IsNullOrEmpty(model.PSNewVersion)?string.Empty:model.PSNewVersion),
                new SqlParameter("@PSAttachment",string.IsNullOrEmpty(model.PSAttachment)?string.Empty: (model.PSAttachment.Equals("D")? string.Empty:model.PSAttachment)),
                new SqlParameter("@PSAttachmentUrl",string.IsNullOrEmpty(model.PSAttachmentUrl)?string.Empty: (model.PSAttachmentUrl.Equals("D")? string.Empty:model.PSAttachmentUrl)),
                new SqlParameter("@PSIsValid",model.PSIsValid),
                new SqlParameter("@PSCreateUserNo",string.IsNullOrEmpty(model.PSCreateUserNo)?string.Empty:model.PSCreateUserNo),
                new SqlParameter("@PSCreateUserName",string.IsNullOrEmpty(model.PSCreateUserName)?string.Empty:model.PSCreateUserName),
                new SqlParameter("@PSCreateTime",model.PSCreateTime),
                new SqlParameter("@PSOperateUserNo", string.IsNullOrEmpty(model.PSOperateUserNo)?string.Empty:model.PSOperateUserNo),
                new SqlParameter("@PSOperateUserName", string.IsNullOrEmpty(model.PSOperateUserName)?string.Empty:model.PSOperateUserName),
                new SqlParameter("@PSOperateTime", model.PSOperateTime),
                new SqlParameter("@PSProblemId", model.PSProblemId)
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
        public bool Update(ProblemStandardizationModel model)
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
            if (!string.IsNullOrEmpty(model.PSItemName))
            {
                paramsql.Append(" [PSItemName] = @PSItemName  ,");
                param.Add(new SqlParameter("@PSItemName ", model.PSItemName));
            }
            if (model.PSItemNameNo > 0)
            {
                paramsql.Append(" [PSItemNameNo] = @PSItemNameNo ,");
                param.Add(new SqlParameter("@PSItemNameNo", model.PSItemNameNo));
            }
            if (model.PSNeedUpdate != null)
            {
                paramsql.Append(" [PSNeedUpdate] = @PSNeedUpdate  ,");
                param.Add(new SqlParameter("@PSNeedUpdate ", model.PSNeedUpdate));
            }
            if (!string.IsNullOrEmpty(model.PSWho))
            {
                paramsql.Append(" [PSWho] = @PSWho  ,");
                param.Add(new SqlParameter("@PSWho ", model.PSWho));
            }
            if (!string.IsNullOrEmpty(model.PSWhoNo))
            {
                paramsql.Append(" [PSWhoNo ] = @PSWhoNo  ,");
                param.Add(new SqlParameter("@PSWhoNo ", model.PSWhoNo));
            }
            if (model.PSPlanDate != null && model.PSPlanDate > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                paramsql.Append(" [PSPlanDate] = @PSPlanDate ,");
                param.Add(new SqlParameter("@PSPlanDate", model.PSPlanDate));
            }
            if (model.PSActualDate != null && model.PSActualDate > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                paramsql.Append(" [PSActualDate] = @PSActualDate ,");
                param.Add(new SqlParameter("@PSActualDate", model.PSActualDate));
            }

            if (!string.IsNullOrEmpty(model.PSDocNo))
            {
                paramsql.Append(" [PSDocNo] = @PSDocNo ,");
                param.Add(new SqlParameter("@PSDocNo", model.PSDocNo));
            }
            if (!string.IsNullOrEmpty(model.PSChangeContent))
            {
                paramsql.Append(" [PSChangeContent] = @PSChangeContent ,");
                param.Add(new SqlParameter("@PSChangeContent", model.PSChangeContent));
            }
            if (!string.IsNullOrEmpty(model.PSOldVersion))
            {
                paramsql.Append(" [PSOldVersion] = @PSOldVersion ,");
                param.Add(new SqlParameter("@PSOldVersion", model.PSOldVersion));
            }

            if (!string.IsNullOrEmpty(model.PSNewVersion))
            {
                paramsql.Append(" [PSNewVersion] = @PSNewVersion ,");
                param.Add(new SqlParameter("@PSNewVersion", model.PSNewVersion));
            }
            if (!string.IsNullOrEmpty(model.PSAttachment))
            {
                if (model.PSAttachment.Equals("D"))
                {
                    paramsql.Append(" [PSAttachment] = '' ,");
                }
                else
                {
                    paramsql.Append(" [PSAttachment] = @PSAttachment ,");
                    param.Add(new SqlParameter("@PSAttachment", model.PSAttachment));
                }
            }

            if (!string.IsNullOrEmpty(model.PSAttachmentUrl))
            {
                if (model.PSAttachmentUrl.Equals("D"))
                {
                    paramsql.Append(" [PSAttachmentUrl] = '' ,");
                }
                else
                {
                    paramsql.Append(" [PSAttachmentUrl] = @PSAttachmentUrl ,");
                    param.Add(new SqlParameter("@PSAttachmentUrl", model.PSAttachmentUrl));
                }
            }
            if (model.PSIsValid != null)
            {
                paramsql.Append(" [PSIsValid] = @PSIsValid ,");
                param.Add(new SqlParameter("@PSIsValid", model.PSIsValid));
            }
            if (!string.IsNullOrEmpty(model.PSCreateUserNo))
            {
                paramsql.Append(" [PSCreateUserNo] = @PSCreateUserNo ,");
                param.Add(new SqlParameter("@PSCreateUserNo", model.PSCreateUserNo));
            }
            if (!string.IsNullOrEmpty(model.PSCreateUserName))
            {
                paramsql.Append(" [PSCreateUserName] = @PSCreateUserName ,");
                param.Add(new SqlParameter("@PSCreateUserName", model.PSCreateUserName));
            }
            if (model.PSCreateTime != null && model.PSCreateTime > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                paramsql.Append(" [PSCreateTime] = @PSCreateTime ,");
                param.Add(new SqlParameter("@PSCreateTime", model.PSCreateTime));
            }

            if (!string.IsNullOrEmpty(model.PSOperateUserNo))
            {
                paramsql.Append(" [PSOperateUserNo] = @PSOperateUserNo ,");
                param.Add(new SqlParameter("@PSOperateUserNo", model.PSOperateUserNo));
            }

            if (!string.IsNullOrEmpty(model.PSOperateUserName))
            {
                paramsql.Append(" [PSOperateUserName] = @PSOperateUserName ,");
                param.Add(new SqlParameter("@PSOperateUserName", model.PSOperateUserName));
            }
            if (model.PSOperateTime != null && model.PSOperateTime > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                paramsql.Append(" [PSOperateTime] = @PSOperateTime ,");
                param.Add(new SqlParameter("@PSOperateTime", model.PSOperateTime));
            }

            if (model.PSProblemId > 0)
            {
                paramsql.Append(" [PSProblemId] = @PSProblemId ,");
                param.Add(new SqlParameter("@PSProblemId", model.PSProblemId));
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
