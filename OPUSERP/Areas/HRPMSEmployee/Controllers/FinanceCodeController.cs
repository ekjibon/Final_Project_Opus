using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Area("HRPMSEmployee")]
    public class FinanceCodeController : Controller
    {
        
        private readonly IFinanceCodeService financeCodeService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IPhotographService photographService;

        public FinanceCodeController(IHostingEnvironment hostingEnvironment, IPhotographService photographService, IFinanceCodeService financeCodeService, IPersonalInfoService personalInfoService)
        {
            this.personalInfoService = personalInfoService;
            this.photographService = photographService;
            this.financeCodeService = financeCodeService;
        }

        // GET: Bank
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new BankViewModel
            {
                employeeID = id.ToString(),
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                financeCodes = await financeCodeService.GetFinanceCodeByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        // POST: Bank/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] BankViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.financeCodes = await financeCodeService.GetFinanceCodeByEmpId(Int32.Parse(model.employeeID));
                model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(Int32.Parse(model.employeeID));
                return View(model);
            }
            //return Json(model);
            FinanceCode data = new FinanceCode
            {
                Id = (int)model.fnCodeId,
                employeeId = Int32.Parse(model.employeeID),
                fnCode = model.fnCode,
            };

            await financeCodeService.SaveFinanceCode(data);

            return RedirectToAction(nameof(Index));
        }

        // Delete: BankInfo
        public async Task<IActionResult> Delete(int id, int empId)
        {
            await financeCodeService.DeleteFinanceCodeById(id);
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "FinanceCode", new
            {
                id = empId
            });
        }
    }
}