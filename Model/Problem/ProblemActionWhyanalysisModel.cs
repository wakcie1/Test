using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Problem
{
    public class ProblemActionWhyanalysisModel
    {
        public long Id { get; set; }
        public string PAWWhyForm { get; set; }
        public string PAWWhyQuestionChain { get; set; }
        public string PAWWhy1 { get; set; }
        public string PAWWhy2 { get; set; }
        public string PAWWhy3 { get; set; }
        public string PAWWhy4 { get; set; }
        public string PAWWhy5 { get; set; }
        public int? PAWIsValid { get; set; }
        public string PAWCreateUserNo { get; set; }
        public string PAWCreateUserName { get; set; }
        public DateTime? PAWCreateTime { get; set; }
        public string PAWOperateUserNo { get; set; }
        public string PAWOperateUserName { get; set; }
        public DateTime? PAWOperateTime { get; set; }
        public long PAWProblemId { get; set; }
        public string PAWProblemNo { get; set; }

    }
}
