using OPUSERP.Areas.FAMSAssetRegister.Models;
using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.FixedAsset.Services.AssetRegister.Interfaces
{
    public interface IAssetDepreciationService
    {
        #region Asset Depreciation

        Task<bool> SaveAssetDepreciation(AssetDepreciation assetDepreciation);
        Task<IEnumerable<AssetDepreciation>> GetAllAssetDepreciation();
        Task<AssetDepreciation> GetAssetDepreciationById(int id);
        Task<bool> DeleteAssetDepreciationById(int id);
        Task<IEnumerable<AssetDepreciation>> GetAssetDepreciationByPeriodId(int periodId, int itemCatId);
        Task<bool> DeleteSAssetDepreciationByPeriodId(int periodId, int itemCatId);
        Task<IEnumerable<AssetDepreciationReportViewModel>> GetAssetDepreciationReportByPeriodId(int periodId, int itemCatId);
        Task<IEnumerable<DepreciationProcessViewModel>> ProcessAssetDepreciation(int depriciationPeriodId, int depRateId, string userName);
        Task<IEnumerable<DepreciationProcessViewModel>> UnProcessAssetDepreciation(int depriciationPeriodId, int depRateId, string userName);

        #endregion

        #region Report
        Task<IEnumerable<AssetDepreciationReportViewModel>> AssetDepreciationReportByPeriod(int periodId, int rateId);
        Task<IEnumerable<AssetRegisterReportViewModel>> AssetRegisterReport(int itemCategoryId, string FDate, string TDate);
        Task<IEnumerable<AssetRegisterReportViewModel>> AssetRegisterAddition(int itemCategoryId, string FDate, string TDate);
        Task<IEnumerable<AssetRegisterSummaryReportViewModel>> AssetRegisterSummaryReport(string FDate, string TDate);
        Task<IEnumerable<AssetScheduleReportViewModel>> AssetScheduleReport(string FDate, string TDate);
        Task<IEnumerable<AssetTransferReportViewModel>> AssetTransferReport(int itemCategoryId, string FDate, string TDate);
        Task<IEnumerable<AssetRetirementReportViewModel>> AssetRetirementReport(int itemCategoryId, string FDate, string TDate);

        #endregion
    }
}
