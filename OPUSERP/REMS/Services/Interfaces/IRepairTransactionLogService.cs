using OPUSERP.REMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.REMS.Services.Interfaces
{
   public interface IRepairTransactionLogService
    {
        Task<int> SaveRepairTransactionLog(RepairTransactionLog repairTransactionLog);
        Task<IEnumerable<RepairTransactionLog>> GetRepairTransactionLog();
        Task<IEnumerable<RepairTransactionLog>> GetRepairTransactionLogByClaim(int claimId);
    }
}
