using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRMMasterData.Models;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using System;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMMasterData.Controllers
{
    [Area("CRMMasterData")]
    public class ActivityStatusController : Controller
    {
        private readonly IActivityStatusService activityStatusService;

        public ActivityStatusController(IActivityStatusService activityStatusService)
        {
            this.activityStatusService = activityStatusService;
        }
        public async Task<IActionResult> Index()
        {
            ActivityStatusViewModel model = new ActivityStatusViewModel()
            {
                activityStatuses = await activityStatusService.GetAllActivityStatus()
            };
            return View(model);
        }

        // POST: Activity Status/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ActivityStatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.activityStatuses = await activityStatusService.GetAllActivityStatus();
                return View(model);
            }

            ActivityStatus data = new ActivityStatus
            {
                Id = model.statusId,
                status = model.statusName,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            await activityStatusService.SaveActivityStatus(data);

            return RedirectToAction(nameof(Index));
        }
    }
}