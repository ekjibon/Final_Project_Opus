using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Data.Entity
{
    [Table("InterviewCalling", Schema = "RCRT")]
    public class InterviewCalling : Base
    {
        public int? jobRequisitionId { get; set; }
        public JobRequisition jobRequisition { get; set; }
        public int? jobPostId { get; set; }
        public JobPost jobPost { get; set; }
        public int? candidateInfoId { get; set; }
        public CandidateInfo candidateInfo { get; set; }
        public DateTime? interviewDate { get; set; }
        public string interviewTime { get; set; }
        public string venue { get; set; }
        public string interviewType { get; set; }
        public string communicationType { get; set; }
        public string status { get; set; }
    }
}
