using OPUSERP.Areas.OtherSales.Models.NotMapped;
using OPUSERP.Areas.POS.Models;
using OPUSERP.CRM.Data.SPModel;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.POS.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.OtherSales.Services.Sales.Interfaces
{
    public interface IOSalesInvoiceMasterService
    {
        #region SaleInvoiceMaster
        Task<IEnumerable<SalesInvoiceMaster>> GetAllSalesInvoiceMasterByrelSuplierCustomerId(int Id);
        Task<int> SaveSalesInvoiceMaster(SalesInvoiceMaster salesInvoiceMaster);
        Task<IEnumerable<SalesInvoiceMaster>> GetAllSalesInvoiceMaster();
        Task<SalesInvoiceMaster> GetSalesInvoiceMasterById(int id);
        Task<bool> DeleteSalesInvoiceMasterById(int id);
        Task<LeadAutoNumber> GetAutoSalesInvoiceNoBySp(string invoiceDate);
        Task<LeadAutoNumber> GetAutoRentSaleInvoiceNoBySp(string invoiceDate);
        Task<IEnumerable<PaymentInvoiceWithDue>> GetDueSalesInvoice(int customerId);
        Task<bool> UpdateSalesInvoiceMasterById(int id);
        Task<bool> UpdateSalesInvoiceMasterStockCloseById(int id);
        Task<CustomerCollectionReportVM> GetSalesInvoiceRecipt(int customerId, string fromDate, string toDate, int storeId);
        //Task<IEnumerable<PosSalesDetailModel>> GetSalesInvoiceMasterByDate(DateTime from, DateTime toDate);
        #endregion
        #region SalesReturnInvoice
        Task<int> SaveSalesReturnInvoiceMaster(SalesReturnInvoiceMaster salesReturnInvoiceMaster);
        Task<IEnumerable<SalesReturnInvoiceMaster>> GetAllSalesReturnInvoiceMaster();
        Task<bool> UpdateSalesReturnInvoiceMasterById(int id);
        Task<SalesReturnInvoiceMaster> GetSalesReturnInvoiceMasterById(int id);
        Task<IEnumerable<SalesReturnInvoiceMaster>> GetAllDueSalesReturnInvoiceMaster();
        Task<bool> UpdateSalesInvoiceReturnMasterStockCloseById(int id);
        Task<CustomerCollectionReportVM> GetSalesReturnInvoiceRecipt(int customerId, string fromDate, string toDate);
        Task<LeadAutoNumber> GetAutoRentServiceInvoiceNoBySp(string invoiceDate);
        #endregion

        #region Terms
        Task<int> SaveSalesTerms(SalesTermsConditions salesInvoiceMaster);
        Task<SalesTermsConditions> GetTermsConditionsById(int id);
        Task<IEnumerable<SalesTermsConditions>> GetTermsConditionsByMasterId(int id);
        Task<bool> DeleteTermsConditionsById(int id);
        Task<bool> DeleteTermsConditionsByMasterId(int id);
        #endregion
    }
}
