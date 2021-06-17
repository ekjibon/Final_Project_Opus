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
using OPUSERP.Areas.Auth.Models;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.Areas.Purchase.Models;
using OPUSERP.Areas.SCMMasterData.Models;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.Purchase.Data.Entity;
using OPUSERP.Purchase.Service.Interfaces;
using OPUSERP.Purchase.Services.Interfaces;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Data.Entity.Requisition;
using OPUSERP.SCM.Data.Entity.Stock;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using OPUSERP.SCM.Services.PurchaseOrder.Interface;
using OPUSERP.SCM.Services.Stock.Interface;
using OPUSERP.SCM.Services.Supplier.Interface;

namespace OPUSERP.Areas.Purchase.Controllers
{
    [Authorize]
    [Area("Purchase")]
    public class PurchaseOrderController : Controller
    {
        private readonly IPurchaseService purchaseService;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string rootPath;
        private readonly MyPDF myPDF;
        private readonly IItemsService ItemsService;
        private readonly IOrganizationService organizationService;
        private readonly ISupplierItemInfoService supplierItemInfoService;
        private readonly IStockService stockService;
        private readonly IERPCompanyService eRPCompanyService;
        private readonly IBillPaymentService billPaymentService;
        private readonly IPurchaseOrderService purchaseOrderService;
        private readonly IUserInfoes userInfo;

        public string FileName;
        public PurchaseOrderController(IHostingEnvironment hostingEnvironment, IBillPaymentService billPaymentService, IOrganizationService organizationService, ISupplierItemInfoService supplierItemInfoService, IStockService stockService, IAttachmentCommentService attachmentCommentService, IConverter converter, IPurchaseService purchaseService, IItemsService ItemsService, IERPCompanyService eRPCompanyService, IPurchaseOrderService purchaseOrderService, IUserInfoes userInfo)
        {

            this._hostingEnvironment = hostingEnvironment;
            this.attachmentCommentService = attachmentCommentService;
            this.purchaseService = purchaseService;
            this.ItemsService = ItemsService;
            this.organizationService = organizationService;
            this.supplierItemInfoService = supplierItemInfoService;
            this.eRPCompanyService = eRPCompanyService;
            this.billPaymentService = billPaymentService;
            this.stockService = stockService;
            this.purchaseOrderService = purchaseOrderService;
            this.userInfo = userInfo;

            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.masterId = id;
            var purchase = await purchaseOrderService.GetPurchaseOrderMasterAll();
            int Cpurchase = 0;
            Cpurchase = purchase.Where(x => Convert.ToDateTime(x.poDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd") && x.poType == "Purchase").Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "Purchase" + '-' + idate + '-' + (Cpurchase + 1);


            if (Convert.ToInt32(id) != 0)
            {
                var data = await purchaseOrderService.GetPurchaseOrderMasterById(id);
                issueNo = data.poNo;
            }
            ViewBag.PurchaseNo = issueNo;
            PurchaseViewModel model = new PurchaseViewModel
            {
                //purchaseOrderMasters = await purchaseOrderService.GetPurchaseOrderMasterAll(),
                purchaseOrderMasters = purchase.Where(x => x.poType == "Purchase"),
                specificationCategories = await ItemsService.GetAllSpecificationCategory(),
            };


            return View(model);

        }
        [Route("/api/global/getPoNo/{poDate}")]
        public async Task<JsonResult> GetPoNumber(string poDate)
        {
            var purchase = await purchaseOrderService.GetPurchaseOrderMasterAll();
            int Cpurchase = 0;
            Cpurchase = purchase.Where(x => Convert.ToDateTime(x.poDate).ToString("yyyyMMdd") == Convert.ToDateTime(poDate).ToString("yyyyMMdd") && x.poType == "Purchase").Count();
            string idate = Convert.ToDateTime(poDate).ToString("yyyy-MM-dd");
            string issueNo = "Purchase" + '-' + idate + '-' + (Cpurchase + 1);
            return Json(issueNo);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] PurchaseViewModel model)
        {
            AspNetUsersViewModel user = await userInfo.GetUserInfoByUser(HttpContext.User.Identity.Name);
            string userName = User.Identity.Name;
            var purchase = await purchaseOrderService.GetPurchaseOrderMasterAll();
            int Cpurchase = 0;
            Cpurchase = purchase.Where(x => Convert.ToDateTime(x.poDate).ToString("yyyyMMdd") == Convert.ToDateTime(model.poDate).ToString("yyyyMMdd") && x.poType == "Purchase").Count();
            string idate = Convert.ToDateTime(model.poDate).ToString("yyyy-MM-dd");
            string issueNo = "Purchase" + '-' + idate + '-' + (Cpurchase + 1);
            if (model.purchaseOrderMasterId > 0)
            {
                issueNo = model.poNo;
            }

            PurchaseOrderMaster purchaseOrderMaster = new PurchaseOrderMaster
            {
                Id = Convert.ToInt32(model.purchaseOrderMasterId),
                poNo = issueNo,
                paymentDate = model.paymentDate,
                poDate = model.poDate,
                supplierId = model.relSupplierCustomerResourseId,
                deliveryDate = model.deliveryDate,
                rfqRef = model.rfqRef,
                remarks = model.remarks,
                billToLocation = model.billToLocation,
                taxPercent = model.taxPercent,
                vatPercent = model.vatPercent,
                totalAmount = model.totalAmount,
                taxAmount = model.taxAmount,
                vatAmount = model.vatAmount,
                netTotal = model.netTotal,
                isClose = 0,
                isStockClose = 0,
                poType = "Purchase"
            };
            int PID = await purchaseOrderService.SavePurchaseOrderMaster(purchaseOrderMaster);


            if (model.purchaseOrderMasterId > 0)
            {
                await purchaseOrderService.DeletePurchaseOrderDetailsByPurchaseId(Convert.ToInt32(PID));
                //await stockService.DeletestockByPOId(Convert.ToInt32(PID));
            }
            //var sale = await stockService.GetStockMasterbyType(1);
            //int CSpurchase = 0;
            //CSpurchase = sale.Where(x => Convert.ToDateTime(x.receiveDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            //string isdate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            //string issuesNo = "MRNO" + '-' + idate + '-' + (Cpurchase + 1);
            //var GRNumber = await stockService.GetGRNumber();
            //StockMaster data = new StockMaster
            //{                
            //    receiveNo = GRNumber,
            //    receiveDate = model.poDate,
            //    StockDate = model.poDate,
            //    receiveType = "PO",
            //    purchaseId = PID,
            //    receiveBy = userName,
            //    userId = user.UserId,
            //    stockStatusId = 1
            //};  
            //var masterId = await stockService.SaveStockMaster(data);

            if (model.quantity != null)
            {
                if (model.quantity.Length > 0)
                {
                    for (int i = 0; i < model.quantity.Length; i++)
                    {
                        PurchaseOrderDetails purchaseOrderDetail = new PurchaseOrderDetails
                        {
                            // purchaseOrderDetail.Id = 0;
                            purchaseId = PID,
                            itemSpecificationId = Convert.ToInt32(model.itemSpecification[i]),
                            poQty = model.quantity[i],
                            poRate = model.rate[i],
                            //currencyId = 1
                        };

                        int did = await purchaseOrderService.SavePurchaseOrderDetails(purchaseOrderDetail);
                        //StockDetails data1 = new StockDetails
                        //{
                        //    stockMasterId = masterId,
                        //    orderDetailsId = did,
                        //    purchaseRate = model.rate[i],
                        //    grQty = model.quantity[i],
                        //    poQty = model.quantity[i],
                        //    itemSpecificationId = Convert.ToInt32(model.itemSpecification[i]),
                        //};
                        //await stockService.SaveStock(data1);
                    }
                }
            }
            return Json(PID);
        }

        [HttpPost]
        public async Task<JsonResult> SaveItemSpecification([FromForm] ItemSpecificationViewModel model)
        {
            try
            {
                int itemId = model.itemIdspec;
                int itemspecid = 0;
                if (model.specificationCategoryId != null)
                {
                    if (model.specificationCategoryId.Length > 0)
                    {

                        for (int i = 0; i < model.specificationCategoryId.Length; i++)
                        {
                            #region ItemSpecification
                            if (model.itemSpecificationName[i] != null)
                            {
                                var Itemspec = await ItemsService.GetAllItemSpecificationByitemIdandspec(itemId, model.itemSpecificationName[i]);

                                if (Itemspec.Count() > 0)
                                {
                                    itemspecid = Itemspec.FirstOrDefault().Id;
                                    await ItemsService.DeleteSpecificationDetailBySpecId(Convert.ToInt32(itemspecid));
                                }
                                else
                                {
                                    ItemSpecification itemspec = new ItemSpecification
                                    {
                                        Id = 0,
                                        itemId = Convert.ToInt32(itemId),
                                        specificationName = model.itemSpecificationName[i],
                                        isDelete = 0,
                                        createdBy = HttpContext.User.Identity.Name,
                                        createdAt = DateTime.Now
                                    };
                                    itemspecid = await ItemsService.SaveItemSpecification(itemspec);
                                }
                            }

                            #endregion
                            SpecificationDetail specificationDetail = new SpecificationDetail();
                            specificationDetail.Id = 0;
                            specificationDetail.itemSpecificationId = itemspecid;
                            specificationDetail.specificationCategoryId = model.specificationCategoryId[i];
                            specificationDetail.value = model.CategoryValue[i];

                            await ItemsService.SaveSpecificationDetail(specificationDetail);
                            specificationDetail = new SpecificationDetail();
                        }

                    }
                }


                return Json(itemId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPurchaseOrderMasterById(int purchaseId)
        {
            return Json(await purchaseOrderService.GetPurchaseOrderMasterById(purchaseId));
        }
        [HttpGet]
        public IActionResult GetPurchaseOrderDetailByPOId(int purchaseId)
        {
            return Json(purchaseOrderService.GetPurchaseOrderDetailByPOId(purchaseId));
        }

        public async Task<IActionResult> PurchaseList()
        {
            var purchase = await purchaseOrderService.GetPurchaseOrderMasterAll();
            var model = new PurchaseViewModel
            {
                purchaseOrderMasters = purchase.Where(x => x.poType == "Purchase")
            };
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult PurchaseOrderPdfAction(int MasterId)
        {

            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Purchase/PurchaseOrder/PurchaseInvoicePDF?MasterId=" + MasterId;

            string fileName;
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
        [HttpGet]
        public async Task<IActionResult> PurchaseInvoicePDF(int MasterId)
        {
            ViewBag.masterId = MasterId;
            var model = new PurchaseViewModel
            {
                GetPurchaseOrderMasterById = await purchaseOrderService.GetPurchaseOrderMasterById(MasterId),
                purchaseOrderDetails = purchaseOrderService.GetPurchaseOrderDetailByPOId(MasterId),
                companies = await eRPCompanyService.GetAllCompany(),

            };
            string Val = model.GetPurchaseOrderMasterById.netTotal.ToString();
            ViewBag.InWord = AmountInWord.ConvertToWords(Val);

            return PartialView(model);
        }

        public async Task<IActionResult> PurchaseDetail(int Id)
        {
            try
            {
                ViewBag.masterId = Id;
                //var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(Id, "Purchase");
                //if (CommentInfo == null)
                //{
                //    CommentInfo = new List<Comment>();
                //}
                var CommentInfo = new List<Comment>();
                //var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Purchase", "photo");
                //if (photoInfo == null)
                //{
                //    photoInfo = new List<DocumentAttachment>();
                //}
                var photoInfo = new List<DocumentAttachment>();
                //var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Purchase", "Document");
                //if (docInfo == null)
                //{
                //    docInfo = new List<DocumentAttachment>();
                //}
                var docInfo = new List<DocumentAttachment>();

                var model = new PurchaseViewModel
                {
                    GetPurchaseOrderMasterById = await purchaseOrderService.GetPurchaseOrderMasterById(Id),
                    purchaseOrderDetails = purchaseOrderService.GetPurchaseOrderDetailByPOId(Id),

                    comments = CommentInfo,
                    photoes = photoInfo,
                    documents = docInfo,
                };
                string Val = model.GetPurchaseOrderMasterById.netTotal.ToString();
                ViewBag.InWord = AmountInWord.ConvertToWords(Val);
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<IActionResult> PurchaseReturnDetail(int Id)
        {
            try
            {
                ViewBag.masterId = Id;
                //var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(Id, "Purchase");
                //if (CommentInfo == null)
                //{
                //    CommentInfo = new List<Comment>();
                //}
                var CommentInfo = new List<Comment>();
                //var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Purchase", "photo");
                //if (photoInfo == null)
                //{
                //    photoInfo = new List<DocumentAttachment>();
                //}
                var photoInfo = new List<DocumentAttachment>();
                //var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Purchase", "Document");
                //if (docInfo == null)
                //{
                //    docInfo = new List<DocumentAttachment>();
                //}
                var docInfo = new List<DocumentAttachment>();

                var model = new PurchaseReturnViewModel
                {
                    purchaseReturnInfoMaster = await billPaymentService.GetPurchaseReturnInfoMasterById(Id),
                    purchaseReturnInfoDetails = await billPaymentService.GetPurchaseReturnInfoDetailByMasterId(Id),

                    comments = CommentInfo,
                    photoes = photoInfo,
                    documents = docInfo,
                };
                string Val = model.purchaseReturnInfoMaster.totalAmount.ToString();
                ViewBag.InWord = AmountInWord.ConvertToWords(Val);
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public IActionResult PurchaseReturnPdfAction(int MasterId)
        {

            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Purchase/PurchaseOrder/PurchaseReturnPDF?MasterId=" + MasterId;

            string fileName;
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
        [HttpGet]
        public async Task<IActionResult> PurchaseReturnPDF(int MasterId)
        {
            ViewBag.masterId = MasterId;
            var model = new PurchaseReturnViewModel
            {
                purchaseReturnInfoMaster = await billPaymentService.GetPurchaseReturnInfoMasterById(MasterId),
                purchaseReturnInfoDetails = await billPaymentService.GetPurchaseReturnInfoDetailByMasterId(MasterId),
                companies = await eRPCompanyService.GetAllCompany(),

            };
            string Val = model.purchaseReturnInfoMaster.totalAmount.ToString();
            ViewBag.InWord = AmountInWord.ConvertToWords(Val);

            return PartialView(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteComments(int Id, int actionId)
        {
            await attachmentCommentService.DeleteCommentById(Id);
            return RedirectToAction("PurchaseDetail", "PurchaseOrder", new { id = actionId, Area = "Purchase" });
        }

        [HttpGet]
        public async Task<IActionResult> DeletePhoto(int actionId, int photoId)
        {
            var data = await attachmentCommentService.GetDocumentAttachmentById(photoId);
            var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";

            var fullFilePath = Path.Combine(filesPath, data.fileName);
            if (System.IO.File.Exists(fullFilePath))
                System.IO.File.Delete(fullFilePath);
            await attachmentCommentService.DeleteDocumentAttachmentById(photoId);
            return RedirectToAction("PurchaseDetail", "PurchaseOrder", new { id = actionId, Area = "Purchase" });
        }


        #region API
        [Route("/global/api/GetAllSupplier")]
        [HttpGet]
        public async Task<IActionResult> GetAllSupplier()
        {
            return Json(await organizationService.GetAllOrganization());

        }
        [Route("/global/api/GetPurchaseOrderMasterBySupplierId/{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetPurchaseOrderMasterBySupplierId(int Id)
        {
            return Json(await purchaseService.GetPurchaseOrderMasterBySupplierId(Id));

        }
        
        #endregion
    }
}