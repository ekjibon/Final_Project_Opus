using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IDrivingLicenseService
    {
        Task<bool> SaveDrivingLicenseInfo(DrivingLicense drivingLicense);
        Task<IEnumerable<DrivingLicense>> GetDrivingLicenseInfo();
        Task<DrivingLicense> GetDrivingLicenseById(int id);
        Task<bool> DeleteDrivingLicenseById(int id);
        Task<IEnumerable<DrivingLicense>> GetDrivingLicenseByEmpId(int empId);
    }
}
