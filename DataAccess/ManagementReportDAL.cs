using Common;
using Common.Costant;
using Model.Problem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class ManagementReportDAL:NewSqlHelper
    {
        private const string tableName = "T_PROBLEM_INFO";

        public List<ProblemInfoModel> GetProblemByStartAndEnd(DateTime startTime, DateTime endTime)
        {
            var list = new List<ProblemInfoModel>();
            var sql = new StringBuilder();
            sql.AppendFormat(@"SELECT [Id]
                        ,[PIProblemDate]
                        ,[PIProcessStatus]
                        ,[PIStatus]
                            FROM {0} WITH(NOLOCK)
                            where [PIIsValid] = 1 
                            AND [PIProblemDate] >= '{1}'
                            AND  [PIProblemDate] < '{2}' ", tableName, startTime.ToString(CommonConstant.DateTimeFormatDay), endTime.ToString(CommonConstant.DateTimeFormatDay));
            var ds = ExecuteDataSet(CommandType.Text, sql.ToString());
            if (ds != null && ds.Tables.Count >0)
            {
                var dt = new DataTable();
                dt = ds.Tables[0];
                list= DataConvertHelper.DataTableToList<ProblemInfoModel>(dt);
            }

            return list;
        }
    }
}
