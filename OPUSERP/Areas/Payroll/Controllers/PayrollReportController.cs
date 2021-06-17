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
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.Payroll.Data.Entity.PF;
using OPUSERP.Payroll.Services.Salary.Interfaces;


namespace OPUSERP.Areas.Payroll.Controllers
{
    [Area("Payroll")]
    public class PayrollReportController : Controller
    {
        private readonly ISalaryService salaryService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly ISpecialBranchUnitService specialBranchUnitService;
        private readonly IERPCompanyService eRPCompanyService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserInfoes userInfo;
        private readonly OPUSERP.ERPServices.MasterData.Interfaces.IDesignationDepartmentService designationDepartmentService;

        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;

        public PayrollReportController(ISalaryService salaryService, IUserInfoes userInfo, IPersonalInfoService personalInfoService, ISpecialBranchUnitService specialBranchUnitService, IERPCompanyService eRPCompanyService, IHostingEnvironment _hostingEnvironment, IConverter converter, OPUSERP.ERPServices.MasterData.Interfaces.IDesignationDepartmentService designationDepartmentService)
        {
            this.salaryService = salaryService;
            this.personalInfoService = personalInfoService;
            this.specialBranchUnitService = specialBranchUnitService;
            this.eRPCompanyService = eRPCompanyService;
            this.designationDepartmentService = designationDepartmentService;
            this.userInfo = userInfo;

            this._hostingEnvironment = _hostingEnvironment;
            myPDF = new MyPDF(_hostingEnvironment, converter);
            rootPath = _hostingEnvironment.ContentRootPath;
        }

        #region Gratuity

        //public async Task<ActionResult> GratuityView()
        public ActionResult GratuityView()
        {
            return View();
        }

        public ActionResult GratuityProcessedData()
        {
            ViewBag.datelist = salaryService.GetAllGratiutyProcessDataDates();
            return View();
        }

        public async Task<ActionResult> GratuityProcessed(DateTime datee)
        {
            var data = await salaryService.GetGratuityReport();
            foreach(var list in data)
            {
                var employee = await personalInfoService.GetEmployeeInfoByCode(list.employeeCode);
                GratiutyProcessData gratiutyProcessData = new GratiutyProcessData
                {
                    employeeInfoId = employee.Id,
                    designation = list.designation,
                    date = datee,
                    basic = list.basicAmount,
                    year = list.fractionalYear,
                    gratuity = list.gratuityAmount
                };
                await salaryService.SaveGratiutyProcessData(gratiutyProcessData);
            }

            return RedirectToAction(nameof(GratuityProcessedData));
        }

        [AllowAnonymous]
        [Route("global/api/GetGratuityData")]
        [HttpGet]
        public async Task<IActionResult> GetGratuityData()
        {
            return Json(await salaryService.GetGratuityReport());
        }

        [AllowAnonymous]
        [Route("global/api/GetAllGratuityReportViewModel")]
        [HttpGet]
        public async Task<IActionResult> GetAllGratuityReportViewModel()
        {
            return Json(await salaryService.GetAllGratuityReportViewModel());
        }

        [AllowAnonymous]
        [Route("global/api/GetAllGratuityReportViewModelByDate/{date}")]
        [HttpGet]
        public async Task<IActionResult> GetAllGratuityReportViewModelByDate(DateTime datee)
        {
            return Json(await salaryService.GetAllGratuityReportViewModelByDate(datee));
        }

        #endregion

        #region Reconciliation

        public async Task<ActionResult> Reconciliation()
        {
            PayrollReportViewModel model = new PayrollReportViewModel
            {
                salaryPeriods = await salaryService.GetAllSalaryPeriod()
            };

            return View(model);
        }

        #endregion

        #region Report
        public async Task<ActionResult> Index()
        {
            PayrollReportViewModel model = new PayrollReportViewModel
            {
                salaryPeriods = await salaryService.GetAllSalaryPeriod(),
                specialBranchUnits = await specialBranchUnitService.GetSpecialBranchUnit(),
                taxYears=await salaryService.GetAllTaxYear()
            };

            return View(model);
        }

        public async Task<ActionResult> PayslipView()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            ViewBag.EmployeeId = userInfos.EmployeeId;

            PayrollReportViewModel model = new PayrollReportViewModel
            {
                salaryPeriods = await salaryService.GetAllSalaryPeriod(),
                specialBranchUnits = await specialBranchUnitService.GetSpecialBranchUnit(),
                taxYears = await salaryService.GetAllTaxYear()
            };

            return View(model);
        }

        public async Task<ActionResult> TaxCertificateView()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            ViewBag.EmployeeId = userInfos.EmployeeId;

            PayrollReportViewModel model = new PayrollReportViewModel
            {
                taxYears = await salaryService.GetAllTaxYear()
            };

            return View(model);
        }

        public async Task<ActionResult> WagesIndex()
        {
            PayrollReportViewModel model = new PayrollReportViewModel
            {
                salaryPeriods = await salaryService.GetAllSalaryPeriod(),
                specialBranchUnits = await specialBranchUnitService.GetSpecialBranchUnit(),
                taxYears = await salaryService.GetAllTaxYear()
            };

            return View(model);
        }

        #endregion

        #region Grameen Report
        public async Task<ActionResult> IndexNew()
        {
            PayrollReportViewModel model = new PayrollReportViewModel
            {
                Departments = await designationDepartmentService.GetDepartment(),
                salaryPeriods = await salaryService.GetAllSalaryPeriod(),
                specialBranchUnits = await specialBranchUnitService.GetSpecialBranchUnit(),
                taxYears = await salaryService.GetAllTaxYear()
            };

            return View(model);
        }

        public async Task<IActionResult> DepartamentWiseReport(string rptType, int? sbuId, int? pnsId, int salaryPeriodId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "MWSS" || rptType == "MWSSBWC" || rptType == "MWBS")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/Payroll/PayrollReport/MonthlySalaryReportPdf_Grammen_Dept" + "?salaryPeriodId=" + salaryPeriodId + "&sbuId=" + sbuId + "&pnsId=" + pnsId;
                //url = scheme + "://" + host + "/Payroll/PayrollReport/" + data.FirstOrDefault().reportFormat + "?salaryPeriodId=" + salaryPeriodId + "&sbuId=" + sbuId + "&pnsId=" + pnsId;
                //url = scheme + "://" + host + "/Payroll/PayrollReport/MonthlySalaryReportPdf_RedCross?salaryPeriodId=" + salaryPeriodId;
            }

            string status = myPDF.GenerateLandscapePDF_WithPad(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        public async Task<IActionResult> MonthlySalaryReportPdf_Grammen_Dept(int salaryPeriodId, int? sbuId, int? pnsId)
        {
            try
            {
                var model = new PayrollReportViewModel
                {
                    monthlySalaryReportViewModels = await salaryService.GetMonthlySalaryReportByPeriodId(salaryPeriodId, sbuId, pnsId),
                    companies = await eRPCompanyService.GetAllCompany(),
                };
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Report SubMonthlySalaryReportView

        [AllowAnonymous]
        public async Task<IActionResult> SubMonthlySalaryReportView(int salaryPeriodId, int? sbuId, int? pnsId)
        {
            try
            {
                var model = new PayrollReportViewModel
                {
                    monthlySalaryReportViewModels = await salaryService.GetMonthlySalaryReportByPeriodId(salaryPeriodId, sbuId, pnsId),
                    companies = await eRPCompanyService.GetAllCompany(),
                };
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region PDF Mehod
        [AllowAnonymous]
        public async Task<IActionResult> SalaryReport(string rptType, int employeeInfoId, int salaryPeriodId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;           

            if (rptType == "PSLIP")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/Payroll/PayrollReport/"+data.FirstOrDefault().reportFormat+"?employeeInfoId=" + employeeInfoId + "&salaryPeriodId=" + salaryPeriodId;
            }           

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
        public async Task<IActionResult> WagesSalaryReport(string rptType, int employeeInfoId, int salaryPeriodId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "PSLIP")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/Payroll/PayrollReport/WagesPayslipReportPdf?employeeInfoId=" + employeeInfoId + "&salaryPeriodId=" + salaryPeriodId;
            }

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
        public async Task<IActionResult> WagesMonthlySalaryReport(string rptType, int? sbuId, int? pnsId, int salaryPeriodId)
        {

            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "MWSS")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/Payroll/PayrollReport/WagesMonthlySalaryReportPdf?salaryPeriodId=" + salaryPeriodId + "&sbuId=" + sbuId + "&pnsId=" + pnsId;
                //url = scheme + "://" + host + "/Payroll/PayrollReport/MonthlySalaryReportPdf_RedCross?salaryPeriodId=" + salaryPeriodId;
            }

            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> MonthlySalaryReport(string rptType, int? sbuId, int? pnsId, int salaryPeriodId)
        {

            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;
            
            if (rptType == "MWSS" || rptType == "MWSSBWC" || rptType == "MWBS")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/Payroll/PayrollReport/" + data.FirstOrDefault().reportFormat + "?salaryPeriodId=" + salaryPeriodId + "&sbuId=" + sbuId + "&pnsId=" + pnsId;
                //url = scheme + "://" + host + "/Payroll/PayrollReport/MonthlySalaryReportPdf_RedCross?salaryPeriodId=" + salaryPeriodId;
            }

            string status = myPDF.GenerateLandscapePDF_WithPad(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> BankStatementReport(string rptType, int? sbuId, int? pnsId, int salaryPeriodId)
        {

            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "BNKSTMNT" || rptType == "WALLSTMNT" || rptType == "CASHSTMNT")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/Payroll/PayrollReport/" + data.FirstOrDefault().reportFormat + "?salaryPeriodId=" + salaryPeriodId + "&sbuId=" + sbuId + "&pnsId=" + pnsId;                
            }            

            string status = myPDF.GeneratePDF_WithPAD(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> BankStatementReport_PAD(string rptType, int? sbuId, int? pnsId, int salaryPeriodId)
        {

            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "BNKSTMNTWTHOUTPAD" || rptType == "BNKAPP")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/Payroll/PayrollReport/" + data.FirstOrDefault().reportFormat + "?salaryPeriodId=" + salaryPeriodId + "&sbuId=" + sbuId + "&pnsId=" + pnsId;
            }

            string status = myPDF.GeneratePDF_WithoutPAD(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> BankStatementReportExcel(string rptType, int? sbuId, int? pnsId, int salaryPeriodId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "BNKSTMNT" || rptType == "BNKSTMNTWTHOUTPAD" || rptType == "BNKAPP" || rptType == "WALLSTMNT" || rptType == "CASHSTMNT")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/Payroll/PayrollReport/" + data.FirstOrDefault().reportFormat + "?salaryPeriodId=" + salaryPeriodId + "&sbuId=" + sbuId + "&pnsId=" + pnsId;
            }

            string status = myPDF.GeneratePDF(out fileName, url);
            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            string pdfpath = rootPath + @"\wwwroot\pdf\" + fileName;
            //string wordpath = fileName.Replace(".pdf", ".xls");
            string wordpath = fileName.Replace(".pdf", ".csv");
            SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
            f.OpenPdf(rootPath + @"\wwwroot\pdf\" + fileName);
            f.ToExcel(rootPath + @"\wwwroot\pdf\" + wordpath);
            var model = wordpath;
            return Json(model);
        }

        // IFRC SUB OFFICE REPORT

        [AllowAnonymous]
        public async Task<IActionResult> SubMonthlySalaryReportPdf(string rptType, int? sbuId, int? pnsId, int salaryPeriodId)
        {

            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "MWSS789")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/Payroll/PayrollReport/" + data.FirstOrDefault().reportFormat + "?salaryPeriodId=" + salaryPeriodId + "&sbuId=" + sbuId + "&pnsId=" + pnsId;
                //url = scheme + "://" + host + "/Payroll/PayrollReport/MonthlySalaryReportPdf_RedCross?salaryPeriodId=" + salaryPeriodId;
            }

            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

        [AllowAnonymous]
        public async Task<IActionResult> PayslipReportPdf(int employeeInfoId, int salaryPeriodId)
        {
            try
            {
                var model = new PayrollReportViewModel
                {
                    payslipReportViewModels = await salaryService.GetPaySlipByEmpId(employeeInfoId, salaryPeriodId),
                    payslipAdditionViewModels = await salaryService.GetPaySlipAdditionByEmpId(employeeInfoId, salaryPeriodId),
                    payslipDeductionViewModels = await salaryService.GetPaySlipDeductionByEmpId(employeeInfoId, salaryPeriodId),
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


        [AllowAnonymous]
        public async Task<IActionResult> WagesPayslipReportPdf(int employeeInfoId, int salaryPeriodId)
        {
            try
            {
                var model = new PayrollReportViewModel
                {
                    payslipReportViewModels = await salaryService.GetWagesPaySlipByEmpId(employeeInfoId, salaryPeriodId),
                    payslipAdditionViewModels = await salaryService.GetWagesPaySlipAdditionByEmpId(employeeInfoId, salaryPeriodId),
                    payslipDeductionViewModels = await salaryService.GetWagesPaySlipDeductionByEmpId(employeeInfoId, salaryPeriodId),
                    companies = await eRPCompanyService.GetAllCompany(),
                };
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [AllowAnonymous]
        public async Task<IActionResult> PayslipReportPdf_RedCross(int employeeInfoId, int salaryPeriodId)
        {
            try
            {
                var model = new PayrollReportViewModel
                {
                    payslipReportViewModels = await salaryService.GetPaySlipByEmpId(employeeInfoId, salaryPeriodId),
                    payslipAdditionViewModels = await salaryService.GetPaySlipAdditionByEmpId(employeeInfoId, salaryPeriodId),
                    payslipDeductionViewModels = await salaryService.GetPaySlipDeductionByEmpId(employeeInfoId, salaryPeriodId),
                    companies = await eRPCompanyService.GetAllCompany(),
                };
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [AllowAnonymous]
        public async Task<IActionResult> UniversalCodePdfAction(string rptType, int employeeInfoId, int salaryPeriodId)
        {

            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "UCP")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/Payroll/PayrollReport/" + data.FirstOrDefault().reportFormat + "?employeeInfoId=" + employeeInfoId + "&salaryPeriodId=" + salaryPeriodId;
                //url = scheme + "://" + host + "/Payroll/PayrollReport/MonthlySalaryReportPdf_RedCross?salaryPeriodId=" + salaryPeriodId;
            }

            string status = myPDF.GenerateLandscapePDF_A3(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        #endregion

        #region Monthly Salary Report

        [AllowAnonymous]
        public async Task<IActionResult> UniversalCodePdf(int employeeInfoId, int salaryPeriodId)
        {
            try
            {
                var model = new PayrollReportViewModel
                {
                    salaryPeriods=await salaryService.GetSalaryPeriodById(salaryPeriodId),
                    universalCodaXLTempleteViewModels = await salaryService.GetUniversalCodaXLTempleteViewModels(salaryPeriodId,employeeInfoId),
                    companies = await eRPCompanyService.GetAllCompany(),
                };
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> MonthlySalaryReportPdf(int salaryPeriodId, int? sbuId, int? pnsId)
        {
            try
            {
                var model = new PayrollReportViewModel
                {
                    monthlySalaryReportViewModels = await salaryService.GetMonthlySalaryReportByPeriodId(salaryPeriodId, sbuId, pnsId),
                    companies = await eRPCompanyService.GetAllCompany(),
                };
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> WagesMonthlySalaryReportPdf(int salaryPeriodId, int? sbuId, int? pnsId)
        {
            try
            {
                var model = new PayrollReportViewModel
                {
                    monthlySalaryReportViewModels = await salaryService.GetWagesMonthlySalaryReportByPeriodId(salaryPeriodId, sbuId, pnsId),
                    companies = await eRPCompanyService.GetAllCompany(),
                };
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [AllowAnonymous]
        public async Task<IActionResult> MonthlySalaryReportPdf_RedCross(int salaryPeriodId, int? sbuId, int? pnsId)
        {
            try
            {
                var model = new PayrollReportViewModel
                {
                    monthlySalaryReportViewModels = await salaryService.GetMonthlySalaryReportByPeriodId(salaryPeriodId, sbuId, pnsId),
                    companies = await eRPCompanyService.GetAllCompany(),
                };
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> MonthlySalaryReportPdf_BnB(int salaryPeriodId, int? sbuId, int? pnsId)
        {
            try
            {
                var model = new PayrollReportViewModel
                {
                    monthlySalaryReportViewModels = await salaryService.GetMonthlySalaryReportByPeriodId(salaryPeriodId, sbuId, pnsId),
                    companies = await eRPCompanyService.GetAllCompany(),
                };
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> MonthlySalaryReportPdf_BnB_V2(int salaryPeriodId, int? sbuId, int? pnsId)
        {
            try
            {
                var model = new PayrollReportViewModel
                {
                    monthlySalaryReportViewModels = await salaryService.GetMonthlySalaryReportByPeriodId(salaryPeriodId, sbuId, pnsId),
                    companies = await eRPCompanyService.GetAllCompany(),
                };
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> MonthlySalaryReportPdf_BWC_BnB(int salaryPeriodId, int? sbuId, int? pnsId)
        {
            try
            {
                var model = new PayrollReportViewModel
                {
                    monthlySalaryReportViewModels = await salaryService.GetMonthlySalaryReportByPeriodId(salaryPeriodId, sbuId, pnsId),
                    companies = await eRPCompanyService.GetAllCompany(),
                };
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> MonthlySalaryReportPdf_Brac(int salaryPeriodId, int? sbuId, int? pnsId)
        {
            try
            {
                var model = new PayrollReportViewModel
                {
                    monthlySalaryReportViewModels = await salaryService.GetMonthlySalaryReportByPeriodId(salaryPeriodId, sbuId, pnsId),
                    companies = await eRPCompanyService.GetAllCompany(),
                };
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> MonthlySalaryReportPdf_DbSchenker(int salaryPeriodId, int? sbuId, int? pnsId)
        {
            try
            {
                var model = new PayrollReportViewModel
                {
                    monthlySalaryReportViewModels = await salaryService.GetMonthlySalaryReportByPeriodId(salaryPeriodId, sbuId, pnsId),
                    companies = await eRPCompanyService.GetAllCompany(),
                };
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> BankStatementReportPdf_BnB(int salaryPeriodId, int? sbuId, int? pnsId)
        {
            try
            {
                var model = new PayrollReportViewModel
                {
                    bankStatementReportViewModels = await salaryService.GetBankStatementByPeriodId(salaryPeriodId, sbuId, pnsId),
                    companies = await eRPCompanyService.GetAllCompany(),
                };
                string Val = model.bankStatementReportViewModels.Where(a => a.bankPayable != 0 && a.bankAccountNo != "").Sum(x => x.bankPayable).ToString();
                ViewBag.InWord = AmountInWord.ConvertToWords(Val);

                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> BankStatementReportPdf_BnB_WithoutPad(int salaryPeriodId, int? sbuId, int? pnsId)
        {
            try
            {
                var model = new PayrollReportViewModel
                {
                    bankStatementReportViewModels = await salaryService.GetBankStatementByPeriodId(salaryPeriodId, sbuId, pnsId),
                    companies = await eRPCompanyService.GetAllCompany(),
                };
                string Val = model.bankStatementReportViewModels.Where(a => a.bankPayable != 0 && a.bankAccountNo != "").Sum(x => x.bankPayable).ToString();
                ViewBag.InWord = AmountInWord.ConvertToWords(Val);

                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> BankApplicationReportPdf_BnB(int salaryPeriodId, int? sbuId, int? pnsId)
        {
            try
            {
                var model = new PayrollReportViewModel
                {
                    bankStatementReportViewModels = await salaryService.GetBankStatementByPeriodId(salaryPeriodId, sbuId, pnsId),
                    companies = await eRPCompanyService.GetAllCompany(),
                };
                string Val = model.bankStatementReportViewModels.Where(a => a.bankPayable != 0 && a.bankAccountNo != "").Sum(x => x.bankPayable).ToString();
                ViewBag.InWord = AmountInWord.ConvertToWords(Val);

                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> WalletStatementReportPdf_BnB(int salaryPeriodId, int? sbuId, int? pnsId)
        {
            try
            {
                var model = new PayrollReportViewModel
                {
                    bankStatementReportViewModels = await salaryService.GetBankStatementByPeriodId(salaryPeriodId, sbuId, pnsId),
                    companies = await eRPCompanyService.GetAllCompany(),
                };
                string Val = model.bankStatementReportViewModels.Sum(x => x.walletPayable).ToString();
                ViewBag.InWord = AmountInWord.ConvertToWords(Val);

                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> CashStatementReportPdf_BnB(int salaryPeriodId, int? sbuId, int? pnsId)
        {
            try
            {
                var model = new PayrollReportViewModel
                {
                    bankStatementReportViewModels = await salaryService.GetBankStatementByPeriodId(salaryPeriodId, sbuId, pnsId),
                    companies = await eRPCompanyService.GetAllCompany(),
                };
                string Val = model.bankStatementReportViewModels.Sum(x => x.cashPayable).ToString();
                ViewBag.InWord = AmountInWord.ConvertToWords(Val);

                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [AllowAnonymous]
        public async Task<IActionResult> MonthlySalaryReportPdf_Priyojon(int salaryPeriodId, int? sbuId, int? pnsId)
        {
            try
            {
                var model = new PayrollReportViewModel
                {
                    monthlySalaryReportViewModels = await salaryService.GetMonthlySalaryReportByPeriodId(salaryPeriodId, sbuId, pnsId),
                    companies = await eRPCompanyService.GetAllCompany(),
                };
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> MonthlySalaryReportPdf_Opus(int salaryPeriodId, int? sbuId, int? pnsId)
        {
            try
            {
                var model = new PayrollReportViewModel
                {
                    monthlySalaryReportViewModels = await salaryService.GetMonthlySalaryReportByPeriodId(salaryPeriodId, sbuId, pnsId),
                    companies = await eRPCompanyService.GetAllCompany(),
                };
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Bonus Report

        [AllowAnonymous]
        public async Task<IActionResult> MonthlyBonusReportPdf_BnB(int salaryPeriodId, int? sbuId, int? pnsId)
        {
            try
            {
                var model = new PayrollReportViewModel
                {
                    monthlySalaryReportViewModels = await salaryService.GetMonthlySalaryReportByPeriodId(salaryPeriodId, sbuId, pnsId),
                    companies = await eRPCompanyService.GetAllCompany(),
                };
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Tax Report

        [AllowAnonymous]
        public async Task<IActionResult> FinalTaxCertificateReportPdfAction(string rptType, int employeeInfoId, int taxYearId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "FTAXCERT")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/Payroll/PayrollReport/" + data.FirstOrDefault().reportFormat + "?employeeInfoId=" + employeeInfoId + "&taxYearId=" + taxYearId;
            }
            if (rptType == "TAXCAL")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/Payroll/PayrollReport/" + data.FirstOrDefault().reportFormat + "?employeeInfoId=" + employeeInfoId + "&taxYearId=" + taxYearId;
            }

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
        public async Task<IActionResult> FinalTaxCertificateReportPdf(int employeeInfoId, int taxYearId)
        {
            try
            {
                var model = new PayrollReportViewModel
                {
                    empTaxDetailsViewModels = await salaryService.GetEmpTaxDetails(employeeInfoId, taxYearId),
                    empTaxSlabViewModels = await salaryService.GetEmpTaxSlab(employeeInfoId, taxYearId),
                    //empRebatableTaxViewModels = await salaryService.GetEmpRebatableTax(employeeInfoId, taxYearId),
                    //empTaxDeductFinalViewModels = await salaryService.GetEmpTaxDeductFinal(employeeInfoId, taxYearId),
                    companies = await eRPCompanyService.GetAllCompany(),
                };
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> TaxCalculationReport(int employeeInfoId, int taxYearId,int periodId)
        {
            try
            {
                //var model = new PayrollReportViewModel
                //{
                //    companies = await eRPCompanyService.GetAllCompany(),
                //};
                ViewBag.employeeId = employeeInfoId;
                ViewBag.taxyearId = taxYearId;
                ViewBag.periodId = periodId;
             var companys=   await eRPCompanyService.GetAllCompany();
                TaxCertificateViewModel model = new TaxCertificateViewModel
                {
                    company = companys.LastOrDefault(),
                    employeeInfo = await personalInfoService.GetEmployeeInfoById(employeeInfoId),
                    salaryPeriod = await salaryService.GetSalaryPeriodNameById(periodId),
                    taxableamountViewModels = await salaryService.TaxableamountViewModels(employeeInfoId,taxYearId),
                    taxableSlabViewModels=await salaryService.TaxableslabViewModels(employeeInfoId, taxYearId),
                    taxablePFViewModels=await salaryService.TaxablePFViewModels(employeeInfoId, taxYearId),
                    InvestmentRebateSettings=await salaryService.GetAllRebateSettingbytaxyearid(taxYearId),
                    taxYear=await salaryService.TaxYearbyId(taxYearId)


                };
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [AllowAnonymous]
        public IActionResult TaxCalculationReportAction(int employeeInfoId, int taxYearId, int periodId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            //url = scheme + "://" + host + "/Payroll/PayrollReport/" + data.FirstOrDefault().reportFormat + "?employeeInfoId=" + employeeInfoId + "&taxYearId=" + taxYearId;
            url = scheme + "://" + host + "/Payroll/PayrollReport/TaxCalculationReportPdf?employeeInfoId=" + employeeInfoId + "&taxYearId=" + taxYearId + "&periodId=" + periodId;

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
        public async Task<IActionResult> TaxCalculationReportPdf(int employeeInfoId, int taxYearId, int periodId)
        {
            try
            {
                //var model = new PayrollReportViewModel
                //{
                //    companies = await eRPCompanyService.GetAllCompany(),
                //};
                var companys = await eRPCompanyService.GetAllCompany();
                TaxCertificateViewModel model = new TaxCertificateViewModel
                {
                    company = companys.LastOrDefault(),
                    employeeInfo = await personalInfoService.GetEmployeeInfoById(employeeInfoId),
                    salaryPeriod = await salaryService.GetSalaryPeriodNameById(periodId),
                    taxableamountViewModels = await salaryService.TaxableamountViewModels(employeeInfoId, taxYearId),
                    taxableSlabViewModels = await salaryService.TaxableslabViewModels(employeeInfoId, taxYearId),
                    taxablePFViewModels = await salaryService.TaxablePFViewModels(employeeInfoId, taxYearId),
                    InvestmentRebateSettings = await salaryService.GetAllRebateSettingbytaxyearid(taxYearId),
                    taxYear = await salaryService.TaxYearbyId(taxYearId)

                };
                
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion        

        #region API
        [AllowAnonymous]
        [Route("global/api/GetUniverdata/{employeeInfoId}/{salaryPeriodId}")]
        [HttpGet]
        public async Task<IActionResult> GetUniverdata(int employeeInfoId, int salaryPeriodId)
        {
            return Json(await salaryService.GetUniversalCodaXLTempleteViewModels(salaryPeriodId, employeeInfoId));
        }

        [Route("global/api/GetBankStatementByPeriodId/{salaryPeriodId}/{sbuId}/{pnsId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetBankStatementByPeriodId(int salaryPeriodId, int? sbuId, int? pnsId)
        {
            var data = await salaryService.GetBankStatementByPeriodId(salaryPeriodId, sbuId, pnsId);
            return Json(data.Where(a => a.bankAccountNo != "" && a.bankPayable != 0));
        }

        [Route("global/api/GetCashStatementByPeriodId/{salaryPeriodId}/{sbuId}/{pnsId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCashStatementByPeriodId(int salaryPeriodId, int? sbuId, int? pnsId)
        {
            var data = await salaryService.GetBankStatementByPeriodId(salaryPeriodId, sbuId, pnsId);
            return Json(data.Where(a => a.cashPayable != 0));
        }

        [Route("global/api/GetWalletStatementByPeriodId/{salaryPeriodId}/{sbuId}/{pnsId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetWalletStatementByPeriodId(int salaryPeriodId, int? sbuId, int? pnsId)
        {
            var data = await salaryService.GetBankStatementByPeriodId(salaryPeriodId, sbuId, pnsId);
            return Json(data.Where(a => a.walletPayable != 0));
        }

        [Route("global/api/GetReconcilationStatement/{salaryPeriodId}/{presalaryPeriodId}/{typeId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetReconcilationStatement(int? salaryPeriodId, int? presalaryPeriodId, int? typeId)
        {
            return Json(await salaryService.GetReconcilationStatement(0,salaryPeriodId, presalaryPeriodId, typeId));
        }

        #endregion
    }
}