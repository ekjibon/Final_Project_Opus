using CLUB.Data;
using CLUB.Data.Entity.Master;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.MasterData
{
    public class EventCategoryService: IEventCategoryService
    {
        private readonly ApplicationDbContext _context;

        public EventCategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveEventCategory(EventCategory eventCategory)
        {
            if (eventCategory.Id != 0)
                _context.eventCategories.Update(eventCategory);
            else
                _context.eventCategories.Add(eventCategory);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EventCategory>> GetAllEventCategory()
        {
            return await _context.eventCategories.AsNoTracking().ToListAsync();
        }

        public async Task<EventCategory> GetEventCategoryById(int id)
        {
            return await _context.eventCategories.FindAsync(id);
        }

        public async Task<bool> DeleteEventCategoryById(int id)
        {
            _context.eventCategories.Remove(_context.eventCategories.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }


    }
}
