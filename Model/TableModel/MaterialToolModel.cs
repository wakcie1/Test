using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.TableModel
{
    public class MaterialToolModel
    {
        public long Id { get; set; }
        public string MTToolNo { get; set; }
        public string MTSapPN { get; set; }
        public string MTSapQuantity { get; set; }
        public string MTSapLibrary { get; set; }
        public string MTSapProductName { get; set; }
        public string MTToolLibrary { get; set; }
        public string MTProductName { get; set; }
        public string MTStatus { get; set; }
        public string MTQuality { get; set; }
        public string MTCustomerPN { get; set; }
        public string MTCustomerNo { get; set; }
        public string MTOutlineDimension { get; set; }
        public string MTBelong { get; set; }
        public string MTToolSupplier { get; set; }
        public string MTToolSupplierNo { get; set; }
        public string MTProductDate { get; set; }
        public string MTCavity { get; set; }
        public int? MTIsValid { get; set; }
        public string MTCreateUserNo { get; set; }
        public string MTCreateUserName { get; set; }
        public DateTime? MTCreateTime { get; set; }
        public string MTOperateUserNo { get; set; }
        public string MTOperateUserName { get; set; }
        public DateTime? MTOperateTime { get; set; } 
    }
}
