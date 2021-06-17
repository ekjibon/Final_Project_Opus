using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace OPUSERP.HRPMS.Services.MasterData
{
    public class MembershipLanguageService : IMembershipLanguageService
    {
        private readonly ERPDbContext _context;

        public MembershipLanguageService(ERPDbContext context)
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
