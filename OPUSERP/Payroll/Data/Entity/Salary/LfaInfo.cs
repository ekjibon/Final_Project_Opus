using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("LfaInfo", Schema = "Payroll")]
    public class LfaInfo : Base
    {
        public int employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }       

        public int salaryPeriodId { get; set; }
        public SalaryPeriod salaryPeriod { get; set; }        

        public decimal lfaAmount { get; set; }
        public DateTime? lfaStartDate { get; set; }
        public DateTime? lfaEndDate { get; set; }
        public int currentLfaYearNo { get; set; }
        [MaxLength(3)]
        public string lfaStatus { get; set; }
    }
}
