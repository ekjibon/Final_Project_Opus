using Microsoft.EntityFrameworkCore;
using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Accounting.Services.MasterData.Interfaces;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Services.MasterData
{
    public class FundSourceService : IFundSourceService
    {
        private readonly ERPDbContext _context;

        public FundSourceService(ERPDbContext context)
        {
            _context = context;
        }

        #region Fund Source

        public async Task<IEnumerable<FundSource>> GetFundSource()
        {
        
            return await _context.FundSources.AsNoTracking().ToListAsync();
        }

        public async Task<FundSource > GetFundSourceById(int id)
        {
            return await _context.FundSources.FindAsync(id);
        }

        public async Task<bool> SaveFundSource(FundSource fundSource)
        {
            if (fundSource .Id != 0)
                _context.FundSources .Update(fundSource );
            else
                _context.FundSources.Add(fundSource);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteFundSourceById(int id)
        {
            _context.FundSources .Remove(_context.FundSources.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

     
    }
}
