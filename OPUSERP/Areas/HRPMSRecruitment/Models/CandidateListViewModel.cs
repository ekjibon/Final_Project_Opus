using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSRecruitment.Models
{
    public class CandidateListViewModel
    {
        [Display(Name = "Circular Reference No")]
        public string circularReferenceNo { get; set; }

        [Display(Name = "Position")]
        public string position { get; set; }
    }
}
