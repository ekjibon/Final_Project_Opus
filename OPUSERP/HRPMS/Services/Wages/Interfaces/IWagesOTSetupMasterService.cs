using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IWagesEmployeeInfoService
    {
        Task<bool> SaveAddress(WagesAddress address);
        Task<IEnumerable<WagesAddress>> GetAllAddress();
        Task<WagesAddress> GetAddressById(int id);
        Task<bool> DeleteAddressById(int id);
        Task<IEnumerable<WagesAddress>> GetAddressByEmpId(int empId);
        Task<WagesAddress> GetAddressByTypeAndEmpId(int empId, string type);
        
        Task<bool> SaveImage(WagesPhotograph photograph);
        Task<IEnumerable<WagesPhotograph>> GetPhotograph();
        Task<IEnumerable<WagesPhotograph>> GetPhotographByEmpId(int empId);
        Task<bool> DeletePhotographById(int id);
        Task<WagesPhotograph> GetPhotographByTypeAndEmpId(int empId, string type);
     
    }
}
