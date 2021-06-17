using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Services.AccountingSettings.Interfaces
{
    public interface IAccountModeService
    {
        Task<int> SaveaccountMode(AccountMode accountMode);
        Task<IEnumerable<AccountMode>> GetaccountMode();
        Task<AccountMode> GetaccountModeById(int id);
        Task<bool> DeleteaccountModeById(int id);
    }
}
