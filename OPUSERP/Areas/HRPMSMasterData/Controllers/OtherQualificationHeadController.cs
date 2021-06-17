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
    public class OtherQualificationHeadController : Controller
    {
        private readonly IOtherQualificationHeadService otherQualificationHeadService;

        public OtherQualificationHeadController(IHostingEnvironment hostingEnvironment, IOtherQualificationHeadService otherQualificationHeadService)
        {
            this.otherQualificationHeadService = otherQualificationHeadService;
        }
        // GET: Award
        public async Task<IActionResult> Index()
        {
            OtherQualificationHeadViewModel model = new OtherQualificationHeadViewModel
            {
                otherQualificationHeads = await otherQualificationHeadService.GetOtherQualificationHead()
            };
            return View(model);
        }

        // POST: Award/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] OtherQualificationHeadViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.otherQualificationHeads = await otherQualificationHeadService.GetOtherQualificationHead();
                return View(model);
            }

            OtherQualificationHead data = new OtherQualificationHead
            {
                Id = (int)model.OtherQualificationHeadId,
                name = model.OtherQualificationHeadName
            };

            await otherQualificationHeadService.SaveOtherQualificationHead(data);

            return RedirectToAction(nameof(Index));
        }
    }
}