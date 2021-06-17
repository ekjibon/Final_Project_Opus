using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Attendance;
using OPUSERP.HRPMS.Data.Entity.Wages;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Attendance
{
    public class WagesPunchCardInfoService : IWagesPunchCardInfoService
    {
        private readonly ERPDbContext _context;

        public WagesPunchCardInfoService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveEmployeePunchCardInfo(WagesPunchCardInfo employeePunchCardInfo)
        {
            if (employeePunchCardInfo.Id != 0)
                _context.wagesPunchCardInfos.Update(employeePunchCardInfo);
            else
                _context.wagesPunchCardInfos.Add(employeePunchCardInfo);
            //_context.employeePunchCardInfos.Add(employeePunchCardInfo);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WagesPunchCardInfo>> GetAllEmployeePunchCardInfo()
        {
            return await _context.wagesPunchCardInfos.Include(a=>a.employee).AsNoTracking().ToListAsync();
        }

        public async Task<WagesPunchCardInfo> GetEmployeePunchCardInfoById(int id)
        {
            return await _context.wagesPunchCardInfos.FindAsync(id);
        }

        public async Task<bool> DeleteEmployeePunchCardInfoById(int id)
        {
            //_context.myquery.FromSql("");
            _context.wagesPunchCardInfos.Remove(_context.wagesPunchCardInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        
    }
}