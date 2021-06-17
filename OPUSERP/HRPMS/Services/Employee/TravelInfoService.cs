using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee
{
    public class TravelInfoService : ITravelInfoService
    {
        private readonly ERPDbContext _context;

        public TravelInfoService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteTraveInfoById(int id)
        {
            _context.traveInfos.Remove(_context.traveInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TraveInfo>> GetTraveInfo()
        {
            return await _context.traveInfos.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TraveInfo>> GetTraveInfoByEmpId(int empId)
        {
            return await _context.traveInfos.Where(x => x.employeeId == empId).Include(x => x.travelPurpose).Include(x => x.country).OrderByDescending(a=>a.Id).AsNoTracking().ToListAsync();
        }

        public async Task<TraveInfo> GetTraveInfoById(int id)
        {
            return await _context.traveInfos.FindAsync(id);
        }

        public async Task<bool> SaveTraveInfo(TraveInfo traveInfo)
        {
            if (traveInfo.Id != 0)
                _context.traveInfos.Update(traveInfo);
            else
                _context.traveInfos.Add(traveInfo);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
