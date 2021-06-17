using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.Areas.MemberInfo.Models.Lang;

namespace OPUSERP.Areas.MemberInfo.Models
{
    public class EducationalQualificationViewModel
    {
        public int employeeId { get; set; }

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

        public string employeeNameCode { get; set; }

        public EducationalQualificationLn fLang { get; set; }

        public IEnumerable<MemberEducationalQualification> educationalQualifications { get; set; }
    }
}
