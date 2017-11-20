using Model.CommonModel;
using Model.TableModel; 
using System.Collections.Generic; 

namespace Model.Material
{
    public class MaterialToolSearchResultModel
    {
        public IEnumerable<MaterialToolModel> Models { get; set; }
        public Page Page { get; set; }
    }
}
