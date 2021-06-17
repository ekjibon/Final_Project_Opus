using OPUSERP.Recruitment.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Services.Interfaces
{
    public interface IInterviewCallingService
    {
        Task<bool> SaveInterviewCalling(InterviewCalling interviewCalling);
        Task<IEnumerable<InterviewCalling>> GetInterviewCalling();
        Task<InterviewCalling> GetInterviewCallingById(int id);
        Task<bool> DeleteInterviewCallingById(int id);
    }
}
