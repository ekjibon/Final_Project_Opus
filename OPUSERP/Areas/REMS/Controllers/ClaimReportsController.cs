using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.REMS.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.REMS.Services.Interfaces;

namespace OPUSERP.Areas.REMS.Controllers
{
    [Area("REMS")]
    public class ClaimReportsController : Controller
    {
        private readonly IClaimRegisterService claimRegisterService;
        private readonly IUserInfoes userInfoes;
        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;

        public ClaimReportsController(IHostingEnvironment hostingEnvironment, IConverter converter, IUserInfoes userInfoes, IClaimRegisterService claimRegisterService)
        {
            this.claimRegisterService = claimRegisterService;
            this.userInfoes = userInfoes;
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        public IActionResult Index()
        {
            return View();
        }


        #region ClaimReceipt
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ClaimReceipt(int claimId)
        {
            try
            {
                var result = await claimRegisterService.GetClaimRegisterById(claimId);
                var userInfo = await userInfoes.GetUserInfoByUserId(result.userId);
                ClaimReportsViewModel model = new ClaimReportsViewModel
                {
                    claimByName = userInfo.EmpCode + " - " + userInfo.EmpName,
                    claimRegister = result
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public IActionResult ClaimReceiptPdf(int claimId)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/REMS/ClaimReports/ClaimReceipt?claimId=" + claimId;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        #endregion

        #region ClaimRaised
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ClaimRaised(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                //var result = await iOUService.GetIOUPaymentStatus(projectId, fromDate, toDate);
                ClaimReportsViewModel model = new ClaimReportsViewModel
                {
                    //iOUPaymentStatusVMs = result
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public IActionResult ClaimRaisedPdf(DateTime? fromDate, DateTime? toDate)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/REMS/ClaimReports/ClaimRaised?fromDate=" + fromDate + "&toDate=" + toDate;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        #endregion

        #region WarrentyClaim
        [HttpGet]
        [AllowAnonymous]
        public IActionResult WarrentyClaim(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                //var result = await iOUService.GetIOUPaymentStatus(projectId, fromDate, toDate);
                ClaimReportsViewModel model = new ClaimReportsViewModel
                {
                    //iOUPaymentStatusVMs = result
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public IActionResult WarrentyClaimPdf(DateTime? fromDate, DateTime? toDate)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/REMS/ClaimReports/WarrentyClaim?fromDate=" + fromDate + "&toDate=" + toDate;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        #endregion

        #region Non-WarrentyClaim
        [HttpGet]
        [AllowAnonymous]
        public IActionResult NonWarrentyClaim(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                //var result = await iOUService.GetIOUPaymentStatus(projectId, fromDate, toDate);
                ClaimReportsViewModel model = new ClaimReportsViewModel
                {
                    //iOUPaymentStatusVMs = result
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public IActionResult NonWarrentyClaimPdf(DateTime? fromDate, DateTime? toDate)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/REMS/ClaimReports/NonWarrentyClaim?fromDate=" + fromDate + "&toDate=" + toDate;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        #endregion

        #region ClaimSuccess
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ClaimSuccess(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                //var result = await iOUService.GetIOUPaymentStatus(projectId, fromDate, toDate);
                ClaimReportsViewModel model = new ClaimReportsViewModel
                {
                    //iOUPaymentStatusVMs = result
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public IActionResult ClaimSuccessPdf(DateTime? fromDate, DateTime? toDate)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/REMS/ClaimReports/ClaimSuccess?fromDate=" + fromDate + "&toDate=" + toDate;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        #endregion

        #region Obsolate
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Obsolate(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                //var result = await iOUService.GetIOUPaymentStatus(projectId, fromDate, toDate);
                ClaimReportsViewModel model = new ClaimReportsViewModel
                {
                    //iOUPaymentStatusVMs = result
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public IActionResult ObsolatePdf(DateTime? fromDate, DateTime? toDate)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/REMS/ClaimReports/Obsolate?fromDate=" + fromDate + "&toDate=" + toDate;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        #endregion
    }
}