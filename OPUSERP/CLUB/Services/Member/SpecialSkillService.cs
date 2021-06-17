using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.CLUB.Services.Member.Interfaces;
using OPUSERP.Data;

namespace OPUSERP.CLUB.Services.Member
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
            _context.specialSkills.Remove(_context.specialSkills.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SpecialSkill>> GetSpacialSkills()
        {
            return await _context.specialSkills.AsNoTracking().ToListAsync();
        }

        public async Task<int> GetSpacialSkillsCheck(string name,int id)
        {
            var skill = await _context.specialSkills.Where(x=>x.skilName == name && x.Id!=id).AsNoTracking().FirstOrDefaultAsync();
            if (skill == null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<SpecialSkill> GetSpacialSkillById(int id)
        {
            return await _context.specialSkills.FindAsync(id);
        }

        public async Task<bool> SaveSpacialSkill(SpecialSkill spacialSkill)
        {
            if (spacialSkill.Id != 0)
                _context.specialSkills.Update(spacialSkill);
            else
                _context.specialSkills.Add(spacialSkill);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetSpacialSkillsByIds(int[] ids)
        {
            return await _context.specialSkills.Where(x => ids.Contains(x.Id)).Select(x => x.skilName).ToListAsync();
        }

    }
}
