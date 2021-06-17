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
    public class PriviousEmploymentService: IPriviousEmploymentService
    {
        private readonly ERPDbContext _context;

        public PriviousEmploymentService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeletePriviousEmploymentById(int id)
        {
            _context.priviousEmployments.Remove(_context.priviousEmployments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PriviousEmployment>> GetPriviousEmployment()
        {
            return await _context.priviousEmployments.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PriviousEmployment>> GetPriviousEmploymentByEmpId(int empId)
        {
            return await _context.priviousEmployments.Where(x => x.employeeID == empId).Include(x => x.organizationType).AsNoTracking().ToListAsync();
        }

        public async Task<PriviousEmployment> GetPriviousEmploymentById(int id)
        {
            return await _context.priviousEmployments.FindAsync(id);
        }

        public async Task<bool> SavePriviousEmployment(PriviousEmployment priviousEmployment)
        {
            if (priviousEmployment.Id != 0)
                _context.priviousEmployments.Update(priviousEmployment);
            else
                _context.priviousEmployments.Add(priviousEmployment);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
