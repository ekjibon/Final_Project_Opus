using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Areas.OtherSales.Models.NotMapped;
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
    public interface IOSalesCollectionService
    {
        Task<int> SaveSalesCollection(SalesCollection salesCollection);
        Task<IEnumerable<SalesCollection>> GetAllSalesCollection();
        Task<IEnumerable<SalesCollection>> GetSalesCollectionListById(int id);
        Task<SalesCollection> GetSalesCollectionById(int id);
        Task<bool> DeleteSalesCollectionById(int id);

        Task<IEnumerable<CustomerWithDue>> GetCustomerWithDue();
        Task<IEnumerable<RelSupplierCustomerResourse>> GetDuesCustomerList();
        Task<IEnumerable<RelSupplierCustomerResourse>> GetCollectionCustomerList();
        Task<BillReturnPaymentMaster> GetSalesReturnPaymentById(int id);
        Task<IEnumerable<RelSupplierCustomerResourse>> GetReturnCustomerList();
        Task<IEnumerable<RelSupplierCustomerResourse>> GetCustomerListForSalesReport();
        Task<IEnumerable<VoucherMaster>> GetSalesVoucherApproveList();
        Task<IEnumerable<VoucherMaster>> GetPurchaseVoucherApproveList();
        Task<bool> UpdateVoucherMasterisPostedById(int masterId, int status);
        Task<IEnumerable<SalesCollection>> GetSalesCollectionForManualVoucher();
        Task<IEnumerable<PosCollectionMaster>> GetPosSalesCollectionForManualVoucher();

        Task<IEnumerable<PosCollectionMaster>> GetPosSalesCollectionForManualVoucherForCard();
        Task<IEnumerable<PosCollectionMaster>> GetPosSalesCollectionForManualVoucherForMobile();
        Task<IEnumerable<PosCollectionMaster>> GetPosSalesCollectionForVoucherForCardReport(int id, string fromDate, string toDate);
        Task<IEnumerable<PosCollectionMaster>> GetPosSalesCollectionForVoucherForMobileReport(int id, string fromDate, string toDate);

        Task<CollectionSummaryReport> GetSummaryCollection(int id);

        Task<int> SaveSalesCollectionBillInfo(SalesCollectionBillInfo salesCollection);
        Task<bool> UpdateSalesCollectionBillInfoById(int id);
        Task<IEnumerable<SalesCollectionBillInfo>> GetSalesCollectionBillInfo();
        Task<IEnumerable<SalesCollectionBillInfo>> GetSalesCollectionBillInfoByInvoiceId(int Id);
        Task<SalesCollectionBillInfo> GetSalesCollectionBillInfoById(int Id);
        Task<IEnumerable<SalesCollectionBillInfo>> GetSalesCollectionBillInfoByCustomerId(int Id);
    }
}
