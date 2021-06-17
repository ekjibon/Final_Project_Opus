using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("WagesProcessEmpSalaryMaster", Schema = "Payroll")]
    public class WagesProcessEmpSalaryMaster : Base
    {
        public int? employeeInfoId { get; set; }
        public WagesEmployeeInfo employeeInfo { get; set; }

        public int? salaryPeriodId { get; set; }
        public SalaryPeriod salaryPeriod { get; set; }

        public decimal? netPayable { get; set; }
        public decimal? walletPayable { get; set; }
        public decimal? bankPayable { get; set; }

        public string bankName { get; set; }
        public string bankAccountNo { get; set; }
        public string walletNo { get; set; }
        public string division { get; set; }
        public string department { get; set; }
        public string designation { get; set; }
        
    }
}
