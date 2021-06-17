using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("Supervisor", Schema = "HR")]
    public class Supervisor : Base
    {
        public int employeeID { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? supervisorId { get; set; }
        public EmployeeInfo supervisor { get; set; }
        [MaxLength(2)]
        public string commandOrder { get; set; }
        [MaxLength(3)]
        public string canFinalApprover { get; set; }
        public DateTime? supervisorDate { get; set; }
    }
}
