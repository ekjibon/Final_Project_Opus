using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData
{
    public class ActivityNameService: IActivityNameService
    {
        private readonly ERPDbContext _context;

        public ActivityNameService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveActivityName(ActivityName activityName)
        {
            if (activityName.Id != 0)
                _context.activityNames.Update(activityName);
            else
                _context.activityNames.Add(activityName);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ActivityName>> GetActivityName()
        {
            return await _context.activityNames.AsNoTracking().Include(x=>x.activityType).ToListAsync();
        }

        public async Task<IEnumerable<ActivityName>> GetActivityNameByType(int typeId)
        {
            return await _context.activityNames.AsNoTracking().Include(x => x.activityType).Where(x=>x.activityTypeId == typeId).ToListAsync();
        }

        public async Task<ActivityName> GetActivityNameById(int id)
        {
            return await _context.activityNames.FindAsync(id);
        }

        public async Task<bool> DeleteActivityNameById(int id)
        {
            _context.activityNames.Remove(_context.activityNames.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
