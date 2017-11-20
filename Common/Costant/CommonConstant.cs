﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Costant
{
    public class CommonConstant
    {
        /// <summary>
        /// 默认时间
        /// </summary>
        public const string DefaultDateTime = "1900-01-01";

        /// <summary>
        /// 默认时间
        /// </summary>
        public const string DefaultDateTimeExtend = "1900/01/01";

        /// <summary>
        /// 默认时间时分
        /// </summary>
        public const string DefaultDateTimeMinutes = "1900-01-01 00:00";

        /// <summary>
        /// 默认时间时分
        /// </summary>
        public const string DefaultDateTimeSeconds = "1900-01-01 00:00:00";

        /// <summary>
        /// 系统默认时间
        /// </summary>
        public const string DefaultDateTimeSystem = "0001-01-01";

        /// <summary>
        /// 时间格式 2012-09-01
        /// </summary>
        public const string DateTimeFormatDay = "yyyy-MM-dd";

        /// <summary>
        /// 时间格式 2012/09/01
        /// </summary>
        public const string DateTimeFormatDayExtend = "yyyy/MM/dd";

        /// <summary>
        /// 时间格式 20120901
        /// </summary>
        public const string DateTimeFormatDayOnly = "yyyyMMdd";

        /// <summary>
        /// 时间格式 2016-10-14 10:53
        /// </summary>
        public const string DateTimeFormatDayMinutes = "yyyy-MM-dd HH:mm";

        /// <summary>
        /// 时间格式 2016/10/14 10:53:22
        /// </summary>
        public const string DateTimeFormatDayMinutesExtend = "yyyy/MM/dd HH:mm";

        /// <summary>
        /// 时间格式 20161014105322
        /// </summary>
        public const string DateTimeFormatDayMinutesOnly = "yyyyMMddHHmm";

        /// <summary>
        /// 时间格式 2016-10-14 10:53:22
        /// </summary>
        public const string DateTimeFormatDaySeconds = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 时间格式 2016/10/14 10:53:22
        /// </summary>
        public const string DateTimeFormatDaySecondsExtend = "yyyy/MM/dd HH:mm:ss";

        /// <summary>
        /// 时间格式 20161014105322
        /// </summary>
        public const string DateTimeFormatDaySecondsOnly = "yyyyMMddHHmmss";

        /// <summary>
        /// 时间格式 2016年10月14日
        /// </summary>
        public const string DateTimeFormatDayOnlyChinese = "yyyy年MM月dd日";

        /// <summary>
        /// 时间格式 2016年10月14日 2时22分
        /// </summary>
        public const string DateTimeFormatDayMinOnlyChinese = "yyyy年MM月dd日 HH时mm分";

        /// <summary>
        /// 时间格式 2016年10月14日 2时22分
        /// </summary>
        public const string DateTimeFormatDayMinColonOnlyChinese = "yyyy年MM月dd日 HH:mm";

        /// <summary>
        /// 时间格式 2016年10月14日
        /// </summary>
        public const string DateTimeFormatHourMinOnlyChinese = "HH时mm分";
    }

    public class CategoryConstant
    {
        /// <summary>
        /// PROBLEM_PROCESS
        /// </summary>
        public const string Process = "ProblemProcess";
        ///// <summary>
        ///// ProblemSeverity
        ///// </summary>
        //public const string ProblemSeverity = "PROBLEM_SEVERITY";

        /// <summary>
        /// DefectCode
        /// </summary>
        //public const string DefectCode = "PROBLEM_DEFECTCODE";
        /// <summary>
        /// Source
        /// </summary>
        public const string Source = "ProblemSource";
        /// <summary>
        ///  RequireType
        /// </summary>
        public const string RequireType = "SuggestRequiretype";
        /// <summary>
        /// Phrase
        /// </summary>
        public const string Phrase = "SuggestPhrase";

    }
}
