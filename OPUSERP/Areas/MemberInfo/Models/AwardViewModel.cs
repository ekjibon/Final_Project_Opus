using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OPUSERP.Areas.Member.Models.Lang;
using OPUSERP.CLUB.Data.Member;

namespace OPUSERP.Areas.MemberInfo.Models
{
    public class AwardViewModel
    {
        public string employeeID { get; set; }

        public string awardId { get; set; }

        [Required]
        [Display(Name = "Award Name")]
        public string awardName { get; set; }

        [Display(Name = "Perpose")]
        public string perpose { get; set; }

        [Display(Name = "Date")]
        public DateTime? txtAwardDate { get; set; }

        public string action { get; set; }

        public string employeeNameCode { get; set; }

        public Award fLang { get; set; }

        public IEnumerable<MemberAward> awards { get; set; }
    }
}
