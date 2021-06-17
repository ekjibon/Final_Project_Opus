using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSRecruitment.Models;
using OPUSERP.Areas.HRPMSRecruitment.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Recruitment;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Recruitment.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.HRPMSRecruitment.Controllers
{
    [Authorize]
    [Area("HRPMSRecruitment")]
    public class CreateJobCircularController : Controller
    {
        private readonly LangGenerate<RecruitmentLn> _lang;
        private readonly IApplicationFormService applicationFormService;

        public CreateJobCircularController(IHostingEnvironment hostingEnvironment, IApplicationFormService applicationFormService)
        {
            _lang = new LangGenerate<RecruitmentLn>(hostingEnvironment.ContentRootPath);
            this.applicationFormService = applicationFormService;
        }

        // GET: CreateJobCircular
        public async Task<IActionResult> Index()
        {
            CreateJobCircularViewModel model = new CreateJobCircularViewModel
            {
                fLang = _lang.PerseLang("Recruitment/CreateJobCircularEN.json", "Recruitment/CreateJobCircularBN.json", Request.Cookies["lang"]),
                jobCirculars = await applicationFormService.GetCreateJobCircular("pending")
            };
            return View(model);
        }

        // POST: CreateJobCircular/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] CreateJobCircularViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.jobCirculars = await applicationFormService.GetCreateJobCircular("pending");
                return View(model);
            }
            
            JobCircular data = new JobCircular
            {
                reference = model.circularRefernce,
                recruitmentType = model.recruitmentType,
                positionName = model.positionName,
                project = model.project,
                noOfPosition = Int32.Parse(model.numberOfPosition),
                jobType = model.jobType,
                location = model.locationofJob,
                comments = model.comments,
                applicationDeadLine = Convert.ToDateTime(model.applicationDeadline),
                lastDateOfWrittenExam = Convert.ToDateTime(model.writtenExamLastDate),
                applicationFee = model.applicationFee,
                educationalQualification = model.educationQualification,
                mainSubject = model.educationMainSubject,
                otherQualification = model.otherQualification,
                leastExperience = model.leastExpperiance,
                maxAge = model.maximumAge,
                maxAgeQuota = model.maximumAgeQuota,
                status = "pending"

            };

            await applicationFormService.SaveCreateJobCircular(data);

            return RedirectToAction(nameof(Index));
        }

    }
}