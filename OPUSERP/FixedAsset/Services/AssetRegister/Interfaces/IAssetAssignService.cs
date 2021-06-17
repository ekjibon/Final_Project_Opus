using OPUSERP.Areas.Auth.Models;
using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.FixedAsset.Services.AssetRegister.Interfaces
{
    public interface IAssetAssignService
    {
        #region Asset Assign
        Task<int> SaveAssetAssign(AssetAssign assetAssign);
        Task<IEnumerable<AssetAssign>> GetAllAssetAssign();
        Task<AssetAssign> GetAssetAssignById(int id);
        Task<bool> DeleteAssetAssignById(int id);
        Task<IEnumerable<AssetRegistration>> GetAllUnassignedList();
        Task<IEnumerable<EmployeeInfoViewModel>> GetAllEmployeesList();
        #endregion

        #region Asset Transfer
        Task<int> SaveAssetTransfer(AssetTransfer assetTransfer);
        Task<IEnumerable<AssetTransfer>> GetAllAssetTransfer();
        Task<AssetTransfer> GetAssetTransferById(int id);
        Task<IEnumerable<AssetRegistration>> GetNotTransferList();
        #endregion

        #region Asset Receive
        Task<int> SaveAssetReceive(AssetReceive assetReceive);
        Task<IEnumerable<AssetReceive>> GetAllAssetReceive();
        Task<AssetReceive> GetAssetReceiveById(int id);
        Task<IEnumerable<AssetRegistration>> GetNotAssetReceiveList();
        #endregion

        #region Asset Reject

        Task<int> SaveAssetReject(AssetReject assetReject);
        Task<IEnumerable<AssetReject>> GetAllAssetReject();

        #endregion

        #region Asset Retirement

        Task<int> SaveAssetRetirement(AssetRetirement assetRetirement);
        Task<IEnumerable<AssetRetirement>> GetAllAssetRetirement();
        Task<AssetRetirement> GetAssetRetirementById(int id);
        Task<IEnumerable<AssetRegistration>> GetNonRetirementAssetList();
        Task<decimal> GetAssetTotalDepreciationByAssetId(int assetid);
        Task<bool> DeleteAssetRetirementById(int id);

        #endregion

        #region Asset Retirement Type

        Task<bool> SaveAssetRetirementType(AssetRetirementType assetRetirementType);
        Task<IEnumerable<AssetRetirementType>> GetAllAssetRetirementType();
        Task<AssetRetirementType> GetAssetRetirementTypeById(int id);
        Task<bool> DeleteAssetRetirementTypeById(int id);

        #endregion

    }
}
