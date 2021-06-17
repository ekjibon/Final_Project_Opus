using OPUSERP.Areas.SCMDashboard.Models;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Dashboard.Interfaces
{
    public interface ISCMDashboardService
    {
        Task<SCMDashboardcountViewModel> GetSCMDataCountViewModel();
    }
}
