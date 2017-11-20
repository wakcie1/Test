using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.TableModel
{
   public  class CodeDefectModel
    {
        public long Id { get; set; }
        public string BDCodeNameEn { get; set; }
        public string BDCodeNameCn { get; set; }
        public string BDCode { get; set; }
        public string BDCodeType { get; set; }
        public int BDCodeNo { get; set; }
        public int? BDIsValid { get; set; }
        public string BDCreateUserNo { get; set; }
        public string BDCreateUserName { get; set; }
        public DateTime? BDCreateTime { get; set; }
        public string BDOperateUserNo { get; set; }
        public string BDOperateUserName { get; set; }
        public DateTime? BDOperateTime { get; set; }
    }
}
