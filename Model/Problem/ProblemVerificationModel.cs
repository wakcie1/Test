using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Problem
{
    public class ProblemVerificationModel
    {
        public long Id { get; set; }
        public string PVWhat { get; set; }
        public string PVWhoNo { get; set; }
        public string PVWho { get; set; } 
        public DateTime? PVPlanDate { get; set; }
        public string PVPlanDateDesc
        {
            get
            {
                var date = PVPlanDate.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm");
                return date == "1900-01-01 00:00" ? string.Empty : date;
            }
        }
        public DateTime? PVActualDate { get; set; }
        public string PVActualDateDesc
        {
            get
            {
                var date = PVActualDate.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm");
                return date == "1900-01-01 00:00" ? string.Empty : date;
            }
        }
        public string PVWhere { get; set; }
        public string PVAttachment { get; set; }
        public string PVAttachmentUrl { get; set; }
        public string PVAttachmentDownloadUrl { get; set; }
        public string PVStatus { get; set; } 
        public string PVComment { get; set; }
        public int? PVIsValid { get; set; }
        public string PVCreateUserNo { get; set; }
        public string PVCreateUserName { get; set; }
        public DateTime PVCreateTime { get; set; }
        public string PVOperateUserNo { get; set; }
        public string PVOperateUserName { get; set; }
        public DateTime PVOperateTime { get; set; }
        public long PVProblemId { get; set; }
        public string PVProblemNo { get; set; }

    }
}
