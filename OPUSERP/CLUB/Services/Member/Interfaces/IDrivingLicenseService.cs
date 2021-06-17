using OPUSERP.CLUB.Data.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Member.Interfaces
{
    public interface IDrivingLicenseService
    {
        Task<bool> SaveDrivingLicenseInfo(MemberDrivingLicense drivingLicense);
        Task<IEnumerable<MemberDrivingLicense>> GetDrivingLicenseInfo();
        Task<MemberDrivingLicense> GetDrivingLicenseById(int id);
        Task<bool> DeleteDrivingLicenseById(int id);
        Task<IEnumerable<MemberDrivingLicense>> GetDrivingLicenseByEmpId(int empId);
    }
}
