using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models.Dashboard
{
    public class ActiveEmployeeDetailView
    {
        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }
       // public IEnumerable<Department> departments { get; set; }
    }
}
