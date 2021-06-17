using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.POS.Data.Entity;
using OPUSERP.Production.Data.Entity;
using OPUSERP.Purchase.Data.Entity;
using OPUSERP.SCM.Data.Entity.IOU;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.Stock
{
    [Table("StockMaster", Schema = "SCM")]
    public class StockMaster:Base
    {
        public int? companyId { get; set; }
        public Company company { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StockDate { get; set; }
        public string MRNO { get; set; }
        public int? storeId { get; set; }
        public Store store { get; set; }
        public string receiveNo { get; set; }
        public DateTime? receiveDate { get; set; }
        public string receiveBy { get; set; }
        public string receiveType { get; set; } //PO or IOU
        public string receiveMode { get; set; }//Draft or Final
        public int? purchaseId { get; set; }
        public PurchaseOrderMaster purchase { get; set; }
        public int? IOUId { get; set; }
        public IOUMaster IOU { get; set; }
        public int? userId { get; set; }
        public int? stockStatusId { get; set; } //1 for stockin 2 for stock out 3 for opening stock in
        public int? purchaseOrdersMasterId { get; set; }
        public PurchaseOrdersMaster  purchaseOrdersMaster { get; set; } //General Purchase
        public int? itemReqMasterId { get; set; }
        public ItemReqMaster itemReqMaster { get; set; }
        public int? productionRequsitionId { get; set; }
        public ProductionRequsitionMaster productionRequsition { get; set; }
        public int? openingStockId { get; set; }
        public OpeningStock openingStock { get; set; }
        public string remarks { get; set; }
        [NotMapped]
        public string SupplierName { get; set; }


    }
}
