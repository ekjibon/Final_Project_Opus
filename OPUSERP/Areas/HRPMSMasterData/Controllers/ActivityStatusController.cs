using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSMasterData.Models;
using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.HRPMS.Services.Dashboard.interfaces;

namespace OPUSERP.Areas.HRPMSMasterData.Controllers
{
    [Authorize]
    [Area("HRPMSMasterData")]
    public class ActivityStatusController : Controller
    {
        private readonly LangGenerate<ActivityStatusLn> _lang;
        private readonly IStatusService statusService;
        private readonly IHrmDashboardService hrmDashboardService;

        public ActivityStatusController(IHostingEnvironment hostingEnvironment, IHrmDashboardService hrmDashboardService, IStatusService statusService)
        {
            _lang = new LangGenerate<ActivityStatusLn>(hostingEnvironment.ContentRootPath);
            this.statusService = statusService;
            this.hrmDashboardService = hrmDashboardService;
        }
         
        // GET: ActivityStatus
        public async Task<IActionResult> Index()
        {
            ActivityStatusViewModel model = new ActivityStatusViewModel
            {
                fLang = _lang.PerseLang("MasterData/ActivityStatusEN.json", "MasterData/ActivityStatusBN.json", Request.Cookies["lang"]),
                activityStatus = await statusService.GetActivityStatus()
            };
            return View(model);
        }

        // GET: ActivityStatus
        public async Task<IActionResult> Dashboard()
        {
            ActivityStatusViewModel model = new ActivityStatusViewModel
            {
                activityStatus = await statusService.GetActivityStatus()
            };
            return View(model);
        }

        // POST: ActivityStatus/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ActivityStatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/ActivityStatusEN.json", "MasterData/ActivityStatusBN.json", Request.Cookies["lang"]);
                model.activityStatus = await statusService.GetActivityStatus();
                return View(model);
            }

            ActivityStatus data = new ActivityStatus
            {
                Id = Int32.Parse(model.activityId),
                statusName = model.statusName,
                statusNameBn = model.statusNameBn,
                shortName = model.shortName
            };

            await statusService.SaveActivityStatus(data);

            return RedirectToAction(nameof(Index));
        }
    }
}