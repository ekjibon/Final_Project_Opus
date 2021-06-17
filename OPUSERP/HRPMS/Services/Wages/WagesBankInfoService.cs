using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee
{
    public class WagesBankInfoService : IWagesBankInfoService
    {
        private readonly ERPDbContext _context;

        public WagesBankInfoService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteBankInfoById(int id)
        {
            _context.wagesBankInfos.Remove(_context.wagesBankInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WagesBankInfo>> GetBankInfo()
        {
            return await _context.wagesBankInfos.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<WagesBankInfo>> GetBankInfoByEmpId(int empId)
        {
            return await _context.wagesBankInfos.Where(x => x.employeeId == empId).Include(x => x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<WagesBankInfo> GetBankInfoById(int id)
        {
            return await _context.wagesBankInfos.FindAsync(id);
        }

        public async Task<bool> SaveBankInfo(WagesBankInfo bankInfo)
        {
            if (bankInfo.Id != 0)
                _context.wagesBankInfos.Update(bankInfo);
            else
                _context.wagesBankInfos.Add(bankInfo);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
