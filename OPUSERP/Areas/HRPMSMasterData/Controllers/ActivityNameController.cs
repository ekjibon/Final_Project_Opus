using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSMasterData.Models;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.HRPMSMasterData.Controllers
{
    [Authorize]
    [Area("HRPMSMasterData")]
    public class ActivityNameController : Controller
    {
        private readonly IActivityNameService activityNameService;
        private readonly IHRPMSActivityTypeService hRPMSActivityTypeService;

        public ActivityNameController(IHostingEnvironment hostingEnvironment, IHRPMSActivityTypeService hRPMSActivityTypeService, IActivityNameService activityNameService)
        {
            this.activityNameService = activityNameService;
            this.hRPMSActivityTypeService = hRPMSActivityTypeService;
        }
        // GET: Award
        public async Task<IActionResult> Index()
        {
            ActivityNameViewModel model = new ActivityNameViewModel
            {
                activityNames = await activityNameService.GetActivityName(),
                hRPMSActivityTypes = await hRPMSActivityTypeService.GetHRPMSActivityType(),
            };
            return View(model);
        }

        // POST: Award/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ActivityNameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.activityNames = await activityNameService.GetActivityName();
                model.hRPMSActivityTypes = await hRPMSActivityTypeService.GetHRPMSActivityType();
                return View(model);
            }

            ActivityName data = new ActivityName
            {
                Id = (int)model.ActivityNameId,
                activityTypeId = (int)model.ActivityType,
                name = model.ActivityName
            };

            await activityNameService.SaveActivityName(data);

            return RedirectToAction(nameof(Index));
        }

        #region API Section
        [Route("global/api/getActivityNameByType/{typeId}")]
        [HttpGet]
        public async Task<IActionResult> GetActivityNameByType(int typeId)
        {
            return Json(await activityNameService.GetActivityNameByType(typeId));
        }
        #endregion

    }
}