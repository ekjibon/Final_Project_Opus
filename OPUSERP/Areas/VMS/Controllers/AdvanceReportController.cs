using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.VMS.Models;
using OPUSERP.VMS.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.VMS.Controllers
{
    [Authorize]
    [Area("VMS")]
    public class AdvanceReportController : Controller
    {
        private readonly IVMSReportService reportService;

        public AdvanceReportController(IVMSReportService reportService)
        {
            this.reportService = reportService;
        }

        public async Task<IActionResult> Index()
        {
            ReportingViewModel model = new ReportingViewModel
            {
                reportFields=await reportService.GetAllReportField()
            };
            return View(model);
        }
    }
}