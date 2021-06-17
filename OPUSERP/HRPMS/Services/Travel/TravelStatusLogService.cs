using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Travel;
using OPUSERP.HRPMS.Services.Travel.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Travel
{
    public class TravelStatusLogService: ITravelStatusLogService
    {
        private readonly ERPDbContext _context;

        public TravelStatusLogService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveTravelStatusLog(TravelStatusLog travelStatusLog)
        {
            if (travelStatusLog.Id != 0)
                _context.travelStatusLogs.Update(travelStatusLog);
            else
                _context.travelStatusLogs.Add(travelStatusLog);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TravelStatusLog>> GetAllTravelStatusLog()
        {
            return await _context.travelStatusLogs.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TravelStatusLog>> GetAllTravelStatusLogByTravelId(int id)
        {
            return await _context.travelStatusLogs.Where(x => x.travelMasterId == id).Include(x => x.travelMaster).Include(x => x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<TravelStatusLog> GetTravelStatusLogById(int id)
        {
            return await _context.travelStatusLogs.FindAsync(id);
        }

        public async Task<bool> DeleteTravelStatusLogById(int id)
        {
            _context.travelStatusLogs.Remove(_context.travelStatusLogs.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
