using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data.Entity.Master;

namespace CLUB.Services.MasterData.Interfaces
{
    public interface ITypeService
    {
        Task<bool> SaveEmployeeType(EmployeeType employeeType);
        Task<IEnumerable<EmployeeType>> GetAllEmployeeType();
        Task<EmployeeType> GetEmployeeTypeById(int id);
        Task<bool> DeleteEmployeeTypeById(int id);

        //for validation
        Task<int> GetEmployeeTypeCheck(int id, string name);
    }
}
