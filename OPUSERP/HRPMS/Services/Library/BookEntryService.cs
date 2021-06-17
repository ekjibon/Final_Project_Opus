using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Library;
using OPUSERP.HRPMS.Services.Library.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Library
{
    public class BookEntryService : IBookEntryService
    {

        private readonly ERPDbContext _context;

        public BookEntryService(ERPDbContext context)
        {
            _context = context;
        }

        //ApplicationForm
        public async Task<bool> DeleteBookEntryById(int id)
        {
            _context.bookEntries.Remove(_context.bookEntries.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookEntry>> GetBookEntry()
        {
            return await _context.bookEntries.Include(x => x.bookCategory).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<BookEntry>> GetBookEntryByOrg(string org)
        {
            return await _context.bookEntries.Include(x => x.bookCategory).Where(x => x.orgType == org).AsNoTracking().ToListAsync();
        }

        public async Task<BookEntry> GetBookEntryById(int id)
        {
            return await _context.bookEntries.Where(x => x.Id == id).Include(x => x.bookCategory).FirstOrDefaultAsync();
        }

        public async Task<string> GetBookNameById(int id)
        {
            BookEntry data = await _context.bookEntries.FindAsync(id);
            return data.bookName;
        }

        public async Task<int> SaveBookEntry(BookEntry bookEntry)
        {
            if (bookEntry.Id != 0)
                _context.bookEntries.Update(bookEntry);
            else
                _context.bookEntries.Add(bookEntry);
            await _context.SaveChangesAsync();
            return bookEntry.Id;
        }
    }
}
