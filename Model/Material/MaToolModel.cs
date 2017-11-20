using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Material
{
    public class MaToolModel
    {
        public long Id { get; set; }
        public string BMEquipmentName { get; set; }
        public int? BMClassification { get; set; }
        public string BMEquipmentNo { get; set; }
        public string BMFixtureNo { get; set; }
        public string BMType { get; set; }
        public string BMSerialNumber { get; set; }
        public int? BMQuantity { get; set; }
        public string BMManufacturedDate { get; set; }
        public string BMPower { get; set; }
        public string BMOutlineDimension { get; set; }
        public string BMAbility { get; set; }
        public int? BMNeedPressureAir { get; set; }
        public int? BMNeedCoolingWater { get; set; }
        public string BMIncomingDate { get; set; }
        public string BMRemarks { get; set; }
        public int? BMIsValid { get; set; }
        public string BMCreateUserNo { get; set; }
        public string BMCreateUserName { get; set; }
        public DateTime? BMCreateTime { get; set; }
        public string BMOperateUserNo { get; set; }
        public string BMOperateUserName { get; set; }
        public DateTime? BMOperateTime { get; set; }

        public MaToolModel()
        {
            if (Id == 0)
            {
                this.BMCreateTime = this.BMOperateTime = DateTime.Now;
            }
            else
            {
                this.BMOperateTime = DateTime.Now;
            }
        }
    }
}
