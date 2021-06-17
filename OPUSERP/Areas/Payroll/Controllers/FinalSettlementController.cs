using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Payroll.Models;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.RetirementAndTermination.Interface;
using OPUSERP.Payroll.Data.Entity.Salary;
using OPUSERP.Payroll.Services.FinalSettlements.Interfaces;
using OPUSERP.Payroll.Services.Salary.Interfaces;

namespace OPUSERP.Areas.Payroll.Controllers
{
    [Area("Payroll")]
    public class FinalSettlementController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string rootPath;
        private readonly MyPDF myPDF;
        private readonly IFinalSettlementService  finalSettlementService;
        private readonly IResignInformationService  resignInformationService;
        private readonly IERPCompanyService eRPCompanyService;
        private readonly ISalaryService salaryService;
        private readonly IPersonalInfoService personalInfoService;

        public string FileName;
        public FinalSettlementController(IHostingEnvironment hostingEnvironment, IConverter converter, IFinalSettlementService finalSettlementService, IPersonalInfoService personalInfoService, IResignInformationService resignInformationService, IERPCompanyService eRPCompanyService, ISalaryService salaryService)
        {
            this.finalSettlementService = finalSettlementService;
            this.resignInformationService = resignInformationService;
            this.eRPCompanyService = eRPCompanyService;
            this.personalInfoService = personalInfoService;
            this.salaryService = salaryService;
            this._hostingEnvironment = hostingEnvironment;
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        #region Final Settlement
        public async Task<ActionResult> Index()
        {
            FinalSettlementViewModel model = new FinalSettlementViewModel
            {
                finalSettlements = await finalSettlementService.GetAllFinalSettlements(),
                resigns=await resignInformationService.GetResignInfoForFinalSettlement(),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };
           
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] FinalSettlementViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.finalSettlements = await finalSettlementService.GetAllFinalSettlements();
                model.resigns = await resignInformationService.GetResignInfoForFinalSettlement();
                return View(model);
            }

            FinalSettlement data = new FinalSettlement
            {
                Id = model.finalSettlementId,
                employeeInfoId= model.employeeInfoId,
                seperationStatus = model.seperationStatus ,
                noticePeriod_Emt = model.noticePeriod_Emt ,
                noticePeriod_Served = model.noticePeriod_Served ,
                lastWorkingDay = model.lastWorkingDay ,
                salaryDisbUpto = model.salaryDisbUpto ,
                lenghtOfService = model.lenghtOfService ,
                leaveBalance = model.leaveBalance ,
                salDue = model.salDue ,
                specDue = model.specDue,
                carADue = model.carADue ,
                leaveEncashment = model.leaveEncashment ,
                fBonus1 = model.fBonus1 ,
                fBonus2 = model.fBonus2 ,
                fB1Remarks = model.fB1Remarks ,
                fB2Remarks = model.fB2Remarks ,
                perfBonus = model.perfBonus ,
                lFAAdjust = model.lFAAdjust ,
                mobilePhone = model.mobilePhone,
                mobileData = model.mobileData ,
                workingBeyendDays = model.workingBeyendDays ,
                daysToYear = model.daysToYear ,
                hardInstall = model.hardInstall ,
                advSal = model.advSal ,
                gratuity = model.gratuity ,
                lofSvsCG = model.lofSvsCG ,
                oDeduction1 = model.oDeduction1 ,
                oDeduction2 = model.oDeduction2 ,
                cODeduction1 = model.cODeduction1 ,
                cODeduction2 = model.cODeduction2 ,
                oAddition1 = model.oAddition1 ,
                oAddition2 = model.oAddition2 ,
                cOAddition1 = model.cOAddition1 ,
                cOAddition2 = model.cOAddition2 ,
                grossSalary = model.grossSalary ,
                basicSalary = model.basicSalary ,
                specialAllowance = model.specialAllowance ,
                carAllowance = model.carAllowance ,
                appDate = model.appDate ,
                trStatus = model.trStatus ,
                noticePeriodDeducion = model.noticePeriodDeducion ,
                incomeTax = model.incomeTax ,
                walletAmount = model.walletAmount ,
                bankAmount = model.bankAmount ,
                totalAmount = model.totalAmount ,
                pFAmount = model.pFAmount ,
                mobileAllowance = model.mobileAllowance,
                dailyAllowance = model.dailyAllowance ,
                internetAllowance = model.internetAllowance ,
                porataBonus = model.porataBonus,
                deductedBonus = model.deductedBonus ,
                totalDeduction = model.totalDeduction ,
                totalBenefit = model.totalBenefit ,
                totalOtherBenefit = model.totalOtherBenefit ,
                pFAmountInter = model.pFAmountInter ,
                deductedAmount = model.deductedAmount ,
                pFAmountFifty = model.pFAmountFifty ,
                pFAmountHund = model.pFAmountHund ,
                poratedBonusAmount = model.poratedBonusAmount ,
                lStatus = model.lStatus ,
                basicDue = model.basicDue ,
                houseRentDue = model.houseRentDue ,
                conveyanceDue = model.conveyanceDue ,
                medicalDue = model.medicalDue ,
                pFCompanyInter = model.pFCompanyInter ,
                carallowanceDeduction = model.carallowanceDeduction ,
                fixedallowanceDeduction = model.fixedallowanceDeduction ,
                mobileallowanceDeduction = model.mobileallowanceDeduction ,
                internetallowanceDeduction = model.internetallowanceDeduction ,
                pFLoan = model.pFLoan ,
                pFOthersadjustment = model.pFOthersadjustment ,
                noticePeriodRequired = model.noticePeriodRequired ,
                salaryadjustment = model.salaryadjustment ,
                wPPF = model.wPPF 
              };


            await finalSettlementService.SaveFinalSettlement(data);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> GetFinalSettlementDataByEmpId(int employeeInfoId)
        {
            return Json(await finalSettlementService.GetFinalSettlementDataByEmpId(employeeInfoId));
        }

        [AllowAnonymous]
        public IActionResult FinalSettlementPdfAction(int employeeInfoId, int salaryPeriodId)
        {

            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Payroll/FinalSettlement/FinalSettlePayslipReportPdf?employeeInfoId=" + employeeInfoId + "&salaryPeriodId=" + salaryPeriodId;

            string fileName;
            string status = myPDF.GeneratePDF(out fileName, url);
            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> FinalSettlementPdf(int employeeInfoId)
        {
          
            FinalSettlementViewModel model = new FinalSettlementViewModel
            {
                finalSettlementData = await finalSettlementService.GetFinalSettlementsByemployeeInfoId(employeeInfoId),
                finalSettlementDataViewModel = await finalSettlementService.GetFinalSettlementDataByEmpId(employeeInfoId),
                companies = await eRPCompanyService.GetAllCompany(),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };
            return PartialView(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> FinalSettlePayslipReportPdf(int employeeInfoId, int salaryPeriodId)
        {
            try
            {
                var model = new FinalSettlementViewModel
                {
                    payslipReportViewModels = await salaryService.GetPaySlipByEmpId(employeeInfoId, salaryPeriodId),
                    payslipAdditionViewModels = await salaryService.GetPaySlipAdditionByEmpId(employeeInfoId, salaryPeriodId),
                    payslipDeductionViewModels = await salaryService.GetPaySlipDeductionByEmpId(employeeInfoId, salaryPeriodId),
                    finalSettlementData = await finalSettlementService.GetFinalSettlementsByemployeeInfoId(employeeInfoId),
                    companies = await eRPCompanyService.GetAllCompany(),
                    visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
                };
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Final Settlement New

        public async Task<ActionResult> IndexNew()
        {
            FinalSettlementViewModel model = new FinalSettlementViewModel
            {
                finalSettlements = await finalSettlementService.GetAllFinalSettlements(),
                resigns = await resignInformationService.GetResignInfoForFinalSettlement(),
                salaryPeriods = await salaryService.GetAllSalaryPeriod(),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexNew([FromForm] FinalSettlementViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.finalSettlements = await finalSettlementService.GetAllFinalSettlements();
                model.resigns = await resignInformationService.GetResignInfoForFinalSettlement();
                return View(model);
            }

            FinalSettlement data = new FinalSettlement
            {
                Id = model.finalSettlementId,
                employeeInfoId = model.employeeInfoId,
                seperationStatus = model.seperationStatus,
                noticePeriod_Emt = model.noticePeriod_Emt,
                noticePeriod_Served = model.noticePeriod_Served,
                lastWorkingDay = model.lastWorkingDay,
                salaryDisbUpto = model.salaryDisbUpto,
                lenghtOfService = model.lenghtOfService,
                leaveBalance = model.leaveBalance,
                lofSvsCG = model.lofSvsCG,
                salaryPeriodId = model.salaryPeriodId,
                salDue = model.salDue
            };
            await finalSettlementService.SaveFinalSettlement(data);

            if (model.employeeInfoId > 0)
            {
                await salaryService.DeleteProcessEmpSalaryStructureByempId(Convert.ToInt32(model.employeeInfoId), Convert.ToInt32(model.salaryPeriodId));
            }
            for (int i = 0; i < model.headIdAll.Length; i++)
            {
                ProcessEmpSalaryStructure salaryAdd = new ProcessEmpSalaryStructure
                {
                    Id = 0,
                    employeeInfoId = Convert.ToInt32(model.employeeInfoId),
                    salaryPeriodId = Convert.ToInt32(model.salaryPeriodId),
                    salaryHeadId = Convert.ToInt32(model.headIdAll[i]),
                    amount = Convert.ToDecimal(model.amountAll[i])
                };
                await salaryService.SaveProcessEmpSalaryStructure(salaryAdd);
            }

            for (int i = 0; i < model.headIdDeductAll.Length; i++)
            {
                ProcessEmpSalaryStructure salaryAdd = new ProcessEmpSalaryStructure
                {
                    Id = 0,
                    employeeInfoId = Convert.ToInt32(model.employeeInfoId),
                    salaryPeriodId = Convert.ToInt32(model.salaryPeriodId),
                    salaryHeadId = Convert.ToInt32(model.headIdDeductAll[i]),
                    amount = Convert.ToDecimal(model.amountDeductAll[i])
                };
                await salaryService.SaveProcessEmpSalaryStructure(salaryAdd);
            }

            await salaryService.ProcessEmpSalaryMasterBySp(model.salaryPeriodId, model.employeeInfoId);

            return RedirectToAction(nameof(IndexNew));
        }

        [HttpGet]
        public async Task<IActionResult> GetAdditionHead(int employeeInfoId, int day)
        {
            var data = await salaryService.GetFsStructure(employeeInfoId, day);
            return Json(data.Where(x => x.salaryHeadType == "Addition"));
        }

        [HttpGet]
        public async Task<IActionResult> GetDeductionHead(int employeeInfoId, int day)
        {
            var data = await salaryService.GetFsStructure(employeeInfoId, day);
            return Json(data.Where(x => x.salaryHeadType == "Deduction"));
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeesSalaryAdditionByEmpId(int periodId,int employeeInfoId)
        {
            var data = await salaryService.GetProcessEmpSalaryStructureByemployeeInfoId(periodId, employeeInfoId);
            return Json(data.Where(x => x.salaryHead.salaryHeadType == "Addition"));
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployeesSalaryDeductionByEmpId(int periodId, int employeeInfoId)
        {
            var data = await salaryService.GetProcessEmpSalaryStructureByemployeeInfoId(periodId, employeeInfoId);
            return Json(data.Where(x => x.salaryHead.salaryHeadType == "Deduction"));
        }
        #endregion
    }
}