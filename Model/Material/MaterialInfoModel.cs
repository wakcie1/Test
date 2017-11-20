using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Material
{
    public class MaterialInfoModel
    {
        public long Id { get; set; }
        public string MIProcessType { get; set; }
        public string MICustomerPN { get; set; }
        public string MIProductName { get; set; }
        public string MICustomer { get; set; }
        public string MIPicture { get; set; }
        public string MIPictureUrl { get; set; }
        public string MIMaterialPN { get; set; }
        public string MIMoldNo { get; set; }
        public string MISapPN { get; set; }
        public string MIInjectionMC { get; set; }
        public int? MICavity { get; set; }
        public decimal? MICycletime { get; set; }
        public decimal? MICycletimeCav { get; set; }
        public int? MIStandardHeadcount { get; set; }
        public string MTStandardScrap { get; set; }
        public decimal? MICavityG { get; set; }
        public string MIWorkOrder { get; set; }
        public int? MIIsValid { get; set; }
        public string MICreateUserNo { get; set; }
        public string MICreateUserName { get; set; }
        public DateTime? MICreateTime { get; set; }
        public string MIOperateUserNo { get; set; }
        public string MIOperateUserName { get; set; }
        public DateTime? MIOperateTime { get; set; }
        public string MIAssAC { get; set; }
 
        public MaterialInfoModel()
        {
            if (Id == 0)
            {
                this.MICreateTime = this.MIOperateTime = DateTime.Now;
            }
            else
            {
                this.MIOperateTime = DateTime.Now;
            }
        }
    }
}
