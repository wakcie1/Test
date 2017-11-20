using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.TableModel
{
    public class GroupModel
    {
        /// <summary>
        /// 资源Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 资源编码
        /// </summary>
        public string BGCode { get; set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        public string BGName { get; set; }

        /// <summary>
        /// 有效性（0.无效 1.有效）
        /// </summary>
        public int BGIsValid { get; set; }
    }
}
