using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class TransferLogViewModel
    {
        public string employeeID { get; set; }

        public string transfarID { get; set; }

        public string workStation { get; set; }

        public DateTime? fromDate { get; set; }

        public DateTime? toDate { get; set; }

        public int? grade { get; set; }

        [Required]
        public string designation { get; set; }
        public int? designationId { get; set; }
        public int? departmentId { get; set; }

        public string employeeNameCode { get; set; }

        public Photograph photograph { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public TransferLogLn fLang { get; set; }

        public IEnumerable<SalaryGrade> salaryGrade { get; set; }

        public IEnumerable<TransferLog> transferLogs { get; set; }
        public IEnumerable<Designation> designations { get; set; }
        public IEnumerable<Department> departments { get; set; }
        
    }
}
