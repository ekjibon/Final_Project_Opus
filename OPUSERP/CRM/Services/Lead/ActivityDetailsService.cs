using Microsoft.EntityFrameworkCore;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Lead
{

    public class ActivityDetailsService : IActivityDetailsService
    {
        private readonly ERPDbContext _context;

        public ActivityDetailsService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveActivityDetails(ActivityDetails ActivityDetails)
        {
            if (ActivityDetails.Id != 0)
                _context.ActivityDetails.Update(ActivityDetails);
            else
                _context.ActivityDetails.Add(ActivityDetails);
            await _context.SaveChangesAsync();
            return ActivityDetails.Id;
        }

        public async Task<IEnumerable<ActivityDetails>> GetAllActivityDetailsByActivityMasterId(int id)
        {
            List<ActivityDetails> ActivityDetailss = new List<ActivityDetails>();
            List<int> contactIds =  _context.ActivityDetails.Include(x=>x.activityMaster.leads).Where(x => x.activityMaster.Id == id).Select(x => x.contactsId).ToList();

            List<Resource> Resources = _context.Resources.Where(x => contactIds.Contains(x.Id)).Include(x=>x.departments).Include(x=>x.designations).ToList();

          IEnumerable<ActivityDetails> activityDetailslist=  await _context.ActivityDetails.Where(x=>x.activityMaster.Id==id).Include(x=>x.activityMaster).ToListAsync();
            foreach(ActivityDetails data in activityDetailslist )
            {
                var resource = Resources.Where(x => x.Id == data.contactsId).FirstOrDefault();
                ActivityDetailss.Add(new ActivityDetails {
                    activityMasterId=data.activityMasterId,
                    contactsId=data.contactsId,
                    contactName=resource.resourceName

                });

            }

            return ActivityDetailss;


        }

        public async Task<ActivityDetails> GetActivityDetailsById(int id)
        {
            return await _context.ActivityDetails.FindAsync(id);
        }

        public async Task<bool> DeleteActivityDetailsById(int id)
        {
            _context.ActivityDetails.Remove(_context.ActivityDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteActivityDetailsByMasterId(int id)
        {
            _context.ActivityDetails.RemoveRange(_context.ActivityDetails.Where(x=>x.activityMasterId==id));
            return 1 == await _context.SaveChangesAsync();
        }


    }
}
