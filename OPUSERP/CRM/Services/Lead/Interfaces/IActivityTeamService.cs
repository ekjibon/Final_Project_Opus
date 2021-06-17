using OPUSERP.CRM.Data.Entity.Lead;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Lead.Interfaces
{
    public interface IActivityTeamService
    {
        Task<int> SaveActivityTeam(ActivityTeam activityTeam);
        Task<IEnumerable<ActivityTeam>> GetAllActivityTeamByActivityMasterId(int id);
        Task<ActivityTeam> GetActivityTeamById(int id);
        Task<bool> DeleteActivityTeamById(int id);
        Task<bool> DeleteActivityTeamByMasterId(int id);
    }
}
