using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IPriviousEmploymentService
    {
        Task<bool> DeletePriviousEmploymentById(int id);
        Task<IEnumerable<PriviousEmployment>> GetPriviousEmployment();
        Task<IEnumerable<PriviousEmployment>> GetPriviousEmploymentByEmpId(int empId);
        Task<PriviousEmployment> GetPriviousEmploymentById(int id);
        Task<bool> SavePriviousEmployment(PriviousEmployment priviousEmployment);
    }
}
