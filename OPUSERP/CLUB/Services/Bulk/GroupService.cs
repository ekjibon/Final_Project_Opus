using Microsoft.EntityFrameworkCore;
using OPUSERP.CLUB.Data.Bulk;
using OPUSERP.CLUB.Services.Bulk.Interface;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Bulk
{
    public class GroupService: IGroupService
    {
        private readonly ERPDbContext _context;

        public GroupService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveMemberGroup(MemberGroup memberGroup)
        {
            if (memberGroup.Id != 0)
                _context.memberGroups.Update(memberGroup);
            else
                _context.memberGroups.Add(memberGroup);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MemberGroup>> GetMemberGroup()
        {
            return await _context.memberGroups.AsNoTracking().ToListAsync();
        }

        public async Task<MemberGroup> GetMemberGroupById(int id)
        {
            return await _context.memberGroups.FindAsync(id);
        }

        public async Task<bool> DeleteMemberGroupById(int id)
        {
            _context.memberGroups.Remove(_context.memberGroups.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

    }
}
