using Microsoft.EntityFrameworkCore;
using OPUSERP.CLUB.Data.Bulk;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.CLUB.Services.Bulk.Interface;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Bulk
{
    public class RlnGroupMemberService: IRlnGroupMemberService
    {
        private readonly ERPDbContext _context;

        public RlnGroupMemberService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveRlnMemberGroup(RlnMemberGroup rlnMemberGroup)
        {
            if (rlnMemberGroup.Id != 0)
                _context.rlnMemberGroups.Update(rlnMemberGroup);
            else
                _context.rlnMemberGroups.Add(rlnMemberGroup);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RlnMemberGroup>> GetRlnMemberGroup()
        {
            return await _context.rlnMemberGroups.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<RlnMemberGroup>> GetRlnMemberGroupByGroupId(int groupId)
        {
            return await _context.rlnMemberGroups.Where(x=>x.memberGroupId== groupId).Include(x=>x.memberGroup).Include(x=>x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MemberInfo>> GetEmployeeInfoByGroupId(int groupId)
        {
            List<int?> ids = await _context.rlnMemberGroups.Where(x => x.memberGroupId == groupId).Select(x=>x.employeeId).ToListAsync();
            return await _context.memberInfos.Where(x => x.orgType == "ministry").Where(x=> !ids.Contains(x.Id)).AsNoTracking().ToListAsync();
        }

        public async Task<RlnMemberGroup> GetRlnMemberGroupById(int id)
        {
            return await _context.rlnMemberGroups.FindAsync(id);
        }

        public async Task<bool> DeleteRlnMemberGroupById(int id)
        {
            _context.rlnMemberGroups.Remove(_context.rlnMemberGroups.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
