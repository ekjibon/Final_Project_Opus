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
    public class QualificationHeadController : Controller
    {
        private readonly IQualificationHeadService qualificationHeadService;

        public QualificationHeadController(IHostingEnvironment hostingEnvironment, IQualificationHeadService qualificationHeadService)
        {
            this.qualificationHeadService = qualificationHeadService;
        }
        // GET: Award
        public async Task<IActionResult> Index()
        {
            QualificationHeadViewModel model = new QualificationHeadViewModel
            {
                qualificationHeads = await qualificationHeadService.GetQualificationHead()
            };
            return View(model);
        }

        // POST: Award/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] QualificationHeadViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.qualificationHeads = await qualificationHeadService.GetQualificationHead();
                return View(model);
            }

            QualificationHead data = new QualificationHead
            {
                Id = (int)model.qualificationHeadId,
                name = model.qualificationHeadName
            };

            await qualificationHeadService.SaveQualificationHead(data);

            return RedirectToAction(nameof(Index));
        }
    }
}