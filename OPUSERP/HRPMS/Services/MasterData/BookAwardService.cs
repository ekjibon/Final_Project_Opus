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
    public class BookAwardService : IBookAwardService
    {
        private readonly ERPDbContext _context;

        public BookAwardService(ERPDbContext context)
        {
            _context = context;
        }
        //Book Info

        public async Task<IEnumerable<BookCategory>> GetBookCategory()
        {
            return await _context.bookCategories.AsNoTracking().ToListAsync();
        }

        public async Task<BookCategory> GetBookCategoryById(int id)
        {
            return await _context.bookCategories.FindAsync(id);
        }

        public async Task<bool> SaveBookCategory(BookCategory bookCategory)
        {
            if (bookCategory.Id != 0)
                _context.bookCategories.Update(bookCategory);
            else
                _context.bookCategories.Add(bookCategory);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteBookCategoryById(int id)
        {
            _context.bookCategories.Remove(_context.bookCategories.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        //Award Info

        public async Task<bool> SaveAwardInfo(Award award)
        {
            if (award.Id != 0)
                _context.awards.Update(award);
            else
                _context.awards.Add(award);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Award>> GetAwardInfo()
        {
            return await _context.awards.AsNoTracking().ToListAsync();
        }

        public async Task<Award> GetAwardInfoById(int id)
        {
            return await _context.awards.FindAsync(id);
        }

        public async Task<bool> DeleteAwardInfoById(int id)
        {
            _context.awards.Remove(_context.awards.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
