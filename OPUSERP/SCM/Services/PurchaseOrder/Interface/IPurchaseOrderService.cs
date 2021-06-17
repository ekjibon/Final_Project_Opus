using OPUSERP.Areas.Purchase.Models.NotMapped;
using OPUSERP.Areas.SCMPurchaseOrder.Models;
using OPUSERP.Models;
using OPUSERP.POS.Data.Entity;
using OPUSERP.SCM.Data.Entity.Matrix;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.PurchaseOrder.Interface
{
   public interface IPurchaseOrderService
    {
        #region Master data Get
        Task<IEnumerable<DeliveryMode>> GetDeliveryMode();
        Task<IEnumerable<DeliveryLocation>> GetDeliveryLocation();
        Task<IEnumerable<PaymentMode>> GetPaymentMode();
        Task<IEnumerable<PaymentTerms>> GetpaymentTerms();
        Task<IEnumerable<Currency>> GetCurrency();
        Task<IEnumerable<CardType>> GetAllCardType();
        Task<decimal> GetDiscountRateForSales();
        #endregion

        #region PurchaseOrder Master
        AutoNumberViewModel GetPuerchaseOrderNo(int reqId);
        Task<int> SavePurchaseOrderMaster(PurchaseOrderMaster purchaseOrderMaster);
        Task<bool> UpdatePurchaseOrderMasterStockCloseById(int id);
        Task<IEnumerable<PurchaseOrderMaster>> GetPurchaseOrderMasterForGR(string userName);
        Task<IEnumerable<PurchaseOrderMaster>> GetPurchaseOrderMaster(string userId);
        Task<IEnumerable<PurchaseOrderMaster>> GetIssuedPurchaseOrderMaster(string userId);
        Task<PurchaseOrderMaster> GetPurchaseOrderMasterById(int id);
        Task<IEnumerable<PurchaseOrderMaster>> GetPurchaseOrderMasterAll();
        Task<IEnumerable<PurchaseOrderMaster>> GetDueStockPurchaseInvoiceList();
        Task<bool> DeletePurchaseOrderMasterById(int id);
        #endregion

        #region PurchaseOrder Details
        Task<int> SavePurchaseOrderDetails(PurchaseOrderDetails purchaseOrderDetails);
        Task<IEnumerable<PurchaseOrderDetails>> GetPurchaseOrderDetails();
        Task<PurchaseOrderDetails> GetPurchaseOrderDetailsById(int id);
        IQueryable<PurchaseOrderDetails> GetPurchaseOrderDetailByPOId(int poId);
        Task<IEnumerable<PurchaseOrderDetails>> GetPurchaseOrderDetailsByMasterId(int MasterId);
        Task<PurchaseOrderDetails> GetPurchaseOrderDetailById(int poId);
        Task<bool> DeletePurchaseOrderDetailsById(int id);
        Task<bool> DeletePurchaseOrderDetailsByMasterId(int id);
        Task<bool> DeletePurchaseOrderDetailsByPurchaseId(int purchaseId);
        Task<IEnumerable<PurchaseOrderRPTViewModel>> GetRPTPurchaseOrderDetailsByMasterId(int poId);
        Task<IEnumerable<PurchaseOrderDetails>> GetPurchaseOrderDetailsbyspecId(int Id);
        Task<IEnumerable<PurchaseOrderDetails>> GetPurchaseOrderDetailsbyspecIdA(int Id);
        Task<IEnumerable<Areas.POS.Models.StockItemViewModel>> GetDueStockPurchaseDetailsInvoiceList(int Id);
        Task<IEnumerable<PurchaseInvoiceWithDue>> GetDuePurchaseInvoiceByCustomerId(int customerId);
        #endregion

        #region PO Term&Condition
        Task<int> SavePOTermAndCondition(POTermAndCondition pOTermAndCondition);
        Task<IEnumerable<POTermAndCondition>> GetPOTermAndCondition();
        Task<POTermAndCondition> GetPOTermAndConditionById(int id);
        Task<bool> DeletePOTermAndConditionById(int id);
        Task<bool> DeletePOTermAndConditionByMasterId(int id);
        Task<IEnumerable<POTermAndCondition>> GetPOTermAndConditionByMasterId(int id);
        #endregion
              

        #region PrintHistory
        Task<bool> SavePintHistory(PrintHistory printHistory);
        Task<PrintHistory> GetPrintHistoryById(int matrixTypeId,int id);
        #endregion
    }
}
