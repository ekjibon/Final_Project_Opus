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
    public class HRDonerController : Controller
    {
        private readonly IHRDonerSevice hRDonerSevice;

        public HRDonerController(IHostingEnvironment hostingEnvironment, IHRDonerSevice hRDonerSevice)
        {
            this.hRDonerSevice = hRDonerSevice;
        }
        // GET: Award
        public async Task<IActionResult> Index()
        {
            HRDonerViewModel model = new HRDonerViewModel
            {
                hRDoners = await hRDonerSevice.GetHRDoner(),
            };
            return View(model);
        }

        // POST: Award/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] HRDonerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.hRDoners = await hRDonerSevice.GetHRDoner();
                return View(model);
            }

            HRDoner data = new HRDoner
            {
                Id = model.Id ?? 0,
                code = model.code,
                name = model.name,
                contactNumber = model.contactNumber,
                Address = model.Address
            };

            await hRDonerSevice.SaveHRDoner(data);

            return RedirectToAction(nameof(Index));
        }

    }
}