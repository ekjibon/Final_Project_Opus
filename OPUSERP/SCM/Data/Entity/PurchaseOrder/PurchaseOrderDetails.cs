using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.PurchaseProcess;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.PurchaseOrder
{
    [Table("PurchaseOrderDetails", Schema = "SCM")]
    public class PurchaseOrderDetails:Base
    {
        public int? purchaseId { get; set; }
        public PurchaseOrderMaster purchase { get; set; }

        public int? cSDetailId { get; set; }
        public CSDetail cSDetail { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? poQty { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? poRate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? grQty { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? vat { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? vatPercent { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? tax { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? taxPercent { get; set; }

        public int? currencyId { get; set; }
        public Currency currency { get; set; }

        public int? deliveryLocationId { get; set; }
        public DeliveryLocation deliveryLocation { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        public string otherDeliveryLocation { get; set; }

        //new add on 12/12/2020
        public int? itemSpecificationId { get; set; }
        public ItemSpecification itemSpecification { get; set; }
        public string description { get; set; }
    }
}
