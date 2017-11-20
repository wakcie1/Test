using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.TableModel
{
    public class WorkOrderInfo
    {
        public long Id { get; set; }
        public string WIWorkOrder { get; set; }
        public string WISapPN { get; set; }
        public string WIProductName { get; set; }
        public string WIReceiptTime { get; set; }
        public string WIReceiptBy { get; set; }
        public string WICloseDateShift { get; set; }
        public string WIOrderArchived { get; set; }
        public string WIParameterRecord { get; set; }
        public string WIToolMaintenanceRecord { get; set; }
        public string WIToolMachineCheck { get; set; }
        public string WIQuantityConfirm { get; set; }
        public string WIArchivedBy { get; set; }
        public string WIWeeklyCheck { get; set; }
        public string WIRemarks { get; set; }
        public string WIGetBy { get; set; }
        public string WIGetTime { get; set; }
        public string WICreateUserNo { get; set; }
        public string WICreateUserName { get; set; }
        public DateTime? WICreateTime { get; set; }
        public string WIOperateUserNo { get; set; }
        public string WIOperateUserName { get; set; }
        public DateTime? WIOperateTime { get; set; }
        public int? WIIsValid { get; set; }
        public int WIType { get; set; }  
    }
}
