using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee
{
    public class WagesPhotographService : IWagesPhotographService
    {
        private readonly ERPDbContext _context;

        public WagesPhotographService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeletePhotographById(int id)
        {
            _context.wagesPhotographs.Remove(_context.wagesPhotographs.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<WagesPhotograph> GetPhotographByEmpIdAndType(int empId, string type)
        {
            return await _context.wagesPhotographs.Where(x => x.type == type && x.employeeId == empId).FirstOrDefaultAsync();
        }

        public async Task<WagesPhotograph> GetPhotographById(int id)
        {
            return await _context.wagesPhotographs.FindAsync(id);
        }

        public async Task<IEnumerable<WagesPhotograph>> GetPhotographs()
        {
            return await _context.wagesPhotographs.AsNoTracking().ToListAsync();
        }

        public async Task<bool> SavePhotograph(WagesPhotograph photograph)
        {
            if (photograph.Id != 0)
                _context.wagesPhotographs.Update(photograph);
            else
                _context.wagesPhotographs.Add(photograph);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
