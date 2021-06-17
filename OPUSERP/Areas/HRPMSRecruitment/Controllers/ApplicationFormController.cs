using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSRecruitment.Models;
using OPUSERP.HRPMS.Data.Entity.Recruitment;
using OPUSERP.HRPMS.Services.Recruitment.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.HRPMSRecruitment.Controllers
{
    [Authorize]
    [Area("HRPMSRecruitment")]
    public class ApplicationFormController : Controller
    {
        private readonly IApplicationFormService applicationFormService;

        public ApplicationFormController(IApplicationFormService applicationFormService)
        {
            this.applicationFormService = applicationFormService;
        }

        // GET: ApplicationForm
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
        public async Task<IActionResult> Index([FromForm] ApplicationFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.applicationForms = await applicationFormService.GetApplicationForm();
                return View(model);
            }

            ApplicationForm data = new ApplicationForm
            {
                nameBN = model.nameBn,
                nameEN = model.nameEn,
                nidNO = model.nid,
                binNO = model.bin,
                birthDate = Convert.ToDateTime(model.dateOfBirth),
                birtPlace = model.locationOfBirth,
                payRef = model.paymentRefno,
                fNmaeBN = model.fnameBn,
                fNmaeEN = model.fnameEn,
                mNmaeBN = model.mnameBn,
                mNmaeEN = model.mnameEn,
                sNameBN = model.spouseNameBn,
                sNameEN = model.spouseNameEn,
                mobile = model.mobile,
                email = model.email,
                nationality = model.nationality,
                religionId = 2,
                occupation = model.occupation,
                gender = model.Gender,
                type = ""
            };

            await applicationFormService.SaveApplicationForm(data);

            return RedirectToAction(nameof(Index));
        }
    }
}