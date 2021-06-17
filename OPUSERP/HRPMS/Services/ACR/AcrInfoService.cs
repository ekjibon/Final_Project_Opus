using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.ACR.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.ACR
{
    public class AcrInfoService : IAcrInfoService
    {
        private readonly ERPDbContext _context;

        public AcrInfoService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteAcrInfoById(int id)
        {
            _context.acrInfos.Remove(_context.acrInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AcrInfo>> GetAcrInfo()
        {
            return await _context.acrInfos.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AcrInfo>> GetAcrInfoByEmpId(int empId)
        {
            return await _context.acrInfos.Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }

        public async Task<AcrInfo> GetAcrInfoById(int id)
        {
            return await _context.acrInfos.FindAsync();
        }

        public async Task<int> SaveACRInfo(AcrInfo acrInfo)
        {
            if (acrInfo.Id != 0)
                _context.acrInfos.Update(acrInfo);
            else
                _context.acrInfos.Add(acrInfo);

            await _context.SaveChangesAsync();
            return acrInfo.Id;
        }
    }
}
