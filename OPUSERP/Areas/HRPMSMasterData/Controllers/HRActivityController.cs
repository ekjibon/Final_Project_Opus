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
    public class HRActivityController : Controller
    {
        private readonly IHRActivityService hRActivityService;

        public HRActivityController(IHostingEnvironment hostingEnvironment, IHRActivityService hRActivityService)
        {
            this.hRActivityService = hRActivityService;
        }

        #region HR Activity
        public async Task<IActionResult> Index()
        {
            HRActivityViewModel model = new HRActivityViewModel
            {
                hRActivities = await hRActivityService.GetHRActivity(),
            };
            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] HRActivityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.hRActivities = await hRActivityService.GetHRActivity();
                return View(model);
            }

            HRActivity data = new HRActivity
            {
                Id = model.Id ?? 0,
                code = model.code,
                name = model.name
            };

            await hRActivityService.SaveHRActivity(data);

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region HR Facility

        public async Task<IActionResult> HRFacility()
        {
            HRFacilityViewModel model = new HRFacilityViewModel
            {
                hRFacilities = await hRActivityService.GetHRFacility(),
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HRFacility([FromForm] HRFacilityViewModel model)
        {

            HRFacility data = new HRFacility
            {
                Id = model.Id ?? 0,
                facilityCode = model.facilityCode,
                facilityName = model.facilityName
            };

            await hRActivityService.SaveHRFacility(data);
            return RedirectToAction(nameof(HRFacility));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteHRFacilityById(int Id)
        {
            await hRActivityService.DeleteHRFacilityById(Id);
            return Json(true);
        }

        #endregion
    }
}