using OPUSERP.Recruitment.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Recruitment.Models
{
    public class EducationInfoViewModel
    {
        public int candidateId { get; set; }

        public int educationId { get; set; }

        [Required]
        public string institution { get; set; }

        public int resultId { get; set; }

        public string majorGroup { get; set; }

        [Required]
        public string grade { get; set; }

        [Required]
        public int passingYear { get; set; }

        [Required]
        public int degreeId { get; set; }

        [Required]
        public int organizationId { get; set; }

        public int reldegreesubjectId { get; set; }

        public CandidateInfo candidateInfo { get; set; }
        public IEnumerable<RCRTEducation> rCRTEducations { get; set; }
    }
}
