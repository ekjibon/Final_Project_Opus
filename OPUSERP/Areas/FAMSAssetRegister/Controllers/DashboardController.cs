using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.FAMSAssetRegister.Models.Dashboard;
using OPUSERP.FixedAsset.Services.Dashboard.Interfaces;

namespace OPUSERP.Areas.FAMSAssetRegister.Controllers
{
    [Area("FAMSAssetRegister")]
    public class DashboardController : Controller
    {
        private readonly IFAMSDashboardService fAMSDashboardService;

        public DashboardController(IFAMSDashboardService fAMSDashboardService)
        {
            this.fAMSDashboardService = fAMSDashboardService;
        }

        public async Task<IActionResult> Index()
        {
            MainDashboardViewModel model = new MainDashboardViewModel
            {
                fAMSDashboardcountViewModel = await fAMSDashboardService.GetFAMSDashboardcountViewModel()
            };
            return View(model);
        }

        public async Task<IActionResult> DashboardValue()
        {
            MainDashboardViewModel model = new MainDashboardViewModel
            {
                fAMSDashboardcountViewModel = await fAMSDashboardService.GetFAMSDashboardcountViewModel()
            };
            return Json(model);
        }
    }
}