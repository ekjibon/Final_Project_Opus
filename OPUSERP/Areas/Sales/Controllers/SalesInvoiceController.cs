using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DinkToPdf.Contracts;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Sales.Models;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.Sales.Data.Entity.Sales;
using OPUSERP.Sales.Services.Sales.Interface;
using OPUSERP.Sales.Services.Sales.Interfaces;

namespace OPUSERP.Areas.Sales.Controllers
{
    [Authorize]
    [Area("Sales")]
    public class SalesInvoiceController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IAgreementService itemPriceFixingService;
        private readonly ISalesInvoiceMasterService salesInvoiceMasterService;
        private readonly ISalesInvoiceDetailService salesInvoiceDetailService;
       // private readonly IAttachmentCommentService attachmentCommentService;
       // private readonly ILedgerService ledgerService;
       // private readonly IStoreService storeService;
       // private readonly IUserInfoes userInfoes;

        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;
        private readonly IERPCompanyService eRPCompanyService;
        private readonly ILeadsService leadsService;


        public SalesInvoiceController(ILeadsService leadsService,IERPCompanyService eRPCompanyService,IHostingEnvironment hostingEnvironment, IAgreementService itemPriceFixingService, ISalesInvoiceMasterService salesInvoiceMasterService, ISalesInvoiceDetailService salesInvoiceDetailService, IConverter converter)
        {
            this._hostingEnvironment = hostingEnvironment;
           // this.attachmentCommentService = attachmentCommentService;
            this.itemPriceFixingService = itemPriceFixingService;
            this.salesInvoiceMasterService = salesInvoiceMasterService;
            this.salesInvoiceDetailService = salesInvoiceDetailService;
            this.eRPCompanyService = eRPCompanyService;
            this.leadsService = leadsService;
           // this.ledgerService = ledgerService;
           // this.userInfoes = userInfoes;
           // this.storeService = storeService;
          
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        // GET: SalesInvoice
        public async Task<IActionResult> Index(int id)
        {
            var sale = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.invoiceDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "Sale" + '-' + idate + '-' + (Cpurchase + 1);
            ViewBag.SaleNo = issueNo;


            var MasterInfo = await salesInvoiceMasterService.GetSalesInvoiceMasterById(id);
            if (MasterInfo == null)
            {
                MasterInfo = new SalesInvoiceMaster();
            }
            string user = HttpContext.User.Identity.Name;
           // var userinfo = await userInfoes.GetUserInfoByUser(user);
            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                masterId = id,
                itemPriceFixings = await itemPriceFixingService.GetAllAgreementDetails(),
                salesInvoiceMaster = MasterInfo,
               // storeAssigns = await storeService.GetAllStoreAssignByUserId(userinfo.ID)
            };
           
            
               
            return View(model);
        }

        // GET: SalesInvoice
        public async Task<IActionResult> InvoiceList()
        {
            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                salesInvoiceMasters = await salesInvoiceMasterService.GetAllSalesInvoiceMaster(),
            };
            return View(model);
        }
       

        public async Task<IActionResult> InvoiceDetails(int id)
        {
            ViewBag.masterId = id;

           

            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                salesInvoiceMaster = await salesInvoiceMasterService.GetSalesInvoiceMasterById(id),
                salesInvoiceDetails = await salesInvoiceDetailService.GetAllSalesInvoiceDetailByMasterId(id),
               
            };
            ViewBag.InWord = AmountInWord.ConvertToWords(model.salesInvoiceMaster.NetTotal.ToString());

            return View(model);
        }

       
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> InvoicePDF(int id)
        {
            ViewBag.masterId = id;
            
         
            SalesInvoiceViewModel model = new SalesInvoiceViewModel
            {
                salesInvoiceMaster = await salesInvoiceMasterService.GetSalesInvoiceMasterById(id),
                salesInvoiceDetails = await salesInvoiceDetailService.GetAllSalesInvoiceDetailByMasterId(id),
                company = await  eRPCompanyService.GetCompanyById(1),
             
            };
            ViewBag.InWord = AmountInWord.ConvertToWords(model.salesInvoiceMaster.NetTotal.ToString());

            return View(model);
        }
       

        [AllowAnonymous]
        public IActionResult InvoicePDFAction(int id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Sales/SalesInvoice/InvoicePDF?id=" + id;

            string fileName;
            string status = myPDF.GeneratePDF(out fileName, url);

            // string status = myPDF.GeneratePDF(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }
        [AllowAnonymous]
        public IActionResult ReturnInvoicePDFAction(int id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Sales/SalesInvoice/ReturnInvoicePDF?id=" + id;

            string fileName;
            string status = myPDF.GeneratePDF(out fileName, url);

            // string status = myPDF.GeneratePDF(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

        [Route("/api/global/getInvoiceNo/{invoiceDate}")]
        public async Task<JsonResult> getInvoiceNo(string invoiceDate)
        {
            var sale = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.invoiceDate).ToString("yyyyMMdd") == Convert.ToDateTime(invoiceDate).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(invoiceDate).ToString("yyyy-MM-dd");
            string issueNo = "Sale" + '-' + idate + '-' + (Cpurchase + 1);
            return Json(issueNo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] SalesInvoiceViewModel model)
        {
            var sale = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
            //return Json(model);
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}
            if (model.customerId <= 0||model.customerId==null)
            {
               sale = await salesInvoiceMasterService.GetAllSalesInvoiceMaster();
                int Cpurchase1 = 0;
                Cpurchase1 = sale.Where(x => Convert.ToDateTime(x.invoiceDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
                string idate1 = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
                string issueNo1 = "Sale" + '-' + idate1 + '-' + (Cpurchase1 + 1);
                ViewBag.SaleNo = issueNo1;


                var MasterInfo = await salesInvoiceMasterService.GetSalesInvoiceMasterById(0);
                if (MasterInfo == null)
                {
                    MasterInfo = new SalesInvoiceMaster();
                }

                model = new SalesInvoiceViewModel
                {
                    masterId = 0,
                    itemPriceFixings = await itemPriceFixingService.GetAllAgreementDetails(),
                    salesInvoiceMaster = MasterInfo,
                };
                ModelState.AddModelError(string.Empty,"Customer is not properly selected");
                return View(model);
            }
         
            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.invoiceDate).ToString("yyyyMMdd") == Convert.ToDateTime(model.invoiceDate).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(model.invoiceDate).ToString("yyyy-MM-dd");
            string issueNo = "Sale" + '-' + idate + '-' + (Cpurchase + 1);
            if (model.masterId > 0)
            {
                issueNo = model.salesInvoiceNo;
            }
            if (model.masterId > 0)
            {                
                try
                {
                    await salesInvoiceDetailService.DeleteSalesInvoiceDetailByMasterId(Convert.ToInt32(model.masterId));
                }
                catch
                {
                    return RedirectToAction(nameof(InvoiceDetails), new { id = model.masterId });
                }
            }
            if (model.masterId > 0)
            {
                SalesInvoiceMaster salesdata =await salesInvoiceMasterService.GetSalesInvoiceMasterById((int)model.masterId);
                IEnumerable<SalesInvoiceDetail> lstsalesdetaildata = await salesInvoiceDetailService.GetAllSalesInvoiceDetailByMasterId((int)model.masterId);
                RepSalesInvoiceMaster repdata = new RepSalesInvoiceMaster
                {
                    Id = 0,
                    salesInvoiceMasterId= salesdata.Id,
                    leadsId = salesdata.leadsId,
                    invoiceDate = salesdata.invoiceDate,
                    paymentDate = salesdata.paymentDate,
                    invoiceNumber = salesdata.invoiceNumber,
                    totalAmount = salesdata.totalAmount,
                    VATOnTotal = salesdata.VATOnTotal,
                    DiscountOnTotal = salesdata.DiscountOnTotal,
                    NetTotal = salesdata.NetTotal,

                    isClose = salesdata.isClose,
                    isStockClose = salesdata.isStockClose
                };
                var repmasterId = await salesInvoiceMasterService.SaveRepSalesInvoiceMaster(repdata);

                foreach (SalesInvoiceDetail datadetail in lstsalesdetaildata)
                {
                    RepSalesInvoiceDetail data1 = new RepSalesInvoiceDetail
                    {
                        Id = 0,
                        repSalesInvoiceMasterId=repmasterId,
                        agreementDetailsId =datadetail.agreementDetailsId,
                        salesInvoiceDetailId=datadetail.Id,
                        quantity = datadetail.quantity,
                        unitPrice = datadetail.unitPrice,
                        discountAmount = datadetail.discountAmount,
                        taxAmount = datadetail.taxAmount,
                        vatAmount = datadetail.vatAmount,
                        
                    };
                    await salesInvoiceDetailService.SaveRepSalesInvoiceDetail(data1);
                }

            }
            SalesInvoiceMaster data = new SalesInvoiceMaster
            {
                Id = Convert.ToInt32(model.masterId),
                leadsId = model.customerId,
                invoiceDate = model.invoiceDate,
                paymentDate=model.paymentDate,
                invoiceNumber = issueNo,//model.salesInvoiceNo,
                totalAmount = model.grossTotal,
                VATOnTotal = model.grossVAT,
                DiscountOnTotal = model.grossDiscount,
                NetTotal = model.netTotal,
               
                isClose = 0,
                isStockClose = 0
            };
            var masterId = await salesInvoiceMasterService.SaveSalesInvoiceMaster(data);
            var reomasterId = 0;
            if (model.masterId == 0)
            {
                RepSalesInvoiceMaster repdata = new RepSalesInvoiceMaster
                {
                    Id = 0,
                    leadsId = model.customerId,
                    invoiceDate = model.invoiceDate,
                    paymentDate = model.paymentDate,
                    invoiceNumber = issueNo,//model.salesInvoiceNo,
                    totalAmount = model.grossTotal,
                    VATOnTotal = model.grossVAT,
                    DiscountOnTotal = model.grossDiscount,
                    NetTotal = model.netTotal,
                    salesInvoiceMasterId = masterId,
                    isClose = 0,
                    isStockClose = 0
                };
                 reomasterId = await salesInvoiceMasterService.SaveRepSalesInvoiceMaster(repdata);
            }
            for (int i = 0; i < model.itemPriceFixingId.Length; i++)
            {
                SalesInvoiceDetail data1 = new SalesInvoiceDetail
                {
                    Id = 0,
                    agreementDetailsId = model.itemPriceFixingId[i],
                    quantity = model.tblQuantity[i],
                    unitPrice = model.lineTotal[i],
                    discountAmount = model.linediscount[i],
                    taxAmount = model.linetax[i],
                    vatAmount = model.linevat[i],
                    salesInvoiceMasterId = masterId
                };
               int detaiid=  await salesInvoiceDetailService.SaveSalesInvoiceDetail(data1);
                if (model.masterId == 0)
                {

                   
                        RepSalesInvoiceDetail repdata1 = new RepSalesInvoiceDetail
                        {
                            Id = 0,
                            agreementDetailsId = model.itemPriceFixingId[i],
                            quantity = model.tblQuantity[i],
                            unitPrice = model.lineTotal[i],
                            discountAmount = model.linediscount[i],
                            taxAmount = model.linetax[i],
                            vatAmount = model.linevat[i],
                            repSalesInvoiceMasterId = masterId,
                            salesInvoiceDetailId=detaiid
                        };
                        await salesInvoiceDetailService.SaveRepSalesInvoiceDetail(repdata1);
                  


                }
            }
           
           

            return RedirectToAction(nameof(Index));
        }

       

        #region API Section

        [Route("global/api/getAllSalesInvoiceDetailByMasterId/{masterId}")]
        [HttpGet]
        public async Task<IActionResult> GetAllSalesInvoiceDetailByMasterId(int masterId)
        {
            return Json(await salesInvoiceDetailService.GetAllSalesInvoiceDetailByMasterId(masterId));
        }

        [Route("global/api/getAllSalesInvoiceMaster/")]
        [HttpGet]
        public async Task<IActionResult> GetAllSalesInvoiceMaster()
        {
            return Json(await salesInvoiceMasterService.GetAllSalesInvoiceMaster());
        }
        [Route("global/api/getAllRelSupplierCustomerResourseByRoleId/")]
        [HttpGet]
        public async Task<IActionResult> GetAllRelSupplierCustomerResourseByRoleId()
        {
            return Json(await leadsService.GetAllLeads());
        }
        [Route("global/api/getAllActiveItemFromItemPricebyId/{id}")]
        [HttpGet]
        public async Task<IActionResult> getAllActiveItemFromItemPricebyId(int id)
        {
            return Json(await itemPriceFixingService.GetAgreementDetailsbyleadId(id));
        }

        

        #endregion

        //xxxxxxxxxxxxxxxxxxxxx
    }
}