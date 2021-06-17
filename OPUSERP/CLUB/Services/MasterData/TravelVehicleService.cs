using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data;
using CLUB.Data.Entity.Master;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;

namespace CLUB.Services.MasterData
{
    public class TravelVehicleService : ITravelService
    {
        private readonly ERPDbContext _context;

        public TravelVehicleService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TravelPurpose>> GetTravelPurposes()
        {
            return await _context.travelPurposes.AsNoTracking().ToListAsync();
        }

        public async Task<TravelPurpose> GetTravelPurposeById(int id)
        {
            return await _context.travelPurposes.FindAsync(id);
        }

        public async Task<bool> SaveTravelPurpose(TravelPurpose travelPurpose)
        {
            if(travelPurpose.Id != 0)
                _context.travelPurposes.Update(travelPurpose);
            else
                _context.travelPurposes.Add(travelPurpose);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteTravelPurposeById(int id)
        {
            _context.travelPurposes.Remove(_context.travelPurposes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        // Vehicle type

        public async Task<IEnumerable<Vehicle>> GetVehicleType()
        {
            return await _context.vehicles.AsNoTracking().ToListAsync();
        }

        public async Task<Vehicle> GetVehicleTypeById(int id)
        {
            return await _context.vehicles.FindAsync(id);
        }

        public async Task<bool> SaveVehicleType(Vehicle vehicle)
        {
            if(vehicle.Id != 0)
                _context.vehicles.Update(vehicle);
            else
                _context.vehicles.Add(vehicle);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteVehicleTypeById(int id)
        {
            _context.vehicles.Remove(_context.vehicles.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
