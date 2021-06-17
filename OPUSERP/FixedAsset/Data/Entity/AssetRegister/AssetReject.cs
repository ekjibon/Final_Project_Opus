using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.FixedAsset.Data.Entity.AssetRegister
{
    [Table("AssetRejects", Schema = "FAMS")]
    public class AssetReject : Base
    {
        public int? assetRegistrationId { get; set; }
        public AssetRegistration assetRegistration { get; set; }        

        public DateTime? rejectDate { get; set; }
        public string rejectBy { get; set; }       
       
    }
}
