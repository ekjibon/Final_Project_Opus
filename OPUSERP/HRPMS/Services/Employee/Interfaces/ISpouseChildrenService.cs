using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.HRPMS.Data.Entity.Employee;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface ISpouseChildrenService
    {
        Task<bool> SaveSpouse(Spouse spouse);
        Task<IEnumerable<Spouse>> GetSpouse();
        Task<Spouse> GetSpouseById(int id);
        Task<bool> DeleteSpouseById(int id);
        Task<IEnumerable<Spouse>> GetSpouseByEmpId(int empId);

        Task<bool> SaveChildren(Children children);
        Task<IEnumerable<Children>> GetChildren();
        Task<Children> GetChildrenById(int id);
        Task<bool> DeleteChildrenById(int id);
        Task<IEnumerable<Children>> GetChildrenByEmpId(int empId);
        
    }
}
