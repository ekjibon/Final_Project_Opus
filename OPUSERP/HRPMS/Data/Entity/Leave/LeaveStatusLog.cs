using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Leave
{
    [Table("LeaveStatusLog", Schema = "HR")]
    public class LeaveStatusLog : Base
    {
        //Foreign Relation -> Resource
        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? leaveRegisterId { get; set; }
        public LeaveRegister leaveRegister { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }

        public string remarks { get; set; }

        public int? Status { get; set; }  
    }
}
