using OPUSERP.CLUB.Data.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Member.Interfaces
{
    public interface IMemberInfoService
    {
        Task<bool> SaveEducationalQualification(MemberEducationalQualification educationalQualification);
        Task<IEnumerable<MemberEducationalQualification>> GetAllEducationalQualification();
        Task<MemberEducationalQualification> GetEducationalQualificationById(int id);
        Task<bool> DeleteEducationalQualificationById(int id);
        Task<IEnumerable<MemberEducationalQualification>> GetEducationalQualificationByEmpId(int empId);
        //Wahid
        Task<IEnumerable<MemberEducationalQualification>> GetEducationalQualificationListByEmpId(int? empId);
        //Wahid


        Task<bool> SaveAddress(MemberAddress address);
        Task<IEnumerable<MemberAddress>> GetAllAddress();
        Task<MemberAddress> GetAddressById(int id);
        Task<bool> DeleteAddressById(int id);
        Task<IEnumerable<MemberAddress>> GetAddressByEmpId(int empId);
        Task<MemberAddress> GetAddressByTypeAndEmpId(int empId, string type);
        
        Task<bool> SaveImage(MemberPhotograph photograph);
        Task<IEnumerable<MemberPhotograph>> GetPhotograph();
        Task<IEnumerable<MemberPhotograph>> GetPhotographByEmpId(int empId);
        Task<bool> DeletePhotographById(int id);
        Task<MemberPhotograph> GetPhotographByTypeAndEmpId(int empId, string type);
     
    }
}
