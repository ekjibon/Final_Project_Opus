using OPUSERP.CRM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData.Interfaces
{
    public interface IActivitySessionService
    {
        Task<bool> SaveActivitySession(ActivitySession activitySession);
        Task<IEnumerable<ActivitySession>> GetAllActivitySession();
        Task<ActivitySession> GetActivitySessionById(int id);
        Task<bool> DeleteActivitySessionById(int id);

    }
}
