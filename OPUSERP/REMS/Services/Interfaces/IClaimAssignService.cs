using OPUSERP.Areas.REMS.Models;
using OPUSERP.REMS.Data.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.REMS.Services.Interfaces
{
    public interface IClaimAssignService
    {
        Task<int> SaveClaimAssign(ClaimAssign claimAssign);
        Task<bool> DeleteClaimAssignById(int id);
        Task<IEnumerable<ClaimAssign>> GetClaimAssign();
        Task<IEnumerable<ClaimAssign>> GetClaimAssignByUserId(int assignUserId);
        Task<IEnumerable<ClaimAssignViewModel>> GetClaimAssignListByAssignType(int userId, int assignTypeId,int statusId);
        Task<IEnumerable<ClaimAssignViewModel>> GetClaimAssignListByAssignUser(int userId, int assignTypeId);
        Task<IEnumerable<ClaimAssignViewModel>> GetClaimAssignListByAssignTypeNew(int userId, int assignTypeId, int statusId);
    }
}
