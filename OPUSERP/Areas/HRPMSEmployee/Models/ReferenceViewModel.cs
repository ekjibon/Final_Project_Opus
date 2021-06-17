using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class ReferenceViewModel
    {
        public int? employeeID { get; set; }
        public int? refID { get; set; }
        public string refName { get; set; }
        public string refOrganization { get; set; }
        public string refDesignation { get; set; }
        public string refEmail { get; set; }
        public string refContact { get; set; }

        public string employeeNameCode { get; set; }

        public Photograph photograph { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public IEnumerable<HRPMS.Data.Entity.Employee.Reference> references { get; set; }
        public IEnumerable<HRPMS.Data.Entity.Employee.WagesReference> wagesReferences { get; set; }
    }
}
