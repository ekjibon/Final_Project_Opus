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
    public class RlnSBUPNSController : Controller
    {
        private readonly IPNSService pNSService;
        private readonly ISpecialBranchUnitService specialBranchUnitService;
        private readonly IRlnSBUPNSService rlnSBUPNSService;

        public RlnSBUPNSController(IHostingEnvironment hostingEnvironment, IRlnSBUPNSService rlnSBUPNSService, ISpecialBranchUnitService specialBranchUnitService, IPNSService pNSService)
        {
            this.pNSService = pNSService;
            this.specialBranchUnitService = specialBranchUnitService;
            this.rlnSBUPNSService = rlnSBUPNSService;
        }

        public async Task<IActionResult> Index()
        {
            RlnSBUPNSViewModel model = new RlnSBUPNSViewModel
            {
                pNs = await pNSService.GetPNS(),
                specialBranchUnits = await specialBranchUnitService.GetSpecialBranchUnit(),
                rlnSBUPNs = await rlnSBUPNSService.GetRlnSBUPNS(),
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] RlnSBUPNSViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.pNs = await pNSService.GetPNS();
                model.specialBranchUnits = await specialBranchUnitService.GetSpecialBranchUnit();
                model.rlnSBUPNs = await rlnSBUPNSService.GetRlnSBUPNS();
                return View(model);
            }

            RlnSBUPNS data = new RlnSBUPNS
            {
                Id = model.Id ?? 0,
                specialBranchUnitId = model.specialBranchUnitId,
                pNSId = model.pNSId
            };

            await rlnSBUPNSService.SaveRlnSBUPNS(data);

            return RedirectToAction(nameof(Index));
        }

        #region API Section
        [AllowAnonymous]
        [Route("global/api/getRlnSBUPNSBySBUId/{sbuId}")]
        [HttpGet]
        public async Task<IActionResult> GetRlnSBUPNSBySBUId(int sbuId)
        {
            return Json(await rlnSBUPNSService.GetRlnSBUPNSBySBUId(sbuId));
        }
        #endregion
    }
}