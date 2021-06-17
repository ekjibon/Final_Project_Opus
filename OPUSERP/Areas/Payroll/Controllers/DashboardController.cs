using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Payroll.Models.Dashboard;
using OPUSERP.Payroll.Services.Dashboard.Interfaces;

namespace OPUSERP.Areas.Payroll.Controllers
{
    [Area("Payroll")]
    public class DashboardController : Controller
    {
        private readonly IPayrollDashboardService payrollDashboardService;

        public DashboardController(IPayrollDashboardService payrollDashboardService)
        {
            this.payrollDashboardService = payrollDashboardService;
        }

        public async Task<IActionResult> Index()
        {
            MainDashboardViewModel model = new MainDashboardViewModel
            {
                sCMDashboardcountViewModel = await payrollDashboardService.GetPayrollDataCountViewModel()
            };
            return View(model);
        }

        public async Task<IActionResult> DashboardValue()
        {
            MainDashboardViewModel model = new MainDashboardViewModel
            {
                sCMDashboardcountViewModel = await payrollDashboardService.GetPayrollDataCountViewModel()
            };
            return Json(model);
        }
    }
}