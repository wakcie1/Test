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
    public class MaterialDAL : NewSqlHelper
    {
        private const string tableName = "T_MATERIAL_WORKORDER_INFO";
        private const string workOrder = "T_WORKORDER_INFO";
        private const string materialTool = "T_MATERIAL_TOOL_INFO";

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(MaterialInfoModel model)
        {
            var sql = string.Format(@"INSERT INTO {0}
                                ([MIProcessType]
                              ,[MICustomer]
                              ,[MISapPN]
                              ,[MIProductName]
                              ,[MICustomerPN]
                              ,[MIInjectionMC]
                              ,[MICavity]
                              ,[MICycletime]
                              ,[MICycletimeCav]
                              ,[MIStandardHeadcount]
                              ,[MTStandardScrap]
                              ,[MIMaterialPN]
                              ,[MICavityG]
                              ,[MIMoldNo]
                              ,[MIWorkOrder]
                              ,[MIPicture]
                              ,[MIPictureUrl]
                              ,[MIIsValid]
                              ,[MICreateUserNo]
                              ,[MICreateUserName]
                              ,[MICreateTime]
                              ,[MIOperateUserNo]
                              ,[MIOperateUserName]
                              ,[MIOperateTime]
                              ,[MIAssAC]) 
                                VALUES
                                (@MIProcessType
                                ,@MICustomer
                                ,@MISapPN
                                ,@MIProductName
                                ,@MICustomerPN
                                ,@MIInjectionMC
                                ,@MICavity
                                ,@MICycletime
                                ,@MICycletimeCav
                                ,@MIStandardHeadcount
                                ,@MTStandardScrap
                                ,@MIMaterialPN
                                ,@MICavityG
                                ,@MIMoldNo
                                ,@MIWorkOrder
                                ,@MIPicture
                                ,@MIPictureUrl
                                ,@MIIsValid
                                ,@MICreateUserNo
                                ,@MICreateUserName
                                ,@MICreateTime
                                ,@MIOperateUserNo
                                ,@MIOperateUserName
                                ,@MIOperateTime
                                ,@MIAssAC);
                                select id = scope_identity();", tableName);
            SqlParameter[] para = {
                    new SqlParameter("@MIProcessType", string.IsNullOrEmpty(model.MIProcessType)?string.Empty:model.MIProcessType),
                    new SqlParameter("@MICustomer", string.IsNullOrEmpty(model.MICustomer)?string.Empty:model.MICustomer),
                    new SqlParameter("@MISapPN", string.IsNullOrEmpty(model.MISapPN)?string.Empty:model.MISapPN),
                    new SqlParameter("@MIProductName", string.IsNullOrEmpty(model.MIProductName) ? string.Empty : model.MIProductName),
                    new SqlParameter("@MICustomerPN", string.IsNullOrEmpty(model.MICustomerPN) ? string.Empty : model.MICustomerPN),
                    new SqlParameter("@MIInjectionMC", string.IsNullOrEmpty(model.MIInjectionMC) ? string.Empty : model.MIInjectionMC),
                    new SqlParameter("@MICavity", model.MICavity ?? 0),
                    new SqlParameter("@MICycletime", model.MICycletime ?? 0),
                    new SqlParameter("@MICycletimeCav", model.MICycletimeCav ?? 0),
                    new SqlParameter("@MIStandardHeadcount", model.MIStandardHeadcount ?? 0),
                    new SqlParameter("@MTStandardScrap", string.IsNullOrEmpty(model.MTStandardScrap) ? string.Empty : model.MTStandardScrap),
                    new SqlParameter("@MIMaterialPN",string.IsNullOrEmpty(model.MIMaterialPN) ? string.Empty : model.MIMaterialPN),
                    new SqlParameter("@MICavityG",model.MICavityG ?? 0),
                    new SqlParameter("@MIMoldNo",string.IsNullOrEmpty(model.MIMoldNo) ? string.Empty : model.MIMoldNo),
                    new SqlParameter("@MIWorkOrder",string.IsNullOrEmpty(model.MIWorkOrder) ? string.Empty : model.MIWorkOrder),
                    new SqlParameter("@MIPicture",string.IsNullOrEmpty(model.MIPicture) ? string.Empty : model.MIPicture),
                    new SqlParameter("@MIPictureUrl",string.IsNullOrEmpty(model.MIPictureUrl) ? string.Empty : model.MIPictureUrl),
                    new SqlParameter("@MIIsValid",model.MIIsValid),
                    new SqlParameter("@MICreateUserNo",model.MICreateUserNo),
                    new SqlParameter("@MICreateUserName",model.MICreateUserName),
                    new SqlParameter("@MICreateTime",model.MICreateTime),
                    new SqlParameter("@MIOperateUserNo",model.MIOperateUserNo),
                    new SqlParameter("@MIOperateUserName",model.MIOperateUserName),
                    new SqlParameter("@MIOperateTime",model.MIOperateTime),
                    new SqlParameter("@MIAssAC",string.IsNullOrEmpty(model.MIAssAC)?string.Empty:model.MIAssAC)
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

        public bool Update(MaterialInfoModel model)
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
            if (!string.IsNullOrEmpty(model.MIProcessType))
            {
                paramsql.Append(" [MIProcessType] = @MIProcessType,");
                param.Add(new SqlParameter("@MIProcessType", model.MIProcessType));
            }
            if (!string.IsNullOrEmpty(model.MICustomer))
            {
                paramsql.Append(" [MICustomer] = @MICustomer,");
                param.Add(new SqlParameter("@MICustomer", model.MICustomer));
            }
            if (!string.IsNullOrEmpty(model.MISapPN))
            {
                paramsql.Append(" [MISapPN] = @MISapPN,");
                param.Add(new SqlParameter("@MISapPN", model.MISapPN));
            }
            if (!string.IsNullOrEmpty(model.MIProductName))
            {
                paramsql.Append(" [MIProductName] = @MIProductName,");
                param.Add(new SqlParameter("@MIProductName", model.MIProductName));
            }
            if (!string.IsNullOrEmpty(model.MICustomerPN))
            {
                paramsql.Append(" [MICustomerPN] = @MICustomerPN,");
                param.Add(new SqlParameter("@MICustomerPN", model.MICustomerPN));
            }
            if (!string.IsNullOrEmpty(model.MIInjectionMC))
            {
                paramsql.Append(" [MIInjectionMC] = @MIInjectionMC,");
                param.Add(new SqlParameter("@MIInjectionMC", model.MIInjectionMC));
            }
            if (model.MICavity != null)
            {
                paramsql.Append(" [MICavity] = @MICavity,");
                param.Add(new SqlParameter("@MICavity", model.MICavity));
            }
            if (model.MICycletime != null)
            {
                paramsql.Append(" [MICycletime] = @MICycletime,");
                param.Add(new SqlParameter("@MICycletime", model.MICycletime));
            }
            if (model.MICycletimeCav != null)
            {
                paramsql.Append(" [MICycletimeCav] = @MICycletimeCav,");
                param.Add(new SqlParameter("@MICycletimeCav", model.MICycletimeCav));
            }
            if (model.MIStandardHeadcount != null)
            {
                paramsql.Append(" [MIStandardHeadcount] = @MIStandardHeadcount,");
                param.Add(new SqlParameter("@MIStandardHeadcount", model.MIStandardHeadcount));
            }
            if (!string.IsNullOrEmpty(model.MTStandardScrap))
            {
                paramsql.Append(" [MTStandardScrap] = @MTStandardScrap,");
                param.Add(new SqlParameter("@MTStandardScrap", model.MTStandardScrap));
            }
            if (!string.IsNullOrEmpty(model.MIMaterialPN))
            {
                paramsql.Append(" [MIMaterialPN] = @MIMaterialPN,");
                param.Add(new SqlParameter("@MIMaterialPN", model.MIMaterialPN));
            }
            if (model.MICavityG != null)
            {
                paramsql.Append(" [MICavityG] = @MICavityG,");
                param.Add(new SqlParameter("@MICavityG", model.MICavityG));
            }
            if (!string.IsNullOrEmpty(model.MIMoldNo))
            {
                paramsql.Append(" [MIMoldNo] = @MIMoldNo,");
                param.Add(new SqlParameter("@MIMoldNo", model.MIMoldNo));
            }
            if (!string.IsNullOrEmpty(model.MIWorkOrder))
            {
                paramsql.Append(" [MIWorkOrder] = @MIWorkOrder,");
                param.Add(new SqlParameter("@MIWorkOrder", model.MIWorkOrder));
            }
            if (!string.IsNullOrEmpty(model.MIPicture))
            {
                paramsql.Append(" [MIPicture] = @MIPicture,");
                param.Add(new SqlParameter("@MIPicture", model.MIPicture));
            }
            if (!string.IsNullOrEmpty(model.MIPictureUrl))
            {
                paramsql.Append(" [MIPictureUrl] = @MIPictureUrl,");
                param.Add(new SqlParameter("@MIPictureUrl", model.MIPictureUrl));
            } 
            if (model.MIIsValid != null)
            {
                paramsql.Append(" [MIIsValid] = @MIIsValid, ");
                param.Add(new SqlParameter("@MIIsValid", model.MIIsValid));
            }
            if (!string.IsNullOrEmpty(model.MICreateUserNo))
            {
                paramsql.Append(" [MICreateUserNo] = @MICreateUserNo,");
                param.Add(new SqlParameter("@MICreateUserNo", model.MICreateUserNo));
            }
            if (!string.IsNullOrEmpty(model.MICreateUserName))
            {
                paramsql.Append(" [MICreateUserName] = @MICreateUserName, ");
                param.Add(new SqlParameter("@MICreateUserName", model.MICreateUserName));
            }
            if (model.MICreateTime != null)
            {
                paramsql.Append(" [MICreateTime] = @MICreateTime,");
                param.Add(new SqlParameter("@MICreateTime", model.MICreateTime));
            }
            if (!string.IsNullOrEmpty(model.MIOperateUserNo))
            {
                paramsql.Append(" [MIOperateUserNo] = @MIOperateUserNo,");
                param.Add(new SqlParameter("@MIOperateUserNo", model.MIOperateUserNo));
            }
            if (!string.IsNullOrEmpty(model.MIOperateUserName))
            {
                paramsql.Append(" [MIOperateUserName] = @MIOperateUserName,");
                param.Add(new SqlParameter("@MIOperateUserName", model.MIOperateUserName));
            }
            if (model.MIOperateTime != null)
            {
                paramsql.Append(" [MIOperateTime] = @MIOperateTime,");
                param.Add(new SqlParameter("@MIOperateTime", model.MIOperateTime));
            }
            if (!string.IsNullOrEmpty(model.MIAssAC))
            {
                paramsql.Append(" [MIAssAC] = @MIAssAC,");
                param.Add(new SqlParameter("@MIAssAC", model.MIAssAC));
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
        /// 描述：获取所有的组织框架
        /// </summary>
        /// <returns></returns>
        public List<MaterialInfoModel> SearchMaterialPageList(MaterialSearchModel param, out int totalCount)
        {
            var list = new List<MaterialInfoModel>();
            var selectSql = new StringBuilder();
            var countSql = new StringBuilder();
            var whereSql = new StringBuilder();
            whereSql.Append(" WHERE 1 = 1 AND MIIsValid=1");
            if (!string.IsNullOrEmpty(param.ProductName))
            {
                whereSql.Append(string.Format(" AND MIProductName like '%{0}%'", param.ProductName));
            }
            if (!string.IsNullOrEmpty(param.Customer))
            {
                whereSql.Append(string.Format(" AND MICustomer like '%{0}%'", param.Customer));
            }
            if (!string.IsNullOrEmpty(param.SapPN))
            {
                whereSql.Append(string.Format(" AND MISapPN like '%{0}%'", param.SapPN));
            }
            selectSql.Append(string.Format(@"
                SELECT  newTable.*
                FROM    ( 
                        SELECT TOP ( {0} * {1} )
                                ROW_NUMBER() OVER ( ORDER BY MIOperateTime DESC) RowNum
                                ,[Id]
                                  ,[MIProcessType]
                                  ,[MICustomer]
                                  ,[MISapPN]
                                  ,[MIProductName]
                                  ,[MICustomerPN]
                                  ,[MIInjectionMC]
                                  ,[MICavity]
                                  ,[MICycletime]
                                  ,[MICycletimeCav]
                                  ,[MIStandardHeadcount]
                                  ,[MTStandardScrap]
                                  ,[MIMaterialPN]
                                  ,[MICavityG]
                                  ,[MIMoldNo]
                                  ,[MIWorkOrder]
                                  ,[MIPicture]
                                  ,[MIPictureUrl]
                                  ,[MIIsValid]
                                  ,[MICreateUserNo]
                                  ,[MICreateUserName]
                                  ,[MICreateTime]
                                  ,[MIOperateUserNo]
                                  ,[MIOperateUserName]
                                  ,[MIOperateTime]
                                  ,[MIAssAC]
                            FROM {2} with(NOLOCK) {3} 
                            ORDER BY MIOperateTime DESC) newTable
                WHERE   newTable.RowNum > ( ( {0} - 1 ) * {1} )  
            ", param.CurrentPage, param.PageSize, tableName, whereSql.ToString()));
            countSql.Append(string.Format(@"SELECT COUNT(1) FROM {0} with(NOLOCK) {1} ", tableName, whereSql.ToString()));

            var ds = ExecuteDataSet(CommandType.Text, selectSql.ToString());
            totalCount = ExecuteCount(CommandType.Text, countSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<MaterialInfoModel>(dt);
            }
            return list;
        }
        /// <summary>
        /// 更改MaterialTool
        /// 创建人：wq
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateMaterialTool(MaterialToolModel model)
        {
            if (model.Id == 0)
            {
                return false;
            }
            var upsql = new StringBuilder();
            var paramsql = new StringBuilder();
            var param = new List<SqlParameter>();
            upsql.Append(string.Format("UPDATE {0} SET ", materialTool));

            #region param

            if (!string.IsNullOrEmpty(model.MTToolNo))
            {
                paramsql.Append(" [MTToolNo] = @MTToolNo ,");
                param.Add(new SqlParameter("@MTToolNo", model.MTToolNo));
            }
            if (!string.IsNullOrEmpty(model.MTSapPN))
            {
                paramsql.Append(" [MTSapPN] = @MTSapPN ,");
                param.Add(new SqlParameter("@MTSapPN", model.MTSapPN));
            }
            if (!string.IsNullOrEmpty(model.MTSapQuantity))
            {
                paramsql.Append(" [MTSapQuantity] = @MTSapQuantity ,");
                param.Add(new SqlParameter("@MTSapQuantity", model.MTSapQuantity));
            }
            if (!string.IsNullOrEmpty(model.MTSapLibrary))
            {
                paramsql.Append(" [MTSapLibrary] = @MTSapLibrary, ");
                param.Add(new SqlParameter("@MTSapLibrary", model.MTSapLibrary));
            }
            if (!string.IsNullOrEmpty(model.MTSapProductName))
            {
                paramsql.Append(" [MTSapProductName] = @MTSapProductName ,");
                param.Add(new SqlParameter("@MTSapProductName", model.MTSapProductName));
            }
            if (!string.IsNullOrEmpty(model.MTToolLibrary))
            {
                paramsql.Append(" [MTToolLibrary] = @MTToolLibrary , ");
                param.Add(new SqlParameter("@MTToolLibrary", model.MTToolLibrary));
            }
            if (!string.IsNullOrEmpty(model.MTProductName))
            {
                paramsql.Append(" [MTProductName] = @MTProductName ,");
                param.Add(new SqlParameter("@MTProductName", model.MTProductName));
            }
            if (!string.IsNullOrEmpty(model.MTStatus ))
            {
                paramsql.Append(" [MTStatus] = @MTStatus,");
                param.Add(new SqlParameter("@MTStatus", model.MTStatus));
            }
            if (!string.IsNullOrEmpty(model.MTQuality))
            {
                paramsql.Append(" [MTQuality] = @MTQuality ,");
                param.Add(new SqlParameter("@MTQuality", model.MTQuality));
            }
            if (!string.IsNullOrEmpty(model.MTCustomerPN))
            {
                paramsql.Append(" [MTCustomerPN] = @MTCustomerPN ,");
                param.Add(new SqlParameter("@MTCustomerPN", model.MTCustomerPN));
            }
            if (!string.IsNullOrEmpty(model.MTCustomerNo))
            {
                paramsql.Append(" [MTCustomerNo] = @MTCustomerNo , ");
                param.Add(new SqlParameter("@MTCustomerNo", model.MTCustomerNo));
            }
            if (!string.IsNullOrEmpty(model.MTOutlineDimension))
            {
                paramsql.Append(" [MTOutlineDimension] = @MTOutlineDimension ,");
                param.Add(new SqlParameter("@MTOutlineDimension", model.MTOutlineDimension));
            }
            if (!string.IsNullOrEmpty(model.MTBelong))
            {
                paramsql.Append(" [MTBelong] = @MTBelong , ");
                param.Add(new SqlParameter("@MTBelong", model.MTBelong));
            }
            if (!string.IsNullOrEmpty(model.MTToolSupplier))
            {
                paramsql.Append("  [MTToolSupplier] = @MTToolSupplier , ");
                param.Add(new SqlParameter("@MTToolSupplier", model.MTToolSupplier));
            }

            if (!string.IsNullOrEmpty(model.MTToolSupplierNo))
            {
                paramsql.Append(" [MTToolSupplierNo] = @MTToolSupplierNo ,");
                param.Add(new SqlParameter("@MTToolSupplierNo", model.MTToolSupplierNo));
            }
            if (!string.IsNullOrEmpty(model.MTProductDate))
            {
                paramsql.Append(" [MTProductDate] = @MTProductDate ,");
                param.Add(new SqlParameter("@MTProductDate", model.MTProductDate));
            }

            if (model.MTIsValid != null)
            {
                paramsql.Append(" [MTIsValid] = @MTIsValid ,");
                param.Add(new SqlParameter("@MTIsValid", model.MTIsValid));
            }
            if (!string.IsNullOrEmpty(model.MTCavity))
            {
                paramsql.Append(" [MTCavity] = @MTCavity ,");
                param.Add(new SqlParameter("@MTCavity", model.MTCavity));
            }
            if (!string.IsNullOrEmpty(model.MTCreateUserNo))
            {
                paramsql.Append(" [MTCreateUserNo] = @MTCreateUserNo ,");
                param.Add(new SqlParameter("@MTCreateUserNo", model.MTCreateUserNo));
            }
            if (!string.IsNullOrEmpty(model.MTCreateUserName))
            {
                paramsql.Append(" [MTCreateUserName] = @MTCreateUserName ,");
                param.Add(new SqlParameter("@MTCreateUserName", model.MTCreateUserName));
            }
            if (model.MTCreateTime != null)
            {
                paramsql.Append(" [MTCreateTime] = @MTCreateTime ,");
                param.Add(new SqlParameter("@MTCreateTime", model.MTCreateTime));
            }
            if (!string.IsNullOrEmpty(model.MTOperateUserNo))
            {
                paramsql.Append(" [MTOperateUserNo] = @MTOperateUserNo ,");
                param.Add(new SqlParameter("@MTOperateUserNo", model.MTOperateUserNo));
            }
            if (!string.IsNullOrEmpty(model.MTOperateUserName))
            {
                paramsql.Append(" [MTOperateUserName] = @MTOperateUserName ,");
                param.Add(new SqlParameter("@MTOperateUserName", model.MTOperateUserName));
            }
            if (model.MTOperateTime != null)
            {
                paramsql.Append(" [MTOperateTime] = @MTOperateTime ,");
                param.Add(new SqlParameter("@MTOperateTime", model.MTOperateTime));
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
        /// 描述：获取物料信息
        /// </summary>
        /// <param name="materialId">物料Id</param>
        /// <returns></returns>
        public MaterialInfoModel GetMaterialById(int materialId)
        {
            var material = new MaterialInfoModel();
            SqlParameter[] para = {
                new SqlParameter("@Id", materialId)
            };

            var sql = @"SELECT TOP 1 [Id]
                                      ,[MIProcessType]
                                      ,[MICustomer]
                                      ,[MISapPN]
                                      ,[MIProductName]
                                      ,[MICustomerPN]
                                      ,[MIInjectionMC]
                                      ,[MICavity]
                                      ,[MICycletime]
                                      ,[MICycletimeCav]
                                      ,[MIStandardHeadcount]
                                      ,[MTStandardScrap]
                                      ,[MIMaterialPN]
                                      ,[MICavityG]
                                      ,[MIMoldNo]
                                      ,[MIWorkOrder]
                                      ,[MIPicture]
                                      ,[MIPictureUrl]
                                      ,[MIIsValid]
                                      ,[MICreateUserNo]
                                      ,[MICreateUserName]
                                      ,[MICreateTime]
                                      ,[MIOperateUserNo]
                                      ,[MIOperateUserName]
                                      ,[MIOperateTime]
                                      ,[MIAssAC]
                        FROM " + tableName + " with(NOLOCK) WHERE Id=@Id";

            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                material = DataConvertHelper.DataTableToList<MaterialInfoModel>(dt)[0];
            }
            else
            {
                return null;
            }
            return material;
        }
         
        /// <summary>
        /// 描述：模糊获取有效物料
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<MaterialInfoModel> GetSelectMaterialInfo(string key, string workorder)
        {
            var list = new List<MaterialInfoModel>();
            var sql = string.Format(@"SELECT TOP 20 [Id]
                                              ,[MIProcessType]
                                              ,[MICustomer]
                                              ,[MISapPN]
                                              ,[MIProductName]
                                              ,[MICustomerPN]
                                              ,[MIInjectionMC]
                                              ,[MICavity]
                                              ,[MICycletime]
                                              ,[MICycletimeCav]
                                              ,[MIStandardHeadcount]
                                              ,[MTStandardScrap]
                                              ,[MIMaterialPN]
                                              ,[MICavityG]
                                              ,[MIMoldNo]
                                              ,[MIWorkOrder]
                                              ,[MIPicture]
                                              ,[MIPictureUrl]
                                              ,[MIIsValid]
                                              ,[MICreateUserNo]
                                              ,[MICreateUserName]
                                              ,[MICreateTime]
                                              ,[MIOperateUserNo]
                                              ,[MIOperateUserName]
                                              ,[MIOperateTime]
                                              ,[MIAssAC]
                        FROM {0} with(NOLOCK) WHERE 1=1 AND MIIsValid=1 "
            , tableName);
            if (!string.IsNullOrEmpty(key))
            {
                sql += string.Format(" AND MISapPN like '%{0}%'", key);
            }
            if (!string.IsNullOrEmpty(workorder))
            {
                sql += string.Format(" AND MIWorkOrder ='{0}'", workorder);
            }
            var ds = ExecuteDataSet(CommandType.Text, sql, null);
            if (ds != null && ds.Tables.Count > 0)
            {
                var dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<MaterialInfoModel>(dt);
            }
            return list;
        }
         
        /// <summary>
        /// 描述：模糊获取WorkOrderInfo
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<WorkOrderInfo> GetSelectWorkOrderInfo(string key)
        {
            var list = new List<WorkOrderInfo>();
            var sql = string.Format(@" SELECT TOP 10 WIWorkOrder,WISapPN FROM (
                        SELECT DISTINCT [WIWorkOrder],[WISapPN]
                        FROM {0} with(NOLOCK) WHERE 1=1 AND WIIsValid=1 "
            , workOrder);
            if (!string.IsNullOrEmpty(key))
            {
                sql += string.Format(" AND WIWorkOrder like '%{0}%'", key);
            }
            sql += ") AS t";
            var ds = ExecuteDataSet(CommandType.Text, sql, null);
            if (ds != null && ds.Tables.Count > 0)
            {
                var dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<WorkOrderInfo>(dt);
            }
            return list;
        }


        /// <summary>
        /// 获取workOrders
        /// 创建人：wq
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<WorkOrderInfo> MaterialOtherSearch(MaterialOtherSearchModel model, out int totalCount)
        {
            var list = new List<WorkOrderInfo>();
            StringBuilder sql = new StringBuilder();
            var countSql = new StringBuilder();
            var whereSql = new StringBuilder();
            whereSql.Append(" WHERE 1 = 1 AND WIIsValid = 1");
            whereSql.AppendFormat(" AND WIType ='{0}' ",model.Type);
            if (!string.IsNullOrWhiteSpace(model.WorkOrderName))
            {
                whereSql.AppendFormat(" AND WIWorkOrder like '%{0}%' ", model.WorkOrderName);
            }
            if (!string.IsNullOrWhiteSpace(model.SapNo))
            {
                whereSql.AppendFormat(" AND WISapPN like '%{0}%' ", model.SapNo);
            }
            sql.AppendFormat(@"  SELECT  newTable.*
                FROM    ( 
                        SELECT TOP ( {0} * {1} )
                                ROW_NUMBER() OVER ( ORDER BY WIOperateTime DESC) RowNum
                                  ,[Id]
                                  ,[WIWorkOrder]
                                  ,[WISapPN]
                                  ,[WIProductName]
                                  ,[WIReceiptTime]
                                  ,[WIReceiptBy]
                                  ,[WICloseDateShift]
                                  ,[WIOrderArchived]
                                  ,[WIParameterRecord]
                                  ,[WIToolMaintenanceRecord]
                                  ,[WIToolMachineCheck]
                                  ,[WIQuantityConfirm]
                                  ,[WIArchivedBy]
                                  ,[WIWeeklyCheck]
                                  ,[WIRemarks]
                                  ,[WIGetBy]
                                  ,[WIGetTime]
                                  ,[WICreateUserNo]
                                  ,[WICreateUserName]
                                  ,[WICreateTime]
                                  ,[WIOperateUserNo]
                                  ,[WIOperateUserName]
                                  ,[WIOperateTime]
                                  ,[WIIsValid]
                                  ,[WIType]
                            FROM {2} with(NOLOCK) {3} 
                            ORDER BY WIOperateTime DESC) newTable
                WHERE   newTable.RowNum > ( ( {0} - 1 ) * {1} )  
            ", model.CurrentPage, model.PageSize, workOrder, whereSql.ToString() );

            countSql.Append(string.Format(@"SELECT COUNT(1) FROM {0} with(NOLOCK) {1} ", workOrder, whereSql.ToString()));

            var ds = ExecuteDataSet(CommandType.Text, sql.ToString());
            totalCount = ExecuteCount(CommandType.Text, countSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<WorkOrderInfo>(dt);
            }
            return list;
        }
        /// <summary>
        /// Insert WorkOrder
        /// 创建人：wq
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertWorkOrder(WorkOrderInfo model)
        {
            var sql = string.Format(@"INSERT INTO {0}
                                ([WIWorkOrder]
                                   ,[WISapPN]
                                   ,[WIProductName]
                                   ,[WIReceiptTime]
                                   ,[WIReceiptBy]
                                   ,[WICloseDateShift]
                                   ,[WIOrderArchived]
                                   ,[WIParameterRecord]
                                   ,[WIToolMaintenanceRecord]
                                   ,[WIToolMachineCheck]
                                   ,[WIQuantityConfirm]
                                   ,[WIArchivedBy]
                                   ,[WIWeeklyCheck]
                                   ,[WIRemarks]
                                   ,[WIGetBy]
                                   ,[WIGetTime]
                                   ,[WICreateUserNo]
                                   ,[WICreateUserName]
                                   ,[WICreateTime]
                                   ,[WIOperateUserNo]
                                   ,[WIOperateUserName]
                                   ,[WIOperateTime]
                                   ,[WIIsValid]
                                   ,[WIType])
                                VALUES
                                (  @WIWorkOrder
                                   ,@WISapPN 
                                   ,@WIProductName 
                                   ,@WIReceiptTime 
                                   ,@WIReceiptBy
                                   ,@WICloseDateShift 
                                   ,@WIOrderArchived 
                                   ,@WIParameterRecord
                                   ,@WIToolMaintenanceRecord 
                                   ,@WIToolMachineCheck 
                                   ,@WIQuantityConfirm 
                                   ,@WIArchivedBy 
                                   ,@WIWeeklyCheck
                                   ,@WIRemarks 
                                   ,@WIGetBy 
                                   ,@WIGetTime 
                                   ,@WICreateUserNo
                                   ,@WICreateUserName 
                                   ,@WICreateTime 
                                   ,@WIOperateUserNo 
                                   ,@WIOperateUserName
                                   ,@WIOperateTime 
                                   ,@WIIsValid 
                                   ,@WIType );
                                select id = scope_identity();", workOrder);
            SqlParameter[] para = {
                    new SqlParameter("@WIWorkOrder", string.IsNullOrEmpty(model.WIWorkOrder)?string.Empty:model.WIWorkOrder),
                    new SqlParameter("@WISapPN", string.IsNullOrEmpty(model.WISapPN) ? string.Empty : model.WISapPN),
                    new SqlParameter("@WIProductName", string.IsNullOrEmpty(model.WIProductName) ? string.Empty : model.WIProductName),
                    new SqlParameter("@WIReceiptTime", string.IsNullOrEmpty(model.WIReceiptTime) ? string.Empty : model.WIReceiptTime),
                    new SqlParameter("@WIReceiptBy", string.IsNullOrEmpty(model.WIReceiptBy) ? string.Empty : model.WIReceiptBy),
                    new SqlParameter("@WICloseDateShift", string.IsNullOrEmpty(model.WICloseDateShift) ? string.Empty : model.WICloseDateShift),
                    new SqlParameter("@WIOrderArchived", string.IsNullOrEmpty(model.WIOrderArchived) ? string.Empty : model.WIOrderArchived),
                    new SqlParameter("@WIParameterRecord", string.IsNullOrEmpty(model.WIParameterRecord) ? string.Empty : model.WIParameterRecord),
                    new SqlParameter("@WIToolMaintenanceRecord", string.IsNullOrEmpty(model.WIToolMaintenanceRecord) ? string.Empty : model.WIToolMaintenanceRecord),
                    new SqlParameter("@WIToolMachineCheck",string.IsNullOrEmpty(model.WIToolMachineCheck ) ? string.Empty : model.WIToolMachineCheck ),
                    new SqlParameter("@WIQuantityConfirm",string.IsNullOrEmpty(model.WIQuantityConfirm ) ? string.Empty : model.WIToolMachineCheck ),
                    new SqlParameter("@WIArchivedBy",string.IsNullOrEmpty(model.WIArchivedBy ) ? string.Empty : model.WIArchivedBy  ),
                    new SqlParameter("@WIWeeklyCheck",string.IsNullOrEmpty(model.WIWeeklyCheck ) ? string.Empty : model.WIWeeklyCheck ),
                    new SqlParameter("@WIRemarks",string.IsNullOrEmpty(model.WIRemarks ) ? string.Empty : model.WIRemarks  ),
                    new SqlParameter("@WIGetBy",string.IsNullOrEmpty(model.WIGetBy ) ? string.Empty : model.WIGetBy ),
                    new SqlParameter("@WIGetTime",string.IsNullOrEmpty(model.WIGetTime ) ? string.Empty : model.WIGetTime ),
                    new SqlParameter("@WICreateUserNo",string.IsNullOrEmpty(model.WICreateUserNo ) ? string.Empty : model.WICreateUserNo ),
                    new SqlParameter("@WICreateUserName",string.IsNullOrEmpty(model.WICreateUserName ) ? string.Empty : model.WICreateUserName ),
                    new SqlParameter("@WICreateTime",model.WICreateTime),
                    new SqlParameter("@WIOperateUserNo",string.IsNullOrEmpty(model.WIOperateUserNo ) ? string.Empty : model.WIOperateUserNo ),
                    new SqlParameter("@WIOperateUserName",string.IsNullOrEmpty(model.WIOperateUserName ) ? string.Empty : model.WIOperateUserName ),
                    new SqlParameter("@WIOperateTime",model.WIOperateTime),
                    new SqlParameter("@WIIsValid",model.WIIsValid),
                    new SqlParameter("@WIType",model.WIType),
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
        /// 根据Id获取WorkOrderInfo
        /// 创建人：wq
        /// </summary>
        /// <param name="workOrderId"></param>
        /// <returns></returns>
        public WorkOrderInfo GetWorkOrderById(int workOrderId)
        {
            var data = new WorkOrderInfo();
            SqlParameter[] para = {
                new SqlParameter("@Id", workOrderId)
            };

            var sql = @"SELECT TOP 1   [Id]
                          ,[WIWorkOrder]
                          ,[WISapPN]
                          ,[WIProductName]
                          ,[WIReceiptTime]
                          ,[WIReceiptBy]
                          ,[WICloseDateShift]
                          ,[WIOrderArchived]
                          ,[WIParameterRecord]
                          ,[WIToolMaintenanceRecord]
                          ,[WIToolMachineCheck]
                          ,[WIQuantityConfirm]
                          ,[WIArchivedBy]
                          ,[WIWeeklyCheck]
                          ,[WIRemarks]
                          ,[WIGetBy]
                          ,[WIGetTime]
                          ,[WICreateUserNo]
                          ,[WICreateUserName]
                          ,[WICreateTime]
                          ,[WIOperateUserNo]
                          ,[WIOperateUserName]
                          ,[WIOperateTime]
                          ,[WIIsValid]
                          ,[WIType]
                        FROM " + workOrder + " with(NOLOCK) WHERE Id=@Id AND WIIsValid=1";

            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    data = DataConvertHelper.DataTableToList<WorkOrderInfo>(dt)[0];
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

        /// <summary>
        /// Update WorkOrder表
        /// 创建人：wq
        /// </summary>
        /// <param name="model"></param>
        public bool UpdateWorkOrder(WorkOrderInfo model)
        {
            if (model.Id == 0)
            {
                return false;
            }
            var upsql = new StringBuilder();
            var paramsql = new StringBuilder();
            var param = new List<SqlParameter>();
            upsql.Append(string.Format("UPDATE {0} SET ", workOrder));


            #region param

            if (!string.IsNullOrEmpty(model.WIWorkOrder))
            {
                paramsql.Append(" [WIWorkOrder] = @WIWorkOrder,");
                param.Add(new SqlParameter("@WIWorkOrder", model.WIWorkOrder));
            }
            if (!string.IsNullOrEmpty(model.WISapPN))
            {
                paramsql.Append(" [WISapPN] = @WISapPN,");
                param.Add(new SqlParameter("@WISapPN", model.WISapPN));
            }
            if (!string.IsNullOrEmpty(model.WIProductName))
            {
                paramsql.Append(" [WIProductName] = @WIProductName,");
                param.Add(new SqlParameter("@WIProductName", model.WIProductName));
            }
            if (!string.IsNullOrEmpty(model.WIReceiptTime))
            {
                paramsql.Append(" [WIReceiptTime] = @WIReceiptTime, ");
                param.Add(new SqlParameter("@WIReceiptTime", model.WIReceiptTime));
            }
            if (!string.IsNullOrEmpty(model.WIReceiptBy))
            {
                paramsql.Append(" [WIReceiptBy] = @WIReceiptBy,");
                param.Add(new SqlParameter("@WIReceiptBy", model.WIReceiptBy));
            }
            if (!string.IsNullOrEmpty(model.WICloseDateShift))
            {
                paramsql.Append(" [WICloseDateShift] = @WICloseDateShift,");
                param.Add(new SqlParameter("@WICloseDateShift", model.WICloseDateShift));
            }
            if (!string.IsNullOrEmpty(model.WIOrderArchived))
            {
                paramsql.Append(" [WIOrderArchived] = @WIOrderArchived,");
                param.Add(new SqlParameter("@WIOrderArchived", model.WIOrderArchived));
            }
            if (!string.IsNullOrEmpty(model.WIParameterRecord))
            {
                paramsql.Append(" [WIParameterRecord] = @WIParameterRecord,");
                param.Add(new SqlParameter("@WIParameterRecord", model.WIParameterRecord));
            }
            if (!string.IsNullOrEmpty(model.WIToolMaintenanceRecord))
            {
                paramsql.Append(" [WIToolMaintenanceRecord] = @WIToolMaintenanceRecord,");
                param.Add(new SqlParameter("@WIToolMaintenanceRecord", model.WIToolMaintenanceRecord));
            } 
            if (!string.IsNullOrEmpty(model.WIWeeklyCheck) )
            {
                paramsql.Append(" [WIToolMachineCheck] = @WIToolMachineCheck,");
                param.Add(new SqlParameter("@WIToolMachineCheck", model.WIToolMachineCheck));
            }
            if (!string.IsNullOrEmpty(model.WIWeeklyCheck) )
            {
                paramsql.Append(" [WIQuantityConfirm] = @WIQuantityConfirm,");
                param.Add(new SqlParameter("@WIQuantityConfirm", model.WIQuantityConfirm));
            }
            if (!string.IsNullOrEmpty(model.WIWeeklyCheck) )
            {
                paramsql.Append(" [WIArchivedBy] = @WIArchivedBy,");
                param.Add(new SqlParameter("@WIArchivedBy", model.WIArchivedBy));
            }
            if (!string.IsNullOrEmpty(model.WIWeeklyCheck))
            {
                paramsql.Append(" [WIWeeklyCheck] = @WIWeeklyCheck,");
                param.Add(new SqlParameter("@WIWeeklyCheck", model.WIWeeklyCheck));
            }
            if (!string.IsNullOrEmpty(model.WIRemarks))
            {
                paramsql.Append(" [WIRemarks] = @WIRemarks,");
                param.Add(new SqlParameter("@WIRemarks", model.WIRemarks));
            }
            if (!string.IsNullOrEmpty(model.WIGetBy))
            {
                paramsql.Append(" [WIGetBy] = @WIGetBy, ");
                param.Add(new SqlParameter("@WIGetBy", model.WIGetBy));
            }
            if (!string.IsNullOrEmpty(model.WIGetTime))
            {
                paramsql.Append(" [WIGetTime] = @WIGetTime, ");
                param.Add(new SqlParameter("@WIGetTime", model.WIGetTime));
            }
            if (!string.IsNullOrEmpty(model.WICreateUserNo))
            {
                paramsql.Append(" [WICreateUserNo] = @WICreateUserNo,");
                param.Add(new SqlParameter("@WICreateUserNo", model.WICreateUserNo));
            }
            if (!string.IsNullOrEmpty(model.WICreateUserName))
            {
                paramsql.Append(" [WICreateUserName] = @WICreateUserName, ");
                param.Add(new SqlParameter("@WICreateUserName", model.WICreateUserName));
            }
            if (model.WICreateTime != null)
            {
                paramsql.Append(" [WICreateTime] = @WICreateTime,");
                param.Add(new SqlParameter("@WICreateTime", model.WICreateTime));
            }
            if (!string.IsNullOrEmpty(model.WIOperateUserNo))
            {
                paramsql.Append(" [WIOperateUserNo] = @WIOperateUserNo,");
                param.Add(new SqlParameter("@WIOperateUserNo", model.WIOperateUserNo));
            }
            if (!string.IsNullOrEmpty(model.WIOperateUserName))
            {
                paramsql.Append(" [WIOperateUserName] = @WIOperateUserName,");
                param.Add(new SqlParameter("@WIOperateUserName", model.WIOperateUserName));
            }
            if (model.WIOperateTime != null)
            {
                paramsql.Append(" [WIOperateTime] = @WIOperateTime,");
                param.Add(new SqlParameter("@WIOperateTime", model.WIOperateTime));
            }
            if ( model.WIIsValid!= null)
            {
                paramsql.Append(" [WIIsValid] = @WIIsValid,");
                param.Add(new SqlParameter("@WIIsValid", model.WIIsValid));
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

        #region MaterialTool
        /// <summary>
        /// 插入MaterialTool
        /// 创建人：wq
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long InsertMaterialTool(MaterialToolModel model)
        {
            var sql = @"INSERT INTO " + materialTool +
                    @"([MTToolNo]
                       ,[MTSapPN]
                       ,[MTSapQuantity]
                       ,[MTSapLibrary]
                       ,[MTSapProductName]
                       ,[MTToolLibrary]
                       ,[MTProductName]
                       ,[MTStatus]
                       ,[MTQuality]
                       ,[MTCustomerPN]
                       ,[MTCustomerNo]
                       ,[MTOutlineDimension]
                       ,[MTBelong]
                       ,[MTToolSupplier]
                       ,[MTToolSupplierNo]
                       ,[MTProductDate]
                       ,[MTCavity]
                       ,[MTIsValid]
                       ,[MTCreateUserNo]
                       ,[MTCreateUserName]
                       ,[MTCreateTime]
                       ,[MTOperateUserNo]
                       ,[MTOperateUserName]
                       ,[MTOperateTime])
                    VALUES
                    (@MTToolNo
                    , @MTSapPN
                    , @MTSapQuantity
                    , @MTSapLibrary
                    , @MTSapProductName
                    , @MTToolLibrary
                    , @MTProductName
                    , @MTStatus
                    , @MTQuality
                    , @MTCustomerPN
                    , @MTCustomerNo
                    , @MTOutlineDimension
                    , @MTBelong
                    , @MTToolSupplier
                    , @MTToolSupplierNo
                    , @MTProductDate
                    , @MTCavity
                    , @MTIsValid
                    , @MTCreateUserNo
                    , @MTCreateUserName
                    , @MTCreateTime
                    , @MTOperateUserNo 
                    , @MTOperateUserName
                    , @MTOperateTime)" +
                "  select id = scope_identity()";
            SqlParameter[] para = {
                new SqlParameter("@MTToolNo", string.IsNullOrEmpty(model.MTToolNo)?string.Empty:model.MTToolNo),
                new SqlParameter("@MTSapPN", string.IsNullOrEmpty(model.MTSapPN) ? string.Empty :model.MTSapPN),
                new SqlParameter("@MTSapQuantity", string.IsNullOrEmpty(model.MTSapQuantity) ? string.Empty : model.MTSapQuantity),
                new SqlParameter("@MTSapLibrary", string.IsNullOrEmpty(model.MTSapLibrary) ? string.Empty : model.MTSapLibrary),
                new SqlParameter("@MTSapProductName", string.IsNullOrEmpty(model.MTSapProductName) ? string.Empty : model.MTSapProductName),
                new SqlParameter("@MTToolLibrary", string.IsNullOrEmpty(model.MTToolLibrary) ? string.Empty : model.MTToolLibrary),
                new SqlParameter("@MTProductName",string.IsNullOrEmpty(model.MTProductName) ? string.Empty : model.MTProductName  ),
                new SqlParameter("@MTStatus", string.IsNullOrEmpty(model.MTStatus) ? string.Empty : model.MTStatus),
                new SqlParameter("@MTQuality", string.IsNullOrEmpty(model.MTQuality) ? string.Empty : model.MTQuality),
                new SqlParameter("@MTCustomerPN", string.IsNullOrEmpty(model.MTCustomerPN) ? string.Empty : model.MTCustomerPN),
                new SqlParameter("@MTCustomerNo", string.IsNullOrEmpty(model.MTCustomerNo) ? string.Empty : model.MTCustomerNo),
                new SqlParameter("@MTOutlineDimension", string.IsNullOrEmpty(model.MTOutlineDimension) ? string.Empty :model.MTOutlineDimension),
                new SqlParameter("@MTBelong",string.IsNullOrEmpty(model.MTOutlineDimension) ? string.Empty:model.MTBelong ),
                new SqlParameter("@MTToolSupplier", string.IsNullOrEmpty(model.MTToolSupplier) ? string.Empty : model.MTToolSupplier),
                new SqlParameter("@MTToolSupplierNo", string.IsNullOrEmpty(model.MTToolSupplierNo) ? string.Empty : model.MTToolSupplierNo),
                new SqlParameter("@MTProductDate", string.IsNullOrEmpty(model.MTProductDate) ? string.Empty :model.MTProductDate  ),
                new SqlParameter("@MTCavity" ,string.IsNullOrEmpty(model.MTCavity) ? string.Empty :model.MTCavity ),
                new SqlParameter("@MTIsValid",model.MTIsValid),
                new SqlParameter("@MTCreateUserNo",string.IsNullOrEmpty(model.MTCreateUserNo) ? string.Empty :model.MTCreateUserNo),
                new SqlParameter("@MTCreateUserName",string.IsNullOrEmpty(model.MTCreateUserName) ? string.Empty :model.MTCreateUserName),
                new SqlParameter("@MTCreateTime",model.MTCreateTime),
                new SqlParameter("@MTOperateUserNo",string.IsNullOrEmpty(model.MTOperateUserNo) ? string.Empty :model.MTOperateUserNo),
                new SqlParameter("@MTOperateUserName",string.IsNullOrEmpty(model.MTOperateUserName) ? string.Empty :model.MTOperateUserName),
                new SqlParameter("@MTOperateTime",model.MTOperateTime),
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
        /// 查询
        /// 创建人：wq
        /// </summary>
        /// <param name="model"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<MaterialToolModel> MaterialToolSearch(MaterialToolSearchModel param, out int totalCount)
        {
            var list = new List<MaterialToolModel>();
            var selectSql = new StringBuilder();
            var countSql = new StringBuilder();
            var whereSql = new StringBuilder();
            whereSql.Append(" WHERE 1 = 1 AND MTIsValid=1");
            if (!string.IsNullOrEmpty(param.ToolNo))
            {
                whereSql.Append(string.Format(" AND MTToolNo like '%{0}%'", param.ToolNo));
            }
            if (!string.IsNullOrEmpty(param.ProductName))
            {
                whereSql.Append(string.Format(" AND MTProductName like N'%{0}%'", param.ProductName));
            }
            if (!string.IsNullOrEmpty(param.ToolSupplier))
            {
                whereSql.Append(string.Format(" AND MTToolSupplier like N'%{0}%'", param.ToolSupplier));
            }
            selectSql.Append(string.Format(@"
                SELECT  newTable.*
                FROM    ( 
                        SELECT TOP ( {0} * {1} )
                                ROW_NUMBER() OVER ( ORDER BY MTOperateTime DESC) RowNum
                                  ,[Id]
                                  ,[MTToolNo]
                                  ,[MTSapPN]
                                  ,[MTSapQuantity]
                                  ,[MTSapLibrary]
                                  ,[MTSapProductName]
                                  ,[MTToolLibrary]
                                  ,[MTProductName]
                                  ,[MTStatus]
                                  ,[MTQuality]
                                  ,[MTCustomerPN]
                                  ,[MTCustomerNo]
                                  ,[MTOutlineDimension]
                                  ,[MTBelong]
                                  ,[MTToolSupplier]
                                  ,[MTToolSupplierNo]
                                  ,[MTProductDate]
                                  ,[MTCavity]
                                  ,[MTIsValid]
                                  ,[MTCreateUserNo]
                                  ,[MTCreateUserName]
                                  ,[MTCreateTime]
                                  ,[MTOperateUserNo]
                                  ,[MTOperateUserName]
                                  ,[MTOperateTime]
                            FROM {2} with(NOLOCK) {3} 
                            ORDER BY MTOperateTime DESC) newTable
                WHERE   newTable.RowNum > ( ( {0} - 1 ) * {1} )  
            ", param.CurrentPage, param.PageSize, materialTool, whereSql.ToString()));
            countSql.Append(string.Format(@"SELECT COUNT(1) FROM {0} with(NOLOCK) {1} ", materialTool, whereSql.ToString()));

            var ds = ExecuteDataSet(CommandType.Text, selectSql.ToString());
            totalCount = ExecuteCount(CommandType.Text, countSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<MaterialToolModel>(dt);
            }
            return list;
        }
        /// <summary>
        /// 根据Id获取materialTool
        /// 创建人；wq
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public MaterialToolModel GetMaterialToolById(int materialId)
        {
            var data = new MaterialToolModel();
            SqlParameter[] para = {
                new SqlParameter("@Id", materialId)
            };

            var sql = @"SELECT TOP 1  [Id]
                                  ,[MTToolNo]
                                  ,[MTSapPN]
                                  ,[MTSapQuantity]
                                  ,[MTSapLibrary]
                                  ,[MTSapProductName]
                                  ,[MTToolLibrary]
                                  ,[MTProductName]
                                  ,[MTStatus]
                                  ,[MTQuality]
                                  ,[MTCustomerPN]
                                  ,[MTCustomerNo]
                                  ,[MTOutlineDimension]
                                  ,[MTBelong]
                                  ,[MTToolSupplier]
                                  ,[MTToolSupplierNo]
                                  ,[MTProductDate]
                                  ,[MTCavity]
                                  ,[MTIsValid]
                                  ,[MTCreateUserNo]
                                  ,[MTCreateUserName]
                                  ,[MTCreateTime]
                                  ,[MTOperateUserNo]
                                  ,[MTOperateUserName]
                                  ,[MTOperateTime]
                        FROM " + materialTool + " with(NOLOCK) WHERE Id=@Id";

            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    data = DataConvertHelper.DataTableToList<MaterialToolModel>(dt)[0];
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<MaterialToolModel> GetMaterialToolList(string key)
        {
            var list = new List<MaterialToolModel>();
            var selectSql = new StringBuilder();
            selectSql.Append(string.Format(@" 
                    SELECT TOP 10 [MTToolNo]
                    FROM {0} with(NOLOCK)  
                    WHERE [MTIsValid] = 1 ", materialTool));
            if (!string.IsNullOrEmpty(key))
            {
                selectSql.Append(string.Format(@" AND MTToolNo like '%{0}%'", key));
            }

            selectSql.Append(" ORDER BY Id ASC");
            var ds = ExecuteDataSet(CommandType.Text, selectSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<MaterialToolModel>(dt);
            }
            return list;
        }
        #endregion
    }
}
