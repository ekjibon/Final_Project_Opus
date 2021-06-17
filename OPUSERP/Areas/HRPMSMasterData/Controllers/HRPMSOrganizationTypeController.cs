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
    public class HRPMSOrganizationTypeController : Controller
    {
        private readonly IHRPMSOrganizationTypeService hRPMSOrganizationTypeService;

        public HRPMSOrganizationTypeController(IHostingEnvironment hostingEnvironment, IHRPMSOrganizationTypeService hRPMSOrganizationTypeService)
        {
            this.hRPMSOrganizationTypeService = hRPMSOrganizationTypeService;
        }
        // GET: Award
        public async Task<IActionResult> Index()
        {
            HRPMSOrganizationTypeViewModel model = new HRPMSOrganizationTypeViewModel
            {
                hRPMSOrganizationTypes = await hRPMSOrganizationTypeService.GetHRPMSOrganizationType()
            };
            return View(model);
        }

        // POST: Award/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] HRPMSOrganizationTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.hRPMSOrganizationTypes = await hRPMSOrganizationTypeService.GetHRPMSOrganizationType();
                return View(model);
            }

            HRPMSOrganizationType data = new HRPMSOrganizationType
            {
                Id = (int)model.HRPMSOrganizationTypeId,
                name = model.HRPMSOrganizationTypeName
            };

            await hRPMSOrganizationTypeService.SaveHRPMSOrganizationType(data);

            return RedirectToAction(nameof(Index));
        }
    }
}