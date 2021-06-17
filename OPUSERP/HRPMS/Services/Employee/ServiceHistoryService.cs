using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee
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
            _context.transferLogs.Remove(_context.transferLogs.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TransferLog>> GetServiceHistory()
        {
            return await _context.transferLogs.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TransferLog>> GetServiceHistoryByEmpId(int empId)
        {
            return await _context.transferLogs.Where(x => x.employeeId == empId).Include(x => x.department).Include(x => x.designatio).Include(x => x.employee).Include(x => x.salaryGrade).AsNoTracking().ToListAsync();
        }

        public async Task<TransferLog> GetServiceHistoryById(int id)
        {
            return await _context.transferLogs.FindAsync(id);
        }

        public async Task<bool> SaveServiceHistory(TransferLog transferLog)
        {
            if (transferLog.Id != 0)
                _context.transferLogs.Update(transferLog);
            else
                _context.transferLogs.Add(transferLog);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
