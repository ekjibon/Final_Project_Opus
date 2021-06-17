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
    public class TravellerInfoService: ITravellerInfoService
    {
        private readonly ERPDbContext _context;

        public TravellerInfoService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveTravellerInfo(TravellerInfo travellerInfo)
        {
            if (travellerInfo.Id != 0)
                _context.travellerInfos.Update(travellerInfo);
            else
                _context.travellerInfos.Add(travellerInfo);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TravellerInfo>> GetTravellerInfo()
        {
            return await _context.travellerInfos.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TravellerInfo>> GetTravellerInfoByMasterId(int masterId)
        {
            return await _context.travellerInfos.Where(x=>x.travelMasterId==masterId).AsNoTracking().ToListAsync();
        }

        public async Task<TravellerInfo> GetTravellerInfoById(int id)
        {

            return await _context.travellerInfos.FindAsync(id);
        }

        public async Task<bool> DeleteTravellerInfoById(int id)
        {
            _context.travellerInfos.Remove(_context.travellerInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
