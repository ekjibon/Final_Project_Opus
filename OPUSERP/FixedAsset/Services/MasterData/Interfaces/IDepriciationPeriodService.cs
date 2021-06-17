using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.FixedAsset.Data.Entity.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.FixedAsset.Services.MasterData.Interfaces
{
    public interface IDepriciationPeriodService
    {
        #region Depriciation Period
        Task<bool> SaveDepriciationPeriod(DepriciationPeriod depriciationPeriod);
        Task<IEnumerable<DepriciationPeriod>> GetAllDepriciationPeriod();
        Task<DepriciationPeriod> GetDepriciationPeriodById(int id);
        Task<bool> DeleteDepriciationPeriodById(int id);
        #endregion

        #region Asset Year
        Task<int> SaveAssetYear(AssetYear assetYear);
        Task<IEnumerable<AssetYear>> GetAllAssetYear();
        Task<AssetYear> GetAssetYearById(int id);
        Task<bool> DeleteAssetYearById(int id);
        #endregion
    }
}
