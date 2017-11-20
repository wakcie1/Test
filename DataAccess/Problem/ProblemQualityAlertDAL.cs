using Common;
using Common.Enum;
using Model.CommonModel;
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
    public class ProblemQualityAlertDAL : NewSqlHelper
    {
        private const string tableName = "T_PROBLEM_QUALITYALERT";

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(ProblemQualityAlertModel model)
        {
            var sql = @"INSERT INTO " + tableName +
                @"([PQProblemId]
                ,[PQWhat]
                ,[PQWhoNo]
                ,[PQWho]
                ,[PQPlanDate]
                ,[PQActualDate]
                ,[PQAttachment]
                ,[PQAttachmentUrl]
                ,[PQIsValid]
                ,[PQCreateUserNo]
                ,[PQCreateUserName]
                ,[PQCreateTime]
                ,[PQOperateUserNo]
                ,[PQOperateUserName]
                ,[PQOperateTime])
                VALUES
                (@PQProblemId
                ,@PQWhat
                ,@PQWhoNo
                ,@PQWho
                ,@PQPlanDate
                ,@PQActualDate
                ,@PQAttachment
                ,@PQAttachmentUrl
                ,@PQIsValid
                ,@PQCreateUserNo
                ,@PQCreateUserName
                ,@PQCreateTime
                ,@PQOperateUserNo
                ,@PQOperateUserName
                ,@PQOperateTime) " +
                "  select id = scope_identity()";
            SqlParameter[] para = {
                new SqlParameter("@PQProblemId",model.PQProblemId),
                new SqlParameter("@PQWhat", string.IsNullOrEmpty(model.PQWhat)?string.Empty:model.PQWhat),
                new SqlParameter("@PQWhoNo", string.IsNullOrEmpty(model.PQWhoNo)?string.Empty:model.PQWhoNo),
                new SqlParameter("@PQWho", string.IsNullOrEmpty(model.PQWho)?string.Empty:model.PQWho),
                new SqlParameter("@PQPlanDate",model.PQPlanDate ?? Convert.ToDateTime("1900-1-1")),
                new SqlParameter("@PQActualDate",model.PQActualDate ?? Convert.ToDateTime("1900-1-1")),
                new SqlParameter("@PQAttachment", string.IsNullOrEmpty(model.PQAttachment) ? string.Empty : (model.PQAttachment.Equals("D")? string.Empty:model.PQAttachment)),
                new SqlParameter("@PQAttachmentUrl", string.IsNullOrEmpty(model.PQAttachmentUrl) ? string.Empty : (model.PQAttachmentUrl.Equals("D")? string.Empty:model.PQAttachmentUrl)),
                new SqlParameter("@PQIsValid",model.PQIsValid),
                new SqlParameter("@PQCreateUserNo",model.PQCreateUserNo),
                new SqlParameter("@PQCreateUserName",model.PQCreateUserName),
                new SqlParameter("@PQCreateTime",model.PQCreateTime),
                new SqlParameter("@PQOperateUserNo",model.PQOperateUserNo),
                new SqlParameter("@PQOperateUserName",model.PQOperateUserName),
                new SqlParameter("@PQOperateTime",model.PQOperateTime),
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

        public bool Update(ProblemQualityAlertModel model)
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

            if (!string.IsNullOrEmpty(model.PQWhat))
            {
                paramsql.Append(" [PQWhat] = @PQWhat ,");
                param.Add(new SqlParameter("@PQWhat", model.PQWhat));
            }
            if (!string.IsNullOrEmpty(model.PQWhoNo))
            {
                paramsql.Append(" [PQWhoNo] = @PQWhoNo ,");
                param.Add(new SqlParameter("@PQWhoNo", model.PQWhoNo));
            }
            if (!string.IsNullOrEmpty(model.PQWho))
            {
                paramsql.Append(" [PQWho] = @PQWho ,");
                param.Add(new SqlParameter("@PQWho", model.PQWho));
            }
            if (model.PQPlanDate != null)
            {
                paramsql.Append(" [PQPlanDate] = @PQPlanDate ,");
                param.Add(new SqlParameter("@PQPlanDate", model.PQPlanDate));
            }
            if (model.PQActualDate != null)
            {
                paramsql.Append(" [PQActualDate] = @PQActualDate ,");
                param.Add(new SqlParameter("@PQActualDate", model.PQActualDate));
            }
            if (!string.IsNullOrEmpty(model.PQAttachment))
            {
                if (model.PQAttachment.Equals("D"))
                {
                    paramsql.Append(" [PQAttachment] = '' ,");
                }
                else
                {
                    paramsql.Append(" [PQAttachment] = @PQAttachment ,");
                    param.Add(new SqlParameter("@PQAttachment", model.PQAttachment));
                }
            }
            if (!string.IsNullOrEmpty(model.PQAttachmentUrl))
            {
                if (model.PQAttachmentUrl.Equals("D"))
                {
                    paramsql.Append(" [PQAttachmentUrl] = '' ,");
                }
                else
                {
                    paramsql.Append(" [PQAttachmentUrl] = @PQAttachmentUrl ,");
                    param.Add(new SqlParameter("@PQAttachmentUrl", model.PQAttachmentUrl));
                }
            }
            if (model.PQIsValid != null)
            {
                paramsql.Append(" [PQIsValid] = @PQIsValid ,");
                param.Add(new SqlParameter("@PQIsValid", model.PQIsValid));
            }
            if (!string.IsNullOrEmpty(model.PQCreateUserNo))
            {
                paramsql.Append(" [PQCreateUserNo] = @PQCreateUserNo ,");
                param.Add(new SqlParameter("@PQCreateUserNo", model.PQCreateUserNo));
            }
            if (!string.IsNullOrEmpty(model.PQCreateUserName))
            {
                paramsql.Append(" [PQCreateUserName] = @PQCreateUserName ,");
                param.Add(new SqlParameter("@PQCreateUserName", model.PQCreateUserName));
            }
            if (model.PQCreateTime != null)
            {
                paramsql.Append(" [PQCreateTime] = @PQCreateTime ,");
                param.Add(new SqlParameter("@PQCreateTime", model.PQCreateTime));
            }
            if (!string.IsNullOrEmpty(model.PQOperateUserNo))
            {
                paramsql.Append(" [PQOperateUserNo] = @PQOperateUserNo ,");
                param.Add(new SqlParameter("@PQOperateUserNo", model.PQOperateUserNo));
            }
            if (!string.IsNullOrEmpty(model.PQOperateUserName))
            {
                paramsql.Append(" [PQOperateUserName] = @PQOperateUserName ,");
                param.Add(new SqlParameter("@PQOperateUserName", model.PQOperateUserName));
            }
            if (model.PQOperateTime != null)
            {
                paramsql.Append(" [PQOperateTime] = @PQOperateTime ,");
                param.Add(new SqlParameter("@PQOperateTime", model.PQOperateTime));
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
            upsql.Append(" [PQIsValid] = @PQIsValid ,");
            upsql.Append(" [PQOperateUserNo] = @PQOperateUserNo ,");
            upsql.Append(" [PQOperateUserName] = @PQOperateUserName ,");
            upsql.Append(" [PQOperateTime] = @PQOperateTime ");
            upsql.Append(" WHERE Id = @Id ");
            var value = 0;
            sqlparam.Add(new SqlParameter("@PQIsValid", value));
            sqlparam.Add(new SqlParameter("@PQOperateUserNo", param.JobNum));
            sqlparam.Add(new SqlParameter("@PQOperateUserName", param.Name));
            sqlparam.Add(new SqlParameter("@PQOperateTime", param.InvalidTime));
            sqlparam.Add(new SqlParameter("@Id", param.Id));
            return ExecteNonQuery(CommandType.Text, upsql.ToString(), null, sqlparam) > 0;
        }

        /// <summary>
        /// 描述：获取质量报警信息
        /// </summary>
        /// <param name="materialId">物料Id</param>
        /// <returns></returns>
        public ProblemQualityAlertModel GetQualityAlertById(int id)
        {
            var material = new ProblemQualityAlertModel();
            SqlParameter[] para = {
                new SqlParameter("@Id", id)
            };

            var sql = @"SELECT TOP 1 [Id]
                              ,[PQProblemId]
                              ,[PQWhat]
                              ,[PQWhoNo]
                              ,[PQWho]
                              ,[PQPlanDate]
                              ,[PQActualDate]
                              ,[PQAttachment]
                              ,[PQAttachmentUrl]
                              ,[PQIsValid]
                              ,[PQCreateUserNo]
                              ,[PQCreateUserName]
                              ,[PQCreateTime]
                              ,[PQOperateUserNo]
                              ,[PQOperateUserName]
                              ,[PQOperateTime]
                        FROM " + tableName + " with(NOLOCK) WHERE Id=@Id";
            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                material = DataConvertHelper.DataTableToList<ProblemQualityAlertModel>(dt)[0];
            }
            else
            {
                return null;
            }
            return material;
        }
    }
}
