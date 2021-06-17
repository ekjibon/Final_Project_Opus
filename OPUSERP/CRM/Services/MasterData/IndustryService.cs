using Microsoft.EntityFrameworkCore;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData
{
    public class IndustryService : IIndustryService
    {
        private readonly ERPDbContext _context;

        public IndustryService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveIndustry(Industry agreementCategory)
        {
            if (agreementCategory.Id != 0)
                _context.Industries.Update(agreementCategory);
            else
                _context.Industries.Add(agreementCategory);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Industry>> GetAllIndustry()
        {
            return await _context.Industries.ToListAsync();
        }

        public async Task<Industry> GetIndustryById(int id)
        {
            return await _context.Industries.FindAsync(id);
        }

        public async Task<bool> DeleteIndustriesById(int id)
        {
            _context.Industries.Remove(_context.Industries.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

       
    }
}
