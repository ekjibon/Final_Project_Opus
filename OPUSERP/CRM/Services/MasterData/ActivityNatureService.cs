using Microsoft.EntityFrameworkCore;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData
{
    public class ActivityNatureService : IActivityNatureService
    {
        private readonly ERPDbContext _context;

        public ActivityNatureService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveActivityNature(ActivityNature activityNature)
        {
            if (activityNature.Id != 0)
                _context.ActivityNatures.Update(activityNature);
            else
                _context.ActivityNatures.Add(activityNature);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ActivityNature>> GetAllActivityNature()
        {
            return await _context.ActivityNatures.ToListAsync();
        }

        public async Task<ActivityNature> GetActivityNatureById(int id)
        {
            return await _context.ActivityNatures.FindAsync(id);
        }

        public async Task<bool> DeleteActivityNatureById(int id)
        {
            _context.ActivityNatures.Remove(_context.ActivityNatures.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

       
    }
}
