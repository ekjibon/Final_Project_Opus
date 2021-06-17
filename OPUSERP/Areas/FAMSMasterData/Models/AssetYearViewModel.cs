using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using OPUSERP.FixedAsset.Data.Entity.MasterData;
using System.Collections.Generic;

namespace OPUSERP.Areas.FAMSMasterData.Models
{
    public class AssetYearViewModel
    {
        public int assetYearId { get; set; }       
        public string AssetYearName { get; set; }

        public IEnumerable<AssetYear> assetYears { get; set; }
    }
}
