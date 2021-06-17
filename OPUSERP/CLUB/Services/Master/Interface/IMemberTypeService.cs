using OPUSERP.CLUB.Data.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Master.Interface
{
    public interface IMemberTypeService
    {
        Task<bool> SaveMemberType(MemberType memberType);
        Task<IEnumerable<MemberType>> GetAllMemberType();
        Task<MemberType> GetMemberTypeById(int id);
        Task<bool> DeleteMemberTypeById(int id);
        //Task<IEnumerable<string>> GetTypNamesByIds(int[] ids);
    }
}
