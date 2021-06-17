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
    public class WagesPriviousEmploymentService: IWagesPriviousEmploymentService
    {
        private readonly ERPDbContext _context;

        public WagesPriviousEmploymentService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeletePriviousEmploymentById(int id)
        {
            _context.wagesPriviousEmployments.Remove(_context.wagesPriviousEmployments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WagesPriviousEmployment>> GetPriviousEmployment()
        {
            return await _context.wagesPriviousEmployments.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<WagesPriviousEmployment>> GetPriviousEmploymentByEmpId(int empId)
        {
            return await _context.wagesPriviousEmployments.Where(x => x.employeeID == empId).Include(x => x.organizationType).AsNoTracking().ToListAsync();
        }

        public async Task<WagesPriviousEmployment> GetPriviousEmploymentById(int id)
        {
            return await _context.wagesPriviousEmployments.FindAsync(id);
        }

        public async Task<bool> SavePriviousEmployment(WagesPriviousEmployment priviousEmployment)
        {
            if (priviousEmployment.Id != 0)
                _context.wagesPriviousEmployments.Update(priviousEmployment);
            else
                _context.wagesPriviousEmployments.Add(priviousEmployment);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
