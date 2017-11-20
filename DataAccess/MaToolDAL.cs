using Common;
using Common.Enum;
using Model.Material;
using Model.TableModel;
using Model.ViewModel.Department;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess
{
    public class MaToolDAL : NewSqlHelper
    {
        private const string tableName = "T_BASE_MACHINETOOL";

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(MaToolModel model)
        {
            var sql = @"INSERT INTO " + tableName +
                    @"([BMEquipmentName]
                    ,[BMClassification]
                    ,[BMEquipmentNo]
                    ,[BMFixtureNo]
                    ,[BMType]
                    ,[BMSerialNumber]
                    ,[BMQuantity]
                    ,[BMManufacturedDate]
                    ,[BMPower]
                    ,[BMOutlineDimension]
                    ,[BMAbility]
                    ,[BMNeedPressureAir]
                    ,[BMNeedCoolingWater]
                    ,[BMIncomingDate]
                    ,[BMRemarks]
                    ,[BMIsValid]
                    ,[BMCreateUserNo]
                    ,[BMCreateUserName]
                    ,[BMCreateTime]
                    ,[BMOperateUserNo]
                    ,[BMOperateUserName]
                    ,[BMOperateTime])
                    VALUES
                    (@BMEquipmentName
                    , @BMClassification
                    , @BMEquipmentNo
                    , @BMFixtureNo
                    , @BMType
                    , @BMSerialNumber
                    , @BMQuantity
                    , @BMManufacturedDate
                    , @BMPower
                    , @BMOutlineDimension
                    , @BMAbility
                    , @BMNeedPressureAir
                    , @BMNeedCoolingWater
                    , @BMIncomingDate
                    , @BMRemarks
                    , @BMIsValid
                    , @BMCreateUserNo
                    , @BMCreateUserName
                    , @BMCreateTime
                    , @BMOperateUserNo
                    , @BMOperateUserName
                    , @BMOperateTime)" +
                "  select id = scope_identity()";
            SqlParameter[] para = {
                new SqlParameter("@BMEquipmentName", string.IsNullOrEmpty(model.BMEquipmentName)?string.Empty:model.BMEquipmentName),
                new SqlParameter("@BMClassification", model.BMClassification??0),
                new SqlParameter("@BMEquipmentNo", string.IsNullOrEmpty(model.BMEquipmentNo) ? string.Empty : model.BMEquipmentNo),
                new SqlParameter("@BMFixtureNo", string.IsNullOrEmpty(model.BMFixtureNo) ? string.Empty : model.BMFixtureNo),
                new SqlParameter("@BMType", string.IsNullOrEmpty(model.BMType) ? string.Empty : model.BMType),
                new SqlParameter("@BMSerialNumber", string.IsNullOrEmpty(model.BMSerialNumber) ? string.Empty : model.BMSerialNumber),
                new SqlParameter("@BMQuantity", model.BMQuantity ?? 0),
                new SqlParameter("@BMManufacturedDate", string.IsNullOrEmpty(model.BMManufacturedDate) ? string.Empty : model.BMManufacturedDate),
                new SqlParameter("@BMPower", string.IsNullOrEmpty(model.BMPower) ? string.Empty : model.BMPower),
                new SqlParameter("@BMOutlineDimension", string.IsNullOrEmpty(model.BMOutlineDimension) ? string.Empty : model.BMOutlineDimension),
                new SqlParameter("@BMAbility", string.IsNullOrEmpty(model.BMAbility) ? string.Empty : model.BMAbility),
                new SqlParameter("@BMNeedPressureAir",model.BMNeedPressureAir??0),
                new SqlParameter("@BMNeedCoolingWater",model.BMNeedCoolingWater??0),
                new SqlParameter("@BMIncomingDate", string.IsNullOrEmpty(model.BMIncomingDate) ? string.Empty : model.BMIncomingDate),
                new SqlParameter("@BMRemarks", string.IsNullOrEmpty(model.BMRemarks) ? string.Empty : model.BMRemarks),
                new SqlParameter("@BMIsValid", model.BMIsValid ?? 1),
                new SqlParameter("@BMCreateUserNo", model.BMCreateUserNo),
                new SqlParameter("@BMCreateUserName",model.BMCreateUserName),
                new SqlParameter("@BMCreateTime",model.BMCreateTime),
                new SqlParameter("@BMOperateUserNo",model.BMOperateUserNo),
                new SqlParameter("@BMOperateUserName",model.BMOperateUserName),
                new SqlParameter("@BMOperateTime",model.BMOperateTime),
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
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(MaToolModel model)
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

            if (!string.IsNullOrEmpty(model.BMEquipmentName))
            {
                paramsql.Append(" [BMEquipmentName] = @BMEquipmentName ,");
                param.Add(new SqlParameter("@BMEquipmentName", model.BMEquipmentName));
            }
            if (model.BMClassification != null)
            {
                paramsql.Append(" [BMClassification] = @BMClassification ,");
                param.Add(new SqlParameter("@BMClassification", model.BMClassification));
            }
            if (!string.IsNullOrEmpty(model.BMEquipmentNo))
            {
                paramsql.Append(" [BMEquipmentNo] = @BMEquipmentNo, ");
                param.Add(new SqlParameter("@BMEquipmentNo", model.BMEquipmentNo));
            }
            if (!string.IsNullOrEmpty(model.BMFixtureNo))
            {
                paramsql.Append(" [BMFixtureNo] = @BMFixtureNo ,");
                param.Add(new SqlParameter("@BMFixtureNo", model.BMFixtureNo));
            }
            if (!string.IsNullOrEmpty(model.BMType))
            {
                paramsql.Append(" [BMType] = @BMType , ");
                param.Add(new SqlParameter("@BMType", model.BMType));
            }
            if (!string.IsNullOrEmpty(model.BMSerialNumber))
            {
                paramsql.Append(" [BMSerialNumber] = @BMSerialNumber ,");
                param.Add(new SqlParameter("@BMSerialNumber", model.BMSerialNumber));
            }
            if (model.BMQuantity != null)
            {
                paramsql.Append(" [BMQuantity] = @BMQuantity,");
                param.Add(new SqlParameter("@BMQuantity", model.BMQuantity));
            }
            if (!string.IsNullOrEmpty(model.BMManufacturedDate))
            {
                paramsql.Append(" [BMManufacturedDate] = @BMManufacturedDate ,");
                param.Add(new SqlParameter("@BMManufacturedDate", model.BMManufacturedDate));
            }
            if (!string.IsNullOrEmpty(model.BMPower))
            {
                paramsql.Append(" [BMPower] = @BMPower ,");
                param.Add(new SqlParameter("@BMPower", model.BMPower));
            }
            if (!string.IsNullOrEmpty(model.BMOutlineDimension))
            {
                paramsql.Append(" [BMOutlineDimension] = @BMOutlineDimension , ");
                param.Add(new SqlParameter("@BMOutlineDimension", model.BMOutlineDimension));
            }
            if (!string.IsNullOrEmpty(model.BMAbility))
            {
                paramsql.Append(" [BMAbility] = @BMAbility ,");
                param.Add(new SqlParameter("@BMAbility", model.BMAbility));
            }
            if (model.BMNeedPressureAir != null)
            {
                paramsql.Append(" [BMNeedPressureAir] = @BMNeedPressureAir , ");
                param.Add(new SqlParameter("@BMNeedPressureAir", model.BMNeedPressureAir));
            }
            if (model.BMNeedCoolingWater != null)
            {
                paramsql.Append("  [BMNeedCoolingWater] = @BMNeedCoolingWater , ");
                param.Add(new SqlParameter("@BMNeedCoolingWater", model.BMNeedCoolingWater));
            }

            if (!string.IsNullOrEmpty(model.BMIncomingDate))
            {
                paramsql.Append(" [BMIncomingDate] = @BMIncomingDate ,");
                param.Add(new SqlParameter("@BMIncomingDate", model.BMIncomingDate));
            }
            if (!string.IsNullOrEmpty(model.BMRemarks))
            {
                paramsql.Append(" [BMRemarks] = @BMRemarks ,");
                param.Add(new SqlParameter("@BMRemarks", model.BMRemarks));
            }

            if (model.BMIsValid != null)
            {
                paramsql.Append(" [BMIsValid] = @BMIsValid ,");
                param.Add(new SqlParameter("@BMIsValid", model.BMIsValid));
            }
            if (!string.IsNullOrEmpty(model.BMCreateUserNo))
            {
                paramsql.Append(" [BMCreateUserNo] = @BMCreateUserNo ,");
                param.Add(new SqlParameter("@BMCreateUserNo", model.BMCreateUserNo));
            }
            if (!string.IsNullOrEmpty(model.BMCreateUserName))
            {
                paramsql.Append(" [BMCreateUserName] = @BMCreateUserName ,");
                param.Add(new SqlParameter("@BMCreateUserName", model.BMCreateUserName));
            }
            if (model.BMCreateTime != null)
            {
                paramsql.Append(" [BMCreateTime] = @BMCreateTime ,");
                param.Add(new SqlParameter("@BMCreateTime", model.BMCreateTime));
            }
            if (!string.IsNullOrEmpty(model.BMOperateUserNo))
            {
                paramsql.Append(" [BMOperateUserNo] = @BMOperateUserNo ,");
                param.Add(new SqlParameter("@BMOperateUserNo", model.BMOperateUserNo));
            }
            if (!string.IsNullOrEmpty(model.BMOperateUserName))
            {
                paramsql.Append(" [BMOperateUserName] = @BMOperateUserName ,");
                param.Add(new SqlParameter("@BMOperateUserName", model.BMOperateUserName));
            }
            if (model.BMOperateTime != null)
            {
                paramsql.Append(" [BMOperateTime] = @BMOperateTime ,");
                param.Add(new SqlParameter("@BMOperateTime", model.BMOperateTime));
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<MaToolModel> SearchPageList(MaToolSearchModel param, out int totalCount)
        {
            var list = new List<MaToolModel>();
            var selectSql = new StringBuilder();
            var countSql = new StringBuilder();
            var whereSql = new StringBuilder();
            whereSql.Append(" WHERE 1 = 1  AND  BMIsValid = 1");
            if (param.Classification > 0)
            {
                whereSql.Append(string.Format(" AND BMClassification = {0}", param.Classification));
            }
            if (!string.IsNullOrWhiteSpace(param.MachineName))
            {
                whereSql.Append(string.Format(" AND BMEquipmentName Like N'%{0}%'", param.MachineName));
            }
            if (!string.IsNullOrWhiteSpace(param.EquipmentNo))
            {
                whereSql.Append(string.Format(" AND BMEquipmentNo Like N'%{0}%'", param.EquipmentNo));
            }
            if (!string.IsNullOrWhiteSpace(param.Type))
            {
                whereSql.Append(string.Format(" AND BMType Like N'%{0}%'", param.Type));
            }
            if (!string.IsNullOrWhiteSpace(param.FixtureNo))
            {
                whereSql.Append(string.Format(" AND BMFixtureNo Like N'%{0}%'", param.FixtureNo));
            }
            selectSql.Append(string.Format(@"
                SELECT  newTable.*
                FROM    ( 
                        SELECT TOP ( {0} * {1} )
                                ROW_NUMBER() OVER ( ORDER BY BMOperateTime DESC) RowNum
                                ,[Id]
                                ,[BMEquipmentName]
                                ,[BMClassification]
                                ,[BMEquipmentNo]
                                ,[BMFixtureNo]
                                ,[BMType]
                                ,[BMSerialNumber]
                                ,[BMQuantity]
                                ,[BMManufacturedDate]
                                ,[BMPower]
                                ,[BMOutlineDimension]
                                ,[BMAbility]
                                ,[BMNeedPressureAir]
                                ,[BMNeedCoolingWater]
                                ,[BMIncomingDate]
                                ,[BMRemarks]
                                ,[BMIsValid]
                                ,[BMCreateUserNo]
                                ,[BMCreateUserName]
                                ,[BMCreateTime]
                                ,[BMOperateUserNo]
                                ,[BMOperateUserName]
                                ,[BMOperateTime]
                            FROM {2} with(NOLOCK) {3} 
                            ORDER BY BMOperateTime DESC) newTable
                WHERE   newTable.RowNum > ( ( {0} - 1 ) * {1} )  
            ", param.CurrentPage, param.PageSize, tableName, whereSql.ToString()));
            countSql.Append(string.Format(@"SELECT COUNT(1) FROM {0} with(NOLOCK) {1} ", tableName, whereSql.ToString()));

            var ds = ExecuteDataSet(CommandType.Text, selectSql.ToString());
            totalCount = ExecuteCount(CommandType.Text, countSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<MaToolModel>(dt);
            }
            return list;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<MaToolModel> GetAllList(string key)
        {
            var list = new List<MaToolModel>();
            var selectSql = new StringBuilder();
            selectSql.Append(string.Format(@" 
                    SELECT TOP 10 [BMEquipmentNo],[BMFixtureNo],[BMClassification]
                    FROM {0} with(NOLOCK)  
                    WHERE [BMIsValid] = 1 ", tableName));
            if (!string.IsNullOrEmpty(key))
            {
                selectSql.Append(string.Format(@" AND BMEquipmentNo like '%{0}%'", key));
            }

            selectSql.Append(" ORDER BY Id ASC");
            var ds = ExecuteDataSet(CommandType.Text, selectSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<MaToolModel>(dt);
            }
            return list;
        }

        public List<MaToolModel> GetMainMachineList(string key)
        {
            var list = new List<MaToolModel>();
            var selectSql = new StringBuilder();
            selectSql.Append(string.Format(@" 
                    SELECT TOP 10 [BMEquipmentNo],[BMFixtureNo],[BMClassification]
                    FROM {0} with(NOLOCK)  
                    WHERE [BMIsValid] = 1 AND [BMClassification] = 1 ", tableName));
            if (!string.IsNullOrEmpty(key))
            {
                selectSql.Append(string.Format(@" AND BMEquipmentNo like '%{0}%'", key));
            }

            selectSql.Append(" ORDER BY Id ASC");
            var ds = ExecuteDataSet(CommandType.Text, selectSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<MaToolModel>(dt);
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public MaToolModel GetMaToolById(int materialId)
        {
            var data = new MaToolModel();
            SqlParameter[] para = {
                new SqlParameter("@Id", materialId)
            };

            var sql = @"SELECT TOP 1  [Id]
                          ,[BMEquipmentName]
                          ,[BMClassification]
                          ,[BMEquipmentNo]
                          ,[BMFixtureNo]
                          ,[BMType]
                          ,[BMSerialNumber]
                          ,[BMQuantity]
                          ,[BMManufacturedDate]
                          ,[BMPower]
                          ,[BMOutlineDimension]
                          ,[BMAbility]
                          ,[BMNeedPressureAir]
                          ,[BMNeedCoolingWater]
                          ,[BMIncomingDate]
                          ,[BMRemarks]
                          ,[BMIsValid]
                          ,[BMCreateUserNo]
                          ,[BMCreateUserName]
                          ,[BMCreateTime]
                          ,[BMOperateUserNo]
                          ,[BMOperateUserName]
                          ,[BMOperateTime]
                        FROM " + tableName + " with(NOLOCK) WHERE Id=@Id";

            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    data = DataConvertHelper.DataTableToList<MaToolModel>(dt)[0];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
            return data;
        }
    }
}
