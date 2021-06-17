using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Data.Entity
{
    [Table("RCRTProfessionalQualifications", Schema = "RCRT")]
    public class RCRTProfessionalQualification : Base
    {
        public int candidateId { get; set; }
        public CandidateInfo candidate { get; set; }

        public int? qualificationHeadId { get; set; }
        public QualificationHead qualificationHead { get; set; }

        public string subject { get; set; }

        public int? resultId { get; set; }
        public Result result { get; set; }

        public string instituteName { get; set; }

        public string passingYear { get; set; }

        public string markGrade { get; set; }
    }
}
