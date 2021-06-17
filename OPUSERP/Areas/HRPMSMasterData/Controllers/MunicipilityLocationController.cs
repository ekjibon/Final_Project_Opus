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

namespace OPUSERP.Areas.HRPMSMasterData.Controllers
{
    [Authorize]
    [Area("HRPMSMasterData")]
    public class MunicipilityLocationController : Controller
    {
        private readonly LangGenerate<MunicipalityInfoLn> _lang;
        // GET: MunicipilityLocation
        private readonly IReligionMunicipalityService religionMunicipalityService;

        public MunicipilityLocationController(IHostingEnvironment hostingEnvironment, IReligionMunicipalityService religionMunicipalityService)
        {
            _lang = new LangGenerate<MunicipalityInfoLn>(hostingEnvironment.ContentRootPath);
            this.religionMunicipalityService = religionMunicipalityService;
        }

        // GET: MunicipilityLocation
        public async Task<IActionResult> Index()
        {
            MunicipilityLocationViewModel model = new MunicipilityLocationViewModel
            {
                fLang = _lang.PerseLang("MasterData/MunicipilityEN.json", "MasterData/MunicipilityBN.json", Request.Cookies["lang"]),
                municipilityLocations = await religionMunicipalityService.GetMunicipilityLocation()
            };
            return View(model);
        }

        // POST: MunicipilityLocation/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] MunicipilityLocationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/MunicipilityEN.json", "MasterData/MunicipilityBN.json", Request.Cookies["lang"]);
                model.municipilityLocations = await religionMunicipalityService.GetMunicipilityLocation();
                return View(model);
            }

            MunicipilityLocation data = new MunicipilityLocation
            {
                Id = model.municipalityId,
                locationName = model.locationName,
                locationNameBn = model.locationNameBn,
                shortName = model.shortName
            };

            await religionMunicipalityService.SaveMunicipilityLocation(data);

            return RedirectToAction(nameof(Index));
        }
    }
}