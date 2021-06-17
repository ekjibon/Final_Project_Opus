using OPUSERP.Areas.SCMMatrix.Models;
using OPUSERP.Data.Entity.Matrix;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Matrix.Interfaces
{
    public interface IApprovalMatrixlogService
    {
        Task<int> SaveApproverMatrixLog(ApprovalMatrixlog approvalLog);

       void SaveApproverMatrixLogList(List<ApprovalMatrixlog> approvalLogs);

        Task<IEnumerable<ApprovalMatrixlog>> GetApproverMatrixLogList();

        Task<ApprovalMatrixlog> GetApproverMatrixLogById(int id);

       
        
    }
}
