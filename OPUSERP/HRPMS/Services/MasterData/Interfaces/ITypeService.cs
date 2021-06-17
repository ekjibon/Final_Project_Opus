using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.HRPMS.Data.Entity.Master;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface ITypeService
    {
        Task<bool> SaveEmployeeType(EmployeeType employeeType);
        Task<IEnumerable<EmployeeType>> GetAllEmployeeType();
        Task<EmployeeType> GetEmployeeTypeById(int id);
        Task<bool> DeleteEmployeeTypeById(int id);
        Task<IEnumerable<string>> GetTypNamesByIds(int[] ids);
    }
}
