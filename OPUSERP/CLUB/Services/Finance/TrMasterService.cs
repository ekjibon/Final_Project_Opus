using Microsoft.EntityFrameworkCore;
using OPUSERP.CLUB.Data.Finance;
using OPUSERP.CLUB.Services.Finance.Interface;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Finance
{
    public class TrMasterService: ITrMasterService
    {
        private readonly ERPDbContext _context;

        public TrMasterService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveTrMaster(TrMaster trMaster)
        {
            if (trMaster.Id != 0)
                _context.trMasters.Update(trMaster);
            else
                _context.trMasters.Add(trMaster);
            await _context.SaveChangesAsync();
            return trMaster.Id;
        }

        public async Task<IEnumerable<TrMaster>> GetTrMaster()
        {
            return await _context.trMasters.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TrMaster>> GetTrMasterByEmpId(int empId)
        {
            return await _context.trMasters.Where(x=>x.employeeId == empId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TrMaster>> GetPendingTrMaster()
        {
            return await _context.trMasters.Where(x=>x.status == 0).Include(x=>x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<TrMaster> GetTrMasterById(int id)
        {
            return await _context.trMasters.Where(x=>x.Id == id).Include(x => x.employee).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteTrMasterById(int id)
        {
            _context.trMasters.Remove(_context.trMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
