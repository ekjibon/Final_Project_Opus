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
using OPUSERP.Areas.Inventory.Models;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.Areas.Purchase.Models;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.Purchase.Data.Entity;
using OPUSERP.Purchase.Service.Interfaces;
using OPUSERP.SCM.Data.Entity.Stock;
using OPUSERP.SCM.Services.Stock.Interface;

namespace OPUSERP.Areas.Purchase
{
    [Authorize]
    [Area("Purchase")]
    public class TransferController : Controller
    {
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public string FileName;
        private readonly IStockService stockService;
        private readonly IStoreService storeService;
        private readonly ITransferService transferService;


        public TransferController(IHostingEnvironment hostingEnvironment, IStockService stockService, IConverter converter, IAttachmentCommentService attachmentCommentService, IStoreService storeService, ITransferService transferService)
        {
            this.stockService = stockService;
            this._hostingEnvironment = hostingEnvironment;
            this.storeService = storeService;
            this.attachmentCommentService = attachmentCommentService;
            this.transferService = transferService;

            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.masterId = id;
            var transfer = await transferService.GetAllTransferMaster();
            int Cpurchase = 0;
            Cpurchase = transfer.Where(x => Convert.ToDateTime(x.transferDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "TR" + '-' + idate + '-' + (Cpurchase + 1);


            if (Convert.ToInt32(id) != 0)
            {
                var data = await transferService.GetAllTransferMasterByMasterId(id);
                issueNo = data.transferNO;
            }
            ViewBag.transferNO = issueNo;
            TransferViewModel model = new TransferViewModel
            {
                stores = await storeService.GetAllStore(),
                storeAssigns = await storeService.GetAllStoreAssignByUser(HttpContext.User.Identity.Name)
                //purchaseOrderMasters = await purchaseService.GetPurchaseOrderMaster(),
                //specificationCategories = await ItemsService.GetAllSpecificationCategory(),
            };


            return View(model);

        }
        [Route("/api/global/getTRNumber/{transferDate}")]
        public async Task<JsonResult> GetTRNumber(string transferDate)
        {
            var transfer = await transferService.GetAllTransferMaster();
            int Cpurchase = 0;
            Cpurchase = transfer.Where(x => Convert.ToDateTime(x.transferDate).ToString("yyyyMMdd") == Convert.ToDateTime(transferDate).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(transferDate).ToString("yyyy-MM-dd");
            string issueNo = "TR" + '-' + idate + '-' + (Cpurchase + 1);
            return Json(issueNo);
        }
        [HttpPost]
        public async Task<JsonResult> Index([FromForm] TransferViewModel model)
        {
            try
            {
                decimal price = 0;
                string userName = User.Identity.Name;
                var transfer = await transferService.GetAllTransferMaster();
                int Cpurchase = 0;
                Cpurchase = transfer.Where(x => Convert.ToDateTime(x.transferDate).ToString("yyyyMMdd") == Convert.ToDateTime(model.transferDate).ToString("yyyyMMdd")).Count();
                string idate = Convert.ToDateTime(model.transferDate).ToString("yyyy-MM-dd");
                string issueNo = "TR" + '-' + idate + '-' + (Cpurchase + 1);
                if (model.transferId > 0)
                {
                    issueNo = model.transferNO;
                }
                TransferMaster transferMaster = new TransferMaster
                {

                    Id = Convert.ToInt32(model.transferId),
                    transferNO = issueNo,
                    transferDate = model.transferDate,
                    storeFromId = model.storeFromId,
                    storeToId = model.storeToId,
                    remarks = "",
                    //companyId = 1,
                    isStockClose = 0,
                    createdBy = userName
                };
                int TID = await transferService.SaveTransferMaster(transferMaster);

                if (model.transferId > 0)
                {
                    var stock = await stockService.GetstockMasterIdBytransferMasterId(TID);
                    await stockService.DeleteStockMasterById(Convert.ToInt32(stock.stockMasterId));
                    await stockService.DeleteStockMasterByMasterId(Convert.ToInt32(stock.stockMasterId));
                    await transferService.DeleteTransferDetailsBytransferMasterId(Convert.ToInt32(TID));
                }

                StockMaster data = new StockMaster
                {
                    Id = 0,
                    //companyId = 1,
                    //remarks = "",
                    MRNO = issueNo,
                    StockDate = model.transferDate,
                    stockStatusId = 2,
                    storeId = model.storeFromId

                };
                int stockmasterId = await stockService.SaveStockMaster(data);


                if (model.quantity != null)
                {
                    if (model.quantity.Length > 0)
                    {

                        for (int i = 0; i < model.quantity.Length; i++)
                        {
                            TransferDetail transferDetail = new TransferDetail();

                            transferDetail.Id = 0;
                            transferDetail.transferMasterId = TID;

                            transferDetail.itemSpecificationId = Convert.ToInt32(model.itemSpecification[i]);
                            transferDetail.qty = model.quantity[i];
                            var rate = await stockService.GetstockMasterByitemSpecificationId(Convert.ToInt32(model.itemSpecification[i]));
                            price = Convert.ToDecimal(rate.rate);
                            transferDetail.rate = price;

                            int transferDetailId = await transferService.SaveTransferDetail(transferDetail);
                            transferDetail = new TransferDetail();

                            StockDetails data1 = new StockDetails
                            {
                                Id = 0,
                                stockMasterId = stockmasterId,
                                transferDetailId = transferDetailId,
                                rate = price,
                                itemSpecificationId = Convert.ToInt32(model.itemSpecification[i]),
                                grQty = model.quantity[i]
                            };
                            await stockService.SaveStock(data1);
                        }

                    }
                }

                return Json(TID);
            }
            catch (Exception ex)
            {
                return Json(ex);
                //throw ex;
            }


        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransferMasterByMasterId(int Id)
        {
            return Json(await transferService.GetAllTransferMasterByMasterId(Id));
        }
        [HttpGet]
        public IActionResult GetAllTransferDetailsBytransferMasterId(int Id)
        {
            return Json(transferService.GetAllTransferDetailsBytransferMasterId(Id));
        }

        [HttpGet]
        public async Task<IActionResult> TransferList()
        {
            TransferViewModel model = new TransferViewModel
            {
                transferMasters = await transferService.GetAllTransferMasterByUser(HttpContext.User.Identity.Name)

            };
            return View(model);
        }




        public async Task<IActionResult> TransferDetail(int id)
        {
            try
            {
                ViewBag.masterId = id;


                var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(id, "Transfer",0);
                if (CommentInfo == null)
                {
                    CommentInfo = new List<Comment>();
                }
                var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "Transfer", "photo",0);
                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "Transfer", "Document",0);
                if (docInfo == null)
                {
                    docInfo = new List<DocumentPhotoAttachment>();
                }
                // var stockdetails = await stockService.GetAllStockByMasterId(id);


                var model = new TransferViewModel
                {
                    GetTransferMasterByMasterId = await transferService.GetAllTransferMasterByMasterId(id),
                    GetTransferDetailByMasterId = transferService.GetAllTransferDetailsBytransferMasterId(id),
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

            string url = scheme + "://" + host + "/Inventory/Transfer/StockOutPdf?id=" + MasterId;

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
                    stockMasterById = await stockService.GetStockMasterById(id),
                    GetStocksbyMasterId = await stockService.GetStockDetailsByMasterId(id),


                };

                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpPost]
        public async Task<ActionResult> SaveComment([FromForm] CommentsViewModel model)
        {
            try
            {
                Comment entityComment = new Comment
                {
                    Id = Convert.ToInt32(model.commentsId),
                    actionType = "Transfer",
                    actionTypeId = model.actionTypeId,
                    title = model.title,
                    comment = model.comment,
                    moduleId=0,
                    createdBy = HttpContext.User.Identity.Name,
                };
                int commentsId = await attachmentCommentService.SaveComment(entityComment);
                return RedirectToAction("TransferDetail", "Transfer", new { id = model.actionTypeId, Area = "Inventory" });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        public async Task<IActionResult> DeleteComments(int Id, int actionId)
        {
            await attachmentCommentService.DeleteCommentById(Id);
            return RedirectToAction("TransferDetail", "Transfer", new { id = actionId, Area = "Inventory" });
        }

        [HttpPost]
        public async Task<IActionResult> SaveDoc([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
        {
            try
            {
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
                        createdBy = userName,
                        moduleId = 0,
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("TransferDetail", "Transfer", new { id = model.actionTypeId, Area = "Inventory" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> SavePhoto([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
        {
            try
            {
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
                        createdBy = userName,
                        moduleId=0
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("TransferDetail", "Transfer", new { id = model.actionTypeId, Area = "Inventory" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            return RedirectToAction("TransferDetail", "Transfer", new { id = actionId, Area = "Inventory" });
        }

        #region API Section
        [Route("global/api/OnHanStockBySpecIdForTransfer/{id}")]
        [HttpGet]
        public async Task<IActionResult> OnHanStockBySpecIdForTransfer(int id)
        {
            return Json(await stockService.OnHanStockBySpecIdForTransfer(id));
        }

        [Route("global/api/GetAllTransferDetailsBytransferMasterId/{id}")]
        [HttpGet]
        public IActionResult GetAllTransferDetailsBytransferId(int id)
        {
            return Json(transferService.GetAllTransferDetailsBytransferMasterId(id));
        }

        #endregion

    }
}