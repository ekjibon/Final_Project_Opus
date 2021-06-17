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
    public class ThanaController : Controller
    {
        private readonly LangGenerate<ThanaInfoLn> _lang;
        private readonly IAddressService addressService;

        public ThanaController(IHostingEnvironment hostingEnvironment, IAddressService addressService)
        {
            _lang = new LangGenerate<ThanaInfoLn>(hostingEnvironment.ContentRootPath);
            this.addressService = addressService;
        }
        // GET: Thana
        public async Task<IActionResult> Index()
        {
            ThanaViewModel model = new ThanaViewModel
            {
                fLang = _lang.PerseLang("MasterData/ThanaEN.json", "MasterData/ThanaBN.json", Request.Cookies["lang"]),
                countries = await addressService.GetAllContry(),
                thanas=await addressService.GetAllThana(),
            };
            return View(model);
        }

        // POST: Thana/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ThanaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/ThanaEN.json", "MasterData/ThanaBN.json", Request.Cookies["lang"]);
                model.countries = await addressService.GetAllContry();
                model.thanas = await addressService.GetAllThana();
                return View(model);
            }

            Thana thana = new Thana
            {
                Id = model.thanaId,
                thanaCode = model.thanaCode,
                thanaName = model.thanaName,
                thanaNameBn = model.thanaNameBn,
                shortName = model.shortName,
                districtId = model.districtId
            };

            await addressService.SaveThana(thana);

            return RedirectToAction(nameof(Index));

        }

        // GET: Thana/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        #region API Section
        //[Route("global/api/thanas/{id}")]
        //[HttpGet]
        //public async Task<IActionResult> Thanas(int Id)
        //{
        //    return Json(await addressService.GetThanasByDistrictId(Id));
        //}
        #endregion
    }
}