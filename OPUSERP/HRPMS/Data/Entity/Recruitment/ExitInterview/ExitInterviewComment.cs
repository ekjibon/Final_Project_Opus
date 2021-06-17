using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Recruitment.ExitInterview
{
    [Table("ExitInterviewComment", Schema = "HR")]
    public class ExitInterviewComment : Base
    {
        public int? exitInterviewMasterId { get; set; }
        public ExitInterviewMaster  exitInterviewMaster { get; set; }

        public int? interviewCommentId { get; set; }
        public InterviewComment  interviewComment { get; set; }

        public string commentText { get; set; }
    }
}
