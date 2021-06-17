using OPUSERP.CLUB.Data.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Member.Interfaces
{
    public interface ISpouseChildrenService
    {
        Task<bool> SaveSpouse(MemberSpouse spouse);
        Task<IEnumerable<MemberSpouse>> GetSpouse();
        Task<MemberSpouse> GetSpouseById(int id);
        Task<bool> DeleteSpouseById(int id);
        Task<IEnumerable<MemberSpouse>> GetSpouseByEmpId(int empId);

        Task<bool> SaveChildren(MemberChildren children);
        Task<IEnumerable<MemberChildren>> GetChildren();
        Task<MemberChildren> GetChildrenById(int id);
        Task<bool> DeleteChildrenById(int id);
        Task<IEnumerable<MemberChildren>> GetChildrenByEmpId(int empId);
        
    }
}
