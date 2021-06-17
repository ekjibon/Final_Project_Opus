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
    public class TravellingInfoService: ITravellingInfoService
    {
        private readonly ERPDbContext _context;

        public TravellingInfoService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveTravellingInfo(TravellingInfo travellingInfo)
        {
            if (travellingInfo.Id != 0)
                _context.travellingInfos.Update(travellingInfo);
            else
                _context.travellingInfos.Add(travellingInfo);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TravellingInfo>> GetTravellingInfo()
        {
            return await _context.travellingInfos.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TravellingInfo>> GetTravellingInfoByMasterId(int masterId)
        {
            return await _context.travellingInfos.Where(x => x.travelMasterId == masterId).AsNoTracking().ToListAsync();
        }

        public async Task<TravellingInfo> GetTravellingInfoById(int id)
        {

            return await _context.travellingInfos.FindAsync(id);
        }

        public async Task<bool> DeleteTravellingInfoById(int id)
        {
            _context.travellingInfos.Remove(_context.travellingInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
