using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Accounting.Services.Voucher.Interfaces;
using OPUSERP.Areas.OtherSales.Models;
using OPUSERP.Areas.Purchase.Models;
using OPUSERP.Distribution.Services.MasterData.Interfaces;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.OtherSales.Services.Sales.Interfaces;
using OPUSERP.SCM.Services.PurchaseOrder.Interface;

namespace OPUSERP.Areas.OtherSales.Controllers
{
    [Authorize]
    [Area("OtherSales")]
    public class SalesCollectionController : Controller
    {
        private readonly IOSalesInvoiceMasterService salesInvoiceMasterService;
        private readonly IOSalesInvoiceDetailService salesInvoiceDetailService;
        private readonly IOSalesCollectionService salesCollectionService;
        private readonly IOSalesCollectionDetailsService salesCollectionDetailsService;
        private readonly IERPCompanyService eRPCompanyService;

        private readonly ICustomerService customerService;
        private readonly ILedgerService ledgerService;
        private readonly IVoucherService voucherService;
        private readonly IUserInfoes userInfoes;
        private readonly string rootPath;
        private readonly MyPDF myPDF;
        private readonly IPurchaseOrderService purchaseOrderService;
        private readonly IPaymentModeService paymentModeService;
        //private readonly IStoreService storeService;
        private readonly IERPModuleService eRPModuleService;
        private readonly IDistributorTypeService distributorTypeService;


        public SalesCollectionController(IVoucherService voucherService, IDistributorTypeService distributorTypeService, IERPModuleService eRPModuleService, IOSalesInvoiceDetailService salesInvoiceDetailService, IERPCompanyService eRPCompanyService, IPaymentModeService paymentModeService, ILedgerService ledgerService, IOSalesInvoiceMasterService salesInvoiceMasterService, IOSalesCollectionService salesCollectionService, IOSalesCollectionDetailsService salesCollectionDetailsService, IHostingEnvironment hostingEnvironment, IConverter converter, ICustomerService customerService, IUserInfoes userInfoes, IPurchaseOrderService purchaseOrderService)
        {
            this.salesInvoiceMasterService = salesInvoiceMasterService;
            this.salesInvoiceDetailService = salesInvoiceDetailService;
            this.salesCollectionService = salesCollectionService;
            this.eRPModuleService = eRPModuleService;
            this.distributorTypeService = distributorTypeService;
            this.salesCollectionDetailsService = salesCollectionDetailsService;
            this.customerService = customerService;
            this.userInfoes = userInfoes;
            this.voucherService = voucherService;
            this.ledgerService = ledgerService;
            this.paymentModeService = paymentModeService;
            this.eRPCompanyService = eRPCompanyService;
            //this.storeService = storeService;
            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }
        // GET: SalesCollection
        public async Task<IActionResult> Index(int id)
        {
            string user = HttpContext.User.Identity.Name;
            var userinfo = await userInfoes.GetUserInfoByUser(user);
            SalesCollectionViewModel model = new SalesCollectionViewModel
            {
                customerWithDues = await salesCollectionService.GetCustomerWithDue(),
                paymentModes=await paymentModeService.GetAllPaymentMode(),
            };
            return View(model);
        }

        public async Task<IActionResult> IndexNew(int id)
        {
            string user = HttpContext.User.Identity.Name;
            var userinfo = await userInfoes.GetUserInfoByUser(user);
            SalesCollectionViewModel model = new SalesCollectionViewModel
            {
                paymentInvoiceWithDues = await salesInvoiceMasterService.GetDueSalesInvoice(id),
                paymentModes = await paymentModeService.GetAllPaymentMode()

            };

            return View(model);
        }
        public async Task<IActionResult> IndexNewH(int id)
        {
            string user = HttpContext.User.Identity.Name;
            var userinfo = await userInfoes.GetUserInfoByUser(user);
            SalesCollectionViewModel model = new SalesCollectionViewModel
            {
                paymentInvoiceWithDues = await salesInvoiceMasterService.GetDueSalesInvoice(id),
                paymentModes = await paymentModeService.GetAllPaymentMode()

            };

            return View(model);
        }
         public async Task<IActionResult> IndexNewS(int id)
        {
            string user = HttpContext.User.Identity.Name;
            var userinfo = await userInfoes.GetUserInfoByUser(user);
            SalesCollectionViewModel model = new SalesCollectionViewModel
            {
                paymentInvoiceWithDues = await salesInvoiceMasterService.GetDueSalesInvoice(id),
                paymentModes = await paymentModeService.GetAllPaymentMode()

            };

            return View(model);
        }

        public async Task<IActionResult> SaleInvoiceBill(int id)
        {
            var purchase = await salesCollectionService.GetSalesCollectionBillInfo();
            int Cpurchase = 0;
            Cpurchase = purchase.Where(x => Convert.ToDateTime(x.billPaymentDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "BillIssue-No" + '-' + idate + '-' + (Cpurchase + 1);
            ViewBag.issueNo = issueNo;

            SalesCollectionViewModel model = new SalesCollectionViewModel
            {
                customerWithDues = await salesCollectionService.GetCustomerWithDue(),
                paymentModes = await paymentModeService.GetAllPaymentMode()
            };

            return View(model);
        }

        public async Task<IActionResult> SaleInvoiceBillList()
        {

            SalesCollectionViewModel model = new SalesCollectionViewModel
            {
                salesCollectionBillInfos = await salesCollectionService.GetSalesCollectionBillInfo(),
                
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> SaleInvoiceBillListByID(int id)
        {
            SalesCollectionViewModel model = new SalesCollectionViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                salesCollection = await salesCollectionService.GetSalesCollectionById(id),
                salesCollectionDetails = await salesCollectionDetailsService.GetAllSalesCollectionDetailByCollectionId(id),
                salesCollectionBillInfobyid = await salesCollectionService.GetSalesCollectionBillInfoById(id),
            };
            return View(model);
        }

        //PrintPDF
        [AllowAnonymous]
        public IActionResult SaleInvoiceBilLPdf(int id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/OtherSales/SalesCollection/SaleInvoiceBillListByID/" + id;

            string fileName;
            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaleInvoiceBill([FromForm] BillReceivedViewModel model)
        {
            //return Json(model);

            var purchase = await salesCollectionService.GetSalesCollectionBillInfo();
            int Cpurchase = 0;
            Cpurchase = purchase.Where(x => Convert.ToDateTime(x.billPaymentDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "BillIssue-No" + '-' + idate + '-' + (Cpurchase + 1);

            SalesCollectionBillInfo data = new SalesCollectionBillInfo
            {
                relSupplierCustomerResourseId = model.customerId,
                billPaymentNo = issueNo,
                Amount = model.BillTotal,
                billPaymentDate = model.poDate,
                salesInvoiceMasterId = model.invoiceId,
                dueAmount = model.DueDate,
                ispaid = 0,
            };
            int saleCollectionId = await salesCollectionService.SaveSalesCollectionBillInfo(data);

            return RedirectToAction(nameof(SaleInvoiceBillList));
        }



        public async Task<IActionResult> SalesReturn(int id)
        {
            var purchase = await salesCollectionService.GetSalesCollectionBillInfo();
            int Cpurchase = 0;
            Cpurchase = purchase.Where(x => Convert.ToDateTime(x.billPaymentDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "SalesReturn-No" + '-' + idate + '-' + (Cpurchase + 1);
            ViewBag.issueNo = issueNo;

            SalesCollectionViewModel model = new SalesCollectionViewModel
            {
                relSupplierCustomerResourses = await customerService.GetAllRelSupplierCustomerResourseByRoleId(4),
            };

            return View(model);
        }

        public async Task<IActionResult> SalesReturnList()
        {
            SalesCollectionViewModel model = new SalesCollectionViewModel
            {
                salesReturnInvoiceMasters = await salesInvoiceMasterService.GetAllSalesReturnInvoiceMaster(),
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SalesReturn([FromForm] PurchaseReturnViewModel model)
        {
            //return Json(model);

            var purchase = await salesCollectionService.GetSalesCollectionBillInfo();
            int Cpurchase = 0;
            Cpurchase = purchase.Where(x => Convert.ToDateTime(x.billPaymentDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "SalesReturn-No" + '-' + idate + '-' + (Cpurchase + 1);
            ViewBag.issueNo = issueNo;

            SalesReturnInvoiceMaster data = new SalesReturnInvoiceMaster
            {
                relSupplierCustomerResourseId = model.customerId,
                invoiceNumber = issueNo,
                totalAmount = model.netTotal,
                invoiceDate = model.poDate,
                NetTotal = model.netTotal,
                isClose = 0,
                isStockClose = 0,
            };
            int saleCollectionId = await salesInvoiceMasterService.SaveSalesReturnInvoiceMaster(data);

            if (model.selectPaymentHeadIds != null)
            {
                for (int i = 0; i < model.selectPaymentHeadIds.Length; i++)
                {
                    SalesReturnInvoiceDetail purchaseReturnInfoDetail = new SalesReturnInvoiceDetail
                    {
                        salesReturnInvoiceMasterId = saleCollectionId,
                        salesInvoiceDetailId = model.selectPaymentHeadIds[i],
                        quantity = model.selectPaymentHeadAmounts[i],
                    };
                    await salesInvoiceDetailService.SaveSalesReturnInvoiceDetail(purchaseReturnInfoDetail);
                }
            }

            return RedirectToAction("ReturnInvoiceDetails", "SalesInvoice", new
            {
                Id = saleCollectionId,
            });
        }




        // GET: SalesCollection
        public async Task<IActionResult> CustomerList()
        {
            string user = HttpContext.User.Identity.Name;
            //var userinfo = await userInfoes.GetUserInfoByUser(user);
            SalesCollectionViewModel model = new SalesCollectionViewModel
            {
                customerWithDues = await salesCollectionService.GetCustomerWithDue(),
                salesCollections = await salesCollectionService.GetAllSalesCollection(),
                relSupplierCustomerResourses = await salesCollectionService.GetDuesCustomerList(),
                //paymentModes =  await purchaseOrderService.GetPaymentMode()
                //storeAssigns = await storeService.GetAllStoreAssignByUserId(userinfo.ID)
            };
            //if (HttpContext.Session.GetInt32("storeId") == null)
            //{
            //    ViewBag.storeId = model.storeAssigns.Where(x => x.isDefault == "Yes").FirstOrDefault().Id;
            //}
            //else
            //{
            //    ViewBag.storeId = HttpContext.Session.GetInt32("storeId");
            //}
            return View(model);
        }

        // POST: SalesCollection/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IndexNew([FromForm] SalesCollectionViewModel model)
        {
            if (model.total < 1)
            {
                model.paymentInvoiceWithDues = await salesInvoiceMasterService.GetDueSalesInvoice((int)model.relSupplierCustomerResourseId);

                ModelState.AddModelError(string.Empty, "You have to pay minimum 1 taka.");
                return View(model);
            }

            var purchase = await salesCollectionService.GetAllSalesCollection();
            int Cpurchase = 0;
            Cpurchase = purchase.Where(x => Convert.ToDateTime(x.collectionDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "Collection-No-" + '-' + idate + '-' + (Cpurchase + 1);


            SalesCollection data = new SalesCollection
            {
                relSupplierCustomerResourseId = model.relSupplierCustomerResourseId,
                salesInvoiceMasterId=model.invoicemasterId,
                collectionNumber = issueNo,
                collectionAmount = model.total,
                collectionDate = model.date,
                companyId = 3,
                remarks = model.remarks,
                paymentModeId = model.paymentModeId,
                Amount = model.Amount,
                bankName = model.bankName,
                checqueNo = model.checqueNo,
                trxNo = model.trxNo,
                storeId = model.storeId
            };
            int saleCollectionId = await salesCollectionService.SaveSalesCollection(data);




            return RedirectToAction(nameof(IndexNew));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IndexNewH([FromForm] SalesCollectionViewModel model)
        {
            if (model.total < 1)
            {
                model.paymentInvoiceWithDues = await salesInvoiceMasterService.GetDueSalesInvoice((int)model.relSupplierCustomerResourseId);

                ModelState.AddModelError(string.Empty, "You have to pay minimum 1 taka.");
                return View(model);
            }

            var purchase = await salesCollectionService.GetAllSalesCollection();
            int Cpurchase = 0;
            Cpurchase = purchase.Where(x => Convert.ToDateTime(x.collectionDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "Collection-No-" + '-' + idate + '-' + (Cpurchase + 1);


            SalesCollection data = new SalesCollection
            {
                relSupplierCustomerResourseId = model.relSupplierCustomerResourseId,
                salesInvoiceMasterId=model.invoicemasterId,
                collectionNumber = issueNo,
                collectionAmount = model.total,
                collectionDate = model.date,
                companyId = 3,
                remarks = model.remarks,
                paymentModeId = model.paymentModeId,
                Amount = model.Amount,
                bankName = model.bankName,
                checqueNo = model.checqueNo,
                trxNo = model.trxNo,
                storeId = model.storeId
            };
            int saleCollectionId = await salesCollectionService.SaveSalesCollection(data);




            return RedirectToAction(nameof(IndexNewH));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IndexNewS([FromForm] SalesCollectionViewModel model)
        {
            if (model.total < 1)
            {
                model.paymentInvoiceWithDues = await salesInvoiceMasterService.GetDueSalesInvoice((int)model.relSupplierCustomerResourseId);

                ModelState.AddModelError(string.Empty, "You have to pay minimum 1 taka.");
                return View(model);
            }

            var purchase = await salesCollectionService.GetAllSalesCollection();
            int Cpurchase = 0;
            Cpurchase = purchase.Where(x => Convert.ToDateTime(x.collectionDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "Collection-No-" + '-' + idate + '-' + (Cpurchase + 1);

            int? invMasterId = 0;
            if (model.invoicemasterId2 == null)
            {
                invMasterId = model.invoicemasterId;
            }
            else
            {
                invMasterId = model.invoicemasterId2;
            }

            SalesCollection data = new SalesCollection
            {
                relSupplierCustomerResourseId = model.relSupplierCustomerResourseId,
                salesInvoiceMasterId = invMasterId,
                collectionNumber = issueNo,
                collectionAmount = model.Amount,
                collectionDate = model.date,
                companyId = 3,
                remarks = model.remarks,
                paymentModeId = model.paymentModeId,
                Amount = model.Amount,
                bankName = model.bankName,
                checqueNo = model.checqueNo,
                trxNo = model.trxNo,
                storeId = model.storeId
            };
            int saleCollectionId = await salesCollectionService.SaveSalesCollection(data);
            return RedirectToAction(nameof(IndexNewS));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] SalesCollectionViewModel model)
        {
            if (model.total < 1)
            {
                model.paymentInvoiceWithDues = await salesInvoiceMasterService.GetDueSalesInvoice((int)model.relSupplierCustomerResourseId);
                //model.paymentModes = await paymentModeService.GetAllPaymentMode();
                ModelState.AddModelError(string.Empty, "You have to pay minimum 1 taka.");
                return View(model);
            }
            if (model.paymentModeId == 1)
            {
                if (model.total != model.cashAmount)
                {
                    model.paymentInvoiceWithDues = await salesInvoiceMasterService.GetDueSalesInvoice((int)model.relSupplierCustomerResourseId);
                    //model.paymentModes = await paymentModeService.GetAllPaymentMode();
                    ModelState.AddModelError(string.Empty, "Total & Cash amount not same.");
                    return View(model);
                }

            }
            else if (model.paymentModeId == 2)
            {
                if (model.total != model.bankAmount)
                {
                    model.paymentInvoiceWithDues = await salesInvoiceMasterService.GetDueSalesInvoice((int)model.relSupplierCustomerResourseId);
                    //model.paymentModes = await paymentModeService.GetAllPaymentMode();
                    ModelState.AddModelError(string.Empty, "Total & Bank amount not same.");
                    return View(model);
                }

            }
            else
            {
                if (model.total != model.bankAmount + model.cashAmount)
                {
                    model.paymentInvoiceWithDues = await salesInvoiceMasterService.GetDueSalesInvoice((int)model.relSupplierCustomerResourseId);
                    //model.paymentModes = await paymentModeService.GetAllPaymentMode();
                    ModelState.AddModelError(string.Empty, "Total and Bank & Cash  amount not same.");
                    return View(model);
                }
            }
            //var accountinfo = await voucherService.GetVoucherSetting();
            //var accountIdB = 0;
            //var accountIdC = 0;
            decimal bankAmount = 0;
            decimal cashAmount = 0;
            if (model.paymentModeId == 1)
            {
                cashAmount = (decimal)model.cashAmount;
                //accountIdC = (int)accountinfo.FirstOrDefault().cashAccountLedgerId;

            }
            else if (model.paymentModeId == 2)
            {
                bankAmount = (decimal)model.bankAmount;
                //accountIdB = (int)accountinfo.FirstOrDefault().bankAccountLedgerId;

            }
            else
            {
                cashAmount = (decimal)model.cashAmount;
                bankAmount = (decimal)model.bankAmount;
                //accountIdC = (int)accountinfo.FirstOrDefault().cashAccountLedgerId;
                //accountIdB = (int)accountinfo.FirstOrDefault().bankAccountLedgerId;

            }
            // 11647
            //var resource = await customerService.GetRelSupplierCustomerResourseById((int)model.relSupplierCustomerResourseId);
            //if (resource?.ledgerId == null)
            // {

            //         model.paymentInvoiceWithDues = await salesInvoiceMasterService.GetDueSalesInvoice((int)model.relSupplierCustomerResourseId);
            //         //model.paymentModes = await paymentModeService.GetAllPaymentMode();
            //         ModelState.AddModelError(string.Empty, "No ledger created for this customer yet.");
            //         return View(model);


            // }
            // var relledger = await ledgerService.GetledgerRelationByLedgerSubledgerId(11647,(int)resource.ledgerId);
            // if (relledger == null)
            // {

            //     model.paymentInvoiceWithDues = await salesInvoiceMasterService.GetDueSalesInvoice((int)model.relSupplierCustomerResourseId);
            //     //model.paymentModes = await paymentModeService.GetAllPaymentMode();
            //     ModelState.AddModelError(string.Empty, "No ledger created for this customer yet.");
            //     return View(model);


            // }
            // var LedgerRelInfoC = await ledgerService.GetledgerRelationById(accountIdC);
            // var LedgerRelInfoB = await ledgerService.GetledgerRelationById(accountIdB);
            // var LedgerRelInfoA = await ledgerService.GetledgerRelationById(relledger.Id);


            //return Json(model);

            var purchase = await salesCollectionService.GetAllSalesCollection();
            int Cpurchase = 0;
            Cpurchase = purchase.Where(x => Convert.ToDateTime(x.collectionDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "Collection-No-" + '-' + idate + '-' + (Cpurchase + 1);
            //return Json(model);

            SalesCollection data = new SalesCollection
            {
                relSupplierCustomerResourseId = model.relSupplierCustomerResourseId,
                collectionNumber = issueNo,
                collectionAmount = model.total,
                collectionDate = model.date,
                remarks = model.remarks,
                paymentModeId = model.paymentModeId,
                cashAmount = cashAmount,
                bankAmount = bankAmount,
                storeId = model.storeId
            };
            int saleCollectionId = await salesCollectionService.SaveSalesCollection(data);


            if (model.selectPaymentHeadIds != null)
            {
                for (var i = 0; i < model.selectPaymentHeadIds.Length; i++)
                {
                    SalesCollectionDetail data1 = new SalesCollectionDetail
                    {
                        salesCollectionId = saleCollectionId,
                        salesCollectionBillInfoId = model.selectPaymentHeadIds[i],
                        Amount = model.selectPaymentHeadAmounts[i],
                        collectionDate = model.date,
                        remarks = model.remarks
                    };
                    await salesCollectionDetailsService.SaveSalesCollectionDetail(data1);
                    if (model.selectPaymentHeadAmounts[i] == model.selectPaymentHeadDues[i])
                    {
                        //Update SalesInvoiceMaster isClose = 1;
                        await salesCollectionService.UpdateSalesCollectionBillInfoById((int)model.selectPaymentHeadIds[i]);
                    }
                }
            }


            return RedirectToAction(nameof(CustomerList));
        }

        // GET: SalesCollection
        [AllowAnonymous]
        public async Task<IActionResult> CollectionReport(int id)
        {
            SalesCollectionViewModel model = new SalesCollectionViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                salesCollection = await salesCollectionService.GetSalesCollectionById(id),
                salesCollectionDetails = await salesCollectionDetailsService.GetAllSalesCollectionDetailByCollectionId(id)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> CollectionReportNew(int id)
        {
            SalesCollectionViewModel model = new SalesCollectionViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                salesCollection = await salesCollectionService.GetSalesCollectionById(id),
                salesCollections = await salesCollectionService.GetSalesCollectionListById(id),
                salesCollectionDetails = await salesCollectionDetailsService.GetAllSalesCollectionDetailByCollectionId(id)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ReturnPaymentReport(int id)
        {
            SalesCollectionViewModel model = new SalesCollectionViewModel
            {
                billReturnPaymentMaster = await salesCollectionService.GetSalesReturnPaymentById(id),
                billReturnPaymentDetails = await salesCollectionDetailsService.GetAllBillReturnPaymentDetailByMasterId(id)
            };
            return View(model);
        }

        //PrintPDF
        [AllowAnonymous]
        public IActionResult ReturnPaymentReportAction(int id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/OtherSales/SalesCollection/ReturnPaymentReport/" + id;

            string fileName;
            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public IActionResult pdspdf(int id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/OtherSales/SalesCollection/CollectionReport/" + id;

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
        public IActionResult CollectionReportData(int id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/OtherSales/SalesCollection/CollectionReportNew/" + id;

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
        public async Task<IActionResult> CustomerCollectionReport()
        {
            SalesCollectionViewModel model = new SalesCollectionViewModel
            {
                relSupplierCustomerResourses = await customerService.GetAllRelSupplierCustomerResourse()
            };
            return View(model);
        }
        [AllowAnonymous]
        public async Task<IActionResult> CustomePaymentReport()
        {
            SalesCollectionViewModel model = new SalesCollectionViewModel
            {
                relSupplierCustomerResourses = await salesCollectionService.GetReturnCustomerList()
            };
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult CustomerCollectionpdf(int id, string name, string fromDate, string toDate)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            var storeId = HttpContext.Session.GetInt32("storeId");
            string url = scheme + "://" + host + "/OtherSales/SalesCollection/CustomerCollectionReportView?id=" + id + "&name=" + name + "&fromDate=" + fromDate + "&toDate=" + toDate + "&storeId=" + Convert.ToInt32(storeId);

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
        public IActionResult CustomerPaymentpdf(int id, string name, string fromDate, string toDate)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/OtherSales/SalesCollection/CustomerPaymentReportView?id=" + id + "&name=" + name + "&fromDate=" + fromDate + "&toDate=" + toDate;

            string fileName;
            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }



        [AllowAnonymous]
        public async Task<IActionResult> CustomerCollectionReportView(int id, string name, string fromDate, string toDate, int storeId)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            ViewBag.ID = id;
            string userName = User.Identity.Name;
            var rel = await customerService.GetRelSupplierCustomerResourseById(id);
            ViewBag.ContactNumber = rel.resource.mobile;
            ViewBag.Name = rel.resource.nameEnglish;

            SalesCollectionViewModel model = new SalesCollectionViewModel
            {
                customerCollectionReportVM = await salesInvoiceMasterService.GetSalesInvoiceRecipt(id, fromDate, toDate, Convert.ToInt32(storeId)),
                company = await ledgerService.Company(3)

            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> CustomerPaymentReportView(int id, string name, string fromDate, string toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            ViewBag.ID = id;
            string userName = User.Identity.Name;
            var rel = await customerService.GetRelSupplierCustomerResourseById(id);
            ViewBag.ContactNumber = rel.resource.mobile;
            ViewBag.Name = rel.resource.nameEnglish;
            SalesCollectionViewModel model = new SalesCollectionViewModel
            {
                customerCollectionReportVM = await salesInvoiceMasterService.GetSalesReturnInvoiceRecipt(id, fromDate, toDate)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> CustomerCollectionSummaryReport(int id)
        {

            SalesCollectionViewModel model = new SalesCollectionViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                relSupplierCustomerResourse = await customerService.GetRelSupplierCustomerResourseById(id),
                collectionSummaryReport = await salesCollectionService.GetSummaryCollection(id)
            };

            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> CustomerCollectionSummaryReportG(int id,int? moduleid)
        {
            //var modulename = await eRPModuleService.GetERPModuleById((int)moduleid);

            //var distributortype = await distributorTypeService.GetAllDistributorType();

            //int distributortypeId = 0;
            //if (distributortype != null)
            //{
            //    distributortypeId = distributortype.Where(x => x.name == modulename.moduleName).FirstOrDefault().Id;
            //}
           // var data = await salesCollectionService.GetSummaryCollection(id);
           
            SalesCollectionViewModel model = new SalesCollectionViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                relSupplierCustomerResourse = await customerService.GetRelSupplierCustomerResourseById(id),
                collectionSummaryReport = await salesCollectionService.GetSummaryCollection(id)
            };

            return Json(model);
        }
        [Route("global/api/CustomerType/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> CustomerType(int? id)
        {
            var modulename = await eRPModuleService.GetERPModuleById((int)id);

            var distributortype = await distributorTypeService.GetAllDistributorType();

            int distributortypeId = 0;
            if (distributortype != null)
            {
                distributortypeId = distributortype.Where(x => x.name == modulename.moduleName).FirstOrDefault().Id;
            }
          

            return Json(distributortypeId);
        }

        [AllowAnonymous]
        public IActionResult CustomerCollectionSummaryReportpdf(int id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/OtherSales/SalesCollection/CustomerCollectionSummaryReport?id=" + id;

            string fileName;
            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        #region API Section
        [Route("global/api/GetDueSalesInvoice/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDueSalesInvoice(int Id)
        {
            var data = await salesInvoiceMasterService.GetDueSalesInvoice(Id);
            return Json(data.Where(x => x.storeId == HttpContext.Session.GetInt32("storeId")));
        }

        [Route("global/api/GetDueSalesInvoiceWithoutStore/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDueSalesInvoiceWithoutStore(int Id)
        {
            return Json(await salesInvoiceMasterService.GetDueSalesInvoice(Id));
        }

        [Route("global/api/GetSalesCollectionBillInfoByInvoiceId/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetSalesCollectionBillInfoByInvoiceId(int Id)
        {
            return Json(await salesCollectionService.GetSalesCollectionBillInfoByInvoiceId(Id));
        }

         [Route("global/api/GetSalesCollectionBillInfoByCustomerId/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetSalesCollectionBillInfoByCustomerId(int Id)
        {
            return Json(await salesCollectionService.GetSalesCollectionBillInfoByCustomerId(Id));
        }

        [Route("global/api/GetDuesCustomerList/")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDuesCustomerList()
        {
            return Json(await salesCollectionService.GetDuesCustomerList());
        }

        [Route("global/api/GetCustomerListForSalesReport/")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCustomerListForSalesReport()
        {
            return Json(await salesCollectionService.GetCustomerListForSalesReport());
        }
        #endregion
    }
}