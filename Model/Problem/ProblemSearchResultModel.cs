using Model.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Problem
{
    public class ProblemSearchResultModel
    {
        public IEnumerable<ProblemInfoModel> Models { get; set; }
        public Page Page { get; set; }
    }
}
