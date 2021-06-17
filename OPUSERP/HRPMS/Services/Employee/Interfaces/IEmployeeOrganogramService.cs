using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IEmployeeOrganogramService
    {
        Task<IEnumerable<EmployeeAvailablePosts>> employeeAvailablePosts(int orgId);
    }
}
