using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Visitor;
using OPUSERP.HRPMS.Services.Visitor.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Visitor
{
    public class PIMSVisitorService:IPIMSVisitorService
    {
        private readonly ERPDbContext _context;

        public PIMSVisitorService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeletePIMSVisitorById(int id)
        {
            _context.pIMSVisitors.Remove(_context.pIMSVisitors.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PIMSVisitor>> GetPIMSVisitor()
        {
            return await _context.pIMSVisitors.AsNoTracking().ToListAsync();
        }

        public async Task<PIMSVisitor> GetPIMSVisitorById(int id)
        {
            return await _context.pIMSVisitors.Where(x => x.Id == id).FirstAsync();
        }

        public async Task<bool> SavePIMSVisitor(PIMSVisitor pIMSVisitor)
        {
            if (pIMSVisitor.Id != 0)
                _context.pIMSVisitors.Update(pIMSVisitor);
            else
                _context.pIMSVisitors.Add(pIMSVisitor);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
