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
    public class JobRequisitionService : IJobRequisitionService
    {
        private readonly ERPDbContext _context;

        public JobRequisitionService(ERPDbContext context)
        {
            _context = context;
        }
        // Job Requisition
        public async Task<bool> SaveJobRequisition(JobRequisition jobRequisition)
        {
            if (jobRequisition.Id != 0)
            {
                _context.JobRequisitions.Update(jobRequisition);
            }
            else
            {
                _context.JobRequisitions.Add(jobRequisition);
            }
            return 1 == await _context.SaveChangesAsync();
            //await _context.SaveChangesAsync();
            //return jobRequisition.Id;
        }

        public async Task<IEnumerable<JobRequisition>> GetAllJobRequisition()
        {
            return await _context.JobRequisitions.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<JobPost>> GetAllJobPostByRequisition()
        {
            return await _context.JobPosts.Include(x=>x.jobReq).AsNoTracking().ToListAsync();
        }

        public async Task<JobRequisition> GetJobRequisitionById(int id)
        {
            return await _context.JobRequisitions.FindAsync(id);
        }

        //public async Task<IEnumerable<JobRequisition>> GetJobRequisitionByJobReqId(int reqId)
        //{
        //    return await _context.JobRequisitions.Where(x => x.Id == reqId).AsNoTracking().ToListAsync();
        //}

        //for 
        public async Task<IEnumerable<JobPost>> GetJobRequisitionByJobReqId(int reqId)
        {
            return await _context.JobPosts.Where(x => x.Id == reqId).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteJobRequisitionById(int id)
        {
            _context.JobRequisitions.Remove(_context.JobRequisitions.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }


        // Job Post
        public async Task<bool> SaveJobPost(JobPost jobPost)
        {
            if (jobPost.Id != 0)
            {
                _context.JobPosts.Update(jobPost);
            }
            else
            {
                _context.JobPosts.Add(jobPost);
            }
            return 1 == await _context.SaveChangesAsync();
            //await _context.SaveChangesAsync();
            //return jobRequisition.Id;
        }

        public async Task<IEnumerable<JobPost>> GetAllJobPost()
        {
            return await _context.JobPosts.Include(x => x.jobReq).AsNoTracking().ToListAsync();
        }

    }
}

