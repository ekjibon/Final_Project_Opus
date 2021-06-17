using OPUSERP.Areas.MemberInfo.Models.Lang;
using OPUSERP.CLUB.Data.Member;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.MemberInfo.Models
{
    public class PassportViewModel
    {

        public string employeeID { get; set; }

        public string passportId { get; set; }

        [Required]
        [Display(Name = "Passport Number")]
        public string passPortNumber { get; set; }

        [Display(Name = "Place of Issue")]
        public string place { get; set; }

        [Display(Name = "Date Of Issue")]
        public DateTime? dateOfIssue { get; set; }

        [Display(Name = "Date Of Expair")]
        public DateTime? dateOfExpair { get; set; }

        public string action { get; set; }

        public string employeeNameCode { get; set; }

        public Passport fLang { get; set; }

        public IEnumerable<MemberPassportDetails> passportDetails { get; set; }
    }
}
