using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("TaxChallan", Schema = "Payroll")]
    public class TaxChallan : Base
    {       
        public int salaryPeriodId { get; set; }
        public SalaryPeriod salaryPeriod { get; set; }

        [MaxLength(50)]
        public string taxChallanNo { get; set; }
        public DateTime? challanDate { get; set; }
        
    }
}
