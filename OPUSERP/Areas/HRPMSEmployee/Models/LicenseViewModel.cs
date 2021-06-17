using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class LicenseViewModel
    {
        public string employeeID { get; set; }

        public string licenseId { get; set; }

        [Required]
        [Display(Name = "License Number")]
        public string licenseNumber { get; set; }

        [Required]
        [Display(Name = "Place of Issue")]
        public string place { get; set; }

        [Display(Name = "Date Of Issue")]
        public DateTime? dateOfIssue { get; set; }

        [Required]
        [Display(Name = "Date Of Expiry")]
        public DateTime? dateOfExpair { get; set; }

        public string licenseCategory { get; set; }



        public string employeeNameCode { get; set; }

        public License fLang { get; set; }

        public Photograph photograph { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public IEnumerable<DrivingLicense> licenses { get; set; }

    }
}
