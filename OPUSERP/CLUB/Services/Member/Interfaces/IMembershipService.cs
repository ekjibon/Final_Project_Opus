using OPUSERP.CLUB.Data.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Member.Interfaces
{
    public interface IMembershipService
    {
        Task<bool> SaveMembershipInfo(OtherMembership employeeMembership);
        Task<IEnumerable<OtherMembership>> GetMembershipInfo();
        Task<OtherMembership> GetMembershipInfoById(int id);
        Task<bool> DeleteMembershipInfoById(int id);
        Task<IEnumerable<OtherMembership>> GetMembershipInfoByEmpId(int empId);
    }
}
