using OPUSERP.Data.Entity;
using OPUSERP.FixedAsset.Data.Entity.MasterData;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;

namespace OPUSERP.FixedAsset.Data.Entity.AssetRegister
{
    [Table("PurchaseInfo", Schema = "FAMS")]
    public class PurchaseInfo:Base
    {
        public int? supplierId { get; set; }
        public SCM.Data.Entity.Supplier.Organization supplier { get; set; }
        public DateTime? purchaseDate { get; set; }
        public DateTime? receiveDate { get; set; }
        public DateTime? challanDate { get; set; }
        public string challanNo { get; set; }
        public int? purchaseOrderMasterId { get; set; }
        public PurchaseOrderMaster purchaseOrderMaster { get; set; }
        public decimal purchaseRate { get; set; }

        public decimal quantity { get; set; }
        public decimal? carringCost { get; set; }
        public decimal? additionalCost { get; set; }
        public string purchaseNo { get; set; }
        public int? registrationTypeId { get; set; }
        public RegistrationType registrationType { get; set; }

        public int? procurementSourceId { get; set; }
        public ProcurementSource procurementSource { get; set; }
    }
}
