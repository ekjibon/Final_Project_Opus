using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Auth.Models;
using OPUSERP.Data;
using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using OPUSERP.FixedAsset.Services.AssetRegister.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.FixedAsset.Services.AssetRegister
{
    public class AssetAssignService : IAssetAssignService
    {
        private readonly ERPDbContext _context;

        public AssetAssignService(ERPDbContext context)
        {
            _context = context;
        }

        #region Asset Assign

        public async Task<int> SaveAssetAssign(AssetAssign assetAssign)
        {
            if (assetAssign.Id != 0)
                _context.AssetAssigns.Update(assetAssign);
            else
                _context.AssetAssigns.Add(assetAssign);
            await _context.SaveChangesAsync();
            return assetAssign.Id;
        }

        public async Task<IEnumerable<AssetAssign>> GetAllAssetAssign()
        {
            return await _context.AssetAssigns.Include(a => a.assetRegistration).Include(a => a.department).Include(a => a.deliveryLocation).OrderByDescending(a => a.Id).ToListAsync();
        }

        public async Task<AssetAssign> GetAssetAssignById(int id)
        {
            return await _context.AssetAssigns.Include(a => a.assetRegistration).Include(a => a.department).Include(a => a.deliveryLocation).Where(a=>a.Id==id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAssetAssignById(int id)
        {
            _context.AssetAssigns.Remove(_context.AssetAssigns.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AssetRegistration>> GetAllUnassignedList()
        {
            List<int?> assetIds = _context.AssetAssigns.Select(x => x.assetRegistrationId).ToList();

            return await _context.AssetRegistration.Where(x => !assetIds.Contains(x.Id)).Include(a => a.purchaseInfo).Include(a => a.purchaseInfo.supplier).OrderByDescending(a => a.Id).ToListAsync();
        }

        public async Task<IEnumerable<EmployeeInfoViewModel>> GetAllEmployeesList()
        {
            return await _context.employeeInfoViewModels.FromSql(@"sp_Get_PayrollEmployeeInfo").AsNoTracking().ToListAsync();
        }

        #endregion

        #region Asset Transfer

        public async Task<int> SaveAssetTransfer(AssetTransfer assetTransfer)
        {
            if (assetTransfer.Id != 0)
                _context.AssetTransfers.Update(assetTransfer);
            else
                _context.AssetTransfers.Add(assetTransfer);
            await _context.SaveChangesAsync();
            return assetTransfer.Id;
        }

        public async Task<IEnumerable<AssetTransfer>> GetAllAssetTransfer()
        {
            return await _context.AssetTransfers.Include(a => a.assetRegistration).Include(a => a.deliveryLocation).OrderByDescending(a => a.Id).ToListAsync();
        }

        public async Task<AssetTransfer> GetAssetTransferById(int id)
        {
            return await _context.AssetTransfers.Include(a => a.assetRegistration).Include(a => a.deliveryLocation).Where(a => a.Id == id).FirstOrDefaultAsync();
        }
      

        public async Task<IEnumerable<AssetRegistration>> GetNotTransferList()
        {
            List<int?> assetIds = _context.AssetTransfers.Select(x => x.assetRegistrationId).ToList();

            return await _context.AssetRegistration.Where(x => !assetIds.Contains(x.Id)).Include(a => a.purchaseInfo).Include(a => a.purchaseInfo.supplier).OrderByDescending(a => a.Id).ToListAsync();
        }

        #endregion

        #region Asset Receive

        public async Task<int> SaveAssetReceive(AssetReceive assetReceive)
        {
            if (assetReceive.Id != 0)
                _context.AssetReceives.Update(assetReceive);
            else
                _context.AssetReceives.Add(assetReceive);
            await _context.SaveChangesAsync();
            return assetReceive.Id;
        }

        public async Task<IEnumerable<AssetReceive>> GetAllAssetReceive()
        {
            return await _context.AssetReceives.Include(a => a.assetRegistration).OrderByDescending(a => a.Id).ToListAsync();
        }

        public async Task<AssetReceive> GetAssetReceiveById(int id)
        {
            return await _context.AssetReceives.Include(a => a.assetRegistration).Where(a => a.Id == id).FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<AssetRegistration>> GetNotAssetReceiveList()
        {
            List<int?> receiveIds = _context.AssetReceives.Select(x => x.assetRegistrationId).ToList();
            List<int?> rejectIds = _context.AssetRejects.Select(x => x.assetRegistrationId).ToList();

            List<int?> assignIds = _context.AssetAssigns.Select(x => x.assetRegistrationId).ToList();
            List<int?> transferIds = _context.AssetTransfers.Select(x => x.assetRegistrationId).ToList();

            return await _context.AssetRegistration.Where(x => (assignIds.Contains(x.Id) || transferIds.Contains(x.Id)) && !receiveIds.Contains(x.Id) && !rejectIds.Contains(x.Id)).Include(a => a.purchaseInfo).Include(a => a.purchaseInfo.supplier).OrderByDescending(a => a.Id).ToListAsync();
        }

        #endregion

        #region Asset Reject

        public async Task<int> SaveAssetReject(AssetReject assetReject)
        {
            if (assetReject.Id != 0)
                _context.AssetRejects.Update(assetReject);
            else
                _context.AssetRejects.Add(assetReject);
            await _context.SaveChangesAsync();
            return assetReject.Id;
        }

        public async Task<IEnumerable<AssetReject>> GetAllAssetReject()
        {
            return await _context.AssetRejects.Include(a => a.assetRegistration).OrderByDescending(a => a.Id).ToListAsync();
        }

        #endregion

        #region Asset Retirement

        public async Task<int> SaveAssetRetirement(AssetRetirement assetRetirement)
        {
            if (assetRetirement.Id != 0)
                _context.AssetRetirements.Update(assetRetirement);
            else
                _context.AssetRetirements.Add(assetRetirement);
            await _context.SaveChangesAsync();
            return assetRetirement.Id;
        }

        public async Task<IEnumerable<AssetRetirement>> GetAllAssetRetirement()
        {
            return await _context.AssetRetirements.Include(a => a.assetRegistration.itemSpecification.Item).Include(a => a.assetRetirementType).OrderByDescending(a => a.Id).ToListAsync();
        }

        public async Task<AssetRetirement> GetAssetRetirementById(int id)
        {
            return await _context.AssetRetirements.Include(a => a.assetRegistration).Where(a => a.Id == id).FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<AssetRegistration>> GetNonRetirementAssetList()
        {
            List<int?> assetIds = _context.AssetRetirements.Select(x => x.assetRegistrationId).ToList();

            return await _context.AssetRegistration.Where(x => !assetIds.Contains(x.Id)).Include(a => a.purchaseInfo).Include(a => a.purchaseInfo.supplier).OrderByDescending(a => a.Id).ToListAsync();
        }

        public async Task<decimal> GetAssetTotalDepreciationByAssetId(int assetid)
        {
            IEnumerable<AssetDepreciation> AssetDepreciation = await _context.assetDepreciations.Where(x => x.assetRegistrationId == assetid).ToListAsync();
            decimal yearAccumulatedDepreciation = AssetDepreciation.Sum(x => (decimal)x.depriciationValue);
            return yearAccumulatedDepreciation;
        }

        public async Task<bool> DeleteAssetRetirementById(int id)
        {
            _context.AssetRetirements.Remove(_context.AssetRetirements.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Asset Retirement Type

        public async Task<bool> SaveAssetRetirementType(AssetRetirementType assetRetirementType)
        {
            if (assetRetirementType.Id != 0)
                _context.AssetRetirementTypes.Update(assetRetirementType);
            else
                _context.AssetRetirementTypes.Add(assetRetirementType);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AssetRetirementType>> GetAllAssetRetirementType()
        {
            return await _context.AssetRetirementTypes.OrderByDescending(a => a.Id).ToListAsync();
        }

        public async Task<AssetRetirementType> GetAssetRetirementTypeById(int id)
        {
            return await _context.AssetRetirementTypes.FindAsync(id);
        }

        public async Task<bool> DeleteAssetRetirementTypeById(int id)
        {
            _context.AssetRetirementTypes.Remove(_context.AssetRetirementTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion
    }
}
