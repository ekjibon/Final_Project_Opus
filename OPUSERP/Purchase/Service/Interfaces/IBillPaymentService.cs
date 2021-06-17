using OPUSERP.Areas.Purchase.Models.NotMapped;
using OPUSERP.Purchase.Data.Entity;
using OPUSERP.SCM.Data.Entity.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Purchase.Service.Interfaces
{
    public interface IBillPaymentService
    {
        #region BillPaymentMaster
        Task<int> SaveBillPaymentMaster(BillPaymentMaster billPaymentMaster);
        //Task<int> SaveReturnBillPaymentMaster(BillReturnPaymentMaster billPaymentMaster);
        Task<IEnumerable<BillPaymentMaster>> GetBillPaymentMaster();
        //Task<IEnumerable<BillReturnPaymentMaster>> GetBillReturnPaymentMaster();
        //Task<BillReturnPaymentMaster> GetBillReturnPaymentbyMasterId(int Id);
        Task<BillPaymentMaster> GetBillPaymentMasterById(int Id);
        Task<bool> DeleteBillPaymentMasterById(int id);

        Task<IEnumerable<SupplierWithDue>> GetSupplierWithDue();
        //Task<IEnumerable<SupplierWithDue>> GetCustomerReturnWithDue();
        Task<IEnumerable<Organization>> GetDueSupplierList();
        //Task<IEnumerable<RelSupplierCustomerResourse>> GetReturnDueCustomerList();
        //Task<IEnumerable<RelSupplierCustomerResourse>> GetSupplierListForPurchaseReport();
        #endregion

        #region BillPaymentDetail
        Task<int> SaveBillPaymentDetail(BillPaymentDetail billPaymentDetail);
        //Task<int> SaveBillReturnPaymentDetail(BillReturnPaymentDetail billPaymentDetail);
        Task<IEnumerable<BillPaymentDetail>> GetBillPaymentDetailByMasterId(int MasterId);
        Task<BillPaymentDetail> GetBillPaymentDetailById(int Id);
        Task<bool> DeleteBillPaymentDetailById(int id);
        Task<bool> DeleteBillPaymentDetailByTMId(int TMId);

        //Task<CustomerCollectionReportVM> GetBillPaymentRecipt(int customerId, string fromDate, string toDate);
        //Task<IEnumerable<RelSupplierCustomerResourse>> SupplierListforBillReport();
        #endregion

        #region BillReceiveInformation
        Task<int> SaveBillReceiveInformation(BillReceiveInformation billPaymentMaster);
        Task<IEnumerable<BillReceiveInformation>> GetBillReceiveInformation();
        Task<IEnumerable<BillReceiveInformation>> GetBillReceiveInformationByInvoiceId(int Id);
        Task<BillReceiveInformation> GetBillReceiveInformationById(int Id);
        Task<bool> UpdateBillReceiveInformationById(int id);
        #endregion

        #region purchase Return
        Task<int> SavePurchaseReturnInfoMaster(PurchaseReturnInfoMaster billPaymentMaster);
        Task<PurchaseReturnInfoMaster> GetPurchaseReturnInfoMasterById(int Id);
        Task<IEnumerable<PurchaseReturnInfoMaster>> GetPurchaseReturnInfoMaster();

        Task<int> SavePurchaseReturnInfoDetail(PurchaseReturnInfoDetail billPaymentMaster);
        Task<IEnumerable<PurchaseReturnInfoDetail>> GetPurchaseReturnInfoDetailByMasterId(int Id);
        #endregion
    }
}
