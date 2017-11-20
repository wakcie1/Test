using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Common.Enum
{
    public enum EnabledEnum
    {
        /// <summary>
        /// 是
        /// </summary>
        [Description("有效")]
        Enabled = 1,

        /// <summary>
        /// 否
        /// </summary>
        [Description("无效")]
        UnEnabled = 0,
    }

    /// <summary>
    /// 描述：数据库种类
    /// </summary>
    public enum DBType
    {
        [Description("默认数据库")]
        Default = 0,
        [Description("新数据库")]
        ReNew = 1,
    }

    /// <summary>
    /// 描述：性别
    /// </summary>
    public enum Sex
    {
        [Description("Other")]
        Other = 0,

        [Description("Male")]
        Male = 1,

        [Description("Female")]
        Female = 2,
    }

    /// <summary>
    /// 日志分类
    /// </summary>
    public enum LogTypeEnum
    {
        [Description("问题跟进")]
        ProblemLog = 1,
    }

    public enum YesOrNoEnum
    {
        [Description("YES")]
        YES = 1,
        [Description("NO")]
        NO = 0
    }
}
