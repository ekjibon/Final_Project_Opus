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
    public class DistrictController : Controller
    {
        private readonly LangGenerate<DistrictLn> _lang;
        private readonly IAddressService addressService;

        public DistrictController(IHostingEnvironment hostingEnvironment, IAddressService addressService)
        {
            _lang = new LangGenerate<DistrictLn>(hostingEnvironment.ContentRootPath);
            this.addressService = addressService;
        }
        // GET: District
        public async Task<IActionResult> Index()
        {
            DistrictViewModel model = new DistrictViewModel
            {
                fLang = _lang.PerseLang("MasterData/DistrictEN.json", "MasterData/DistrictBN.json", Request.Cookies["lang"]),
                districts =await addressService.GetAllDistrict(),
                divisions = await addressService.GetAllDivision(),
                countries = await addressService.GetAllContry()
            };
            return View(model);
        }

        // POST: District/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] DistrictViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/DistrictEN.json", "MasterData/DistrictBN.json", Request.Cookies["lang"]);
                model.districts = await addressService.GetAllDistrict();
                model.divisions = await addressService.GetAllDivision();
                model.countries = await addressService.GetAllContry();
                return View(model);
            }

            District district = new District
            {
                Id = model.districtId,
                districtCode = model.districtCode,
                districtName = model.districtName,
                districtNameBn = model.districtNameBn,
                shortName = model.shortName,
                divisionId = model.divisionId
            };

            await addressService.SaveDistrict(district);

            return RedirectToAction(nameof(Index));

        }
        
        #region API Section
        //[Route("global/api/districts/{id}")]
        //[HttpGet]
        //public async Task<IActionResult> Districts(int Id)
        //{
        //    return Json(await addressService.GetDistrictsByDivisonId(Id));
        //}
        #endregion
    }
}