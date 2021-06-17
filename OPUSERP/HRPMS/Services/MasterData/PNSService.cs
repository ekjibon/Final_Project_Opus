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
    public class PNSService: IPNSService
    {
        private readonly ERPDbContext _context;

        public PNSService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SavePNS(PNS pNS)
        {
            if (pNS.Id != 0)
                _context.pNs.Update(pNS);
            else
                _context.pNs.Add(pNS);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PNS>> GetPNS()
        {
            return await _context.pNs.AsNoTracking().ToListAsync();
        }

        public async Task<PNS> GetPNSById(int id)
        {
            return await _context.pNs.FindAsync(id);
        }

        public async Task<bool> DeletePNSById(int id)
        {
            _context.pNs.Remove(_context.pNs.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
