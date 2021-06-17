using Microsoft.EntityFrameworkCore;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData
{
    public class ActivityCategoryService : IActivityCategoryService
    {
        private readonly ERPDbContext _context;

        public ActivityCategoryService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveActivityCategory(ActivityCategory activityCategory)
        {
            if (activityCategory.Id != 0)
                _context.ActivityCategories.Update(activityCategory);
            else
                _context.ActivityCategories.Add(activityCategory);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ActivityCategory>> GetAllActivityCategory()
        {
            return await _context.ActivityCategories.ToListAsync();
        }

        public async Task<ActivityCategory> GetActivityCategoryById(int id)
        {
            return await _context.ActivityCategories.FindAsync(id);
        }

        public async Task<bool> DeleteActivityCategoryById(int id)
        {
            _context.ActivityCategories.Remove(_context.ActivityCategories.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

       
    }
}
