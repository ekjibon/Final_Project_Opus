using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data;
using CLUB.Data.Entity.Master;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;
using CLUB.Areas.Member.Models;


namespace CLUB.Services.MasterData
{
    public class HolidayService : IHolidayService
    {
        private readonly ApplicationDbContext _context;

        public HolidayService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveHoliday(Holiday holiday)
        {
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
