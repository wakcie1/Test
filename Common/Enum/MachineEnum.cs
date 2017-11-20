using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Common.Enum
{
    public enum MachineEnum
    {
        /// <summary>
        /// 注塑机
        /// </summary>
        [Description("注塑机")]
        Machine1 = 1,

        /// <summary>
        /// 工具
        /// </summary>
        [Description("工具")]
        Machine2 = 2,

        /// <summary>
        /// 工具
        /// </summary>
        [Description("工具")]
        Machine3 = 3,
    }


    public enum WorkOrderType
    {
        /// <summary>
        /// Molding
        /// </summary>
        [Description("Molding")]
        Molding = 1,

        /// <summary>
        /// Ass`y line
        /// </summary>
        [Description("Assy line")]
        AssYLine = 2,

        /// <summary>
        /// 3D blow molding
        /// </summary>
        [Description("3D blow molding")]
        BlowMolding = 3,

        /// <summary>
        /// Foaming
        /// </summary>
        [Description("Foaming")]
        Foaming = 4,
    }
}
