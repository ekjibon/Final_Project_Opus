using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.DisciplineInvestigation.Interfaces;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Authorize]
    [Area("HRPMSEmployee")]
    public class DisciplinaryController : Controller
    {
        private readonly LangGenerate<Disciplinary> _lang;
        private readonly IDisciplineInvestigation disciplineInvestigation;
        private readonly IDisciplinaryService disciplinaryService;
        private readonly IPhotographService photographService;
        private readonly IPersonalInfoService personalInfoService;

        public DisciplinaryController(IHostingEnvironment hostingEnvironment, IPhotographService photographService, IDisciplineInvestigation disciplineInvestigation, IDisciplinaryService disciplinaryService, IPersonalInfoService personalInfoService)
        {
            _lang = new LangGenerate<Disciplinary>(hostingEnvironment.ContentRootPath);
            this.disciplineInvestigation = disciplineInvestigation;
            this.photographService = photographService;
            this.disciplinaryService = disciplinaryService;
            this.personalInfoService = personalInfoService;
        }

        // GET: Disciplinary
        public async Task<ActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new DisciplinaryViewModel
            {
                employeeId = id,
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                fLang = _lang.PerseLang("Employee/DisciplinaryEN.json", "Employee/DisciplinaryBN.json", Request.Cookies["lang"]),
                offenses = await disciplineInvestigation.GetAllOffense(),
                punishments = await disciplineInvestigation.GetAllPunishment(),
                disciplinaries = await disciplinaryService.GetDisciplinaryLogByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        // POST: Disciplinary/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] DisciplinaryViewModel model)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeId;
                model.fLang = _lang.PerseLang("Employee/DisciplinaryEN.json", "Employee/DisciplinaryBN.json", Request.Cookies["lang"]);
                model.offenses = await disciplineInvestigation.GetAllOffense();
                model.punishments = await disciplineInvestigation.GetAllPunishment();
                model.disciplinaries = await disciplinaryService.GetDisciplinaryLogByEmpId(model.employeeId);
                model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(model.employeeId);
                return View(model);
            }

            string fileName = "";
            if (model.goAttachment != null)
            {
                FileSave.SaveFile(out fileName, model.goAttachment, "DecActionFiles");
            }
            DisciplinaryLog data = new DisciplinaryLog
            {
                Id = Convert.ToInt32(model.disciplinaryId),
                employeeId = model.employeeId,
                OffenseId = model.offenseId,
                naturalPunishmentId = Convert.ToInt32(model.punishmentId),
                punishmentDate = model.punishmentDate,
                startingDate = model.startFrom,
                endDate = model.endTo,
                goNumberWithDate = model.goNo,
                remarks = model.remarks,
                goFileURL = fileName
            };

            await disciplinaryService.SaveDisciplinaryLog(data);

            return RedirectToAction(nameof(Index));

        }

        // Delete: Disciplinary
        public async Task<IActionResult> Delete(int id, int empId)
        {
            await disciplinaryService.DeleteDisciplinaryLogById(id);
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "Disciplinary", new
            {
                id = empId
            });
        }

    }
}