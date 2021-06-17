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
    [Table("AttendanceUpdateApply", Schema = "HR")]
    public class AttendanceUpdateApply : Base
    {
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        [MaxLength(20)]
        public string applicationNo { get; set; }        
        public DateTime? applyDateTime { get; set; }
        public string notes { get; set; }
        public int? approvalStatus { get; set; } //0=apply,1=approved
    }
}
