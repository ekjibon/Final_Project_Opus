using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using OPUSERP.FixedAsset.Data.Entity.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.FixedAsset.Services.AssetRegister.Interfaces
{
    public interface IAssetRegistrationService
    {
        #region  Asset Registration

        Task<int> SaveAssetRegistration(AssetRegistration assetRegistration);
        Task<IEnumerable<AssetRegistration>> GetAllAssetRegistration();
        Task<AssetRegistration> GetAssetRegistrationById(int id);
        Task<bool> DeleteAssetRegistrationById(int id);
        Task<bool> DeleteAssetRegistrationByPurchaseId(int id);
        Task<IEnumerable<AssetRegistration>> GetAllAssetRegistrationbycatId(int Id);
        Task<IEnumerable<AssetRegistration>> GetAllAssetRegistrationbypurchaseId(int Id);
        Task<IEnumerable<AssetRegistration>> GetAllAssetRegistrationbycatIdForDepreciation(int Id);
        Task<IEnumerable<AssetRegistration>> GetAssetRegistrationBypurchaseMasterId(int id);

        #endregion

        #region  Asset Overhauling

        Task<int> SaveAssetOverhauling(AssetOverhauling assetOverhauling);
        Task<IEnumerable<AssetOverhauling>> GetAllAssetOverhauling();
        Task<bool> DeleteAssetOverhaulingById(int id);

        #endregion
    }
}
