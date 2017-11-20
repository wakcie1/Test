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
    public class ProblemSolvingTeamDAL : NewSqlHelper
    {
        private const string tableName = "T_PROBLEM_SOLVINGTEAM";

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(ProblemSolvingTeamModel model)
        {
            var sql = @"INSERT INTO " + tableName +
            @" ([PSProblemId]
            ,[PSUserNo]
            ,[PSUserName]
            ,[PSDeskEXT]
            ,[PSDeptId]
            ,[PSDeptName]
            ,[PSUserTitle]
            ,[PSIsLeader]
            ,[PSIsValid]
            ,[PSCreateUserNo]
            ,[PSCreateUserName]
            ,[PSCreateTime]
            ,[PSOperateUserNo]
            ,[PSOperateUserName]
            ,[PSOperateTime])" +
            @" VALUES (@PSProblemId
            ,@PSUserNo
            ,@PSUserName
            ,@PSDeskEXT
            ,@PSDeptId
            ,@PSDeptName
            ,@PSUserTitle
            ,@PSIsLeader
            ,@PSIsValid
            ,@PSCreateUserNo
            ,@PSCreateUserName
            ,@PSCreateTime
            ,@PSOperateUserNo
            ,@PSOperateUserName
            ,@PSOperateTime) " +
            "  select id = scope_identity()";
            SqlParameter[] para = {
                new SqlParameter("@PSProblemId",model.PSProblemId),
                new SqlParameter("@PSUserNo", string.IsNullOrEmpty(model.PSUserNo)?string.Empty:model.PSUserNo),
                new SqlParameter("@PSUserName", string.IsNullOrEmpty(model.PSUserName)?string.Empty:model.PSUserName),
                new SqlParameter("@PSDeskEXT", string.IsNullOrEmpty(model.PSDeskEXT) ? string.Empty : model.PSDeskEXT),
                new SqlParameter("@PSDeptId",model.PSDeptId),
                new SqlParameter("@PSDeptName", string.IsNullOrEmpty(model.PSDeptName) ? string.Empty : model.PSDeptName),
                new SqlParameter("@PSUserTitle", string.IsNullOrEmpty(model.PSUserTitle) ? string.Empty : model.PSUserTitle),
                new SqlParameter("@PSIsLeader",model.PSIsLeader),
                new SqlParameter("@PSIsValid",model.PSIsValid),
                new SqlParameter("@PSCreateUserNo",model.PSCreateUserNo),
                new SqlParameter("@PSCreateUserName",model.PSCreateUserName),
                new SqlParameter("@PSCreateTime",model.PSCreateTime),
                new SqlParameter("@PSOperateUserNo",model.PSOperateUserNo),
                new SqlParameter("@PSOperateUserName",model.PSOperateUserName),
                new SqlParameter("@PSOperateTime",model.PSOperateTime),
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

        public bool Update(ProblemSolvingTeamModel model)
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

            if (!string.IsNullOrEmpty(model.PSUserNo))
            {
                paramsql.Append(" [PSUserNo] = @PSUserNo ,");
                param.Add(new SqlParameter("@PSUserNo", model.PSUserNo));
            }
            if (!string.IsNullOrEmpty(model.PSUserName))
            {
                paramsql.Append(" [PSUserName] = @PSUserName ,");
                param.Add(new SqlParameter("@PSUserName", model.PSUserName));
            }
            if (!string.IsNullOrEmpty(model.PSDeskEXT))
            {
                paramsql.Append(" [PSDeskEXT] = @PSDeskEXT ,");
                param.Add(new SqlParameter("@PSDeskEXT", model.PSDeskEXT));
            }
            if (model.PSDeptId > 0)
            {
                paramsql.Append(" [PSDeptId] = @PSDeptId ,");
                param.Add(new SqlParameter("@PSDeptId", model.PSDeptId));
            }
            if (!string.IsNullOrEmpty(model.PSDeptName))
            {
                paramsql.Append(" [PSDeptName] = @PSDeptName ,");
                param.Add(new SqlParameter("@PSDeptName", model.PSDeptName));
            }
            if (!string.IsNullOrEmpty(model.PSUserTitle))
            {
                paramsql.Append(" [PSUserTitle] = @PSUserTitle ,");
                param.Add(new SqlParameter("@PSUserTitle", model.PSUserTitle));
            }
            if (model.PSIsLeader != null)
            {
                paramsql.Append(" [PSIsLeader] = @PSIsLeader ,");
                param.Add(new SqlParameter("@PSIsLeader", model.PSIsLeader));
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
            if (model.PSCreateTime != null)
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
            if (model.PSOperateTime != null)
            {
                paramsql.Append(" [PSOperateTime] = @PSOperateTime ,");
                param.Add(new SqlParameter("@PSOperateTime", model.PSOperateTime));
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
            upsql.Append(" [PSIsValid] = @PSIsValid ,");
            upsql.Append(" [PSOperateUserNo] = @PSOperateUserNo ,");
            upsql.Append(" [PSOperateUserName] = @PSOperateUserName ,");
            upsql.Append(" [PSOperateTime] = @PSOperateTime ");
            upsql.Append(" WHERE Id = @Id ");
            var value = 0;
            sqlparam.Add(new SqlParameter("@PSIsValid", value));
            sqlparam.Add(new SqlParameter("@PSOperateUserNo", param.JobNum));
            sqlparam.Add(new SqlParameter("@PSOperateUserName", param.Name));
            sqlparam.Add(new SqlParameter("@PSOperateTime", param.InvalidTime));
            sqlparam.Add(new SqlParameter("@Id", param.Id));
            return ExecteNonQuery(CommandType.Text, upsql.ToString(), null, sqlparam) > 0;
        }

        /// <summary>
        /// 描述：获取质量报警信息
        /// </summary>
        /// <param name="materialId">物料Id</param>
        /// <returns></returns>
        public ProblemSolvingTeamModel GetSolvingTeamById(int id)
        {
            var material = new ProblemSolvingTeamModel();
            SqlParameter[] para = {
                new SqlParameter("@Id", id)
            };

            var sql = @"SELECT TOP 1 [Id]
                            ,[PSProblemId]
                            ,[PSUserNo]
                            ,[PSUserName]
                            ,[PSDeskEXT]
                            ,[PSDeptId]
                            ,[PSDeptName]
                            ,[PSUserTitle]
                            ,[PSIsLeader]
                            ,[PSIsValid]
                            ,[PSCreateUserNo]
                            ,[PSCreateUserName]
                            ,[PSCreateTime]
                            ,[PSOperateUserNo]
                            ,[PSOperateUserName]
                            ,[PSOperateTime]
                        FROM " + tableName + " with(NOLOCK) WHERE Id=@Id";
            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                material = DataConvertHelper.DataTableToList<ProblemSolvingTeamModel>(dt)[0];
            }
            else
            {
                return null;
            }
            return material;
        }
    }
}
