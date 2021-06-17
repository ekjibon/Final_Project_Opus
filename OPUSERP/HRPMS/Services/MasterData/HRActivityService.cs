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
    public class HRActivityService : IHRActivityService
    {
        private readonly ERPDbContext _context;

        public HRActivityService(ERPDbContext context)
        {
            _context = context;
        }

        #region HR Activity

        public async Task<bool> SaveHRActivity(HRActivity hRActivity)
        {
            if (hRActivity.Id != 0)
                _context.hRActivities.Update(hRActivity);
            else
                _context.hRActivities.Add(hRActivity);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<HRActivity>> GetHRActivity()
        {
            return await _context.hRActivities.AsNoTracking().ToListAsync();
        }

        public async Task<HRActivity> GetHRActivityById(int id)
        {

            return await _context.hRActivities.FindAsync(id);
        }

        public async Task<bool> DeleteHRActivityById(int id)
        {
            _context.hRActivities.Remove(_context.hRActivities.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region HR Facility

        public async Task<bool> SaveHRFacility(HRFacility hRFacility)
        {
            if (hRFacility.Id != 0)
                _context.hRFacilities.Update(hRFacility);
            else
                _context.hRFacilities.Add(hRFacility);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<HRFacility>> GetHRFacility()
        {
            return await _context.hRFacilities.AsNoTracking().ToListAsync();
        }

        public async Task<HRFacility> GetHRFacilityById(int id)
        {
            return await _context.hRFacilities.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteHRFacilityById(int id)
        {
            _context.hRFacilities.Remove(_context.hRFacilities.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion
    }
}
