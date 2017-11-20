using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Problem
{
    public class ProblemActionPreventiveModel
    {
        public long Id { get; set; }
        public string PAPWhat { get; set; }
        public string PAPWhoNo { get; set; }
        public string PAPWho { get; set; }
        public DateTime? PAPPlanDate { get; set; }
        public string PAPPlanDateDesc
        {
            get
            {
                var date = PAPPlanDate.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm");
                return date == "1900-01-01 00:00" ? string.Empty : date;
            }
        }
        public DateTime? PAPActualDate { get; set; }
        public string PAPActualDateDesc
        {
            get
            {
                var date = PAPActualDate.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm");
                return date == "1900-01-01 00:00" ? string.Empty : date;
            }
        }
        public string PAPWhere { get; set; }
        public string PAPAttachment { get; set; }
        public string PAPAttachmentUrl { get; set; }
        public string PAPAttachmentDownloadUrl { get; set; }
        public string PAPStatus { get; set; }
        public string PAPComment { get; set; }
        public int? PAPIsValid { get; set; }
        public string PAPCreateUserNo { get; set; }
        public string PAPCreateUserName { get; set; }
        public DateTime? PAPCreateTime { get; set; }
        public string PAPOperateUserNo { get; set; }
        public string PAPOperateUserName { get; set; }
        public DateTime? PAPOperateTime { get; set; }
        public long PAPProblemId { get; set; }
        public string PAPProblemNo { get; set; }
    }
}
