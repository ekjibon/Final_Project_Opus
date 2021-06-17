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
    public class ApprovedJobCircularController : Controller
    {
        private readonly LangGenerate<RecruitmentLn> _lang;
        private readonly IApplicationFormService applicationFormService;

        public ApprovedJobCircularController(IHostingEnvironment hostingEnvironment, IApplicationFormService applicationFormService)
        {
            _lang = new LangGenerate<RecruitmentLn>(hostingEnvironment.ContentRootPath);
            this.applicationFormService = applicationFormService;
        }

        // GET: CreateJobCircular
        public async Task<IActionResult> Index()
        {
            CreateJobCircularViewModel model = new CreateJobCircularViewModel
            {
                fLang = _lang.PerseLang("Recruitment/ApprovedJobCircularEN.json", "Recruitment/ApprovedJobCircularBN.json", Request.Cookies["lang"]),
                jobCirculars = await applicationFormService.GetCreateJobCircular("Approved")
            };
            return View(model);
        }

        // GET: ApprovedJobCircular/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ApprovedJobCircular/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApprovedJobCircular/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
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

        // GET: ApprovedJobCircular/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ApprovedJobCircular/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ApprovedJobCircular/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ApprovedJobCircular/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}