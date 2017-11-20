using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Suggest;
using System.Data;
using Common;
using Model.TableModel;
using System.Data.SqlClient;

namespace DataAccess
{
    public class SuggestDAL : NewSqlHelper
    {
        private const string tableName = "T_BASE_FEEDBACK";
        private const string tableNameBase = "T_BASE_CODE";
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<SuggestionsInfoModel> SearchSuggestInfoList(SuggestionsSearchModel model)
        {
            List<SuggestionsInfoModel> list = new List<SuggestionsInfoModel>();

            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@" select
                                  Id,
                                  BFType ,
                                  BFPhase  ,
                                  BFDesc  ,
                                  BFRespUserNo ,
                                  BFRespName  ,
                                  BFStatus ,
                                  BFPicture  ,
                                  BFFeedBackComment  ,
                                  BFCreateUserNo  ,
                                  BFCreateUserName  ,
                                  BFCreateTime  ,
                                  BFOperateUserNo  ,
                                  BFOperateUserName  ,
                                  BFOperateTime ,
                                  BFIsValid 
                                 FROM {0} with(NOLOCK)  ", tableName);
            sql.Append(" WHERE 1=1 ");
            sql.Append(" AND BFIsValid=1 ");
            if (model.DateForm != null && model.DateForm > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                sql.AppendFormat("AND BFCreateTime >='{0}'", model.DateForm);
            }
            var aa = DateTime.Parse("0001-01-01 00:00:00");
            if (model.DateTo != null && model.DateTo > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                sql.AppendFormat("And BFCreateTime<='{0}'", model.DateTo);
            }
            var ds = ExecuteDataSet(CommandType.Text, sql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<SuggestionsInfoModel>(dt);
            }

            return list.AsEnumerable();
        }

        public SuggestionsInfoModel GetSuggesById(long userIdlong)
        {
            List<SuggestionsInfoModel> list = new List<SuggestionsInfoModel>();

            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@" select
                                  Id,
                                  BFType ,
                                  BFPhase  ,
                                  BFDesc  ,
                                  BFRespUserNo ,
                                  BFRespName  ,
                                  BFStatus ,
                                  BFPicture  ,
                                  BFFeedBackComment  ,
                                  BFCreateUserNo  ,
                                  BFCreateUserName  ,
                                  BFCreateTime  ,
                                  BFOperateUserNo  ,
                                  BFOperateUserName  ,
                                  BFOperateTime ,
                                  BFIsValid 
                                 FROM {0} with(NOLOCK) ", tableName);
            sql.Append(" WHERE 1=1 ");
            sql.Append(" AND BFIsValid=1 ");
            if (userIdlong != 0)
            {
                sql.AppendFormat("AND Id= {0} ", userIdlong);
            }
            var ds = ExecuteDataSet(CommandType.Text, sql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<SuggestionsInfoModel>(dt);
            }
            return list.FirstOrDefault();
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long Insert(SuggestionsInfoModel model)
        {
            var sql = @"INSERT INTO " + tableName +
                 @" ([BFType]
                   ,[BFPhase]
                   ,[BFDesc]
                   ,[BFRespUserNo]
                   ,[BFRespName]
                   ,[BFStatus]
                   ,[BFPicture]
                   ,[BFPictureUrl]
                   ,[BFFeedBackComment]
                   ,[BFCreateUserNo]
                   ,[BFCreateUserName]
                   ,[BFCreateTime]
                   ,[BFOperateUserNo]
                   ,[BFOperateUserName]
                   ,[BFOperateTime]
                   ,[BFIsValid])" +
                 @" VALUES (@BFType
                    , @BFPhase
                    , @BFDesc
                    , @BFRespUserNo
                    , @BFRespName
                    , @BFStatus
                    , @BFPicture
                    , @BFPictureUrl
                    , @BFFeedBackComment
                    , @BFCreateUserNo
                    , @BFCreateUserName
                    , @BFCreateTime
                    , @BFOperateUserNo
                    , @BFOperateUserName
                    , @BFOperateTime
                    , @BFIsValid) " +
                 "  select id = scope_identity()";
            SqlParameter[] para = {
                new SqlParameter("@BFType", string.IsNullOrEmpty(model.BFType)?string.Empty:model.BFType),
                new SqlParameter("@BFPhase",string.IsNullOrEmpty(model.BFPhase)?string.Empty: model.BFPhase),
                new SqlParameter("@BFDesc", string.IsNullOrEmpty(model.BFDesc)?string.Empty:model.BFDesc),
                new SqlParameter("@BFRespUserNo", string.IsNullOrEmpty(model.BFRespUserNo)?string.Empty:model.BFRespUserNo),
                new SqlParameter("@BFRespName", string.IsNullOrEmpty(model.BFRespName)?string.Empty:model.BFRespName),
                new SqlParameter("@BFStatus",  model.BFStatus),
                new SqlParameter("@BFPicture", string.IsNullOrEmpty(model.BFPicture)?string.Empty:model.BFPicture),
                new SqlParameter("@BFPictureUrl", string.IsNullOrEmpty(model.BFPictureUrl)?string.Empty:model.BFPictureUrl),
                new SqlParameter("@BFFeedBackComment",model.BFFeedBackComment),
                new SqlParameter("@BFCreateUserNo",model.BFCreateUserNo),
                new SqlParameter("@BFCreateUserName",model.BFCreateUserName),
                new SqlParameter("@BFCreateTime",model.BFCreateTime),
                new SqlParameter("@BFOperateUserNo",model.BFOperateUserNo),
                new SqlParameter("@BFOperateUserName",model.BFOperateUserName),
                new SqlParameter("@BFOperateTime", model.BFOperateTime),
                new SqlParameter("@BFIsValid", model.BFIsValid)
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

        public bool Update(SuggestionsInfoModel model)
        {
            var sql = @"UPDATE " + tableName +
                @" SET [BFFeedBackComment]=@BFFeedBackComment
                       ,[BFStatus]=@BFStatus  WHERE Id =@Id ";

            SqlParameter[] para = {
                new SqlParameter("@Id",model.Id),
                new SqlParameter("@BFFeedBackComment",model.BFFeedBackComment),
                new SqlParameter("@BFStatus",model.BFStatus)
            };

            return ExecteNonQuery(CommandType.Text, sql, null, para) > 0;
        }
        public List<CodeModel> GetAllList()
        {
            var list = new List<CodeModel>();
            var selectSql = new StringBuilder();
            selectSql.Append(string.Format(@" 
                   SELECT [Id]
                          ,[BCCode]
                          ,[BCCodeDesc]
                          ,[BCCategory]
                          ,[BCCodeOrder]
                          ,[BCIsValid]
                          ,[BCCreateUserNo]
                          ,[BCCreateUserName]
                          ,[BCCreateTime]
                          ,[BCOperateUserNo]
                          ,[BCOperateUserName]
                          ,[BCOperateTime]
                       FROM {0} with(NOLOCK) ", tableNameBase));

            var ds = ExecuteDataSet(CommandType.Text, selectSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<CodeModel>(dt);
            }
            return list;
        }
    }
}
