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
    public class LocationService: ILocationService
    {
        private readonly ERPDbContext _context;

        public LocationService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Location>> GetLocation()
        {
            return await _context.Locations.Include(x=>x.company).AsNoTracking().ToListAsync();
        }

        public async Task<Location> GetLocationById(int id)
        {
            return await _context.Locations.FindAsync(id);
        }

        

        public async Task<bool> SaveLocation(Location location)
        {
            if (location.Id != 0)
                _context.Locations.Update(location);
            else
                _context.Locations.Add(location);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteLocationById(int id)
        {
            _context.Locations.Remove(_context.Locations.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
