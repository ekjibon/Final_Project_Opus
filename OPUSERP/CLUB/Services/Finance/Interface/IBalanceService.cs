using OPUSERP.CLUB.Data.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Finance.Interface
{
   public interface IBalanceService
    {
        Task<bool> SaveBalance(Balance balance);
        Task<IEnumerable<Balance>> GetBalance();
        Task<Balance> GetBalanceById(int id);
        Task<bool> DeleteBalanceById(int id);
    }
}
