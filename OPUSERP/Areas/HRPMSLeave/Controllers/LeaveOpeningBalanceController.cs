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
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.Leave.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.HRPMSLeave.Controllers
{
    [Authorize]
    [Area("HRPMSLeave")]
    public class LeaveOpeningBalanceController : Controller
    {
        private readonly LangGenerate<LeaveLn> _lang;
        private readonly ILeaveTypeService leaveTypeService;
        private readonly IYearCourseTitleService yearCourseTitleService;
        private readonly ILeavePolicyService leavePolicyService;
        private readonly IPersonalInfoService personalInfoService;

        public LeaveOpeningBalanceController(IHostingEnvironment hostingEnvironment, ILeaveTypeService leaveTypeService, IYearCourseTitleService yearCourseTitleService, ILeavePolicyService leavePolicyService, IPersonalInfoService personalInfoService)
        {
            _lang = new LangGenerate<LeaveLn>(hostingEnvironment.ContentRootPath);
            this.leaveTypeService = leaveTypeService;
            this.yearCourseTitleService = yearCourseTitleService;
            this.leavePolicyService = leavePolicyService;
            this.personalInfoService = personalInfoService;
        }


        public async Task<IActionResult> Index()
        {
            LeavePolicyViewModel model = new LeavePolicyViewModel
            {
                leaveTypelist = await leaveTypeService.GetAllLeaveType(),
                years = await yearCourseTitleService.GetYear(),
                leavePolicies = await leavePolicyService.GetLeavePolicy(),
                leaveOpeningBalances = await leavePolicyService.GetLeaveOpeningBalance(),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };
            return View(model);
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
                model.leavePolicies = await leavePolicyService.GetLeavePolicy();
                model.leaveOpeningBalances = await leavePolicyService.GetLeaveOpeningBalance();
                return View(model);
            }

            LeaveOpeningBalance leaveOpeningBalance = new LeaveOpeningBalance
            {
                Id = model.id,
                yearId = model.year,
                leaveTypeId = model.leaveTypeId,
                employeeId = model.employeeId,
                leaveDays = model.maxYearlyLeave,
                leaveCarryDays = model.maxCarry
            };

            await leavePolicyService.SaveLeaveOpeningBalance(leaveOpeningBalance);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id, int empId)
        {
            await leavePolicyService.DeleteLeaveOpeningBalanceById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}