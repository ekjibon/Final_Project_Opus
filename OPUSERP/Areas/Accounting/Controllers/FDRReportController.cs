using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Accounting.Services.FDR.Interface;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;

namespace OPUSERP.Areas.Accounting.Controllers
{
    [Authorize]
    [Area("Accounting")]
    public class FDRReportController : Controller
    {
        private readonly IFDRInvestmentService fDRInvestmentService;
        private readonly IBankBranchService bankBranchService;
        private readonly IERPCompanyService eRPCompanyService;
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;

        public FDRReportController(IFDRInvestmentService fDRInvestmentService, IBankBranchService bankBranchService, IERPCompanyService eRPCompanyService, IHostingEnvironment _hostingEnvironment, IConverter converter)
        {
            this.fDRInvestmentService = fDRInvestmentService;
            this.bankBranchService = bankBranchService;
            this.eRPCompanyService = eRPCompanyService;

            this._hostingEnvironment = _hostingEnvironment;
            myPDF = new MyPDF(_hostingEnvironment, converter);
            rootPath = _hostingEnvironment.ContentRootPath;
        }

        #region Float FDR

        public async Task<IActionResult> FloatFdr()
        {
            FDRRenewalViewModel model = new FDRRenewalViewModel
            {
                banks = await bankBranchService.GetAllBank()
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetFdrReportingHistory(int? bankId, DateTime? frmDate, DateTime? toDate, string sOF)
        {
            return Json(await fDRInvestmentService.GetFdrReportingHistory(bankId, frmDate, toDate, sOF));
        }

        #endregion 

        #region Capital Fdr Report

        public async Task<IActionResult> CapitalFdr()
        {
            FDRRenewalViewModel model = new FDRRenewalViewModel
            {
                banks = await bankBranchService.GetAllBank()
            };
            return View(model);
        }


        [AllowAnonymous]
        public IActionResult FdrEncashmentReport(int fdrDetailsId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;

            string url = scheme + "://" + host + "/Accounting/FDRReport/FdrEncashmentReportPdf?fdrDetailsId=" + fdrDetailsId;


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
        public async Task<IActionResult> FdrEncashmentReportPdf(int fdrDetailsId)
        {
            try
            {
                var model = new FDRRenewalViewModel
                {
                    fDRReportViewModel = await fDRInvestmentService.ReportCapitalFdrEncashment(fdrDetailsId),
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

        #region

        public async Task<IActionResult> Index()
        {
            FDRInvestmentViewModel model = new FDRInvestmentViewModel
            {
                fDRInvestmentDetails = await fDRInvestmentService.GetFDRInvestmentDetail(),
            };
            return View(model);
        }

        #endregion

        #region FDR Investment Report

        [AllowAnonymous]
        public IActionResult FdrInvestmentScheduleData(string rptType, string fromDate, string toDate)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;
           
            if (rptType == "1")
            {
                url = scheme + "://" + host + "/Accounting/FDRReport/FdrInvestmentSchedulePdf?fromDate=" + fromDate + "&toDate=" + toDate;
            }
            if (rptType == "2")
            {
                url = scheme + "://" + host + "/Accounting/FDRReport/FdrInterestReceivablePdf?fromDate=" + fromDate + "&toDate=" + toDate;
            }
            else if (rptType == "3")
            {
                url = scheme + "://" + host + "/Accounting/FDRReport/FdrInterestIncomeStatementPdf?fromDate=" + fromDate + "&toDate=" + toDate;
            }           

            string status = myPDF.GenerateLandscapePDFA4(out fileName, url);
            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }
            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public IActionResult FdrInterestScheduleData(string rptType, int? fdrId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "4")
            {
                url = scheme + "://" + host + "/Accounting/FDRReport/FdrInterestSchedulePdf?fdrId=" + fdrId;
            }

            string status = myPDF.GenerateLandscapePDFA4(out fileName, url);
            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }
            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public IActionResult FdrInvestmentScheduleExcelData(string rptType, string fromDate, string toDate)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;
           
            if (rptType == "1")
            {
                url = scheme + "://" + host + "/Accounting/FDRReport/FdrInvestmentSchedulePdf?fromDate=" + fromDate + "&toDate=" + toDate;
            }
            if (rptType == "2")
            {
                url = scheme + "://" + host + "/Accounting/FDRReport/FdrInterestReceivablePdf?fromDate=" + fromDate + "&toDate=" + toDate;
            }
            else if (rptType == "3")
            {
                url = scheme + "://" + host + "/Accounting/FDRReport/FdrInterestIncomeStatementPdf?fromDate=" + fromDate + "&toDate=" + toDate;
            }

            string status = myPDF.GenerateLandscapePDFA4(out fileName, url);
            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            string pdfpath = rootPath + @"\wwwroot\pdf\" + fileName;
            string wordpath = fileName.Replace(".pdf", ".xls");
            SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
            f.OpenPdf(rootPath + @"\wwwroot\pdf\" + fileName);
            f.ToExcel(rootPath + @"\wwwroot\pdf\" + wordpath);
            var model = wordpath;
            return Json(model);
        }

        [AllowAnonymous]
        public IActionResult FdrInterestScheduleDataExcelData(string rptType, int? fdrId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "4")
            {
                url = scheme + "://" + host + "/Accounting/FDRReport/FdrInterestSchedulePdf?fdrId=" + fdrId;
            }            

            string status = myPDF.GenerateLandscapePDFA4(out fileName, url);
            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            string pdfpath = rootPath + @"\wwwroot\pdf\" + fileName;
            string wordpath = fileName.Replace(".pdf", ".xls");
            SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
            f.OpenPdf(rootPath + @"\wwwroot\pdf\" + fileName);
            f.ToExcel(rootPath + @"\wwwroot\pdf\" + wordpath);
            var model = wordpath;
            return Json(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> FdrInvestmentSchedulePdf(string fromDate, string toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            FDRRenewalViewModel model = new FDRRenewalViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                fDRScheduleReportViewModels = await fDRInvestmentService.FdrScheduleReport(fromDate, toDate)
            };
            return View(model);
        }       

        [AllowAnonymous]
        public async Task<ActionResult> FdrInterestReceivablePdf(string fromDate, string toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            FDRRenewalViewModel model = new FDRRenewalViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                fDRScheduleReportViewModels = await fDRInvestmentService.FdrScheduleReport(fromDate, toDate)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> FdrInterestIncomeStatementPdf(string fromDate, string toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            ViewBag.MonthYear = Convert.ToDateTime(toDate).ToString("MMM yyyy");
            FDRRenewalViewModel model = new FDRRenewalViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                fDRScheduleReportViewModels = await fDRInvestmentService.FdrScheduleReport(fromDate, toDate)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> FdrInterestSchedulePdf(int? fdrId)
        {   
            FDRRenewalViewModel model = new FDRRenewalViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                fDRInterestScheduleReportViewModels = await fDRInvestmentService.FdrInterestScheduleReport(fdrId)
            };
            return View(model);
        }

        #endregion

    }
}