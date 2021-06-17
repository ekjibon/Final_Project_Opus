using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using OPUSERP.FixedAsset.Services.AssetRegister.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.FixedAsset.Services.AssetRegister
{
    public class AssetRegistrationService : IAssetRegistrationService
    {
        private readonly ERPDbContext _context;

        public AssetRegistrationService(ERPDbContext context)
        {
            _context = context;
        }

        #region  Asset Registration

        public async Task<int> SaveAssetRegistration(AssetRegistration assetRegistration)
        {
            if (assetRegistration.Id != 0)
                _context.AssetRegistration.Update(assetRegistration);
            else
                _context.AssetRegistration.Add(assetRegistration);
            await _context.SaveChangesAsync();
            return assetRegistration.Id;
        }

        public async Task<IEnumerable<AssetRegistration>> GetAllAssetRegistration()
        {
            return await _context.AssetRegistration.Include(x => x.purchaseInfo).Include(x => x.itemSpecification.Item.parent).OrderByDescending(a => a.Id).ToListAsync();
        }

        public async Task<IEnumerable<AssetRegistration>> GetAllAssetRegistrationbypurchaseId(int Id)
        {
            return await _context.AssetRegistration.Where(x => x.purchaseInfoId == Id).ToListAsync();
        }
        public async Task<IEnumerable<AssetRegistration>> GetAllAssetRegistrationbycatId(int Id)
        {
            return await _context.AssetRegistration.Where(x => x.itemSpecification.Item.parentId == Id).ToListAsync();
        }
        public async Task<IEnumerable<AssetRegistration>> GetAllAssetRegistrationbycatIdForDepreciation(int Id)
        {
            List<int?> assetIds = _context.AssetRetirements.Select(x => x.assetRegistrationId).ToList();
            return await _context.AssetRegistration.Where(x => x.depriciationRateId == Id && !assetIds.Contains(x.Id)).ToListAsync();
            //return await _context.AssetRegistration.Where(x => x.itemSpecification.Item.parentId == Id && !assetIds.Contains(x.Id)).ToListAsync();
        }

        public async Task<AssetRegistration> GetAssetRegistrationById(int id)
        {
            return await _context.AssetRegistration.Include(a => a.purchaseInfo).Include(a => a.itemSpecification).Include(a => a.itemSpecification.Item).Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<AssetRegistration>> GetAssetRegistrationBypurchaseMasterId(int id)
        {
            return await _context.AssetRegistration.Include(a => a.purchaseInfo).Include(a => a.itemSpecification).Include(a => a.itemSpecification.Item).Where(x => x.purchaseInfo.purchaseOrderMasterId == id).ToListAsync();
        }

        public async Task<bool> DeleteAssetRegistrationById(int id)
        {
            _context.AssetRegistration.Remove(_context.AssetRegistration.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAssetRegistrationByPurchaseId(int id)
        {
            _context.AssetRegistration.RemoveRange(_context.AssetRegistration.Where(x => x.purchaseInfoId == id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region  Asset Overhauling

        public async Task<int> SaveAssetOverhauling(AssetOverhauling assetOverhauling)
        {
            if (assetOverhauling.Id != 0)
                _context.AssetOverhaulings.Update(assetOverhauling);
            else
                _context.AssetOverhaulings.Add(assetOverhauling);
            await _context.SaveChangesAsync();
            return assetOverhauling.Id;
        }

        public async Task<IEnumerable<AssetOverhauling>> GetAllAssetOverhauling()
        {
            return await _context.AssetOverhaulings.Include(x => x.assetRegistration.purchaseInfo).Include(x => x.assetRegistration.itemSpecification.Item.parent).OrderByDescending(a => a.Id).ToListAsync();
        }

        public async Task<bool> DeleteAssetOverhaulingById(int id)
        {
            _context.AssetOverhaulings.Remove(_context.AssetOverhaulings.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

    }
}
