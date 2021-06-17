using OPUSERP.Areas.MemberInfo.Models.Lang;
using OPUSERP.CLUB.Data.Member;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.MemberInfo.Models
{
    public class ChildrenViewModel
    {
        public string childrenID { get; set; }
        public string employeeID { get; set; }
        [Required]
        [Display(Name = "Child Name in English")]
        public string childName { get; set; }

        [Display(Name = "Child Name in Bengali")]
        public string childNameBN { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime? dateOfBirth { get; set; }

        [Display(Name = "Education")]
        public string education { get; set; }

        [Display(Name = "Gender")]
        public string gender { get; set; }

        [Display(Name = "BIN")]
        public string bin { get; set; }

        [Display(Name = "Occupation")]
        public string occupation { get; set; }

        [Display(Name = "Designation")]
        public string designation { get; set; }

        [Display(Name = "Organization")]
        public string organization { get; set; }

        [Display(Name = "NID")]
        public string nid { get; set; }

        [Display(Name = "Blood Group")]
        public string bloodGroup { get; set; }

        public int[] skills { get; set; }

        public string employeeNameCode { get; set; }

        public ChildrenLn fLang { get; set; }

        public IEnumerable<MemberChildren> children { get; set; }
        public IEnumerable<SpecialSkill> spacialSkills { get; set; }
    }
}
