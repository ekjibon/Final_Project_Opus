using Microsoft.EntityFrameworkCore;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data;
using OPUSERP.FixedAsset.Data.Entity.MasterData;
using OPUSERP.FixedAsset.Services.MasterData.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.FixedAsset.Services.MasterData
{
    public class DepriciationPeriodService : IDepriciationPeriodService
    {
        private readonly ERPDbContext _context;

        public DepriciationPeriodService(ERPDbContext context)
        {
            _context = context;
        }

        #region Depriciation Period
        public async Task<bool> SaveDepriciationPeriod(DepriciationPeriod depriciationPeriod)
        {
            if (depriciationPeriod.Id != 0)
                _context.DepriciationPeriods.Update(depriciationPeriod);
            else
                _context.DepriciationPeriods.Add(depriciationPeriod);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DepriciationPeriod>> GetAllDepriciationPeriod()
        {
            return await _context.DepriciationPeriods.ToListAsync();
        }

        public async Task<DepriciationPeriod> GetDepriciationPeriodById(int id)
        {
            return await _context.DepriciationPeriods.Include(x => x.Year).Where(x => x.Id == id).FirstOrDefaultAsync(); ;
        }

        public async Task<bool> DeleteDepriciationPeriodById(int id)
        {
            _context.DepriciationPeriods.Remove(_context.DepriciationPeriods.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
        
        #region Asset Year
        public async Task<int> SaveAssetYear(AssetYear assetYear)
        {
            if (assetYear.Id != 0)
                _context.AssetYears.Update(assetYear);
            else
                _context.AssetYears.Add(assetYear);
            await _context.SaveChangesAsync();            
            return assetYear.Id;
        }

        public async Task<IEnumerable<AssetYear>> GetAllAssetYear()
        {
            return await _context.AssetYears.ToListAsync();
        }

        public async Task<AssetYear> GetAssetYearById(int id)
        {
            return await _context.AssetYears.Where(x => x.Id == id).FirstOrDefaultAsync(); ;
        }

        public async Task<bool> DeleteAssetYearById(int id)
        {
            _context.AssetYears.Remove(_context.AssetYears.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion


    }
}
