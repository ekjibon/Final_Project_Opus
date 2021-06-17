using OPUSERP.Recruitment.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Recruitment.Models
{
    public class JobPostViewModel
    {
        public int? jobPostId { get; set; }
        public int? jobRequisitionId { get; set; }

        public int? jobReqId { get; set; }
        public JobRequisition jobReq { get; set; }
        public string jobNo { get; set; }
        public string media { get; set; }
        public DateTime? postDate { get; set; }
        public DateTime? postingDate { get; set; }        
        public DateTime? deadline { get; set; }

        public IEnumerable<JobPost> jobPosts { get; set; }
        public IEnumerable<JobRequisition> jobRequisitions { get; set; }
    }
}
