using OPUSERP.Areas.REMS.Models;
using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using OPUSERP.REMS.Data.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.REMS.Services.Interfaces
{
    public interface IClaimRegisterService
    {
        Task<int> SaveClaimRegister(ClaimRegister claim);
        void UpdateClaimRegister(int id, int status);
        Task<IEnumerable<ClaimRegister>> GetClaimRegisterList(int userId);
        Task<ClaimRegister> GetClaimRegisterById(int id);
        Task<IEnumerable<RegisterAssetViewModel>> GetAssetRegisterByUserId(int userId);
        Task<IEnumerable<AssetRegistration>> GetAssetRegistration();
        Task<AssetWarrenty> GetAssetWarrenty(int id);
        Task<AssetAssign> GetAssetAssign(int id);
        Task<IEnumerable<ClaimRegister>> GetClaimRegisterListByStatus(int status);
    }
}
