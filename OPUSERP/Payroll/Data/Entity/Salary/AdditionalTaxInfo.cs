using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("AdditionalTaxInfo", Schema = "Payroll")]
    public class AdditionalTaxInfo : Base
    {
        public int employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public int taxYearId { get; set; }
        public TaxYear taxYear { get; set; }

        public int salaryHeadId { get; set; }
        public SalaryHead salaryHead { get; set; }
        [MaxLength(300)]
        public string exemptionRule { get; set; }
        public decimal exemptionAmount { get; set; }
        
    }
}
