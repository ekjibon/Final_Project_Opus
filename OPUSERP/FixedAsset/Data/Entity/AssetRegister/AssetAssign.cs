using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.FixedAsset.Data.Entity.AssetRegister
{
    [Table("AssetAssign", Schema = "FAMS")]
    public class AssetAssign : Base
    {
        public int? assetRegistrationId { get; set; }
        public AssetRegistration assetRegistration { get; set; }

        public DateTime? assignDate { get; set; }

        public int? empId { get; set; }
        public int? coordinatorempId { get; set; }

        public int? departmentId { get; set; }
        public Department department { get; set; }

        public int? deliveryLocationId { get; set; }
        public DeliveryLocation deliveryLocation { get; set; }

        public string remarks { get; set; }

        
    }
}
