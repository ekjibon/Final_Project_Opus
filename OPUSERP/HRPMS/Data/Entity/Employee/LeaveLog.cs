using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Leave;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("LeaveLog", Schema = "HR")]
    public class LeaveLog : Base
    {
        //Foreign Relation -> Resource
        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? leaveTypeId { get; set; }
        public LeaveType leaveType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? leaveFrom { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? leaveTo { get; set; }

        public string purposeOfLeave { get; set; }
        public string Status { get; set; }
    }
}
