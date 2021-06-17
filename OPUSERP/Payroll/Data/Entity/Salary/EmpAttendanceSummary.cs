using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("EmpAttendanceSummary", Schema = "Payroll")]
    public class EmpAttendanceSummary : Base
    {
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }       

        public int? salaryPeriodId { get; set; }
        public SalaryPeriod salaryPeriod { get; set; }
        
        [DefaultValue(0)]
        public int? weeklyOff { get; set; }
        [DefaultValue(0)]
        public int? holiday { get; set; }
        [DefaultValue(0)]
        public int? leave { get; set; }
        [DefaultValue(0)]
        public int? present { get; set; }
        [DefaultValue(0)]
        public int? absent { get; set; }
        [DefaultValue(0)]
        public int? late { get; set; }
        [MaxLength(400)]
        public string remarks { get; set; }
    }
}
