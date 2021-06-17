using OPUSERP.CLUB.Data.Bulk;
using OPUSERP.CLUB.Data.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Bulk.Interface
{
   public interface IRlnGroupMemberService
    {
        Task<bool> SaveRlnMemberGroup(RlnMemberGroup rlnMemberGroup);
        Task<IEnumerable<RlnMemberGroup>> GetRlnMemberGroup();
        Task<IEnumerable<MemberInfo>> GetEmployeeInfoByGroupId(int groupId);
        Task<IEnumerable<RlnMemberGroup>> GetRlnMemberGroupByGroupId(int groupId);
        Task<RlnMemberGroup> GetRlnMemberGroupById(int id);
        Task<bool> DeleteRlnMemberGroupById(int id);
    }
}
