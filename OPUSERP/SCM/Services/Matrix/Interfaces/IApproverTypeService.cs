using OPUSERP.Data.Entity.Matrix;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Matrix.Interfaces
{
    public interface IApproverTypeService
    {
        Task<int> SaveApproverType(ApproverType approverType);
        Task<IEnumerable<ApproverType>> GetApproverTypeList();
        Task<ApproverType> GetApproverTypeById(int id);
        Task<bool> DeleteApproverTypeById(int id);
    }
}
