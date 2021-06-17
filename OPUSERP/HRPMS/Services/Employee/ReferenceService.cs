using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee
{
    public class ReferenceService: IReferenceService
    {
        private readonly ERPDbContext _context;

        public ReferenceService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteReferenceById(int id)
        {
            _context.references.Remove(_context.references.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Data.Entity.Employee.Reference>> GetReference()
        {
            return await _context.references.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Data.Entity.Employee.Reference>> GetReferenceByEmpId(int empId)
        {
            return await _context.references.Where(x => x.employeeID == empId).AsNoTracking().ToListAsync();
        }

        public async Task<Data.Entity.Employee.Reference> GetReferenceById(int id)
        {
            return await _context.references.FindAsync(id);
        }

        public async Task<int> SaveReference(Data.Entity.Employee.Reference reference)
        {
            if (reference.Id != 0)
                _context.references.Update(reference);
            else
                _context.references.Add(reference);

            await _context.SaveChangesAsync();
            return reference.Id;
        }
    }
}
