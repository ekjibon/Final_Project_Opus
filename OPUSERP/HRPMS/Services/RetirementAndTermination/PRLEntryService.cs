using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.RetirementAndTermination;
using Microsoft.EntityFrameworkCore;

namespace OPUSERP.HRPMS.Services.RetirementAndTermination
{
    public class PRLEntryService : IPRLEntryService
    {
        private readonly ERPDbContext _context;

        public PRLEntryService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DeletePrlEntryById(int id)
        {
            _context.pRLApplications.Remove(_context.pRLApplications.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PRLApplication>> GetPrlEntry()
        {
            return await _context.pRLApplications.Include(x=>x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<PRLApplication> GetPrlEntryById(int id)
        {
            return await _context.pRLApplications.Include(x => x.employee).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SavePrlEntry(PRLApplication PrlEntry)
        {
            if (PrlEntry.Id != 0)
                _context.pRLApplications.Update(PrlEntry);
            else
                _context.pRLApplications.Add(PrlEntry);
            await _context.SaveChangesAsync();
            return PrlEntry.Id;
        }

        public async Task<bool> UpdatePRLStatus(int Id, string fromDate, string toDate, string duration, string status)
        {
            PRLApplication data = await _context.pRLApplications.FindAsync(Id);
            if (data != null)
            {
                data.status = status;
                data.fromDate = fromDate;
                data.toDate = toDate;
                data.duration = duration;
                return 1 == await _context.SaveChangesAsync();
            }
            return false;
        }


        public async Task<IEnumerable<PRLApplication>> GetPRLEntryByStatus(string status)
        {
            return await _context.pRLApplications.Where(x => x.status == status).Include(x => x.employee).AsNoTracking().ToListAsync();
        }
    }
}
