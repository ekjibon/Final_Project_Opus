using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using CLUB.Areas.Assignment.Helpers;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.CLUB.Services.Member.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.Areas.MemberInfo.Models;
using OPUSERP.Areas.MemberInfo.Models.Lang;

namespace OPUSERP.CLUB.Areas.MemberInfo.Controllers
{
    [Authorize]
    [Area("MemberInfo")]
    public class TransferLogController : Controller
    {
        //private readonly LangGenerate<TransferLogLn> _lang;
        private readonly LangGenerate<TransferLogLn> _lang;
        private readonly ISalaryGradeService salaryGradeService;
        private readonly IServiceHistoryService serviceHistoryService;
        private readonly IPersonalInfoService personalInfoService;

        public TransferLogController(IHostingEnvironment hostingEnvironment, IPersonalInfoService personalInfoService, ISalaryGradeService salaryGradeService, IServiceHistoryService serviceHistoryService)
        {
            _lang = new LangGenerate<TransferLogLn>(hostingEnvironment.ContentRootPath);
            this.salaryGradeService = salaryGradeService;
            this.serviceHistoryService = serviceHistoryService;
            this.personalInfoService = personalInfoService;
        }

        // GET: Transfar
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new TransferLogViewModel
            {
                employeeID = id.ToString(),
                fLang = _lang.PerseLang("Employee/TransferLogEN.json", "Employee/TransferLogBN.json", Request.Cookies["lang"]),
                salaryGrade = await salaryGradeService.GetAllSalaryGrade(),
                transferLogs = await serviceHistoryService.GetServiceHistoryByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }


        // POST: Transfar/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] TransferLogViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.fLang = _lang.PerseLang("Employee/TransferLogEN.json", "Employee/TransferLogBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            MemberTransferLog data = new MemberTransferLog
            {
                Id = Int32.Parse(model.transfarID),
                employeeId = Int32.Parse(model.employeeID),
                workStation = model.workStation,
                from = model.fromDate,
                to = model.toDate,
                department = model.department,
                designation = model.designation
            };

            await serviceHistoryService.SaveServiceHistory(data);

            return RedirectToAction(nameof(Index));
        }

    }
}