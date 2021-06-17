using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSEmployee.Models.Dashboard;
using OPUSERP.HRPMS.Services.Dashboard.interfaces;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Authorize]
    [Area("HRPMSEmployee")]
    public class DashboardController : Controller
    {
        private readonly IHrmDashboardService hrmDashboardService;

        public DashboardController(IHrmDashboardService hrmDashboardService)
        {
            this.hrmDashboardService = hrmDashboardService;
        }

        public async Task<IActionResult> Index()
        {
            MainDashboardViewModel model = new MainDashboardViewModel
            {
                activeEmployeeCountViewModel = await hrmDashboardService.GetActiveEmployeeCountViewModel()
            };
            return View(model);
        }

        public async Task<IActionResult> DashboardValue()
        {
            MainDashboardViewModel model = new MainDashboardViewModel
            {
                activeEmployeeCountViewModel = await hrmDashboardService.GetActiveEmployeeCountViewModel()
            };
            return Json(model);
        }
    }
}