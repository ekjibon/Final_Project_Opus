using OPUSERP.Areas.OtherSales.Models;
using OPUSERP.Areas.POS.Models;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.POS.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.OtherSales.Services.Sales.Interfaces
{
    public interface IOSalesInvoiceDetailService
    {
        #region ReturnSaleDetail
        Task<bool> SaveSalesInvoiceDetail(SalesInvoiceDetail salesInvoiceDetail);
        Task<IEnumerable<SalesInvoiceDetail>> GetAllSalesInvoiceDetail();
        Task<SalesInvoiceDetail> GetSalesInvoiceDetailById(int id);
        Task<bool> DeleteSalesInvoiceDetailById(int id);
        Task<IEnumerable<SalesInvoiceDetail>> GetAllSalesInvoicelistbydaterange(DateTime? fromDate, DateTime? toDate);
        Task<IEnumerable<SalesInvoiceDetail>> GetAllSalesInvoicelistBydaterangeAndUser(DateTime? fromDate, DateTime? toDate, string userName);
        Task<IEnumerable<POSInvoiceListViewModel>> GetPosInvoiceList(string FDate, string TDate);
        Task<IEnumerable<SalesInvoiceDetail>> GetAllSalesInvoiceDetailByMasterId(int masterId);
        Task<bool> DeleteSalesInvoiceDetailByMasterId(int masterId);
        #endregion
        #region ReturnSaleDetail
        Task<bool> SaveSalesReturnInvoiceDetail(SalesReturnInvoiceDetail salesReturnInvoiceDetail);
        Task<bool> DeleteSalesReturnInvoiceDetailByMasterId(int masterId);
        Task<IEnumerable<SalesReturnInvoiceDetail>> GetAllSalesReturnInvoiceDetailBySaleMaster(int MasterId);
        Task<IEnumerable<SalesReturnInvoiceDetail>> GetAllSalesReturnInvoiceDetailByMasterId(int masterId);
        Task<SalesReturnInvoiceDetail> GetSalesReturnInvoiceDetailById(int id);
        #endregion
    }
}
