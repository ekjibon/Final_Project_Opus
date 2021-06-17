using OPUSERP.Data.Entity;
using OPUSERP.FixedAsset.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.MasterData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.FixedAsset.Data.Entity.AssetRegister
{
    [Table("AssetRegistration", Schema = "FAMS")]
    public class AssetRegistration : Base
    {
        public int? purchaseInfoId { get; set; }
        public PurchaseInfo purchaseInfo { get; set; }
        
        public int? itemSpecificationId { get; set; }
        public ItemSpecification itemSpecification { get; set; }

        public int? depriciationRateId { get; set; }
        public DepriciationRate depriciationRate { get; set; }

        public string assetNo { get; set; }
        public decimal? assetValue { get; set; }
        public decimal? unitPrice { get; set; }
        public decimal quantity { get; set; }
        public decimal? carringCost { get; set; }
        public decimal? additionalCost { get; set; }
        [MaxLength(300)]
        public string assetDescription { get; set; }
        [MaxLength(30)]
        public string assetSerialNo { get; set; }
        
    }
}
