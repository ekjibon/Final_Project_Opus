using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRMClient.Models.Dashboard;
using OPUSERP.CRM.Services.Dashboard.Interfaces;

namespace OPUSERP.Areas.CRMLead.Controllers
{
    [Authorize]
    [Area("CRMLead")]
    public class DashboardController : Controller
    {
        private readonly ICRMDashboardService iCRMDashboardService;

        public DashboardController(ICRMDashboardService iCRMDashboardService)
        {
            this.iCRMDashboardService = iCRMDashboardService;
        }

        public async Task<IActionResult> Index()
        {
            MainDashboardViewModel model = new MainDashboardViewModel
            {
               cRMDataCountViewModel = await iCRMDashboardService.GetRMDataCountViewModel()
            };
            return View(model);
        }

        public async Task<IActionResult> DashboardValue()
        {
            MainDashboardViewModel model = new MainDashboardViewModel
            {
                cRMDataCountViewModel = await iCRMDashboardService.GetRMDataCountViewModel()
            };
            return Json(model);
        }
    }
}