using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;

using OPUSERP.Areas.VAT.Models.Lang;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Helpers;


//namespace JSON.Areas.Report.Controllers
namespace OPUSERP.Areas.VAT.Controllers
{
    [Area("VAT")]
    public class VATReportController : Controller
    {
        private readonly LangGenerate<VATReportLn> _lang;
        private readonly string rootPath;
        private readonly MyPDF myPDF;


        public VATReportController(IHostingEnvironment hostingEnvironment, IConverter converter /*IVATReportService vATReportService, ITransferService transferService, ILedgerService ledgerService, IAddressService addressService, IAddressCategoryService addressCategoryService, IVATPaymentService vATPaymentService*/)
        {
            _lang = new LangGenerate<VATReportLn>(hostingEnvironment.ContentRootPath);
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        public async Task<ActionResult> Index()
        {
            //    VATReportViewModel model = new VATReportViewModel
            //    {
            //        fLang = _lang.PerseLang("VATReport/VATReportEN.json", "VATReport/VATReportBN.json", Request.Cookies["lang"]),
            //        transferMasters = await transferService.GetAllTransferMaster(),
            //        fiscalYears = await addressCategoryService.GetAllFiscalYear()
            //    };
            //return View(model);
            return View();
        }
        [AllowAnonymous]
        public async Task<ActionResult> PurchaseBooklet()
        {
            
            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> PurchaseBookletpdf()
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

           
            string url = scheme + "://" + host + "/Vat/VATReport/PurchaseBooklet";

            string fileName;
            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }
           

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public async Task<ActionResult> SalesBooklet()
        {
            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> SalesBookletpdf()
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Vat/VATReport/SalesBooklet";

            string fileName;
            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }
            
            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public async Task<ActionResult> ContractualProductChalan()
        {
            return View();
        }
        [AllowAnonymous]
        public async Task<ActionResult> ContractualProductChalanpdf()
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = scheme + "://" + host + "/Vat/VATReport/ContractualProductChalan";
            string fileName;
            string status = myPDF.GenerateLandscapePDF(out fileName, url);
            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }
            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public async Task<ActionResult> TaxChalan()
        {
            return View();
        }
        [AllowAnonymous]
        public async Task<ActionResult> TaxChalanPDF()
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = scheme + "://" + host + "/Vat/VATReport/TaxChalan";
            string fileName;
            string status = myPDF.GenerateLandscapePDF(out fileName, url);
            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }
            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public async Task<ActionResult> RegisteredEntityChalan()
        {
            return View();
        }
        [AllowAnonymous]
        public async Task<ActionResult> RegisteredEntityChalanpdf()
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = scheme + "://" + host + "/Vat/VATReport/RegisteredEntityChalan";
            string fileName;
            string status = myPDF.GenerateLandscapePDF(out fileName, url);
            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }
            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public async Task<ActionResult> CertificateTaxDeductAtSource()
        {
            return View();
        }
        [AllowAnonymous]
        public async Task<ActionResult> CertificateTaxDeductAtSourcePDF()
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = scheme + "://" + host + "/Vat/VATReport/CertificateTaxDeductAtSource";
            string fileName;
            string status = myPDF.GenerateLandscapePDF(out fileName, url);
            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }
            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public async Task<ActionResult> CreditNote()
        {
            return View();
        }
        [AllowAnonymous]
        public async Task<ActionResult> CreditNotePDF()
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = scheme + "://" + host + "/Vat/VATReport/CreditNote";
            string fileName;
            string status = myPDF.GenerateLandscapePDF(out fileName, url);
            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }
            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public async Task<ActionResult> DebitNote()
        {
            return View();
        }
        [AllowAnonymous]
        public async Task<ActionResult> DebitNotePDF()
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = scheme + "://" + host + "/Vat/VATReport/DebitNote";
            string fileName;
            string status = myPDF.GenerateLandscapePDF(out fileName, url);
            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }
            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public async Task<ActionResult> PurchaseSalesAccount()
        {
            return View();
        }
        [AllowAnonymous]
        public async Task<ActionResult> PurchaseSalesAccountpdf()
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = scheme + "://" + host + "/Vat/VATReport/PurchaseSalesAccount";
            string fileName;
            string status = myPDF.GenerateLandscapePDF(out fileName, url);
            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }
            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public async Task<ActionResult> PurchaseSalesChalan()
        {
            return View();
        }
        [AllowAnonymous]
        public async Task<ActionResult> PurchaseSalesChalanpdf()
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = scheme + "://" + host + "/Vat/VATReport/PurchaseSalesChalan";
            string fileName;
            string status = myPDF.GenerateLandscapePDF(out fileName, url);
            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }
            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public async Task<ActionResult> InputOutputCoefficientDeclaration()
        {
            return View();
        }
        [AllowAnonymous]
        public async Task<ActionResult> InputOutputCoefficientDeclarationpdf()
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = scheme + "://" + host + "/Vat/VATReport/InputOutputCoefficientDeclaration";
            string fileName;
            string status = myPDF.GenerateLandscapePDF(out fileName, url);
            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }
            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public async Task<ActionResult> VATSubmissionNew()
        {
            return View();
        }
        [AllowAnonymous]
        public async Task<ActionResult> VATSubmissionNewPDF()
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = scheme + "://" + host + "/Vat/VATReport/VATSubmissionNew";
            string fileName;
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