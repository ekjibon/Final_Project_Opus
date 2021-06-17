using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IWagesReferenceService
    {
        Task<bool> DeleteReferenceById(int id);
        Task<IEnumerable<WagesReference>> GetReference();
        Task<IEnumerable<WagesReference>> GetReferenceByEmpId(int empId);
        Task<WagesReference> GetReferenceById(int id);
        Task<int> SaveReference(WagesReference reference);
    }
}
