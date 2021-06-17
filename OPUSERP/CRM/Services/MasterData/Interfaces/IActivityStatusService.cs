using OPUSERP.CRM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData.Interfaces
{
    public interface IActivityStatusService
    {
        Task<bool> SaveActivityStatus(ActivityStatus activityStatus);
        Task<IEnumerable<ActivityStatus>> GetAllActivityStatus();
        Task<ActivityStatus> GetActivityStatusById(int id);
        Task<bool> DeleteActivityStatusById(int id);

    }
}
