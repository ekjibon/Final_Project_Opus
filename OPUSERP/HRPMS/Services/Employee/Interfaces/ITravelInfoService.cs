using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface ITravelInfoService
    {
        Task<bool> SaveTraveInfo(TraveInfo traveInfo);
        Task<IEnumerable<TraveInfo>> GetTraveInfo();
        Task<TraveInfo> GetTraveInfoById(int id);
        Task<bool> DeleteTraveInfoById(int id);
        Task<IEnumerable<TraveInfo>> GetTraveInfoByEmpId(int empId);
    }
}
