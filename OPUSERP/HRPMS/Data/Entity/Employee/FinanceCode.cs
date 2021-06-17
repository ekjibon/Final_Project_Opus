using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("FinanceCode", Schema = "HR")]
    public class FinanceCode:Base
    {
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public string fnCode { get; set; }
    }
}
