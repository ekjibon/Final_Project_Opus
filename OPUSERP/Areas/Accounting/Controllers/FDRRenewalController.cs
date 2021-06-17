using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Accounting.Services.FDR.Interface;
using OPUSERP.Accounting.Services.MasterData.Interfaces;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Accounting.Controllers
{
    [Authorize]
    [Area("Accounting")]
    public class FDRRenewalController : Controller
    {
        private readonly IFDRInvestmentService fDRInvestmentService;
        private readonly IBankBranchService bankBranchService;
        private readonly ILedgerService ledgerService;
        private readonly IERPCompanyService eRPCompanyService;
        private readonly ICostCentreService costCentreService;
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;


        public FDRRenewalController(IFDRInvestmentService fDRInvestmentService, IBankBranchService bankBranchService, ILedgerService ledgerService, IERPCompanyService eRPCompanyService, ICostCentreService costCentreService, IHostingEnvironment _hostingEnvironment, IConverter converter)
        {
            this.fDRInvestmentService = fDRInvestmentService;
            this.bankBranchService = bankBranchService;
            this.ledgerService = ledgerService;
            this.eRPCompanyService = eRPCompanyService;
            this.costCentreService = costCentreService;

            this._hostingEnvironment = _hostingEnvironment;
            myPDF = new MyPDF(_hostingEnvironment, converter);
            rootPath = _hostingEnvironment.ContentRootPath;
        }

        #region FDR Renewal Pending List
        public async Task<IActionResult> Index()
        {
            FDRRenewalViewModel model = new FDRRenewalViewModel
            {
                banks = await bankBranchService.GetAllBank()
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFDRRenewalPendingList(int? bankId, DateTime? frmDate, DateTime? toDate)
        {
            string userName = HttpContext.User.Identity.Name;
            return Json(await fDRInvestmentService.GetFDRRenewalPendingList(bankId, frmDate, toDate, userName));
        }
        #endregion

        #region FDR Renew
        public async Task<IActionResult> RenewFdr(int? masterId, int? detailsId, int? isEdit)
        {
            FDRRenewalViewModel model = new FDRRenewalViewModel
            {
                fDRMaturityStatusForRenewalViewModels = await fDRInvestmentService.GetFDRInformationForRenewal(masterId, detailsId, isEdit)
            };
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> GetFdrAutoNo(string bnkName, DateTime ftDate, string accountNo, string destinationType, string ftType)
        {
            var data = await fDRInvestmentService.GetFdrAutoNo(bnkName, ftDate, accountNo, destinationType, ftType);
            return Json(data.FirstOrDefault().ftInstructionNo);
        }

        [HttpGet]
        public async Task<IActionResult> GetLedgerForFdr()
        {
            return Json(await ledgerService.GetLedgerForFdr());
        }

        [HttpPost]
        public async Task<JsonResult> SaveFDRRenew(int FTID, int FDRID, int ParentFTID, int ParentFDRID, string ftno, string ftDate, string openDate, string maturityDate, decimal rate, decimal interestAmt, string tenure, string tenureType,  decimal? EncashBankCharge, string bankAccNo, string bankBranch, string fdrNo, decimal? amount,DateTime? fdrDate)
        {
            string user = HttpContext.User.Identity.Name;

            await costCentreService.FDRenewAutoVoucher(amount, fdrDate.ToString(), fdrNo, user);
            
            await fDRInvestmentService.SaveFdrRenew(FTID, FDRID, ParentFTID, ParentFDRID, ftno, ftDate, openDate, maturityDate, rate, interestAmt, tenure, tenureType,  EncashBankCharge, user, bankAccNo, bankBranch, fdrNo); 

            return Json(true);
        }

        #endregion


        #region Fdr Renewal List
        public async Task<IActionResult> FdrRenewalList()
        {
            FDRRenewalViewModel model = new FDRRenewalViewModel
            {
                banks = await bankBranchService.GetAllBank()
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFDRRenewalList(int? bankId, DateTime? frmDate, DateTime? toDate)
        {           
            return Json(await fDRInvestmentService.GetFDRRenewalList(bankId, frmDate, toDate));
        }
        #endregion

        #region Report FT

        [AllowAnonymous]
        public IActionResult FtInstructionReport(int fdrMasterId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;

            string url = scheme + "://" + host + "/Accounting/FDRRenewal/FtInstructionReportPdf?fdrMasterId=" + fdrMasterId;


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
        public async Task<IActionResult> FtInstructionReportPdf(int fdrMasterId)
        {
            try
            {
                var model = new FDRRenewalViewModel
                {
                    fDRReportViewModel = await fDRInvestmentService.ReportFdrFtInstruction(fdrMasterId),
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
    }
}