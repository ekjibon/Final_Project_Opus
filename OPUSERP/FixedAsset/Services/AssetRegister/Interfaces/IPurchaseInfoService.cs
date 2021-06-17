using OPUSERP.Areas.FAMSAssetRegister.Models;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using OPUSERP.FixedAsset.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.FixedAsset.Services.AssetRegister.Interfaces
{
    public interface IPurchaseInfoService
    {
        Task<int> SavePurchaseInfo(PurchaseInfo purchaseInfo);
        Task<IEnumerable<PurchaseInfo>> GetAllPurchaseInfo();
        Task<PurchaseInfo> GetPurchaseInfoById(int id);
        Task<bool> DeletePurchaseInfoById(int id);
        Task<IEnumerable<PurchaseOrderViewModel>> GetAllPurchaseInfofromScm();
        Task<IEnumerable<ItemSpecification>> GetAllItemfromScm(int Id);
        Task<StockViewModel> GetStockViewModel(int Id, int purchaseId);
        Task<IEnumerable<StockViewModel>> GetAllItemfromScmS(int Id);
        Task<IEnumerable<PurchaseOrderViewModel>> GetAllPurchaseInfofromScmAfter();
    }
}
