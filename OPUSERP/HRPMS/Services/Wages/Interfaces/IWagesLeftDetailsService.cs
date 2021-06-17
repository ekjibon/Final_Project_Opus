using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.HRPMS.Data.Entity.Attendance;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Wages;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface IWagesLeftDetailsService
    {
        Task<bool> SaveWagesLeftDetails(WagesLeftDetails wagesLeftDetails);
        Task<IEnumerable<WagesLeftDetails>> GetWagesLeftDetails();
        Task<WagesLeftDetails> GetWagesLeftDetailsById(int id);
        Task<bool> DeleteWagesLeftDetailsById(int id);
    }
}
