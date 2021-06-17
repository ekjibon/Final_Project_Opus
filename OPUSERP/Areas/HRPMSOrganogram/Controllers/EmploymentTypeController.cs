using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSOrganogram.Models;
using OPUSERP.Areas.HRPMSOrganogram.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Organogram;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Organogram.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.HRPMSOrganogram.Controllers
{
    [Authorize]
    [Area("HRPMSOrganogram")]
    public class EmploymentTypeController : Controller
    {
        private readonly LangGenerate<EmploymentTypeLn> _lang;
        private readonly IPostingEmploymentTypeService postingEmploymentTypeService;

        public EmploymentTypeController(IHostingEnvironment hostingEnvironment, IPostingEmploymentTypeService postingEmploymentTypeService)
        {
            _lang = new LangGenerate<EmploymentTypeLn>(hostingEnvironment.ContentRootPath);
            this.postingEmploymentTypeService = postingEmploymentTypeService;
        }

        // GET: Organization
        public async Task<IActionResult> Index()
        {
            EmploymentTypeViewModel model = new EmploymentTypeViewModel
            {
                fLang = _lang.PerseLang("Organogram/EmploymentTypeEN.json", "Organogram/EmploymentTypeBN.json", Request.Cookies["lang"]),
                employmentTypes = await postingEmploymentTypeService.GetAllEmploymentType()
            };
            return View(model);
        }

        // POST: OrganizationType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] EmploymentTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("Organogram/EmploymentTypeEN.json", "Organogram/EmploymentTypeBN.json", Request.Cookies["lang"]);
                model.employmentTypes = await postingEmploymentTypeService.GetAllEmploymentType();
                return View(model);
            }

            EmploymentType data = new EmploymentType
            {
                Id = model.id,
                nameEN = model.nameEN,
                nameBN = model.nameBN,
                remarks = model.remarks,
            };

            await postingEmploymentTypeService.SaveEmploymentType(data);

            return RedirectToAction(nameof(Index));
        }

        // GET: EmploymentType/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmploymentType/Edit/5
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

        // GET: EmploymentType/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmploymentType/Delete/5
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