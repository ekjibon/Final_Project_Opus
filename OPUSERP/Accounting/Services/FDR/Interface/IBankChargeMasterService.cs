using OPUSERP.Accounting.Data.Entity.FDR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Services.FDR.Interface
{
   public interface IBankChargeMasterService
    {
        Task<int> SaveBankChargeMaster(BankChargeMaster bankChargeMaster);
        Task<IEnumerable<BankChargeMaster>> GetBankChargeMaster();
        Task<BankChargeMaster> GetBankChargeMasterById(int id);
        Task<bool> DeleteBankChargeMasterById(int id);
    }
}
