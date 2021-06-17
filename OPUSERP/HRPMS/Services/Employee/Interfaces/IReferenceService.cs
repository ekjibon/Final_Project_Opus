using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IReferenceService
    {
        Task<bool> DeleteReferenceById(int id);
        Task<IEnumerable<Data.Entity.Employee.Reference>> GetReference();
        Task<IEnumerable<Data.Entity.Employee.Reference>> GetReferenceByEmpId(int empId);
        Task<Data.Entity.Employee.Reference> GetReferenceById(int id);
        Task<int> SaveReference(Data.Entity.Employee.Reference reference);
    }
}
