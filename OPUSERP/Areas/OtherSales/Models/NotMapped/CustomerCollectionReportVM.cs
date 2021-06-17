using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.OtherSales.Models.NotMapped
{
    public class CustomerCollectionReportVM
    {
        public decimal? openingDue { get; set; }

        public decimal? openingPayment { get; set; }
        
        public IEnumerable<SalesCollectionDetail> salesCollectionDetail { get; set; }
        public IEnumerable<BillReturnPaymentDetail> billReturnPaymentDetails { get; set; }

        public IEnumerable<SalesInvoiceMaster> salesInvoiceMaster { get; set; }
        public IEnumerable<SalesReturnInvoiceMaster> salesReturnInvoiceMasters { get; set; }

        public IEnumerable<PurchaseOrderMaster> purchaseOrderMasters { get; set; }

        //public IEnumerable<BillPaymentDetail> billPaymentDetails { get; set; }
        public IEnumerable<CollectionReportDetailsModel> collectionReportDetailsModels { get; set; }
    }
}
