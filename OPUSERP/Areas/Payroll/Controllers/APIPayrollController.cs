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
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.Payroll.Services.Salary.Interfaces;

namespace OPUSERP.Areas.Payroll.Controllers
{
    public class APIPayrollController : Controller
    {
        private readonly ISalaryService salaryService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly ISpecialBranchUnitService specialBranchUnitService;
        private readonly IERPCompanyService eRPCompanyService;
        private readonly IHostingEnvironment hostingEnvironment;

        //private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;

        public APIPayrollController(ISalaryService salaryService, IConverter converter, IPersonalInfoService personalInfoService, ISpecialBranchUnitService specialBranchUnitService, IERPCompanyService eRPCompanyService, IHostingEnvironment hostingEnvironment /*string rootPath, MyPDF myPDF*/)
        {
            this.salaryService = salaryService;
            this.personalInfoService = personalInfoService;
            this.specialBranchUnitService = specialBranchUnitService;
            this.eRPCompanyService = eRPCompanyService;
            this.hostingEnvironment = hostingEnvironment;
            myPDF = new MyPDF(hostingEnvironment, converter);
            //rootPath = hostingEnvironment.ContentRootPath;

        }

        [AllowAnonymous]
        [Route("global/api/GetUniverdata/{employeeInfoId}/{salaryPeriodId}")]
        [HttpGet]
        public async Task<IActionResult> GetUniverdata(int employeeInfoId, int salaryPeriodId)
        {
            return Json(await salaryService.GetUniversalCodaXLTempleteViewModels(salaryPeriodId, employeeInfoId));
        }



        [AllowAnonymous]
        [Route("global/api/GetPayrollReportViewModel")]
        [HttpGet]
        public async Task<ActionResult> GetPayrollReportViewModel()
        {
            PayrollReportViewModel model = new PayrollReportViewModel
            {
                salaryPeriods = await salaryService.GetAllSalaryPeriod(),
                specialBranchUnits = await specialBranchUnitService.GetSpecialBranchUnit(),
                taxYears = await salaryService.GetAllTaxYear()
            };

            return Json(model);
        }
        public IActionResult Index()
        {
            return View();
        }


        #region PDF
        //[AllowAnonymous]
        //[Route("global/api/GetSalaryReport/{rptType}/{employeeInfoId}/{salaryPeriodId}")]
        //[HttpGet]
        //public async Task<IActionResult> SalaryReport(string rptType, int employeeInfoId, int salaryPeriodId)
        //{
        //    string scheme = Request.Scheme;
        //    var host = Request.Host;
        //    var data = await salaryService.GetReportFormatByReportType(rptType);
        //    //string url = scheme + "://" + host + "/Payroll/PayrollReport/" + data.FirstOrDefault().reportFormat + "?employeeInfoId=" + employeeInfoId + "&salaryPeriodId=" + salaryPeriodId;

        //    string url = "";
        //    string fileName;


        //    string status = myPDF.GeneratePDF(out fileName, url);
        //    FileName = fileName;
        //    if (status != "done")
        //    {
        //        return Content("<h1>" + status + "</h1>");
        //    }

        //    // var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
        //    var stream = "pdf/" + fileName;
        //    return Json(stream);
        //}





        [AllowAnonymous]
        [Route("global/api/GetMonthlySalaryReport/{rptType}/{sbuId}/{pnsId}/{salaryPeriodId}")]
        [HttpGet]
        public async Task<IActionResult> MonthlySalaryReport(string rptType, int? sbuId, int? pnsId, int salaryPeriodId)
        {

            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "MWSS")
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

            // var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            var stream = "pdf/" + fileName;
            return Json(stream);
        }

        [AllowAnonymous]

        [Route("global/api/PayslipReportPdf/{employeeInfoId}/{salaryPeriodId}")]
        [HttpGet]
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
                };
                return Json(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        [Route("global/api/GetPayslipReportPdf_RedCross/{employeeInfoId}/{salaryPeriodId}")]
        [HttpGet]
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
                return Json(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [AllowAnonymous]

        [Route("global/api/GetUniversalCodePdfAction/{rptType}/{employeeInfoId}/{salaryPeriodId}")]
        [HttpGet]
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

            //    FileName = fileName;
            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            // var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            var stream = "pdf/" + fileName;
            return Json(stream);
        }

        [AllowAnonymous]
        public async Task<IActionResult> UniversalCodePdf(int employeeInfoId, int salaryPeriodId)
        {
            try
            {
                var model = new PayrollReportViewModel
                {
                    salaryPeriods = await salaryService.GetSalaryPeriodById(salaryPeriodId),
                    universalCodaXLTempleteViewModels = await salaryService.GetUniversalCodaXLTempleteViewModels(salaryPeriodId, employeeInfoId),
                    companies = await eRPCompanyService.GetAllCompany(),
                };
                return Json(model);
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
                return Json(model);
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
                return Json(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]


        [Route("global/api/GetFinalTaxCertificateReportPdfAction/{rptType}/{employeeInfoId}/{salaryPeriodId}/{taxYearId}")]
        [HttpGet]
        public async Task<IActionResult> FinalTaxCertificateReportPdfAction(string rptType, int employeeInfoId,int salaryPeriodId, int taxYearId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;
            string status;

            if (rptType == "PSLIP")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/Payroll/PayrollReport/" + data.FirstOrDefault().reportFormat + "?employeeInfoId=" + employeeInfoId + "&salaryPeriodId=" + salaryPeriodId;
            }
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

            status = myPDF.GeneratePDF(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            // var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            var stream = "pdf/" + fileName;
            return Json(stream);
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
                return Json(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> TaxCalculationReportPdf(int employeeInfoId, int taxYearId)
        {
            try
            {
                var model = new PayrollReportViewModel
                {
                    companies = await eRPCompanyService.GetAllCompany(),
                };
                return Json(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
    }
}