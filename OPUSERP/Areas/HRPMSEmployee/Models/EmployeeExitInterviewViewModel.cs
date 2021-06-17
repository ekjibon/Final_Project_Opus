using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Recruitment.ExitInterview;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class EmployeeExitInterviewViewModel
    {

        public int? employeeId { get; set; }


        public int? exitInterviewMasterId { get; set; }
        public ExitInterviewMaster exitInterviewMaster { get; set; }


        public EmployeeInfo employeeInfo { get; set; }
        public Supervisor supervisor { get; set; }
        public IEnumerable<EmployeeProjectActivity>  employeeProjectActivities { get; set; }


        public string unit { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? startDate { get; set; }


        public decimal? lastWithdrawBlance { get; set; }


        public int?[] reason { get; set; }

        public int?[] suggestionId { get; set; }
        public string[] suggestion { get; set; }

        public int?[] payAndBenefitsId { get; set; }
        public string[] payAndBenefitsAns { get; set; }

        public int?[] feelAboutFollowingId { get; set; }
        public string[] feelAboutFollowingAns { get; set; }

        public int?[] interviewCommentId { get; set; }
        public string[] interviewComment { get; set; }




        public IEnumerable<ExitInterviewMaster>  exitInterviewMasters { get; set; }
        public IEnumerable<ExitInterviewReasonOfLeaving> exitInterviewReasonOfLeaving { get; set; }
        public IEnumerable<ExitInterviewSuggestion> exitInterviewSuggestion { get; set; }
        public IEnumerable<ExitInterviewPayNBenefits> exitInterviewPayNBenefits { get; set; }
        public IEnumerable<ExitInterviewFeelAndFollowings> exitInterviewFeelAndFollowings { get; set; }
        public IEnumerable<ExitInterviewComment> exitInterviewComment { get; set; }


        public IEnumerable<ReasonForLeaving> reasonForLeavings { get; set; }
        public IEnumerable<CommentORSuggestion> commentORSuggestions { get; set; }
        public IEnumerable<PayAndBenefits> payAndBenefits { get; set; }
        public IEnumerable<FeelAboutTheFollowing> feelAboutTheFollowings { get; set; }
        public IEnumerable<InterviewComment> interviewComments { get; set; }
    }
}
