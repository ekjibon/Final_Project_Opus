using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Attendance;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Wages;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Attendance
{
    public class WagesLeftDetailsService : IWagesLeftDetailsService
    {
        private readonly ERPDbContext _context;

        public WagesLeftDetailsService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveWagesLeftDetails(WagesLeftDetails wagesLeftDetails)
        {
            if (wagesLeftDetails.Id != 0)
                _context.wagesLeftDetails.Update(wagesLeftDetails);
            else
                _context.wagesLeftDetails.Add(wagesLeftDetails);
            //_context.employeePunchCardInfos.Add(employeePunchCardInfo);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WagesLeftDetails>> GetWagesLeftDetails()
        {
            return await _context.wagesLeftDetails.Include(a=>a.employee).AsNoTracking().ToListAsync();
        }

        public async Task<WagesLeftDetails> GetWagesLeftDetailsById(int id)
        {
            return await _context.wagesLeftDetails.FindAsync(id);
        }

        public async Task<bool> DeleteWagesLeftDetailsById(int id)
        {
            //_context.myquery.FromSql("");
            _context.wagesLeftDetails.Remove(_context.wagesLeftDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        } 
    }
}