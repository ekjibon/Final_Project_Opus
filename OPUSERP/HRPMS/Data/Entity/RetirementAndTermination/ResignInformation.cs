using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.RetirementAndTermination
{
    [Table("ResignInformation", Schema = "HR")]
    public class ResignInformation:Base
    {
        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public DateTime? resignDate { get; set; }

        public DateTime? lastWorkingDate { get; set; }
        [MaxLength(500)]
        public string resignReason { get; set; }
    }
}
