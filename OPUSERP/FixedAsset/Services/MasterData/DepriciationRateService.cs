using Microsoft.EntityFrameworkCore;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data;
using OPUSERP.FixedAsset.Data.Entity.MasterData;
using OPUSERP.FixedAsset.Services.MasterData.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using OPUSERP.SCM.Data.Entity.MasterData;

namespace OPUSERP.FixedAsset.Services.MasterData
{
    public class DepriciationRateService : IDepriciationRateService
    {
        private readonly ERPDbContext _context;

        public DepriciationRateService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveDepriciationRate(DepriciationRate depriciationRate)
        {
            if (depriciationRate.Id != 0)
                _context.DepriciationRates.Update(depriciationRate);
            else
                _context.DepriciationRates.Add(depriciationRate);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DepriciationRate>> GetAllDepriciationRate()
        {
            return await _context.DepriciationRates.AsNoTracking().ToListAsync();
        }

        public async Task<DepriciationRate> GetDepriciationRateById(int id)
        {
            return await _context.DepriciationRates.FindAsync(id);
        }
        public async Task<DepriciationRate> GetDepriciationRateByCatId(int id)
        {
            return await _context.DepriciationRates.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteDepriciationRateById(int id)
        {
            _context.DepriciationRates.Remove(_context.DepriciationRates.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<ItemCategory>> GetItemCategoryForDepRate()
        {
            return await _context.ItemCategories.Where(x => x.categoryDescription.Contains("FIXED ASSET") ).AsNoTracking().ToListAsync();

        }

    }
}
