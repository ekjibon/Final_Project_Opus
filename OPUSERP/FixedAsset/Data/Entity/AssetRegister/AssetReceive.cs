using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.FixedAsset.Data.Entity.AssetRegister
{
    [Table("AssetReceives", Schema = "FAMS")]
    public class AssetReceive : Base
    {
        public int? assetRegistrationId { get; set; }
        public AssetRegistration assetRegistration { get; set; }       

        public DateTime? receiveDate { get; set; }        
        public string receiveBy { get; set; }
        public string sendBy { get; set; }
    }
}
