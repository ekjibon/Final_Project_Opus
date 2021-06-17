using Microsoft.EntityFrameworkCore;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData
{
    public class ActivitySessionService : IActivitySessionService
    {
        private readonly ERPDbContext _context;

        public ActivitySessionService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveActivitySession(ActivitySession activitySession)
        {
            if (activitySession.Id != 0)
                _context.ActivitySessions.Update(activitySession);
            else
                _context.ActivitySessions.Add(activitySession);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ActivitySession>> GetAllActivitySession()
        {
            return await _context.ActivitySessions.ToListAsync();
        }

        public async Task<ActivitySession> GetActivitySessionById(int id)
        {
            return await _context.ActivitySessions.FindAsync(id);
        }

        public async Task<bool> DeleteActivitySessionById(int id)
        {
            _context.ActivitySessions.Remove(_context.ActivitySessions.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

       
    }
}
