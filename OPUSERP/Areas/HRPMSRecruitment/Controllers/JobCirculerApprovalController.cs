using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSRecruitment.Models;
using OPUSERP.Areas.HRPMSRecruitment.Models.Lang;
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
    public class JobCirculerApprovalController : Controller
    {
        private readonly LangGenerate<RecruitmentLn> _lang;
        private readonly IApplicationFormService applicationFormService;

        public JobCirculerApprovalController(IHostingEnvironment hostingEnvironment, IApplicationFormService applicationFormService)
        {
            _lang = new LangGenerate<RecruitmentLn>(hostingEnvironment.ContentRootPath);
            this.applicationFormService = applicationFormService;
        }

        // GET: CreateJobCircular
        public async Task<IActionResult> Index()
        {
            CreateJobCircularViewModel model = new CreateJobCircularViewModel
            {
                fLang = _lang.PerseLang("Recruitment/JobCircularApprovalEN.json", "Recruitment/JobCircularApprovalBN.json", Request.Cookies["lang"]),
                jobCirculars = await applicationFormService.GetCreateJobCircular("pending")
            };
            return View(model);
        }

        //// GET: CallForExam
        //public ActionResult ViewJob()
        //{
        //    return View();
        //}

        // GET: View Job
        public async Task<IActionResult> ViewJob(int id)
        {
            CreateJobCircularViewModel model = new CreateJobCircularViewModel
            {
                jobCircular = await applicationFormService.GetCreateJobCircularById(id)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Action([FromForm] int id, string type)
        {
            if (type == "approve") await applicationFormService.UpdateJobCircular(id, "approved");
            else if (type == "reject") await applicationFormService.UpdateJobCircular(id, "rejected");

            return RedirectToAction(nameof(Index));

        }

    }
}