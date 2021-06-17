using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IWagesPriviousEmploymentService
    {
        Task<bool> DeletePriviousEmploymentById(int id);
        Task<IEnumerable<WagesPriviousEmployment>> GetPriviousEmployment();
        Task<IEnumerable<WagesPriviousEmployment>> GetPriviousEmploymentByEmpId(int empId);
        Task<WagesPriviousEmployment> GetPriviousEmploymentById(int id);
        Task<bool> SavePriviousEmployment(WagesPriviousEmployment priviousEmployment);
    }
}
