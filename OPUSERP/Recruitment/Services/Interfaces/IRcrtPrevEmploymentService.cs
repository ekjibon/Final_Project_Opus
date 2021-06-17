using OPUSERP.Recruitment.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Services.Interfaces
{
    public interface IRcrtPrevEmploymentService
    {
        Task<bool> SaveRcrtPreviousEmployment(RcrtPreviousEmployment rcrtPreviousEmployment);
        Task<IEnumerable<RcrtPreviousEmployment>> GetPreviousEmploymentByCandidateId(int candidateId);
    }
}
