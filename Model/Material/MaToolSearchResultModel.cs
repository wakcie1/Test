using Model.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Material
{
    public class MaToolSearchResultModel
    {
        public IEnumerable<MaToolModel> Models { get; set; }
        public Page Page { get; set; }
    }
}
