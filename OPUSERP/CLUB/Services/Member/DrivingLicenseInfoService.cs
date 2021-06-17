using Microsoft.EntityFrameworkCore;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.CLUB.Services.Member.Interfaces;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Member
{
    public class DrivingLicenseInfoService: IDrivingLicenseService
    {
        private readonly ERPDbContext _context;

        public DrivingLicenseInfoService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveDrivingLicenseInfo(MemberDrivingLicense drivingLicense)
        {

            if (drivingLicense.Id != 0)
                _context.memberDrivingLicenses.Update(drivingLicense);
            else
                _context.memberDrivingLicenses.Add(drivingLicense);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MemberDrivingLicense>> GetDrivingLicenseInfo()
        {
            return await _context.memberDrivingLicenses.AsNoTracking().ToListAsync();
        }

        public async Task<MemberDrivingLicense> GetDrivingLicenseById(int id)
        {
            return await _context.memberDrivingLicenses.FindAsync(id);
        }

        public async Task<bool> DeleteDrivingLicenseById(int id)
        {
            _context.memberDrivingLicenses.Remove(_context.memberDrivingLicenses.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MemberDrivingLicense>> GetDrivingLicenseByEmpId(int empId)
        {
            return await _context.memberDrivingLicenses.Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }
    }
}
