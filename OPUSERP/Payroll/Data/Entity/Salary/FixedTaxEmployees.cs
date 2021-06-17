using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("FixedTaxEmployees", Schema = "Payroll")]
    public class FixedTaxEmployees : Base
    {
        public int employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }        

        public int taxYearId { get; set; }
        public TaxYear taxYear { get; set; }

        public decimal? taxAmount { get; set; }
        public int? noofPeriod { get; set; }
       
       
    }
}
