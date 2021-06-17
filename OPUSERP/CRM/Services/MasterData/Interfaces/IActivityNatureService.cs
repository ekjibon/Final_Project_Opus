using OPUSERP.CRM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData.Interfaces
{
    public interface IActivityNatureService
    {
        Task<bool> SaveActivityNature(ActivityNature activityNature);
        Task<IEnumerable<ActivityNature>> GetAllActivityNature();
        Task<ActivityNature> GetActivityNatureById(int id);
        Task<bool> DeleteActivityNatureById(int id);

    }
}
