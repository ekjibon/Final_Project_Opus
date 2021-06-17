using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSRecruitment.Models
{
    public class ValidApplicantsViewModel
    {
        [Display(Name = "Circular Reference")]
        public string circularReference { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Mobile")]
        public string mobile { get; set; }
    }
}
