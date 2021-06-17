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
    public class RelDegreeSubjectController : Controller
    {
        private readonly LangGenerate<DegSubRelationInfoLn> _lang;
        private readonly IRelDegreeSubjectService degreeSubjectService;
        private readonly IDegreeService degreeService;
        private readonly ISubjectService subjectService;

        public RelDegreeSubjectController(IHostingEnvironment hostingEnvironment, IRelDegreeSubjectService degreeSubjectService, IDegreeService degreeService, ISubjectService subjectService)
        {
            _lang = new LangGenerate<DegSubRelationInfoLn>(hostingEnvironment.ContentRootPath);
            this.degreeSubjectService = degreeSubjectService;
            this.degreeService = degreeService;
            this.subjectService = subjectService;
        }
        // GET: RelDegreeSubject
        public async Task<IActionResult> Index()
        {
            RelDegreeSubjectViewModel model = new RelDegreeSubjectViewModel
            {
                fLang = _lang.PerseLang("MasterData/DegreeSubjectEN.json", "MasterData/DegreeSubjectBN.json", Request.Cookies["lang"]),
                relDegreeSubjectslist = await degreeSubjectService.GetAllDegreeSubject(),
                degreeslist = await degreeService.GetAllDegree(),
                subjectslist = await subjectService.GetAllSubject()
            };
            return View(model);
        }

        // POST: RelDegreeSubject/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] RelDegreeSubjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/DegreeSubjectEN.json", "MasterData/DegreeSubjectBN.json", Request.Cookies["lang"]);
                model.relDegreeSubjectslist = await degreeSubjectService.GetAllDegreeSubject();
                model.degreeslist = await degreeService.GetAllDegree();
                model.subjectslist = await subjectService.GetAllSubject();
                return View(model);
            }

            RelDegreeSubject data = new RelDegreeSubject
            {
                Id= Int32.Parse(model.relDegSubId),
                degreeId = model.degreeId,
                subjectId = model.subjectId
            };

            await degreeSubjectService.SaveDegreeSubject(data);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteDegreeSubjectById(int Id)
        {
            await degreeSubjectService.DeleteDegreeSubjectById(Id);
            return Json(true);
        }

        #region API Section
        [Route("global/api/relDegreeSubjects/{id}")]
        [HttpGet]
        public async Task<IActionResult> RelDegreeSubjects(int Id)
        {
            return Json(await degreeSubjectService.GetSubjectByDegreeId(Id));
        }
        #endregion
    }
}