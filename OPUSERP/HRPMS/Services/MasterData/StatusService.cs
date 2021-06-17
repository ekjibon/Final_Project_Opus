using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace OPUSERP.HRPMS.Services.MasterData
{
    public class StatusService : IStatusService
    {
        private readonly ERPDbContext _context;

        public StatusService(ERPDbContext context)
        {
            _context = context;
        }

        #region Activity Status
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



        #endregion 

        #region ServiceStatus
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
            if (serviceStatus.Id != 0)
                _context.serviceStatuses.Update(serviceStatus);
            else
                _context.serviceStatuses.Add(serviceStatus);
            return 1 == await _context.SaveChangesAsync();
        }


        #endregion 

        #region HrProgram
        public async Task<bool> DeleteHrProgramById(int id)
        {
            _context.hrPrograms.Remove(_context.hrPrograms.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<HrProgram>> GetHrProgram()
        {
            return await _context.hrPrograms.AsNoTracking().ToListAsync();
        }

        public async Task<HrProgram> GetHrProgramById(int id)
        {
            return await _context.hrPrograms.FindAsync(id);
        }

        public async Task<bool> SaveHrProgram(HrProgram hrProgram)
        {
            if (hrProgram.Id != 0)
                _context.hrPrograms.Update(hrProgram);
            else
                _context.hrPrograms.Add(hrProgram);
            return 1 == await _context.SaveChangesAsync();
        }


        #endregion

        #region HrUnit
        public async Task<bool> DeleteHrUnitById(int id)
        {
            _context.hrUnits.Remove(_context.hrUnits.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<HrUnit>> GetHrUnit()
        {
            return await _context.hrUnits.AsNoTracking().ToListAsync();
        }

        public async Task<HrUnit> GetHrUnitById(int id)
        {
            return await _context.hrUnits.FindAsync(id);
        }

        public async Task<bool> SaveHrUnit(HrUnit hrUnit)
        {
            if (hrUnit.Id != 0)
                _context.hrUnits.Update(hrUnit);
            else
                _context.hrUnits.Add(hrUnit);
            return 1 == await _context.SaveChangesAsync();
        }


        #endregion

    }
}
