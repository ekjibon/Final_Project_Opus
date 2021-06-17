using OPUSERP.CLUB.Data.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Member.Interfaces
{
    public interface IPassportInfoService
    {
        Task<bool> SavePassportInfo(MemberPassportDetails passportDetail);
        Task<IEnumerable<MemberPassportDetails>> GetPassportInfo();
        Task<MemberPassportDetails> GetPassportInfoById(int id);
        Task<bool> DeletePassportInfoById(int id);
        Task<IEnumerable<MemberPassportDetails>> GetPassportInfoByEmpId(int empId);
    }
}
