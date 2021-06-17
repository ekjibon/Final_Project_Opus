using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OPUSERP.Data.Entity;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.VMS.Data.Entity.Inventory
{
    [Table("PartsIssueMaster", Schema = "VMS")]
    public class PartsIssueMaster:Base
    {
        public int? vehicleId { get; set; }
        public VehicleInformation vehicle { get; set; }
        [MaxLength(100)]
        public string issueNo { get; set; }

        public DateTime? issueDate { get; set; }

        public int? purchasePartsId { get; set; }
        public PurchasePartsMaster purchaseParts { get; set; }

        public decimal quantity { get; set; }
    }
}
