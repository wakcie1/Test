using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Problem
{
    public class ProblemStandardizationModel
    {
        public long Id { get; set; }
        public string PSItemName { get; set; }
        public int PSItemNameNo { get; set; }
        public int? PSNeedUpdate { get; set; }
        public string PSWhoNo { get; set; }
        public string PSWho { get; set; }
        public DateTime? PSPlanDate { get; set; }
        public string PSPlanDateDesc
        {
            get
            {
                var date = PSPlanDate.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm");
                return date == "1900-01-01 00:00" ? string.Empty : date;
            }
        }
        public DateTime? PSActualDate { get; set; }
        public string PSActualDateDesc
        {
            get
            {
                var date = PSActualDate.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm");
                return date == "1900-01-01 00:00" ? string.Empty : date;
            }
        }
        public string PSDocNo { get; set; }
        public string PSChangeContent { get; set; }
        public string PSOldVersion { get; set; }
        public string PSNewVersion { get; set; }
        public string PSAttachment { get; set; }
        public string PSAttachmentUrl { get; set; }
        public string PSAttachmentDownloadUrl { get; set; }
        public int? PSIsValid { get; set; }
        public string PSCreateUserNo { get; set; }
        public string PSCreateUserName { get; set; }
        public DateTime? PSCreateTime { get; set; }
        public string PSOperateUserNo { get; set; }
        public string PSOperateUserName { get; set; }
        public DateTime? PSOperateTime { get; set; }
        public long PSProblemId { get; set; }
        public string PSProblemNo { get; set; }
    }
}
