using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data.Entity.Master;

namespace CLUB.Services.MasterData.Interfaces
{
    public interface ISpecialSkillService
    {
        Task<bool> SaveSpacialSkill(SpacialSkill spacialSkill);
        Task<IEnumerable<SpacialSkill>> GetSpacialSkills();
        Task<SpacialSkill> GetSpacialSkillById(int id);
        Task<bool> DeleteSpacialSkillById(int id);
        Task<IEnumerable<string>> GetSpacialSkillsByIds(int[] ids);

        //Name Validation
        Task<int> GetSpacialSkillsCheck(string name,int id);
    }
}
