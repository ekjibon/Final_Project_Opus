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
    public class OfficeAssignService : IOfficeAssignService
    {
        private readonly ERPDbContext _context;

        public OfficeAssignService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteOfficeAssignById(int id)
        {
            _context.officeAssigns.Remove(_context.officeAssigns.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OfficeAssign>> GetOfficeAssign()
        {
            return await _context.officeAssigns.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<OfficeAssign>> GetOfficeAssignByEmpId(int empId)
        {
            return await _context.officeAssigns.Where(x => x.employeeInfoId == empId).AsNoTracking().ToListAsync();
        }

        public async Task<OfficeAssign> GetOfficeAssignById(int id)
        {
            return await _context.officeAssigns.FindAsync(id);
        }

        public async Task<int> SaveofficeAssign(OfficeAssign officeAssign)
        {
            if (officeAssign.Id != 0)
                _context.officeAssigns.Update(officeAssign);
            else
                _context.officeAssigns.Add(officeAssign);

            await _context.SaveChangesAsync();
            return officeAssign.Id;
        }
    }
}
