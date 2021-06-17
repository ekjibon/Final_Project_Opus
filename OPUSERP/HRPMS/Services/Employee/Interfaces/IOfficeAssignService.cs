using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
   public interface IOfficeAssignService
    {
        Task<int> SaveofficeAssign(OfficeAssign officeAssign);
        Task<IEnumerable<OfficeAssign>> GetOfficeAssign();
        Task<OfficeAssign> GetOfficeAssignById(int id);
        Task<bool> DeleteOfficeAssignById(int id);
        Task<IEnumerable<OfficeAssign>> GetOfficeAssignByEmpId(int empId);
    }
}
