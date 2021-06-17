using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSAssignment.Helpers;
using OPUSERP.Areas.HRPMSLeave.Models;
using OPUSERP.Areas.HRPMSLeave.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Leave;
using OPUSERP.HRPMS.Services.Leave.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.HRPMSLeave.Controllers
{
    [Authorize]
    [Area("HRPMSLeave")]
    public class LeavePolicyController : Controller
    {
        private readonly LangGenerate<LeaveLn> _lang;
        private readonly ILeaveTypeService leaveTypeService;
        private readonly IYearCourseTitleService yearCourseTitleService;
        private readonly ILeavePolicyService leavePolicyService;

        public LeavePolicyController(IHostingEnvironment hostingEnvironment,ILeaveTypeService leaveTypeService, IYearCourseTitleService yearCourseTitleService, ILeavePolicyService leavePolicyService)
        {
            _lang = new LangGenerate<LeaveLn>(hostingEnvironment.ContentRootPath);
            this.leaveTypeService = leaveTypeService;
            this.yearCourseTitleService = yearCourseTitleService;
            this.leavePolicyService = leavePolicyService;
        }


        public async Task<IActionResult> Index()
        {
            LeavePolicyViewModel model = new LeavePolicyViewModel
            {
                leaveTypelist = await leaveTypeService.GetAllLeaveType(),
                years = await yearCourseTitleService.GetYear(),
                leavePolicies = await leavePolicyService.GetLeavePolicy()
            };
            return View(model);
        }

        public async Task<IActionResult> OpeningBalanceProcess(int id)
        {
            await leavePolicyService.OpeningBalanceProcess(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] LeavePolicyViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                model.leaveTypelist = await leaveTypeService.GetAllLeaveType();
                model.years = await yearCourseTitleService.GetYear();
                return View(model);
            }

            LeavePolicy data = new LeavePolicy
            {
                Id = model.id,
                leaveTypeId = model.leaveTypeId,
                yearId = model.year,
                yearlyMaxLeave = model.maxYearlyLeave,
                yearlyMaxCarry = model.maxCarry,
                weeklyOffBridge = model.weeklyBridge,
                govtHolidayBridge = model.govtHolidayBridge,
                remarks = model.remarks,
                paymentType=model.paymentType,
                maxBridgeLimit=model.maxBridgeLimit,
                highestCarryForward=model.highestCarryForward
            };

            await leavePolicyService.SaveLeavePolicy(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Status()
        {
            LeavePolicyViewModel model = new LeavePolicyViewModel
            {
                leaveTypelist = await leaveTypeService.GetAllLeaveType(),
                years = await yearCourseTitleService.GetYear(),
            };
            return View(model);
        }

        // GET: LeaveDay
        public async Task<ActionResult> LeaveDay()
        {
            LeaveDayViewModel model = new LeaveDayViewModel
            {
                leaveDays = await leavePolicyService.GetAllLeaveDay(),
            };
            return View(model);
        }

        // POST: LeaveDay/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LeaveDay([FromForm] LeaveDayViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.leaveDays = await leavePolicyService.GetAllLeaveDay();
                return View(model);
            }

            LeaveDay data = new LeaveDay
            {
                Id = (int)model.leaveDayId,
                leaveDayName = model.leaveDayName,
                dayStartTime = model.dayStartTime,
                dayEndTime = model.dayEndTime,
                description = model.description
            };

            await leavePolicyService.SaveLeaveDay(data);

            return RedirectToAction(nameof(LeaveDay));
        }
        public async Task<IActionResult> DeleteLeaveDayById(int id)
        {
            await leavePolicyService.DeleteLeaveDayById(id);
            return RedirectToAction("LeaveDay", "LeavePolicy", new { Area = "HRPMSLeave" });
        }

    }
}