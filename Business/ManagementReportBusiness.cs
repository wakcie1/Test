using DataAccess;
using Model.Problem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class ManagementReportBusiness
    {
        private static ManagementReportDAL _managementDal = new ManagementReportDAL();
        private static DateTime GetTimeStart(DateTime nowTime)
        {
            return nowTime.AddYears(-1).AddDays((-1) * nowTime.Day+1);
        }

        private static DateTime GetTimeEnd(DateTime nowTime)
        {
            return nowTime.AddMonths(1).AddDays((-1) * nowTime.Day + 1);
        }

        public static List<ProblemInfoModel> GetProblemByStartAndEnd(DateTime time)
        {
            var startTime = GetTimeStart(time);
            var endTime = GetTimeEnd(time);
            return _managementDal.GetProblemByStartAndEnd(startTime, endTime);
        }
    }
}
