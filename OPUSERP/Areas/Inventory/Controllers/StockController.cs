using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using LanguageDetection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Auth.Models;
using OPUSERP.Areas.Inventory.Models;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.OtherSales.Services.Sales.Interfaces;
using OPUSERP.Purchase.Service.Interfaces;
using OPUSERP.Purchase.Services.Interfaces;
using OPUSERP.SCM.Data.Entity.Stock;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using OPUSERP.SCM.Services.PurchaseOrder.Interface;
using OPUSERP.SCM.Services.Stock.Interface;

namespace OPUSERP.Areas.Inventory.Controllers
{
    [Authorize]
    [Area("Inventory")]
    public class StockController : Controller
    {
        private readonly IPurchaseService purchaseService;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IOSalesInvoiceMasterService salesInvoiceMasterService;
        private readonly IOSalesInvoiceDetailService salesInvoiceDetailService;
        private readonly ITransferService transferService;
        private readonly IERPCompanyService iERPCompanyService;
        private readonly IStoreService storeService;
        private readonly IItemsService itemsService;
        private readonly IPurchaseOrderService purchaseOrderService;
        public readonly IERPModuleService iERPModuleService;
        public readonly IStoreDepartmentService storeDepartmentService;

        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public string FileName;
        private readonly IStockService stockService;
        private readonly IUserInfoes userInfoes;


        public StockController(IHostingEnvironment hostingEnvironment, IStoreService storeService, IItemsService itemsService,  IUserInfoes userInfoes, IStockService stockService, IConverter converter, IPurchaseService purchaseService, IAttachmentCommentService attachmentCommentService, ITransferService transferService, IOSalesInvoiceMasterService salesInvoiceMasterService, IOSalesInvoiceDetailService salesInvoiceDetailService, IERPCompanyService iERPCompanyService, IPurchaseOrderService purchaseOrderService, IERPModuleService iERPModuleService, IStoreDepartmentService storeDepartmentService)
        {
            this.stockService = stockService;
            this._hostingEnvironment = hostingEnvironment;
           
            this.attachmentCommentService = attachmentCommentService;
            this.purchaseService = purchaseService;
            this.salesInvoiceMasterService = salesInvoiceMasterService;
            this.salesInvoiceDetailService = salesInvoiceDetailService;
            this.storeService = storeService;
            this.userInfoes = userInfoes;
            this.iERPCompanyService = iERPCompanyService;
            this.transferService = transferService;
            this.itemsService = itemsService;
            this.purchaseOrderService = purchaseOrderService;
            this.iERPModuleService = iERPModuleService;
            this.storeDepartmentService = storeDepartmentService;

            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        #region Index/Purchase stock in
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            StockInViewModel model = new StockInViewModel
            {
                purchaseOrdersMasters = await purchaseOrderService.GetDueStockPurchaseInvoiceList()

            };
            return View(model);
        }

        #endregion

        #region ItemList StockIn

        [HttpGet]
        public async Task<IActionResult> ItemListStockIn(int Id)
        {
            var sale = await stockService.GetStockMasterbyType(1);
            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.StockDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "MRNO" + '-' + idate + '-' + (Cpurchase + 1);
            ViewBag.MRNO = issueNo;
            string user = HttpContext.User.Identity.Name;
            var userinfo = await userInfoes.GetUserInfoByUser(user);

            StockInViewModel model = new StockInViewModel
            {
                purchaseOrdersMaster = await purchaseOrderService.GetPurchaseOrderMasterById(Id),
                stockItemViewModels = await purchaseOrderService.GetDueStockPurchaseDetailsInvoiceList(Id),
                storeDepartmentTypes = await storeDepartmentService.GetDeartmentTypeList()
                //storeAssigns = await storeService.GetAllStoreAssignByUserId(userinfo.ID)

            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] StockInViewModel model)
        {
            AspNetUsersViewModel user = await userInfoes.GetUserInfoByUser(HttpContext.User.Identity.Name);
            //var GRNumber = await stockService.GetGRNumber();
            var PurchasedetailsId = 0;
            var sale = await stockService.GetStockMasterbyType(1);
            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.StockDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "MRNO" + '-' + idate + '-' + (Cpurchase + 1);
            int masterId = 0;
            if (model.detailids != null)
            {
                StockMaster data = new StockMaster
                {
                    Id = Convert.ToInt32(model.stockMasterId),
                    remarks = model.remarks,
                    stockStatusId = 1,
                    receiveNo = issueNo, //model.MRNO,
                    MRNO = issueNo,
                    receiveDate = model.stockDate,
                    StockDate = model.stockDate,
                    receiveType = "GeneralPurchase",
                    purchaseId = model.purchaseId,
                    receiveBy = user.EmpName,
                    userId = user.UserId


                };
                masterId = await stockService.SaveStockMaster(data);
                await stockService.DeletestockByStockMasterId(masterId);
                for (var i = 0; i < model.detailids.Length; i++)
                {
                    PurchasedetailsId = (int)model.detailids[i];
                    StockDetails data1 = new StockDetails
                    {
                        stockMasterId = masterId,
                        orderDetailsId = model.detailids[i],
                        purchaseRate = model.rates[i],
                        rate = model.rates[i],
                        grQty = model.quantitys[i],
                        poQty = model.quantitys[i],
                        itemSpecificationId = model.specids[i],
                        storeDepartmentTypeId = model.storeDepartmentId[i]
                    };
                    await stockService.SaveStock(data1);
                }
            }
            var purchasedata = await purchaseOrderService.GetPurchaseOrderDetailById(PurchasedetailsId);
            var purchasemasterId = purchasedata.purchaseId;
            var puchasedatatotal = purchaseOrderService.GetPurchaseOrderDetailByPOId((int)purchasemasterId);
            var stock = await stockService.GetAllStockInByPurchaseMaster((int)purchasemasterId);
            if (puchasedatatotal.Sum(x => x.poQty) == stock.Sum(x => x.grQty))
            {
                await purchaseOrderService.UpdatePurchaseOrderMasterStockCloseById((int)purchasemasterId);
            }
            //  return RedirectToAction(nameof(StockInDetails));
            return RedirectToAction("StockInDetails", "Stock", new { id = masterId, Area = "Inventory" });
        }


        #endregion

        #region StockOut

        [HttpGet]
        public async Task<IActionResult> ItemStockOut(int?Id)
        {
            var sale = await stockService.GetStockMasterbyType(2);
            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.StockDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "SRNO" + '-' + idate + '-' + (Cpurchase + 1);
            ViewBag.issueNo = issueNo;
            StockInViewModel model = new StockInViewModel
            {
              

            };
            return View(model);
        }



        [HttpPost]      
        public async Task<ActionResult> ItemStockOut([FromForm] StockInViewModel model)
        {        

            AspNetUsersViewModel user = await userInfoes.GetUserInfoByUser(HttpContext.User.Identity.Name);
            var sale = await stockService.GetStockMasterbyType(2);
            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.StockDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "SRNO" + '-' + idate + '-' + (Cpurchase + 1);
            int masterId = 0;
            //int reqId = 0;



            if (model.itemPriceFixingId != null)
            {
                //ItemReqMaster itemStockOut = new ItemReqMaster
                //{
                //    Id = Convert.ToInt32(model.itemReqMasterId),
                //    reqNo = model.reqNO,
                //    reqDate = model.paymentDate
                //};
                //reqId = await stockService.SaveItemReqMaster(itemStockOut);
                StockMaster data = new StockMaster
                {
                    Id = Convert.ToInt32(model.stockMasterId),
                    remarks = model.remarks,
                    stockStatusId = 2,
                    receiveNo = issueNo, 
                    receiveDate = model.stockDate,
                    MRNO = issueNo, 
                    StockDate = model.stockDate,
                    receiveType = "Requisition",
                    //itemReqMasterId = reqId,
                    receiveBy = user.EmpName,
                    userId = user.UserId


                };
                masterId = await stockService.SaveStockMaster(data);

                await stockService.DeletestockByStockMasterId(masterId);
                //await stockService.DeleteItemReqDetailbymasterId(reqId);
                for (var i = 0; i < model.itemPriceFixingId.Length; i++)
                {
                    //ItemReqDetails itemReqDetails = new ItemReqDetails
                    //{
                    //    itemReqMasterId=reqId,
                    //    itemspecificationId= model.itemPriceFixingId[i],
                    //    quantity=model.quantitys[i]

                    //};
                    //int reqdetailid = await stockService.SaveItemReqDetail(itemReqDetails);
                    StockDetails data1 = new StockDetails
                    {
                        stockMasterId = masterId,
                        //itemReqDetailsId = reqdetailid,  
                        qty = model.quantitys[i],
                        itemSpecificationId = model.itemPriceFixingId[i]
                    };
                    await stockService.SaveStock(data1);

                }
            }
           
            return RedirectToAction("StockOutList", "Stock", new { id = 0, Area = "Inventory" });
        }

        #endregion

        #region StockOut Requisition

        [HttpGet]
        public async Task<IActionResult> ItemStockOutRequisition(int? Id)
        {
            var sale = await stockService.GetStockMasterbyType(2);
            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.StockDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "SRNO" + '-' + idate + '-' + (Cpurchase + 1);
            ViewBag.issueNo = issueNo;
            StockInViewModel model = new StockInViewModel
            {


            };
            return View(model);
        }

        

        #endregion

        [HttpGet]
        public async Task<IActionResult> UpdateSigleStock(int id, int masterId, decimal qty)
        {


            await stockService.UpdatesingleStock(id, masterId, qty);

            var data = await stockService.GetStockDetailsById(id);
            var stockdata = await stockService.GettemReqDetailbyId((int)data.itemReqDetailsId);
            ItemReqDetails datax = new ItemReqDetails
            {
                Id=stockdata.Id,
                itemReqMasterId=stockdata.itemReqMasterId,
                quantity=qty,
                itemspecificationId=stockdata.itemspecificationId

            };
            await stockService.SaveItemReqDetail(datax);

            //await attachmentCommentService.DeleteCommentById(Id);
            return RedirectToAction("StockOutDetails", "Stock", new { id = masterId, Area = "Inventory" });
        }
        
        [HttpGet]
        public async Task<IActionResult> TransferReceive()
        {
            StockInViewModel model = new StockInViewModel
            {
                transferMasters = await transferService.GetAllTransferMasterbyStoreId()

            };
            return View(model);
        }

        #region Stock Balance
        [HttpGet]
        public async Task<IActionResult> StockBalance()
        {
            StockInViewModel model = new StockInViewModel
            {


            };
            return View(model);
        }

        public IActionResult StockBalanceBrc()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult StockBalanceReportViewpdf(int tid, int? itemspecid, DateTime? fromDate, DateTime? toDate)
        {

            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Inventory/Stock/StockBalanceReportView?tid=" + tid + "&&itemspecid=" + itemspecid + "&&fromDate=" + fromDate + "&&toDate=" + toDate;

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
        public IActionResult StockBalanceReportDataBrac(int? itemspecid)
        {

            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Inventory/Stock/StockBalanceReportViewBrac?itemspecid=" + itemspecid;

            string fileName;
            string status = myPDF.GenerateLandscapePDFA4(out fileName, url);

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
        public async Task<IActionResult> StockBalanceReportView(int tid, int? itemspecid, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                var item = await itemsService.GetItemById(tid);
                ViewBag.item = item.itemName;
                var itemspec = await itemsService.GetItemSpecificationById((int)itemspecid);
                ViewBag.spec = "";
                if (itemspec != null)
                {
                    ViewBag.spec = itemspec.specificationName;
                }

                var model = new StockInViewModel
                {
                    stockBalancesViewModels = await stockService.GetStockBalanceViewModels(tid, (int)itemspecid, fromDate, toDate),
                    companies = await iERPCompanyService.GetAllCompany()

                };
                ViewBag.fromDate = fromDate;
                ViewBag.toDate = toDate;
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [AllowAnonymous]
        public async Task<IActionResult> StockBalanceReportViewBrac(int? itemspecid)
        {
            try
            {
                var model = new StockInViewModel
                {
                    stockBalanceByItemViewModels = await stockService.GetStockBalanceByItem(itemspecid),
                    companies = await iERPCompanyService.GetAllCompany()
                };
                //ViewBag.fromDate = fromDate;
                //ViewBag.toDate = toDate;
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStockBalanceByItem(int? itemspecid)
        {
            var data = await stockService.GetStockBalanceByItem(itemspecid);
            return Json(data);
        }

        #endregion

        #region Stock Summary

        public IActionResult StockSummary()
        {            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetStockSummary(string FDate, string TDate)
        {
            var data = await stockService.GetStockSummary(FDate, TDate);
            return Json(data);
        }

        [AllowAnonymous]
        public IActionResult StockSummaryView(string fromDate, string toDate)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Inventory/Stock/StockSummaryPdf?fromDate=" + fromDate + "&&toDate=" + toDate;

            string fileName;
            string status = myPDF.GenerateLandscapePDFA4(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> StockSummaryPdf(string fromDate, string toDate)
        {
            try
            { 
                var model = new StockInViewModel
                {
                    stockSummaryViewModels = await stockService.GetStockSummary(fromDate, toDate),
                    companies = await iERPCompanyService.GetAllCompany()
                };
                ViewBag.fromDate = fromDate;
                ViewBag.toDate = toDate;
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        [HttpGet]
        public IActionResult IndexReturnInvoice()
        {
            StockInViewModel model = new StockInViewModel
            {
                //SalesReturnInvoiceMasters = await salesInvoiceMasterService.GetAllDueSalesReturnInvoiceMaster()

            };
            return View(model);
        }
        [HttpGet]
        public IActionResult SaleReturnInvoice(int? Id)
        {
            StockInViewModel model = new StockInViewModel
            {
                //invoiceMasters = await purchaseService.GetSaleInvoiceListForReturn()

            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> InoviceList()
        {
            StockInViewModel model = new StockInViewModel
            {
                invoiceMasters = await purchaseService.GetDueStockPymentInvoiceList()
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult RequisitionList()
        {
            StockInViewModel model = new StockInViewModel
            {
                
            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> StockInList()
        {
            StockInViewModel model = new StockInViewModel
            {
                stockMasters = await stockService.GetStockMasterbyType(1)

            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> StockInListReturnSale()
        {
            StockInViewModel model = new StockInViewModel
            {
                stockMasters = await stockService.GetAllStockMaster(3)

            };
            return View(model);
        }


        [HttpGet]
        public IActionResult StockBalanceList()
        {
            StockInViewModel model = new StockInViewModel
            {
                //stocks = await stockService.GetAllStockBlanace()

            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> StockOutList()
        {
            StockInViewModel model = new StockInViewModel
            {
                stockMasters = await stockService.GetStockMasterbyType(2)

            };
            return View(model);
        }

        

        [HttpGet]
        public IActionResult StockInFromProduction()
        {
            StockInViewModel model = new StockInViewModel
            {
               

            };
            return View(model);
        }

        [HttpGet]
        public IActionResult StockInForProductionItemList(int Id)
        {


            StockInViewModel model = new StockInViewModel
            {

            };

            return View(model);
        }



        public async Task<IActionResult> StockInDetails(int id)
        {
            try
            {
                var module = await iERPModuleService.GetERPModuleByModuleName("Inventory");
                int moduleId = module.Id;
                ViewBag.masterId = id;

                var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(id, "StockIn", moduleId);
               
                if (CommentInfo == null)
                {
                    CommentInfo = new List<Comment>();
                }
                var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "StockIn", "photo", moduleId);
                
                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "StockIn", "Document", moduleId);
                
                if (docInfo == null)
                {
                    docInfo = new List<DocumentPhotoAttachment>();
                }
                // var stockdetails = await stockService.GetAllStockByMasterId(id);


                var model = new StockInViewModel
                {
                    stockMasterById = await stockService.GetStockInMasterById(id),
                    GetStocksbyMasterId = await stockService.GetAllStockByMasterId(id),

                    //company = await ledgerService.Company(Convert.ToInt32(companyId)),
                    //costCentreAllocations = costInf,
                    comments = CommentInfo,
                    photoes = photoInfo,
                    documents = docInfo,
                };

                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
       

        [AllowAnonymous]
        public IActionResult StockOutPdfAction(int MasterId)
        {

            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Inventory/Stock/StockOutPdf?id=" + MasterId;

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
        public IActionResult StockInPdfAction(int MasterId)
        {

            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Inventory/Stock/StockInPdf?id=" + MasterId;

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
        public async Task<IActionResult> StockOutPdf(int id)
        {
            try
            {


                var model = new StockInViewModel
                {
                    stockMasterById = await stockService.GetStockOutMasterById(id),
                    GetStocksbyMasterId = await stockService.GetAllStockByMasterId(id),
                    companies = await iERPCompanyService.GetAllCompany()

                };

                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [AllowAnonymous]
        public async Task<IActionResult> StockInPdf(int id)
        {
            try
            {

                var model = new StockInViewModel
                {

                    stockMasterById = await stockService.GetStockInMasterById(id),
                    GetStocksbyMasterId = await stockService.GetAllStockByMasterId(id),
                    companies = await iERPCompanyService.GetAllCompany()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
       
        public async Task<IActionResult> StockOutDetails(int id)
        {
            try
            {
                var module = await iERPModuleService.GetERPModuleByModuleName("Inventory");
                int moduleId = module.Id;

                ViewBag.masterId = id;

                var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(id, "StockIn", moduleId);

                if (CommentInfo == null)
                {
                    CommentInfo = new List<Comment>();
                }
                var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "StockIn", "photo", moduleId);

                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "StockIn", "Document", moduleId);

                if (docInfo == null)
                {
                    docInfo = new List<DocumentPhotoAttachment>();
                }

                // var stockdetails = await stockService.GetAllStockByMasterId(id);

                var model = new StockInViewModel
                {
                    stockMasterById = await stockService.GetStockOutMasterById(id),
                    GetStocksbyMasterId = await stockService.GetAllStockByMasterId(id),

                    //company = await ledgerService.Company(Convert.ToInt32(companyId)),
                    //costCentreAllocations = costInf,
                    comments = CommentInfo,
                    photoes = photoInfo,
                    documents = docInfo,
                };
                if (model.GetStocksbyMasterId.FirstOrDefault().salesInvoiceDetailId != null)
                {
                    ViewBag.invoiceNumber = model.GetStocksbyMasterId.FirstOrDefault().salesInvoiceDetail.salesInvoiceMaster.invoiceNumber;
                    ViewBag.invoiceDate = model.GetStocksbyMasterId.FirstOrDefault().salesInvoiceDetail.salesInvoiceMaster.invoiceDate;
                    ViewBag.nameEnglish = model.GetStocksbyMasterId.FirstOrDefault().salesInvoiceDetail.salesInvoiceMaster.relSupplierCustomerResourse.Leads.leadName;
                    ViewBag.totalAmount = model.GetStocksbyMasterId.FirstOrDefault().salesInvoiceDetail.salesInvoiceMaster.totalAmount;
                }
                else
                {
                    //ViewBag.invoiceNumber = model.GetStocksbyMasterId.FirstOrDefault().posInvoiceDetail.posInvoiceMaster.invoiceNumber;
                    //ViewBag.invoiceDate = model.GetStocksbyMasterId.FirstOrDefault().posInvoiceDetail.posInvoiceMaster.invoiceDate;
                    //ViewBag.nameEnglish = model.GetStocksbyMasterId.FirstOrDefault().posInvoiceDetail.posInvoiceMaster.posCustomer.name;
                    //ViewBag.totalAmount = model.GetStocksbyMasterId.FirstOrDefault().posInvoiceDetail.posInvoiceMaster.totalAmount;
                }

                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IndexOut([FromForm] StockInViewModel model)
        {
            var PurchasedetailsId = 0;
            var sale = await stockService.GetStockMasterbyType(2);
            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.StockDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "SRNO" + '-' + idate + '-' + (Cpurchase + 1);
            int masterId = 0;
            if (model.detailids != null)
            {
                StockMaster data = new StockMaster
                {
                    Id = Convert.ToInt32(model.stockMasterId),
                    //companyId = 1,
                    remarks = model.remarks,
                    MRNO = issueNo,//model.MRNO,
                    StockDate = model.stockDate,
                    stockStatusId = 2,
                    //storeId = 1

                };
                masterId = await stockService.SaveStockMaster(data);
                await stockService.DeleteStockMasterById(masterId);
                for (var i = 0; i < model.detailids.Length; i++)
                {
                    PurchasedetailsId = (int)model.detailids[i];
                    StockDetails data1 = new StockDetails
                    {
                        stockMasterId = masterId,
                        salesInvoiceDetailId = model.detailids[i],
                        purchaseRate = model.rates[i],
                        rate = model.rates[i],
                        qty = model.quantitys[i],
                        itemSpecificationId = model.specids[i]

                    };
                    await stockService.SaveStock(data1);
                }
            }
            var purchasedata = await salesInvoiceDetailService.GetSalesInvoiceDetailById(PurchasedetailsId);
            var purchasemasterId = purchasedata.salesInvoiceMasterId;
            var puchasedatatotal = await salesInvoiceDetailService.GetAllSalesInvoiceDetailByMasterId((int)purchasemasterId);
            var stock = await stockService.GetAllStockOutBySalesMaster((int)purchasemasterId);
            if (puchasedatatotal.Sum(x => x.quantity) == stock.Sum(x => x.grQty))
            {
                await salesInvoiceMasterService.UpdateSalesInvoiceMasterStockCloseById((int)purchasemasterId);
            }
            //return Json(data);
            // return RedirectToAction(nameof(StockOutDetails));
            return RedirectToAction("StockOutDetails", "Stock", new { id = masterId, Area = "Inventory" });
        }

        [HttpGet]
        public async Task<IActionResult> ItemListTransferStockIn(int Id)
        {
            var sale = await stockService.GetStockMasterbyType(1);
            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.StockDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "MRNO" + '-' + idate + '-' + (Cpurchase + 1);
            ViewBag.MRNO = issueNo;
            string user = HttpContext.User.Identity.Name;
            var userinfo = await userInfoes.GetUserInfoByUser(user);

            StockInViewModel model = new StockInViewModel
            {
                transferMaster = await transferService.GetAllTransferMasterByMasterId(Id),
                stockItemViewModels = await transferService.GetDueStockTransferDetailsInvoiceList(Id),
                //storeAssigns = await storeService.GetAllStoreAssignByUserId(userinfo.ID)


            };
           
           
            return View(model);

        }

        [HttpGet]
        public IActionResult ItemListStockInSaleReturn(int Id)
        {
            StockInViewModel model = new StockInViewModel
            {

            };
          
            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> ItemListStockOut(int Id)
        {
            var sale = await stockService.GetStockMasterbyType(2);
            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.StockDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "SRNO" + '-' + idate + '-' + (Cpurchase + 1);
            ViewBag.MRNO = issueNo;
            string user = HttpContext.User.Identity.Name;
            var userinfo = await userInfoes.GetUserInfoByUser(user);
            StockInViewModel model = new StockInViewModel
            {
                SalesInvoiceMaster=await salesInvoiceMasterService.GetSalesInvoiceMasterById(Id),
                stockItemViewModels = await purchaseService.GetDueStockoutDetailsInvoiceList(Id),
                //storeAssigns = await storeService.GetAllStoreAssignByUserId(userinfo.ID)
            };
            
            return View(model);

        }

        [HttpGet]
        public IActionResult ReqItemListStockOut(int Id)
        {          

            StockInViewModel model = new StockInViewModel
            {
              
            };


            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> ItemListSaleReturn(int Id)
        {
            var sale = await salesInvoiceMasterService.GetAllSalesReturnInvoiceMaster();
            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.invoiceDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "Sale-Return-NO" + '-' + idate + '-' + (Cpurchase + 1);
            ViewBag.MRNO = issueNo;
            string user = HttpContext.User.Identity.Name;
            var userinfo = await userInfoes.GetUserInfoByUser(user);
            StockInViewModel model = new StockInViewModel
            {
                SalesInvoiceMaster = await salesInvoiceMasterService.GetSalesInvoiceMasterById(Id),
                stockItemViewModels = await purchaseService.GetReturnDetailsInvoiceList(Id),
                storeAssigns = await storeService.GetAllStoreAssignByUserId(userinfo.aspnetId)


            };
            if (HttpContext.Session.GetInt32("storeId") == null)
            {
                ViewBag.storeId = 0; //model.storeAssigns.Where(x => x.isDefault == "Yes").FirstOrDefault().Id;
            }
            else
            {
                ViewBag.storeId = HttpContext.Session.GetInt32("storeId");
            }
            return View(model);

        }
        [HttpPost]
        public async Task<ActionResult> SaveCommentIn([FromForm] CommentsViewModel model)
        {
            try
            {
                var module = await iERPModuleService.GetERPModuleByModuleName("Inventory");
                int moduleId = module.Id;

                Comment entityComment = new Comment
                {
                    Id = Convert.ToInt32(model.commentsId),
                    actionType = "StockIn",
                    actionTypeId = model.actionTypeId,
                    title = model.title,
                    comment = model.comment,
                    moduleId = moduleId,
                };
                int commentsId = await attachmentCommentService.SaveComment(entityComment);
                return RedirectToAction("StockInDetails", "Stock", new { id = model.actionTypeId, Area = "Inventory" });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<ActionResult> SaveCommentInReturn([FromForm] CommentsViewModel model)
        {
            try
            {
                var module = await iERPModuleService.GetERPModuleByModuleName("Inventory");
                int moduleId = module.Id;
                Comment entityComment = new Comment
                {
                    Id = Convert.ToInt32(model.commentsId),
                    actionType = "StockInReturn",
                    actionTypeId = model.actionTypeId,
                    title = model.title,
                    comment = model.comment,
                    moduleId = moduleId,
                };
                int commentsId = await attachmentCommentService.SaveComment(entityComment);
                return RedirectToAction("StockInReturnDetails", "Stock", new { id = model.actionTypeId, Area = "Inventory" });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<ActionResult> SaveCommentOut([FromForm] CommentsViewModel model)
        {
            try
            {
                var module = await iERPModuleService.GetERPModuleByModuleName("Inventory");
                int moduleId = module.Id;

                Comment entityComment = new Comment
                {
                    Id = Convert.ToInt32(model.commentsId),
                    actionType = "StockOut",
                    actionTypeId = model.actionTypeId,
                    title = model.title,
                    comment = model.comment,
                    moduleId = moduleId,
                };
                int commentsId = await attachmentCommentService.SaveComment(entityComment);
                return RedirectToAction("StockOutDetails", "Stock", new { id = model.actionTypeId, Area = "Inventory" });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCommentsIn(int Id, int actionId)
        {
            await attachmentCommentService.DeleteCommentById(Id);
            return RedirectToAction("StockInDetails", "Stock", new { id = actionId, Area = "Inventory" });
        }
        [HttpGet]
        public async Task<IActionResult> DeleteCommentsInReturn(int Id, int actionId)
        {
            await attachmentCommentService.DeleteCommentById(Id);
            return RedirectToAction("StockInReturnDetails", "Stock", new { id = actionId, Area = "Inventory" });
        }
        [HttpGet]
        public async Task<IActionResult> DeleteCommentsOut(int Id, int actionId)
        {
            await attachmentCommentService.DeleteCommentById(Id);
            return RedirectToAction("StockOutDetails", "Stock", new { id = actionId, Area = "Inventory" });
        }

        
        [HttpPost]
        public async Task<IActionResult> SaveDocIn([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
        {
            try
            {
                var module = await iERPModuleService.GetERPModuleByModuleName("Inventory");
                int moduleId = module.Id;

                if (formFile != null)
                {
                    string userName = User.Identity.Name;
                    string documentType = "Document";
                    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                    var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName;
                    string fileType = Path.GetExtension(fileName);
                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');

                    string NewFileName = model.actionTypeId + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = Convert.ToInt32(model.documentId),
                        actionType = model.actionType,
                        actionTypeId = model.actionTypeId,
                        subject = model.subject,
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description = model.description,
                        documentType = documentType,
                        moduleId = moduleId,
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("StockInDetails", "Stock", new { id = model.actionTypeId, Area = "Inventory" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveDocInReturn([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
        {
            try
            {
                var module = await iERPModuleService.GetERPModuleByModuleName("Inventory");
                int moduleId = module.Id;

                if (formFile != null)
                {
                    string userName = User.Identity.Name;
                    string documentType = "Document";
                    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                    var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName;
                    string fileType = Path.GetExtension(fileName);
                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');

                    string NewFileName = model.actionTypeId + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = Convert.ToInt32(model.documentId),
                        actionType = model.actionType,
                        actionTypeId = model.actionTypeId,
                        subject = model.subject,
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description = model.description,
                        documentType = documentType,
                        moduleId = moduleId,
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("StockInReturnDetails", "Stock", new { id = model.actionTypeId, Area = "Inventory" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveDocOut([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
        {
            try
            {
                var module = await iERPModuleService.GetERPModuleByModuleName("Inventory");
                int moduleId = module.Id;

                if (formFile != null)
                {
                    string userName = User.Identity.Name;
                    string documentType = "Document";
                    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                    var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName;
                    string fileType = Path.GetExtension(fileName);
                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');

                    string NewFileName = model.actionTypeId + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = Convert.ToInt32(model.documentId),
                        actionType = model.actionType,
                        actionTypeId = model.actionTypeId,
                        subject = model.subject,
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description = model.description,
                        documentType = documentType,
                        moduleId = moduleId,
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("StockOutDetails", "Stock", new { id = model.actionTypeId, Area = "Inventory" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> SavePhotoIn([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
        {
            try
            {
                var module = await iERPModuleService.GetERPModuleByModuleName("Inventory");
                int moduleId = module.Id;

                if (formFile != null)
                {
                    string userName = User.Identity.Name;
                    string documentType = "photo";
                    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                    var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName;
                    string fileType = Path.GetExtension(fileName);
                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');

                    string NewFileName = model.actionTypeId + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = Convert.ToInt32(model.documentId),
                        actionType = model.actionType,
                        actionTypeId = model.actionTypeId,
                        subject = model.subject,
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description = model.description,
                        documentType = documentType,
                        moduleId = moduleId,
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("StockInDetails", "Stock", new { id = model.actionTypeId, Area = "Inventory" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> SavePhotoInReturn([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
        {
            try
            {
                var module = await iERPModuleService.GetERPModuleByModuleName("Inventory");
                int moduleId = module.Id;
                if (formFile != null)
                {
                    string userName = User.Identity.Name;
                    string documentType = "photo";
                    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                    var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName;
                    string fileType = Path.GetExtension(fileName);
                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');

                    string NewFileName = model.actionTypeId + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = Convert.ToInt32(model.documentId),
                        actionType = model.actionType,
                        actionTypeId = model.actionTypeId,
                        subject = model.subject,
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description = model.description,
                        documentType = documentType,
                        moduleId = moduleId,
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("StockInReturnDetails", "Stock", new { id = model.actionTypeId, Area = "Inventory" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> SavePhotoOut([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
        {
            try
            {
                var module = await iERPModuleService.GetERPModuleByModuleName("Inventory");
                int moduleId = module.Id;

                if (formFile != null)
                {
                    string userName = User.Identity.Name;
                    string documentType = "photo";
                    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                    var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName;
                    string fileType = Path.GetExtension(fileName);
                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');

                    string NewFileName = model.actionTypeId + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = Convert.ToInt32(model.documentId),
                        actionType = model.actionType,
                        actionTypeId = model.actionTypeId,
                        subject = model.subject,
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description = model.description,
                        documentType = documentType,
                        moduleId = moduleId,
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("StockOutDetails", "Stock", new { id = model.actionTypeId, Area = "Inventory" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeletePhotoIn(int actionId, int photoId)
        {
            var data = await attachmentCommentService.GetDocumentAttachmentById(photoId);
            var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";

            var fullFilePath = Path.Combine(filesPath, data.fileName);
            if (System.IO.File.Exists(fullFilePath))
                System.IO.File.Delete(fullFilePath);
            await attachmentCommentService.DeleteDocumentAttachmentById(photoId);
            return RedirectToAction("StockInDetails", "Stock", new { id = actionId, Area = "Inventory" });
        }
        [HttpGet]
        public async Task<IActionResult> DeletePhotoInReturn(int actionId, int photoId)
        {
            var data = await attachmentCommentService.GetDocumentAttachmentById(photoId);
            var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";

            var fullFilePath = Path.Combine(filesPath, data.fileName);
            if (System.IO.File.Exists(fullFilePath))
                System.IO.File.Delete(fullFilePath);
            await attachmentCommentService.DeleteDocumentAttachmentById(photoId);
            return RedirectToAction("StockInReturnDetails", "Stock", new { id = actionId, Area = "Inventory" });
        }
        [HttpGet]
        public async Task<IActionResult> DeletePhotoOut(int actionId, int photoId)
        {
            var data = await attachmentCommentService.GetDocumentAttachmentById(photoId);
            var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";

            var fullFilePath = Path.Combine(filesPath, data.fileName);
            if (System.IO.File.Exists(fullFilePath))
                System.IO.File.Delete(fullFilePath);
            await attachmentCommentService.DeleteDocumentAttachmentById(photoId);
            return RedirectToAction("StockOutDetails", "Stock", new { id = actionId, Area = "Inventory" });
        }

        #region API Section

        //[Route("global/api/getAllStockInbyId/{Id}")]
        //[HttpGet]
        //public async Task<IActionResult> getAllStockInbyId(int Id)
        //{
        //    var data = await stockService.Getbalancestockbyid(Id);

        //    return Json(data);
        //}
        //[Route("global/api/getAllStockItemByStockIn/")]
        //[HttpGet]
        //public async Task<IActionResult> GetAllStockItemByStockIn()
        //{
        //    string status = "Stock In";
        //    return Json(await stockService.GetAllStockItemByStockStatus(status));
        //}
        //[Route("global/api/getAllStockItemByStockOut/")]
        //[HttpGet]
        //public async Task<IActionResult> GetAllStockItemByStockOut()
        //{
        //    string status = "Stock Out";
        //    return Json(await stockService.GetAllStockItemByStockStatus(status));
        //}

        //[Route("global/api/getAllStockItemSpacificationByItemId/{itemId}")]
        //[HttpGet]
        //public async Task<IActionResult> GetAllStockItemSpacificationByItemId(int itemId)
        //{
        //    return Json(await stockService.GetAllStockItemSpacificationByItemId(itemId));
        //}

        //[Route("global/api/getLastStockItemSpacificationByItemSpacificationId/{itemSpacificationId}")]
        //[HttpGet]
        //public async Task<IActionResult> GetLastStockItemSpacificationByItemSpacificationId(int itemSpacificationId)
        //{
        //    return Json(await stockService.GetLastStockItemSpacificationByItemSpacificationId(itemSpacificationId));
        //}

        [Route("global/api/getbalance/{Id}")]
        [HttpGet]
        public async Task<IActionResult> getbalance(int Id)
        {
            var stock = await stockService.GetStockDetailsbyspecid(Id);
            var stockin = stock.Where(x => x.stockMaster.stockStatusId == 1 || x.stockMaster.stockStatusId == 3).Sum(x => x.grQty);
            var stockout = stock.Where(x => x.stockMaster.stockStatusId == 2).Sum(x => x.qty);
            //var balance = ((stockin == null) ? 0 : stockin) - ((stockout == null) ? 0 : stockout);
            var balance = stockin - stockout;
            return Json(balance);
        }

        

        #endregion


    }
}