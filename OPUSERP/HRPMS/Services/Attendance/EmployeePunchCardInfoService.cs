using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Attendance;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Attendance
{
    public class EmployeePunchCardInfoService : IEmployeePunchCardInfoService
    {
        private readonly ERPDbContext _context;

        public EmployeePunchCardInfoService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveEmployeePunchCardInfo(EmployeePunchCardInfo employeePunchCardInfo)
        {
            if (employeePunchCardInfo.Id != 0)
                _context.employeePunchCardInfos.Update(employeePunchCardInfo);
            else
                _context.employeePunchCardInfos.Add(employeePunchCardInfo);
            //_context.employeePunchCardInfos.Add(employeePunchCardInfo);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeePunchCardInfo>> GetAllEmployeePunchCardInfo()
        {
            return await _context.employeePunchCardInfos.Include(a=>a.shiftGroupMaster).AsNoTracking().ToListAsync();
        }

        public async Task<EmployeePunchCardInfo> GetEmployeePunchCardInfoById(int id)
        {
            return await _context.employeePunchCardInfos.FindAsync(id);
        }

        public async Task<bool> DeleteEmployeePunchCardInfoById(int id)
        {
            //_context.myquery.FromSql("");
            _context.employeePunchCardInfos.Remove(_context.employeePunchCardInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        
    }
}