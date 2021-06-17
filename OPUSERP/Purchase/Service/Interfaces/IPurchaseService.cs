
using OPUSERP.Areas.POS.Models;
using OPUSERP.Areas.Purchase.Models.NotMapped;
using OPUSERP.Areas.SCMStock.Models;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.Purchase.Data.Entity;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Purchase.Services.Interfaces
{
    public interface IPurchaseService
    {
        #region Purchase Order Master
        Task<int> SavePurchaseOrderMaster(PurchaseOrdersMaster purchaseOrderMaster);
        Task<PurchaseOrdersMaster> GetPurchaseOrderMasterById(int Id);
        Task<IEnumerable<PurchaseOrdersMaster>> GetPurchaseOrderMaster();
        Task<IEnumerable<SalesInvoiceMaster>> GetDueStockPymentInvoiceList();
        Task<IEnumerable<Areas.POS.Models.StockItemViewModel>> GetDueStockoutDetailsInvoiceList(int Id);
        //Task<IEnumerable<PurchaseInvoiceWithDue>> GetDuePurchaseInvoiceByCustomerId(int customerId);
        Task<IEnumerable<PurchaseOrdersMaster>> GetDueStockPurchaseInvoiceList();     
        IQueryable<PurchaseOrdersMaster> GetPurchaseOrderMasterByUser(string userName);
        Task<PurchaseOrdersMaster> GetGetvoucherMasterById(int id);
        Task<bool> DeletePurchaseOrderMasterById(int id);
        Task<bool> UpdatePurchaseOrderMasterById(int id);
        Task<bool> UpdatePurchaseOrderMasterStockCloseById(int id);
        Task<IEnumerable<PurchaseOrdersMaster>> GetPurchaseOrderMasterBySupplierId(int Id);
        #endregion

        #region Purchase Order Details

        Task<int> SavePurchaseOrderDetail(PurchaseOrdersDetail purchaseOrderDetail);
        IQueryable<PurchaseOrdersDetail> GetPurchaseOrderDetailByPOId(int poId);
        Task<PurchaseOrdersDetail> GetPurchaseOrderDetailById(int poId);
        Task<IEnumerable<Areas.POS.Models.StockItemViewModel>> GetDueStockPurchaseDetailsInvoiceList(int Id);
        Task<bool> DeletePurchaseOrderDetailByPOId(int poId);       
        Task<PurchaseOrdersDetail> GetPurchaseOrderDetailBySpecId(int specId);       

        #endregion

        #region Delivery Location
        Task<int> SaveDeliveryLoacation(DeliveryLocation deliveryLoacation);
        Task<IEnumerable<DeliveryLocation>> GetDeliveryLoacation();
        Task<bool> DeleteDeliveryLoacationById(int poId);
        #endregion

        Task<IEnumerable<Areas.POS.Models.StockItemViewModel>> GetReturnDetailsInvoiceList(int Id);
        Task<IEnumerable<BillReceiveInformation>> GetDueBillReceiveByCustomerId(int customerId);
        



    }
}
