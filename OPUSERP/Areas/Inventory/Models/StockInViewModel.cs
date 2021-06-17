using OPUSERP.Areas.POS.Models;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.Purchase.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Data.Entity.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Inventory.Models
{
    public class StockInViewModel
    {
       public int? itemReqMasterId { get; set; }
        public string MRNO { get; set; }
        public string reqNO { get; set; }
        public string remarks { get; set; }
        public int? storeId { get; set; }
        public int?[] detailids { get; set; }
        public int?[] itemPriceId { get; set; }
        public decimal?[] rates { get; set; }
        public int?[] specids { get; set; }
        public int?[] itemPriceFixingId { get; set; }
        public decimal?[] quantitys { get; set; }
        public decimal?[] duequantitys { get; set; }
        public int?[] storeDepartmentId { get; set; }
        public DateTime stockDate { get; set; }
        public DateTime? paymentDate { get; set; }
        public int? stockMasterId { get; set; }
        public int? purchaseId { get; set; }
        public int? resouceId { get; set; }
        public int? productionId { get; set; }
        public string receiveBy { get; set; }


        public int?[] selectPaymentHeadIds { get; set; }

        public int? relSupplierCustomerResourseId { get; set; }

        public decimal?[] selectPaymentHeadAmounts { get; set; }

        public decimal?[] selectPaymentHeadDues { get; set; }

        public IEnumerable<PurchaseOrderMaster> purchaseOrdersMasters { get; set; }
        public PurchaseOrderMaster purchaseOrdersMaster { get; set; }
        public IEnumerable<SalesInvoiceMaster> invoiceMasters { get; set; }
        public IEnumerable<StockItemViewModel> stockItemViewModels { get; set; }
        public IEnumerable<StockMaster> stockMasters { get; set; }
        public IEnumerable<StockDetails> GetStocksbyMasterId { get; set; } 
        public IEnumerable<StoreDepartmentType> storeDepartmentTypes { get; set; } 
        public StockMaster stockMasterById { get; set; }
        public SalesInvoiceMaster SalesInvoiceMaster { get; set; }
        public IEnumerable<Comment> comments { get; set; }
        public IEnumerable<DocumentPhotoAttachment> photoes { get; set; }
        public IEnumerable<DocumentPhotoAttachment> documents { get; set; }
        public IEnumerable<TransferMaster> transferMasters { get; set; }
        public TransferMaster transferMaster { get; set; }
        public IEnumerable<Company> companies { get; set; }
        public Company company { get; set; }
        public IEnumerable<StoreAssign> storeAssigns { get; set; }
        public IEnumerable<StockBalancesViewModel> stockBalancesViewModels { get; set; }
        public IEnumerable<StockSummaryViewModel> stockSummaryViewModels { get; set; }
        public IEnumerable<StockBalanceByItemViewModel> stockBalanceByItemViewModels { get; set; }
    }
}
