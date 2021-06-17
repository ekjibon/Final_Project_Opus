using OPUSERP.Areas.SCMMatrix.Models;
using OPUSERP.Data.Entity.Matrix;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Matrix.Interfaces
{
    public interface IApprovalLogService
    {
        Task<int> SaveApproverLog(ApprovalLog approvalLog);

        Task<IEnumerable<ApprovalLog>> GetApproverLogList();
        Task<IEnumerable<ApproverPanelViewModel>> GetApprovedListByApprovarbyreqId(int masterId);
        Task<ApprovalLog> GetApproverLogById(int id);

        Task<ApprovalLog> GetNextApproverLogByUserId(int userId,int masterId,int matrixTypeId);
        Task<bool> UpdateApproverReLog(int? Id, string username);
        Task<ApproverPanelViewModel> UpdateApprovalLogStatus(string userName, int masterId, int matrixTypeId,string remark);

        Task<IEnumerable<ApproverPanelViewModel>> GetApprovedPanelList(string userName, int masterId, int matrixTypeId);

        Task<IEnumerable<ApproverPanelViewModel>> GetApprovedListByApprovar(int userId, int masterId, int matrixTypeId);

        Task<ApproverPanelViewModel> GetNextApproverInfo(string userName, int masterId, int matrixTypeId);

        Task<bool> DeleteApproverLogById(int id);

        Task<bool> DeleteApproverLogByMatrixTypeId(int matrixTypeId, int masterId);

        void SaveApproverLogList(List<ApprovalLog> approvalLogs);
        Task<bool> UpdateApproverLog(int? Id,string userinfo);
        Task<bool> UpdateDOALog(int? Id, DateTime? fromDate, DateTime? toDate);
        Task<bool> UpdateReDOALog(int? Id);
        Task<IEnumerable<ChangeOfDoa>> Getchangedoas();
        Task<IEnumerable<ChangeDoaViewModel>> Getchangedoasp();
        Task<IEnumerable<MatrixChangeHistory>> GetForceShiftLogList();
       


    }
}
