using OPUSERP.CLUB.Data.Member;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Member.Interfaces
{
    public interface IPhotographService
    {
        Task<bool> SavePhotograph(MemberPhotograph photograph);
        Task<IEnumerable<MemberPhotograph>> GetPhotographs();
        Task<MemberPhotograph> GetPhotographById(int id);
        Task<bool> DeletePhotographById(int id);
        Task<MemberPhotograph> GetPhotographByEmpIdAndType(int empId, string type);
    }
}
