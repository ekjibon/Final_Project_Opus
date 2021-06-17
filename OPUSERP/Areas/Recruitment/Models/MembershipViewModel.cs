using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.Recruitment.Data.Entity;

namespace OPUSERP.Areas.Recruitment.Models
{
    public class MembershipViewModel
    {
        public string candidateId { get; set; }

        public string membershipId { get; set; }

        [Required]
        [Display(Name = "Name of Organization")]
        public string nameOrganization { get; set; }

        [Required]
        [Display(Name = "Membership Type")]
        public string membershipType { get; set; }

        public string membershipNo { get; set; }

        [Display(Name = "Remarks")]
        public string remarks { get; set; }

        public IEnumerable<RcrtEmpMembership> rcrtEmpMemberships { get; set; }
        public IEnumerable<HRPMS.Data.Entity.Master.Membership> memberships { get; set; }
    }
}
