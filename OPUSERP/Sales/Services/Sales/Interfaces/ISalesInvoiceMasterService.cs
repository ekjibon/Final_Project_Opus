

using OPUSERP.Areas.Sales.Models.NotMapped;
using OPUSERP.Sales.Data.Entity.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Sales.Services.Sales.Interface
{
   public interface ISalesInvoiceMasterService
    {
        #region SaleInvoiceMaster
        Task<int> SaveSalesInvoiceMaster(SalesInvoiceMaster salesInvoiceMaster);
        Task<IEnumerable<SalesInvoiceMaster>> GetAllSalesInvoiceMaster();
        Task<SalesInvoiceMaster> GetSalesInvoiceMasterById(int id);
        Task<bool> DeleteSalesInvoiceMasterById(int id);

        Task<IEnumerable<PaymentInvoiceWithDue>> GetDueSalesInvoice(int customerId);
        Task<bool> UpdateSalesInvoiceMasterById(int id);
        Task<bool> UpdateSalesInvoiceMasterStockCloseById(int id);
        Task<CustomerCollectionReportVM> GetSalesInvoiceRecipt(int customerId, string fromDate, string toDate);
        //Task<CustomerCollectionReportVM> GetSalesInvoiceRecipt(int customerId, string fromDate, string toDate, int storeId);
        //Task<IEnumerable<PosSalesDetailModel>> GetSalesInvoiceMasterByDate(DateTime from, DateTime toDate);
        #endregion
        #region
        Task<int> SaveRepSalesInvoiceMaster(RepSalesInvoiceMaster salesInvoiceMaster);
        Task<IEnumerable<RepSalesInvoiceMaster>> GetAllRepSalesInvoiceMaster();
        Task<RepSalesInvoiceMaster> GetRepSalesInvoiceMasterById(int id);
        Task<bool> UpdateRepSalesInvoiceMasterById(int id);
        Task<bool> UpdateRepSalesInvoiceMasterStockCloseById(int id);
        Task<bool> DeleteRepSalesInvoiceMasterById(int id);

        #endregion

    }
}
