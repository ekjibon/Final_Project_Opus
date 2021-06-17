using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Data.Entity
{
    [Table("RCRTEducation", Schema = "RCRT")]
    public class RCRTEducation : Base
    {
        public int candidateId { get; set; }
        public CandidateInfo candidate { get; set; }

        public string institution { get; set; }

        public int? resultId { get; set; }
        public Result result { get; set; }

        public string majorGroup { get; set; }

        public string grade { get; set; }

        public int? passingYear { get; set; }

        public int? degreeId { get; set; }
        public Degree degree { get; set; }

        public int? organizationId { get; set; }
        public Organization organization { get; set; }

        public int? reldegreesubjectId { get; set; }
        public RelDegreeSubject reldegreesubject { get; set; }
    }
}
