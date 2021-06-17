using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSRecruitment.Models
{
    public class SelectCandidateForRecruitmentViewModel
    {
        [Display(Name = "Job Circular Reference")]
        public string jobCircularReference { get; set; }

        [Display(Name = "Quota")]
        public string quota { get; set; }
    }
}
