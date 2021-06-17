using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.FixedAsset.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace OPUSERP.FixedAsset.Data.Entity.AssetRegister
{
    [Table("AssetDepreciation", Schema = "FAMS")]
    public class AssetDepreciation : Base
    {
        public int? assetRegistrationId { get; set; }
        public AssetRegistration assetRegistration { get; set; }
        
        public int? depriciationPeriodId { get; set; }
        public DepriciationPeriod depriciationPeriod { get; set; }

        public decimal? depriciationRate { get; set; }
        public decimal? depriciationValue { get; set; }

    }
}
