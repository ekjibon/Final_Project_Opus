using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData
{
    public class SpecialBranchUnitService: ISpecialBranchUnitService
    {
        private readonly ERPDbContext _context;

        public SpecialBranchUnitService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SpecialBranchUnit>> GetSpecialBranchUnit()
        {
            return await _context.SpecialBranchUnits.Include(x=>x.company).AsNoTracking().ToListAsync();
        }

        public async Task<SpecialBranchUnit> GetSpecialBranchUnitById(int id)
        {
            return await _context.SpecialBranchUnits.FindAsync(id);
        }

        public async Task<IEnumerable<SpecialBranchUnit>> GetSpecialBranchUnitByUserBranchId(int id)
        {
            return await _context.SpecialBranchUnits.Where(a => a.Id == id).AsNoTracking().ToListAsync();
        }

        public async Task<bool> SaveSpecialBranchUnit(SpecialBranchUnit specialBranchUnit)
        {
            if (specialBranchUnit.Id != 0)
                _context.SpecialBranchUnits.Update(specialBranchUnit);
            else
                _context.SpecialBranchUnits.Add(specialBranchUnit);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteSpecialBranchUnitById(int id)
        {
            _context.SpecialBranchUnits.Remove(_context.SpecialBranchUnits.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
