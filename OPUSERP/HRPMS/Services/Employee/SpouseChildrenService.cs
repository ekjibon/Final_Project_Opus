using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee
{
    public class SpouseChildrenService : ISpouseChildrenService
    {
        private readonly ERPDbContext _context;

        public SpouseChildrenService(ERPDbContext context)
        {
            _context = context;
        }

        //Spouse
        public async Task<bool> DeleteSpouseById(int id)
        {
            _context.spouses.Remove(_context.spouses.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Spouse>> GetSpouse()
        {
            return await _context.spouses.AsNoTracking().ToListAsync();
        }

        public async Task<Spouse> GetSpouseById(int id)
        {
            return await _context.spouses.FindAsync(id);
        }

        public async Task<bool> SaveSpouse(Spouse spouse)
        {
            if (spouse.Id != 0)
                _context.spouses.Update(spouse);
            else
                _context.spouses.Add(spouse);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Spouse>> GetSpouseByEmpId(int empId)
        {
            return await _context.spouses.Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }

        //Children
        public async Task<bool> DeleteChildrenById(int id)
        {
            _context.childrens.Remove(_context.childrens.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Children>> GetChildren()
        {
            return await _context.childrens.AsNoTracking().ToListAsync();
        }

        public async Task<Children> GetChildrenById(int id)
        {
            return await _context.childrens.FindAsync(id);
        }

        public async Task<bool> SaveChildren(Children children)
        {
            if (children.Id != 0)
                _context.childrens.Update(children);
            else
            _context.childrens.Add(children);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Children>> GetChildrenByEmpId(int empId)
        {
            return await _context.childrens.Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }
    }
}
