using OPUSERP.Recruitment.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Services.Interfaces
{
    public interface IUpdateCandidateInfoService
    {
        Task<bool> SaveRCRTEducation(RCRTEducation rCRTEducation);
        Task<IEnumerable<RCRTEducation>> GetAllRCRTEducation();
        Task<RCRTEducation> GetRCRTEducationById(int id);
        Task<bool> DeleteEducationalQualificationById(int id);
        Task<IEnumerable<RCRTEducation>> GetRCRTEducationByCandidateId(int candidateId);
        //Wahid
        Task<IEnumerable<RCRTEducation>> GetRCRTEducationListByCandidateId(int? candidateId);
        

        Task<bool> SaveAddress(RCRTAddress address);
        Task<IEnumerable<RCRTAddress>> GetAllAddress();
        Task<RCRTAddress> GetAddressById(int id);
        Task<bool> DeleteAddressById(int id);
        Task<IEnumerable<RCRTAddress>> GetAddressByCanidateId(int candidateId);
        Task<RCRTAddress> GetAddressByTypeAndCandidateId(int candidateId, string type);

        Task<bool> SaveImage(CandidatePhoto photograph);
        Task<IEnumerable<CandidatePhoto>> GetPhotograph();
        Task<IEnumerable<CandidatePhoto>> GetPhotographByCandidateId(int candidateId);
        Task<bool> DeletePhotographById(int id);
        Task<CandidatePhoto> GetPhotographByTypeAndCandidateId(int empId, string type);
    }
}
