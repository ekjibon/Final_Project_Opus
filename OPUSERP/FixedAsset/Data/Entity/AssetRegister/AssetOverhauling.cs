using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.FixedAsset.Data.Entity.AssetRegister
{
    [Table("AssetOverhauling", Schema = "FAMS")]
    public class AssetOverhauling : Base
    {
        public int? assetRegistrationId { get; set; }
        public AssetRegistration assetRegistration { get; set; }

        public DateTime? overhaulingDate { get; set; }
        public decimal? overhaulingCost { get; set; }
        public string description { get; set; }
    }
}
