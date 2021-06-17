
using OPUSERP.Sales.Data.Entity.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Sales.Services.Sales.Interfaces
{
   public interface ISalesInvoiceDetailService
    {
        #region SaleDetail
        Task<int> SaveSalesInvoiceDetail(SalesInvoiceDetail salesInvoiceDetail);
        Task<IEnumerable<SalesInvoiceDetail>> GetAllSalesInvoiceDetail();
        Task<SalesInvoiceDetail> GetSalesInvoiceDetailById(int id);
        Task<bool> DeleteSalesInvoiceDetailById(int id);

        Task<IEnumerable<SalesInvoiceDetail>> GetAllSalesInvoiceDetailByMasterId(int masterId);
        Task<bool> DeleteSalesInvoiceDetailByMasterId(int masterId);
        #endregion
        #region RepSalesDetail
        Task<bool> SaveRepSalesInvoiceDetail(RepSalesInvoiceDetail salesInvoiceDetail);
        Task<IEnumerable<RepSalesInvoiceDetail>> GetAllRepSalesInvoiceDetail();
        Task<IEnumerable<RepSalesInvoiceDetail>> GetAllRepSalesInvoiceDetailByMasterId(int masterId);
        Task<RepSalesInvoiceDetail> GetRepSalesInvoiceDetailById(int id);
        Task<bool> DeleteRepSalesInvoiceDetailById(int id);
        Task<bool> DeleteRepSalesInvoiceDetailByMasterId(int masterId);
        #endregion

    }
}
