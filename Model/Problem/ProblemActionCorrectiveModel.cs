using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Problem
{
    public class ProblemActionCorrectiveModel
    {

        public long Id { get; set; }
        public string PACWhat { get; set; }
        public string PACWhoNo { get; set; }
        public string PACWho { get; set; }
        public DateTime? PACPlanDate { get; set; }
        public string PACPlanDateDesc
        {
            get
            {
                var date = PACPlanDate.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm");
                return date == "1900-01-01 00:00" ? string.Empty : date;
            }
        }
        public DateTime? PACActualDate { get; set; }
        public string PACActualDateDesc
        {
            get
            {
                var date = PACActualDate.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm");
                return date == "1900-01-01 00:00" ? string.Empty : date;
            }
        }
        public string PACWhere { get; set; }
        public string PACAttachment { get; set; }
        public string PACAttachmentUrl { get; set; }
        public string PACAttachmentDownloadUrl { get; set; }
        public string PACStatus { get; set; }
        public string PACComment { get; set; }
        public int? PACIsValid { get; set; }
        public string PACCreateUserNo { get; set; }
        public string PACCreateUserName { get; set; }
        public DateTime? PACCreateTime { get; set; }
        public string PACOperateUserNo { get; set; }
        public string PACOperateUserName { get; set; }
        public DateTime? PACOperateTime { get; set; }
        public long PACProblemId { get; set; }
        public string PACProblemNo { get; set; }
    }
}
