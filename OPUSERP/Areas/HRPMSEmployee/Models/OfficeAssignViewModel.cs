using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class OfficeAssignViewModel
    {
        public int? employeeID { get; set; }
        public int? officeAssignID { get; set; }
        public string roomNo { get; set; }
        public string floorNo { get; set; }
        public string deskNo { get; set; }

        public string employeeNameCode { get; set; }

        public Photograph photograph { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public IEnumerable<OfficeAssign> officeAssigns { get; set; }
        
    }
}
