using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.Payroll.Services.Salary.Interfaces;
using System;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Authorize]
    [Area("HRPMSEmployee")]
    public class BankInfoController : Controller
    {
        private readonly LangGenerate<BankLn> _lang;
        private readonly IBankInfoService bankInfoService;
        private readonly IPhotographService photographService;
        private readonly IWagesBankInfoService wagesBankInfoService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IWagesPersonalInfoService wagesPersonalInfoService;
        private readonly IBankBranchService bankBranchService;
        private readonly ISalaryService salaryService;

        public BankInfoController(IHostingEnvironment hostingEnvironment, IPhotographService photographService, IPersonalInfoService personalInfoService, IBankInfoService bankInfoService, IWagesBankInfoService wagesBankInfoService, IWagesPersonalInfoService wagesPersonalInfoService, IBankBranchService bankBranchService, ISalaryService salaryService)
        {
            _lang = new LangGenerate<BankLn>(hostingEnvironment.ContentRootPath);
            this.bankInfoService = bankInfoService;
            this.photographService = photographService;
            this.personalInfoService = personalInfoService;
            this.wagesBankInfoService = wagesBankInfoService;
            this.wagesPersonalInfoService = wagesPersonalInfoService;
            this.bankBranchService = bankBranchService;
            this.salaryService = salaryService;
        }

        #region Employee Bank Info
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new BankViewModel
            {
                employeeID = id.ToString(),
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                fLang = _lang.PerseLang("Employee/BankEN.json", "Employee/BankBN.json", Request.Cookies["lang"]),
                bankInfos = await bankInfoService.GetBankInfoByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
                banks = await bankBranchService.GetAllBank(),
                walletTypes = await salaryService.GetAllWalletType()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] BankViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.fLang = _lang.PerseLang("Employee/BankEN.json", "Employee/BankBN.json", Request.Cookies["lang"]);
                model.bankInfos = await bankInfoService.GetBankInfoByEmpId(Int32.Parse(model.employeeID));
                model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(Int32.Parse(model.employeeID));
                return View(model);
            }

            BankInfo data = new BankInfo
            {
                Id = model.bankInfoId,
                employeeId = Int32.Parse(model.employeeID),
                walletTypeId = model.walletTypeId,
                bankId = model.bankId,
                branchName = model.branchName,
                accountNumber = model.accountNumber,
                walletNumber = model.walletNumber,
                ibus = model.ibus
            };

            await bankInfoService.SaveBankInfo(data);
            await personalInfoService.UpdateEmployeeinfoById(Int32.Parse(model.employeeID));
            return RedirectToAction("Index", "BankInfo", new
            {
                id = model.employeeID
            });
        }

        public async Task<IActionResult> Delete(int id, int empId)
        {
            await bankInfoService.DeleteBankInfoById(id);
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "BankInfo", new
            {
                id = empId
            });
        }

        #endregion

        #region Employee Wages Bank Info
        public async Task<IActionResult> WagesIndex(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new BankViewModel
            {
                employeeID = id.ToString(),
                fLang = _lang.PerseLang("Employee/BankEN.json", "Employee/BankBN.json", Request.Cookies["lang"]),
                wagesBankInfos = await wagesBankInfoService.GetBankInfoByEmpId(id),
                employeeNameCode = await wagesPersonalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WagesIndex([FromForm] BankViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.fLang = _lang.PerseLang("Employee/BankEN.json", "Employee/BankBN.json", Request.Cookies["lang"]);
                model.wagesBankInfos = await wagesBankInfoService.GetBankInfoByEmpId(Int32.Parse(model.employeeID));
                model.employeeNameCode = await wagesPersonalInfoService.GetEmployeeNameCodeById(Int32.Parse(model.employeeID));
                return View(model);
            }

            WagesBankInfo data = new WagesBankInfo
            {
                Id = model.bankInfoId,
                employeeId = Int32.Parse(model.employeeID),
                bankName = model.bankName,
                branchName = model.branchName,
                accountNumber = model.accountNumber,
                ibus = model.ibus
            };

            await wagesBankInfoService.SaveBankInfo(data);
            await wagesPersonalInfoService.UpdateEmployeeinfoById(Int32.Parse(model.employeeID));
            return RedirectToAction("WagesIndex", "BankInfo", new
            {
                id = model.employeeID
            });
        }

        public async Task<IActionResult> WagesDelete(int id, int empId)
        {
            await wagesBankInfoService.DeleteBankInfoById(id);
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("WagesIndex", "BankInfo", new
            {
                id = empId
            });
        }
        #endregion
    }
}