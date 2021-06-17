using Microsoft.EntityFrameworkCore;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData
{
    public class ActivityTypeService : IActivityTypeService
    {
        private readonly ERPDbContext _context;

        public ActivityTypeService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveActivityType(ActivityType activityType)
        {
            if (activityType.Id != 0)
                _context.ActivityTypes.Update(activityType);
            else
                _context.ActivityTypes.Add(activityType);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ActivityType>> GetAllActivityType()
        {
            return await _context.ActivityTypes.ToListAsync();
        }

        public async Task<ActivityType> GetActivityTypeById(int id)
        {
            return await _context.ActivityTypes.FindAsync(id);
        }

        public async Task<bool> DeleteActivityTypeById(int id)
        {
            _context.ActivityTypes.Remove(_context.ActivityTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

       
    }
}
