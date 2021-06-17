using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Authorize]
    [Area("HRPMSEmployee")]
    public class EmployeeContractInfoController : Controller
    {
        private readonly IEmployeeContratInfoService employeeContratInfoService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IPhotographService photographService;

        public EmployeeContractInfoController(IEmployeeContratInfoService employeeContratInfoService, IPhotographService photographService, IPersonalInfoService personalInfoService)
        {
            this.employeeContratInfoService = employeeContratInfoService;
            this.personalInfoService = personalInfoService;
            this.photographService = photographService;
        }

        public async Task<IActionResult> Index(int id)
        {
            EmployeeContractInfoViewModel model = new EmployeeContractInfoViewModel
            {
                employeeID = id,
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                employeeContractInfos =await employeeContratInfoService.GetContractInfoByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        // POST: Contract/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] EmployeeContractInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            EmployeeContractInfo data = new EmployeeContractInfo
            {
                Id = Convert.ToInt32(model.contractId),
                employeeId = model.employeeID,
                contractStartDate = model.ContractStartDate,
                contractEndDate = model.ContractEndDate,
                remarks = model.remarks,
            };

            await employeeContratInfoService.SaveContractInfo(data);
            await personalInfoService.UpdateEmployeeinfoById((int)model.employeeID);
            return RedirectToAction("Index", "EmployeeContractInfo", new
            {
                id = model.employeeID
            });
        }

        // Delete: Contract
        public async Task<IActionResult> Delete(int id, int empId)
        {
            await employeeContratInfoService.DeletContractInfoById(id);
            return RedirectToAction("Index", "EmployeeContractInfo", new
            {
                id = empId
            });
        }
    }
}