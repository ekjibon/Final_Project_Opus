using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Recruitment.ExitInterview
{
    [Table("ExitInterviewSuggestion", Schema = "HR")]
    public class ExitInterviewSuggestion : Base
    {
        public int? exitInterviewMasterId { get; set; }
        public ExitInterviewMaster exitInterviewMaster { get; set; }

        public int? commentORSuggestionId { get; set; }
        public CommentORSuggestion  commentORSuggestion { get; set; }

        public string suggestionText { get; set; }
    }
}
