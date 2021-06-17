using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.FixedAsset.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.FixedAsset.Services.MasterData.Interfaces
{
    public interface IDepriciationRateService
    {
        Task<bool> SaveDepriciationRate(DepriciationRate depriciationRate);
        Task<IEnumerable<DepriciationRate>> GetAllDepriciationRate();
        Task<DepriciationRate> GetDepriciationRateById(int id);
        Task<bool> DeleteDepriciationRateById(int id);
        Task<DepriciationRate> GetDepriciationRateByCatId(int id);
        Task<IEnumerable<ItemCategory>> GetItemCategoryForDepRate();
    }
}
