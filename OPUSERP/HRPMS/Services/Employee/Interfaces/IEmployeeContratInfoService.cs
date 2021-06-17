using OPUSERP.HRPMS.Data.Entity.Employee;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IEmployeeContratInfoService
    {
        Task<bool> SaveContractInfo(EmployeeContractInfo contractInfo);
        Task<EmployeeContractInfo> GetContractInfoById(int id);
        Task<bool> DeletContractInfoById(int id);
        Task<IEnumerable<EmployeeContractInfo>> GetContractInfoByEmpId(int empId);
    }
}
