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
    public class RlnSBUPNSService : IRlnSBUPNSService
    {
        private readonly ERPDbContext _context;

        public RlnSBUPNSService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveRlnSBUPNS(RlnSBUPNS rlnSBUPNS)
        {
            if (rlnSBUPNS.Id != 0)
                _context.rlnSBUPNs.Update(rlnSBUPNS);
            else
                _context.rlnSBUPNs.Add(rlnSBUPNS);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RlnSBUPNS>> GetRlnSBUPNS()
        {
            return await _context.rlnSBUPNs.Include(x => x.specialBranchUnit).Include(x => x.pNS).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<RlnSBUPNS>> GetRlnSBUPNSBySBUId(int sbuId)
        {
            return await _context.rlnSBUPNs.Where(x => x.specialBranchUnitId == sbuId).Include(x => x.specialBranchUnit).Include(x => x.pNS).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<RlnSBUPNS>> GetRlnSBUPNSByPNSId(int pnsId)
        {
            return await _context.rlnSBUPNs.Where(x => x.pNSId == pnsId).Include(x => x.specialBranchUnit).Include(x => x.pNS).AsNoTracking().ToListAsync();
        }

        public async Task<RlnSBUPNS> GetRlnSBUPNSById(int id)
        {
            return await _context.rlnSBUPNs.FindAsync(id);
        }

        public async Task<bool> DeleteRlnSBUPNSById(int id)
        {
            _context.rlnSBUPNs.Remove(_context.rlnSBUPNs.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
