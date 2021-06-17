using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.VIMS.Data;
using OPUSERP.VIMS.Services.VIMS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VIMS.Services.VIMS
{
    public class VisitorEntryService : IVisitorEntryService
    {
        private readonly ERPDbContext _context;

        public VisitorEntryService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveVisitor(Visitor visitor)
        {
            if (visitor.Id != 0)
                _context.Visitors.Update(visitor);
            else
                _context.Visitors.Add(visitor);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Visitor>> GetAllVisitor()
        {
            return await _context.Visitors.AsNoTracking().ToListAsync();
        }

        public async Task<Visitor> GetVisitorById(int id)
        {
            return await _context.Visitors.FindAsync(id);
        }

        public async Task<bool> DeleteVisitorById(int id)
        {
            _context.Visitors.Remove(_context.Visitors.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Visitor>> GetContactphotoByContactId(int id)
        {
            return await _context.Visitors.Where(x => x.Id == id).ToListAsync();
        }
    }
}
