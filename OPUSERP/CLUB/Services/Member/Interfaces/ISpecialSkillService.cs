using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.CLUB.Data.Member;

namespace OPUSERP.CLUB.Services.Member.Interfaces
{
    public interface ISpecialSkillService
    {
        Task<bool> SaveSpacialSkill(SpecialSkill spacialSkill);
        Task<IEnumerable<SpecialSkill>> GetSpacialSkills();
        Task<SpecialSkill> GetSpacialSkillById(int id);
        Task<bool> DeleteSpacialSkillById(int id);
        Task<IEnumerable<string>> GetSpacialSkillsByIds(int[] ids);

        //Name Validation
        Task<int> GetSpacialSkillsCheck(string name,int id);
    }
}
