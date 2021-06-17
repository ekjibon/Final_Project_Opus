using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.FixedAsset.Data.Entity.AssetRegister
{
    [Table("AssetRetirements", Schema = "FAMS")]
    public class AssetRetirement : Base
    {
        public int? assetRegistrationId { get; set; }
        public AssetRegistration assetRegistration { get; set; }

        public int? assetRetirementTypeId { get; set; }
        public AssetRetirementType assetRetirementType { get; set; }

        public DateTime? retirementDate { get; set; }
        public string causeOfRetirement { get; set; }
        public decimal? scrapValue { get; set; }
    }
}
