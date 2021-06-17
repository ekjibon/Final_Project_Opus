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
    public class PreviousEmploymentController : Controller
    {
        private readonly IPersonalInfoService personalInfoService;
        private readonly IPhotographService photographService;
        private readonly IWagesPersonalInfoService wagesPersonalInfoService;
        private readonly IPriviousEmploymentService priviousEmploymentService;
        private readonly IWagesPriviousEmploymentService wagesPriviousEmploymentService;
        private readonly IHRPMSOrganizationTypeService iHRPMSOrganizationTypeService;

        public PreviousEmploymentController(IPersonalInfoService personalInfoService, IPhotographService photographService, IPriviousEmploymentService priviousEmploymentService, IHRPMSOrganizationTypeService iHRPMSOrganizationTypeService, IWagesPriviousEmploymentService wagesPriviousEmploymentService, IWagesPersonalInfoService wagesPersonalInfoService)
        {
            this.personalInfoService = personalInfoService;
            this.photographService = photographService;
            this.priviousEmploymentService = priviousEmploymentService;
            this.iHRPMSOrganizationTypeService = iHRPMSOrganizationTypeService;
            this.wagesPriviousEmploymentService = wagesPriviousEmploymentService;
            this.wagesPersonalInfoService = wagesPersonalInfoService;
        }

        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new PreviousEmploymentViewModel
            {
                employeeID = id,
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                priviousEmployments = await priviousEmploymentService.GetPriviousEmploymentByEmpId(id),
                hRPMSOrganizationTypes = await iHRPMSOrganizationTypeService.GetHRPMSOrganizationType(),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        public async Task<IActionResult> WagesIndex(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new PreviousEmploymentViewModel
            {
                employeeID = id,
                wagesPriviousEmployments = await wagesPriviousEmploymentService.GetPriviousEmploymentByEmpId(id),
                hRPMSOrganizationTypes = await iHRPMSOrganizationTypeService.GetHRPMSOrganizationType(),
                employeeNameCode = await wagesPersonalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WagesIndex([FromForm] PreviousEmploymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.hRPMSOrganizationTypes = await iHRPMSOrganizationTypeService.GetHRPMSOrganizationType();
                model.wagesPriviousEmployments = await wagesPriviousEmploymentService.GetPriviousEmploymentByEmpId((int)model.employeeID);
                model.employeeNameCode = await wagesPersonalInfoService.GetEmployeeNameCodeById((int)model.employeeID);
                return View(model);
            }

            WagesPriviousEmployment data = new WagesPriviousEmployment
            {
                Id = model.previousEmploymentID ?? 0,
                employeeID = (int)model.employeeID,
                organizationTypeId = model.organizationType,
                companyName = model.companyName,
                position = model.position,
                department = model.department,
                companyBusiness = model.companyBusiness,
                startDate = model.startDate,
                endDate = model.endDate,
                companyLocation = model.companyLocation,
                responsibilities = model.responsibilities
            };

            await wagesPriviousEmploymentService.SavePriviousEmployment(data);

            return RedirectToAction("WagesIndex", "PreviousEmployment", new
            {
                id = (int)model.employeeID
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] PreviousEmploymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.hRPMSOrganizationTypes = await iHRPMSOrganizationTypeService.GetHRPMSOrganizationType();
                model.priviousEmployments = await priviousEmploymentService.GetPriviousEmploymentByEmpId((int)model.employeeID);
                model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById((int)model.employeeID);
                return View(model);
            }

            PriviousEmployment data = new PriviousEmployment
            {
                Id = model.previousEmploymentID ?? 0,
                employeeID = (int)model.employeeID,
                organizationTypeId = model.organizationType,
                companyName = model.companyName,
                position = model.position,
                department = model.department,
                companyBusiness = model.companyBusiness,
                startDate = model.startDate,
                endDate = model.endDate,
                companyLocation = model.companyLocation,
                responsibilities = model.responsibilities
            };

            await priviousEmploymentService.SavePriviousEmployment(data);

            return RedirectToAction("Index", "PreviousEmployment", new
            {
                id = (int)model.employeeID
            });
        }

        public async Task<IActionResult> Delete(int id, int empId)
        {
            await priviousEmploymentService.DeletePriviousEmploymentById(id);
            return RedirectToAction("Index", "PreviousEmployment", new
            {
                id = empId
            });
        }

        public async Task<IActionResult> WagesDelete(int id, int empId)
        {
            await wagesPriviousEmploymentService.DeletePriviousEmploymentById(id);
            return RedirectToAction("WagesIndex", "PreviousEmployment", new
            {
                id = empId
            });
        }

    }
}