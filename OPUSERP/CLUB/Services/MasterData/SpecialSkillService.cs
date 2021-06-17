using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data.Entity.Master;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;

namespace CLUB.Services.MasterData
{
    public class SpecialSkillService : ISpecialSkillService
    {
        private readonly ERPDbContext _context;

        public SpecialSkillService(ERPDbContext context)
        {
            _context = context;
        }
        

        public async Task<bool> DeleteSpacialSkillById(int id)
        {
            _context.spacialSkills.Remove(_context.spacialSkills.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SpacialSkill>> GetSpacialSkills()
        {
            return await _context.spacialSkills.AsNoTracking().ToListAsync();
        }

        public async Task<int> GetSpacialSkillsCheck(string name,int id)
        {
            var skill = await _context.spacialSkills.Where(x=>x.skilName == name && x.Id!=id).AsNoTracking().FirstOrDefaultAsync();
            if (skill == null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<SpacialSkill> GetSpacialSkillById(int id)
        {
            return await _context.spacialSkills.FindAsync(id);
        }

        public async Task<bool> SaveSpacialSkill(SpacialSkill spacialSkill)
        {
            if (spacialSkill.Id != 0)
                _context.spacialSkills.Update(spacialSkill);
            else
                _context.spacialSkills.Add(spacialSkill);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetSpacialSkillsByIds(int[] ids)
        {
            return await _context.spacialSkills.Where(x => ids.Contains(x.Id)).Select(x => x.skilName).ToListAsync();
        }

    }
}
