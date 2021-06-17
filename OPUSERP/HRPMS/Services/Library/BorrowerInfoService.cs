using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Library;
using OPUSERP.HRPMS.Services.Library.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Library
{
    public class BorrowerInfoService : IBorrowerInfoService
    {
        private readonly ERPDbContext _context;

        public BorrowerInfoService(ERPDbContext context)
        {
            _context = context;
        }

        //ApplicationForm
        public async Task<bool> DeleteBorrowerInfoById(int id)
        {
            _context.borrowerInfos.Remove(_context.borrowerInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BorrowerInfo>> GetBorrowerInfo()
        {
            return await _context.borrowerInfos.Include(x=>x.borrower).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<BorrowerInfo>> GetBorrowerInfoByBookId(int bookId)
        {
            return await _context.borrowerInfos.Include(x => x.borrower).Where(x => x.bookEntryId == bookId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<BorrowerInfo>> GetBorrowerInfoByEmpId(int empId)
        {
            return await _context.borrowerInfos.Include(x => x.bookEntry).Where(x => x.borrowerId == empId).AsNoTracking().ToListAsync();
        }

        public async Task<BorrowerInfo> GetBorrowerInfoById(int id)
        {
            return await _context.borrowerInfos.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveBorrowerInfo(BorrowerInfo borrowerInfo)
        {
            if (borrowerInfo.Id != 0)
                _context.borrowerInfos.Update(borrowerInfo);
            else
                _context.borrowerInfos.Add(borrowerInfo);
            await _context.SaveChangesAsync();
            return borrowerInfo.Id;
        }
    }
}
