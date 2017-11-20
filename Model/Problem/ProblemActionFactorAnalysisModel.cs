using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Problem
{
   public class ProblemActionFactorAnalysisModel
   {
        public long Id { get; set; }
        public string PAFType { get; set; }
        public string PAFPossibleCause { get; set; }
        public string PAFWhat { get; set; }
        public string PAFWhoNo { get; set; }
        public string PAFWho { get; set; }
        public DateTime? PAFValidatedDate { get; set; }
        public string PAFValidatedDateDesc
        {
            get
            {
                var date = PAFValidatedDate.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm");
                return date == "1900-01-01 00:00" ? string.Empty : date;
            }
        }
        public string PAFPotentialCause { get; set; } 
        public int PAFIsValid { get; set; } 
        public string PAFCreateUserNo { get; set; }
        public string PAFCreateUserName { get; set; }
        public DateTime PAFCreateTime { get; set; }
        public string PAFOperateUserNo { get; set; }
        public string PAFOperateUserName { get; set; }
        public DateTime PAFOperateTime { get; set; }
        public long PAFProblemId { get; set; }
        public string PAFProblemNo { get; set; }
    }
}
