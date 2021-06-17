using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSMasterData.Models;
using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Data.Entity.Master;

namespace OPUSERP.Areas.HRPMSMasterData.Controllers
{
    [Authorize]
    [Area("HRPMSMasterData")]
    public class DivisionController : Controller
    {
        private readonly LangGenerate<DivisionLn> _lang;
        private readonly IAddressService addressService;

        public DivisionController(IHostingEnvironment hostingEnvironment, IAddressService addressService)
        {
            _lang = new LangGenerate<DivisionLn>(hostingEnvironment.ContentRootPath);
            this.addressService = addressService;
        }
        // GET: Division
        public async Task<IActionResult> Index()
        {
            DivisionViewModel model = new DivisionViewModel
            {
                fLang = _lang.PerseLang("MasterData/DivisionEN.json", "MasterData/DivisionBN.json", Request.Cookies["lang"]),
                divisions = await addressService.GetAllDivision(),
                countries=await addressService.GetAllContry()
            };
            return View(model);
        }

        // POST: Division/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] DivisionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/DivisionEN.json", "MasterData/DivisionBN.json", Request.Cookies["lang"]);
                model.divisions = await addressService.GetAllDivision();
                model.countries = await addressService.GetAllContry();
                return View(model);
            }


            Division division = new Division
            {
                Id = model.divisionId,
                divisionCode = model.divisionCode,
                divisionName = model.divisionName,
                divisionNameBn = model.divisionNameBn,
                shortName = model.shortName,
                countryId=model.countryId
            };

            await addressService.SaveDivision(division);

            return RedirectToAction(nameof(Index));

        }

        // GET: Division/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Division/Edit/5
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

        // GET: Division/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Division/Delete/5
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

        #region API Section
        //[Route("global/api/divisions/{id}")]
        //[HttpGet]
        //public async Task<IActionResult> Divisions(int Id)
        //{
        //    return Json(await addressService.GetDivisionsByCountryId(Id));
        //}
        #endregion

    }
}