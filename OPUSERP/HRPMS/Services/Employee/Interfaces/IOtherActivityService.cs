using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IOtherActivityService
    {
        Task<bool> DeleteOtherActivityById(int id);
        Task<IEnumerable<OtherActivity>> GetOtherActivity();
        Task<IEnumerable<OtherActivity>> GetOtherActivityByEmpId(int empId);
        Task<OtherActivity> GetOtherActivityById(int id);
        Task<bool> SaveOtherActivity(OtherActivity otherActivity);
    }
}
