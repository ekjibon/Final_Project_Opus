using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.VEMS.Data.Entity.RegistrationForm;
using OPUSERP.VEMS.Services.Registration.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VEMS.Services.Registration
{
    public class AuthorizedPersonService: IAuthorizedPersonService
    {
        private readonly ERPDbContext _context;

        public AuthorizedPersonService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveAuthorizedPerson(AuthorizedPerson authorizedPerson)
        {
            if (authorizedPerson.Id != 0)
            {
                authorizedPerson.updatedBy = authorizedPerson.createdBy;
                authorizedPerson.updatedAt = DateTime.Now;
                _context.authorizedPerson.Update(authorizedPerson);
            }
            else
            {
                authorizedPerson.createdAt = DateTime.Now;
                _context.authorizedPerson.Add(authorizedPerson);
            }
            await _context.SaveChangesAsync();
            return authorizedPerson.Id;
        }

        public async Task<IEnumerable<AuthorizedPerson>> GetAuthorizedPerson()
        {
            return await _context.authorizedPerson.AsNoTracking().ToListAsync();
        }

        public async Task<AuthorizedPerson> GetAuthorizedPersonById(int id)
        {
            return await _context.authorizedPerson.Where(x => x.Id == id).Include(x => x.registrationForm).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AuthorizedPerson>> GetAuthorizedPersonByRegId(int regId)
        {
            return await _context.authorizedPerson.Where(x => x.registrationFormId == regId).Include(x=>x.registrationForm).ToListAsync();
        }

        public async Task<bool> DeleteAuthorizedPersonById(int id)
        {
            _context.authorizedPerson.Remove(_context.authorizedPerson.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

    }
}
