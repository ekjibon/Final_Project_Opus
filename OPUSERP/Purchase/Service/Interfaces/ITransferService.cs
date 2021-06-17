using OPUSERP.Areas.POS.Models;
using OPUSERP.Purchase.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Purchase.Service.Interfaces
{
   public interface ITransferService
    {
        #region TransferMaster
        Task<int> SaveTransferMaster(TransferMaster transferMaster);
        Task<IEnumerable<TransferMaster>> GetAllTransferMaster();
        Task<IEnumerable<TransferMaster>> GetAllTransferMasterByUser(string Username);
        Task<TransferMaster> GetAllTransferMasterByMasterId(int Id);
        Task<bool> DeleteTransferMasterById(int id);
        Task<IEnumerable<TransferMaster>> GetAllTransferMasterbyStoreId();
        Task<bool> UpdateTransferMasterStockCloseById(int id);
        Task<bool> UpdateTransferMasterStockOpenById(int id);
        #endregion

        #region Transfer Detail
        Task<int> SaveTransferDetail(TransferDetail transferDetail);
        IQueryable<TransferDetail> GetAllTransferDetailsBytransferMasterId(int transferMasterId);
        Task<bool> DeleteTransferDetailById(int id);
        Task<bool> DeleteTransferDetailsBytransferMasterId(int transferMasterId);
        Task<IEnumerable<StockItemViewModel>> GetDueStockTransferDetailsInvoiceList(int Id);
        Task<TransferDetail> GetAllTransferDetailsById(int Id);
        #endregion
    }
}
