using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.HRPMS.Data.Entity.Employee;

namespace OPUSERP.HRPMS.Services.ACR.Interfaces
{
    public interface IAcrInfoService
    {
        Task<int> SaveACRInfo(AcrInfo acrInfo);
        Task<IEnumerable<AcrInfo>> GetAcrInfo();
        Task<AcrInfo> GetAcrInfoById(int id);
        Task<bool> DeleteAcrInfoById(int id);
        Task<IEnumerable<AcrInfo>> GetAcrInfoByEmpId(int empId);
    }
}
