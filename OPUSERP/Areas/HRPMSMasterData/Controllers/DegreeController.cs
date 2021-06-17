using OPUSERP.Areas.HRPMSMasterData.Models;
using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
namespace OPUSERP.Areas.HRPMSMasterData.Controllers
{
    [Authorize]
    [Area("HRPMSMasterData")]
    public class DegreeController : Controller
    {
        private readonly LangGenerate<DegreeInfoLn> _lang;
        private readonly IDegreeService degreeService;

        private readonly ILevelofEducationService levelofEducationService;

        public DegreeController(IHostingEnvironment hostingEnvironment, IDegreeService degreeService, ILevelofEducationService levelofEducationService)
        {
            _lang = new LangGenerate<DegreeInfoLn>(hostingEnvironment.ContentRootPath);
            this.degreeService = degreeService;
            this.levelofEducationService = levelofEducationService;
        }

        // GET: Degree
        public async Task<IActionResult> Index()
        {
            DegreeViewModel model = new DegreeViewModel
            {
                fLang = _lang.PerseLang("MasterData/DegreeEN.json", "MasterData/DegreeBN.json", Request.Cookies["lang"]),
                degrees = await degreeService.GetAllDegree(),
                levelofeducationNamelist = await levelofEducationService.GetAllLevelofEducation()
            };


            return View(model);
        }

        // POST: Degree/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] DegreeViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/DegreeEN.json", "MasterData/DegreeBN.json", Request.Cookies["lang"]);
                model.degrees = await degreeService.GetAllDegree();
                model.levelofeducationNamelist = await levelofEducationService.GetAllLevelofEducation();
                return View(model);
            }

            Degree data = new Degree
            {
                Id = Int32.Parse(model.degreeId),
                degreeName = model.degreeName,
                degreeNameBn = model.degreeNameBn,
                degreeShortName = model.degreeShortName,
                levelofeducationId=model.levelofeducationId
            };

            await degreeService.SaveDegree(data);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteDegreeById(int Id)
        {
            await degreeService.DeleteDegreeById(Id);
            return Json(true);
        }

        #region API Section
        [Route("global/api/degrees/{id}")]
        [HttpGet]
        public async Task<IActionResult> Degrees(int Id)
        {
            return Json(await degreeService.GetDegreeByLOEId(Id));
        }
        #endregion
    }
}