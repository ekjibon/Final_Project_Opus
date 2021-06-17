using Microsoft.AspNetCore.Http;
using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Leave
{
    [Table("LeaveRegister", Schema = "HR")]
    public class LeaveRegister:Base
    {
        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? substitutionUserId { get; set; }
        public EmployeeInfo substitutionUser { get; set; }

        public int? leaveTypeId { get; set; }
        public LeaveType leaveType { get; set; }

        public int? leaveDayId { get; set; }
        public LeaveDay leaveDay { get; set; }

        public string whenLeave { get; set; }              

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? leaveFrom { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? leaveTo { get; set; }
        
        public int daysLeave { get; set; }

        public string purposeOfLeave { get; set; }  

        public string fileUrl { get; set; }  
        
        public string emergencyContactNo { get; set; } 

        public int? leaveStatus { get; set; }//1=Applied,2=onApprove,3=Final Approve/Manual leave apply,4=Return,5=Reject,6=Cancel

        public string address { get; set; }
        [MaxLength(20)]
        public string paymentOption { get; set; }

    }
}
