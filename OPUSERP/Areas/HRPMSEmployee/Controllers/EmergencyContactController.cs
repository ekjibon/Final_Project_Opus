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

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Authorize]
    [Area("HRPMSEmployee")]
    public class EmergencyContactController : Controller
    {
        private readonly IPersonalInfoService personalInfoService;
        private readonly IPhotographService photographService;
        private readonly IWagesPersonalInfoService wagesPersonalInfoService;
        private readonly IEmergencyContactService emergencyContactService;
        private readonly IWagesEmergencyContactService wagesEmergencyContactService;

        public EmergencyContactController(IHostingEnvironment hostingEnvironment, IPhotographService photographService, IEmergencyContactService emergencyContactService, IPersonalInfoService personalInfoService, IWagesPersonalInfoService wagesPersonalInfoService, IWagesEmergencyContactService wagesEmergencyContactService)
        {
            this.personalInfoService = personalInfoService;
            this.photographService = photographService;
            this.emergencyContactService = emergencyContactService;
            this.wagesPersonalInfoService = wagesPersonalInfoService;
            this.wagesEmergencyContactService = wagesEmergencyContactService;
        }

        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new EmergencyContactViewModel
            {
                employeeID = id,
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                emergencyContacts = await emergencyContactService.GetEmergencyContactByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        public async Task<IActionResult> WagesIndex(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new EmergencyContactViewModel
            {
                employeeID = id,
                wagesEmergencyContacts = await wagesEmergencyContactService.GetEmergencyContactByEmpId(id),
                employeeNameCode = await wagesPersonalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] EmergencyContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.emergencyContacts = await emergencyContactService.GetEmergencyContactByEmpId((int)model.employeeID);
                model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById((int)model.employeeID);
                return View(model);
            }

            EmergencyContact data = new EmergencyContact
            {
                Id = model.refID ?? 0,
                employeeID = (int)model.employeeID,
                name = model.refName,
                relation = model.refRelation,
                organization = model.refOrganization,
                designation = model.refDesignation,
                email = model.refEmail,
                contact = model.refContact
            };

            await emergencyContactService.SaveEmergencyContact(data);
            await personalInfoService.UpdateEmployeeinfoById((int)model.employeeID);
            return RedirectToAction("Index", "EmergencyContact", new
            {
                id = (int)model.employeeID
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WagesIndex([FromForm] EmergencyContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.wagesEmergencyContacts = await wagesEmergencyContactService.GetEmergencyContactByEmpId((int)model.employeeID);
                model.employeeNameCode = await wagesPersonalInfoService.GetEmployeeNameCodeById((int)model.employeeID);
                return View(model);
            }

            WagesEmergencyContact data = new WagesEmergencyContact
            {
                Id = model.refID ?? 0,
                employeeID = (int)model.employeeID,
                name = model.refName,
                relation = model.refRelation,
                organization = model.refOrganization,
                designation = model.refDesignation,
                email = model.refEmail,
                contact = model.refContact
            };

            await wagesEmergencyContactService.SaveEmergencyContact(data);
            await wagesPersonalInfoService.UpdateEmployeeinfoById((int)model.employeeID);
            return RedirectToAction("WagesIndex", "EmergencyContact", new
            {
                id = (int)model.employeeID
            });
        }

        public async Task<IActionResult> Delete(int id, int empId)
        {
            await emergencyContactService.DeleteEmergencyContactById(id);
            return RedirectToAction("Index", "EmergencyContact", new
            {
                id = empId
            });
        }

        public async Task<IActionResult> WagesDelete(int id, int empId)
        {
            await wagesEmergencyContactService.DeleteEmergencyContactById(id);
            return RedirectToAction("WagesIndex", "EmergencyContact", new
            {
                id = empId
            });
        }
    }
}