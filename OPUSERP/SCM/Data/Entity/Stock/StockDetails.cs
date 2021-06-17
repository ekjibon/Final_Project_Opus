using OPUSERP.Data.Entity;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.Production.Data.Entity;
using OPUSERP.Purchase.Data.Entity;
using OPUSERP.SCM.Data.Entity.IOU;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.Stock
{
    [Table("StockDetails", Schema = "SCM")]
    public class StockDetails:Base
    {
        public int? stockMasterId { get; set; }
        public StockMaster stockMaster { get; set; }

        public int? orderDetailsId { get; set; }
        public PurchaseOrderDetails orderDetails { get; set; }

        public int? transferDetailId { get; set; }
        public TransferDetail transferDetail { get; set; }

        public int? iOUDetailsId { get; set; }
        public IOUDetails iOUDetails { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? rate { get; set; }

        public decimal? grQty { get; set; }

        public decimal? poQty { get; set; }
        public decimal? qty { get; set; }

        public decimal? purchaseRate { get; set; }

        public int? purchaseOrdersDetailId { get; set; }
        public PurchaseOrdersDetail purchaseOrdersDetail { get; set; }

        public int? itemSpecificationId { get; set; }
        public ItemSpecification itemSpecification { get; set; }

        public int? salesInvoiceDetailId { get; set; }
        public SalesInvoiceDetail salesInvoiceDetail { get; set; }

        public int? itemReqDetailsId { get; set; }
        public ItemReqDetails itemReqDetails { get; set; }

        public int? productionRequsitionDetailsId { get; set; }
        public ProductionRequsitionDetails productionRequsitionDetails { get; set; }
        public int? storeDepartmentTypeId { get; set; }
        public StoreDepartmentType storeDepartmentType { get; set; }
    }
}
