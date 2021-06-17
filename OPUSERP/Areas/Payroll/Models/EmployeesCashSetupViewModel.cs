using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class EmployeesCashSetupViewModel
    {
        public int cashSetupId { get; set; }       
        public int employeeInfoId { get; set; }
        public decimal? bankAmount { get; set; }
        public decimal? walletAmount { get; set; }
        public decimal? cashAmount { get; set; }

        public IEnumerable<EmployeesCashSetup> employeesCashSetups { get; set; }
    }
}
