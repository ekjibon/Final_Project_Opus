using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IMembershipService
    {
        Task<bool> SaveMembershipInfo(EmployeeMembership employeeMembership);
        Task<IEnumerable<EmployeeMembership>> GetMembershipInfo();
        Task<EmployeeMembership> GetMembershipInfoById(int id);
        Task<bool> DeleteMembershipInfoById(int id);
        Task<IEnumerable<EmployeeMembership>> GetMembershipInfoByEmpId(int empId);
    }
}
