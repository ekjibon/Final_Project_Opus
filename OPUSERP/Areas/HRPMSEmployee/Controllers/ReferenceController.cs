using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Authorize]
    [Area("HRPMSEmployee")]
    public class ReferenceController : Controller
    {
        private readonly IPersonalInfoService personalInfoService;
        private readonly IPhotographService photographService;
        private readonly IWagesPersonalInfoService wagesPersonalInfoService;
        private readonly IWagesReferenceService wagesReferenceService;
        private readonly IReferenceService referenceService;

        public ReferenceController(IHostingEnvironment hostingEnvironment, IPhotographService photographService, IReferenceService referenceService, IPersonalInfoService personalInfoService, IWagesPersonalInfoService wagesPersonalInfoService, IWagesReferenceService wagesReferenceService)
        {
            this.personalInfoService = personalInfoService;
            this.photographService = photographService;
            this.referenceService = referenceService;
            this.wagesPersonalInfoService = wagesPersonalInfoService;
            this.wagesReferenceService = wagesReferenceService;
        }

        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new ReferenceViewModel
            {
                employeeID = id,
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                references = await referenceService.GetReferenceByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        public async Task<IActionResult> WagesIndex(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new ReferenceViewModel
            {
                employeeID = id,
                wagesReferences = await wagesReferenceService.GetReferenceByEmpId(id),
                employeeNameCode = await wagesPersonalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WagesIndex([FromForm] ReferenceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.wagesReferences = await wagesReferenceService.GetReferenceByEmpId((int)model.employeeID);
                model.employeeNameCode = await wagesPersonalInfoService.GetEmployeeNameCodeById((int)model.employeeID);
                return View(model);
            }

            HRPMS.Data.Entity.Employee.WagesReference data = new HRPMS.Data.Entity.Employee.WagesReference
            {
                Id = model.refID ?? 0,
                employeeID = (int)model.employeeID,
                name = model.refName,
                organization = model.refOrganization,
                designation = model.refDesignation,
                email = model.refEmail,
                contact = model.refContact
            };

            await wagesReferenceService.SaveReference(data);
            await wagesPersonalInfoService.UpdateEmployeeinfoById((int)model.employeeID);
            return RedirectToAction("WagesIndex", "Reference", new
            {
                id = (int)model.employeeID
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ReferenceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.references = await referenceService.GetReferenceByEmpId((int)model.employeeID);
                model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById((int)model.employeeID);
                return View(model);
            }

            HRPMS.Data.Entity.Employee.Reference data = new HRPMS.Data.Entity.Employee.Reference
            {
                Id = model.refID ?? 0,
                employeeID = (int)model.employeeID,
                name = model.refName,
                organization = model.refOrganization,
                designation = model.refDesignation,
                email = model.refEmail,
                contact = model.refContact
            };

            await referenceService.SaveReference(data);
            await personalInfoService.UpdateEmployeeinfoById((int)model.employeeID);
            return RedirectToAction("Index", "Reference", new
            {
                id = (int)model.employeeID
            });
        }

        public async Task<IActionResult> Delete(int id, int empId)
        {
            await referenceService.DeleteReferenceById(id);
            return RedirectToAction("Index", "Reference", new
            {
                id = empId
            });
        }

        public async Task<IActionResult> WagesDelete(int id, int empId)
        {
            await wagesReferenceService.DeleteReferenceById(id);
            return RedirectToAction("WagesIndex", "Reference", new
            {
                id = empId
            });
        }

    }
}