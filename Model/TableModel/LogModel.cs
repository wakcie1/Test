using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.TableModel
{
    public class LogModel
    {
        public long Id { get; set; }
        public string BLCode { get; set; }
        public string BLLogDesc { get; set; }
        public int BLLogType { get; set; }
        public string BLFilterValue1 { get; set; }
        public string BLFilterValue2 { get; set; }
        public long BLFilterId1 { get; set; }
        public long BLFilterId2 { get; set; }
        public int  BLIsValid { get; set; } 
        public string BLCreateUserNo { get; set; }
        public string BLCreateUserName { get; set; }
        public DateTime BLCreateTime { get; set; }
    }
}
