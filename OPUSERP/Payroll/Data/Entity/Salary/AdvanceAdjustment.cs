using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("AdvanceAdjustment", Schema = "Payroll")]
    public class AdvanceAdjustment : Base
    {
        public int employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }       

        public int? salaryPeriodId { get; set; }
        public SalaryPeriod salaryPeriod { get; set; }

        public int salaryHeadId { get; set; }
        public SalaryHead salaryHead { get; set; }

        public DateTime? date { get; set; }

        public decimal advanceAmount { get; set; }
        public decimal installmentAmount { get; set; }
        public int noOfInstallment { get; set; }
        [DefaultValue(0)]
        public int? isContinue { get; set; }
        [DefaultValue(0)]
        public int? isComplete { get; set; }
        [MaxLength(300)]
        public string purpose { get; set; }
    }
}
