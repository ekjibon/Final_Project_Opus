using OPUSERP.Areas.Auth.Models;
using OPUSERP.Data.Entity.Master;
using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using OPUSERP.FixedAsset.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.Areas.FAMSAssetRegister.Models
{
    public class AssetDepreciationViewModel
    {
        public int? depriciationPeriodId { get; set; }
        public int? itemCategoryId { get; set; }


        public IEnumerable<DepriciationPeriod> depriciationPeriods { get; set; }
        public IEnumerable<DepriciationRate> depriciationRates { get; set; }
        public IEnumerable<AssetDepreciation> assetDepreciations { get; set; }
        


    }
}
