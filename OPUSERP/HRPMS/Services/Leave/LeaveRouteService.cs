using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Leave;
using OPUSERP.HRPMS.Services.Leave.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Leave
{
    public class LeaveRouteService : ILeaveRouteService
    {
        private readonly ERPDbContext _context;

        public LeaveRouteService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveLeaveRoute(LeaveRoute leaveRoute)
        {
            if (leaveRoute.Id != 0)
                _context.leaveRoutes.Update(leaveRoute);
            else
                _context.leaveRoutes.Add(leaveRoute);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LeaveRoute>> GetLeaveRoute()
        {
            return await _context.leaveRoutes.Include(x => x.employee).Include(x => x.leaveRegister).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<LeaveRoute>> GetLeaveRouteByEmpId(int empId)
        {
            return await _context.leaveRoutes.Include(x => x.leaveRegister.employee.department).Include(x => x.leaveRegister.leaveType).Where(x => x.employeeId == empId && x.isActive == 1).AsNoTracking().ToListAsync();
        }

        public async Task<LeaveRoute> GetLeaveRouteByLeaveRegisterId(int leaveId)
        {
            return await _context.leaveRoutes.Where(x => x.leaveRegisterId == leaveId && x.isActive == 1).Include(x => x.employee).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<LeaveRoute> GetLeaveRouteById(int id)
        {

            return await _context.leaveRoutes.FindAsync(id);
        }

        public async Task<LeaveRoute> GetLeaveRouteByRouteOrder(int id, int order)
        {

            return await _context.leaveRoutes.Where(x => x.leaveRegisterId == id && x.routeOrder == order).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteLeaveRouteById(int id)
        {
            _context.leaveRoutes.Remove(_context.leaveRoutes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateLeaveRoute(int Id, int Type)
        {
            LeaveRoute data = await _context.leaveRoutes.FindAsync(Id);
            if (data != null)
            {
                data.isActive = Type;
                _context.leaveRoutes.Update(data);
                return 1 == await _context.SaveChangesAsync();
            }
            return false;
        }
    }
}
