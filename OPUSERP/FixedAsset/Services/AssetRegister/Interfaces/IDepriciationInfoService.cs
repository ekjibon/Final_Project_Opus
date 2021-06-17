using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.FixedAsset.Services.AssetRegister.Interfaces
{
    public interface IDepriciationInfoService
    {
        Task<bool> SaveDepriciationInfo(DepriciationInfo depriciationInfo);
        Task<IEnumerable<DepriciationInfo>> GetAllDepriciationInfo();
        Task<DepriciationInfo> GetDepriciationInfoById(int id);
        Task<bool> DeleteDepriciationInfoById(int id);
        Task<bool> DeleteDepriciationInfoByAssetId(int id);
        Task<DepriciationInfo> GetAllDepriciationInfobyassetId(int Id);
    }
}
