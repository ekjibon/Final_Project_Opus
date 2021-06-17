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
    public class NaturalPunishmentController : Controller
    {
        private readonly LangGenerate<PunishmentLn> _lang;
        private readonly IDisciplineInvestigation disciplineInvestigation;

        public NaturalPunishmentController(IHostingEnvironment hostingEnvironment, IDisciplineInvestigation disciplineInvestigation)
        {
            _lang = new LangGenerate<PunishmentLn>(hostingEnvironment.ContentRootPath);
            this.disciplineInvestigation = disciplineInvestigation;
        }

        // GET: OffenseSetup
        public async Task<ActionResult> Index()
        {
            PunishmentViewModel model = new PunishmentViewModel
            {
                fLang = _lang.PerseLang("Disciplinary/PunishmentEN.json", "Disciplinary/PunishmentBN.json", Request.Cookies["lang"]),
                punishments = await disciplineInvestigation.GetAllPunishment()
            };
            return View(model);
        }

        // POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] PunishmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("Disciplinary/PunishmentEN.json", "Disciplinary/PunishmentBN.json", Request.Cookies["lang"]);
                model.punishments = await disciplineInvestigation.GetAllPunishment();
                return View(model);
            }

            NaturalPunishment data = new NaturalPunishment
            {
                Id = Convert.ToInt32(model.punishmentID),
                name = model.punishment,
                description = model.description
            };

            await disciplineInvestigation.SavePunishment(data);

            return RedirectToAction(nameof(Index));
        }

        #region API Section
        [Route("global/api/naturalpunishment/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            return Json(await disciplineInvestigation.DeletePunishmentById(Id));
        }
        #endregion
    }
}