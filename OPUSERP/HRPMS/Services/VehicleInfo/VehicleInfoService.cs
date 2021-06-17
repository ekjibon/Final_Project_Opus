using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.VehicleInfo;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData
{
    public class VehicleInfoService : IVehicleInfoService
    {
        private readonly ERPDbContext _context;

        public VehicleInfoService(ERPDbContext context)
        {
            _context = context;
        }
                
        // Vehicle type

        public async Task<IEnumerable<VehicleEntry>> GetVehicleInfo()
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await _context.vehicleEntries.Include(x => x.vehicle).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<VehicleEntry>> GetVehicleInfoByOrg(string org)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await _context.vehicleEntries.Include(x => x.vehicle).Where(x => x.orgType == org).AsNoTracking().ToListAsync();
        }

        public async Task<VehicleEntry> GetVehicleInfoById(int id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await _context.vehicleEntries.Include(x=> x.vehicle).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveVehicleInfo(VehicleEntry vehicleEntry)
        {
            if (vehicleEntry.Id != 0)
                _context.vehicleEntries.Update(vehicleEntry);
            else
                _context.vehicleEntries.Add(vehicleEntry);
            await _context.SaveChangesAsync();
            return vehicleEntry.Id;
        }

        public async Task<bool> DeleteVehicleInfoById(int id)
        {
            _context.vehicleEntries.Remove(_context.vehicleEntries.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<string> GetVehicleNameById(int id)
        {
            VehicleEntry data = await _context.vehicleEntries.FindAsync(id);
            return data.vID;
        }
    }
}
