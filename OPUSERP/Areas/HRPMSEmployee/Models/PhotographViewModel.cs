using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Employee;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class PhotographViewModel
    {
        public int employeeID { get; set; }

        public int photographID { get; set; }

        [Display(Name = "Employee Photo")]
        public IFormFile empPhoto { get; set; }

        public Photograph photograph { get;set; }

        public EmployeeSignature employeeSignature { get;set; }

        public WagesPhotograph wagesPhotograph { get;set; }

        public string employeeNameCode { get; set; }

        public EmployeeInfoLn fLang { get; set; }

        public EmployeeInfo employeeInfo { get; set; }

        public WagesEmployeeInfo wagesEmployeeInfo { get; set; }

        public string visualEmpCodeName { get; set; }
    }
}
