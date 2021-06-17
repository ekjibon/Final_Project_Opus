using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData
{
    public class EmployeeExitInterviewService : IEmployeeExitInterviewService
    {
        private readonly ERPDbContext _context;

        public EmployeeExitInterviewService(ERPDbContext context)
        {
            _context = context;
        }


        #region ReasonForLeaving

        public async Task<bool> SaveReasonForLeaving(ReasonForLeaving reasonForLeaving)
        {
            if (reasonForLeaving.Id != 0)
                _context.reasonForLeavings.Update(reasonForLeaving);
            else
                _context.reasonForLeavings.Add(reasonForLeaving);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReasonForLeaving>> GetAllReasonForLeaving()
        {
            return await _context.reasonForLeavings.AsNoTracking().ToListAsync();
        }

        public async Task<ReasonForLeaving> GetReasonForLeavingById(int id)
        {
            return await _context.reasonForLeavings.FindAsync(id);
        }

        public async Task<bool> DeleteReasonForLeavingById(int id)
        {
            _context.reasonForLeavings.Remove(_context.reasonForLeavings.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region CommentORSuggestion

        public async Task<bool> SaveCommentORSuggestion(CommentORSuggestion  commentORSuggestion)
        {
            if (commentORSuggestion.Id != 0)
                _context.commentORSuggestions.Update(commentORSuggestion);
            else
                _context.commentORSuggestions.Add(commentORSuggestion);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CommentORSuggestion>> GetAllCommentORSuggestion()
        {
            return await _context.commentORSuggestions.AsNoTracking().ToListAsync();
        }

        public async Task<CommentORSuggestion> GetCommentORSuggestionById(int id)
        {
            return await _context.commentORSuggestions.FindAsync(id);
        }

        public async Task<bool> DeleteCommentORSuggestionById(int id)
        {
            _context.commentORSuggestions.Remove(_context.commentORSuggestions.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region PayAndBenefits

        public async Task<bool> SavePayAndBenefits(PayAndBenefits  payAndBenefits)
        {
            if (payAndBenefits.Id != 0)
                _context.payAndBenefits.Update(payAndBenefits);
            else
                _context.payAndBenefits.Add(payAndBenefits);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PayAndBenefits>> GetAllPayAndBenefits()
        {
            return await _context.payAndBenefits.AsNoTracking().ToListAsync();
        }

        public async Task<PayAndBenefits> GetPayAndBenefitsById(int id)
        {
            return await _context.payAndBenefits.FindAsync(id);
        }

        public async Task<bool> DeletePayAndBenefitsById(int id)
        {
            _context.payAndBenefits.Remove(_context.payAndBenefits.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region FeelAboutTheFollowing

        public async Task<bool> SaveFeelAboutTheFollowing(FeelAboutTheFollowing  feelAboutTheFollowing)
        {
            if (feelAboutTheFollowing.Id != 0)
                _context.feelAboutTheFollowings.Update(feelAboutTheFollowing);
            else
                _context.feelAboutTheFollowings.Add(feelAboutTheFollowing);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FeelAboutTheFollowing>> GetAllFeelAboutTheFollowing()
        {
            return await _context.feelAboutTheFollowings.AsNoTracking().ToListAsync();
        }

        public async Task<FeelAboutTheFollowing> GetFeelAboutTheFollowingById(int id)
        {
            return await _context.feelAboutTheFollowings.FindAsync(id);
        }

        public async Task<bool> DeleteFeelAboutTheFollowingById(int id)
        {
            _context.feelAboutTheFollowings.Remove(_context.feelAboutTheFollowings.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region InterviewComment

        public async Task<bool> SaveInterviewComment(InterviewComment  interviewComment)
        {
            if (interviewComment.Id != 0)
                _context.interviewComments.Update(interviewComment);
            else
                _context.interviewComments.Add(interviewComment);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<InterviewComment>> GetAllInterviewComment()
        {
            return await _context.interviewComments.AsNoTracking().ToListAsync();
        }

        public async Task<InterviewComment> GetInterviewCommentById(int id)
        {
            return await _context.interviewComments.FindAsync(id);
        }

        public async Task<bool> DeleteInterviewCommentById(int id)
        {
            _context.interviewComments.Remove(_context.interviewComments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion
    }
}
