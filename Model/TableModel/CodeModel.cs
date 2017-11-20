using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.TableModel
{
   public  class CodeModel
    {
        public long Id { get; set; }
        public string BCCode { get; set; }
        public string BCCodeDesc { get; set; }
        public string BCCategory { get; set; }
        public int BCCodeOrder { get; set; }
        public int BCIsValid { get; set; }
        public string BCCreateUserNo { get; set; }
        public string BCCreateUserName { get; set; }
        public DateTime BCCreateTime { get; set; }
        public string BCOperateUserNo { get; set; }
        public string BCOperateUserName { get; set; }
        public DateTime BCOperateTime { get; set; }
    }
}
