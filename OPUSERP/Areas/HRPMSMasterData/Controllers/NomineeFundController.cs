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
    public class NomineeFundController : Controller
    {
        private readonly INomineeFundService nomineeFundService;

        public NomineeFundController(IHostingEnvironment hostingEnvironment, INomineeFundService nomineeFundService)
        {
            this.nomineeFundService = nomineeFundService;
        }
        // GET: Award
        public async Task<IActionResult> Index()
        {
            NomineeFundViewModel model = new NomineeFundViewModel
            {
                nomineeFunds = await nomineeFundService.GetNomineeFund()
            };
            return View(model);
        }

        // POST: Award/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] NomineeFundViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.nomineeFunds = await nomineeFundService.GetNomineeFund();
                return View(model);
            }

            NomineeFund data = new NomineeFund
            {
                Id = (int)model.nomineeFundId,
                name = model.nomineeFundName
            };

            await nomineeFundService.SaveNomineeFund(data);

            return RedirectToAction(nameof(Index));
        }
    }
}