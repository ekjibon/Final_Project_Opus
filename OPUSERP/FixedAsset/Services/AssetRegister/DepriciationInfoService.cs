using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using OPUSERP.FixedAsset.Services.AssetRegister.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.FixedAsset.Services.AssetRegister
{
    public class DepriciationInfoService : IDepriciationInfoService
    {
        private readonly ERPDbContext _context;

        public DepriciationInfoService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveDepriciationInfo(DepriciationInfo depriciationInfo)
        { 
            if (depriciationInfo.Id != 0)
                _context.DepriciationInfo.Update(depriciationInfo);
            else
                _context.DepriciationInfo.Add(depriciationInfo);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DepriciationInfo>> GetAllDepriciationInfo()
        {
            return await _context.DepriciationInfo.ToListAsync();
        }
        public async Task<DepriciationInfo> GetAllDepriciationInfobyassetId(int Id)
        {
            return await _context.DepriciationInfo.Where(x=>x.assetRegistrationId==Id).FirstOrDefaultAsync();
        }

        public async Task<DepriciationInfo> GetDepriciationInfoById(int id)
        {
            return await _context.DepriciationInfo.FindAsync(id);
        }

        public async Task<bool> DeleteDepriciationInfoById(int id)
        {
            _context.DepriciationInfo.Remove(_context.DepriciationInfo.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteDepriciationInfoByAssetId(int id)
        {
            _context.DepriciationInfo.RemoveRange(_context.DepriciationInfo.Where(x=>x.assetRegistrationId==id));
            return 1 == await _context.SaveChangesAsync();
        }


    }
}
