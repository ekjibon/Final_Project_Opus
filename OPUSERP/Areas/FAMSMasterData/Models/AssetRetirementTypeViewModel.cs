using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using System.Collections.Generic;

namespace OPUSERP.Areas.FAMSMasterData.Models
{
    public class AssetRetirementTypeViewModel
    {
        public int assetRetirementTypeId { get; set; }       
        public string assetRetirementTypeName { get; set; }

        public IEnumerable<AssetRetirementType> assetRetirementTypes { get; set; }
    }
}
