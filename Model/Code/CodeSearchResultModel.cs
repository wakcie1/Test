using Model.CommonModel;
using Model.TableModel;
using System.Collections.Generic;

namespace Model.Code
{
    public class CodeSearchResultModel
    {
       public IEnumerable<CodeDisplayMode> Models { get; set; }
       public Page Page { get; set; }
    }

    public class CodeDisplayMode
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class DefectCodeSearchResultModel
    {
        public IEnumerable<CodeDefectModel> Models { get; set; }
        public Page Page { get; set; }
    }
}
