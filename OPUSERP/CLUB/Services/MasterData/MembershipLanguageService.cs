using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data;
using CLUB.Data.Entity.Master;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CLUB.Services.MasterData
{
    public class MembershipLanguageService : IMembershipLanguageService
    {
        private readonly ApplicationDbContext _context;

        public MembershipLanguageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Membership>> GetMembershipInfo()
        {
            return await _context.memberships.AsNoTracking().ToListAsync();
        }

        public async Task<Membership> GetMembershipById(int id)
        {
            return await _context.memberships.FindAsync(id);
        }

        public async Task<bool> SaveMembershipInfo(Membership membership)
        {
            if(membership.Id != 0)
                _context.memberships.Update(membership);
            else
                _context.memberships.Add(membership);
            return 1 == await _context.SaveChangesAsync();
        }
        
        public async Task<bool> DeleteMembershipById(int id)
        {
            _context.memberships.Remove(_context.memberships.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        // Language Info

        public async Task<IEnumerable<Language>> GetLanguageInfo()
        {
            return await _context.languages.AsNoTracking().ToListAsync();
        }

        public async Task<Language> GetLanguageInfoById(int id)
        {
            return await _context.languages.FindAsync(id);
        }

        public async Task<int> GetLanguageCheck(int id, string name)
        {
            var Language = await _context.languages.Where(x => x.languageName == name && x.Id != id).FirstOrDefaultAsync();
            if (Language == null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<bool> SaveLanguageInfo(Language language)
        {
            if(language.Id != 0)
                _context.languages.Update(language);
            else
                _context.languages.Add(language);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteLanguageInfoById(int id)
        {
            _context.languages.Remove(_context.languages.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
