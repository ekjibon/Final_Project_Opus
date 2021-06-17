using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.ACR
{
    [Table("Position", Schema = "HR")]
    public class Position:Base
    {
        public int employeeId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }
        public int? totalEmplyee { get; set; }
        public int? noOfOfficer { get; set; }
        public int? noOfWarker { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? developmentBudget { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? infrasturcturBudget { get; set; }
    }
}
