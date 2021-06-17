using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.Areas.MemberInfo.Models.Lang;
using OPUSERP.CLUB.Data.Master;

namespace OPUSERP.Areas.MemberInfo.Models
{
    public class MembershipViewModel
    {
        public string employeeID { get; set; }

        public string membershipId { get; set; }

        [Required]
        [Display(Name = "Name of Organization")]
        public string nameOrganization { get; set; }

        [Display(Name = "Membership Type")]
        public string membershipType { get; set; }

        [Display(Name = "Remarks")]
        public string remarks { get; set; }

        public MembershipLn fLang { get; set; }

        public IEnumerable<OtherMembership> employeeMemberships { get; set; }
        public IEnumerable<MemberType> memberTypes { get; set; }
    }
}
