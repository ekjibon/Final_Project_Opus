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
    public class PNSController : Controller
    {
        private readonly IPNSService pNSService;

        public PNSController(IHostingEnvironment hostingEnvironment, IPNSService pNSService)
        {
            this.pNSService = pNSService;
        }
        
        public async Task<IActionResult> Index()
        {
            PNSViewModel model = new PNSViewModel
            {
                pNs = await pNSService.GetPNS(),
            };
            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] PNSViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.pNs = await pNSService.GetPNS();
                return View(model);
            }

            PNS data = new PNS
            {
                Id = model.Id ?? 0,
                code = model.code,
                name = model.name
            };

            await pNSService.SavePNS(data);

            return RedirectToAction(nameof(Index));
        }


    }
}