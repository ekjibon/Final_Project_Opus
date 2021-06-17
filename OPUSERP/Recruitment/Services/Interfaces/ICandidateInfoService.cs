using OPUSERP.Areas.Recruitment.Models;
using OPUSERP.Recruitment.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Services.Interfaces
{
    public interface ICandidateInfoService
    {
        Task<int> SaveCandidateInfo(CandidateInfo candidateInfo);
        Task<IEnumerable<CandidateInfo>> GetCandidateInfo();
        Task<CandidateInfo> GetCandidateInfoById(int id);
        Task<bool> DeletCandidateInfoById(int id); 
        Task<bool> UpdateCandidateInfo(int Id, int Type, string updateBy);

        Task<IEnumerable<AllCandidateJson>> GetCandidateInfoAsQueryAble();

        Task<IEnumerable<CVFilterViewModelReport>> GetCVInfoAsQueryAble(string queryString);
    }
}
