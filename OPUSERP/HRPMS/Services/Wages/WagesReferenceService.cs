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
    public class WagesReferenceService: IWagesReferenceService
    {
        private readonly ERPDbContext _context;

        public WagesReferenceService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteReferenceById(int id)
        {
            _context.wagesReferences.Remove(_context.wagesReferences.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WagesReference>> GetReference()
        {
            return await _context.wagesReferences.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<WagesReference>> GetReferenceByEmpId(int empId)
        {
            return await _context.wagesReferences.Where(x => x.employeeID == empId).AsNoTracking().ToListAsync();
        }

        public async Task<WagesReference> GetReferenceById(int id)
        {
            return await _context.wagesReferences.FindAsync(id);
        }

        public async Task<int> SaveReference(WagesReference reference)
        {
            if (reference.Id != 0)
                _context.wagesReferences.Update(reference);
            else
                _context.wagesReferences.Add(reference);

            await _context.SaveChangesAsync();
            return reference.Id;
        }
    }
}
