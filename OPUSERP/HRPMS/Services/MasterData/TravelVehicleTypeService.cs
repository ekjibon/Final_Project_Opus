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
    public class TravelVehicleTypeService: ITravelVehicleTypeService
    {
        private readonly ERPDbContext _context;

        public TravelVehicleTypeService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveTravelVehicleType(TravelVehicleType travelVehicleType)
        {
            if (travelVehicleType.Id != 0)
                _context.travelVehicleTypes.Update(travelVehicleType);
            else
                _context.travelVehicleTypes.Add(travelVehicleType);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TravelVehicleType>> GetTravelVehicleType()
        {
            return await _context.travelVehicleTypes.AsNoTracking().ToListAsync();
        }

        public async Task<TravelVehicleType> GetTravelVehicleTypeById(int id)
        {

            return await _context.travelVehicleTypes.FindAsync(id);
        }

        public async Task<bool> DeleteTravelVehicleTypeById(int id)
        {
            _context.travelVehicleTypes.Remove(_context.travelVehicleTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
