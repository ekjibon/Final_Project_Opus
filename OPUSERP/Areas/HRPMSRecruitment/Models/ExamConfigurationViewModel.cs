using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSRecruitment.Models
{
    public class ExamConfigurationViewModel
    {
        [Display(Name = "Circular Reference No")]
        public string circularReferenceNo { get; set; }

        [Display(Name = "Position")]
        public string position { get; set; }

        [Display(Name = "Exam Type")]
        public string examType { get; set; }

        [Display(Name = "Total Marks")]
        public string totalMarks { get; set; }

        [Display(Name = "Pass Marks")]
        public string passMarks { get; set; }
    }
}
