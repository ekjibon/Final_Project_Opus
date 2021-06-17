using OPUSERP.Areas.HRPMSRecruitment.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Recruitment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSRecruitment.Models
{
    public class CreateJobCircularViewModel
    {
        [Display(Name = "Circular Refernce")]
        public string circularRefernce { get; set; }

        [Required]
        [Display(Name = "Recruitment Type")]
        public string recruitmentType { get; set; }

        [Required]
        [Display(Name = "Position Name")]
        public string positionName { get; set; }

        [Required]
        [Display(Name = "Project")]
        public string project { get; set; }

        [Required]
        [Display(Name = "Number Of Position")]
        public string numberOfPosition { get; set; }

        [Required]
        [Display(Name = "Job Type")]
        public string jobType { get; set; }

        [Required]
        [Display(Name = "Job Locationof")]
        public string locationofJob { get; set; }

        [Display(Name = "Comments")]
        public string comments { get; set; }

        [Required]
        [Display(Name = "Date For Age Calculator")]
        public string ageCalculatorDate { get; set; }

        [Required]
        [Display(Name = "Application Deadline")]
        public string applicationDeadline { get; set; }

        [Display(Name = "Last Date of Written Exam")]
        public string writtenExamLastDate { get; set; }

        [Required]
        [Display(Name = "Application Fee")]
        public string applicationFee { get; set; }

        [Required]
        [Display(Name = "Education Qualification")]
        public string educationQualification { get; set; }

        [Display(Name = "Main Subject of Education")]
        public string educationMainSubject { get; set; }

        [Display(Name = "Other Qualification")]
        public string otherQualification { get; set; }

        [Display(Name = "Least Expperiance")]
        public int leastExpperiance { get; set; }

        [Required]
        [Display(Name = "Least Age")]
        public string leastAge { get; set; }

        [Display(Name = "Maximum Age")]
        public int maximumAge { get; set; }

        [Display(Name = "Maximum Age Quota")]
        public int maximumAgeQuota { get; set; }

        [Required]
        [Display(Name = "Go No With Date")]
        public string goNoWithDate { get; set; }

        public RecruitmentLn fLang { get; set; }

        public string IsPreferableDistricttoAttendExam { get; set; }

        public IEnumerable<JobCircular> jobCirculars { get; set; }

        public JobCircular jobCircular { get; set; }
    }
}
