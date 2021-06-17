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
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.HRPMSMasterData.Controllers
{
    [Authorize]
    [Area("HRPMSMasterData")]
    public class CadreController : Controller
    {
        private readonly LangGenerate<CadreInfoLn> _lang;
        private readonly IOccupationCadreService occupationCadreService;

        public CadreController(IHostingEnvironment hostingEnvironment, IOccupationCadreService occupationCadreService)
        {
            _lang = new LangGenerate<CadreInfoLn>(hostingEnvironment.ContentRootPath);
            this.occupationCadreService = occupationCadreService;
        }
        // GET: Occupation
        public async Task<IActionResult> Index()
        {
            CadreViewModel model = new CadreViewModel
            {
                fLang = _lang.PerseLang("MasterData/CadreEN.json", "MasterData/CadreBN.json", Request.Cookies["lang"]),
                cadres = await occupationCadreService.GetCadreInfo()
            };
            return View(model);
        }

        // POST: Occupation/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] CadreViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/CadreEN.json", "MasterData/CadreBN.json", Request.Cookies["lang"]);
                model.cadres = await occupationCadreService.GetCadreInfo();
                return View(model);
            }

            Cadre data = new Cadre
            {
                Id = Int32.Parse(model.cadreId),
                cadreName = model.cadreName,
                cadreNameBn = model.cadreNameBn,
                cadreShortName = model.cadreShortName
            };

            await occupationCadreService.SaveCadreInfo(data);

            return RedirectToAction(nameof(Index));
        }
    }
}