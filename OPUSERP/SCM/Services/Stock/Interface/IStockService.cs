using OPUSERP.Areas.Inventory.Models;
using OPUSERP.Areas.SCMStock.Models;
using OPUSERP.SCM.Data.Entity.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Stock.Interface
{
   public interface IStockService
    {
        #region Stock Master

        Task<IEnumerable<StockItemViewModel>> GetDueStockPurchaseDetailsInvoiceList(int Id);
        Task<IEnumerable<StockItemViewModel>> GetDueStockIOUDetailsInvoiceList(int Id);
        Task<int> SaveStockMaster(StockMaster stockMaster);
        Task<IEnumerable<StockMaster>> GetStockMaster();
        Task<string> GetGRNumber();
        Task<IEnumerable<StockMaster>> GetStockMasterByReceiveType(string type, string userName);
        Task<IEnumerable<StockBalanceViewModel>> InventoryReport(int? projectId, DateTime? fromDate, DateTime? toDatee);
        Task<StockMaster> GetStockMasterById(int id);
        Task<bool> DeletestockByPOId(int poId);
        Task<bool> DeletestockByStockMasterId(int stockMasterId);
        Task<IEnumerable<StockMaster>> GetStockMasterbyType(int type);
        Task<IEnumerable<StockMaster>> GetStockMasterbyTypeForProd(int type, int? storeId);
        Task<StockMaster> GetStockInMasterById(int Id);
        Task<StockMaster> GetStockOutMasterById(int Id);
        Task<StockMaster> GetStockOutForProductionMasterById(int Id);
        Task<IEnumerable<StockMaster>> GetAllStockMaster(int StatusId);

        #endregion

        #region Stock Details

        Task<int> SaveStock(StockDetails stock);
        Task<IEnumerable<StockDetails>> GetStockDetails();
        Task<IEnumerable<StockDetails>> GetStockDetailsByMasterId(int id);
        Task<StockDetails> GetstockMasterIdBytransferMasterId(int Id);
        Task<StockDetails> GetStockBymasterId(int masterId);
        Task<bool> DeleteStockMasterById(int id);
        Task<bool> DeleteStockMasterByMasterId(int id);
        Task<StockDetails> GetstockMasterByitemSpecificationId(int itemSpecificationId);
        Task<decimal> OnHanStockBySpecIdForTransfer(int id);
        Task<IEnumerable<StockDetails>> GetAllStockInByPurchaseMaster(int Id);
        Task<IEnumerable<StockDetails>> GetAllStockByMasterId(int Id);
        Task<IEnumerable<StockDetails>> GetAllStockOutBySalesMaster(int Id);
        Task<IEnumerable<StockDetails>> GetAllStockForProductionReqByMasterId(int Id);
        Task<IEnumerable<StockDetails>> GetStockDetailsbyspecid(int Id);
        Task<bool> UpdatesingleStock(int id, int masterId, decimal qty);
        Task<StockDetails> GetStockDetailsById(int Id);
        Task<IEnumerable<StockBalancesViewModel>> GetStockBalanceViewModels(int tid, int? itemspecid, DateTime? fromdate, DateTime? todate);
        Task<IEnumerable<StockSummaryViewModel>> GetStockSummary(string FDate, string TDate);
        Task<IEnumerable<StockBalanceByItemViewModel>> GetStockBalanceByItem(int? itemspecid);
        #endregion

        #region reqitem
        Task<IEnumerable<ItemReqMaster>> GettemReqMaster();
        Task<ItemReqMaster> GettemReqMasterbyId(int Id);
        Task<bool> DeleteItemReqMasterbyId(int Id);
        Task<int> SaveItemReqMaster(ItemReqMaster stockMaster);
        #endregion

        #region reqitem detail
        Task<IEnumerable<ItemReqDetails>> GettemReqDetail();
        Task<IEnumerable<ItemReqDetails>> GettemReqDetailbymasterId(int Id);
        Task<ItemReqDetails> GettemReqDetailbyId(int Id);
        Task<bool> DeleteItemReqDetailbyId(int Id);
        Task<int> SaveItemReqDetail(ItemReqDetails stockMaster);
        Task<bool> DeleteItemReqDetailbymasterId(int Id);
        #endregion

        #region Opening Stock

        Task<int> SaveOpeningStock(OpeningStock stock);
        Task<IEnumerable<OpeningStock>> GetOpeningStockAll();
        Task<OpeningStock> GetOpeningStockById(int id);
        Task<bool> DeleteOpeningStockById(int id);

        #endregion

    }
}
