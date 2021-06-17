using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.SCMDashboard.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.SCM.Services.Dashboard.Interfaces;
using OPUSERP.SCM.Services.Requisition.Interfaces;

namespace OPUSERP.Areas.SCMDashboard.Controllers
{
    [Area("SCMDashboard")]
    public class DashboardController : Controller
    {
        private readonly IRequisitionService requisitionService;
        private readonly IUserInfoes userInfoes;
        private readonly ISCMDashboardService sCMDashboardService;

        public DashboardController(IRequisitionService requisitionService, IUserInfoes userInfoes, ISCMDashboardService sCMDashboardService)
        {
            this.requisitionService = requisitionService;
            this.userInfoes = userInfoes;
            this.sCMDashboardService = sCMDashboardService;
        }


        // GET: Dashboard
        public async Task<IActionResult> Index()
        {
            var user = await userInfoes.GetUserInfoByUser(User.Identity.Name);
            DashboardViewModel model = new DashboardViewModel
            {
                dashboardModels = await requisitionService.GetRequisitionMasterListForDashboard(user.UserId),
                dashboardVMs=await requisitionService.GetTopFiveUsedItem(user.UserId)
            };
            return View(model);
        }

        // GET: Dashboard/Details/5
        
        public async Task<IActionResult> Dashboard()
        {
            MainDashboardViewModel model = new MainDashboardViewModel
            {
                sCMDashboardcountViewModel = await sCMDashboardService.GetSCMDataCountViewModel()
            };
            return View(model);
        }

        public async Task<IActionResult> DashboardValue()
        {
            MainDashboardViewModel model = new MainDashboardViewModel
            {
                sCMDashboardcountViewModel = await sCMDashboardService.GetSCMDataCountViewModel()
            };
            return Json(model);
        }


        [HttpGet]
        [Route("/api/dashoard/GetCurrentMonthPRStatus")]
        public async Task<JsonResult> GetCurrentMonthPRStatus()
        {
            var user = await userInfoes.GetUserInfoByUser(User.Identity.Name);
            var result = await requisitionService.GetCurrentMonthPRStatus(user.UserId);
            return Json(result);
        }
    }
}