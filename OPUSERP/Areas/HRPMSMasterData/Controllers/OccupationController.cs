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
    public class OccupationController : Controller
    {
        private readonly LangGenerate<OccupationLn> _lang;
        private readonly IOccupationCadreService occupationCadreService;

        public OccupationController(IHostingEnvironment hostingEnvironment, IOccupationCadreService occupationCadreService)
        {
            _lang = new LangGenerate<OccupationLn>(hostingEnvironment.ContentRootPath);
            this.occupationCadreService = occupationCadreService;
        }
        // GET: Occupation
        public async Task<IActionResult> Index()
        {
            OccupationViewModel model = new OccupationViewModel
            {
                fLang = _lang.PerseLang("MasterData/OccupationEN.json", "MasterData/OccupationBN.json", Request.Cookies["lang"]),
                occupations = await occupationCadreService.GetOccupationInfo()
            };
            return View(model);
        }

        // POST: Occupation/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] OccupationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/OccupationEN.json", "MasterData/OccupationBN.json", Request.Cookies["lang"]);
                model.occupations = await occupationCadreService.GetOccupationInfo();
                return View(model);
            }

            Occupation data = new Occupation
            {
                Id = model.occupationId,
                occupationName = model.occupationName,
                occupationNameBn = model.occupationNameBn,
                occupationShortName = model.occupationShortName
            };

            await occupationCadreService.SaveOccupationInfo(data);

            return RedirectToAction(nameof(Index));
        }
    }
}