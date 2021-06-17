using OPUSERP.CRM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData.Interfaces
{
    public interface IActivityTypeService
    {
        Task<bool> SaveActivityType(ActivityType activityType);
        Task<IEnumerable<ActivityType>> GetAllActivityType();
        Task<ActivityType> GetActivityTypeById(int id);
        Task<bool> DeleteActivityTypeById(int id);

    }
}
