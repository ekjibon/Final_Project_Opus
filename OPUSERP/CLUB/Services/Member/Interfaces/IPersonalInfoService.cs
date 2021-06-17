using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.Grettings.Models;
using OPUSERP.Areas.Member.Models;
//using OPUSERP.CLUB.Areas.Member.Models.NotMappedEntity;
using OPUSERP.Areas.MemberInfo.Models;
using OPUSERP.Areas.MemberInfo.Models.NotMappedEntity;
using OPUSERP.CLUB.Data.Member;

namespace OPUSERP.CLUB.Services.Member.Interfaces
{
    public interface IPersonalInfoService
    {
        Task<int> SaveEmployeeInfo(MemberInfo employeeInfo);
        Task<IEnumerable<MemberInfo>> GetEmployeeInfo();
        Task<MemberInfo> GetEmployeeInfoById(int id);
        Task<bool> DeleteEmployeeInfoById(int id);
        Task<IEnumerable<MemberWithDesignationVM>> GetEmployeeInfoDetailsList(int empId);
        Task<MemberInfo> GetEmployeeInfoByCode(string code);
        Task<MemberInfo> GetEmployeeInfoByCodeAndOrg(string code,string orgType);
        Task<MemberInfo> GetFreeEmployeeByCode(string code);
        Task<string> GetEmployeeNameCodeById(int Id);
        Task<bool> UpdateEmployee(int Id, string authId, string org);
        Task<IEnumerable<MemberInfo>> GetEmployeeInfoByOrg(string org);
        Task<string> GetAuthCodeByUserId(int empId);

        Task<string> GetEmployeeIDByAuthID(string empId);
        Task<int> IsThisEmpIDPresent(string employeeId);
        Task<IEnumerable<AllMemberJson>> GetEmployeeInfoAsQueryAble(string queryString, string org);
        Task<IEnumerable<AllMemberJson>> GetEmployeeInfoAsQueryAbleMore(string queryString, string org);

        //Grettings
        //Task<IEnumerable<Grettings>> GetEmployeeGrettings();

        //for Fees
        Task<IEnumerable<MemberInfo>> GetEmployeeInfoByType();
        Task<IEnumerable<AllMemberJson>> GetAllMemberInfoJson();
        //for EmployeeInfo with Photo
        Task<IEnumerable<MemberwithPhoto>> GetEmployeeInfoWithPhoto();

        //for validation
        Task<int> GetEmployeeInfoCheck(string empCode, int id);

        Task<IEnumerable<Grettings>> GetThisMonthGrettings();
    }
}
