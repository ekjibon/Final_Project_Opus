using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Authorize]
    [Area("HRPMSEmployee")]
    public class OtherActivitiesController : Controller
    {
        private readonly IPersonalInfoService personalInfoService;
        private readonly IOtherActivityService otherActivityService;
        private readonly IHRPMSActivityTypeService iHRPMSActivityTypeService;
        private readonly IActivityNameService activityNameService;
        private readonly IPhotographService photographService;

        public OtherActivitiesController(IHostingEnvironment hostingEnvironment, IPhotographService photographService, IOtherActivityService otherActivityService, IActivityNameService activityNameService, IHRPMSActivityTypeService iHRPMSActivityTypeService, IPersonalInfoService personalInfoService)
        {
            this.personalInfoService = personalInfoService;
            this.photographService = photographService;
            this.otherActivityService = otherActivityService;
            this.activityNameService = activityNameService;
            this.iHRPMSActivityTypeService = iHRPMSActivityTypeService;
        }

        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new OtherActivitiesViewModel
            {
                employeeID = id,
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                hRPMSActivityTypes = await iHRPMSActivityTypeService.GetHRPMSActivityType(),
                otherActivities = await otherActivityService.GetOtherActivityByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] OtherActivitiesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.hRPMSActivityTypes = await iHRPMSActivityTypeService.GetHRPMSActivityType();
                model.otherActivities = await otherActivityService.GetOtherActivityByEmpId((int)model.employeeID);
                model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById((int)model.employeeID);
                return View(model);
            }

            OtherActivity data = new OtherActivity
            {
                Id = model.activityID ?? 0,
                employeeID = (int)model.employeeID,
                activityNameId = model.activityName,
                description = model.description
            };

            await otherActivityService.SaveOtherActivity(data);
            await personalInfoService.UpdateEmployeeinfoById((int)model.employeeID);
            return RedirectToAction("Index", "OtherActivities", new
            {
                id = (int)model.employeeID
            });
        }
        
        public async Task<IActionResult> Delete(int id, int empId)
        {
            await otherActivityService.DeleteOtherActivityById(id);
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "OtherActivities", new
            {
                id = empId
            });
        }

    }
}