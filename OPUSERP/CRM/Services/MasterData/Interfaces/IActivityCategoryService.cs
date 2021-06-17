using OPUSERP.CRM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData.Interfaces
{
    public interface IActivityCategoryService
    {
        Task<bool> SaveActivityCategory(ActivityCategory activityCategory);
        Task<IEnumerable<ActivityCategory>> GetAllActivityCategory();
        Task<ActivityCategory> GetActivityCategoryById(int id);
        Task<bool> DeleteActivityCategoryById(int id);
    }
}
