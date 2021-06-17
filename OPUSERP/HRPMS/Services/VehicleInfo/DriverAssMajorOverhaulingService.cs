using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.VehicleInfo;
using OPUSERP.HRPMS.Services.VehicleInfo.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.VehicleInfo
{
    public class DriverAssMajorOverhaulingService: IDriverAssMajorOverhaulingService
    {
        private readonly ERPDbContext _context;

        public DriverAssMajorOverhaulingService(ERPDbContext context)
        {
            _context = context;
        }

        //Driver Assignment

        public async Task<IEnumerable<DriverAssignment>> GetDriverAssignment()
        {
            return await _context.driverAssignments.AsNoTracking().ToListAsync();
        }

        public async Task<DriverAssignment> GetDriverAssignmentById(int id)
        {
            return await _context.driverAssignments.FindAsync(id);
        }

        public async Task<bool> SaveDriverAssignment(DriverAssignment driverAssignment)
        {
            if (driverAssignment.Id != 0)
                _context.driverAssignments.Update(driverAssignment);
            else
                _context.driverAssignments.Add(driverAssignment);
            return 1 == await _context.SaveChangesAsync(); 
        }

        public async Task<bool> DeleteDriverAssignmentById(int id)
        {
            _context.driverAssignments.Remove(_context.driverAssignments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DriverAssignment>> GetDriverAssignmentByVehicleId(int vehicleId)
        {
            return await _context.driverAssignments.Where(x => x.vehicleEntryId == vehicleId).AsNoTracking().ToListAsync();
        }

        //SaveMajor Overhauling

        public async Task<IEnumerable<MajorOverholding>> GetMajorOverhauling()
        {
            return await _context.majorOverholdings.AsNoTracking().ToListAsync();
        }

        public async Task<MajorOverholding> GetMajorOverhaulingById(int id)
        {
            return await _context.majorOverholdings.Where(x => x.vehicleEntryId == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveMajorOverhauling(MajorOverholding majorOverholding)
        {
            if (majorOverholding.Id != 0)
                _context.majorOverholdings.Update(majorOverholding);
            else
                _context.majorOverholdings.Add(majorOverholding);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteMajorOverhaulingById(int id)
        {
            _context.majorOverholdings.Remove(_context.majorOverholdings.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MajorOverholding>> GetMajorOverhaulingByVehicleId(int vehicleId)
        {
            return await _context.majorOverholdings.Where(x => x.vehicleEntryId == vehicleId).AsNoTracking().ToListAsync();
        }
    }
}
