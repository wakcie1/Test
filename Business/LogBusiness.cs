using Common;
using Common.Enum;
using DataAccess;
using Model.TableModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class LogBusiness
    {
        private static LogDAL _userDal = new LogDAL();
        public static void InsertLog(LogModel model)
        {
            try
            {
                _userDal.InsertModel(model);
            }
            catch
            {
                //TODO
            }
        }

        /// <summary>
        /// 插入问题跟进日志
        /// </summary>
        /// <param name="code">日志关键字</param>
        /// <param name="message">日志信息</param>
        /// <param name="filter">过滤字段</param>
        public static void Problemfollow(string code, string message, string filter1, string filter2)
        {
            var loginUser = LoginHelper.GetUser();
            var model = new LogModel
            {
                BLCode = code ?? string.Empty,
                BLLogDesc = message ?? string.Empty,
                BLLogType = LogTypeEnum.ProblemLog.GetHashCode(),
                BLFilterValue1 = filter1 ?? string.Empty,
                BLFilterValue2 = filter2 ?? string.Empty,
                BLFilterId1 = 0,
                BLFilterId2 = 0,
                BLIsValid = EnabledEnum.Enabled.GetHashCode(),
                BLCreateUserNo = loginUser.JobNum,
                BLCreateUserName = loginUser.UserName,
                BLCreateTime = DateTime.Now
            };
            InsertLog(model);
        }

        public static List<LogModel> LogSearch(string code)
        {
            return _userDal.SearchProblemFollew(code);
        }
    }
}
