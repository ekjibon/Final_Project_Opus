

using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Purchase.Data.Entity
{

    [Table("PurchaseOrdersDetails", Schema = "Purchase")]
    public class PurchaseOrdersDetail:Base
    {
        public int? purchaseId { get; set; }
        public PurchaseOrdersMaster purchase { get; set; }

        public int? itemSpecificationId { get; set; }
        public ItemSpecification itemSpecification { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string description { get; set; }
        
       

        [Column(TypeName = "decimal(18,2)")]
        public decimal? quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? rate { get; set; }

        public int? currencyId { get; set; }
        public Currency currency { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? vatPercent { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? taxPercent { get; set; }
    }
}
