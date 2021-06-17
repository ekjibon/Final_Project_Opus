using OPUSERP.HRPMS.Data.Entity.Employee;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IWagesPhotographService
    {
        Task<bool> SavePhotograph(WagesPhotograph photograph);
        Task<IEnumerable<WagesPhotograph>> GetPhotographs();
        Task<WagesPhotograph> GetPhotographById(int id);
        Task<bool> DeletePhotographById(int id);
        Task<WagesPhotograph> GetPhotographByEmpIdAndType(int empId, string type);
    }
}
