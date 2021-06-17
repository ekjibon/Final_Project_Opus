using OPUSERP.CLUB.Data.Bulk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Bulk.Interface
{
   public interface IGroupService
    {
        Task<bool> SaveMemberGroup(MemberGroup memberGroup);
        Task<IEnumerable<MemberGroup>> GetMemberGroup();
        Task<MemberGroup> GetMemberGroupById(int id);
        Task<bool> DeleteMemberGroupById(int id);
    }
}
