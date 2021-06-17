using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.REMS.Data.Entity;
using OPUSERP.REMS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.REMS.Services
{
    public class RepairTransactionLogService : IRepairTransactionLogService
    {
        private readonly ERPDbContext _context;

        public RepairTransactionLogService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveRepairTransactionLog(RepairTransactionLog repairTransactionLog)
        {
            if (repairTransactionLog.Id != 0)
            {
                _context.RepairTransactionLogs.Update(repairTransactionLog);
            }
            else
            {
                _context.RepairTransactionLogs.Add(repairTransactionLog);
            }

            await _context.SaveChangesAsync();
            return repairTransactionLog.Id;
        }

        public async Task<IEnumerable<RepairTransactionLog>> GetRepairTransactionLog()
        {
            return await _context.RepairTransactionLogs
                .OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<RepairTransactionLog>> GetRepairTransactionLogByClaim(int claimId)
        {
            return await _context.RepairTransactionLogs.Include(x=>x.claim.assetRegistration.itemSpecification.Item).Include(x=>x.statusInfo).Where(x=>x.claimId==claimId)
                .OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }
    }
}
