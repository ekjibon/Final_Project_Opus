using OPUSERP.CLUB.Services.Member.Interfaces;
using Microsoft.EntityFrameworkCore;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Member
{
    public class PassportInfoService: IPassportInfoService
    {
        private readonly ERPDbContext _context;

        public PassportInfoService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SavePassportInfo(MemberPassportDetails passportDetail)
        {
            if (passportDetail.Id != 0)
                _context.memberPassportDetails.Update(passportDetail);
            else
                _context.memberPassportDetails.Add(passportDetail);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MemberPassportDetails>> GetPassportInfo()
        {
            return await _context.memberPassportDetails.AsNoTracking().ToListAsync();
        }

        public async Task<MemberPassportDetails> GetPassportInfoById(int id)
        {
            return await _context.memberPassportDetails.FindAsync(id);
        }

        public async Task<bool> DeletePassportInfoById(int id)
        {
            _context.memberPassportDetails.Remove(_context.memberPassportDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MemberPassportDetails>> GetPassportInfoByEmpId(int empId)
        {
            return await _context.memberPassportDetails.Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }
    }
}
