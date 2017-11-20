using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Common.Enum
{
    /// <summary>
    /// 问题流程
    /// </summary>
    public enum ProblemProcessStatusEnum
    {
        /// <summary>
        /// 草稿
        /// </summary>
        [Description("草稿")]
        DraftP0 = 0,

        /// <summary>
        /// 问题基本信息
        /// </summary>
        [Description("基本信息")]
        InfoP1 = 100,

        /// <summary>
        /// 问题基本信息审核通过
        /// </summary>
        [Description("问题基本信息审核通过")]
        InfoP1Ap = 101,

        /// <summary>
        /// 处理小组
        /// </summary>
        [Description("处理小组")]
        SolvingTeamP2 = 200,

        /// <summary>
        /// 处理小组信息审核通过
        /// </summary>
        [Description("处理小组审核通过")]
        SolvingTeamP2Ap = 201,

        /// <summary>
        /// 质量问题
        /// </summary>
        [Description("质量问题")]
        QualityAlertP3 = 300,

        /// <summary>
        /// 质量问题审核通过
        /// </summary>
        [Description("质量问题审核通过")]
        QualityAlertP3Ap = 301,

        /// <summary>
        /// 活动整理
        /// </summary>
        [Description("活动整理")]
        SortingActivityP4 = 400,

        /// <summary>
        /// 活动整理审核通过
        /// </summary>
        [Description("活动整理审核通过")]
        SortingActivityP4Ap = 401,

        /// <summary>
        /// 遏制措施
        /// </summary>
        [Description("遏制措施")]
        ContainmentActionP5 = 500,

        /// <summary>
        /// 遏制措施审核通过
        /// </summary>
        [Description("遏制措施审核通过")]
        ContainmentActionP5Ap = 501,

        /// <summary>
        /// 遏制措施二级审核通过
        /// </summary>
        [Description("遏制措施二级审核通过")]
        ContainmentActionP5Ap2 = 502,

        /// <summary>
        /// 工厂分析
        /// </summary>
        [Description("工厂分析")]
        FactorAnalysisP6 = 600,

        /// <summary>
        /// 工厂分析审核通过
        /// </summary>
        [Description("工厂分析审核通过")]
        FactorAnalysisP6Ap = 601,

        /// <summary>
        /// 工厂分析
        /// </summary>
        [Description("工厂分析")]
        WhyAnalysisP6 = 650,

        /// <summary>
        /// 工厂分析审核通过
        /// </summary>
        [Description("工厂分析审核通过")]
        WhyAnalysisP6Ap = 651,

        /// <summary>
        /// 处理错误
        /// </summary>
        [Description("处理错误")]
        CorrectiveActionP7 = 700,

        /// <summary>
        /// 处理错误审核通过
        /// </summary>
        [Description("处理错误审核通过")]
        CorrectiveActionP7Ap = 701,

        /// <summary>
        /// 预防措施
        /// </summary>
        [Description("预防措施")]
        PreventiveMeasuresP8 = 800,

        /// <summary>
        /// 预防措施审核通过
        /// </summary>
        [Description("预防措施审核通过")]
        PreventiveMeasuresP8Ap = 801,

        /// <summary>
        /// 分层审批
        /// </summary>
        [Description("分层审批")]
        LayeredAuditP9 = 900,

        /// <summary>
        /// 分层审批审核通过
        /// </summary>
        [Description("分层审批审核通过")]
        LayeredAuditP9Ap = 901,

        /// <summary>
        /// 验证
        /// </summary>
        [Description("验证")]
        VerificationP10 = 1000,

        /// <summary>
        /// 验证审核通过
        /// </summary>
        [Description("验证审核通过")]
        VerificationP10Ap = 1001,

        /// <summary>
        /// 标准化
        /// </summary>
        [Description("标准化")]
        StandardizationP11 = 1100,

        /// <summary>
        /// 标准化审核通过
        /// </summary>
        [Description("标准化审核通过")]
        StandardizationP11Ap = 1101,

        /// <summary>
        /// 审核
        /// </summary>
        [Description("审核")]
        AuditP12 = 1200,

        /// <summary>
        /// 批准
        /// </summary>
        [Description("批准")]
        Authorized = 1300,
    }

    /// <summary>
    /// 问题严重程度
    /// </summary>
    public enum ProblemSeverityEnum
    {
        /// <summary>
        /// 普通
        /// </summary>
        [Description("General")]
        General = 1,

        /// <summary>
        /// 严重
        /// </summary>
        [Description("Sevirity")]
        Sevirity = 2,
    }


    /// <summary>
    /// 问题状态
    /// </summary>
    public enum ProblemStatusEnum
    {
        /// <summary>
        /// 开启
        /// </summary>
        [Description("Open")]
        Open = 1,

        /// <summary>
        /// 关闭
        /// </summary>
        [Description("Close")]
        Close = 2,


    }
}
