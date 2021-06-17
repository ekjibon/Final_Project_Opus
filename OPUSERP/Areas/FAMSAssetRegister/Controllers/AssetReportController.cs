using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.FAMSAssetRegister.Models;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.FixedAsset.Services.AssetRegister.Interfaces;
using OPUSERP.FixedAsset.Services.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.Payroll.Services.Salary.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.FAMSAssetRegister.Controllers
{
    [Authorize]
    [Area("FAMSAssetRegister")]
    public class AssetReportController : Controller
    {
        private readonly IAssetDepreciationService assetDepreciationService;
        private readonly IDepriciationPeriodService depriciationPeriodService;
        private readonly IERPCompanyService eRPCompanyService;
        private readonly ISalaryService salaryService;
        private readonly string rootPath;
        private readonly MyPDF myPDF;
        //public string FileName;

        public AssetReportController(IHostingEnvironment hostingEnvironment, IAssetDepreciationService assetDepreciationService, IConverter converter, IDepriciationPeriodService depriciationPeriodService, IERPCompanyService eRPCompanyService, ISalaryService salaryService)
        {
            this.assetDepreciationService = assetDepreciationService;
            this.depriciationPeriodService = depriciationPeriodService;
            this.eRPCompanyService = eRPCompanyService;
            this.salaryService = salaryService;

            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        public async Task<IActionResult> Index()
        {
            AssetReportViewModel model = new AssetReportViewModel
            {
                depriciationPeriods = await depriciationPeriodService.GetAllDepriciationPeriod(),
            };
            return View(model);
        }

        #region Asset Register/Sub Group wise Report

        [AllowAnonymous]
        public async Task<IActionResult> AssetRegisterData(string rptType, int itemCategoryId, string fromDate, string toDate)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "FAREGSTR" || rptType == "FARGROUPWISE" || rptType == "FAADDITION")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/FAMSAssetRegister/AssetReport/" + data.FirstOrDefault().reportFormat + "?itemCategoryId=" + itemCategoryId + "&fromDate=" + fromDate + "&toDate=" + toDate;

            }
            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<ActionResult> AssetRegisterPdf(int itemCategoryId, string fromDate, string toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            AssetReportViewModel model = new AssetReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                assetRegisterReportViewModels = await assetDepreciationService.AssetRegisterReport(itemCategoryId, fromDate, toDate)

            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> AssetRegisterSubGroupPdf(int itemCategoryId, string fromDate, string toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            AssetReportViewModel model = new AssetReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                assetRegisterReportViewModels = await assetDepreciationService.AssetRegisterReport(itemCategoryId, fromDate, toDate)

            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> AssetRegisterAddition(int itemCategoryId, string fromDate, string toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            AssetReportViewModel model = new AssetReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                assetRegisterReportViewModels = await assetDepreciationService.AssetRegisterAddition(itemCategoryId, fromDate, toDate)

            };
            return View(model);
        }

        #endregion

        #region Asset Transfer Report

        [AllowAnonymous]
        public async Task<IActionResult> AssetTransferData(string rptType, int itemCategoryId, string fromDate, string toDate)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "FATRANSFER")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/FAMSAssetRegister/AssetReport/" + data.FirstOrDefault().reportFormat + "?itemCategoryId=" + itemCategoryId + "&fromDate=" + fromDate + "&toDate=" + toDate;
            }

            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<ActionResult> AssetTransferPdf(int itemCategoryId, string fromDate, string toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            AssetReportViewModel model = new AssetReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                assetTransferReportViewModels = await assetDepreciationService.AssetTransferReport(itemCategoryId, fromDate, toDate)

            };
            return View(model);
        }

        #endregion

        #region Asset Retirement Report

        [AllowAnonymous]
        public async Task<IActionResult> AssetRetirementData(string rptType, int itemCategoryId, string fromDate, string toDate)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "FARETIRMT")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/FAMSAssetRegister/AssetReport/" + data.FirstOrDefault().reportFormat + "?itemCategoryId=" + itemCategoryId + "&fromDate=" + fromDate + "&toDate=" + toDate;
            }
            
            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<ActionResult> AssetRetirementPdf(int itemCategoryId, string fromDate, string toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            AssetReportViewModel model = new AssetReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                assetRetirementReportViewModels = await assetDepreciationService.AssetRetirementReport(itemCategoryId, fromDate, toDate)

            };
            return View(model);
        }

        #endregion

        #region Asset Register Summary Report

        [AllowAnonymous]
        public async Task<IActionResult> AssetRegisterSummaryData(string rptType, string fromDate, string toDate)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "FASUMMARY")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/FAMSAssetRegister/AssetReport/" + data.FirstOrDefault().reportFormat + "?fromDate=" + fromDate + "&toDate=" + toDate;
            }            
            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<ActionResult> AssetRegisterSummaryPdf(string fromDate, string toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            AssetReportViewModel model = new AssetReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                assetRegisterSummaryReportViewModels = await assetDepreciationService.AssetRegisterSummaryReport(fromDate, toDate)

            };
            return View(model);
        }

        #endregion

        #region Asset Schedule Report

        [AllowAnonymous]
        public async Task<IActionResult> AssetScheduleData(string rptType, string fromDate, string toDate)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "FASCHDL")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/FAMSAssetRegister/AssetReport/" + data.FirstOrDefault().reportFormat + "?fromDate=" + fromDate + "&toDate=" + toDate;
            }           
            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<ActionResult> AssetSchedulePdf(string fromDate, string toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            AssetReportViewModel model = new AssetReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                assetScheduleReportViewModels = await assetDepreciationService.AssetScheduleReport(fromDate, toDate)

            };
            return View(model);
        }

        #endregion

        #region Depreciation Report

        [AllowAnonymous]
        public async Task<IActionResult> AssetDepreciationReportByPeriodId(string rptType, int periodId, int itemCatId)
        {           
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;            
            string url = "";
            string fileName;

            if (rptType == "FAMNTHDEPRE")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = $"" + scheme + "://" + host + "/FAMSAssetRegister/AssetReport/" + data.FirstOrDefault().reportFormat + "/" + "?periodId=" + periodId + "&itemCatId=" + itemCatId;
            }

            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> AssetDepreciationReport(int periodId, int itemCatId)
        {
            try
            {
                AssetReportViewModel model = new AssetReportViewModel
                {
                    companies = await eRPCompanyService.GetAllCompany(),
                    //assetDepreciationReportViewModels = await assetDepreciationService.GetAssetDepreciationReportByPeriodId(periodId, itemCatId)
                    assetDepreciationReportViewModels = await assetDepreciationService.AssetDepreciationReportByPeriod(periodId, itemCatId)

                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}