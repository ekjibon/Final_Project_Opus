using Microsoft.EntityFrameworkCore;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData
{
    public class SectorService : ISectorService
    {
        private readonly ERPDbContext _context;

        public SectorService(ERPDbContext context)
        {
            _context = context;
        }

       
        public async Task<bool> SaveSector(Sector  sector)
        {
            if (sector.Id != 0)
                _context.Sectors.Update(sector);
            else
                _context.Sectors.Add(sector);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Sector>> GetAllSector()
        {
            return await _context.Sectors.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Sector>> GetSectorByParrentId(int parrentId)
        {
            return await _context.Sectors.Where(x => x.sectorId == parrentId).AsNoTracking().ToListAsync();
        }

        public async Task<Sector> GetSectorById(int id)
        {
            return await _context.Sectors.FindAsync(id);
        }

        public async Task<bool> DeleteSectorById(int id)
        {
            _context.Sectors.Remove(_context.Sectors.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<int> GetRootId(int currentID)
        {
            Sector sector;
            do
            {
                sector = await _context.Sectors.FindAsync(currentID);
                currentID = sector.sectorId ?? 0;
            }
            while (currentID != 0);
          //  int a = 10;
            return sector.Id;
        }
    }
}
