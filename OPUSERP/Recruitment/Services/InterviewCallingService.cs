using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.Recruitment.Data.Entity;
using OPUSERP.Recruitment.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Services
{
    public class InterviewCallingService : IInterviewCallingService
    {
        private readonly ERPDbContext _context;

        public InterviewCallingService(ERPDbContext context)
        {
            _context = context;
        }

        // InterviewCalling
        public async Task<bool> SaveInterviewCalling(InterviewCalling interviewCalling)
        {
            if (interviewCalling.Id != 0)
            {
                _context.InterviewCallings.Update(interviewCalling);
            }
            else
            {
                _context.InterviewCallings.Add(interviewCalling);
            }
            return 1 == await _context.SaveChangesAsync();
            //await _context.SaveChangesAsync();
            //return jobRequisition.Id;
        }

        public async Task<IEnumerable<InterviewCalling>> GetInterviewCalling()
        {
            return await _context.InterviewCallings.AsNoTracking().ToListAsync();
        }

        public async Task<InterviewCalling> GetInterviewCallingById(int id)
        {
            return await _context.InterviewCallings.FindAsync(id);
        }

        public async Task<bool> DeleteInterviewCallingById(int id)
        {
            _context.InterviewCallings.Remove(_context.InterviewCallings.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        //public async Task<IEnumerable<InterviewCalling>> GetAllJobPostByRequisition()
        //{
        //    return await _context.InterviewCallings.Include(x => x.jobReq).AsNoTracking().ToListAsync();
        //}

        //public async Task<IEnumerable<JobPost>> GetJobRequisitionByJobReqId(int reqId)
        //{
        //    return await _context.JobPosts.Where(x => x.Id == reqId).AsNoTracking().ToListAsync();
        //}
    }
}
