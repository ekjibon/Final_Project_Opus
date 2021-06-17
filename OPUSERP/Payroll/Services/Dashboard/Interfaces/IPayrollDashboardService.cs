using OPUSERP.Areas.CRMClient.Models.Dashboard;
using OPUSERP.Areas.Payroll.Models.Dashboard;
using OPUSERP.Areas.SCMDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Payroll.Services.Dashboard.Interfaces
{
    public interface IPayrollDashboardService
    {
        Task<PayrollDashboardcountViewModel> GetPayrollDataCountViewModel();
    }
}
