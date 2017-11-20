using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Problem
{
    public class ProblemInfoModel
    {
        public long Id { get; set; }
        public string PIProblemNo { get; set; }
        public string PIProcess { get; set; }
        public string PIMachine { get; set; }
        public string PITool { get; set; }
        public long PIMaterialId { get; set; }
        public string PICustomerPN { get; set; }
        public string PICustomer { get; set; }
        public string PIProductName { get; set; }
        public string PIWorkOrder { get; set; }
        public DateTime PIProblemDate { get; set; }
        public string PIProblemDateDesc { get { return PIProblemDate.ToString("yyyy-MM-dd HH:mm"); } }
        public string PIProblemSource { get; set; }
        public string PIDefectType { get; set; }
        public string PIDefectCode { get; set; }
        public int? PIDefectQty { get; set; }
        public string PIShiftType { get; set; }
        public int? PIIsRepeated { get; set; }
        public string PIProblemDesc { get; set; }
        public string PIPicture1 { get; set; }
        public string PIPicture1Url { get; set; }
        public string PIPicture2 { get; set; }
        public string PIPicture2Url { get; set; }
        public string PIPicture3 { get; set; }
        public string PIPicture3Url { get; set; }
        public string PIPicture4 { get; set; }
        public string PIPicture4Url { get; set; }
        public string PIPicture5 { get; set; }
        public string PIPicture5Url { get; set; }
        public string PIPicture6 { get; set; }
        public string PIPicture6Url { get; set; }
        public int? PIProcessStatus { get; set; }
        public int? PIStatus { get; set; }
        public string PIStatusDesc { get; set; }
        public int? PISeverity { get; set; }
        public string PISeverityDesc { get; set; }
        public string PIRootCause { get; set; }
        public string PIRootCauseAssignNo { get; set; }
        public string PIRootCauseAssignName { get; set; }
        public string PIActionPlan { get; set; }
        public int? PIExtendPorjects { get; set; }
        public string PIExtendComment { get; set; }
        public string PIExtendApproveComment { get; set; }
        public int? PIIsValid { get; set; }
        public string PICreateUserNo { get; set; }
        public string PICreateUserName { get; set; }
        public DateTime PICreateTime { get; set; }
        public string PIOperateUserNo { get; set; }
        public string PIOperateUserName { get; set; }
        public DateTime PIOperateTime { get; set; }
        public string PISapPN { get; set; }
        public DateTime? PINextProblemDate { get; set; }
        public string PINextProblemDateDesc
        {
            get
            {
                var date = PINextProblemDate.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm");
                return date == "1900-01-01 00:00" ? string.Empty : date;
            }
        }

        public DateTime? PIFinishDate { get; set; }
        public string PIFinishDateDesc
        {
            get
            {
                var date = PIFinishDate.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm");
                return date == "1900-01-01 00:00" ? string.Empty : date;
            }
        }

        public int? PIApproveLayeredAudit { get; set; }

        public int? PIApproveVerification { get; set; }
        public int? PIApproveStandardization { get; set; }

        public ProblemInfoModel()
        {
            if (Id == 0)
            {
                this.PICreateTime = this.PIOperateTime = DateTime.Now;
            }
            else
            {
                this.PIOperateTime = DateTime.Now;
            }
        }
    }
}
