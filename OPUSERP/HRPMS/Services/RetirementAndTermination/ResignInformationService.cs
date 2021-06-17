using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.RetirementAndTermination;
using OPUSERP.HRPMS.Services.RetirementAndTermination.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.RetirementAndTermination
{
    public class ResignInformationService: IResignInformationService
    {
        private readonly ERPDbContext _context;

        public ResignInformationService(ERPDbContext context)
        {
            _context = context;
        }
        
        public async Task<bool> DeleteResignInformationById(int id)
        {
            _context.resignInformation.Remove(_context.resignInformation.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ResignInformation>> GetResignInformation()
        {
            return await _context.resignInformation.Include(x => x.employee).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<ResignInformation>> GetResignInfoForFinalSettlement()
        {
            List<int?> employe = await _context.FinalSettlements.Select(x => x.employeeInfoId).ToListAsync();
            return await _context.resignInformation.Include(x => x.employee).Where(x=>! employe.Contains(x.employeeId)).AsNoTracking().ToListAsync();
        }

        public async Task<ResignInformation> GetResignInformationById(int id)
        {
            return await _context.resignInformation.Include(x => x.employee).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveResignInformation(ResignInformation resignInformation)
        {
            if (resignInformation.Id != 0)
                _context.resignInformation.Update(resignInformation);
            else
                _context.resignInformation.Add(resignInformation);
            await _context.SaveChangesAsync();
            return resignInformation.Id;
        }

    }
}
