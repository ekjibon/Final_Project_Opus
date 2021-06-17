using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSRecruitment.Models;
using OPUSERP.HRPMS.Services.Recruitment.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.HRPMSRecruitment.Controllers
{
    [Authorize]
    [Area("HRPMSRecruitment")]
    public class ValidApplicantsController : Controller
    {
        private readonly IApplicationFormService applicationFormService;

        public ValidApplicantsController(IApplicationFormService applicationFormService)
        {
            this.applicationFormService = applicationFormService;
        }

        // GET: CreateJobCircular
        public async Task<IActionResult> Index()
        {
            ApplicationFormViewModel model = new ApplicationFormViewModel
            {
                applicationForms = await applicationFormService.GetApplicationForm()
            };
            return View(model);
        }

        // POST: CreateJobCircular/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] ValidApplicantsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}