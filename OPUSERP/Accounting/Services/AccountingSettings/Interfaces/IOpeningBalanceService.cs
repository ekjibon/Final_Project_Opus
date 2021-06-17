using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Services.AccountingSettings.Interfaces
{
    public interface IOpeningBalanceService
    {
        Task<int> SaveopeningBalance(OpeningBalance openingBalance);
        Task<IEnumerable<OpeningBalance>> GetOpeningBalance();
        Task<OpeningBalance> GetopeningBalanceById(int id);
        Task<IEnumerable<OpeningBalance>> GetOpeningBalancebyLedgerRelId(int id);
        Task<bool> DeleteopeningBalanceById(int id);
    }
}
