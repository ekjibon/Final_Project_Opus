using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSDisciplineInvestigation.Models;
using OPUSERP.Areas.HRPMSDisciplineInvestigation.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.DisciplineInvestigation;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.DisciplineInvestigation.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.HRPMSDisciplineInvestigation.Controllers
{
    [Authorize]
    [Area("HRPMSDisciplineInvestigation")]
    public class DisciplinaryActionController : Controller
    {
        private readonly LangGenerate<DisciplinaryLn> _lang;
        private readonly IDisciplineInvestigation disciplineInvestigation;

        public DisciplinaryActionController(IHostingEnvironment hostingEnvironment, IDisciplineInvestigation disciplineInvestigation)
        {
            _lang = new LangGenerate<DisciplinaryLn>(hostingEnvironment.ContentRootPath);
            this.disciplineInvestigation = disciplineInvestigation;
        }
        public async Task<ActionResult> Index()
        {
            DisciplinaryActionViewModel model = new DisciplinaryActionViewModel
            {
                fLang = _lang.PerseLang("Disciplinary/DisciplinaryEN.json", "Disciplinary/DisciplinaryBN.json", Request.Cookies["lang"]),
                offenses=await disciplineInvestigation.GetAllOffense(),
                punishments=await disciplineInvestigation.GetAllPunishment(),
                disciplinaries=await disciplineInvestigation.GetAllDisciplinary()
            };
            return View(model);
        }

        // POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] DisciplinaryActionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("Disciplinary/DisciplinaryEN.json", "Disciplinary/DisciplinaryBN.json", Request.Cookies["lang"]);
                model.offenses = await disciplineInvestigation.GetAllOffense();
                model.punishments=await disciplineInvestigation.GetAllPunishment();
                model.disciplinaries = await disciplineInvestigation.GetAllDisciplinary();
                return View(model);
            }

            DisciplinaryAction data = new DisciplinaryAction
            {
                Id = Convert.ToInt32(model.disciplinaryId),
                employeeId = model.employeeId,
                OffenseId = model.offenseId,
                naturalPunishmentId=Convert.ToInt32(model.punishmentId),
                punishmentDate=Convert.ToDateTime(model.punishmentDate),
                startingDate=Convert.ToDateTime(model.startFrom),
                endDate=Convert.ToDateTime(model.endTo),
                goNumberWithDate=model.goNo,
                remarks=model.remarks,
                status="pending"
            };

            await disciplineInvestigation.SaveDisciplinary(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ApplicationApprovalList()
        {
            DisciplinaryActionViewModel model = new DisciplinaryActionViewModel
            {
                fLang = _lang.PerseLang("Disciplinary/DisciplinaryEN.json", "Disciplinary/DisciplinaryBN.json", Request.Cookies["lang"]),
                offenses = await disciplineInvestigation.GetAllOffense(),
                punishments = await disciplineInvestigation.GetAllPunishment(),
                disciplinaries = await disciplineInvestigation.GetAllDisciplinary()
            };
            return View(model);
        }

        public IActionResult Details()
        {
            DisciplinaryActionViewModel model = new DisciplinaryActionViewModel
            {
                fLang = _lang.PerseLang("Disciplinary/DisciplinaryEN.json", "Disciplinary/DisciplinaryBN.json", Request.Cookies["lang"])
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Action([FromForm] int id, string type)
        {
            await disciplineInvestigation.UpdateDisciplinaryStatus(id, "approved");
            

            return RedirectToAction(nameof(ApplicationApprovalList));

        }

        #region API Section
        [Route("global/api/disciplinary/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            return Json(await disciplineInvestigation.DeleteDisciplinaryById(Id));
        }
        #endregion
    }
}