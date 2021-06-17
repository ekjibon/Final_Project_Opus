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
    public class FreedomFighterService: IFreedomFighterService
    {
        private readonly ERPDbContext _context;

        public FreedomFighterService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteFreedomFighterById(int id)
        {
            _context.freedomFighters.Remove(_context.freedomFighters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FreedomFighter>> GetFreedomFighter()
        {
            return await _context.freedomFighters.AsNoTracking().ToListAsync();
        }

        public async Task<FreedomFighter> GetFreedomFighterByEmpId(int empId)
        {
            return await _context.freedomFighters.Where(x => x.employeeID == empId).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<FreedomFighter>> GetFreedomFighterlistByEmpId(int empId)
        {
            return await _context.freedomFighters.Where(x => x.employeeID == empId).AsNoTracking().ToListAsync();
        }

        public async Task<FreedomFighter> GetFreedomFighterById(int id)
        {
            return await _context.freedomFighters.FindAsync(id);
        }

        public async Task<bool> SaveFreedomFighter(FreedomFighter freedomFighter)
        {
            if (freedomFighter.Id != 0)
                _context.freedomFighters.Update(freedomFighter);
            else
                _context.freedomFighters.Add(freedomFighter);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
