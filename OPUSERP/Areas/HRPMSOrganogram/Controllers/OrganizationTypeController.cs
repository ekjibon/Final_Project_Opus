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
    public class OrganizationTypeController : Controller
    {
        private readonly LangGenerate<OrganizationTypeLn> _lang;
        private readonly IOrganizationTypeService organizationTypeService;

        public OrganizationTypeController(IHostingEnvironment hostingEnvironment, IOrganizationTypeService organizationTypeService)
        {
            _lang = new LangGenerate<OrganizationTypeLn>(hostingEnvironment.ContentRootPath);
            this.organizationTypeService = organizationTypeService;
        }

        // GET: Organization
        public async Task<IActionResult> Index()
        {
            OrganizationTypeViewModel model = new OrganizationTypeViewModel
            {
                fLang = _lang.PerseLang("Organogram/OrganizationTypeEN.json", "Organogram/OrganizationTypeBN.json", Request.Cookies["lang"]),
                organizationTypes = await organizationTypeService.GetAllOrganizationType()
            };
            return View(model);
        }

        // POST: OrganizationType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] OrganizationTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("Organogram/OrganizationTypeEN.json", "Organogram/OrganizationTypeBN.json", Request.Cookies["lang"]);
                model.organizationTypes = await organizationTypeService.GetAllOrganizationType();
                return View(model);
            }

            OrganizationType data = new OrganizationType
            {
                Id = model.organizationTypeId,
                nameEN = model.organizationTypeName,
                nameBN = model.organizationTypeNameBN,
                remarks = model.remarks,
            };

            await organizationTypeService.SaveOrganizationType(data);

            return RedirectToAction(nameof(Index));
        }

        // GET: OrganizationType/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrganizationType/Edit/5
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

        // GET: OrganizationType/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrganizationType/Delete/5
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