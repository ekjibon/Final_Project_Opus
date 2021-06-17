using OPUSERP.HRPMS.Data.Entity.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Library.Interfaces
{
    public interface IBookEntryService
    {
        // ApplicationForm
        Task<int> SaveBookEntry(BookEntry bookEntry);
        Task<IEnumerable<BookEntry>> GetBookEntry();
        Task<IEnumerable<BookEntry>> GetBookEntryByOrg(string org);
        Task<BookEntry> GetBookEntryById(int id);
        Task<bool> DeleteBookEntryById(int id);
        Task<string> GetBookNameById(int id);
    }
}
