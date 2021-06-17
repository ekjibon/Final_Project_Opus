using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSMasterData.Models;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.HRPMSMasterData.Controllers
{
    [Authorize]
    [Area("HRPMSMasterData")]
    public class LocationController : Controller
    {
        private readonly ILocationService specialBranchUnitService;
        private readonly IERPCompanyService eRPCompanyService;

        public LocationController(ILocationService specialBranchUnitService, IERPCompanyService eRPCompanyService)
        {
            this.specialBranchUnitService = specialBranchUnitService;
            this.eRPCompanyService = eRPCompanyService;
        }
        // GET: Award
        public async Task<IActionResult> Index()
        {
            LocationViewModel model = new LocationViewModel
            {
                locations = await specialBranchUnitService.GetLocation(),
                companies = await eRPCompanyService.GetAllCompany()
            };
            return View(model);
        }

        // POST: Award/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] LocationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.locations = await specialBranchUnitService.GetLocation();
                return View(model);
            }

            Location data = new Location
            {
                Id = model.id,
                branchUnitName = model.name,
                branchCode = model.branchCode,
                companyId = model.companyId
            };

            await specialBranchUnitService.SaveLocation(data);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteSpecialBranchUnitById(int Id)
        {            
            await specialBranchUnitService.DeleteLocationById(Id);
            return Json(true);
        }
    }
}