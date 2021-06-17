using OPUSERP.Areas.CRMClient.Models.Dashboard;
using OPUSERP.Areas.FAMSAssetRegister.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.FixedAsset.Services.Dashboard.Interfaces
{
    public interface IFAMSDashboardService
    {
        Task<FAMSDashboardcountViewModel> GetFAMSDashboardcountViewModel();
    }
}
