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
    public class ServiceHistoryService : IServiceHistoryService
    {
        private readonly ERPDbContext _context;

        public ServiceHistoryService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteServiceHistoryById(int id)
        {
            _context.memberTransferLogs.Remove(_context.memberTransferLogs.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MemberTransferLog>> GetServiceHistory()
        {
            return await _context.memberTransferLogs.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MemberTransferLog>> GetServiceHistoryByEmpId(int empId)
        {
            return await _context.memberTransferLogs.Where(x => x.employeeId == empId).Include(x => x.employee).Include(x => x.salaryGrade).AsNoTracking().ToListAsync();
        }

        public async Task<MemberTransferLog> GetServiceHistoryById(int id)
        {
            return await _context.memberTransferLogs.FindAsync(id);
        }

        public async Task<bool> SaveServiceHistory(MemberTransferLog transferLog)
        {
            if (transferLog.Id != 0)
                _context.memberTransferLogs.Update(transferLog);
            else
                _context.memberTransferLogs.Add(transferLog);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
