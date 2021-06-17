using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.CLUB.Services.Member.Interfaces;
using Microsoft.EntityFrameworkCore;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.Data;

namespace OPUSERP.CLUB.Services.Member
{
    public class SpouseChildrenService : ISpouseChildrenService
    {
        private readonly ERPDbContext _context;

        public SpouseChildrenService(ERPDbContext context)
        {
            _context = context;
        }

        //Spouse
        public async Task<bool> DeleteSpouseById(int id)
        {
            _context.memberSpouses.Remove(_context.memberSpouses.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MemberSpouse>> GetSpouse()
        {
            return await _context.memberSpouses.AsNoTracking().ToListAsync();
        }

        public async Task<MemberSpouse> GetSpouseById(int id)
        {
            return await _context.memberSpouses.FindAsync(id);
        }

        public async Task<bool> SaveSpouse(MemberSpouse spouse)
        {
            if (spouse.Id != 0)
                _context.memberSpouses.Update(spouse);
            else
                _context.memberSpouses.Add(spouse);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MemberSpouse>> GetSpouseByEmpId(int empId)
        {
            return await _context.memberSpouses.Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }

        //Children
        public async Task<bool> DeleteChildrenById(int id)
        {
            _context.childrens.Remove(_context.childrens.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MemberChildren>> GetChildren()
        {
            return await _context.memberChildrens.AsNoTracking().ToListAsync();
        }

        public async Task<MemberChildren> GetChildrenById(int id)
        {
            return await _context.memberChildrens.FindAsync(id);
        }

        public async Task<bool> SaveChildren(MemberChildren children)
        {
            if (children.Id != 0)
                _context.memberChildrens.Update(children);
            else
            _context.memberChildrens.Add(children);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MemberChildren>> GetChildrenByEmpId(int empId)
        {
            return await _context.memberChildrens.Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }
    }
}
