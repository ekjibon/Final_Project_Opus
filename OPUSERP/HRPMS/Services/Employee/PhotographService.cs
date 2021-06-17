using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee
{
    public class PhotographService : IPhotographService
    {
        private readonly ERPDbContext _context;

        public PhotographService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeletePhotographById(int id)
        {
            _context.photographs.Remove(_context.photographs.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<Photograph> GetPhotographByEmpIdAndType(int empId, string type)
        {
            return await _context.photographs.Where(x => x.type == type && x.employeeId == empId).FirstOrDefaultAsync();
        }

        public async Task<Photograph> GetPhotographById(int id)
        {
            return await _context.photographs.FindAsync(id);
        }

        public async Task<IEnumerable<Photograph>> GetPhotographs()
        {
            return await _context.photographs.AsNoTracking().ToListAsync();
        }

        public async Task<bool> SavePhotograph(Photograph photograph)
        {
            if (photograph.Id != 0)
                _context.photographs.Update(photograph);
            else
                _context.photographs.Add(photograph);

            return 1 == await _context.SaveChangesAsync();
        }



        public async Task<bool> DeleteEmployeeSignatureById(int id)
        {
            _context.employeeSignatures.Remove(_context.employeeSignatures.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<EmployeeSignature> GetEmployeeSignatureByEmpId(int empId)
        {
            return await _context.employeeSignatures.Where(x => x.employeeId == empId).FirstOrDefaultAsync();
        }

        public async Task<EmployeeSignature> GetEmployeeSignatureById(int id)
        {
            return await _context.employeeSignatures.FindAsync(id);
        }

        public async Task<IEnumerable<EmployeeSignature>> GetEmployeeSignature()
        {
            return await _context.employeeSignatures.AsNoTracking().ToListAsync();
        }

        public async Task<bool> SaveEmployeeSignature(EmployeeSignature photograph)
        {
            if (photograph.Id != 0)
                _context.employeeSignatures.Update(photograph);
            else
                _context.employeeSignatures.Add(photograph);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
