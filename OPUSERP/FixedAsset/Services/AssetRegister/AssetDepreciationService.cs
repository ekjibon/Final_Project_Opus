using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.FAMSAssetRegister.Models;
using OPUSERP.Data;
using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using OPUSERP.FixedAsset.Data.Entity.MasterData;
using OPUSERP.FixedAsset.Services.AssetRegister.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.FixedAsset.Services.AssetRegister
{
    public class AssetDepreciationService : IAssetDepreciationService
    {
        private readonly ERPDbContext _context;

        public AssetDepreciationService(ERPDbContext context)
        {
            _context = context;
        }

        #region Asset Depreciation

        public async Task<bool> SaveAssetDepreciation(AssetDepreciation assetDepreciation)
        {
            if (assetDepreciation.Id != 0)
                _context.assetDepreciations.Update(assetDepreciation);
            else
                _context.assetDepreciations.Add(assetDepreciation);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AssetDepreciation>> GetAllAssetDepreciation()
        {
            return await _context.assetDepreciations.ToListAsync();
        }

        public async Task<IEnumerable<AssetDepreciation>> GetAssetDepreciationByPeriodId(int periodId, int itemCatId)
        {
            return await _context.assetDepreciations.Include(x => x.assetRegistration).Include(x => x.depriciationPeriod).Where(x => x.depriciationPeriodId == periodId && x.assetRegistration.depriciationRateId == itemCatId).AsNoTracking().ToListAsync();
        }

        public async Task<AssetDepreciation> GetAssetDepreciationById(int id)
        {
            return await _context.assetDepreciations.FindAsync(id);
        }

        public async Task<bool> DeleteSAssetDepreciationByPeriodId(int periodId, int itemCatId)
        {
            _context.assetDepreciations.RemoveRange(_context.assetDepreciations.Where(x => x.depriciationPeriodId == periodId && x.assetRegistration.depriciationRateId == itemCatId));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAssetDepreciationById(int id)
        {
            _context.assetDepreciations.Remove(_context.assetDepreciations.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AssetDepreciationReportViewModel>> GetAssetDepreciationReportByPeriodId(int periodId, int itemCatId)
        {
            //IEnumerable<DepriciationPeriod> depPeriod;
            var depPeriod = _context.DepriciationPeriods.Where(a => a.Id == periodId).Select(a=>a.SDate).FirstOrDefault();
            var periodStartDate = Convert.ToDateTime(depPeriod).ToString("yyyyMM");

            IEnumerable<AssetDepreciation> depreciation;
            if (itemCatId != 0)
            {
                depreciation = await _context.assetDepreciations.Include(x => x.assetRegistration.itemSpecification.Item.parent).Include(x => x.depriciationPeriod).Where(x => x.depriciationPeriodId == periodId && x.assetRegistration.depriciationRateId == itemCatId).AsNoTracking().ToListAsync();
            }
            else
            {
                depreciation = await _context.assetDepreciations.Include(x => x.assetRegistration.itemSpecification.Item.parent).Include(x => x.depriciationPeriod).Where(x => x.depriciationPeriodId == periodId).AsNoTracking().ToListAsync();
            }
            List<AssetDepreciationReportViewModel> lstdata = new List<AssetDepreciationReportViewModel>();
            foreach (AssetDepreciation data in depreciation)
            {
                var totaldepreciation = _context.assetDepreciations.Where(x => x.assetRegistrationId == data.assetRegistrationId).Sum(x => x.depriciationValue);

                var overhaulingCost = _context.AssetOverhaulings.Where(a => a.assetRegistrationId == data.assetRegistrationId && Convert.ToDateTime(a.overhaulingDate).ToString("yyyyMM") == periodStartDate).Sum(a => a.overhaulingCost);

                lstdata.Add(new AssetDepreciationReportViewModel
                {
                    Id = data.Id,
                    assetNo = data.assetRegistration.assetNo,
                    itemName = data.assetRegistration.itemSpecification.Item.itemName,
                    category = data.assetRegistration.itemSpecification.Item.parent.categoryName,
                    periodName = data.depriciationPeriod.PeriodName,
                    depriciationRate = data.depriciationRate,
                    depriciationValue = data.depriciationValue,
                    assetValue = data.assetRegistration.assetValue,
                    accmulatedDepriciation = totaldepreciation - data.depriciationValue,
                    totalDepriciation = totaldepreciation,
                    writtenDownValue = data.assetRegistration.assetValue - totaldepreciation,
                    overhaulingCost = overhaulingCost
                });
            }
            return lstdata;
        }

        public async Task<IEnumerable<DepreciationProcessViewModel>> ProcessAssetDepreciation(int depriciationPeriodId, int depRateId, string userName)
        {
            return await _context.DepreciationProcessViewModels.FromSql($"SP_Process_AssetDepreciation {depriciationPeriodId},{depRateId},{userName}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<DepreciationProcessViewModel>> UnProcessAssetDepreciation(int depriciationPeriodId, int depRateId, string userName)
        {
            return await _context.DepreciationProcessViewModels.FromSql($"SP_UnProcess_AssetDepreciation {depriciationPeriodId},{depRateId},{userName}").AsNoTracking().ToListAsync();
        }

        #endregion

        #region Report

        public async Task<IEnumerable<AssetDepreciationReportViewModel>> AssetDepreciationReportByPeriod(int periodId, int rateId)
        {
            return await _context.AssetDepreciationReportViewModels.FromSql($"RPTAssetDepreciation {periodId},{rateId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AssetRegisterReportViewModel>> AssetRegisterReport(int itemCategoryId, string FDate, string TDate)
        {
            return await _context.AssetRegisterReportViewModels.FromSql($"RPTAssetRegister {itemCategoryId},{Convert.ToDateTime(FDate).ToString("yyyyMMdd")},{Convert.ToDateTime(TDate).ToString("yyyyMMdd")}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AssetRegisterReportViewModel>> AssetRegisterAddition(int itemCategoryId, string FDate, string TDate)
        {
            return await _context.AssetRegisterReportViewModels.FromSql($"RPTAssetAddition {itemCategoryId},{Convert.ToDateTime(FDate).ToString("yyyyMMdd")},{Convert.ToDateTime(TDate).ToString("yyyyMMdd")}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AssetTransferReportViewModel>> AssetTransferReport(int itemCategoryId, string FDate, string TDate)
        {
            return await _context.AssetTransferReportViewModels.FromSql($"RPTAssetTransfer {itemCategoryId},{Convert.ToDateTime(FDate).ToString("yyyyMMdd")},{Convert.ToDateTime(TDate).ToString("yyyyMMdd")}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AssetRetirementReportViewModel>> AssetRetirementReport(int itemCategoryId, string FDate, string TDate)
        {
            return await _context.AssetRetirementReportViewModels.FromSql($"RPTAssetRetirement {itemCategoryId},{Convert.ToDateTime(FDate).ToString("yyyyMMdd")},{Convert.ToDateTime(TDate).ToString("yyyyMMdd")}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AssetRegisterSummaryReportViewModel>> AssetRegisterSummaryReport(string FDate, string TDate)
        {
            return await _context.AssetRegisterSummaryReportViewModels.FromSql($"RPTAssetRegisterSummary {Convert.ToDateTime(FDate).ToString("yyyyMMdd")},{Convert.ToDateTime(TDate).ToString("yyyyMMdd")}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AssetScheduleReportViewModel>> AssetScheduleReport(string FDate, string TDate)
        {
            return await _context.AssetScheduleReportViewModels.FromSql($"RPTAssetScheduleSummary {Convert.ToDateTime(FDate).ToString("yyyyMMdd")},{Convert.ToDateTime(TDate).ToString("yyyyMMdd")}").AsNoTracking().ToListAsync();
        }

        #endregion
    }
}
