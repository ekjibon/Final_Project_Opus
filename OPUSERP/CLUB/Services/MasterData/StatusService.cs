using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data;
using CLUB.Data.Entity.Master;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;

namespace CLUB.Services.MasterData
{
    public class StatusService : IStatusService
    {
        private readonly ERPDbContext _context;

        public StatusService(ERPDbContext context)
        {
            _context = context;
        }

        //ActivityStatus
        public async Task<bool> DeleteActivityStatusById(int id)
        {
            _context.activityStatuses.Remove(_context.activityStatuses.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ActivityStatus>> GetActivityStatus()
        {
            return await _context.activityStatuses.AsNoTracking().ToListAsync();
        }

        public async Task<ActivityStatus> GetActivityStatusById(int id)
        {
            return await _context.activityStatuses.FindAsync(id);
        }

        public async Task<bool> SaveActivityStatus(ActivityStatus activityStatuse)
        {
            if (activityStatuse.Id != 0)
                _context.activityStatuses.Update(activityStatuse);
            else
                _context.activityStatuses.Add(activityStatuse);

            return 1 == await _context.SaveChangesAsync();
        }

        //ServiceStatus
        public async Task<bool> DeleteServiceStatusById(int id)
        {
            _context.serviceStatuses.Remove(_context.serviceStatuses.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ServiceStatus>> GetServiceStatus()
        {
            return await _context.serviceStatuses.AsNoTracking().ToListAsync();
        }

        public async Task<ServiceStatus> GetServiceStatusById(int id)
        {
            return await _context.serviceStatuses.FindAsync(id);
        }

        public async Task<bool> SaveServiceStatus(ServiceStatus serviceStatus)
        {
            if(serviceStatus.Id != 0)
                _context.serviceStatuses.Update(serviceStatus);
            else
                _context.serviceStatuses.Add(serviceStatus);
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
