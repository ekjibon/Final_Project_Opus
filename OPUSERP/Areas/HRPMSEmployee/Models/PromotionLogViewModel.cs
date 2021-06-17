using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class PromotionLogViewModel
    {
        public string employeeID { get; set; }

        public string promotionId { get; set; }

        public string designation { get; set; }

        public int? designationNewId { get; set; }

        public int? designationOldId { get; set; }

        [Display(Name = "Promotion Date")]
        public DateTime date { get; set; }

        public string payScale { get; set; }

        public string nature { get; set; }

        public string basic { get; set; }

        public string rank { get; set; }

        public string remark { get; set; }

        public string employeeNameCode { get; set; }

        public string goNumber { get; set; }

        public DateTime? goDate { get; set; }

        public Photograph photograph { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public PromotionLogLn fLang { get; set; }

        public IEnumerable<Designation> designations { get; set; }
        public IEnumerable<PromotionLog> promotionLogs { get; set; }
        public IEnumerable<SalaryGrade> salaryGrades { get; set; }
        public Designation designationName { get; set; }
    }
}
