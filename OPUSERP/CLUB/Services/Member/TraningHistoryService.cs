using OPUSERP.CLUB.Services.Member.Interfaces;
using Microsoft.EntityFrameworkCore;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Member
{
    public class TraningHistoryService : ITraningHistoryService
    {
        private readonly ERPDbContext _context;

        public TraningHistoryService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteTraningHistoryById(int id)
        {
            _context.memberTraningLogs.Remove(_context.memberTraningLogs.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MemberTraningLog>> GetTraningHistory()
        {
            return await _context.memberTraningLogs.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MemberTraningLog>> GetTraningHistoryByEmpId(int empId)
        {
            return await _context.memberTraningLogs.Where(x => x.employeeId == empId).Include(x => x.employee).Include(x => x.country).Include(x => x.trainingCategory).Include(x => x.trainingInstitute).AsNoTracking().ToListAsync();
        }

        public async Task<MemberTraningLog> GetTraningHistoryById(int id)
        {
            return await _context.memberTraningLogs.FindAsync(id);
        }

        public async Task<bool> SaveTraningHistory(MemberTraningLog traningLog)
        {
            if (traningLog.Id != 0)
                _context.memberTraningLogs.Update(traningLog);
            else
                _context.memberTraningLogs.Add(traningLog);

            return 1 == await _context.SaveChangesAsync();
        }

        //Wahid 
        public async Task<IEnumerable<MemberTraningLog>> GetTraningHistoryListById(int? empId)
        {
            return await _context.memberTraningLogs.AsNoTracking().Where(x=>x.employeeId==empId).ToListAsync();
        }
        //Wahid
    }
}
