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
    public class TravelRouteService: ITravelRouteService
    {
        private readonly ERPDbContext _context;

        public TravelRouteService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveTravelRoute(TravelRoute travelRoute)
        {
            if (travelRoute.Id != 0)
                _context.travelRoutes.Update(travelRoute);
            else
                _context.travelRoutes.Add(travelRoute);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TravelRoute>> GetTravelRoute()
        {
            return await _context.travelRoutes.Include(x => x.employee).Include(x => x.travelMaster).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TravelRoute>> GetTravelRouteByEmpId(int empId)
        {
            return await _context.travelRoutes.Where(x => x.employeeId == empId && x.isActive == 1).Include(x => x.employee).Include(x => x.travelMaster.employee).Include(x =>x.travelMaster.travelProject).Include(x => x.travelMaster.hRDoner).Include(x => x.travelMaster.hRActivity).AsNoTracking().ToListAsync();
        }

        public async Task<TravelRoute> GetTravelRouteByTravelMasterId(int leaveId)
        {
            return await _context.travelRoutes.Where(x => x.travelMasterId == leaveId && x.isActive == 1).Include(x => x.employee).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<TravelRoute> GetTravelRouteById(int id)
        {

            return await _context.travelRoutes.FindAsync(id);
        }

        public async Task<TravelRoute> GetTravelRouteByRouteOrder(int id, int order)
        {

            return await _context.travelRoutes.Where(x => x.travelMasterId == id && x.routeOrder == order).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteTravelRouteById(int id)
        {
            _context.travelRoutes.Remove(_context.travelRoutes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateTravelRoute(int Id, int Type)
        {
            TravelRoute data = await _context.travelRoutes.FindAsync(Id);
            if (data != null)
            {
                data.isActive = Type;
                _context.travelRoutes.Update(data);
                return 1 == await _context.SaveChangesAsync();
            }
            return false;
        }
    }
}
