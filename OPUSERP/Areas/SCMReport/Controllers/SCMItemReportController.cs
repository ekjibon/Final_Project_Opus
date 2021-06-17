using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.SCMReport.Models;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.SCM.Services.Report.Interface;

namespace OPUSERP.Areas.SCMReport.Controllers
{
    [Area("SCMReport")]
    

    public class SCMItemReportController : Controller
    {

        private readonly MyPDF myPDF;
        private readonly string rootPath;
        private readonly IRequisition _requisition;
        public string FileName;
        public readonly IERPCompanyService eRPCompanyService;
        private readonly IItemSummaryService itemSummaryService;

        public SCMItemReportController(
		IHostingEnvironment hostingEnvironment, 
		IConverter converter,
		IItemSummaryService itemSummaryService, 
		IRequisition requisition, 
		IERPCompanyService eRPCompanyService)
        {
            this.myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
            this.itemSummaryService = itemSummaryService;
            this.eRPCompanyService = eRPCompanyService;
            _requisition = requisition;
        }



   

        public IActionResult IndexPO()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult SKUSummerSheetReportPdf(DateTime fromDate, DateTime toDate)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/SCMItemReport/SKUSummerSheetReport";
            string status = myPDF.GenerateLandscapePDF(out fileName, url);
            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }
            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> SKUSummerSheetReport()
        {
            var data =await _requisition
                .GetRequisitionDetails();
            return View(data);
        }

        [AllowAnonymous]
        public IActionResult ItemSummarySheetReportPdf(DateTime fromDate, DateTime toDate)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/SCMItemReport/ItemSummarySheetReport" + "?fromDate=" + fromDate + "&toDate=" + toDate;
            string status = myPDF.GenerateLandscapePDF(out fileName, url);
            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public async Task<IActionResult> ItemSummarySheetReport(DateTime fromDate, DateTime toDate)
        {
            //var company = await eRPCompanyService.GetAllCompany();

            var model = await itemSummaryService.GetItemSummary(fromDate, toDate);
            //ItemSummaryViewModel item = new ItemSummaryViewModel
            //{
            //    companies = company,
            //};

            ViewBag.FromDate = Convert.ToDateTime(fromDate).ToString("dd MMM yyyy");
            ViewBag.ToDate = Convert.ToDateTime(toDate).ToString("dd MMM yyyy");

            return View(model);
        }

        [AllowAnonymous]
        public  async Task<IActionResult> IssueDetailsReport()
        {
            var data =await _requisition.GetItemSpecifications();
             return  View(data);
        }

        [AllowAnonymous]
        public  IActionResult IssueDetailsReportPdf(DateTime toDate, DateTime formDate)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/SCMItemReport/IssueDetailsReport";

            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
      
        }


        [AllowAnonymous]
        public async Task<IActionResult> ItemWiseIssueDetailsReport(DateTime fromDate, DateTime toDate)
        {
            var model = await itemSummaryService.ItemwiseIssueDetails(fromDate, toDate);
            //ItemSummaryViewModel item = new ItemSummaryViewModel
            //{
            //    companies = company,
            //};

            ViewBag.FromDate = Convert.ToDateTime(fromDate).ToString("dd MMM yyyy");
            ViewBag.ToDate = Convert.ToDateTime(toDate).ToString("dd MMM yyyy");

            return View(model);
        }

        [AllowAnonymous]
        public  IActionResult ItemWiseIssueDetailsReportPdf(DateTime fromDate, DateTime toDate)
        {

            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/SCMReport/SCMItemReport/ItemWiseIssueDetailsReport" + "?fromDate=" + fromDate + "&toDate=" + toDate;

            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }


        
    }
}
