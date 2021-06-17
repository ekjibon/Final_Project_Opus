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
    public class HRDonerSevice: IHRDonerSevice
    {
        private readonly ERPDbContext _context;

        public HRDonerSevice(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveHRDoner(HRDoner hRDoner)
        {
            if (hRDoner.Id != 0)
                _context.hRDoners.Update(hRDoner);
            else
                _context.hRDoners.Add(hRDoner);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<HRDoner>> GetHRDoner()
        {
            return await _context.hRDoners.AsNoTracking().ToListAsync();
        }

        public async Task<HRDoner> GetHRDonerById(int id)
        {

            return await _context.hRDoners.FindAsync(id);
        }

        public async Task<bool> DeleteHRDonerById(int id)
        {
            _context.hRDoners.Remove(_context.hRDoners.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
