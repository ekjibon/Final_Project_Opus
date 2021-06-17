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
    public class HRPMSActivityTypeController : Controller
    {
        private readonly IHRPMSActivityTypeService hRPMSActivityTypeService;

        public HRPMSActivityTypeController(IHostingEnvironment hostingEnvironment, IHRPMSActivityTypeService hRPMSActivityTypeService)
        {
            this.hRPMSActivityTypeService = hRPMSActivityTypeService;
        }
        // GET: Award
        public async Task<IActionResult> Index()
        {
            HRPMSActivityTypeViewModel model = new HRPMSActivityTypeViewModel
            {
                hRPMSActivityTypes = await hRPMSActivityTypeService.GetHRPMSActivityType()
            };
            return View(model);
        }

        // POST: Award/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] HRPMSActivityTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.hRPMSActivityTypes = await hRPMSActivityTypeService.GetHRPMSActivityType();
                return View(model);
            }

            HRPMSActivityType data = new HRPMSActivityType
            {
                Id = (int)model.HRPMSActivityTypeId,
                name = model.HRPMSActivityTypeName
            };

            await hRPMSActivityTypeService.SaveHRPMSActivityType(data);

            return RedirectToAction(nameof(Index));
        }
    }
}