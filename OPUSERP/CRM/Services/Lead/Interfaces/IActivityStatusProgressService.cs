using OPUSERP.CRM.Data.Entity.Lead;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Lead.Interfaces
{
    public interface IActivityStatusProgressService
    {
        Task<int> SaveActivityStatusProgress(ActivityStatusProgress activityStatusProgress);
        Task<IEnumerable<ActivityStatusProgress>> GetAllActivityStatusProgressByActivityMasterId(int id);

        Task<bool> DeleteActivityStatusProgressById(int id);
        Task<bool> DeleteActivityStatusProgressByMasterId(int id);
        Task<IEnumerable<ActivityStatusProgress>> GetAllActivityStatusProgressByLeadId(int id);
    }
}
