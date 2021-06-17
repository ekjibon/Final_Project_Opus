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
    public class BelongingsService: IBelongingsService
    {
        private readonly ERPDbContext _context;

        public BelongingsService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteBelongingsById(int id)
        {
            _context.Belongings.Remove(_context.Belongings.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Belongings>> GetBelongings()
        {
            return await _context.Belongings.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Belongings>> GetBelongingsByEmpId(int empId)
        {
            return await _context.Belongings.Where(x => x.employeeId == empId).Include(x => x.belongingItem).Include(x => x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<Belongings> GetBelongingsById(int id)
        {
            return await _context.Belongings.FindAsync(id);
        }

        public async Task<bool> SaveBelongings(Belongings belongings)
        {
            if (belongings.Id != 0)
                _context.Belongings.Update(belongings);
            else
                _context.Belongings.Add(belongings);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
