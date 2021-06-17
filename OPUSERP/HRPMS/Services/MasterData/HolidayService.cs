using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.HRPMSEmployee.Models;


namespace OPUSERP.HRPMS.Services.MasterData
{
    public class HolidayService : IHolidayService
    {
        private readonly ERPDbContext _context;

        public HolidayService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveHoliday(Holiday holiday)
        {
            if (holiday.Id != 0)
                _context.holidays.Update(holiday);
            else
                _context.holidays.Add(holiday);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Holiday>> GetAllHoliday()
        {
            return await _context.holidays.AsNoTracking().ToListAsync();
        }

        public async Task<Holiday> GetHolidayById(int id)
        {
            return await _context.holidays.FindAsync(id);
        }

        public async Task<bool> DeleteHolidayById(int id)
        {
            _context.holidays.Remove(_context.holidays.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        
    }
}
