using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Problem
{
     public class ProblemLayeredAuditModel
    {
        public long Id { get; set; }
        public string PLWhat { get; set; }
        public string PLWhoNo { get; set; }
        public string PLWho { get; set; }
        public DateTime? PLPlanDate { get; set; }
        public string PLPlanDateDesc
        {
            get
            {
                var date = PLPlanDate.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm");
                return date == "1900-01-01 00:00" ? string.Empty : date;
            }
        }
        public DateTime? PLActualDate { get; set; }
        public string PLActualDateDesc
        {
            get
            {
                var date = PLActualDate.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm");
                return date == "1900-01-01 00:00" ? string.Empty : date;
            }
        }
        public string PLWhere { get; set; }
        public string PLAttachment { get; set; }
        public string PLAttachmentUrl { get; set; }
        public string PLAttachmentDownloadUrl { get; set; }
        public string PLStatus { get; set; }
        public string PLComment { get; set; }
        public int? PLIsValid { get; set; }
        public string PLCreateUserNo { get; set; }
        public string PLCreateUserName { get; set; }
        public DateTime? PLCreateTime { get; set; }
        public string PLOperateUserNo { get; set; }
        public string PLOperateUserName { get; set; }
        public DateTime? PLOperateTime { get; set; }
        public long PLProblemId { get; set; }
        public string PLProblemNo { get; set; }
    }
}
