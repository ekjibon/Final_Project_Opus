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
    public class FunctionController : Controller
    {
        private readonly IFunctionsInfoService specialBranchUnitService;
        private readonly IERPCompanyService eRPCompanyService;

        public FunctionController(IFunctionsInfoService specialBranchUnitService, IERPCompanyService eRPCompanyService)
        {
            this.specialBranchUnitService = specialBranchUnitService;
            this.eRPCompanyService = eRPCompanyService;
        }
        // GET: Award
        public async Task<IActionResult> Index()
        {
            FunctionViewModel model = new FunctionViewModel
            {
                functionInfos = await specialBranchUnitService.GetFunctionInfo(),
                companies = await eRPCompanyService.GetAllCompany()
            };
            return View(model);
        }

        // POST: Award/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] FunctionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.functionInfos = await specialBranchUnitService.GetFunctionInfo();
                return View(model);
            }

            FunctionInfo data = new FunctionInfo
            {
                Id = model.id,
                branchUnitName = model.name,
                branchCode = model.branchCode,
                companyId = model.companyId
            };

            await specialBranchUnitService.SaveFunctionInfo(data);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteSpecialBranchUnitById(int Id)
        {            
            await specialBranchUnitService.DeleteFunctionInfoById(Id);
            return Json(true);
        }
    }
}