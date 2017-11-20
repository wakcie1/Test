using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Problem
{
    public class ProblemSortingActivityModel
    {
        public long Id { get; set; }
        public long PSAProblemId { get; set; }
        public string PSAProblemNo { get; set; }
        public string PSAValueStream { get; set; }
        public int PSAValueStreamNo { get; set; }
        public int? PSADefectQty { get; set; }
        public int? PSASortedQty { get; set; }
        public DateTime? PSADeadLine { get; set; }
        public string PSADeadLineDesc
        {
            get
            {
                var date = PSADeadLine.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm");
                return date == "1900-01-01 00:00" ? string.Empty : date;
            }
        }
        public int? PSAIsValid { get; set; }
        public string PSACreateUserNo { get; set; }
        public string PSACreateUserName { get; set; }
        public DateTime? PSACreateTime { get; set; }
        public string PSAOperateUserNo { get; set; }
        public string PSAOperateUserName { get; set; }
        public DateTime? PSAOperateTime { get; set; }
    }
}
