using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Problem
{
    public class ProblemQualityAlertModel
    {
        public long Id { get; set; }
        public long PQProblemId { get; set; }
        public string PQProblemNo { get; set; }
        public string PQWhat { get; set; }
        public string PQWhoNo { get; set; }
        public string PQWho { get; set; }
        public DateTime? PQPlanDate { get; set; }
        public string PQPlanDateDesc
        {
            get
            {
                var date = PQPlanDate.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm");
                return date == "1900-01-01 00:00" ? string.Empty : date;
            }
        }

        public DateTime? PQActualDate { get; set; }
        public string PQActualDateDesc
        {
            get
            {
                var date = PQActualDate.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm");
                return date == "1900-01-01 00:00" ? string.Empty : date;
            }
        }
        public string PQAttachment { get; set; }
        public string PQAttachmentUrl { get; set; }
        public string PQAttachmentDownloadUrl { get; set; }
        public int? PQIsValid { get; set; }
        public string PQCreateUserNo { get; set; }
        public string PQCreateUserName { get; set; }
        public DateTime? PQCreateTime { get; set; }
        public string PQOperateUserNo { get; set; }
        public string PQOperateUserName { get; set; }
        public DateTime? PQOperateTime { get; set; }

    }
}
