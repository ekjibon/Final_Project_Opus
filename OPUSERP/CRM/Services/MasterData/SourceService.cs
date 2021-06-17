using Microsoft.EntityFrameworkCore;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData
{
    public class SourceService : ISourceService
    {
        private readonly ERPDbContext _context;

        public SourceService(ERPDbContext context)
        {
            _context = context;
        }

        // Sector
        public async Task<bool> SaveSource(Source source)
        {
            if (source.Id != 0)
                _context.Sources.Update(source);
            else
                _context.Sources.Add(source);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Source>> GetAllSource()
        {
            return await _context.Sources.AsNoTracking().ToListAsync();
        }

        public async Task<Source> GetSourceById(int id)
        {
            return await _context.Sources.FindAsync(id);
        }

        public async Task<bool> DeleteSourceById(int id)
        {
            _context.Sources.Remove(_context.Sources.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
