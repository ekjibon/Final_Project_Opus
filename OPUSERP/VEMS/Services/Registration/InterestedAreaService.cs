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
    public class InterestedAreaService: IInterestedAreaService
    {
        private readonly ERPDbContext _context;

        public InterestedAreaService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveInterestedArea(InterestedArea interestedArea)
        {
            if (interestedArea.Id != 0)
            {
                interestedArea.updatedBy = interestedArea.createdBy;
                interestedArea.updatedAt = DateTime.Now;
                _context.interestedAreas.Update(interestedArea);
            }
            else
            {
                interestedArea.createdAt = DateTime.Now;
                _context.interestedAreas.Add(interestedArea);
            }
            await _context.SaveChangesAsync();
            return interestedArea.Id;
        }

        public async Task<IEnumerable<InterestedArea>> GetInterestedArea()
        {
            return await _context.interestedAreas.AsNoTracking().ToListAsync();
        }

        public async Task<InterestedArea> GetInterestedAreaById(int id)
        {
            return await _context.interestedAreas.Where(x => x.Id == id).Include(x => x.registrationForm).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<InterestedArea>> GetInterestedAreaByRegId(int regId)
        {
            return await _context.interestedAreas.Where(x => x.registrationFormId == regId).Include(x => x.registrationForm).ToListAsync();
        }

        public async Task<bool> DeleteInterestedAreaById(int id)
        {
            _context.interestedAreas.Remove(_context.interestedAreas.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
