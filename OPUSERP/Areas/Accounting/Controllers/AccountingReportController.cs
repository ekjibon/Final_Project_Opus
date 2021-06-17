using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Accounting.Services.MasterData.Interfaces;
using OPUSERP.Accounting.Services.Voucher.Interfaces;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.Budget.Service.Interface;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.Payroll.Services.Salary.Interfaces;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Accounting.Controllers
{
    [Authorize]
    [Area("Accounting")]
    public class AccountingReportController : Controller
    {
        private readonly IVoucherTypeService voucherTypeService;
        private readonly ILedgerService ledgerService;
        private readonly IAccountingReportService reportingService;
        private readonly ICostCentreService costCentreService;
        private readonly IERPCompanyService eRPCompanyService;
        private readonly ISpecialBranchUnitService specialBranchUnitService;
        private readonly IProjectService projectService;
        private readonly IUserInfoes userInfo;
        private readonly IBudgetRequsitionMasterService budgetRequsitionMasterService;
        private readonly ISalaryService salaryService;

        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public AccountingReportController(IVoucherTypeService voucherTypeService, IUserInfoes userInfo, IBudgetRequsitionMasterService budgetRequsitionMasterService, ISpecialBranchUnitService specialBranchUnitService, IProjectService projectService, ILedgerService ledgerService, IAccountingReportService reportingService, ICostCentreService costCentreService, IHostingEnvironment hostingEnvironment, IConverter converter, IERPCompanyService eRPCompanyService, ISalaryService salaryService)
        {
            this.voucherTypeService = voucherTypeService;
            this.ledgerService = ledgerService;
            this.reportingService = reportingService;
            this.costCentreService = costCentreService;
            this.eRPCompanyService = eRPCompanyService;
            this.specialBranchUnitService = specialBranchUnitService;
            this.projectService = projectService;
            this.userInfo = userInfo;
            this.budgetRequsitionMasterService = budgetRequsitionMasterService;
            this.salaryService = salaryService;

            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }
        // GET: AccountingReport
        public ActionResult Index()
        {
            return View();
        }

        #region Cash Book

        public ActionResult CashBook()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult CashBookReportDataViewpdf(int ledgerId, int subledgerId, DateTime fromDate, DateTime toDate, int sbuId, int projectId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Accounting/AccountingReport/CashBookReportDataView?ledgerId=" + ledgerId + "&subledgerId=" + subledgerId + "&fromDate=" + fromDate + "&toDate=" + toDate + "&sbuId=" + sbuId + "&projectId=" + projectId;

            string fileName;
            string status = myPDF.GenerateLandscapePDFA4(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        #endregion

        #region Bank Book

        public ActionResult BankBook()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult BankBookReportDataViewpdf(int ledgerId, int subledgerId, DateTime fromDate, DateTime toDate, int sbuId, int projectId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Accounting/AccountingReport/BankBookReportDataView?ledgerId=" + ledgerId + "&subledgerId=" + subledgerId + "&fromDate=" + fromDate + "&toDate=" + toDate + "&sbuId=" + sbuId + "&projectId=" + projectId;

            string fileName;
            string status = myPDF.GenerateLandscapePDFA4(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        #endregion


        #region LedgerBook

        public ActionResult LedgerBook()
        {
            return View();
        }

        #endregion

        #region SubLedgerBook

        public ActionResult SubLedgerBook()
        {
            return View();
        }

        #endregion

        #region DayBook

        public ActionResult DayBook()
        {
            return View();
        }

        #endregion

        #region TrialBalance

        public ActionResult TrialBalance()
        {
            return View();
        }

        #endregion

        #region Financial Reports

        public ActionResult ProfitAndLossAccount()
        {
            return View();
        }
        public ActionResult BalanceSheet()
        {
            return View();
        }
        public ActionResult CFS()
        {
            return View();
        }
        public ActionResult CFSIndirect()
        {
            return View();
        }
        public ActionResult RV()
        {
            return View();
        }

        public ActionResult BudgetExpenseReport()
        {
            return View();
        }
        public async Task<ActionResult> BudgetExpenseReportP()
        {
            BudgetExpenseMasterViewModel model = new BudgetExpenseMasterViewModel
            {
                fiscalYears= await budgetRequsitionMasterService.GetFiscalYear()
        };
           
            return View(model);
        }
        #endregion

        #region DailyBillReceive

        public ActionResult DailyBillReceive()
        {

            return View();
        }

        #endregion

        #region CCWiseReport

        public ActionResult CcWiseReport()
        {
            return View();
        }

        #endregion

        #region LedgerDetails

        public ActionResult LedgerDetails()
        {
            return View();
        }
        public ActionResult ChartOAcc()
        {
            return View();
        }


        #endregion

        [AllowAnonymous]
        public async Task<ActionResult> LedgerBookReportDataView(int ledgerId, int subledgerId, DateTime fromDate, DateTime toDate, int sbuId, int projectId)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            var model = await reportingService.GetLedgerBookReportData(ledgerId, subledgerId, fromDate, toDate, sbuId, projectId);
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> CashBookReportDataView(int ledgerId, int subledgerId, DateTime fromDate, DateTime toDate, int sbuId, int projectId)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            var model = await reportingService.GetCashLedgerBookReportData(ledgerId, subledgerId, fromDate, toDate, sbuId, projectId);
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> BankBookReportDataView(int ledgerId, int subledgerId, DateTime fromDate, DateTime toDate, int sbuId, int projectId)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            var model = await reportingService.GetBankLedgerBookReportData(ledgerId, subledgerId, fromDate, toDate, sbuId, projectId);
            return View(model);
        }


        [AllowAnonymous]
        public async Task<IActionResult> LedgerDetailsDataView(int ledgerId, DateTime fromDate, DateTime toDate)
        {
            var model = await reportingService.LedgerDetailsViewModels(ledgerId, fromDate, toDate);
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult LedgerBookReportDataViewpdf(int ledgerId, int subledgerId, DateTime fromDate, DateTime toDate, int sbuId, int projectId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Accounting/AccountingReport/LedgerBookReportDataView?ledgerId=" + ledgerId + "&subledgerId=" + subledgerId + "&fromDate=" + fromDate + "&toDate=" + toDate + "&sbuId=" + sbuId + "&projectId=" + projectId;

            string fileName;
            string status = myPDF.GenerateLandscapePDFA4(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }


        [AllowAnonymous]
        public async Task<ActionResult> SubLedgerBookReportDataView(int ledgerId, int subledgerId, DateTime fromDate, DateTime toDate, int sbuId, int projectId)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            //var model = await reportingService.GetSubLedgerBookReportData(subledgerId, fromDate, toDate);
            var model = await reportingService.GetLedgerBookReportData(ledgerId, subledgerId, fromDate, toDate, sbuId, projectId);
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult SubLedgerBookReportDataViewpdf(int ledgerId, int subledgerId, DateTime fromDate, DateTime toDate, int sbuId, int projectId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            
            string url = scheme + "://" + host + "/Accounting/AccountingReport/SubLedgerBookReportDataView?ledgerId=" + ledgerId + "&subledgerId=" + subledgerId + "&fromDate=" + fromDate + "&toDate=" + toDate + "&sbuId=" + sbuId + "&projectId=" + projectId;

            string fileName;
            string status = myPDF.GenerateLandscapePDFA4(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<ActionResult> DayBookReportDataView(int voucherTypeId, DateTime fromDate, DateTime toDate, int? sbuId, int? projectId)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            var model = await reportingService.GetDayBookReportData(voucherTypeId, fromDate, toDate,sbuId,projectId);
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult DayBookReportDataViewpdf(int voucherTypeId, DateTime fromDate, DateTime toDate, int? sbuId, int? projectId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Accounting/AccountingReport/DayBookReportDataView?voucherTypeId=" + voucherTypeId + "&fromDate=" + fromDate + "&toDate=" + toDate + "&sbuId=" + sbuId + "&projectId=" + projectId;

            string fileName;
            string status = myPDF.GenerateLandscapePDFA4(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<ActionResult> TrialBalanceReportData(DateTime fromDate, DateTime toDate, int? sbuId, int? projectId)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            var model = await reportingService.GetTrialBalanceReportData(fromDate, toDate, sbuId, projectId);
            return View(model);
        }


        [AllowAnonymous]
        public async Task<ActionResult> CCReportDataView(int costcentreId, DateTime fromDate, DateTime toDate, int? sbuId, int? projectId)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            var model = await reportingService.GetCCWiseLedgerReportViewModels(costcentreId, fromDate, toDate, sbuId, projectId);
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult CCReportDataViewpdf(int costcentreId, DateTime fromDate, DateTime toDate, int? sbuId, int? projectId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Accounting/AccountingReport/CCReportDataView?costcentreId=" + costcentreId + "&fromDate=" + fromDate + "&toDate=" + toDate + "&sbuId=" + sbuId + "&projectId=" + projectId;

            string fileName;
            string status = myPDF.GenerateLandscapePDFA4(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public async Task<ActionResult> chartofAccounts(int? projectId, int? sbuId)
        {
            var model = await reportingService.ChartOfAccountViewModels(sbuId, projectId);
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult chartofAccountspdf(int? projectId, int? sbuId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Accounting/AccountingReport/chartofAccounts?projectId=" + projectId + "&sbuId=" + sbuId;

            string fileName;
            string status = myPDF.GeneratePDFVoucher(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public IActionResult TrialBalanceReportDatapdf(DateTime fromDate, DateTime toDate, int? sbuId, int? projectId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Accounting/AccountingReport/TrialBalanceReportData?fromDate=" + fromDate + "&toDate=" + toDate + "&sbuId=" + sbuId + "&projectId=" + projectId;

            string fileName;
            string status = myPDF.GeneratePDFVoucher(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public IActionResult LedgerDetailsPdf(int ledgerId, DateTime fromDate, DateTime toDate)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Accounting/AccountingReport/LedgerDetailsDataView?ledgerId=" + ledgerId + "&fromDate=" + fromDate + "&toDate=" + toDate;

            string fileName;
            string status = myPDF.GenerateLandscapePDFA4(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        #region Profit & Loss

        [AllowAnonymous]
        //public async Task<ActionResult> PLReportData(int? salaryYearId)
        public async Task<ActionResult> PLReportData(string fromDate, string toDate,int? sbuId,int? projectId)
        {
            ViewBag.toDate = Convert.ToDateTime(toDate).Year - 1;
            ViewBag.fromDate = Convert.ToDateTime(fromDate).Year;
            var model = new FinancialReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                profitAndLossAccountViewModels = await reportingService.GetProfitLossACData(fromDate, toDate, sbuId, projectId)
                //profitAndLossAccountViewModels = await reportingService.GetProfitLossACData(salaryYearId)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> PLReportDataBrac(string fromDate, string toDate, int? sbuId, int? projectId)
        {
            ViewBag.previousYear = Convert.ToDateTime(toDate).Year - 1;
            ViewBag.currentYear = Convert.ToDateTime(fromDate).Year;
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            var model = new FinancialReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                profitAndLossAccountViewModels = await reportingService.GetProfitLossACData(fromDate, toDate, sbuId, projectId)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> CFSReportDataBrac(string fromDate, string toDate, int? sbuId, int? projectId)
        {

            var model = new FinancialReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                cFSMasterViewModels = await reportingService.GetCFSMasterViewModels(fromDate, toDate, sbuId, projectId)
            };
            // return Json(model.cFSMasterViewModels);
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> CFSReportIndirectDataBrac(string fromDate, string toDate, int? sbuId, int? projectId)
        {
            var model = new FinancialReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                cFSMasterViewModels = await reportingService.GetCFSIndirectMasterViewModels(fromDate, toDate, sbuId, projectId)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> RVReportDataBrac(string fromDate, string toDate, int? sbuId, int? projectId)
        {

            var model = new FinancialReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                rVMasterViewModels = await reportingService.GetRVMasterViewModels(fromDate, toDate, sbuId, projectId)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> RVReportDataPolwel(string fromDate, string toDate, int? sbuId, int? projectId)
        {

            var model = new FinancialReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                rVMasterViewModels = await reportingService.GetRVMasterViewModels(fromDate, toDate, sbuId, projectId)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> BEReportDataBrac(string fromDate, string toDate, int? sbuId, int? projectId)
        {

            var model = new FinancialReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                budgetExpenseMasterViewModels = await reportingService.GetBudgetExpenseMasterViewModels(fromDate, toDate, sbuId, projectId)
            };
            // return Json(model.cFSMasterViewModels);
            return View(model);
        }
        [AllowAnonymous]
        public async Task<ActionResult> BEReportDataBracp(int Id,int? partnerId, int? sbuId, int? projectId)
        {

            var model = new FinancialReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                budgetExpenseMasterPViewModels = await reportingService.GetBudgetExpenseMasterPViewModels(Id,partnerId, sbuId, projectId),
                project=await projectService.GetProjectById(Convert.ToInt32(projectId))
            };
            // return Json(model.cFSMasterViewModels);
            return View(model);
        }


        [AllowAnonymous]
        public IActionResult PLReportDatapdf(string fromDate, string toDate, int? sbuId, int? projectId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            //string url = scheme + "://" + host + "/Accounting/AccountingReport/PLReportData?salaryYearId=" + salaryYearId;
            string url = scheme + "://" + host + "/Accounting/AccountingReport/PLReportDataBrac?fromDate=" + fromDate + "&toDate=" + toDate + "&sbuId=" + sbuId + "&projectId=" + projectId;

            string fileName;
            string status = myPDF.GeneratePDFVoucher(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> PLReportDataExcel(string fromDate, string toDate, int? sbuId, int? projectId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            //string url = scheme + "://" + host + "/Accounting/AccountingReport/PLReportData?salaryYearId=" + salaryYearId;
            string url = scheme + "://" + host + "/Accounting/AccountingReport/PLReportDataBrac?fromDate=" + fromDate + "&toDate=" + toDate + "&sbuId=" + sbuId + "&projectId=" + projectId;

            string fileName;
            string status = myPDF.GeneratePDFVoucher(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            string pdfpath = rootPath + @"\wwwroot\pdf\" + fileName;
            string wordpath = fileName.Replace(".pdf", ".csv");
            SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
            f.OpenPdf(rootPath + @"\wwwroot\pdf\" + fileName);
            f.ToExcel(rootPath + @"\wwwroot\pdf\" + wordpath);
            var model = wordpath;
            return Json(model);
        }

        [AllowAnonymous]
        public IActionResult CFSReportDatapdf(string fromDate, string toDate, int? sbuId, int? projectId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            //string url = scheme + "://" + host + "/Accounting/AccountingReport/PLReportData?salaryYearId=" + salaryYearId;
            string url = scheme + "://" + host + "/Accounting/AccountingReport/CFSReportDataBrac?fromDate=" + fromDate + "&toDate=" + toDate + "&sbuId=" + sbuId + "&projectId=" + projectId;

            string fileName;
            string status = myPDF.GeneratePDFVoucher(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public IActionResult CFSIndirectReportDatapdf(string fromDate, string toDate, int? sbuId, int? projectId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Accounting/AccountingReport/CFSReportIndirectDataBrac?fromDate=" + fromDate + "&toDate=" + toDate + "&sbuId=" + sbuId + "&projectId=" + projectId;

            string fileName;
            string status = myPDF.GeneratePDFVoucher(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> RVReportDatapdf(string fromDate, string toDate, int? sbuId, int? projectId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string rptType = "RECEIVEPAYMENT";

            //string url = scheme + "://" + host + "/Accounting/AccountingReport/RVReportDataBrac?fromDate=" + fromDate + "&toDate=" + toDate + "&sbuId=" + sbuId + "&projectId=" + projectId;

            var data = await salaryService.GetReportFormatByReportType(rptType);
            string url = scheme + "://" + host + "/Accounting/AccountingReport/" + data.FirstOrDefault().reportFormat + "?fromDate=" + fromDate + "&toDate=" + toDate + "&sbuId=" + sbuId + "&projectId=" + projectId;

            string fileName;
            string status = myPDF.GenerateLandscapePDFA4(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        public IActionResult BEReportDatapdf(string fromDate, string toDate, int? sbuId, int? projectId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Accounting/AccountingReport/BEReportDataBrac?fromDate=" + fromDate + "&toDate=" + toDate + "&sbuId=" + sbuId + "&projectId=" + projectId;

            string fileName;
            string status = myPDF.GeneratePDFVoucher(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        public IActionResult BEReportDatapdfP(int Id,int? partnerId, int? sbuId, int? projectId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            //string url = scheme + "://" + host + "/Accounting/AccountingReport/PLReportData?salaryYearId=" + salaryYearId;
            string url = scheme + "://" + host + "/Accounting/AccountingReport/BEReportDataBracp?Id=" + Id+ "&partnerId=" + partnerId + "&sbuId=" + sbuId + "&projectId=" + projectId;

            string fileName;
            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        #endregion

        #region Balance Sheet

        [AllowAnonymous]
        public async Task<ActionResult> BalanceSheetReportData(string fromDate, string toDate, int? sbuId, int? projectId)
        //public async Task<ActionResult> BalanceSheetReportData(int? salaryYearId)
        {
            //ViewBag.toDate = toDate;
            //ViewBag.lasttoDate = ltoDate;
            var model = new FinancialReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                balanceSheetViewModels = await reportingService.GetBalanceSheetData(fromDate, toDate, sbuId, projectId),
                profitAndLossAccountViewModels = await reportingService.GetProfitLossACData(fromDate, toDate, sbuId, projectId)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> BalanceSheetReportDataBrac(string fromDate, string toDate, int? sbuId, int? projectId)
        {
            ViewBag.previousYear = Convert.ToDateTime(toDate).Year - 1;
            ViewBag.currentYear = Convert.ToDateTime(fromDate).Year;
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            var model = new FinancialReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                balanceSheetViewModels = await reportingService.GetBalanceSheetData(fromDate, toDate, sbuId, projectId),
                profitAndLossAccountViewModels = await reportingService.GetProfitLossACData(fromDate, toDate, sbuId, projectId)
            };
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult BalanceSheetReportDatapdf(string fromDate, string toDate, int? sbuId, int? projectId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            
            string url = scheme + "://" + host + "/Accounting/AccountingReport/BalanceSheetReportDataBrac?fromDate=" + fromDate + "&toDate=" + toDate + "&sbuId=" + sbuId + "&projectId=" + projectId;

            string fileName;
            string status = myPDF.GeneratePDFVoucher(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        #endregion

        #region API Section
        [Route("global/api/GetVoucherType/")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetVoucherType()
        {
            return Json(await voucherTypeService.GetVoucherType());
        }

        [Route("global/api/GetLedger/")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetLedger()
        {

            return Json(await ledgerService.GetLedger());
        }
        [Route("global/api/GetLedgerbysbuId/{id}")]

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetLedgerbysbuId(int id)
        {
           
            return Json(await ledgerService.GetLedgerLedgerBySbu(id));
        }
        [Route("global/api/GetBudget/")]

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetBudget()
        {
            var data = await budgetRequsitionMasterService.GetBudgetRequsitionMasterFin();


            return Json(data);
        }
        [Route("global/api/getAllpartnetBybudgetIdId/{budgetId}")]

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> getAllpartnetBybudgetIdId(int budgetId)
        {
            var data = await budgetRequsitionMasterService.GetBudgetRequsitionDetailFinBymasterId(budgetId);
            var partnets = data.Select(x => x.partner).Distinct();

            return Json(partnets);
        }
        [Route("global/api/GetCOA/{sbuId}/{projectId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCOA(int? sbuId, int? projectId)
        {
            return Json(await reportingService.ChartOfAccountdataViewModel(sbuId, projectId));
        }

        [Route("global/api/GetAllSubLedger/")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllSubLedger()
        {
            return Json(await ledgerService.GetAllSubLedger());
        }

        [Route("global/api/GetAllSubLedgerBySbu/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllSubLedgerBySbu(int id)
        {
            return Json(await ledgerService.GetAllSubLedgerBySbu(id));
        }

        [Route("global/api/GetAllSubLedgerbyledger/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllSubLedgerbyledger(int id)
        {
            return Json(await ledgerService.GetAllSubLedgerbyledger(id));
        }

        [Route("global/api/GetLedgerBookReportData/{ledgerId}/{subledgerId}/{fromDate}/{toDate}/{sbuId}/{projectId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetLedgerBookReportData(int ledgerId, int subledgerId, DateTime fromDate, DateTime toDate, int sbuId, int projectId)
        {
            return Json(await reportingService.GetLedgerBookReportDatad(ledgerId, subledgerId, fromDate, toDate, sbuId, projectId));
        }

        [Route("global/api/GetSubLedgerBookReportData/{subledgerId}/{fromDate}/{toDate}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetSubLedgerBookReportData(int subledgerId, DateTime fromDate, DateTime toDate)
        {
            return Json(await reportingService.GetSubLedgerBookReportDatad(subledgerId, fromDate, toDate));
        }

        [Route("global/api/GetDayBookReportData/{voucherTypeId}/{fromDate}/{toDate}/{sbuId}/{projectId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDayBookReportData(int voucherTypeId, DateTime fromDate, DateTime toDate,int? sbuId, int? projectId)
        {
            return Json(await reportingService.GetDayBookReportDatad(voucherTypeId, fromDate, toDate, sbuId, projectId));
        }

        [Route("global/api/GetTrialBalanceReportData/{fromDate}/{toDate}/{sbuId}/{projectId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetTrialBalanceReportData(DateTime fromDate, DateTime toDate, int? sbuId, int? projectId)
        {
            return Json(await reportingService.GetTrialBalanceReportData(fromDate, toDate, sbuId, projectId));
        }

        [Route("global/api/GetProfitLossACReportData/{fromDate}/{toDate}/{sbuId}/{projectId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProfitLossACReportData(string fromDate, string toDate, int? sbuId, int? projectId)
        {
            return Json(await reportingService.GetProfitLossACData(fromDate, toDate, sbuId, projectId));
        }

        [Route("global/api/GetBalanceSheetData/{fromDate}/{toDate}/{sbuId}/{projectId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetBalanceSheetData(string fromDate, string toDate, int? sbuId, int? projectId)
        {
            return Json(await reportingService.GetBalanceSheetData(fromDate, toDate, sbuId, projectId));
        }

        [Route("global/api/GetdailyBillReceive/{supplierId}/{fromDate}/{toDate}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetdailyBillReceive(int supplierId, DateTime fromDate, DateTime toDate)
        {
            return Json(await reportingService.GetdailyBillReceive(supplierId, fromDate, toDate));
        }
        [Route("global/api/GetCCWiseReport/{ccId}/{fromDate}/{toDate}/{sbuId}/{projectId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCCWiseReport(int ccId, DateTime fromDate, DateTime toDate, int? sbuId, int? projectId)
        {
            return Json(await reportingService.GetCCWiseLedgerReportViewModelsT(ccId, fromDate, toDate, sbuId, projectId));
        }

        [Route("global/api/GetLedgerDetailsData/{ledgerId}/{fromDate}/{toDate}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetLedgerDetailsData(int ledgerId, DateTime fromDate, DateTime toDate)
        {
            return Json(await reportingService.LedgerDetailsViewModels(ledgerId, fromDate, toDate));
        }       

        [Route("global/api/GetCostCentres/")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCostCentres()
        {
            return Json(await costCentreService.GetCostCentres());
        }

        [Route("global/api/GetSBU/")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetSBU()
        {
            string username = HttpContext.User.Identity.Name;
            var userinfo = await userInfo.GetUserInfoByUser(username);
            ViewBag.branchid = userinfo?.specialBranchUnitId;

            string userName = userinfo?.UserName;
            if (userName == "biplab" || userName == "suza")
            {
                return Json(await specialBranchUnitService.GetSpecialBranchUnit());
            }
            else
            {
                return Json(await specialBranchUnitService.GetSpecialBranchUnitByUserBranchId(ViewBag.branchid));
            }

        }

        [Route("global/api/GetProject/{Id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProject(int Id)
        {
            return Json(await projectService.GetProjectBySBUId(Id));
        }
        #endregion
    }
}