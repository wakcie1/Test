using Model.CommonModel;
using Model.TableModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Code
{
    public  class CodeInfoModel: ResultInfoModel
    {
        public CodeModel model { get; set; }
    }

    public class DefectCodeInfoModel : ResultInfoModel
    {
        public CodeDefectModel model { get; set; }
    }
}
