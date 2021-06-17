using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Recruitment.ExitInterview;
using OPUSERP.HRPMS.Services.Recruitment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Recruitment
{
    public class ExitInterviewService : IExitInterviewService
    {
        private readonly ERPDbContext _context;

        public ExitInterviewService(ERPDbContext context)
        {
            _context = context;
        }


        #region

        //ExitInterviewComment

        public async Task<bool> SaveExitInterviewComment(ExitInterviewComment exitInterviewComment)
        {
            if (exitInterviewComment.Id != 0)
                _context.exitInterviewComments.Update(exitInterviewComment);
            else
                _context.exitInterviewComments.Add(exitInterviewComment);
            await _context.SaveChangesAsync();

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExitInterviewComment>> GetAllExitInterviewComment()
        {
            return await _context.exitInterviewComments.AsNoTracking().ToListAsync();
        }

        public async Task<ExitInterviewComment> GetExitInterviewCommentById(int id)
        {
            return await _context.exitInterviewComments.Where(x => x.Id == id).FirstAsync();
        }


        public async Task<bool> DeleteExitInterviewCommentById(int id)
        {
            _context.exitInterviewComments.Remove(_context.exitInterviewComments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion


        #region 

        //ExitInterviewFeelAndFollowings

        public async Task<bool> SaveExitInterviewFeelAndFollowings(ExitInterviewFeelAndFollowings  exitInterviewFeelAndFollowings)
        {
            if (exitInterviewFeelAndFollowings.Id != 0)
                _context.exitInterviewFeelAndFollowings.Update(exitInterviewFeelAndFollowings);
            else
                _context.exitInterviewFeelAndFollowings.Add(exitInterviewFeelAndFollowings);
            await _context.SaveChangesAsync();

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExitInterviewFeelAndFollowings>> GetAllExitInterviewFeelAndFollowings()
        {
            return await _context.exitInterviewFeelAndFollowings.AsNoTracking().ToListAsync();
        }

        public async Task<ExitInterviewFeelAndFollowings> GetExitInterviewFeelAndFollowingsById(int id)
        {
            return await _context.exitInterviewFeelAndFollowings.Where(x => x.Id == id).FirstAsync();
        }


        public async Task<bool> DeleteExitInterviewFeelAndFollowingsById(int id)
        {
            _context.exitInterviewFeelAndFollowings.Remove(_context.exitInterviewFeelAndFollowings.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion


        #region 

        //ExitInterviewMaster

        public async Task<int> SaveExitInterviewMaster(ExitInterviewMaster  exitInterviewMaster)
        {
            if (exitInterviewMaster.Id != 0)
                _context.exitInterviewMasters.Update(exitInterviewMaster);
            else
                _context.exitInterviewMasters.Add(exitInterviewMaster);
            await _context.SaveChangesAsync();

             await _context.SaveChangesAsync();

            return exitInterviewMaster.Id;
        }

        public async Task<IEnumerable<ExitInterviewMaster>> GetAllExitInterviewMaster()
        {
            return await _context.exitInterviewMasters.Include(x=>x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<ExitInterviewMaster> GetExitInterviewMasterById(int id)
        {
            return await _context.exitInterviewMasters.Where(x => x.Id == id).Include(y=>y.employee).FirstAsync();
        }


        public async Task<bool> DeleteExitInterviewMasterById(int id)
        {
            _context.exitInterviewMasters.Remove(_context.exitInterviewMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion


        #region 

        //ExitInterviewPayNBenefits

        public async Task<bool> SaveExitInterviewPayNBenefits(ExitInterviewPayNBenefits  exitInterviewPayNBenefits)
        {
            if (exitInterviewPayNBenefits.Id != 0)
                _context.exitInterviewPayNBenefits.Update(exitInterviewPayNBenefits);
            else
                _context.exitInterviewPayNBenefits.Add(exitInterviewPayNBenefits);
            await _context.SaveChangesAsync();

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExitInterviewPayNBenefits>> GetAllExitInterviewPayNBenefits()
        {
            return await _context.exitInterviewPayNBenefits.AsNoTracking().ToListAsync();
        }

        public async Task<ExitInterviewPayNBenefits> GetExitInterviewPayNBenefitsById(int id)
        {
            return await _context.exitInterviewPayNBenefits.Where(x => x.Id == id).FirstAsync();
        }


        public async Task<bool> DeleteExitInterviewPayNBenefitsById(int id)
        {
            _context.exitInterviewPayNBenefits.Remove(_context.exitInterviewPayNBenefits.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion


        #region 

        //ExitInterviewReasonOfLeaving

        public async Task<bool> SaveExitInterviewReasonOfLeaving(ExitInterviewReasonOfLeaving  exitInterviewReasonOfLeaving)
        {
            if (exitInterviewReasonOfLeaving.Id != 0)
                _context.exitInterviewReasonOfLeavings.Update(exitInterviewReasonOfLeaving);
            else
                _context.exitInterviewReasonOfLeavings.Add(exitInterviewReasonOfLeaving);
            await _context.SaveChangesAsync();

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExitInterviewReasonOfLeaving>> GetAllExitInterviewReasonOfLeaving()
        {
            return await _context.exitInterviewReasonOfLeavings.AsNoTracking().ToListAsync();
        }

        public async Task<ExitInterviewReasonOfLeaving> GetExitInterviewReasonOfLeavingById(int id)
        {
            return await _context.exitInterviewReasonOfLeavings.Where(x => x.Id == id).FirstAsync();
        }


        public async Task<bool> DeleteExitInterviewReasonOfLeavingById(int id)
        {
            _context.exitInterviewReasonOfLeavings.Remove(_context.exitInterviewReasonOfLeavings.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion


        #region 

        //ExitInterviewSuggestion

        public async Task<bool> SaveExitInterviewSuggestion(ExitInterviewSuggestion  exitInterviewSuggestion)
        {
            if (exitInterviewSuggestion.Id != 0)
                _context.exitInterviewSuggestions.Update(exitInterviewSuggestion);
            else
                _context.exitInterviewSuggestions.Add(exitInterviewSuggestion);
            await _context.SaveChangesAsync();

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExitInterviewSuggestion>> GetAllExitInterviewSuggestion()
        {
            return await _context.exitInterviewSuggestions.AsNoTracking().ToListAsync();
        }

        public async Task<ExitInterviewSuggestion> GetExitInterviewSuggestionById(int id)
        {
            return await _context.exitInterviewSuggestions.Where(x => x.Id == id).FirstAsync();
        }


        public async Task<bool> DeleteExitInterviewSuggestionById(int id)
        {
            _context.exitInterviewSuggestions.Remove(_context.exitInterviewSuggestions.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion


        #region

        //public async Task<IEnumerable<ExitInterviewReasonOfLeaving>> GetExitInterviewReasonOfLeavingByMasterId(int ma)
        //{
        //    return await _context.exitInterviewReasonOfLeavings.Where(x => x.exitInterviewMaster = id).AsNoTracking().ToListAsync();
        //}

        public async Task<IEnumerable<ExitInterviewReasonOfLeaving>> GetExitInterviewReasonOfLeavingByMasterId(int id)
        {
            return await _context.exitInterviewReasonOfLeavings.Where(x => x.exitInterviewMasterId == id).Include(x=>x.reasonForLeaving).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ExitInterviewSuggestion>> GetExitInterviewSuggestionByMasterId(int id)
        {
            return await _context.exitInterviewSuggestions.Where(x => x.exitInterviewMasterId == id).Include(x=>x.commentORSuggestion).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ExitInterviewPayNBenefits>> GetExitInterviewPayNBenefitsByMasterId(int id)
        {
            return await _context.exitInterviewPayNBenefits.Where(x => x.exitInterviewMasterId == id).Include(x=>x.payAndBenefits).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ExitInterviewFeelAndFollowings>> GetExitInterviewFeelAndFollowingsByMasterId(int id)
        {
            return await _context.exitInterviewFeelAndFollowings.Where(x => x.exitInterviewMasterId == id).Include(x=>x.feelAboutTheFollowing).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ExitInterviewComment>> GetExitInterviewCommentByMasterId(int id)
        {
            return await _context.exitInterviewComments.Where(x => x.exitInterviewMasterId == id).Include(x=>x.interviewComment).AsNoTracking().ToListAsync();
        }


        #endregion
    }
}
