using OPUSERP.Recruitment.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Services.Interfaces
{
    public interface IJobRequisitionService
    {
        Task<bool> SaveJobRequisition(JobRequisition jobRequisition);
        Task<IEnumerable<JobRequisition>> GetAllJobRequisition();
        Task<JobRequisition> GetJobRequisitionById(int id);        
        Task<bool> DeleteJobRequisitionById(int id);

        Task<IEnumerable<JobPost>> GetAllJobPostByRequisition();
        //Task<IEnumerable<JobRequisition>> GetJobRequisitionByJobReqId(int id);
        Task<IEnumerable<JobPost>> GetJobRequisitionByJobReqId(int id);
        Task<bool> SaveJobPost(JobPost jobPost);
        Task<IEnumerable<JobPost>> GetAllJobPost();
    }
}
