
using OPUSERP.Sales.Data.Entity.Collection;
using OPUSERP.Sales.Data.Entity.Sales;
using System.Collections.Generic;

namespace OPUSERP.Areas.Sales.Models.NotMapped
{
    public class CustomerCollectionReportVM
    {
        public decimal? openingDue { get; set; }

        public decimal? openingPayment { get; set; }
        
        public IEnumerable<SalesCollectionDetail> salesCollectionDetail { get; set; }
      

        public IEnumerable<SalesInvoiceMaster> salesInvoiceMaster { get; set; }
       

       
        public IEnumerable<CollectionReportDetailsModel> collectionReportDetailsModels { get; set; }
    }
}
