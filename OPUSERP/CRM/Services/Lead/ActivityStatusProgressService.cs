using Microsoft.EntityFrameworkCore;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Lead
{

    public class ActivityStatusProgressService : IActivityStatusProgressService
    {
        private readonly ERPDbContext _context;

        public ActivityStatusProgressService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveActivityStatusProgress(ActivityStatusProgress activityStatusProgress)
        {
            if (activityStatusProgress.Id != 0)
                _context.ActivityStatusProgresses.Update(activityStatusProgress);
            else
                _context.ActivityStatusProgresses.Add(activityStatusProgress);
            await _context.SaveChangesAsync();
            return activityStatusProgress.Id;
        }

        public async Task<IEnumerable<ActivityStatusProgress>> GetAllActivityStatusProgressByActivityMasterId(int id)
        {
            List<ActivityStatusProgress> ActivityDetailss = await _context.ActivityStatusProgresses.Include(x=>x.activityMaster).Include(x=>x.activityStatus).Where(x => x.activityMasterId == id).ToListAsync();
            


            return ActivityDetailss;


        }
        public async Task<IEnumerable<ActivityStatusProgress>> GetAllActivityStatusProgressByLeadId(int id)
        {
            List<ActivityStatusProgress> ActivityDetailss = await _context.ActivityStatusProgresses.Include(x => x.activityMaster.leads).Include(x => x.activityStatus).Where(x => x.activityMaster.leadsId == id).ToListAsync();



            return ActivityDetailss;


        }



        public async Task<bool> DeleteActivityStatusProgressById(int id)
        {
            _context.ActivityStatusProgresses.Remove(_context.ActivityStatusProgresses.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteActivityStatusProgressByMasterId(int id)
        {
            _context.ActivityStatusProgresses.RemoveRange(_context.ActivityStatusProgresses.Where(x=>x.activityMasterId==id));
            return 1 == await _context.SaveChangesAsync();
        }


    }
}
