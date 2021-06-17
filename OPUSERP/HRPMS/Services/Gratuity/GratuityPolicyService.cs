using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Gratuity;
using OPUSERP.HRPMS.Services.Gratuity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Gratuity
{
    public class GratuityPolicyService : IGratuityPolicyService
    {
        private readonly ERPDbContext _context;

        public GratuityPolicyService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GratuityPolicyMaster>> GetGratuityPolicyMaster()
        {
            return await _context.gratuityPolicyMasters.Include(x => x.sbu).AsNoTracking().ToListAsync();
        }

        public async Task<GratuityPolicyMaster> GetGratuityPolicyMasterBybranch(int branch)
        {
            return await _context.gratuityPolicyMasters.Where(x=>x.sbuId==branch).Include(x => x.sbu).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<GratuityPolicyDetail>> GetGratuityPolicyDetail()
        {
            return await _context.gratuityPolicyDetails.Include(x => x.gratuityPolicy).Include(x => x.gratuityPolicy.sbu).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<GratuityPolicyDetail>> GetGratuityPolicyDetailByBranchId(int? branchId)
        {
            return await _context.gratuityPolicyDetails.Include(x => x.gratuityPolicy).Include(x => x.gratuityPolicy.sbu).Where(x=>x.gratuityPolicy.sbu.Id== branchId).AsNoTracking().ToListAsync();
        }

        public async Task<int> SaveGratuityPolicyMaster(GratuityPolicyMaster gratuityPolicyMaster)
        {
            if (gratuityPolicyMaster.Id != 0)
                _context.gratuityPolicyMasters.Update(gratuityPolicyMaster);
            else
                _context.gratuityPolicyMasters.Add(gratuityPolicyMaster);
            await _context.SaveChangesAsync();
            return gratuityPolicyMaster.Id;
        }

        public async Task<bool> SaveGratuityPolicyDetail(GratuityPolicyDetail gratuityPolicyDetail)
        {
            if (gratuityPolicyDetail.Id != 0)
                _context.gratuityPolicyDetails.Update(gratuityPolicyDetail);
            else
                _context.gratuityPolicyDetails.Add(gratuityPolicyDetail);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteGratuityPolicyDetailByMasterId(int id)
        {
            _context.gratuityPolicyDetails.RemoveRange(_context.gratuityPolicyDetails.Where(x => x.gratuityPolicyId == id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
