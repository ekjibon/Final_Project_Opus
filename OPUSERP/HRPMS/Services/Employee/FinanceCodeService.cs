using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee
{
    public class FinanceCodeService: IFinanceCodeService
    {
        private readonly ERPDbContext _context;

        public FinanceCodeService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteFinanceCodeById(int id)
        {
            _context.financeCodes.Remove(_context.financeCodes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FinanceCode>> GetFinanceCode()
        {
            return await _context.financeCodes.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<FinanceCode>> GetFinanceCodeByEmpId(int empId)
        {
            return await _context.financeCodes.Where(x => x.employeeId == empId).Include(x=>x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<FinanceCode> GetFinanceCodeById(int id)
        {
            return await _context.financeCodes.FindAsync(id);
        }

        public async Task<bool> SaveFinanceCode(FinanceCode financeCode)
        {
            if (financeCode.Id != 0)
                _context.financeCodes.Update(financeCode);
            else
                _context.financeCodes.Add(financeCode);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
