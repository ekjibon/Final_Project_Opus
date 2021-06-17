using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Payroll.Data.Entity.Salary;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    /*
        status: Not used by apllication
        Verified By:  Jaggesher
        Date: 23/04/2019
    */
    [Table("Promotion", Schema = "HR")]
    public class Promotion:Base
    {
        public int employeeId { get; set; }

        public EmployeeInfo employeeInfo { get; set; }

        public string promotionType { get; set; }

        public int? designationId { get; set; }
        public Designation designation { get; set; }

        public int? designationOldId { get; set; }
        public Designation designationOld { get; set; }

        public string promotionDate { get; set; }

        public int? salaryGradeId { get; set; }
        public SalaryGrade salaryGrade { get; set; }

        public decimal? Basic { get; set; }

        public string Remarks { get; set; }
    }
}
