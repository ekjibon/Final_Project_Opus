using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.FixedAsset.Services.AssetRegister.Interfaces
{
    public interface IAssetWarrentyService
    {
        Task<bool> SaveAssetWarrenty(AssetWarrenty assetWarrenty);
        Task<IEnumerable<AssetWarrenty>> GetAllAssetWarrenty();
        Task<AssetWarrenty> GetAssetWarrentyById(int id);
        Task<bool> DeleteAssetWarrentyById(int id);
        Task<bool> DeleteAssetWarrentyByassetId(int id);
        Task<IEnumerable<AssetWarrenty>> GetAllAssetWarrentybyassetId(int Id);
    }
}
