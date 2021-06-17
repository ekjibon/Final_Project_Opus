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
    public class AwardController : Controller
    {
        private readonly LangGenerate<AwardLn> _lang;
        private readonly IBookAwardService bookAwardService;

        public AwardController(IHostingEnvironment hostingEnvironment, IBookAwardService bookAwardService)
        {
            _lang = new LangGenerate<AwardLn>(hostingEnvironment.ContentRootPath);
            this.bookAwardService = bookAwardService;
        }
        // GET: Award
        public async Task<IActionResult> Index()
        {
            AwardViewModel model = new AwardViewModel
            {
                fLang = _lang.PerseLang("MasterData/AwardEN.json", "MasterData/AwardBN.json", Request.Cookies["lang"]),
                awards = await bookAwardService.GetAwardInfo()
            };
            return View(model);
        }

        // POST: Award/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] AwardViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/AwardEN.json", "MasterData/AwardBN.json", Request.Cookies["lang"]);
                model.awards = await bookAwardService.GetAwardInfo();
                return View(model);
            }

            Award data = new Award
            {
                Id = Int32.Parse(model.awardId),
                awardName = model.awardName,
                awardNameBn = model.awardNameBn,
                awardShortName = model.awardShortName
            };

            await bookAwardService.SaveAwardInfo(data);

            return RedirectToAction(nameof(Index));
        }
    }
}