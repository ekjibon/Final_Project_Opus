using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Requisition;

namespace OPUSERP.VMS.Services.Requisition.Interfaces
{
    public interface IVMSRequisitionService
    {
        #region Requisition master
        Task<int> SaverequisitionMaster(VMSRequisitionMaster requisitionMaster);
        Task<IEnumerable<VMSRequisitionMaster>> GetrequisitionMaster();
        
        Task<VMSRequisitionMaster> GetrequisitionMasterById(int id);
        Task<bool> DeleterequisitionMasterById(int id);
        #endregion
        #region maintenance Detail
        Task<int> SaverequisitionDetail(VMSRequisitionDetails requisitionDetails);
        Task<IEnumerable<VMSRequisitionDetails>> GetrequisitionDetails();
        Task<IEnumerable<VMSRequisitionDetails>> GetrequisitionDetailsbyMasterId(int serviceId);
        Task<VMSRequisitionDetails> GetrequisitionDetailsById(int id);
        Task<bool> DeleterequisitionDetailsById(int id);
        Task<bool> DeleterequisitionDetailsByMasterId(int id);

        #endregion

        #region Fuel Requisition Comment
        Task<int> SaveRequisitionComment(RequisitionComment requisitionComment);
        Task<IEnumerable<RequisitionComment>> GetCommentByRequisitionId(int vid);
        Task<RequisitionComment> GetRequisitionCommentById(int id);
        Task<bool> DeleteRequisitionCommentById(int id);
        #endregion
    }
}
