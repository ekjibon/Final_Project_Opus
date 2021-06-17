using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data;
using CLUB.Data.Entity.Master;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CLUB.Services.MasterData
{
    public class BookAwardService : IBookAwardService
    {
        private readonly ApplicationDbContext _context;

        public BookAwardService(ApplicationDbContext context)
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

        public async Task<int> GetAwardCheck(int id, string name)
        {
            var Result = await _context.awards.Where(x => x.awardName == name && x.Id != id).FirstOrDefaultAsync();
            if (Result == null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
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
