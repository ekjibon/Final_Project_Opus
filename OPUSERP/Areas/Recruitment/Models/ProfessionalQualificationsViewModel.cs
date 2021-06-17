using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Recruitment.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Recruitment.Models
{
    public class ProfessionalQualificationsViewModel
    {
        public int? candidateId { get; set; }
        public int? qualificationID { get; set; }
        public int? qualificationHeadId { get; set; }
        public string subject { get; set; }
        public int? result { get; set; }
        public string instituteName { get; set; }
        public string passingYear { get; set; }
        public string markGrade { get; set; }

        public string employeeNameCode { get; set; }

        public IEnumerable<QualificationHead> qualificationHeads { get; set; }
        public IEnumerable<RCRTProfessionalQualification> rCRTProfessionalQualifications { get; set; }
        public IEnumerable<Result> results { get; set; }
    }
}
