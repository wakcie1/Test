using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Problem
{
    public class ProblemSolvingTeamModel
    {
        public long Id { get; set; }
        public long PSProblemId { get; set; }
        public string PSProblemNo { get; set; }
        public string PSUserNo { get; set; }
        public string PSUserName { get; set; }
        public string PSDeskEXT { get; set; }
        public long PSDeptId { get; set; }
        public string PSDeptName { get; set; }
        public string PSUserTitle { get; set; }
        public int? PSIsLeader { get; set; }
        public int? PSIsValid { get; set; }
        public string PSCreateUserNo { get; set; }
        public string PSCreateUserName { get; set; }
        public DateTime? PSCreateTime { get; set; }
        public string PSOperateUserNo { get; set; }
        public string PSOperateUserName { get; set; }
        public DateTime? PSOperateTime { get; set; }


        public ProblemSolvingTeamModel()
        {
            if (Id == 0)
            {
                this.PSCreateTime = this.PSOperateTime = DateTime.Now;
            }
            else
            {
                this.PSOperateTime = DateTime.Now;
            }
        }
    }
}
