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
    public class AwardPublicationLanguageService : IAwardPublicationLanguageService
    {
        private readonly ERPDbContext _context;

        public AwardPublicationLanguageService(ERPDbContext context)
        {
            _context = context;
        }

        //Award;
        public async Task<bool> SaveAward(MemberAward employeeAward)
        {
            if (employeeAward.Id != 0)
                _context.memberAwards.Update(employeeAward);
            else
            _context.memberAwards.Add(employeeAward);
            
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAwardById(int id)
        {
            _context.memberAwards.Remove(await _context.memberAwards.FindAsync(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MemberAward>> GetAward()
        {
            return await _context.memberAwards.AsNoTracking().ToListAsync();
        }

        public async Task<MemberAward> GetAwardById(int id)
        {
            return await _context.memberAwards.FindAsync(id);
        }

        public async Task<IEnumerable<MemberAward>> GetAwardsByEmpId(int empId)
        {
            return await _context.memberAwards.Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }


        //Publications..
        public async Task<IEnumerable<MemberPublication>> GetPublication()
        {
            return await _context.memberPublications.AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeletePublicationById(int id)
        {
            _context.memberPublications.Remove(await _context.memberPublications.FindAsync(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<MemberPublication> GetPublicationById(int id)
        {
            return await _context.memberPublications.FindAsync(id);
        }

        public async Task<bool> SavePublication(MemberPublication publication)
        {
            if (publication.Id != 0)
                _context.memberPublications.Update(publication);
            else
                _context.memberPublications.Add(publication);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MemberPublication>> GetPublicationsByEmpId(int empId)
        {
            return await _context.memberPublications.Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }


        //Language
        public async Task<bool> SaveLanguage(MemberLanguage employeeLanguage)
        {
            if (employeeLanguage.Id != 0)
                _context.memberLanguages.Update(employeeLanguage);
            else
                _context.memberLanguages.Add(employeeLanguage);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MemberLanguage>> GetLanguage()
        {
            return await _context.memberLanguages.AsNoTracking().ToListAsync();
        }

        public async Task<MemberLanguage> GetLanguageById(int id)
        {
            return await _context.memberLanguages.FindAsync(id);
        }

        public async Task<bool> DeleteLanguageById(int id)
        {
            _context.memberLanguages.Remove(await _context.memberLanguages.FindAsync(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MemberLanguage>> GetLanguageByEmpId(int empId)
        {
            return await _context.memberLanguages.Where(x => x.employeeId == empId).Include(x=> x.language).AsNoTracking().ToListAsync();
        }
    }
}
