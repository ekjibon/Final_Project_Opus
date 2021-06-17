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
    public class HRProjectController : Controller
    {
        private readonly IHRProjectService hRProjectService;

        public HRProjectController(IHostingEnvironment hostingEnvironment, IHRProjectService hRProjectService)
        {
            this.hRProjectService = hRProjectService;
        }
        // GET: Award
        public async Task<IActionResult> Index()
        {
            HRProjectViewModel model = new HRProjectViewModel
            {
                hRProjects = await hRProjectService.GetHRProject(),
            };
            return View(model);
        }

        // POST: Award/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] HRProjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.hRProjects = await hRProjectService.GetHRProject();
                return View(model);
            }

            HRProject data = new HRProject
            {
                Id = model.Id ?? 0,
                code = model.code,
                name = model.name
            };

            await hRProjectService.SaveHRProject(data);

            return RedirectToAction(nameof(Index));
        }
    }
}