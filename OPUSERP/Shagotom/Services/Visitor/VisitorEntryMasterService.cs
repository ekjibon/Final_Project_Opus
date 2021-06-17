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
    public class VisitorEntryMasterService : IVisitorEntryMasterService
    {
        private readonly ERPDbContext _context;

        public VisitorEntryMasterService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveVisitorEntryMaster(VisitorEntryMaster visitorEntry)
        {
            if (visitorEntry.Id != 0)
            {
                _context.VisitorEntryMasters.Update(visitorEntry);

                await _context.SaveChangesAsync();

                return visitorEntry.Id;
            }

            await _context.VisitorEntryMasters.AddAsync(visitorEntry);

            await _context.SaveChangesAsync();

            return visitorEntry.Id;
        }

        public async Task<IEnumerable<VisitorEntryMaster>> GetAllVisitorEntryMaster()
        {

            return await _context.VisitorEntryMasters.Include(x=>x.employeeInfo).ToListAsync();
        }

        public async Task<VisitorEntryMaster> GetVisitorEntryMasterById(int id)
        {
            return await _context.VisitorEntryMasters.FindAsync(id);
        }

        public async Task<bool> DeleteVisitorEntryMasterById(int id)
        {
            _context.VisitorEntryMasters.Remove(_context.VisitorEntryMasters.Find(id));

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
