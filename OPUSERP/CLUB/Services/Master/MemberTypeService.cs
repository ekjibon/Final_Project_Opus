using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Master;
using Microsoft.EntityFrameworkCore;
using OPUSERP.CLUB.Services.Master.Interface;
using OPUSERP.CLUB.Data.Master;

namespace OPUSERP.CLUB.Services.Master
{
    public class MemberTypeService : IMemberTypeService
    {
        private readonly ERPDbContext _context;

        public MemberTypeService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MemberType>> GetAllMemberType()
        {
            return await _context.memberTypes.AsNoTracking().ToListAsync();
        }

        public async Task<MemberType> GetMemberTypeById(int id)
        {
            return await _context.memberTypes.FindAsync(id);
        }

        public async Task<bool> SaveMemberType(MemberType employeeType)
        {
            if(employeeType.Id != 0)
                _context.memberTypes.Update(employeeType);
            else
                _context.memberTypes.Add(employeeType);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteMemberTypeById(int id)
        {
            _context.memberTypes.Remove(_context.memberTypes.Find(id));
            return 1 ==  await _context.SaveChangesAsync();

        }

        //public async Task<IEnumerable<string>> GetTypNamesByIds(int[] ids)
        //{
        //   return await _context.employeeTypes.Where(x => ids.Contains(x.Id)).Select(x => x.empType).ToListAsync();
        //}
    }
}
