using OPUSERP.Recruitment.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Services.Interfaces
{
    public interface IRcrtQualificationsService
    {
        Task<bool> SaveRcrtOtherQualification(RCRTOtherQualification rCRTOtherQualification);
        Task<IEnumerable<RCRTOtherQualification>> GetOtherQualificationByCandidateId(int candidateId);

        Task<bool> SaveRcrtProfessionalQualification(RCRTProfessionalQualification rCRTProfessionalQualification);
        Task<IEnumerable<RCRTProfessionalQualification>> GetProfessionalQualificationByCandidateId(int candidateId);
    }
}
