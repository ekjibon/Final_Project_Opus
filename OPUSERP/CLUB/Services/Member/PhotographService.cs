using Microsoft.EntityFrameworkCore;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.CLUB.Services.Member.Interfaces;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Member
{
    public class Photographservice : IPhotographService
    {
        private readonly ERPDbContext _context;

        public Photographservice(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeletePhotographById(int id)
        {
            _context.memberPhotographs.Remove(_context.memberPhotographs.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<MemberPhotograph> GetPhotographByEmpIdAndType(int empId, string type)
        {
            return await _context.memberPhotographs.Where(x => x.type == type && x.employeeId == empId).FirstOrDefaultAsync();
        }

        public async Task<MemberPhotograph> GetPhotographById(int id)
        {
            return await _context.memberPhotographs.FindAsync(id);
        }

        public async Task<IEnumerable<MemberPhotograph>> GetPhotographs()
        {
            return await _context.memberPhotographs.AsNoTracking().ToListAsync();
        }

        public async Task<bool> SavePhotograph(MemberPhotograph photograph)
        {
            if (photograph.Id != 0)
                _context.memberPhotographs.Update(photograph);
            else
                _context.memberPhotographs.Add(photograph);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
