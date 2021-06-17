using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.FAMSAssetRegister.Models
{
    public class AssetOverhaulingViewModel
    {
        public int overhaulingId { get; set; }
        public int? assetRegistrationId { get; set; }
        public DateTime? overhaulingDate { get; set; }
        public decimal? overhaulingCost { get; set; }
        public string description { get; set; }

        public IEnumerable<AssetRegistration> assetRegistrations { get; set; }
        public IEnumerable<AssetOverhauling> assetOverhaulings { get; set; }
    }
}
