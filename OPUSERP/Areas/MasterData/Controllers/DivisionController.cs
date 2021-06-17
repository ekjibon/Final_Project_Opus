using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//using UMBRELLA.Areas.MasterData.Models.Lang;

//using UMBRELLA.Helpers;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.Data.Entity.Master;

namespace OPUSERP.Areas.MasterData.Controllers
{
    [Area("MasterData")]
    public class DivisionController : Controller
    {
        //private readonly LangGenerate<DivisionLn> _lang;
        private readonly IAddressService addressService;

        public DivisionController(IAddressService addressService)
        {
            //_lang = new LangGenerate<DivisionLn>(hostingEnvironment.ContentRootPath);
            this.addressService = addressService;
        }
        // GET: Division
        public async Task<IActionResult> Index()
        {
            DivisionViewModel model = new DivisionViewModel
            {
                //fLang = _lang.PerseLang("MasterData/DivisionEN.json", "MasterData/DivisionBN.json", Request.Cookies["lang"]),
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
                //model.fLang = _lang.PerseLang("MasterData/DivisionEN.json", "MasterData/DivisionBN.json", Request.Cookies["lang"]);
                model.divisions = await addressService.GetAllDivision();
                model.countries = await addressService.GetAllContry();
                return View(model);
            }


            Division division = new Division
            {
                Id = model.divisionId,
                divisionCode = model.divisionCode,
                divisionName = model.divisionName,
                shortName = model.shortName,
                countryId = model.countryId
            };

            await addressService.SaveDivision(division);

            return RedirectToAction(nameof(Index));

        }


        #region API Section
        [Route("global/api/divisions/{id}")]
        [HttpGet]
        public async Task<IActionResult> Divisions(int Id)
        {
            return Json(await addressService.GetDivisionsByCountryId(Id));
        }
        #endregion

    }
}