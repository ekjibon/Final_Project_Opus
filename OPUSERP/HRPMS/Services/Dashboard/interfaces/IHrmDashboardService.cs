using OPUSERP.Areas.HRPMSEmployee.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Dashboard.interfaces
{
    public interface IHrmDashboardService
    {
        Task<ActiveEmployeeCountViewModel> GetActiveEmployeeCountViewModel();
    }
}
