using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Payroll.Data.Entity.PF
{
    [Table("GratiutyProcessData", Schema = "Payroll")]
    public class GratiutyProcessData:Base
    {
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public string designation { get; set; }

        public DateTime? date { get; set; }

        public decimal? basic { get; set; }
        public decimal? year { get; set; }
        public decimal? gratuity { get; set; }
    }
}
