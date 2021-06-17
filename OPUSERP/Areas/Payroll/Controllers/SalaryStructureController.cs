using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Payroll.Models;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.Payroll.Data.Entity.Salary;
using OPUSERP.Payroll.Services.Salary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Controllers
{
    [Area("Payroll")]
    public class SalaryStructureController : Controller
    {
        private readonly ISalaryService salaryService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IEmployeeProjectActivityService employeeProjectActivityService;
        public SalaryStructureController(ISalaryService salaryService, IPersonalInfoService personalInfoService, IEmployeeProjectActivityService employeeProjectActivityService)
        {
            this.salaryService = salaryService;
            this.personalInfoService = personalInfoService;
            this.employeeProjectActivityService = employeeProjectActivityService;
        }

        #region Salary Structure

        public async Task<ActionResult> Index(int? employeeInfoId)
        {
            EmployeesSalaryStructureViewModel model = new EmployeesSalaryStructureViewModel
            {
                employeesSalaryStructuresList = await salaryService.GetEmployeesSalaryStructureByEmpId(Convert.ToInt32(employeeInfoId)),
                salaryGradesList = await salaryService.GetAllSalaryGrade(),
                salaryPeriod = await salaryService.GetSalaryPeriodmax()


            };
            if (model.employeesSalaryStructure == null) model.employeesSalaryStructure = new EmployeesSalaryStructure();
            ViewBag.employeeInfoId = employeeInfoId;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] EmployeesSalaryStructureViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.employeesSalaryStructuresList = await salaryService.GetEmployeesSalaryStructureByEmpId(model.employeeInfoId);
                model.salaryGradesList = await salaryService.GetAllSalaryGrade();
                return View(model);
            }

            IEnumerable<SalaryHead> lstSalaryHead = await salaryService.GetAllSalaryHead();

            var head = await salaryService.GetEmployeesSalaryStructureByEmpId(model.employeeInfoId);
            if (head.Count() > 0)
            {
                await salaryService.SaveStructureHistory(model.employeeInfoId, HttpContext.User.Identity.Name);
                await salaryService.DeleteEmployeesSalaryStructureByempId(Convert.ToInt32(model.employeeInfoId));
            }
            foreach (SalaryHead Headdata in lstSalaryHead)
            {
                decimal Amount = 0;
                var headinfo = await salaryService.GetSalaryGradePercentBysalaryHeadId(Convert.ToInt32(model.salaryGradeId), Headdata.Id);
                if (headinfo != null)
                {
                    if (headinfo.salaryCalulationTypeId == 2)
                    {
                        Amount = model.amount * (Convert.ToDecimal(headinfo.percentAmount) / 100);
                    }
                    else if (headinfo.salaryCalulationTypeId == 1 || headinfo.salaryCalulationTypeId == 3)
                    {
                        Amount = Convert.ToDecimal(headinfo.percentAmount);
                    }
                }

                EmployeesSalaryStructure data = new EmployeesSalaryStructure
                {
                    Id = 0,
                    employeeInfoId = model.employeeInfoId,
                    salarySlabId = Convert.ToInt32(model.salarySlab),
                    salaryHeadId = Headdata.Id,
                    amount = Math.Ceiling(Amount),
                    isActive = "Active",
                    effectiveDate = model.effectiveDate
                };
                await salaryService.SaveEmployeesSalaryStructure(data);
            }

            return RedirectToAction("Index", "SalaryStructure", new { employeeInfoId = model.employeeInfoId, Area = "Payroll" });
        }

        [HttpPost]
        public async Task<JsonResult> Editsalarystructure([FromForm] EditsalarystructureViewModel model)
        {
            try
            {
                await salaryService.UpdateStructureHistory(model.editsalarystructureId, HttpContext.User.Identity.Name);

                var masterId = await salaryService.EditEmployeesSalaryStructure(model.editsalarystructureId, model.headamount, model.salarystructureStatus);

                return Json(masterId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSalarySlabBysalaryGradeId(int salaryGradeId)
        {
            return Json(await salaryService.GetSalarySlabBysalaryGradeId(salaryGradeId));
        }
        [HttpGet]
        public async Task<IActionResult> GetSalarySlabById(int Id)
        {
            return Json(await salaryService.GetSalarySlabById(Id));
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployeesSalaryStructureAdditionByEmpId(int employeeInfoId)
        {
            var data = await salaryService.GetEmployeesSalaryStructureByEmpId(employeeInfoId);
            return Json(data.Where(x => x.salaryHead.salaryHeadType == "Addition"));
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployeesSalaryStructureDeductionByEmpId(int employeeInfoId)
        {
            var data = await salaryService.GetEmployeesSalaryStructureByEmpId(employeeInfoId);
            return Json(data.Where(x => x.salaryHead.salaryHeadType == "Deduction"));
        }
        [HttpGet]
        public async Task<JsonResult> GetEmployeesGrossAmountByEmpId(int employeeInfoId)
        {
            decimal GrossAmount = 0;
            var data = await salaryService.GetEmployeesSalaryStructureByEmpId(employeeInfoId);
            decimal additions = data.Where(x => x.salaryHead.salaryHeadType == "Addition" && x.isActive == "Active").Sum(x => x.amount);
            decimal deductions = data.Where(x => x.salaryHead.salaryHeadType == "Deduction" && x.isActive == "Active").Sum(x => x.amount);
            GrossAmount = additions - deductions;
            return Json(GrossAmount);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmpStructureByEmpId(int employeeInfoId)
        {
            var data = await salaryService.GetEmpStructureByEmpId(employeeInfoId);
            return Json(data);
        }

        #endregion

        #region Structure History
        public IActionResult StructureHistory()
        {
            EmployeesSalaryStructureViewModel model = new EmployeesSalaryStructureViewModel
            {

            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetStructureHistoryByEmpId(int empId)
        {

            return Json(await salaryService.GetStructureHistoryByEmpId(empId));
        }

        #endregion

        #region Cash Setup
        public async Task<ActionResult> CashSetup()
        {
            EmployeesCashSetupViewModel model = new EmployeesCashSetupViewModel
            {
                employeesCashSetups = await salaryService.GetAllEmployeesCashSetup()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> CashSetup([FromForm] EmployeesCashSetupViewModel model)
        {
            int cashId = 0;

            var cashCheck = await salaryService.GetEmployeesCashSetupByEmployeeId(model.employeeInfoId);
            if (cashCheck.Count() > 0 && model.cashSetupId == 0)
            {
                cashId = 0;
            }
            else
            {
                EmployeesCashSetup data = new EmployeesCashSetup
                {
                    Id = model.cashSetupId,
                    employeeInfoId = model.employeeInfoId,
                    bankAmount = model.bankAmount,
                    cashAmount = model.cashAmount,
                    walletAmount = model.walletAmount
                };

                cashId = await salaryService.SaveEmployeesCashSetup(data);
            }

            return Json(cashId);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteEmployeesCashSetupById(int Id)
        {
            await salaryService.DeleteEmployeesCashSetupById(Id);
            return Json(true);
        }

        #endregion

        #region SalaryActivityProject
        public async Task<ActionResult> SalaryActivityPercent()
        {
            EmployeesSalaryActivityViewModel model = new EmployeesSalaryActivityViewModel
            {
                // employeeProjectActivities = await employeeProjectActivityService.GetEmployeeProjectActivity(),
                GetSalaryActivityPercents = await salaryService.GetsalaryActivityPercent()

            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalaryActivityPercent([FromForm] EmployeesSalaryActivityViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    model.slabIncomeTaxes = await salaryService.GetEmployeesSalaryStructureByEmpId(model.taxYearId);
            //    model.salaryGradesList = await salaryService.GetAllSalaryGrade();               
            //    return View(model);
            //}

            SalaryActivityPercent data = new SalaryActivityPercent
            {
                Id = model.salaryActivityPercentId,

                employeeInfoId = model.employeeInfoId,
                employeeProjectActivityId = model.employeeProjectActivityId,
                Amount = model.Amount


            };
            await salaryService.SavesalaryActivityPercent(data);


            return RedirectToAction("SalaryActivityPercent", "SalaryStructure", new { Area = "Payroll" });


        }
        #endregion

        #region API Section

        [Route("global/api/GetAllEmployess")]
        [HttpGet]
        public async Task<IActionResult> GetAllEmployess()
        {
            return Json(await personalInfoService.GetEmployeeInfo());
        }

        [Route("global/api/GetActiveAllEmployeeInfo")]
        [HttpGet]
        public async Task<IActionResult> GetActiveAllEmployeeInfo()
        {
            return Json(await personalInfoService.GetActiveAllEmployeeInfo());
        }

        [Route("global/api/GetAllEmployessbyId/{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetAllEmployessbyId(int Id)
        {
            return Json(await personalInfoService.GetEmployeeInfoById(Id));
        }

        [Route("global/api/GetAcitivyProject/{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetAcitivyProject(int Id)
        {
            return Json(await employeeProjectActivityService.GetEmployeeProjectActivityByEmpId(Id));
        }
        #endregion

    }
}