using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Inventory;

namespace OPUSERP.VMS.Services.Inventory.interfaces
{
    public interface IPurchasePartsService
    {
        #region Purchase Parts Master
        Task<int> SavePurchasePartsMaster(PurchasePartsMaster purchasePartsMaster);
        Task<IEnumerable<PurchasePartsMaster>> GetAllPurchasePartsMaster();
        Task<PurchasePartsMaster> GetPurchasePartsMasterById(int id);
        IQueryable<PurchasePartsMaster> GetPurchasePartsMasterByPartsId(int partsId);
        Task<bool> DeletePurchasePartsMasterById(int id);
        #endregion

        #region Purchase Parts Details

        Task<int> SavePurchasePartsDetails(PurchasePartsDetail purchasePartsDetail);
        Task<IEnumerable<PurchasePartsDetail>> GetAllPurchasePartsDetails();
        Task<PurchasePartsDetail> GetPurchasePartsDetailsById(int id);
        IQueryable<PurchasePartsDetail> GetPurchasePartsDetailsByMasterId(int partsId);
        IQueryable<PurchasePartsDetail> GetUnUsedPurchasePartsDetailsByMasterId(int partsId);
        Task<bool> DeletePurchasePartsDetailsByMasterId(int masterId);

        #endregion
    }
}
