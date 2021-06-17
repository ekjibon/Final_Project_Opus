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
    public class LevelOfEducationController : Controller
    {
        private readonly LangGenerate<LevelOfEducationLn> _lang;
        private readonly ILevelofEducationService levelofEducationService;

        public LevelOfEducationController(IHostingEnvironment hostingEnvironment, ILevelofEducationService levelofEducationService)
        {
            _lang = new LangGenerate<LevelOfEducationLn>(hostingEnvironment.ContentRootPath);
            this.levelofEducationService = levelofEducationService;
        }
        // GET: LevelofEducation
        public async Task<IActionResult> Index()
        {
            LevelofEducationViewModel model = new LevelofEducationViewModel
            {
                fLang = _lang.PerseLang("MasterData/LevelofEducationEN.json", "MasterData/LevelofEducationBN.json", Request.Cookies["lang"]),
                levelofEducations = await levelofEducationService.GetAllLevelofEducation()
            };
            return View(model);
        }

        // POST: LevelofEducation/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] LevelofEducationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/LevelofEducationEN.json", "MasterData/LevelofEducationBN.json", Request.Cookies["lang"]);
                model.levelofEducations = await levelofEducationService.GetAllLevelofEducation();
                return View(model);
            }

            LevelofEducation data = new LevelofEducation
            {
                Id= model.loeId,
                levelofeducationName = model.levelofeducationName,
                levelofeducationNameBn = model.levelofeducationNameBn,
            };

            await levelofEducationService.SaveLevelofEducation(data);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteLevelofEducationById(int Id)
        {
            await levelofEducationService.DeleteLevelofEducationById(Id);
            return Json(true);
        }

        #region API Section
        [Route("global/api/levelOfEducations")]
        [HttpGet]
        public async Task<IActionResult> LevelOfEducations()
        {
            return Json(await levelofEducationService.GetAllLevelofEducation());
        }
        #endregion
    }
}