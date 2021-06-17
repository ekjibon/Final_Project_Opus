using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.CLUB.Services.Member.Interfaces;

namespace OPUSERP.CLUB.Services.Member
{
    public class MembershipService : IMembershipService
    {
        private readonly ERPDbContext _context;

        public MembershipService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteMembershipInfoById(int id)
        {
            _context.employeeMemberships.Remove(_context.employeeMemberships.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OtherMembership>> GetMembershipInfo()
        {
            return await _context.otherMemberships.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<OtherMembership>> GetMembershipInfoByEmpId(int empId)
        {
            return await _context.otherMemberships.Where(x => x.employeeId == empId).Include(x => x.membership).AsNoTracking().ToListAsync();
        }

        public async Task<OtherMembership> GetMembershipInfoById(int id)
        {
            return await _context.otherMemberships.FindAsync(id);
        }

        public async Task<bool> SaveMembershipInfo(OtherMembership employeeMembership)
        {
            if (employeeMembership.Id != 0)
                _context.otherMemberships.Update(employeeMembership);
            else
                _context.otherMemberships.Add(employeeMembership);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
