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
    public class ProblemActionContainmentDAL : NewSqlHelper
    {
        private const string tableName = "T_PROBLEM_ACTION_CONTAINMENT";

        /// <summary>
        /// Search Mode by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<ProblemActionContainmentModel> SearchModelById(int id = 0)
        {
            List<ProblemActionContainmentModel> list = new List<ProblemActionContainmentModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@" SELECT [Id]
                                      ,[PACWhat]
                                      ,[PACCheck]
                                      ,[PACWhoNo]
                                      ,[PACWho]
                                      ,[PACPlanDate]
                                      ,[PACVarificationDate]
                                      ,[PACWhere]
                                      ,[PACAttachment]
                                      ,[PACAttachmentUrl]
                                      ,[PACEffeective]
                                      ,[PACComment]
                                      ,[PACIsValid]
                                      ,[PACCreateUserNo]
                                      ,[PACCreateUserName]
                                      ,[PACCreateTime]
                                      ,[PACOperateUserNo]
                                      ,[PACOperateUserName]
                                      ,[PACOperateTime]
                                      ,[PACProblemId]
                                  FROM {0} with(NOLOCK) ", tableName);
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
                list = DataConvertHelper.DataTableToList<ProblemActionContainmentModel>(dt);
            }

            return list.AsEnumerable();
        }
        /// <summary>
        /// insert Table
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(ProblemActionContainmentModel model)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@" 
                INSERT INTO {0}
                              ([PACWhat]
                               ,[PACCheck]
                               ,[PACWho]
                               ,[PACWhoNo]
                               ,[PACPlanDate]
                               ,[PACVarificationDate]
                               ,[PACWhere]
                               ,[PACAttachment]
                               ,[PACAttachmentUrl]
                               ,[PACEffeective]
                               ,[PACComment]
                               ,[PACIsValid]
                               ,[PACCreateUserNo]
                               ,[PACCreateUserName]
                               ,[PACCreateTime]
                               ,[PACOperateUserNo]
                               ,[PACOperateUserName]
                               ,[PACOperateTime]
                               ,[PACProblemId])
                         VALUES
                               (
		                         @PACWhat
                               , @PACCheck
                               , @PACWho
                               , @PACWhoNo
                               , @PACPlanDate
                               , @PACVarificationDate
                               , @PACWhere
                               , @PACAttachment
                               , @PACAttachmentUrl
                               , @PACEffeective
                               , @PACComment
                               , @PACIsValid
                               , @PACCreateUserNo
                               , @PACCreateUserName
                               , @PACCreateTime
                               , @PACOperateUserNo
                               , @PACOperateUserName
                               , @PACOperateTime
                               , @PACProblemId )
                   select id = scope_identity() ", tableName);

            SqlParameter[] para = {
                new SqlParameter("@PACWhat", string.IsNullOrEmpty(model.PACWhat)?string.Empty:model.PACWhat),
                new SqlParameter("@PACCheck", string.IsNullOrEmpty(model.PACCheck) ? string.Empty : model.PACCheck),
                new SqlParameter("@PACWho", string.IsNullOrEmpty(model.PACWho) ? string.Empty : model.PACWho),
                new SqlParameter("@PACWhoNo", string.IsNullOrEmpty(model.PACWhoNo) ? string.Empty : model.PACWhoNo),
                new SqlParameter("@PACPlanDate",model.PACPlanDate ?? Convert.ToDateTime("1900-1-1")),
                new SqlParameter("@PACVarificationDate",model.PACVarificationDate ?? Convert.ToDateTime("1900-1-1")),
                new SqlParameter("@PACWhere", string.IsNullOrEmpty(model.PACWhere) ? string.Empty : model.PACWhere),
                new SqlParameter("@PACAttachment", string.IsNullOrEmpty(model.PACAttachment) ? string.Empty : (model.PACAttachment.Equals("D")? string.Empty:model.PACAttachment)),
                new SqlParameter("@PACAttachmentUrl", string.IsNullOrEmpty(model.PACAttachmentUrl) ? string.Empty : (model.PACAttachment.Equals("D")? string.Empty:model.PACAttachment)),
                new SqlParameter("@PACEffeective",model.PACEffeective ?? 0),
                new SqlParameter("@PACComment", string.IsNullOrEmpty(model.PACComment) ? string.Empty : model.PACComment),
                new SqlParameter("@PACIsValid",model.PACIsValid),
                new SqlParameter("@PACCreateUserNo",model.PACCreateUserNo),
                new SqlParameter("@PACCreateUserName",model.PACCreateUserName),
                new SqlParameter("@PACCreateTime",model.PACCreateTime),
                new SqlParameter("@PACOperateUserNo",model.PACOperateUserNo),
                new SqlParameter("@PACOperateUserName",model.PACOperateUserName),
                new SqlParameter("@PACOperateTime",model.PACOperateTime),
                new SqlParameter("@PACProblemId",model.PACProblemId)
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
        public bool Update(ProblemActionContainmentModel model)
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

            if (!string.IsNullOrEmpty(model.PACWhat))
            {
                paramsql.Append(" [PACWhat] = @PACWhat ,");
                param.Add(new SqlParameter("@PACWhat", model.PACWhat));
            }
            if (!string.IsNullOrEmpty(model.PACCheck))
            {
                paramsql.Append(" [PACCheck] = @PACCheck ,");
                param.Add(new SqlParameter("@PACCheck", model.PACCheck));
            }
            if (!string.IsNullOrEmpty(model.PACWho))
            {
                paramsql.Append(" [PACWho] = @PACWho ,");
                param.Add(new SqlParameter("@PACWho", model.PACWho));
            }
            if (!string.IsNullOrEmpty(model.PACWhoNo))
            {
                paramsql.Append(" [PACWhoNo] = @PACWhoNo ,");
                param.Add(new SqlParameter("@PACWhoNo", model.PACWhoNo));
            }
            if (model.PACPlanDate != null)
            {
                paramsql.Append(" [PACPlanDate] = @PACPlanDate ,");
                param.Add(new SqlParameter("@PACPlanDate", model.PACPlanDate));
            }
            if (model.PACVarificationDate != null)
            {
                paramsql.Append(" [PACVarificationDate] = @PACVarificationDate ,");
                param.Add(new SqlParameter("@PACVarificationDate", model.PACVarificationDate));
            }
            if (!string.IsNullOrEmpty(model.PACWhere))
            {
                paramsql.Append(" [PACWhere] = @PACWhere ,");
                param.Add(new SqlParameter("@PACWhere", model.PACWhere));
            }
            if (!string.IsNullOrEmpty(model.PACAttachment))
            {
                if (model.PACAttachment.Equals("D"))
                {
                    paramsql.Append(" [PACAttachment] = '' ,");
                }
                else
                {
                    paramsql.Append(" [PACAttachment] = @PACAttachment ,");
                    param.Add(new SqlParameter("@PACAttachment", model.PACAttachment));
                }
            }
            if (!string.IsNullOrEmpty(model.PACAttachmentUrl))
            {
                if (model.PACAttachmentUrl.Equals("D"))
                {
                    paramsql.Append(" [PACAttachmentUrl] = '' ,");
                }
                else
                {
                    paramsql.Append(" [PACAttachmentUrl] = @PACAttachmentUrl ,");
                    param.Add(new SqlParameter("@PACAttachmentUrl", model.PACAttachmentUrl));
                }
            }
            if (model.PACEffeective != null)
            {
                paramsql.Append(" [PACEffeective] = @PACEffeective ,");
                param.Add(new SqlParameter("@PACEffeective", model.PACEffeective));
            }
            if (!string.IsNullOrEmpty(model.PACComment))
            {
                paramsql.Append(" [PACComment] = @PACComment ,");
                param.Add(new SqlParameter("@PACComment", model.PACComment));
            }
            if (model.PACIsValid != null)
            {
                paramsql.Append(" [PACIsValid] = @PACIsValid ,");
                param.Add(new SqlParameter("@PACIsValid", model.PACIsValid));
            }
            if (!string.IsNullOrEmpty(model.PACCreateUserNo))
            {
                paramsql.Append(" [PACCreateUserNo] = @PACCreateUserNo ,");
                param.Add(new SqlParameter("@PACCreateUserNo", model.PACCreateUserNo));
            }
            if (!string.IsNullOrEmpty(model.PACCreateUserName))
            {
                paramsql.Append(" [PACCreateUserName] = @PACCreateUserName ,");
                param.Add(new SqlParameter("@PACCreateUserName", model.PACCreateUserName));
            }
            if (model.PACCreateTime != null)
            {
                paramsql.Append(" [PACCreateTime] = @PACCreateTime ,");
                param.Add(new SqlParameter("@PACCreateTime", model.PACCreateTime));
            }
            if (!string.IsNullOrEmpty(model.PACOperateUserNo))
            {
                paramsql.Append(" [PACOperateUserNo] = @PACOperateUserNo ,");
                param.Add(new SqlParameter("@PACOperateUserNo", model.PACOperateUserNo));
            }
            if (!string.IsNullOrEmpty(model.PACOperateUserName))
            {
                paramsql.Append(" [PACOperateUserName] = @PACOperateUserName ,");
                param.Add(new SqlParameter("@PACOperateUserName", model.PACOperateUserName));
            }
            if (model.PACOperateTime != null)
            {
                paramsql.Append(" [PACOperateTime] = @PACOperateTime ,");
                param.Add(new SqlParameter("@PACOperateTime", model.PACOperateTime));
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
            upsql.Append(" [PACIsValid] = @PACIsValid ,");
            upsql.Append(" [PACOperateUserNo] = @PACOperateUserNo ,");
            upsql.Append(" [PACOperateUserName] = @PACOperateUserName ,");
            upsql.Append(" [PACOperateTime] = @PACOperateTime ");
            upsql.Append(" WHERE Id = @Id ");
            var value = 0;
            sqlparam.Add(new SqlParameter("@PACIsValid", value));
            sqlparam.Add(new SqlParameter("@PACOperateUserNo", param.JobNum));
            sqlparam.Add(new SqlParameter("@PACOperateUserName", param.Name));
            sqlparam.Add(new SqlParameter("@PACOperateTime", param.InvalidTime));
            sqlparam.Add(new SqlParameter("@Id", param.Id));
            return ExecteNonQuery(CommandType.Text, upsql.ToString(), null, sqlparam) > 0;
        }
    }
}
