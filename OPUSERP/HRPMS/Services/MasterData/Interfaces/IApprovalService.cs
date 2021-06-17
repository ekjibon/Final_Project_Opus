using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
   public interface IApprovalService
    {
        #region ApprovalType
        Task<bool> SaveApprovalType(ApprovalType approvalType);
        Task<IEnumerable<ApprovalType>> GetAllApprovalType();
        Task<ApprovalType> GetApprovalTypeById(int id);
        Task<bool> DeleteApprovalTypeById(int id);
        #endregion

        #region ApprovalMaster
        Task<int> SaveApprovalMaster(ApprovalMaster approvalMaster);
        Task<IEnumerable<ApprovalMaster>> GetAllApprovalMaster();
        Task<ApprovalMaster> GetApprovalMasterByApprovalMasterId(int ApprovalMasterId);
        Task<ApprovalMaster> GetApprovalMasterById(int id);
        Task<bool> DeleteApprovalMasterById(int id);
        #endregion

        #region Approval Detail
        Task<bool> SaveApprovalDetail(ApprovalDetail approvalDetail);
        Task<IEnumerable<ApprovalDetail>> GetAllApprovalDetail();
        Task<IEnumerable<ApprovalDetail>> GetApprovalDetailByApprovalMasterId(int ApprovalMasterId);
        Task<ApprovalDetail> GetapprovalDetailsById(int id);
        Task<bool> DeleteapprovalDetailsById(int id);
        Task<bool> DeleteapprovalDetailsByApprovalMasterId(int ApprovalMasterId);
        Task<IEnumerable<ApprovalDetail>> GetApprovalDetailByEmpAndType(int empId, string type);
        Task<ApprovalDetail> GetSingleApprovalDetailByEmpAndType(int empId, string type);
        #endregion
    }
}
