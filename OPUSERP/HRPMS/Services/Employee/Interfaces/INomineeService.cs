using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
   public interface INomineeService
    {
        #region Nominee

        Task<int> SaveNominee(Nominee nominee);
        Task<IEnumerable<Nominee>> GetNominee();
        Task<Nominee> GetNomineeById(int id);
        Task<bool> DeleteNomineeById(int id);
        Task<IEnumerable<Nominee>> GetNomineeByEmpId(int empId);

        #endregion

        #region Employee Insurance

        Task<int> SaveEmployeeInsurance(EmployeeInsurance employeeInsurance);         
        Task<bool> DeleteEmployeeInsuranceById(int id);
        Task<IEnumerable<EmployeeInsurance>> GetEmployeeInsuranceByEmpId(int empId);

        #endregion
    }
}
