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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.HRPMSDisciplineInvestigation.Controllers
{
    [Authorize]
    [Area("HRPMSDisciplineInvestigation")]
    public class OffenseSetupController : Controller
    {
        private readonly LangGenerate<OffenseLn> _lang;
        private readonly IDisciplineInvestigation disciplineInvestigation;
        public OffenseSetupController(IHostingEnvironment hostingEnvironment, IDisciplineInvestigation disciplineInvestigation)
        {
            _lang = new LangGenerate<OffenseLn>(hostingEnvironment.ContentRootPath);
            this.disciplineInvestigation = disciplineInvestigation;
        }

        // GET: OffenseSetup
        public async Task<ActionResult> Index()
        {
            OffenseViewModel model = new OffenseViewModel
            {
                fLang = _lang.PerseLang("Disciplinary/OffenseEN.json", "Disciplinary/OffenseBN.json", Request.Cookies["lang"]),
                offenses = await disciplineInvestigation.GetAllOffense()
            };
            return View(model);
        }

        // POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] OffenseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("Disciplinary/OffenseEN.json", "Disciplinary/OffenseBN.json", Request.Cookies["lang"]);
                model.offenses = await disciplineInvestigation.GetAllOffense();
                return View(model);
            }

            Offense data = new Offense
            {
                Id=Convert.ToInt32(model.offenseID),
                name = model.offense,
                description = model.description
            };

            await disciplineInvestigation.SaveOffense(data);

            return RedirectToAction(nameof(Index));
        }

        #region API Section
        [Route("global/api/offense/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteOffense(int Id)
        {
            return Json(await disciplineInvestigation.DeleteOffenseById(Id));
        }
        #endregion
    }
}