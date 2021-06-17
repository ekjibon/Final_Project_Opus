using OPUSERP.HRPMS.Data.Entity.Employee;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IPhotographService
    {
        Task<bool> SavePhotograph(Photograph photograph);
        Task<IEnumerable<Photograph>> GetPhotographs();
        Task<Photograph> GetPhotographById(int id);
        Task<bool> DeletePhotographById(int id);
        Task<Photograph> GetPhotographByEmpIdAndType(int empId, string type);

        Task<bool> SaveEmployeeSignature(EmployeeSignature photograph);
        Task<IEnumerable<EmployeeSignature>> GetEmployeeSignature();
        Task<EmployeeSignature> GetEmployeeSignatureById(int id);
        Task<bool> DeleteEmployeeSignatureById(int id);
        Task<EmployeeSignature> GetEmployeeSignatureByEmpId(int empId);
    }
}
