using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class AttendanceReportViewModel
    {
        public int presentAddressID { get; set; }

        public int permanentAddressID { get; set; }

        public int employeeID { get; set; }

        public string presentRoadNo { get; set; }

        public string permanentRoadNo { get; set; }

        public EmployeeInfo employee { get; set; }
    }
}
