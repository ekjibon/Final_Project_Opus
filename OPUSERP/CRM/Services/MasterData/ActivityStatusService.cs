using Microsoft.EntityFrameworkCore;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData
{
    public class ActivityStatusService : IActivityStatusService
    {
        private readonly ERPDbContext _context;

        public ActivityStatusService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveActivityStatus(ActivityStatus activityStatus)
        {
            if (activityStatus.Id != 0)
                _context.ActivityStatuses.Update(activityStatus);
            else
                _context.ActivityStatuses.Add(activityStatus);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ActivityStatus>> GetAllActivityStatus()
        {
            return await _context.ActivityStatuses.ToListAsync();
        }

        public async Task<ActivityStatus> GetActivityStatusById(int id)
        {
            return await _context.ActivityStatuses.FindAsync(id);
        }

        public async Task<bool> DeleteActivityStatusById(int id)
        {
            _context.ActivityStatuses.Remove(_context.ActivityStatuses.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

       
    }
}
