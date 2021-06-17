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
    public class AssetDepreciationReportViewModel
    {
        public int? Id { get; set; }
        public string assetNo { get; set; }
        public string itemName { get; set; }
        public string category { get; set; }
        public string periodName { get; set; }
        public decimal? depriciationRate { get; set; }
        public decimal? depriciationValue { get; set; }
        public decimal? assetValue { get; set; }
        public decimal? accmulatedDepriciation { get; set; }
        public decimal? totalDepriciation { get; set; }
        public decimal? writtenDownValue { get; set; }
        public decimal? overhaulingCost { get; set; }
    }
}
