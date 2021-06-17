using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Attendance
{
    [Table("AttendanceRoute", Schema = "HR")]
    public class AttendanceRoute : Base
    {
        public int? attendanceUpdateApplyId { get; set; }
        public AttendanceUpdateApply attendanceUpdateApply { get; set; }

        //Supervisor Id
        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? routeOrder { get; set; }
        public int? isActive { get; set; }
    }
}
