using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Suggest
{
    public class SuggestionsModel
    {
        public SuggestionsSearchModel SearchModel { get; set; }

        public IEnumerable<SuggestionsInfoModel> SearchResultModel { get; set; }

        public SuggestionsInfoModel NewModel { get; set; }

        public SuggestionsModel() {

            this.SearchModel = new SuggestionsSearchModel();
            this.NewModel = new SuggestionsInfoModel();
        }
    } 
}
