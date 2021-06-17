using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.HRPMS.Data.Entity.Attendance;
using OPUSERP.HRPMS.Data.Entity.Master;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface IEmployeePunchCardInfoService
    {
        Task<bool> SaveEmployeePunchCardInfo(EmployeePunchCardInfo employeePunchCardInfo);
        Task<IEnumerable<EmployeePunchCardInfo>> GetAllEmployeePunchCardInfo();
        Task<EmployeePunchCardInfo> GetEmployeePunchCardInfoById(int id);
        Task<bool> DeleteEmployeePunchCardInfoById(int id);
    }
}
