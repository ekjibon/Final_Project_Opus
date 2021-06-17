using OPUSERP.HRPMS.Data.Entity.Recruitment.ExitInterview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Recruitment.Interfaces
{
    public interface IExitInterviewService
    {

        // ExitInterviewComment

        Task<bool> SaveExitInterviewComment(ExitInterviewComment exitInterviewComment);
        Task<IEnumerable<ExitInterviewComment>> GetAllExitInterviewComment();
        Task<ExitInterviewComment> GetExitInterviewCommentById(int id);
        Task<bool> DeleteExitInterviewCommentById(int id);


        // ExitInterviewFeelAndFollowings

        Task<bool> SaveExitInterviewFeelAndFollowings(ExitInterviewFeelAndFollowings  exitInterviewFeelAndFollowings);
        Task<IEnumerable<ExitInterviewFeelAndFollowings>> GetAllExitInterviewFeelAndFollowings();
        Task<ExitInterviewFeelAndFollowings> GetExitInterviewFeelAndFollowingsById(int id);
        Task<bool> DeleteExitInterviewFeelAndFollowingsById(int id);



        // ExitInterviewMaster

        Task<int> SaveExitInterviewMaster(ExitInterviewMaster  exitInterviewMaster);
        Task<IEnumerable<ExitInterviewMaster>> GetAllExitInterviewMaster();
        Task<ExitInterviewMaster> GetExitInterviewMasterById(int id);
        Task<bool> DeleteExitInterviewMasterById(int id);



        // ExitInterviewPayNBenefits

        Task<bool> SaveExitInterviewPayNBenefits(ExitInterviewPayNBenefits exitInterviewPayNBenefits);
        Task<IEnumerable<ExitInterviewPayNBenefits>> GetAllExitInterviewPayNBenefits();
        Task<ExitInterviewPayNBenefits> GetExitInterviewPayNBenefitsById(int id);
        Task<bool> DeleteExitInterviewPayNBenefitsById(int id);



        // ExitInterviewReasonOfLeaving

        Task<bool> SaveExitInterviewReasonOfLeaving(ExitInterviewReasonOfLeaving  exitInterviewReasonOfLeaving);
        Task<IEnumerable<ExitInterviewReasonOfLeaving>> GetAllExitInterviewReasonOfLeaving();
        Task<ExitInterviewReasonOfLeaving> GetExitInterviewReasonOfLeavingById(int id);
        Task<bool> DeleteExitInterviewReasonOfLeavingById(int id);


        // ExitInterviewSuggestion

        Task<bool> SaveExitInterviewSuggestion(ExitInterviewSuggestion  exitInterviewSuggestion);
        Task<IEnumerable<ExitInterviewSuggestion>> GetAllExitInterviewSuggestion();
        Task<ExitInterviewSuggestion> GetExitInterviewSuggestionById(int id);
        Task<bool> DeleteExitInterviewSuggestionById(int id);


        // Report

        Task<IEnumerable<ExitInterviewReasonOfLeaving>> GetExitInterviewReasonOfLeavingByMasterId(int id);
        Task<IEnumerable<ExitInterviewSuggestion>> GetExitInterviewSuggestionByMasterId(int id);
        Task<IEnumerable<ExitInterviewPayNBenefits>> GetExitInterviewPayNBenefitsByMasterId(int id);
        Task<IEnumerable<ExitInterviewFeelAndFollowings>> GetExitInterviewFeelAndFollowingsByMasterId(int id);
        Task<IEnumerable<ExitInterviewComment>> GetExitInterviewCommentByMasterId(int id);

    }
}
