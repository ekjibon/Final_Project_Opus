using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Payroll.Data.Entity.Salary;
using System.Collections.Generic;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class PromotionViewModel
    {
        public int? ID { get; set; }
        public int employeeId { get; set; }

        public string employeeName { get; set; }

        public string promotionType { get; set; }

        public int designationId { get; set; }

        public string designationName { get; set; }

        public string promotionDate { get; set; }

        public int salaryGradeId { get; set; }

        public string gradeName { get; set; }

        public string gradeAliasName { get; set; }

        public decimal? Basic { get; set; }

        public string Remarks { get; set; }

        public IEnumerable<Designation> designations { get; set; }
        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }
        public IEnumerable<SalaryGrade> salaryGrades { get; set; }
        public IEnumerable<HRPMS.Data.Entity.Employee.Promotion> promotions { get; set; }
        

        public Lang.Promotion fLang { get; set; }
    }
}
