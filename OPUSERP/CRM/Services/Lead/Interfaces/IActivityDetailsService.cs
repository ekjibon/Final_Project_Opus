using OPUSERP.CRM.Data.Entity.Lead;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Lead.Interfaces
{
    public interface IActivityDetailsService
    {
        Task<int> SaveActivityDetails(ActivityDetails ActivityDetails);
        Task<IEnumerable<ActivityDetails>> GetAllActivityDetailsByActivityMasterId(int id);
        Task<ActivityDetails> GetActivityDetailsById(int id);
        Task<bool> DeleteActivityDetailsById(int id);
        Task<bool> DeleteActivityDetailsByMasterId(int id);
    }
}
