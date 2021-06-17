using OPUSERP.HRPMS.Data.Entity.Recruitment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Recruitment.Interfaces
{
    public interface IApplicationFormService
    {
        // ApplicationForm
        Task<int> SaveApplicationForm(ApplicationForm applicationForm);
        Task<IEnumerable<ApplicationForm>> GetApplicationForm();
        Task<ApplicationForm> GetApplicationFormById(int id);
        Task<bool> DeleteApplicationFormById(int id);

        // JobCircular
        Task<int> SaveCreateJobCircular(JobCircular jobCircular);
        Task<IEnumerable<JobCircular>> GetCreateJobCircular(string status);
        Task<JobCircular> GetCreateJobCircularById(int id);
        Task<bool> DeleteCreateJobCircularById(int id);
        Task<bool> UpdateJobCircular(int Id, string Type);
    }
}
