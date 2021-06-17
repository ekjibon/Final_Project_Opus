using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class SupervisorViewModel
    {
        public int? employeeID { get; set; }
        public int? supervisorID { get; set; }
        public int? supervisorEmpID { get; set; }

        public string commandOrder { get; set; }
        public string canFinalApprover { get; set; }

        public string employeeNameCode { get; set; }
        public DateTime? supervisorDate { get; set; }

        public Photograph photograph { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public SupervisorLn fLang { get; set; }
        public IEnumerable<Supervisor> supervisors { get; set; }
        public string visualEmpCodeName { get; set; }
    }
}
