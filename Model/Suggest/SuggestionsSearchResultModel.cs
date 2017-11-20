using Model.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Suggest
{
    public class SuggestionsSearchResultModel
    {
        public IEnumerable<SuggestionsInfoModel> SearchResultModel { get; set; }
        public Page Page { get; set; }
    }
}
