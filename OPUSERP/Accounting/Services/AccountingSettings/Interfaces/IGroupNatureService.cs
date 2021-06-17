using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Services.AccountingSettings.Interfaces
{
    public interface IGroupNatureService
    {
        Task<int>SavegroupNature(GroupNature groupNature);
        Task<IEnumerable<GroupNature>> GetgroupNature();
        Task<GroupNature> GetgroupNatureById(int id);
        Task<bool> DeletegroupNatureById(int id);
    }
}
