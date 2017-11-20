using Common;
using Common.Enum;
using Model.Problem;
using Model.TableModel;
using Model.ViewModel.Department;
using Model.ViewModel.Problem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess
{
    public class ProblemDAL : NewSqlHelper
    {
        private const string problemTable = "T_PROBLEM_INFO";
        private const string problemSolvingteamTable = "T_PROBLEM_SOLVINGTEAM";
        private const string problemQualityalertTable = "T_PROBLEM_QUALITYALERT";
        private const string problemSortingactivityTable = "T_PROBLEM_SORTINGACTIVITY";
        private const string problemActioncontainmentTable = "T_PROBLEM_ACTION_CONTAINMENT";
        private const string problemActionwhyanalysisTable = "T_PROBLEM_ACTION_WHYANALYSIS";
        private const string problemActionfactoranalysisTable = "T_PROBLEM_ACTION_FACTORANALYSIS";
        private const string problemActioncorrectiveTable = "T_PROBLEM_ACTION_CORRECTIVE";
        private const string problemActionpreventiveTable = "T_PROBLEM_ACTION_PREVENTIVE";
        private const string problemLayeredauditTable = "T_PROBLEM_LAYEREDAUDIT";
        private const string problemVerificationTable = "T_PROBLEM_VERIFICATION";
        private const string problemStandardizationTable = "T_PROBLEM_STANDARDIZATION";

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(ProblemInfoModel model)
        {
            var sql = @"INSERT INTO " + problemTable +
                @" ([PIProblemNo]
                ,[PIProcess]
                ,[PIMachine]
                ,[PITool]
                ,[PIMaterialId]
                ,[PICustomerPN]
                ,[PICustomer]
                ,[PIProductName]
                ,[PIWorkOrder]
                ,[PIProblemDate]
                ,[PIProblemSource]
                ,[PIDefectType]
                ,[PIDefectCode]
                ,[PIDefectQty]
                ,[PIShiftType]
                ,[PIIsRepeated]
                ,[PIProblemDesc]
                ,[PIPicture1]
                ,[PIPicture2]
                ,[PIPicture3]
                ,[PIPicture4]
                ,[PIPicture5]
                ,[PIPicture6]
                ,[PIProcessStatus]
                ,[PIStatus]
                ,[PISeverity]
                ,[PIRootCause]
                ,[PIRootCauseAssignNo]
                ,[PIRootCauseAssignName]
                ,[PIActionPlan]
                ,[PIExtendPorjects]
                ,[PIExtendComment]
                ,[PIExtendApproveComment]
                ,[PIIsValid]
                ,[PICreateUserNo]
                ,[PICreateUserName]
                ,[PICreateTime]
                ,[PIOperateUserNo]
                ,[PIOperateUserName]
                ,[PIOperateTime]
                ,[PISapPN]
                ,[PINextProblemDate]
                ,[PIFinishDate]
                ,[PIApproveLayeredAudit]
                ,[PIApproveVerification]
                ,[PIApproveStandardization])" +
                @" VALUES (@PIProblemNo
                , @PIProcess
                , @PIMachine
                , @PITool
                , @PIMaterialId
                , @PICustomerPN
                , @PICustomer
                , @PIProductName
                , @PIWorkOrder
                , @PIProblemDate
                , @PIProblemSource
                , @PIDefectType
                , @PIDefectCode
                , @PIDefectQty
                , @PIShiftType
                , @PIIsRepeated
                , @PIProblemDesc
                , @PIPicture1
                , @PIPicture2
                , @PIPicture3
                , @PIPicture4
                , @PIPicture5
                , @PIPicture6
                , @PIProcessStatus
                , @PIStatus
                , @PISeverity
                , @PIRootCause
                , @PIRootCauseAssignNo
                , @PIRootCauseAssignName
                , @PIActionPlan
                , @PIExtendPorjects
                , @PIExtendComment
                , @PIExtendApproveComment
                , @PIIsValid
                , @PICreateUserNo
                , @PICreateUserName
                , @PICreateTime
                , @PIOperateUserNo
                , @PIOperateUserName
                , @PIOperateTime
                , @PISapPN
                , @PINextProblemDate
                , @PIFinishDate
                , @PIApproveLayeredAudit
                , @PIApproveVerification
                , @PIApproveStandardization) " +
                "  select id = scope_identity()";
            SqlParameter[] para = {
                new SqlParameter("@PIProblemNo", model.PIProblemNo),
                new SqlParameter("@PIProcess", string.IsNullOrEmpty(model.PIProcess)?string.Empty:model.PIProcess),
                new SqlParameter("@PIMachine", string.IsNullOrEmpty(model.PIMachine)?string.Empty:model.PIMachine),
                new SqlParameter("@PITool", string.IsNullOrEmpty(model.PITool)?string.Empty:model.PITool),
                new SqlParameter("@PIMaterialId", model.PIMaterialId),
                new SqlParameter("@PICustomerPN", string.IsNullOrEmpty(model.PICustomerPN)?string.Empty:model.PICustomerPN),
                new SqlParameter("@PICustomer", string.IsNullOrEmpty(model.PICustomer)?string.Empty:model.PICustomer),
                new SqlParameter("@PIProductName", string.IsNullOrEmpty(model.PIProductName)?string.Empty:model.PIProductName),
                new SqlParameter("@PIWorkOrder", string.IsNullOrEmpty(model.PIWorkOrder)?string.Empty:model.PIWorkOrder),
                new SqlParameter("@PIProblemDate", model.PIProblemDate),
                new SqlParameter("@PIProblemSource", string.IsNullOrEmpty(model.PIProblemSource)?string.Empty:model.PIProblemSource),
                new SqlParameter("@PIDefectType", string.IsNullOrEmpty(model.PIDefectType)?string.Empty:model.PIDefectType),
                new SqlParameter("@PIDefectCode", string.IsNullOrEmpty(model.PIDefectCode)?string.Empty:model.PIDefectCode),
                new SqlParameter("@PIDefectQty",model.PIDefectQty ?? 0),
                new SqlParameter("@PIShiftType", string.IsNullOrEmpty(model.PIShiftType)?string.Empty:model.PIShiftType),
                new SqlParameter("@PIIsRepeated",model.PIIsRepeated ?? 0),
                new SqlParameter("@PIProblemDesc", string.IsNullOrEmpty(model.PIProblemDesc)?string.Empty:model.PIProblemDesc),
                new SqlParameter("@PIPicture1", string.IsNullOrEmpty(model.PIPicture1)?string.Empty:model.PIPicture1),
                new SqlParameter("@PIPicture2", string.IsNullOrEmpty(model.PIPicture2)?string.Empty:model.PIPicture2),
                new SqlParameter("@PIPicture3", string.IsNullOrEmpty(model.PIPicture3)?string.Empty:model.PIPicture3),
                new SqlParameter("@PIPicture4", string.IsNullOrEmpty(model.PIPicture4)?string.Empty:model.PIPicture4),
                new SqlParameter("@PIPicture5", string.IsNullOrEmpty(model.PIPicture5)?string.Empty:model.PIPicture5),
                new SqlParameter("@PIPicture6", string.IsNullOrEmpty(model.PIPicture6)?string.Empty:model.PIPicture6),
                new SqlParameter("@PIProcessStatus",model.PIProcessStatus ?? 0),
                new SqlParameter("@PIStatus",model.PIStatus ?? 0),
                new SqlParameter("@PISeverity",model.PISeverity ?? 0),
                new SqlParameter("@PIRootCause", string.IsNullOrEmpty(model.PIRootCause)?string.Empty:model.PIRootCause),
                new SqlParameter("@PIRootCauseAssignNo", string.IsNullOrEmpty(model.PIRootCauseAssignNo)?string.Empty:model.PIRootCauseAssignNo),
                new SqlParameter("@PIRootCauseAssignName", string.IsNullOrEmpty(model.PIRootCauseAssignName)?string.Empty:model.PIRootCauseAssignName),
                new SqlParameter("@PIActionPlan", string.IsNullOrEmpty(model.PIActionPlan)?string.Empty:model.PIActionPlan),
                new SqlParameter("@PIExtendPorjects",model.PIExtendPorjects ?? 0),
                new SqlParameter("@PIExtendComment", string.IsNullOrEmpty(model.PIExtendComment)?string.Empty:model.PIExtendComment),
                new SqlParameter("@PIExtendApproveComment", string.IsNullOrEmpty(model.PIExtendApproveComment)?string.Empty:model.PIExtendApproveComment),
                new SqlParameter("@PIIsValid",model.PIIsValid ?? 0),
                new SqlParameter("@PICreateUserNo", string.IsNullOrEmpty(model.PICreateUserNo)?string.Empty:model.PICreateUserNo),
                new SqlParameter("@PICreateUserName", string.IsNullOrEmpty(model.PICreateUserName)?string.Empty:model.PICreateUserName),
                new SqlParameter("@PICreateTime",model.PICreateTime),
                new SqlParameter("@PIOperateUserNo", string.IsNullOrEmpty(model.PIOperateUserNo) ? string.Empty : model.PIOperateUserNo),
                new SqlParameter("@PIOperateUserName", string.IsNullOrEmpty(model.PIOperateUserName) ? string.Empty : model.PIOperateUserName),
                new SqlParameter("@PIOperateTime",model.PIOperateTime),
                new SqlParameter("@PISapPN", string.IsNullOrEmpty(model.PISapPN)?string.Empty:model.PISapPN),
                new SqlParameter("@PINextProblemDate", model.PINextProblemDate ?? Convert.ToDateTime("1900-1-1")),
                new SqlParameter("@PIFinishDate", model.PIFinishDate ?? Convert.ToDateTime("1900-1-1")),
                new SqlParameter("@PIApproveLayeredAudit",model.PIApproveLayeredAudit ?? 0),
                new SqlParameter("@PIApproveVerification",model.PIApproveVerification ?? 0),
                new SqlParameter("@PIApproveStandardization",model.PIApproveStandardization ?? 0),
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

        public IEnumerable<ProblemInfoModel> ProblemSearchResult(ProblemSearchModel model)
        {
            List<ProblemInfoModel> list = new List<ProblemInfoModel>();

            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@" SELECT [Id]
                                ,[PIProblemNo]
                                ,[PIProcess]
                                ,[PIMachine]
                                ,[PITool]
                                ,[PIMaterialId]
                                ,[PICustomerPN]
                                ,[PICustomer]
                                ,[PIProductName]
                                ,[PIWorkOrder]
                                ,[PIProblemDate]
                                ,[PIProblemSource]
                                ,[PIDefectType]
                                ,[PIDefectCode]
                                ,[PIDefectQty]
                                ,[PIShiftType]
                                ,[PIIsRepeated]
                                ,[PIProblemDesc]
                                ,[PIPicture1]
                                ,[PIPicture2]
                                ,[PIPicture3]
                                ,[PIPicture4]
                                ,[PIPicture5]
                                ,[PIPicture6]
                                ,[PIProcessStatus]
                                ,[PIStatus]
                                ,[PISeverity]
                                ,[PIRootCause]
                                ,[PIRootCauseAssignNo]
                                ,[PIRootCauseAssignName]
                                ,[PIActionPlan]
                                ,[PIExtendPorjects]
                                ,[PIExtendComment]
                                ,[PIExtendApproveComment]
                                ,[PIIsValid]
                                ,[PICreateUserNo]
                                ,[PICreateUserName]
                                ,[PICreateTime]
                                ,[PIOperateUserNo]
                                ,[PIOperateUserName]
                                ,[PIOperateTime]
                                ,[PISapPN]
                                ,[PINextProblemDate]
                                ,[PIFinishDate]
                                ,[PIApproveLayeredAudit]
                                ,[PIApproveVerification]
                                ,[PIApproveStandardization]
                                FROM {0} with(NOLOCK) ", problemTable);
            sql.Append(" WHERE 1=1 ");
            //Todo
            if (model.DateForm != null && model.DateForm > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                sql.AppendFormat("AND PICreateTime >='{0}' ", model.DateForm);
            }
            //var aa = DateTime.Parse("0001-01-01 00:00:00");
            if (model.DateTo != null && model.DateTo > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                sql.AppendFormat("And PICreateTime<='{0}' ", model.DateTo);
            }
            if (!string.IsNullOrEmpty(model.Process) && model.Process != "-1")
            {
                sql.AppendFormat("And PIProcess='{0}' ", model.Process);
            }
            //Todo To confirm  which colum
            if (!string.IsNullOrEmpty(model.ProblemSeverity) && model.ProblemSeverity != "-1")
            {
                sql.AppendFormat("And PISeverity={0} ", model.ProblemSeverity);
            }
            //if (!string.IsNullOrEmpty(model.PlantNo) && model.PlantNo != "")
            //{
            //    sql.AppendFormat("AND PIMaterialId LIKE '%{0}%'", model.PlantNo);
            //}
            if (!string.IsNullOrEmpty(model.WorkOrderNo) && model.WorkOrderNo != "")
            {
                sql.AppendFormat("AND PIWorkOrder LIKE '%{0}%' ", model.WorkOrderNo);
            }
            if (!string.IsNullOrEmpty(model.ToolingNo) && model.ToolingNo != "")
            {
                sql.AppendFormat("AND PITool LIKE '%{0}%' ", model.ToolingNo);
            }
            if (!string.IsNullOrEmpty(model.MachineNo) && model.MachineNo != "")
            {
                sql.AppendFormat("AND PIMachine LIKE '%{0}%' ", model.MachineNo);
            }
            if (model.Status > 0)
            {
                sql.AppendFormat("And PIStatus={0} ", model.Status);
            }
            sql.AppendFormat("AND PIIsValid =1 ");
            if (!string.IsNullOrEmpty(model.Repeatable) && model.Repeatable != "-1")
            {
                sql.AppendFormat("AND PIIsRepeated ={0} ", model.Repeatable);
            }

            var ds = ExecuteDataSet(CommandType.Text, sql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<ProblemInfoModel>(dt);
            }

            return list;
        }

        public List<ProblemInfoModel> ProblemSearchResultPage(ProblemSearchModel model, out int totalcount)
        {
            var list = new List<ProblemInfoModel>();
            var selectSql = new StringBuilder();
            var countSql = new StringBuilder();
            var whereSql = new StringBuilder();
            whereSql.Append(" WHERE 1 = 1 ");
            if (model.DateForm != null && model.DateForm > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                whereSql.AppendFormat("AND PICreateTime >='{0}' ", model.DateForm);
            }
            if (model.DateTo != null && model.DateTo > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                whereSql.AppendFormat("And PICreateTime< '{0}' ", Convert.ToDateTime(model.DateTo).AddDays(1));
            }
            if (model.NextProblemDateFrom != null && model.NextProblemDateFrom > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                whereSql.AppendFormat("AND PINextProblemDate >='{0}' ", model.NextProblemDateFrom);
            }
            if (model.NextProblemDateTo != null && model.NextProblemDateTo > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                whereSql.AppendFormat("And NextProblemDateTo< '{0}' ", Convert.ToDateTime(model.NextProblemDateTo).AddDays(1));
            }
            if (!string.IsNullOrEmpty(model.Process) && model.Process != "-1")
            {
                whereSql.AppendFormat("And PIProcess='{0}' ", model.Process);
            }
            if (!string.IsNullOrEmpty(model.ProblemSeverity) && model.ProblemSeverity != "-1")
            {
                whereSql.AppendFormat("And PISeverity={0} ", model.ProblemSeverity);
            }
            if (!string.IsNullOrEmpty(model.WorkOrderNo) && model.WorkOrderNo != "")
            {
                whereSql.AppendFormat("AND PIWorkOrder LIKE '%{0}%' ", model.WorkOrderNo);
            }
            if (!string.IsNullOrEmpty(model.ToolingNo) && model.ToolingNo != "")
            {
                whereSql.AppendFormat("AND PITool LIKE '%{0}%' ", model.ToolingNo);
            }
            if (!string.IsNullOrEmpty(model.MachineNo) && model.MachineNo != "")
            {
                whereSql.AppendFormat("AND PIMachine LIKE '%{0}%' ", model.MachineNo);
            }
            if (model.Status > 0)
            {
                whereSql.AppendFormat("And PIStatus={0} ", model.Status);
            }
            whereSql.AppendFormat("AND PIIsValid =1 ");
            if (!string.IsNullOrEmpty(model.Repeatable) && model.Repeatable != "-1")
            {
                whereSql.AppendFormat("AND PIIsRepeated ={0} ", model.Repeatable);
            }

            if (!string.IsNullOrEmpty(model.SapNo) && model.SapNo != "")
            {
                whereSql.AppendFormat("And PISapPN Like N'%{0}%' ", model.SapNo);
            }
            if (!string.IsNullOrEmpty(model.PartName) && model.PartName != "")
            {
                whereSql.AppendFormat("And PIProductName LIKE N'%{0}%' ", model.ProblemSeverity);
            }
            if (!string.IsNullOrEmpty(model.Customer) && model.Customer != "")
            {
                whereSql.AppendFormat("AND PICustomer LIKE N'%{0}%' ", model.Customer);
            }
            if (!string.IsNullOrEmpty(model.DefectType) && model.DefectType != "-1")
            {
                whereSql.AppendFormat("AND PIDefectType = '{0}' ", model.DefectType);
            }
            if (!string.IsNullOrEmpty(model.Source) && model.Source != "-1")
            {
                whereSql.AppendFormat("AND PIProblemSource = '{0}' ", model.Source);
            }
            selectSql.AppendFormat(string.Format(@"
                SELECT  newTable.*
                FROM (
                    SELECT TOP ( {0} * {1} )
                            ROW_NUMBER() OVER ( ORDER BY Id DESC) RowNum
                                ,[Id]
                                  ,[PIProblemNo]
                                  ,[PIProcess]
                                  ,[PIMachine]
                                  ,[PITool]
                                  ,[PIMaterialId]
                                  ,[PICustomerPN]
                                  ,[PICustomer]
                                  ,[PIProductName]
                                  ,[PIWorkOrder]
                                  ,[PIProblemDate]
                                  ,[PIProblemSource]
                                  ,[PIDefectType]
                                  ,[PIDefectCode]
                                  ,[PIDefectQty]
                                  ,[PIShiftType]
                                  ,[PIIsRepeated]
                                  ,[PIProblemDesc]
                                  ,[PIPicture1]
                                  ,[PIPicture2]
                                  ,[PIPicture3]
                                  ,[PIPicture4]
                                  ,[PIPicture5]
                                  ,[PIPicture6]
                                  ,[PIProcessStatus]
                                  ,[PIStatus]
                                  ,[PISeverity]
                                  ,[PIRootCause]
                                  ,[PIRootCauseAssignNo]
                                  ,[PIRootCauseAssignName]
                                  ,[PIActionPlan]
                                  ,[PIExtendPorjects]
                                  ,[PIExtendComment]
                                  ,[PIExtendApproveComment]
                                  ,[PIIsValid]
                                  ,[PICreateUserNo]
                                  ,[PICreateUserName]
                                  ,[PICreateTime]
                                  ,[PIOperateUserNo]
                                  ,[PIOperateUserName]
                                  ,[PIOperateTime]
                                  ,[PISapPN]
                                  ,[PINextProblemDate]
                                  ,[PIFinishDate]
                                  ,[PIApproveLayeredAudit]
                                  ,[PIApproveVerification]
                                  ,[PIApproveStandardization]
                                FROM {2} with(NOLOCK) {3} ) newTable where newTable.RowNum>(({0}-1)*{1})", model.CurrentPage, model.PageSize, problemTable, whereSql.ToString()));
            countSql.Append(string.Format(@"SELECT COUNT(1) FROM {0} WITH(NOLOCK) {1} ", problemTable, whereSql.ToString()));

            var ds = ExecuteDataSet(CommandType.Text, selectSql.ToString());
            totalcount = ExecuteCount(CommandType.Text, countSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<ProblemInfoModel>(dt);
            }
            return list;
        }

        public bool Update(ProblemInfoModel model)
        {
            if (model.Id == 0)
            {
                return false;
            }
            var upsql = new StringBuilder();
            var paramsql = new StringBuilder();
            var param = new List<SqlParameter>();
            upsql.Append(string.Format("UPDATE {0} SET ", problemTable));

            #region param

            if (!string.IsNullOrEmpty(model.PIProcess))
            {
                paramsql.Append(" [PIProcess] = @PIProcess ,");
                param.Add(new SqlParameter("@PIProcess", model.PIProcess));
            }
            if (!string.IsNullOrEmpty(model.PIMachine))
            {
                paramsql.Append(" [PIMachine] = @PIMachine ,");
                param.Add(new SqlParameter("@PIMachine", model.PIMachine));
            }
            if (!string.IsNullOrEmpty(model.PITool))
            {
                paramsql.Append(" [PITool] = @PITool ,");
                param.Add(new SqlParameter("@PITool", model.PITool));
            }
            if (model.PIMaterialId > 0)
            {
                paramsql.Append(" [PIMaterialId] = @PIMaterialId ,");
                param.Add(new SqlParameter("@PIMaterialId", model.PIMaterialId));
            }
            if (!string.IsNullOrEmpty(model.PICustomerPN))
            {
                paramsql.Append(" [PICustomerPN] = @PICustomerPN ,");
                param.Add(new SqlParameter("@PICustomerPN", model.PICustomerPN));
            }
            if (!string.IsNullOrEmpty(model.PICustomer))
            {
                paramsql.Append(" [PICustomer] = @PICustomer ,");
                param.Add(new SqlParameter("@PICustomer", model.PICustomer));
            }
            if (!string.IsNullOrEmpty(model.PIProductName))
            {
                paramsql.Append(" [PIProductName] = @PIProductName ,");
                param.Add(new SqlParameter("@PIProductName", model.PIProductName));
            }
            if (!string.IsNullOrEmpty(model.PIWorkOrder))
            {
                paramsql.Append(" [PIWorkOrder] = @PIWorkOrder ,");
                param.Add(new SqlParameter("@PIWorkOrder", model.PIWorkOrder));
            }
            if (model.PIProblemDate != null && model.PIProblemDate > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                paramsql.Append(" [PIProblemDate] = @PIProblemDate ,");
                param.Add(new SqlParameter("@PIProblemDate", model.PIProblemDate));
            }
            if (!string.IsNullOrEmpty(model.PIProblemSource))
            {
                paramsql.Append(" [PIProblemSource] = @PIProblemSource ,");
                param.Add(new SqlParameter("@PIProblemSource", model.PIProblemSource));
            }
            if (!string.IsNullOrEmpty(model.PIDefectType))
            {
                paramsql.Append(" [PIDefectType] = @PIDefectType ,");
                param.Add(new SqlParameter("@PIDefectType", model.PIDefectType));
            }
            if (!string.IsNullOrEmpty(model.PIDefectCode))
            {
                paramsql.Append(" [PIDefectCode] = @PIDefectCode ,");
                param.Add(new SqlParameter("@PIDefectCode", model.PIDefectCode));
            }
            if (model.PIDefectQty != null)
            {
                paramsql.Append(" [PIDefectQty] = @PIDefectQty ,");
                param.Add(new SqlParameter("@PIDefectQty", model.PIDefectQty));
            }
            if (!string.IsNullOrEmpty(model.PIShiftType))
            {
                paramsql.Append(" [PIShiftType] = @PIShiftType ,");
                param.Add(new SqlParameter("@PIShiftType", model.PIShiftType));
            }
            if (model.PIIsRepeated != null)
            {
                paramsql.Append(" [PIIsRepeated] = @PIIsRepeated ,");
                param.Add(new SqlParameter("@PIIsRepeated", model.PIIsRepeated));
            }
            if (!string.IsNullOrEmpty(model.PIProblemDesc))
            {
                paramsql.Append(" [PIProblemDesc] = @PIProblemDesc ,");
                param.Add(new SqlParameter("@PIProblemDesc", model.PIProblemDesc));
            }
            if (!string.IsNullOrEmpty(model.PIPicture1))
            {
                paramsql.Append(" [PIPicture1] = @PIPicture1 ,");
                param.Add(new SqlParameter("@PIPicture1", model.PIPicture1));
            }
            if (!string.IsNullOrEmpty(model.PIPicture2))
            {
                paramsql.Append(" [PIPicture2] = @PIPicture2 ,");
                param.Add(new SqlParameter("@PIPicture2", model.PIPicture2));
            }
            if (!string.IsNullOrEmpty(model.PIPicture3))
            {
                paramsql.Append(" [PIPicture3] = @PIPicture3 ,");
                param.Add(new SqlParameter("@PIPicture3", model.PIPicture3));
            }
            if (!string.IsNullOrEmpty(model.PIPicture4))
            {
                paramsql.Append(" [PIPicture4] = @PIPicture4 ,");
                param.Add(new SqlParameter("@PIPicture4", model.PIPicture4));
            }
            if (!string.IsNullOrEmpty(model.PIPicture5))
            {
                paramsql.Append(" [PIPicture5] = @PIPicture5 ,");
                param.Add(new SqlParameter("@PIPicture5", model.PIPicture5));
            }
            if (!string.IsNullOrEmpty(model.PIPicture6))
            {
                paramsql.Append(" [PIPicture6] = @PIPicture6 ,");
                param.Add(new SqlParameter("@PIPicture6", model.PIPicture6));
            }
            if (model.PIProcessStatus != null)
            {
                paramsql.Append(" [PIProcessStatus] = @PIProcessStatus ,");
                param.Add(new SqlParameter("@PIProcessStatus", model.PIProcessStatus));
            }
            if (model.PIStatus != null)
            {
                paramsql.Append(" [PIStatus] = @PIStatus ,");
                param.Add(new SqlParameter("@PIStatus", model.PIStatus));
            }
            if (model.PISeverity > 0)
            {
                paramsql.Append(" [PISeverity] = @PISeverity ,");
                param.Add(new SqlParameter("@PISeverity", model.PISeverity));
            }
            if (!string.IsNullOrEmpty(model.PIRootCause))
            {
                paramsql.Append(" [PIRootCause] = @PIRootCause ,");
                param.Add(new SqlParameter("@PIRootCause", model.PIRootCause));
            }
            if (!string.IsNullOrEmpty(model.PIRootCauseAssignNo))
            {
                paramsql.Append(" [PIRootCauseAssignNo] = @PIRootCauseAssignNo ,");
                param.Add(new SqlParameter("@PIRootCauseAssignNo", model.PIRootCauseAssignNo));
            }
            if (!string.IsNullOrEmpty(model.PIRootCauseAssignName))
            {
                paramsql.Append(" [PIRootCauseAssignName] = @PIRootCauseAssignName ,");
                param.Add(new SqlParameter("@PIRootCauseAssignName", model.PIRootCauseAssignName));
            }
            if (!string.IsNullOrEmpty(model.PIActionPlan))
            {
                paramsql.Append(" [PIActionPlan] = @PIActionPlan ,");
                param.Add(new SqlParameter("@PIActionPlan", model.PIActionPlan));
            }
            if (model.PIExtendPorjects != null)
            {
                paramsql.Append(" [PIExtendPorjects] = @PIExtendPorjects ,");
                param.Add(new SqlParameter("@PIExtendPorjects", model.PIExtendPorjects));
            }
            if (!string.IsNullOrEmpty(model.PIExtendComment))
            {
                paramsql.Append(" [PIExtendComment] = @PIExtendComment ,");
                param.Add(new SqlParameter("@PIExtendComment", model.PIExtendComment));
            }
            if (!string.IsNullOrEmpty(model.PIExtendApproveComment))
            {
                paramsql.Append(" [PIExtendApproveComment] = @PIExtendApproveComment ,");
                param.Add(new SqlParameter("@PIExtendApproveComment", model.PIExtendApproveComment));
            }
            if (model.PIIsValid != null)
            {
                paramsql.Append(" [PIIsValid] = @PIIsValid ,");
                param.Add(new SqlParameter("@PIIsValid", model.PIIsValid));
            }
            if (!string.IsNullOrEmpty(model.PICreateUserNo))
            {
                paramsql.Append(" [PICreateUserNo] = @PICreateUserNo ,");
                param.Add(new SqlParameter("@PICreateUserNo", model.PICreateUserNo));
            }
            if (!string.IsNullOrEmpty(model.PICreateUserName))
            {
                paramsql.Append(" [PICreateUserName] = @PICreateUserName ,");
                param.Add(new SqlParameter("@PICreateUserName", model.PICreateUserName));
            }
            if (model.PICreateTime != null)
            {
                paramsql.Append(" [PICreateTime] = @PICreateTime ,");
                param.Add(new SqlParameter("@PICreateTime", model.PICreateTime));
            }
            if (!string.IsNullOrEmpty(model.PIOperateUserNo))
            {
                paramsql.Append(" [PIOperateUserNo] = @PIOperateUserNo ,");
                param.Add(new SqlParameter("@PIOperateUserNo", model.PIOperateUserNo));
            }
            if (!string.IsNullOrEmpty(model.PIOperateUserName))
            {
                paramsql.Append(" [PIOperateUserName] = @PIOperateUserName ,");
                param.Add(new SqlParameter("@PIOperateUserName", model.PIOperateUserName));
            }
            if (model.PIOperateTime != null)
            {
                paramsql.Append(" [PIOperateTime] = @PIOperateTime ,");
                param.Add(new SqlParameter("@PIOperateTime", model.PIOperateTime));
            }
            if (!string.IsNullOrEmpty(model.PISapPN))
            {
                paramsql.Append(" [PISapPN] = @PISapPN ,");
                param.Add(new SqlParameter("@PISapPN", model.PISapPN));
            }
            if (model.PINextProblemDate != null && model.PINextProblemDate > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                paramsql.Append(" [PINextProblemDate] = @PINextProblemDate ,");
                param.Add(new SqlParameter("@PINextProblemDate", model.PINextProblemDate));
            }
            if (model.PIFinishDate != null && model.PIFinishDate > Convert.ToDateTime("0001-01-01 00:00:00"))
            {
                paramsql.Append(" [PIFinishDate] = @PIFinishDate ,");
                param.Add(new SqlParameter("@PIFinishDate", model.PIFinishDate));
            }
            if (model.PIApproveLayeredAudit != null)
            {
                paramsql.Append(" [PIApproveLayeredAudit] = @PIApproveLayeredAudit ,");
                param.Add(new SqlParameter("@PIApproveLayeredAudit", model.PIApproveLayeredAudit));
            }
            if (model.PIApproveVerification != null)
            {
                paramsql.Append(" [PIApproveVerification] = @PIApproveVerification ,");
                param.Add(new SqlParameter("@PIApproveVerification", model.PIApproveVerification));
            }
            if (model.PIApproveStandardization != null)
            {
                paramsql.Append(" [PIApproveStandardization] = @PIApproveStandardization ,");
                param.Add(new SqlParameter("@PIApproveStandardization", model.PIApproveStandardization));
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
        /// 描述：
        /// </summary>
        /// <param name="proId"></param>
        /// <returns></returns>
        public ProblemInfoModel GeProblemById(int proId)
        {
            var pro = new ProblemInfoModel();
            SqlParameter[] para = {
                new SqlParameter("@Id", proId)
            };

            var sql = @"SELECT TOP 1 [Id]
                            ,[PIProblemNo]
                            ,[PIProcess]
                            ,[PIMachine]
                            ,[PITool]
                            ,[PIMaterialId]
                            ,[PICustomerPN]
                            ,[PICustomer]
                            ,[PIProductName]
                            ,[PIWorkOrder]
                            ,[PIProblemDate]
                            ,[PIProblemSource]
                            ,[PIDefectType]
                            ,[PIDefectCode]
                            ,[PIDefectQty]
                            ,[PIShiftType]
                            ,[PIIsRepeated]
                            ,[PIProblemDesc]
                            ,[PIPicture1]
                            ,[PIPicture2]
                            ,[PIPicture3]
                            ,[PIPicture4]
                            ,[PIPicture5]
                            ,[PIPicture6]
                            ,[PIProcessStatus]
                            ,[PIStatus]
                            ,[PISeverity]
                            ,[PIRootCause]
                            ,[PIRootCauseAssignNo]
                            ,[PIRootCauseAssignName]
                            ,[PIActionPlan]
                            ,[PIExtendPorjects]
                            ,[PIExtendComment]
                            ,[PIExtendApproveComment]
                            ,[PIIsValid]
                            ,[PICreateUserNo]
                            ,[PICreateUserName]
                            ,[PICreateTime]
                            ,[PIOperateUserNo]
                            ,[PIOperateUserName]
                            ,[PIOperateTime]
                            ,[PISapPN]
                            ,[PINextProblemDate]
                            ,[PIFinishDate]
                            ,[PIApproveLayeredAudit]
                            ,[PIApproveVerification]
                            ,[PIApproveStandardization]
                        FROM " + problemTable + " with(NOLOCK) WHERE Id=@Id";
            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                pro = DataConvertHelper.DataTableToList<ProblemInfoModel>(dt)[0];
            }
            else
            {
                return null;
            }
            return pro;
        }

        public List<ProblemInfoModel> GetMaxProblemNoCurrentDay()
        {
            var list = new List<ProblemInfoModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@"SELECT  Max(DISTINCT PIProblemNo) AS PIProblemNo 
                            FROM {0} WITH (NOLOCK)", problemTable);
            sql.AppendFormat(@"WHERE PICreateTime>'{0}' AND PICreateTime<'{1}' ", DateTime.Now.ToShortDateString(), DateTime.Now.AddDays(1).ToShortDateString());

            var ds = ExecuteDataSet(CommandType.Text, sql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                var dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<ProblemInfoModel>(dt);
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public List<ProblemInfoModel> GetSelectProblemNo(string Key)
        {
            var list = new List<ProblemInfoModel>();

            string sql = string.Format(@"SELECT TOP 5 Id,PIProblemNo FROM {0} WITH(NOLOCK) " +
                " WHERE PIProblemNo like '%{1}%' AND PIIsValid=1", problemTable, Key);

            var ds = ExecuteDataSet(CommandType.Text, sql, null);
            if (ds != null && ds.Tables.Count > 0)
            {
                var dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<ProblemInfoModel>(dt);
            }
            return list;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public List<ProblemInfoModel> GetMyToDoProblemList(string jobnum)
        {
            var list = new List<ProblemInfoModel>();

            string sql = string.Format(@"SELECT TOP 5 Id,PIProblemNo FROM {0} WITH(NOLOCK) " +
                " WHERE PIProblemNo like '%{1}%' AND PIIsValid=1", problemTable, jobnum);

            var ds = ExecuteDataSet(CommandType.Text, sql, null);
            if (ds != null && ds.Tables.Count > 0)
            {
                var dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<ProblemInfoModel>(dt);
            }
            return list;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="proId"></param>
        /// <returns></returns>
        public ProblemUnionInfo GetProblemUnionInfo(long proId)
        {
            var problemUnionInfo = new ProblemUnionInfo();
            SqlParameter[] para = {
                new SqlParameter("@Id", proId)
            };
            var sql = new StringBuilder();

            #region probleminfo
            sql.AppendFormat(@" SELECT TOP 1 [Id]
                                ,[PIProblemNo]
                                ,[PIProcess]
                                ,[PIMachine]
                                ,[PITool]
                                ,[PIMaterialId]
                                ,[PICustomerPN]
                                ,[PICustomer]
                                ,[PIProductName]
                                ,[PIWorkOrder]
                                ,[PIProblemDate]
                                ,[PIProblemSource]
                                ,[PIDefectType]
                                ,[PIDefectCode]
                                ,[PIDefectQty]
                                ,[PIShiftType]
                                ,[PIIsRepeated]
                                ,[PIProblemDesc]
                                ,[PIPicture1]
                                ,[PIPicture2]
                                ,[PIPicture3]
                                ,[PIPicture4]
                                ,[PIPicture5]
                                ,[PIPicture6]
                                ,[PIProcessStatus]
                                ,[PIStatus]
                                ,[PISeverity]
                                ,[PIRootCause]
                                ,[PIRootCauseAssignNo]
                                ,[PIRootCauseAssignName]
                                ,[PIActionPlan]
                                ,[PIExtendPorjects]
                                ,[PIExtendComment]
                                ,[PIExtendApproveComment]
                                ,[PIIsValid]
                                ,[PICreateUserNo]
                                ,[PICreateUserName]
                                ,[PICreateTime]
                                ,[PIOperateUserNo]
                                ,[PIOperateUserName]
                                ,[PIOperateTime]
                                ,[PISapPN]
                                ,[PINextProblemDate]
                                ,[PIFinishDate]
                                ,[PIApproveLayeredAudit]
                                ,[PIApproveVerification]
                                ,[PIApproveStandardization]
                        FROM {0} with(NOLOCK) WHERE Id=@Id; ", problemTable);
            #endregion

            #region problemsolvingteam
            sql.AppendFormat(@"SELECT [Id]
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
                        FROM {0} with(NOLOCK) WHERE PSProblemId=@Id AND PSIsValid=1; ", problemSolvingteamTable);
            #endregion

            #region problemqualityalert

            sql.AppendFormat(@"SELECT [Id]
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
                        FROM {0} with(NOLOCK) WHERE PQProblemId=@Id AND PQIsValid=1; ", problemQualityalertTable);

            #endregion

            #region problemsortingactivity
            sql.AppendFormat(@" SELECT [Id]
                          ,[PSAProblemId]
                          ,[PSAValueStreamNo]
                          ,[PSAValueStream]
                          ,[PSADefectQty]
                          ,[PSASortedQty]
                          ,[PSADeadLine]
                          ,[PSAIsValid]
                          ,[PSACreateUserNo]
                          ,[PSACreateUserName]
                          ,[PSACreateTime]
                          ,[PSAOperateUserNo]
                          ,[PSAOperateUserName]
                          ,[PSAOperateTime]
                        FROM {0} with(NOLOCK) WHERE PSAProblemId=@Id AND PSAIsValid=1; ", problemSortingactivityTable);
            #endregion

            #region problemactioncontainment
            sql.AppendFormat(@"SELECT [Id]
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
                                  FROM {0} with(NOLOCK) WHERE PACProblemId=@Id AND PACIsValid=1; ", problemActioncontainmentTable);
            #endregion

            #region problemactionwhyanalysis
            sql.AppendFormat(@"SELECT [Id]
                          ,[PAWWhyForm]
                          ,[PAWWhyQuestionChain]
                          ,[PAWWhy1]
                          ,[PAWWhy2]
                          ,[PAWWhy3]
                          ,[PAWWhy4]
                          ,[PAWWhy5]
                          ,[PAWIsValid]
                          ,[PAWCreateUserNo]
                          ,[PAWCreateUserName]
                          ,[PAWCreateTime]
                          ,[PAWOperateUserNo]
                          ,[PAWOperateUserName]
                          ,[PAWOperateTime]
                          ,[PAWProblemId]
                      FROM {0} with(NOLOCK) WHERE PAWProblemId=@Id AND PAWIsValid=1; ", problemActionwhyanalysisTable);
            #endregion

            #region problemactioncorrective
            sql.AppendFormat(@"SELECT [Id]
                          ,[PACWhat]
                          ,[PACWhoNo]
                          ,[PACWho]
                          ,[PACPlanDate]
                          ,[PACActualDate]
                          ,[PACWhere]
                          ,[PACAttachment]
                          ,[PACAttachmentUrl]
                          ,[PACStatus]
                          ,[PACComment]
                          ,[PACIsValid]
                          ,[PACCreateUserNo]
                          ,[PACCreateUserName]
                          ,[PACCreateTime]
                          ,[PACOperateUserNo]
                          ,[PACOperateUserName]
                          ,[PACOperateTime]
                          ,[PACProblemId]
                      FROM {0} with(NOLOCK) WHERE PACProblemId=@Id AND PACIsValid=1; ", problemActioncorrectiveTable);
            #endregion

            #region problemactionpreventive
            sql.AppendFormat(@"SELECT [Id]
                          ,[PAPWhat]
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
                          ,[PAPProblemId]
                      FROM {0} with(NOLOCK) WHERE PAPProblemId=@Id AND PAPIsValid=1; ", problemActionpreventiveTable);
            #endregion

            #region problemlayeredaudit
            sql.AppendFormat(@"SELECT [Id]
                          ,[PLWhat]
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
                          ,[PLProblemId]
                      FROM {0} with(NOLOCK) WHERE PLProblemId=@Id AND PLIsValid=1; ", problemLayeredauditTable);
            #endregion

            #region problemverification
            sql.AppendFormat(@"SELECT [Id]
                          ,[PVWhat]
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
                          ,[PVProblemId]
                      FROM {0} with(NOLOCK) WHERE PVProblemId=@Id AND PVIsValid=1; ", problemVerificationTable);
            #endregion

            #region problemstandardization
            sql.AppendFormat(@"SELECT [Id]
                          ,[PSItemName]
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
                          ,[PSProblemId]
                      FROM {0} with(NOLOCK) WHERE PSProblemId=@Id AND PSIsValid=1; ", problemStandardizationTable);
            #endregion

            #region problemactionfactoranalysis
            sql.AppendFormat(@"SELECT [Id]
                          ,[PAFType]
                          ,[PAFPossibleCause]
                          ,[PAFWhat]
                          ,[PAFWhoNo]
                          ,[PAFWho]
                          ,[PAFValidatedDate]
                          ,[PAFPotentialCause]
                          ,[PAFIsValid]
                          ,[PAFCreateUserNo]
                          ,[PAFCreateUserName]
                          ,[PAFCreateTime]
                          ,[PAFOperateUserNo]
                          ,[PAFOperateUserName]
                          ,[PAFOperateTime]
                          ,[PAFProblemId]
                      FROM {0} with(NOLOCK) WHERE PAFProblemId=@Id AND PAFIsValid=1; ", problemActionfactoranalysisTable);
            #endregion

            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                var probleminfoDt = ds.Tables[0];
                if (probleminfoDt.Rows.Count > 0)
                {
                    var probleminfo = DataConvertHelper.DataTableToList<ProblemInfoModel>(probleminfoDt)[0];
                    problemUnionInfo.problem = probleminfo;

                    var problemsolvingteamDt = ds.Tables[1];
                    var problemsolvingteam = DataConvertHelper.DataTableToList<ProblemSolvingTeamModel>(problemsolvingteamDt);
                    problemUnionInfo.solvingteam = problemsolvingteam;

                    var problemqualityalertDt = ds.Tables[2];
                    var problemqualityalert = DataConvertHelper.DataTableToList<ProblemQualityAlertModel>(problemqualityalertDt);
                    problemUnionInfo.qualityalert = problemqualityalert;

                    var problemsortingactivityDt = ds.Tables[3];
                    var problemsortingactivity = DataConvertHelper.DataTableToList<ProblemSortingActivityModel>(problemsortingactivityDt);
                    problemUnionInfo.sortingactivity = problemsortingactivity;

                    var problemactioncontainmentDt = ds.Tables[4];
                    var problemactioncontainment = DataConvertHelper.DataTableToList<ProblemActionContainmentModel>(problemactioncontainmentDt);
                    problemUnionInfo.actioncontainment = problemactioncontainment;

                    var problemactionwhyanalysisDt = ds.Tables[5];
                    var problemactionwhyanalysis = DataConvertHelper.DataTableToList<ProblemActionWhyanalysisModel>(problemactionwhyanalysisDt);
                    problemUnionInfo.actionwhyanalysisi = problemactionwhyanalysis;

                    var problemactioncorrectiveDt = ds.Tables[6];
                    var problemactioncorrective = DataConvertHelper.DataTableToList<ProblemActionCorrectiveModel>(problemactioncorrectiveDt);
                    problemUnionInfo.actioncorrective = problemactioncorrective;

                    var problemactionpreventiveDt = ds.Tables[7];
                    var problemactionpreventive = DataConvertHelper.DataTableToList<ProblemActionPreventiveModel>(problemactionpreventiveDt);
                    problemUnionInfo.actionpreventive = problemactionpreventive;

                    var problemlayeredauditDt = ds.Tables[8];
                    var problemlayeredaudit = DataConvertHelper.DataTableToList<ProblemLayeredAuditModel>(problemlayeredauditDt);
                    problemUnionInfo.layeredaudit = problemlayeredaudit;

                    var problemverificationDt = ds.Tables[9];
                    var problemverification = DataConvertHelper.DataTableToList<ProblemVerificationModel>(problemverificationDt);
                    problemUnionInfo.verification = problemverification;

                    var problemstandardizationDt = ds.Tables[10];
                    var problemstandardization = DataConvertHelper.DataTableToList<ProblemStandardizationModel>(problemstandardizationDt);
                    problemUnionInfo.standardization = problemstandardization;

                    var problemactionfactoranalysisDt = ds.Tables[11];
                    var problemactionfactoranalysis = DataConvertHelper.DataTableToList<ProblemActionFactorAnalysisModel>(problemactionfactoranalysisDt);
                    problemUnionInfo.actionfactoranalysis = problemactionfactoranalysis;
                }
            }
            return problemUnionInfo;
        }
    }
}
