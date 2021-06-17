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
    public class PostingTypeController : Controller
    {
        private readonly LangGenerate<PostingTypeLn> _lang;
        private readonly IPostingEmploymentTypeService postingEmploymentTypeService;

        public PostingTypeController(IHostingEnvironment hostingEnvironment, IPostingEmploymentTypeService postingEmploymentTypeService)
        {
            _lang = new LangGenerate<PostingTypeLn>(hostingEnvironment.ContentRootPath);
            this.postingEmploymentTypeService = postingEmploymentTypeService;
        }

        // GET: Organization
        public async Task<IActionResult> Index()
        {
            PostingTypeViewModel model = new PostingTypeViewModel
            {
                fLang = _lang.PerseLang("Organogram/PostingTypeEN.json", "Organogram/PostingTypeBN.json", Request.Cookies["lang"]),
                postingTypes = await postingEmploymentTypeService.GetAllPostingType()
            };
            return View(model);
        }

        // POST: OrganizationType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] PostingTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("Organogram/PostingTypeEN.json", "Organogram/PostingTypeBN.json", Request.Cookies["lang"]);
                model.postingTypes = await postingEmploymentTypeService.GetAllPostingType();
                return View(model);
            }

            PostingType data = new PostingType
            {
                Id = model.id,
                nameEN = model.nameEN,
                nameBN = model.nameBN,
                remarks = model.remarks,
            };

            await postingEmploymentTypeService.SavePostingType(data);

            return RedirectToAction(nameof(Index));
        }

        // GET: PostingType/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostingType/Edit/5
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

        // GET: PostingType/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostingType/Delete/5
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