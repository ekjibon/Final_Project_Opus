using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Payroll.Models;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.Payroll.Data.Entity.Salary;
using OPUSERP.Payroll.Services.IncomeTax.Interfaces;
using OPUSERP.Payroll.Services.Salary.Interfaces;

namespace OPUSERP.Areas.Payroll.Controllers
{
    [Area("Payroll")]
    public class IncomeTaxSlabController : Controller
    {
        private readonly IIncomeTaxSlabService incomeTaxSlabService;
        private readonly ISalaryService salaryService;
        private readonly IPersonalInfoService personalInfoService;
        //private readonly IHostingEnvironment _hostingEnvironment;
        //  private readonly ITax personalInfoService;
        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public IncomeTaxSlabController(IHostingEnvironment hostingEnvironment, IConverter converter, IIncomeTaxSlabService incomeTaxSlabService, ISalaryService salaryService, IPersonalInfoService personalInfoService)
        {
            this.salaryService = salaryService;
            this.incomeTaxSlabService = incomeTaxSlabService;
            this.personalInfoService = personalInfoService;
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        #region IComeTaxSlab

        public async Task<ActionResult> Index()
        {
            IncomeTaxSlabViewModel model = new IncomeTaxSlabViewModel
            {
                slabIncomeTaxes = await incomeTaxSlabService.GetSlabIncomeTax(),
                taxYearsList = await salaryService.GetAllTaxYear(),
                slabTypes = await salaryService.GetAllSlabType()



            };

            return View(model);
        }
        public async Task<ActionResult> TaxSlabAssign()
        {
            IncomeTaxSlabViewModel model = new IncomeTaxSlabViewModel
            {
                // slabIncomeTaxes = await incomeTaxSlabService.GetSlabIncomeTax(),
                // taxYearsList = await salaryService.GetAllTaxYear(),
                slabTypes = await salaryService.GetAllSlabType(),
                slabIncomeTaxeAssigns = await incomeTaxSlabService.GetSlabIncomeTaxAssign()



            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaxSlabAssign([FromForm] IncomeTaxSlabViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    model.slabIncomeTaxes = await salaryService.GetEmployeesSalaryStructureByEmpId(model.taxYearId);
            //    model.salaryGradesList = await salaryService.GetAllSalaryGrade();               
            //    return View(model);
            //}

            SlabIncomeTaxAssign data = new SlabIncomeTaxAssign
            {
                Id = model.slabIncomeTaxId,
                slabTypeId = model.slabTypeId,
               employeeInfoId=model.employeeInfoId
            };
            await incomeTaxSlabService.SaveIncomeTaxSlabAssign(data);


            return RedirectToAction("TaxSlabAssign", "IncomeTaxSlab", new { Area = "Payroll" });


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] IncomeTaxSlabViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    model.slabIncomeTaxes = await salaryService.GetEmployeesSalaryStructureByEmpId(model.taxYearId);
            //    model.salaryGradesList = await salaryService.GetAllSalaryGrade();               
            //    return View(model);
            //}

            SlabIncomeTax data = new SlabIncomeTax
            {
                Id = model.slabIncomeTaxId,
                slabTypeId = model.slabTypeId,
                taxYearId = model.taxYearId,
                slabAmount = model.slabAmount,
                taxRate = model.taxRate,
                sortOrder = model.sortOrder,
                slabText = model.slabText
            };
            await incomeTaxSlabService.SaveIncomeTaxSlab(data);


            return RedirectToAction("Index", "IncomeTaxSlab", new { Area = "Payroll" });


        }

        #endregion
        #region taxslab
        public async Task<ActionResult> TaxSlab()
        {
            TaxSlabViewModel model = new TaxSlabViewModel
            {

                slabTypes = await salaryService.GetAllSlabType()



            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaxSlab([FromForm] TaxSlabViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    model.slabIncomeTaxes = await salaryService.GetEmployeesSalaryStructureByEmpId(model.taxYearId);
            //    model.salaryGradesList = await salaryService.GetAllSalaryGrade();               
            //    return View(model);
            //}

            SlabType data = new SlabType
            {
                Id = model.slabId,
                slabTypeName = model.slabTypeName,

            };
            await salaryService.SaveTaxSlab(data);


            return RedirectToAction("TaxSlab", "IncomeTaxSlab", new { Area = "Payroll" });


        }
        #endregion
        #region rebateslab
        public async Task<ActionResult> RebateSlab()
        {
            RebateSlabTypeViewModel model = new RebateSlabTypeViewModel
            {

                rebateSlabTypes = await salaryService.GetAllRebateSlabType()



            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RebateSlab([FromForm] RebateSlabTypeViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    model.slabIncomeTaxes = await salaryService.GetEmployeesSalaryStructureByEmpId(model.taxYearId);
            //    model.salaryGradesList = await salaryService.GetAllSalaryGrade();               
            //    return View(model);
            //}

            RebateSlabType data = new RebateSlabType
            {
                Id = model.rebateslabTypeId,
                slabTypeName = model.slabTypeName,
                minValue = model.minValue,
                maxValue = model.maxValue

            };
            await salaryService.SaveRebateSlab(data);


            return RedirectToAction("RebateSlab", "IncomeTaxSlab", new { Area = "Payroll" });


        }
        #endregion
        #region IncomeTaxSlab

        public async Task<ActionResult> Rebate()
        {
            IncomeTaxRebateViewModel model = new IncomeTaxRebateViewModel
            {
                slabRebates = await incomeTaxSlabService.GetSlabRebate(),
                taxYearsList = await salaryService.GetAllTaxYear(),
                rebateSlabTypes = await salaryService.GetAllRebateSlabType()



            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rebate([FromForm] IncomeTaxRebateViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    model.slabIncomeTaxes = await salaryService.GetEmployeesSalaryStructureByEmpId(model.taxYearId);
            //    model.salaryGradesList = await salaryService.GetAllSalaryGrade();               
            //    return View(model);
            //}

            SlabRebate data = new SlabRebate
            {
                Id = model.rebateIncomeTaxId,
                slabTypeId = model.slabTypeId,
                taxYearId = model.taxYearId,
                slabRebateAmount = model.slabRebateAmount,
                taxRate = model.taxRate,
                sortOrder = model.sortOrder,
                slabRebateText = model.slabRebateText
            };
            await incomeTaxSlabService.SaveRebateSlab(data);


            return RedirectToAction("Rebate", "IncomeTaxSlab", new { Area = "Payroll" });


        }

        #endregion
        #region IncomeTaxSetup

        public async Task<ActionResult> TaxSetup()
        {
            IncomeTaxSetupViewModel model = new IncomeTaxSetupViewModel
            {
                incomeTaxSetups = await incomeTaxSlabService.GetTaxSetup(),
                taxYearsList = await salaryService.GetAllTaxYear(),
                salaryHeads = await salaryService.GetAllSalaryHead()



            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaxSetup([FromForm] IncomeTaxSetupViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    model.slabIncomeTaxes = await salaryService.GetEmployeesSalaryStructureByEmpId(model.taxYearId);
            //    model.salaryGradesList = await salaryService.GetAllSalaryGrade();               
            //    return View(model);
            //}

            IncomeTaxSetup data = new IncomeTaxSetup
            {
                Id = model.IncomeTaxSettupId,
                salaryHeadId = model.salaryHeadId,
                taxYearId = model.taxYearId,
                exemption = model.exemption,
                exemptionAmount = model.exemptionAmount,
                sortOrder = model.sortOrder,
                exemptionPercent = model.exemptionPercent,
                exemptionMaxAmount = model.exemptionMaxAmount,
                exemptionRule = model.exemptionRule
            };
            await incomeTaxSlabService.SaveTaxSetup(data);


            return RedirectToAction("TaxSetup", "IncomeTaxSlab", new { Area = "Payroll" });


        }

        #endregion
        #region IncomeTaxChallan

        public async Task<ActionResult> TaxChallan()
        {
            IncomeTaxChallanViewModel model = new IncomeTaxChallanViewModel
            {
                taxChallans = await incomeTaxSlabService.GetTaxChallan(),
                salaryPeriods = await salaryService.GetAllSalaryPeriod(),




            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaxChallan([FromForm] IncomeTaxChallanViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    model.slabIncomeTaxes = await salaryService.GetEmployeesSalaryStructureByEmpId(model.taxYearId);
            //    model.salaryGradesList = await salaryService.GetAllSalaryGrade();               
            //    return View(model);
            //}

            TaxChallan data = new TaxChallan
            {
                Id = model.IncomeTaxchallanId,
                salaryPeriodId = model.periodId,
                taxChallanNo = model.challanNo,
                challanDate = model.challanDate,

            };
            await incomeTaxSlabService.SaveTaxChallan(data);


            return RedirectToAction("TaxChallan", "IncomeTaxSlab", new { Area = "Payroll" });


        }

        #endregion
        #region Additional Tax Info

        public async Task<ActionResult> AdditionalTax()
        {
            AdditionalTaxInfoViewModel model = new AdditionalTaxInfoViewModel
            {
                additionalTaxInfos = await incomeTaxSlabService.GetAdditionalTaxInfo(),
                taxYears = await salaryService.GetAllTaxYear(),
                employeeInfos = await personalInfoService.GetEmployeeInfo(),
                salaryHeads = await salaryService.GetAllSalaryHead()




            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdditionalTax([FromForm] AdditionalTaxInfoViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    model.slabIncomeTaxes = await salaryService.GetEmployeesSalaryStructureByEmpId(model.taxYearId);
            //    model.salaryGradesList = await salaryService.GetAllSalaryGrade();               
            //    return View(model);
            //}

            AdditionalTaxInfo data = new AdditionalTaxInfo
            {
                Id = model.AdditionalTaxInfoId,
                employeeInfoId = model.employeeInfoId,
                taxYearId = model.taxYearId,
                salaryHeadId = model.salaryHeadId,
                exemptionRule = model.exemptionRule,
                exemptionAmount = model.exemptionAmount

            };
            await incomeTaxSlabService.SaveAdditionalTax(data);


            return RedirectToAction("AdditionalTax", "IncomeTaxSlab", new { Area = "Payroll" });


        }

        #endregion
        #region Tax Process

        public async Task<ActionResult> TaxProcess()
        {


            IncomeTaxProcessViewModel model = new IncomeTaxProcessViewModel
            {

                employeeInfos = await personalInfoService.GetEmployeeInfo(),
                salaryPeriods = await salaryService.GetAllSalaryPeriod(),
                taxYearlist = await salaryService.GetAllTaxYear(),
                employeesTaxes = await salaryService.GetAllEmployeesTax()




            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> calculatealltax(int salaryperiod)
        {
            await salaryService.TaxCalculateforall(salaryperiod);

            return Json(1);
        }

        [HttpGet]
        public async Task<IActionResult> processAllTax(int id)
        {
            await salaryService.UpdateTaxInSalaryall(id);

            return Json(1);
        }
        [Route("global/api/processsingleTax/{salaryperiod}/{employeeId}/{id}")]
        [HttpGet]
        public async Task<IActionResult> processsingleTax(int salaryperiod,int employeeId, int id)
        {
            await salaryService.UpdateTaxInSalary(salaryperiod,employeeId,id);

            return Json(1);
        }
        [HttpGet]
        public async Task<IActionResult> processAllTaxStruc()
        {
            await salaryService.UpdateTaxInSalaryallstruc();

            return Json(1);
        }
        [Route("global/api/processsingleTaxStruc/{employeeId}/{id}")]
        [HttpGet]
        public async Task<IActionResult> processsingleTaxStruc(int employeeId, int id)
        {
            await salaryService.UpdateTaxInstruc(employeeId, id);

            return Json(1);
        }
        public async Task<ActionResult> TaxCalculation()
        {
           
           
            IncomeTaxProcessViewModel model = new IncomeTaxProcessViewModel
            {
               
                employeeInfos = await personalInfoService.GetEmployeeInfo(),
                salaryPeriods = await salaryService.GetAllSalaryPeriod(),
                taxYearlist = await salaryService.GetAllTaxYear()




            };
           
            return View(model);
        }
        //public async Task<ActionResult> StrucTaxProcess()
        //{


           

        //    return View();
        //}
        public async Task<ActionResult> StrucTaxProcess()
        {


            IncomeTaxProcessViewModel model = new IncomeTaxProcessViewModel
            {

                //employeeInfos = await personalInfoService.GetEmployeeInfo(),
                //salaryPeriods = await salaryService.GetAllSalaryPeriod(),
                //taxYearlist = await salaryService.GetAllTaxYear(),
                employeesTaxes = await salaryService.GetAllEmployeesTax()




            };

            return View(model);
        }
        public async Task<ActionResult> StrucTaxProcessPDF()
        {


            IncomeTaxProcessViewModel model = new IncomeTaxProcessViewModel
            {

                //employeeInfos = await personalInfoService.GetEmployeeInfo(),
                //salaryPeriods = await salaryService.GetAllSalaryPeriod(),
                //taxYearlist = await salaryService.GetAllTaxYear(),
                employeesTaxes = await salaryService.GetAllEmployeesTax()




            };

            return View(model);
        }
        [AllowAnonymous]
        public IActionResult StrucTaxExcelPdfAction(int AgreementId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Payroll/IncomeTaxSlab/StrucTaxProcessPDF";

            string fileName;
            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }


            string pdfpath = rootPath + @"\wwwroot\pdf\" + fileName;
            string wordpath = fileName.Replace(".pdf", ".xls");
            SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
            f.OpenPdf(rootPath + @"\wwwroot\pdf\" + fileName);
            f.ToExcel(rootPath + @"\wwwroot\pdf\" + wordpath);
            //  var stream = new FileStream(rootPath + "/wwwroot/pdf/" + wordpath, FileMode.Open);
            var model = wordpath;
            return Json(model);
        }
        [AllowAnonymous]
        public IActionResult StrucTaxPdfAction()
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Payroll/IncomeTaxSlab/StrucTaxProcessPDF";

            string fileName;
            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaxProcess([FromForm] IncomeTaxProcessViewModel model)
        {


            await incomeTaxSlabService.taxProcessNew(model.salaryPeriodId,model.employeeInfoId);

          
            return RedirectToAction("TaxProcess", "IncomeTaxSlab", new {id=1, Area = "Payroll" });


        }

        #endregion

        #region Additional Tax Info

        public async Task<ActionResult> RebateSetting()
        {
            InvestmentRebateSettingViewModel model = new InvestmentRebateSettingViewModel
            {
                InvestmentRebateSettings = await salaryService.GetAllRebateSetting(),
                taxYears = await salaryService.GetAllTaxYear(),
              




            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RebateSetting([FromForm] InvestmentRebateSettingViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    model.slabIncomeTaxes = await salaryService.GetEmployeesSalaryStructureByEmpId(model.taxYearId);
            //    model.salaryGradesList = await salaryService.GetAllSalaryGrade();               
            //    return View(model);
            //}

            InvestmentRebateSettings data = new InvestmentRebateSettings
            {
                Id = model.investmentRebateSettingId,
               
                taxYearId = model.taxYearId,
                allowableInvestment = model.allowableInvestment,
                investmentRebate = model.investmentRebate,
                orInvestmentRebate = model.orInvestmentRebate

            };
            await salaryService.SaveRebateSetting(data);


            return RedirectToAction("RebateSetting", "IncomeTaxSlab", new { Area = "Payroll" });


        }

        #endregion
        #region Reportformat
        public async Task<ActionResult> ReportFormat()
        {
            ReportFormatViewModel model = new ReportFormatViewModel
            {
                reportFormats = await salaryService.GetReportFormat()

            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReportFormat([FromForm] ReportFormatViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    model.slabIncomeTaxes = await salaryService.GetEmployeesSalaryStructureByEmpId(model.taxYearId);
            //    model.salaryGradesList = await salaryService.GetAllSalaryGrade();               
            //    return View(model);
            //}
            var predata = await salaryService.GetReportFormatByReportType(model.reportTypeName);
            if(predata.Count()>0)
            { 
            await salaryService.DeleteformatById(model.reportTypeName);
            }
            ReportFormat data = new ReportFormat
            {
                Id = 0,

                reportFormat = model.reportFormat,
                reportTypeName = model.reportTypeName,
              

            };
            await salaryService.SaveReportFormat(data);


            return RedirectToAction("ReportFormat", "IncomeTaxSlab", new { Area = "Payroll" });


        }
        #endregion


    }
}