using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.HRPMS.Data.Entity.Employee;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Authorize]
    [Area("HRPMSEmployee")]
    public class SupervisorController : Controller
    {
        private readonly LangGenerate<SupervisorLn> _lang;
        private readonly IPersonalInfoService personalInfoService;
        private readonly ISupervisorService supervisorService;
        private readonly IPhotographService photographService;

        public SupervisorController(IHostingEnvironment hostingEnvironment, IPhotographService photographService, ISupervisorService supervisorService, IPersonalInfoService personalInfoService)
        {
            this.personalInfoService = personalInfoService;
            this.supervisorService = supervisorService;
            this.photographService = photographService;
            _lang = new LangGenerate<SupervisorLn>(hostingEnvironment.ContentRootPath);
        }

        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new SupervisorViewModel
            {
                employeeID = id,
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                fLang = _lang.PerseLang("Employee/SupervisorEN.json", "Employee/SupervisorBN.json", Request.Cookies["lang"]),
                supervisors = await supervisorService.GetSupervisorByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] SupervisorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.fLang = _lang.PerseLang("Employee/SupervisorEN.json", "Employee/SupervisorBN.json", Request.Cookies["lang"]);
                model.supervisors = await supervisorService.GetSupervisorByEmpId((int)model.employeeID);
                model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById((int)model.employeeID);
                return View(model);
            }
            
            Supervisor data = new Supervisor
            {
                Id = model.supervisorID ?? 0,
                employeeID = (int)model.employeeID,
                supervisorId = model.supervisorEmpID,
                commandOrder = model.commandOrder,
                canFinalApprover = model.canFinalApprover,
                supervisorDate=model.supervisorDate
            };

            //return Json(data);

            await supervisorService.SaveSupervisor(data);
            await personalInfoService.UpdateEmployeeinfoById((int)model.employeeID);
            return RedirectToAction("Index", "Supervisor", new
            {
                id = (int)model.employeeID
            });
        }

        public async Task<IActionResult> Delete(int id, int empId)
        {
            await supervisorService.DeleteSupervisorById(id);
            return RedirectToAction("Index", "Supervisor", new
            {
                id = empId
            });
        }

        #region API Section
        [Route("global/api/GetFirstSupervisorByEmpId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetFirstSupervisorByEmpId(int id)
        {
            return Json(await supervisorService.GetFirstSupervisorByEmpId(id));
        }
        #endregion

    }
}