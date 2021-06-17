using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.FixedAsset.Data.Entity.AssetRegister
{
    [Table("AssetTransfer", Schema = "FAMS")]
    public class AssetTransfer : Base
    {
        public int? assetRegistrationId { get; set; }
        public AssetRegistration assetRegistration { get; set; }

        public int? deliveryLocationId { get; set; }
        public DeliveryLocation deliveryLocation { get; set; }
        public int? departmentId { get; set; }
        public Department department { get; set; }
        public DateTime? transferDate { get; set; }

        public int? currentEmpId { get; set; }
        public int? previousEmpId { get; set; }
        public int? currentCoordinatorId { get; set; }
        public int? previousCoordinatorId { get; set; }
       
    }
}
