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
    [Table("EmployeePunchCardInfo", Schema = "HR")]
    public class EmployeePunchCardInfo : Base
    {
        public string employeeCode { get; set; }
        //public EmployeeInfo employee { get; set; }
        [Required]
        public string punchCardNo { get; set; }        

        public int shiftGroupMasterId { get; set; }

        public ShiftGroupMaster shiftGroupMaster { get; set; }


    }
}
