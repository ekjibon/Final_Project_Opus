using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.SCMMasterData.Models;
using OPUSERP.Data;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.SCM.Data;
using OPUSERP.SCM.Services.Matrix.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Matrix
{
    public class StatusLogService: IStatusLogService
    {
        private readonly ERPDbContext _context;

        public StatusLogService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveStatusLog(StatusLog statusLog)
        {
            if (statusLog.Id != 0)
            {
                statusLog.updatedBy = statusLog.createdBy;
                statusLog.updatedAt = DateTime.Now;
                _context.StatusLogs.Update(statusLog);
            }
            else
            {
                statusLog.createdAt = DateTime.Now;
                _context.StatusLogs.Add(statusLog);
            }

            await _context.SaveChangesAsync();
            return statusLog.Id;
        }

        public async Task<IEnumerable<StatusLog>> GetStatusLogList()
        {
            return await _context.StatusLogs.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<StatusLog>> GetStatusLogListbyreqsid(int Id)
        {
            return await _context.StatusLogs.Where(x=>x.requisitionId==Id).AsNoTracking().ToListAsync();
        }

        public async Task<StatusLog> GetStatusLogById(int id)
        {
            return await _context.StatusLogs.FindAsync(id);
        }

        public async Task<bool> DeleteStatusLogById(int id)
        {
            _context.StatusLogs.Remove(_context.StatusLogs.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StatusLogVM>> GetStatusLogListByReqId(int reqId)
        {
            var statusLog = await _context.StatusLogs.AsNoTracking()
                .Where(x=>x.requisitionId == reqId)
                .Select(x=> new StatusLogVM {
                    Id = x.Id,
                    empName = x.empName,
                    nextEmpName = x.nextEmpName,
                    Status = x.Status,
                    timeDate = x.createdAt.Value.ToShortDateString() + " " + x.createdAt.Value.ToShortTimeString()
                }).OrderByDescending(x=>x.Id).ToListAsync();
            for (int i = 0; i < statusLog.Count(); i++)
            {
                statusLog[i].Sl = (i+1);
            }
            return statusLog;
        }

    }
}
