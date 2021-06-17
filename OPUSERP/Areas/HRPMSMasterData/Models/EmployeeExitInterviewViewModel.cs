using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class EmployeeExitInterviewViewModel
    {
        public int reasonId { get; set; }
        public string reasonName { get; set; }

        public int commentORSuggestionId { get; set; }
        public string commentORSuggestionName { get; set; }

        public int payAndBenefitsId { get; set; }
        public string payAndBenefitsName { get; set; }

        public int feelAboutTheFollowingId { get; set; }
        public string feelAboutTheFollowingName { get; set; }

        public int commentTextId { get; set; }
        public string commentText { get; set; }


        public IEnumerable<ReasonForLeaving> reasonForLeavings { get; set; }
        public IEnumerable<CommentORSuggestion> commentORSuggestions { get; set; }
        public IEnumerable<PayAndBenefits> payAndBenefits { get; set; }
        public IEnumerable<FeelAboutTheFollowing> feelAboutTheFollowings { get; set; }
        public IEnumerable<InterviewComment> interviewComments { get; set; }
    }
}
