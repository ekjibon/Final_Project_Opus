using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSAssignment.Helpers;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.Areas.HRPMSPunishment.Models;
using OPUSERP.Areas.HRPMSPunishment.Models.Lang;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.PunishmentCharge;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.PunishmentCharge.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.HRPMSPunishment.Controllers
{
    [Authorize]
    [Area("HRPMSPunishment")]
    public class ChargePunishmentController : Controller
    {
        private readonly LangGenerate<PunishmentLn> _lang;
        private readonly IPersonalInfoService personalInfoService;
        private readonly ICPService cpService;

        public ChargePunishmentController(IHostingEnvironment hostingEnvironment, IPersonalInfoService personalInfoService, ICPService cpService)
        {
            _lang = new LangGenerate<PunishmentLn>(hostingEnvironment.ContentRootPath);
            this.personalInfoService = personalInfoService;
            this.cpService = cpService;
        }

        // GET: Punishment/CPEmployeeList
        public async Task<IActionResult> CPEmployeeList()
        {
            var model = new EmployeeInfoViewModel
            {
                fLang3 = _lang.PerseLang("Punishment/ChargeEmpListBN.json"),
                allEmployeeInfos = await personalInfoService.GetEmployeeInfo()
            };
            return View(model);
        }

        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new ChargeViewModel
            {
                //fLang = _lang.PerseLang("Assignment/AssignmentEN.json"), 
                charges = await cpService.GetChargeByEmpId(id)
            };
            return View(model);
        }

        // POST: Info/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ChargeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeId.ToString();
                model.charges = await cpService.GetChargeByEmpId(model.employeeId);
                return View(model);
            }

            Charge data = new Charge
            {
                Id = Convert.ToInt32(model.ChargeID),
                employeeId = Convert.ToInt32(model.employeeId),
                letterNo = model.letterNo,
                chargeDate = model.chargeDate,
                chargeFor = model.chargeFor,
                remarks = model.remarks
            };

            int lstId = await cpService.SaveCharge(data);

            return RedirectToAction(nameof(Index), new { @id = model.employeeId });
        }

        // GET: Punishment/CPEmployeeList
        public async Task<IActionResult> PunishEmployeeList()
        {
            var model = new EmployeeInfoViewModel
            {
                fLang3 = _lang.PerseLang("Punishment/PunishmentEmpListBN.json"),
                allEmployeeInfos = await personalInfoService.GetEmployeeInfo()
            };
            return View(model);
        }

        public async Task<IActionResult> CreatePunishment(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new PunishmentViewModel
            {
                //fLang = _lang.PerseLang("Assignment/AssignmentEN.json"), 
                punishments = await cpService.GetPunishmentByEmpId(id)
            };
            return View(model);
        }

        // POST: Info/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePunishment([FromForm] PunishmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeId.ToString();
                model.punishments = await cpService.GetPunishmentByEmpId(model.employeeId);
                return View(model);
            }

            HRPMS.Data.Entity.PunishmentCharge.Punishment data = new HRPMS.Data.Entity.PunishmentCharge.Punishment
            {
                Id = Convert.ToInt32(model.PunishmentID),
                employeeId = Convert.ToInt32(model.employeeId),
                letterNo = model.letterNo,
                punishmentDate = model.punishmentDate,
                punishmentFor = model.punishmentFor,
                remarks = model.remarks
            };

            int lstId = await cpService.SavePunishment(data);

            return RedirectToAction(nameof(CreatePunishment), new { @id = model.employeeId });
        }
    }
}