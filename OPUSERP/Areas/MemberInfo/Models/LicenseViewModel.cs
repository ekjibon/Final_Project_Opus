using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using OPUSERP.Areas.MemberInfo.Models.Lang;
using OPUSERP.CLUB.Data.Member;

namespace OPUSERP.Areas.MemberInfo.Models
{
    public class LicenseViewModel
    {
        public string employeeID { get; set; }

        public string licenseId { get; set; }

        [Required]
        [Display(Name = "License Number")]
        public string licenseNumber { get; set; }

        [Display(Name = "Place of Issue")]
        public string place { get; set; }

        [Display(Name = "Date Of Issue")]
        public DateTime? dateOfIssue { get; set; }

        [Display(Name = "Date Of Expiry")]
        public DateTime? dateOfExpair { get; set; }

        public string licenseCategory { get; set; }



        public string employeeNameCode { get; set; }

        public License fLang { get; set; }

        public IEnumerable<MemberDrivingLicense> licenses { get; set; }

    }
}
