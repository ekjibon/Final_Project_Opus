using OPUSERP.CRM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData.Interfaces
{
    public interface IOwnershipService
    {
        // Ownership
        Task<bool> SaveOwnership(OwnerShipType ownerShipType);
        Task<IEnumerable<OwnerShipType>> GetAllOwnership();
        Task<OwnerShipType> GetOwnershipById(int id);
        Task<bool> DeleteOwnershipById(int id);
    }
}
