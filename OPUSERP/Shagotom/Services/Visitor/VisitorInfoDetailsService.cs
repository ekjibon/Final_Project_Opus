using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.Shagotom.Data.Entity.Visitor;
using OPUSERP.Shagotom.Services.Visitor.Interfaces;

namespace OPUSERP.Shagotom.Services.Visitor
{
    public class VisitorInfoDetailsService : IVisitorInfoDetailsService
    {
        private readonly ERPDbContext _context;

        public VisitorInfoDetailsService(ERPDbContext context)
        {
            _context = context;
        }


        public async Task<string> GetCardNo()
        {
            int cardNo = await _context.VisitorEntryDetails.CountAsync() + 1;

            return Convert.ToString(cardNo);
        }

        public async Task<int> SaveVisitorEntryDetails(VisitorEntryDetails visitorInfo)
        {
            if (visitorInfo.Id != 0)
            {
                _context.VisitorEntryDetails.Update(visitorInfo);

                await _context.SaveChangesAsync();

                return visitorInfo.Id;
            }

            await _context.VisitorEntryDetails.AddAsync(visitorInfo);

            await _context.SaveChangesAsync();

            return visitorInfo.Id;
        }

        public async Task<IEnumerable<VisitorEntryDetails>> GetAllVisitorEntryDetails()
        {

            return await _context.VisitorEntryDetails.ToListAsync();
        }

        public async Task<VisitorEntryDetails> GetVisitorEntryDetailsById(int id)
        {
            return await _context.VisitorEntryDetails.FindAsync(id);
        }

        public async Task<bool> DeleteVisitorEntryDetailsById(int id)
        {
            _context.VisitorEntryDetails.Remove(_context.VisitorEntryDetails.Find(id));

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
