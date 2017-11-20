using Model.CommonModel;
using Model.TableModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Material
{
    public class MaterialOtherSearchResultModel
    {
        public IEnumerable<WorkOrderInfo> Models { get; set; }
        public Page Page { get; set; }
    }
}
