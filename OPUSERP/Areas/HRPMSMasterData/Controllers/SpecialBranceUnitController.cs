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
    public class SpecialBranceUnitController : Controller
    {
        private readonly ISpecialBranchUnitService specialBranchUnitService;
        private readonly IERPCompanyService eRPCompanyService;

        public SpecialBranceUnitController(ISpecialBranchUnitService specialBranchUnitService, IERPCompanyService eRPCompanyService)
        {
            this.specialBranchUnitService = specialBranchUnitService;
            this.eRPCompanyService = eRPCompanyService;
        }
        // GET: Award
        public async Task<IActionResult> Index()
        {
            SpecialBranchUnitViewModel model = new SpecialBranchUnitViewModel
            {
                specialBranchUnits = await specialBranchUnitService.GetSpecialBranchUnit(),
                companies = await eRPCompanyService.GetAllCompany()
            };
            return View(model);
        }

        // POST: Award/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] SpecialBranchUnitViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.specialBranchUnits = await specialBranchUnitService.GetSpecialBranchUnit();
                return View(model);
            }

            SpecialBranchUnit data = new SpecialBranchUnit
            {
                Id = model.id,
                branchUnitName = model.name,
                branchCode = model.branchCode,
                companyId = model.companyId,
                isdefault=model.isdefault
            };

            await specialBranchUnitService.SaveSpecialBranchUnit(data);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteSpecialBranchUnitById(int Id)
        {            
            await specialBranchUnitService.DeleteSpecialBranchUnitById(Id);
            return Json(true);
        }
    }
}