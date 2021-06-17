using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.Leave.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Authorize]
    [Area("HRPMSEmployee")]
    public class LeaveLogController : Controller
    {
        // GET: LeaveLog
        private readonly LangGenerate<LeaveLogLn> _lang;
        private readonly ILeaveLogService leaveLogService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly ILeaveTypeService leaveTypeService;

        public LeaveLogController(IHostingEnvironment hostingEnvironment, ILeaveTypeService leaveTypeService, ILeaveLogService leaveLogService, IPersonalInfoService personalInfoService)
        {
            _lang = new LangGenerate<LeaveLogLn>(hostingEnvironment.ContentRootPath);
            this.leaveLogService = leaveLogService;
            this.personalInfoService = personalInfoService;
            this.leaveTypeService = leaveTypeService;
        }

        // GET: PromotionLog
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            LeaveLogViewModel model = new LeaveLogViewModel
            {
                employeeID = id.ToString(),
                fLang = _lang.PerseLang("Employee/LeaveLogEN.json", "Employee/LeaveLogBN.json", Request.Cookies["lang"]),
                leaveLogs = await leaveLogService.GetLeaveLogByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
                leaveTypelist = await leaveTypeService.GetAllLeaveType()
            };

            return View(model);
        }

        // POST: PromotionLog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] LeaveLogViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("Employee/LeaveLogEN.json", "Employee/LeaveLogBN.json", Request.Cookies["lang"]);
                model.leaveLogs = await leaveLogService.GetLeaveLogByEmpId(Int32.Parse(model.employeeID));
                model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(Int32.Parse(model.employeeID));
                model.leaveTypelist = await leaveTypeService.GetAllLeaveType();
                return View(model);
            }

            LeaveLog data = new LeaveLog
            {
                Id = Int32.Parse(model.leaveId),
                employeeId = Int32.Parse(model.employeeID),
                leaveTypeId = model.type,
                purposeOfLeave = model.purpose,
                leaveFrom = model.from,
                leaveTo = model.to

            };
            await leaveLogService.SaveLeaveLog(data);

            return RedirectToAction(nameof(Index));

        }

        // Delete: LeaveLog
        public async Task<IActionResult> Delete(int id, int empId)
        {
            await leaveLogService.DeleteLeaveLogById(id);
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "LeaveLog", new
            {
                id = empId
            });
        }

        //xxxxxxxxxxxxxxxxxxxxxx
    }
}