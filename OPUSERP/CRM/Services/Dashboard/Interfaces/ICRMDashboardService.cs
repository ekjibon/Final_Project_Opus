using OPUSERP.Areas.CRMClient.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Dashboard.Interfaces
{
    public interface ICRMDashboardService
    {
        Task<CRMDataCountViewModel> GetRMDataCountViewModel();
    }
}
