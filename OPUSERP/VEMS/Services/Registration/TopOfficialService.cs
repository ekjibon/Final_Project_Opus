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
    public class TopOfficialService: ITopOfficialService
    {
        private readonly ERPDbContext _context;

        public TopOfficialService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveTopOfficial(TopOfficial topOfficial)
        {
            if (topOfficial.Id != 0)
            {
                topOfficial.updatedBy = topOfficial.createdBy;
                topOfficial.updatedAt = DateTime.Now;
                _context.topOfficials.Update(topOfficial);
            }
            else
            {
                topOfficial.createdAt = DateTime.Now;
                _context.topOfficials.Add(topOfficial);
            }
            await _context.SaveChangesAsync();
            return topOfficial.Id;
        }

        public async Task<IEnumerable<TopOfficial>> GetTopOfficial()
        {
            return await _context.topOfficials.AsNoTracking().ToListAsync();
        }

        public async Task<TopOfficial> GetTopOfficialById(int id)
        {
            return await _context.topOfficials.Where(x => x.Id == id).Include(x => x.registrationForm).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TopOfficial>> GetTopOfficialByRegId(int regId)
        {
            return await _context.topOfficials.Where(x => x.registrationFormId == regId).Include(x => x.registrationForm).ToListAsync();
        }

        public async Task<bool> DeleteTopOfficialById(int id)
        {
            _context.topOfficials.Remove(_context.topOfficials.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
