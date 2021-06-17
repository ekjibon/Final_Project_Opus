using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.HRPMS.Data.Entity.Attendance;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Wages;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface IWagesPunchCardInfoService
    {
        Task<bool> SaveEmployeePunchCardInfo(WagesPunchCardInfo employeePunchCardInfo);
        Task<IEnumerable<WagesPunchCardInfo>> GetAllEmployeePunchCardInfo();
        Task<WagesPunchCardInfo> GetEmployeePunchCardInfoById(int id);
        Task<bool> DeleteEmployeePunchCardInfoById(int id);
    }
}
