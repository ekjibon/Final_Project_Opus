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
    public class MarksEntryService : IMarksEntryService
    {
        private readonly ERPDbContext _context;

        public MarksEntryService(ERPDbContext context)
        {
            _context = context;
        }

        // Marks Entry Master
        public async Task<int> SaveMarks(ResultMaster resultMaster)
        {
            if (resultMaster.Id != 0)
            {
                _context.ResultMasters.Update(resultMaster);
            }
            else
            {
                _context.ResultMasters.Add(resultMaster);
            }
            await _context.SaveChangesAsync();
            return resultMaster.Id;
        }

        public async Task<IEnumerable<ResultMaster>> GetMarks()
        {
            return await _context.ResultMasters.AsNoTracking().ToListAsync();
        }

        public async Task<ResultMaster> GetMarksById(int id)
        {
            return await _context.ResultMasters.FindAsync(id);
        }

        public async Task<bool> DeleteMarksById(int id)
        {
            _context.ResultMasters.Remove(_context.ResultMasters.Find(id));
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

        // Marks Details
        public async Task<int> SaveMarksDetails(ResultDetails resultDetails)
        {
            if (resultDetails.Id != 0)
            {
                _context.ResultDetails.Update(resultDetails);
            }
            else
            {
                _context.ResultDetails.Add(resultDetails);
            }
            await _context.SaveChangesAsync();
            return resultDetails.Id;
        }

        public async Task<IEnumerable<ResultDetails>> GetMarksDetails()
        {
            return await _context.ResultDetails.AsNoTracking().ToListAsync();
        }
    }
}
