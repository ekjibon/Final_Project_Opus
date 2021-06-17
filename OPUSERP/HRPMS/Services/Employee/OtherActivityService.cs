using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee
{
    public class OtherActivityService: IOtherActivityService
    {
        private readonly ERPDbContext _context;

        public OtherActivityService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteOtherActivityById(int id)
        {
            _context.otherActivities.Remove(_context.otherActivities.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OtherActivity>> GetOtherActivity()
        {
            return await _context.otherActivities.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<OtherActivity>> GetOtherActivityByEmpId(int empId)
        {
            return await _context.otherActivities.Where(x => x.employeeID == empId).Include(x => x.activityName.activityType).AsNoTracking().ToListAsync();
        }

        public async Task<OtherActivity> GetOtherActivityById(int id)
        {
            return await _context.otherActivities.FindAsync(id);
        }

        public async Task<bool> SaveOtherActivity(OtherActivity otherActivity)
        {
            if (otherActivity.Id != 0)
                _context.otherActivities.Update(otherActivity);
            else
                _context.otherActivities.Add(otherActivity);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
