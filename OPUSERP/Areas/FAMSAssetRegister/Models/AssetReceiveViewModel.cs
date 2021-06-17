using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.Areas.FAMSAssetRegister.Models
{
    public class AssetReceiveViewModel
    {
        public int? assetReceiveId { get; set; }
        public int? assetRegistrationId { get; set; }
        public DateTime? receiveDate { get; set; }
        public string receiveBy { get; set; }
        public string sendBy { get; set; }

        public IEnumerable<AssetReceive> assetReceives { get; set; }
        public AssetReceive assetReceive { get; set; }
        public IEnumerable<AssetRegistration> assetRegistrations { get; set; }
        public Task<AssetRegistration> assetRegistration { get; set; }
        public IEnumerable<AssetReject> assetRejects { get; set; }
    }
}
