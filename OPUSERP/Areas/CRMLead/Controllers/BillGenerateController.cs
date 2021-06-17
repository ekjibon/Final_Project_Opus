using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.OtherSales.Services.Sales.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMLead.Controllers
{
    [Area("CRMLead")]
    public class BillGenerateController : Controller
    {
        private readonly IBillGenerateService billGenerateService;
        private readonly IERPCompanyService eRPCompanyService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IOSalesInvoiceMasterService salesInvoiceMasterService;
        private readonly IOSalesInvoiceDetailService salesInvoiceDetailService;
        private readonly ICustomerService customerService;
        private readonly IUserInfoes userInfoes;
        private readonly IAgreementService agreementService;
        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;

        public BillGenerateController(IBillGenerateService billGenerateService, IAgreementService agreementService, IUserInfoes userInfoes, ICustomerService customerService, IOSalesInvoiceDetailService salesInvoiceDetailService, IOSalesInvoiceMasterService salesInvoiceMasterService, IERPCompanyService eRPCompanyService, IHostingEnvironment _hostingEnvironment, IConverter converter)
        {
            this.billGenerateService = billGenerateService;
            this.eRPCompanyService = eRPCompanyService;
            this.salesInvoiceMasterService = salesInvoiceMasterService;
            this.salesInvoiceDetailService = salesInvoiceDetailService;
            this.customerService = customerService;
            this.userInfoes = userInfoes;
            this.agreementService = agreementService;


            this._hostingEnvironment = _hostingEnvironment;
            myPDF = new MyPDF(_hostingEnvironment, converter);
            rootPath = _hostingEnvironment.ContentRootPath;
        }

        #region bill Generate
        public async Task<ActionResult> Index()
        {
            var bill = await billGenerateService.GetAllBillGenerate();
            int Cbill = 0;
            Cbill = bill.OrderByDescending(x=>x.Id).FirstOrDefault().Id;
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy");
            string billNo = idate + '/' + (Cbill + 1);

            BillGenerateViewModel model = new BillGenerateViewModel
            {
                //clients = await billGenerateService.BillGeneratePendingList(0),
                bankBranchDetails = await billGenerateService.GetBankBranchDetails()
            };
            ViewBag.billNo = billNo;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] BillGenerateViewModel model)
        {
            var bill = await billGenerateService.GetAllBillGenerate();
            int Cbill = 0;
            Cbill = bill.OrderByDescending(x => x.Id).FirstOrDefault().Id;
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy");

            string agreementCategory = "";
            var agglist = await billGenerateService.GetBillGenerateInfoById(Convert.ToInt32(model.approvedforCroId));
            //foreach (var item in agglist.)
            //{
            agreementCategory = agglist.agreementDetails.agreement.agreementCategory.agreementCategoryName;
            //}

            string billNo = agreementCategory + '/' + idate + '/' + (Cbill + 1);
            string billNumber = "";

            if (model.billGenerateId == 0)
            {
                billNumber = billNo;
            }
            else
            {
                billNumber = model.billNo;
            }

            if (!ModelState.IsValid)
            {
                //model.clients = await billGenerateService.BillGeneratePendingList(0);
                model.bankBranchDetails = await billGenerateService.GetBankBranchDetails();
                return View(model);
            }

            int billgenerateId = 0;
           
            int Cpurchase = 0;
            var sale = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.invoiceDate).ToString("yyyyMMdd") == Convert.ToDateTime(model.billDate).ToString("yyyyMMdd")).Count();
            idate = Convert.ToDateTime(model.billDate).ToString("yyyy-MM-dd");
            string issueNo = "Sale" + '-' + idate + '-' + (Cpurchase + 1);
          

            var relcustomer = await customerService.GetRelSupplierCustomerResourseByLeadId((int)agglist.agreementDetails.agreement.leadsId);
            SalesInvoiceMaster datas = new SalesInvoiceMaster
            {
                Id = 0,
                relSupplierCustomerResourseId = relcustomer.Id,
                invoiceDate = model.billDate,
                paymentDate = model.billDate,
                invoiceNumber = issueNo,//model.salesInvoiceNo,
                totalAmount = model.ratingAmount,
                VATOnTotal = model.vatAmount,
                TAXOnTotal = 0,
                vds = 0,
                //DiscountOnTotal = model.grossDiscount,
                NetTotal = model.totalAmount,
                //storeId=model.storeId,
                isClose = 0,
                isStockClose = 0
            };
            var masterId = await salesInvoiceMasterService.SaveSalesInvoiceMaster(datas);


            SalesInvoiceDetail data1 = new SalesInvoiceDetail
            {
                Id = 0,
                //itemPriceFixingId = 0,
                quantity = 1,
                salesInvoiceMasterId = masterId,

                vatAmount =model.vatAmount,
                taxAmount = 0,
                lineTotal = model.ratingAmount



            };
            await salesInvoiceDetailService.SaveSalesInvoiceDetail(data1);

            var approvecrodatat = await agreementService.GetApprovedforCroById((int)model.approvedforCroId);
            BillGenerate data = new BillGenerate
            {
                Id = model.billGenerateId,
                approvedforCroId = model.approvedforCroId,
                bankBranchDetailsId = model.bankBranchDetailsId,
                billNo = billNumber,
                billDate = model.billDate,
                ratingAmount = model.ratingAmount,
                totalAmount = model.totalAmount,
                remarks = model.remarks,
                salesInvoiceMasterId= masterId,
                vatAmount=model.vatAmount,
                invoiceAmount=model.invoiceAmount,
                invoiceOrder=model.invoiceOrder,
                agreementDetailssId= approvecrodatat.agreementDetailsId,
                agreementtId=approvecrodatat.agreementDetails.agreementId,
                leadssId=approvecrodatat.agreementDetails.agreement.leadsId
            };

            billgenerateId = await billGenerateService.SaveBillGenerate(data);

            #region Save History           

            BillGenerateHistory leadsData = new BillGenerateHistory
            {
                Id = 0,
                billGenerateId = billgenerateId,
                billNo = billNumber,
                billDate = model.billDate,
                ratingAmount = model.agreementAmount,
                totalAmount = model.totalAmount
            };
            await billGenerateService.SaveBillGenerateHistory(leadsData);
            #endregion

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateBilll([FromForm] BillGenerateViewModel model)
        {

            if (!ModelState.IsValid)
            {
                //model.clients = await billGenerateService.BillGeneratePendingList(0);
                model.bankBranchDetails = await billGenerateService.GetBankBranchDetails();
                return View(model);
            }
            string agreementCategory = "";
            var agglist = await billGenerateService.GetBillGenerateInfoById(Convert.ToInt32(model.approvedforCroId));
           
            agreementCategory = agglist.agreementDetails.agreement.agreementCategory.agreementCategoryName;
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy");
            int Cpurchase = 0;
            var sale = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.invoiceDate).ToString("yyyyMMdd") == Convert.ToDateTime(model.billDate).ToString("yyyyMMdd")).Count();
            idate = Convert.ToDateTime(model.billDate).ToString("yyyy-MM-dd");
            string issueNo = "Sale" + '-' + idate + '-' + (Cpurchase + 1);


            var relcustomer = await customerService.GetRelSupplierCustomerResourseByLeadId((int)agglist.agreementDetails.agreement.leadsId);
            SalesInvoiceMaster datas = new SalesInvoiceMaster
            {
                Id = 0,
                relSupplierCustomerResourseId = relcustomer.Id,
                invoiceDate = model.billDate,
                paymentDate = model.billDate,
                invoiceNumber = issueNo,//model.salesInvoiceNo,
                totalAmount = model.ratingAmount,
                VATOnTotal = model.vatAmount,
                TAXOnTotal = 0,
                vds = 0,
                //DiscountOnTotal = model.grossDiscount,
                NetTotal = model.totalAmount,
                //storeId=model.storeId,
                isClose = 0,
                isStockClose = 0
            };
            var masterId = await salesInvoiceMasterService.SaveSalesInvoiceMaster(datas);


            SalesInvoiceDetail data1 = new SalesInvoiceDetail
            {
                Id = 0,
                //itemPriceFixingId = 0,
                quantity = 1,
                salesInvoiceMasterId = masterId,

                vatAmount = model.vatAmount,
                taxAmount = 0,
                lineTotal = model.ratingAmount



            };
            await salesInvoiceDetailService.SaveSalesInvoiceDetail(data1);
            var approvecrodatat = await agreementService.GetApprovedforCroById((int)model.approvedforCroId);
            //int billgenerateId = 0;
            BillGenerate data = new BillGenerate
            {
                Id = model.billGenerateId,
                approvedforCroId = model.approvedforCroId,
                bankBranchDetailsId = model.bankBranchDetailsId,
                billNo = model.billNo,
                billDate = model.billDate,
                invoiceOrder=model.invoiceOrder,
                ratingAmount = model.agreementAmount,
                totalAmount = model.totalAmount,
                remarks = model.remarks,
                salesInvoiceMasterId=masterId,
                agreementDetailssId = approvecrodatat.agreementDetailsId,
                agreementtId = approvecrodatat.agreementDetails.agreementId,
                leadssId = approvecrodatat.agreementDetails.agreement.leadsId


            };

            int billgenerateId = await billGenerateService.SaveBillGenerate(data);


            #region Save History           

            BillGenerateHistory leadsData = new BillGenerateHistory
            {
                Id = 0,
                billGenerateId = billgenerateId,
                billNo = model.billNo,
                billDate = model.billDate,
                ratingAmount = model.agreementAmount,
                totalAmount = model.totalAmount
            };
            await billGenerateService.SaveBillGenerateHistory(leadsData);
            #endregion

            return RedirectToAction(nameof(BillGenerateList));
        }

        [HttpGet]
        public async Task<IActionResult> GetRatingYearForBillGenerate(int clientId)
        {
            return Json(await billGenerateService.GetRatingYearForBillGenerate(clientId));
        }

        [HttpGet]
        public async Task<IActionResult> GetBillGenerateInfoById(int id)
        {
            return Json(await billGenerateService.GetBillGenerateInfoById(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetBankDetails(int detailsId)
        {
            var data = await billGenerateService.GetBankBranchDetails();
            return Json(data.Where(a => a.Id == detailsId).FirstOrDefault());
        }

        [HttpGet]
        public async Task<IActionResult> BillGeneratePendingListById(int id)
        {
            return Json(await billGenerateService.BillGeneratePendingListById(id));
        }

        [HttpGet]
        public async Task<IActionResult> InvoiceListById(int id)
        {
            return Json(await billGenerateService.GetBillGenerateByCroId(id));
        }

        [HttpGet]
        public async Task<IActionResult> BillGeneratePendingList()
        {
            return Json(await billGenerateService.BillGeneratePendingList(0));
        }
        #endregion

        #region Bill Generate List
        public async Task<IActionResult> BillGenerateList()
        {
            var bill = await billGenerateService.GetAllBillGenerate();
            int Cbill = 0;
            Cbill = Cbill = bill.OrderByDescending(x => x.Id).FirstOrDefault().Id;
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy");
            string billNo = idate + '/' + (Cbill + 1);
            string user = HttpContext.User.Identity.Name;
            BillGenerateViewModel model = new BillGenerateViewModel()
            {
                //billGenerates = await billGenerateService.GetAllBillGenerate(),
                bankBranchDetails = await billGenerateService.GetBankBranchDetails()
            };
            ViewBag.billNo = billNo;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBillGenerate()
        {
            return Json(await billGenerateService.GetAllBillGenerateBySP());
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllBillDueList()
        //{
        //    var data = await billGenerateService.GetAllBillGenerateBySP();
        //    return Json(data.Where(a=>a.du));
        //}
        #endregion

        #region Invoice Report

        [AllowAnonymous]
        public IActionResult InvoiceReport(int invoiceId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;

            string url = scheme + "://" + host + "/CRMLead/BillGenerate/InvoiceReportPdf?invoiceId=" + invoiceId;


            string status = myPDF.GenerateCRMPDF(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> InvoiceReportPdf(int invoiceId)
        {
            try
            {
                var model = new BillGenerateViewModel
                {
                    billGenerateReportViewModels = await billGenerateService.GetInvoiceReportByInvoiceId(invoiceId),
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
        public IActionResult InvoiceReportWL(int invoiceId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;

            string url = scheme + "://" + host + "/CRMLead/BillGenerate/InvoiceReportPdfWL?invoiceId=" + invoiceId;


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
        public async Task<IActionResult> InvoiceReportPdfWL(int invoiceId)
        {
            try
            {
                var model = new BillGenerateViewModel
                {
                    billGenerateReportViewModels = await billGenerateService.GetInvoiceReportByInvoiceId(invoiceId),
                    companies = await eRPCompanyService.GetAllCompany(),
                };
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> getbankbranchlist()
        {
            var data = await billGenerateService.GetBankBranchDetails();
            return Json(data.Where(x=>x.collectionType=="Yes"));
        }
        #endregion
    }
}