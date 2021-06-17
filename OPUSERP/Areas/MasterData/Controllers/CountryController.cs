
//using UMBRELLA.Areas.MasterData.Models.Lang;

//using UMBRELLA.Helpers;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.ERPServices.MasterData.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace OPUSERP.Areas.MasterData.Controllers
{
    [Area("MasterData")]
    public class CountryController : Controller
    {
        //private readonly LangGenerate<CountryLn> _lang;
        private readonly IAddressService addressService;
        private readonly IUserTypeService userTypeService;

        public CountryController(IAddressService addressService,IUserTypeService userTypeService)
        {
            //_lang = new LangGenerate<CountryLn>(hostingEnvironment.ContentRootPath);
            this.addressService = addressService;
            this.userTypeService = userTypeService;
        }

        // GET: Country
        public async Task<IActionResult> Index()
        {
            var country = await addressService.GetAllContry();
            int count = country.Count();
            var autonumber = await userTypeService.GetAutonumberingInfoById("Country");
            var total = 0;
            string code = "";
            if (autonumber.NumType == 1)
            {
                total = count + (int)autonumber.defaultValue;
                code = userTypeService.GetNumberCode(total.ToString(), (int)autonumber.NumValue);
            }
            else
            {
                total = count + (int)autonumber.startValue;
                code = userTypeService.GetNumberCode(total.ToString(), (int)autonumber.NumValue);
                code = autonumber.prefix + code;
            }
            CountryViewModel model = new CountryViewModel
            {
                countryCode = code,
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
                //model.fLang = _lang.PerseLang("MasterData/CountryEN.json", "MasterData/CountryBN.json", Request.Cookies["lang"]);
                model.countries = await addressService.GetAllContry();
                return View(model);
            }

            Country country = new Country
            {
                Id = Int32.Parse(model.countryId),
                countryCode = model.countryCode,
                countryName = model.countryName,
                shortName = model.shortName,
                
                //countryNameBn = model.countryNameBn
            };

            await addressService.SaveCountry(country);

            return RedirectToAction(nameof(Index));

        }

        // GET: Country
        public async Task<IActionResult> AutoCodeGenerate()
        {
            AutoCodeGenerateViewModel model = new AutoCodeGenerateViewModel
            {
                autonumberingInfos = await userTypeService.GetAllAutonumberingInfo()
            };
            return View(model);
        }

        // POST: Country/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AutoCodeGenerate([FromForm] AutoCodeGenerateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.autonumberingInfos = await userTypeService.GetAllAutonumberingInfo();
                return View(model);
            }

            AutonumberingInfo country = new AutonumberingInfo
            {
                Id = model.Id,
                fieldName = model.fieldName,
                NumType = model.NumType,
                defaultValue = model.defaultValue,
                prefix= model.prefix,
                startValue = model.startValue,
                NumValue = model.NumValue,
                isdate=model.isdate,
                isyear=model.isyear,
                ismonth=model.ismonth,
                separator=model.separator,
                mseparator=model.mseparator,
                yseparator=model.yseparator,
                dseparator=model.dseparator

            };

            await userTypeService.SaveAutonumberingInfo(country);

            return RedirectToAction(nameof(AutoCodeGenerate));

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