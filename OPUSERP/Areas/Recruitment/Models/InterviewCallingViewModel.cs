using OPUSERP.Recruitment.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Recruitment.Models
{
    public class InterviewCallingViewModel
    {
        public int? jobId { get; set; }
        public int? reqId { get; set; }
        public int? candidateId { get; set; }
        public DateTime? interviewDate { get; set; }
        public string interviewTime { get; set; }
        public string venue { get; set; }
        public string interviewType { get; set; }
        public string communicationType { get; set; }
        public string status { get; set; }

        public InterviewCalling interviewCalling { get; set; }
        public IEnumerable<InterviewCalling> interviewCallings { get; set; }
        public IEnumerable<CandidateInfo> candidateInfos { get; set; }
    }
}
