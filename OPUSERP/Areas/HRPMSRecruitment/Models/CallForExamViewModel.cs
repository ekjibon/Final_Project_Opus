using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSRecruitment.Models
{
    public class CallForExamViewModel
    {
        [Display(Name = "Circular Refernce No")]
        public string circularRefernceNo { get; set; }

        [Required]
        [Display(Name = "Position")]
        public string position { get; set; }

        [Required]
        [Display(Name = "Exam Type")]
        public string examType { get; set; }

        [Required]
        [Display(Name = "Exam Date")]
        public string examDate { get; set; }

        [Required]
        [Display(Name = "Exam Venue")]
        public string examVenue { get; set; }

        [Required]
        [Display(Name = "Exam Start End Time")]
        public string examStartEndTime { get; set; }
    }
}
