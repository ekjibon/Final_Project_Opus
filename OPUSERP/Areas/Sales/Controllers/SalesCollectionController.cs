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
using OPUSERP.Areas.Sales.Models;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.Data.Entity.Master;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.Sales.Data.Entity.Collection;
using OPUSERP.Sales.Services.Collection.Interface;
using OPUSERP.Sales.Services.Collection.Interfaces;
using OPUSERP.Sales.Services.Sales.Interface;
using OPUSERP.SCM.Services.PurchaseOrder.Interface;

namespace OPUSERP.Areas.Sales.Controllers
{
    [Authorize]
    [Area("Sales")]
    public class SalesCollectionController : Controller
    {
        private readonly ISalesInvoiceMasterService salesInvoiceMasterService;
        private readonly ISalesCollectionService salesCollectionService;
        private readonly ISalesCollectionDetailsService salesCollectionDetailsService;
       // private readonly Company
      
        private readonly ILeadsService customerService;
       // private readonly ILedgerService ledgerService;
       // private readonly IVoucherService voucherService;
       // private readonly IUserInfoes userInfoes;
        private readonly string rootPath;
        private readonly MyPDF myPDF;
        private readonly IPurchaseOrderService paymentModeService;
       private readonly IERPCompanyService eRPCompanyService;
       

        public SalesCollectionController(IERPCompanyService eRPCompanyService,IPurchaseOrderService paymentModeService,ISalesInvoiceMasterService salesInvoiceMasterService, ISalesCollectionService salesCollectionService, ISalesCollectionDetailsService salesCollectionDetailsService, IHostingEnvironment hostingEnvironment, IConverter converter, ILeadsService customerService)
        {
            this.salesInvoiceMasterService = salesInvoiceMasterService;
            this.salesCollectionService = salesCollectionService;
            this.salesCollectionDetailsService = salesCollectionDetailsService;
            this.customerService = customerService;
            this.paymentModeService = paymentModeService;
            this.eRPCompanyService = eRPCompanyService;
           
            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }
        // GET: SalesCollection
        public async Task<IActionResult> Index(int id)
        {
            string user = HttpContext.User.Identity.Name;
          //  var userinfo = await userInfoes.GetUserInfoByUser(user);
            SalesCollectionViewModel model = new SalesCollectionViewModel
            {
                paymentInvoiceWithDues = await salesInvoiceMasterService.GetDueSalesInvoice(id),
                paymentModes=await paymentModeService.GetPaymentMode(),
               // storeAssigns = await storeService.GetAllStoreAssignByUserId(userinfo.ID)
            };
           
            return View(model);
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
                paymentModes = await paymentModeService.GetPaymentMode(),
                //storeAssigns = await storeService.GetAllStoreAssignByUserId(userinfo.ID)
            };
           
            return View(model);
        }

        // POST: SalesCollection/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] SalesCollectionViewModel model)
        {
            if (model.total < 1)
            {
                model.paymentInvoiceWithDues = await salesInvoiceMasterService.GetDueSalesInvoice((int)model.relSupplierCustomerResourseId);
                model.paymentModes = await paymentModeService.GetPaymentMode();
                ModelState.AddModelError(string.Empty, "You have to pay minimum 1 taka.");
                return View(model);
            }
            if (model.paymentModeId == 1)
            {
                if (model.total != model.cashAmount)
                {
                    model.paymentInvoiceWithDues = await salesInvoiceMasterService.GetDueSalesInvoice((int)model.relSupplierCustomerResourseId);
                    model.paymentModes = await paymentModeService.GetPaymentMode();
                    ModelState.AddModelError(string.Empty, "Total & Cash amount not same.");
                    return View(model);
                }

            }
            else if (model.paymentModeId == 2)
            {
                if (model.total != model.bankAmount)
                {
                    model.paymentInvoiceWithDues = await salesInvoiceMasterService.GetDueSalesInvoice((int)model.relSupplierCustomerResourseId);
                    model.paymentModes = await paymentModeService.GetPaymentMode();
                    ModelState.AddModelError(string.Empty, "Total & Bank amount not same.");
                    return View(model);
                }

            }
            else
            {
                if (model.total != model.bankAmount+model.cashAmount)
                {
                    model.paymentInvoiceWithDues = await salesInvoiceMasterService.GetDueSalesInvoice((int)model.relSupplierCustomerResourseId);
                    model.paymentModes = await paymentModeService.GetPaymentMode();
                    ModelState.AddModelError(string.Empty, "Total and Bank & Cash  amount not same.");
                    return View(model);
                }
            }
         
           
            decimal bankAmount =0;
            decimal cashAmount = 0;
            if (model.paymentModeId == 1)
            {
                cashAmount = (decimal)model.cashAmount;
               

            }
            else if (model.paymentModeId == 2)
            {
                bankAmount = (decimal)model.bankAmount;
              

            }
            else
            {
                cashAmount = (decimal)model.cashAmount;
                bankAmount = (decimal)model.bankAmount;
               

            }
            // 11647
          




            var purchase = await salesCollectionService.GetAllSalesCollection();
            int Cpurchase = 0;
            Cpurchase = purchase.Where(x => Convert.ToDateTime(x.collectionDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "Collection-No-" + '-' + idate + '-' + (Cpurchase + 1);
            //return Json(model);

            SalesCollection data = new SalesCollection
            {
                leadsId = model.relSupplierCustomerResourseId,
                collectionNumber=issueNo,
                collectionAmount = model.total,
                collectionDate = model.date,
              //  companyId=1,
                remarks = model.remarks,
                paymentModeId=model.paymentModeId,
                cashAmount=cashAmount,
                bankAmount=bankAmount
            };
           int saleCollectionId =  await salesCollectionService.SaveSalesCollection(data);


            if (model.selectPaymentHeadIds != null)
            {
                for (var i = 0; i < model.selectPaymentHeadIds.Length; i++)
                {
                    SalesCollectionDetail data1 = new SalesCollectionDetail
                    {
                        salesCollectionId = saleCollectionId,
                        salesInvoiceMasterId = model.selectPaymentHeadIds[i],
                        Amount = model.selectPaymentHeadAmounts[i],
                        collectionDate = model.date,
                        remarks = model.remarks
                    };
                    await salesCollectionDetailsService.SaveSalesCollectionDetail(data1);
                    if(model.selectPaymentHeadAmounts[i]== model.selectPaymentHeadDues[i])
                    {
                        //Update SalesInvoiceMaster isClose = 1;
                        await salesInvoiceMasterService.UpdateSalesInvoiceMasterById((int)model.selectPaymentHeadIds[i]);
                    }
                }
            }

          


            //return Json(data);
            return RedirectToAction(nameof(CustomerList));
        }

        // GET: SalesCollection
        [AllowAnonymous]
        public async Task<IActionResult> CollectionReport(int id)
        {
            SalesCollectionViewModel model = new SalesCollectionViewModel
            {
                salesCollection = await salesCollectionService.GetSalesCollectionById(id),
                salesCollectionDetails = await salesCollectionDetailsService.GetAllSalesCollectionDetailByCollectionId(id)
            };
            return View(model);
        }

       

       
        [AllowAnonymous]
        public IActionResult pdspdf(int id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Sales/SalesCollection/CollectionReport/" + id;

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
        public async Task<IActionResult> CustomerCollectionReport()
        {
            SalesCollectionViewModel model = new SalesCollectionViewModel
            {
                relSupplierCustomerResourses = await salesCollectionService.GetCollectionCustomerList()
            };
            return View(model);
        }
        //[AllowAnonymous]
        //public async Task<IActionResult> CustomePaymentReport()
        //{
        //    SalesCollectionViewModel model = new SalesCollectionViewModel
        //    {
        //        relSupplierCustomerResourses = await salesCollectionService.GetReturnCustomerList()
        //    };
        //    return View(model);
        //}

        [AllowAnonymous]
        public IActionResult CustomerCollectionpdf(int id,string name, string fromDate, string toDate)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            var storeId = HttpContext.Session.GetInt32("storeId");
            string url = scheme + "://" + host + "/Sales/SalesCollection/CustomerCollectionReportView?id=" + id + "&name=" + name + "&fromDate=" + fromDate + "&toDate=" + toDate + "&storeId=" + Convert.ToInt32(storeId);

            string fileName;
            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        //[AllowAnonymous]
        //public IActionResult CustomerPaymentpdf(int id, string name, string fromDate, string toDate)
        //{
        //    string scheme = Request.Scheme;
        //    var host = Request.Host;

        //    string url = scheme + "://" + host + "/Sales/SalesCollection/CustomerPaymentReportView?id=" + id + "&name=" + name + "&fromDate=" + fromDate + "&toDate=" + toDate;

        //    string fileName;
        //    string status = myPDF.GeneratePDF(out fileName, url);

        //    if (status != "done")
        //    {
        //        return Content("<h1>Something Went Wrong</h1>");
        //    }

        //    var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
        //    return new FileStreamResult(stream, "application/pdf");
        //}

       

        [AllowAnonymous]
        public async Task<IActionResult> CustomerCollectionReportView(int id,string name,string fromDate,string toDate,int storeId)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            ViewBag.ID = id;
            string userName = User.Identity.Name;
            var rel = await customerService.GetLeadsById(id);
            ViewBag.ContactNumber ="";
            ViewBag.Name = rel.leadName;

            SalesCollectionViewModel model = new SalesCollectionViewModel
            {
                customerCollectionReportVM = await salesInvoiceMasterService.GetSalesInvoiceRecipt(id, fromDate, toDate),
                company = await eRPCompanyService.GetCompanyById(1)

            };
            return View(model);
        }

       

        #region API Section
        [Route("global/api/GetDueSalesInvoice/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDueSalesInvoice(int Id)
        {
            var data = await salesInvoiceMasterService.GetDueSalesInvoice(Id);
            return Json(data.Where(x=>x.storeId== HttpContext.Session.GetInt32("storeId")));
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