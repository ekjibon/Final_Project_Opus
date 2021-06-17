using OPUSERP.CLUB.Data.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Member.Interfaces
{
    public interface IAwardPublicationLanguageService
    {
        //Award
        Task<bool> SaveAward(MemberAward employeeAward);
        Task<IEnumerable<MemberAward>> GetAward();
        Task<MemberAward> GetAwardById(int id);
        Task<bool> DeleteAwardById(int id);
        Task<IEnumerable<MemberAward>> GetAwardsByEmpId(int empId);

        //publications
        Task<bool> SavePublication(MemberPublication publication);
        Task<IEnumerable<MemberPublication>> GetPublication();
        Task<MemberPublication> GetPublicationById(int id);
        Task<bool> DeletePublicationById(int id);
        Task<IEnumerable<MemberPublication>> GetPublicationsByEmpId(int empId);

        //Languages
        Task<bool> SaveLanguage(MemberLanguage employeeLanguage);
        Task<IEnumerable<MemberLanguage>> GetLanguage();
        Task<MemberLanguage> GetLanguageById(int id);
        Task<bool> DeleteLanguageById(int id);
        Task<IEnumerable<MemberLanguage>> GetLanguageByEmpId(int empId);
    }
}
