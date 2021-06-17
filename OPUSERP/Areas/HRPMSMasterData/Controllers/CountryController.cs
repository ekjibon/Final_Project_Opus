using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSMasterData.Models;
using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using System;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSMasterData.Controllers
{
    [Authorize]
    [Area("HRPMSMasterData")]
    public class CountryController : Controller
    {
        private readonly LangGenerate<CountryLn> _lang;
        private readonly IAddressService addressService;

        public CountryController(IHostingEnvironment hostingEnvironment, IAddressService addressService)
        {
            _lang = new LangGenerate<CountryLn>(hostingEnvironment.ContentRootPath);
            this.addressService = addressService;
        }
        // GET: Country
        public async Task<IActionResult> Index()
        {
            CountryViewModel model = new CountryViewModel
            {
                fLang = _lang.PerseLang("MasterData/CountryEN.json", "MasterData/CountryBN.json", Request.Cookies["lang"]),
                countries = await addressService.GetAllContry()
            };
            return View(model);
        }

        // POST: Country/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] CountryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/CountryEN.json", "MasterData/CountryBN.json", Request.Cookies["lang"]);
                model.countries = await addressService.GetAllContry();
                return View(model);
            }

            Country country = new Country
            {
                Id = Int32.Parse(model.countryId),
                countryCode = model.countryCode,
                shortName = model.shortName,
                countryName = model.countryName,
                countryNameBn = model.countryNameBn
            };

            await addressService.SaveCountry(country);

            return RedirectToAction(nameof(Index));

        }
        

        #region API Section
        [Route("global/api/countries")]
        [HttpGet]
        public async Task<IActionResult> Countries()
        {
            return Json(await addressService.GetAllContry());
        }
        #endregion
    }
}