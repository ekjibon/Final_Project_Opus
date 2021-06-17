using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface IEmployeeExitInterviewService
    {
        Task<bool> SaveReasonForLeaving(ReasonForLeaving reasonForLeaving);
        Task<IEnumerable<ReasonForLeaving>> GetAllReasonForLeaving();
        Task<ReasonForLeaving> GetReasonForLeavingById(int id);
        Task<bool> DeleteReasonForLeavingById(int id);


        Task<bool> SaveCommentORSuggestion(CommentORSuggestion commentORSuggestion);
        Task<IEnumerable<CommentORSuggestion>> GetAllCommentORSuggestion();
        Task<CommentORSuggestion> GetCommentORSuggestionById(int id);
        Task<bool> DeleteCommentORSuggestionById(int id);


        Task<bool> SavePayAndBenefits(PayAndBenefits  payAndBenefits);
        Task<IEnumerable<PayAndBenefits>> GetAllPayAndBenefits();
        Task<PayAndBenefits> GetPayAndBenefitsById(int id);
        Task<bool> DeletePayAndBenefitsById(int id);


        Task<bool> SaveFeelAboutTheFollowing(FeelAboutTheFollowing feelAboutTheFollowing);
        Task<IEnumerable<FeelAboutTheFollowing>> GetAllFeelAboutTheFollowing();
        Task<FeelAboutTheFollowing> GetFeelAboutTheFollowingById(int id);
        Task<bool> DeleteFeelAboutTheFollowingById(int id);


        Task<bool> SaveInterviewComment(InterviewComment interviewComment);
        Task<IEnumerable<InterviewComment>> GetAllInterviewComment();
        Task<InterviewComment> GetInterviewCommentById(int id);
        Task<bool> DeleteInterviewCommentById(int id);
    }
}
