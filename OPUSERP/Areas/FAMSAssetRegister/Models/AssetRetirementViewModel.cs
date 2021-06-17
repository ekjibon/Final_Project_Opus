using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.Areas.FAMSAssetRegister.Models
{
    public class AssetRetirementViewModel
    {
        public int? assetRetirementId { get; set; }
        public int? assetRegistrationId { get; set; }
        public int? assetRetirementTypeId { get; set; }
        public DateTime? retirementDate { get; set; }
        public string causeOfRetirement { get; set; }
        public decimal? scrapValue { get; set; }

        public IEnumerable<AssetRetirementType> assetRetirementTypes { get; set; }
        public IEnumerable<AssetRetirement> assetRetirements { get; set; }
        public AssetRetirement assetRetirement { get; set; }
        public IEnumerable<AssetRegistration> assetRegistrations { get; set; }
        public Task<AssetRegistration> assetRegistration { get; set; }
    }
}
