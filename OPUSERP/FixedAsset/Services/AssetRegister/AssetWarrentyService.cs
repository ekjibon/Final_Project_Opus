using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using OPUSERP.FixedAsset.Services.AssetRegister.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.FixedAsset.Services.AssetRegister
{
    public class AssetWarrentyService : IAssetWarrentyService
    {
        private readonly ERPDbContext _context;

        public AssetWarrentyService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveAssetWarrenty(AssetWarrenty assetWarrenty)
        { 
            if (assetWarrenty.Id != 0)
                _context.AssetWarrenties.Update(assetWarrenty);
            else
                _context.AssetWarrenties.Add(assetWarrenty);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AssetWarrenty>> GetAllAssetWarrenty()
        {
            return await _context.AssetWarrenties.ToListAsync();
        }
        public async Task<IEnumerable<AssetWarrenty>> GetAllAssetWarrentybyassetId(int Id)
        {
            return await _context.AssetWarrenties.Where(x=>x.assetRegistrationId==Id).ToListAsync();
        }

        public async Task<AssetWarrenty> GetAssetWarrentyById(int id)
        {
            return await _context.AssetWarrenties.FindAsync(id);
        }

        public async Task<bool> DeleteAssetWarrentyById(int id)
        {
            _context.AssetWarrenties.Remove(_context.AssetWarrenties.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAssetWarrentyByassetId(int id)
        {
            _context.AssetWarrenties.RemoveRange(_context.AssetWarrenties.Where(x=>x.assetRegistrationId==id));
            return 1 == await _context.SaveChangesAsync();
        }


    }
}
