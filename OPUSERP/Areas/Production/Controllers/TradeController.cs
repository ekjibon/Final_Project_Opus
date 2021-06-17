using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using OPUSERP.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Areas.Production.Models;
using OPUSERP.Production.Data.Entity;
using OPUSERP.Production.Services.AttachmentComment.Interfaces;
using OPUSERP.Production.Services.Interfaces;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using DocumentAttachmentViewModel = OPUSERP.Areas.Production.Models.DocumentAttachmentViewModel;

namespace OPUSERP.Areas.Production.Controllers
{
    //[Authorize]
    [Area("Production")]
    public class TradeController : Controller
    {
        private readonly IBOMService  bOMService;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string rootPath;
        private readonly MyPDF myPDF;
        private readonly IItemsService ItemsService;
        

        public string FileName;
        public TradeController(IHostingEnvironment hostingEnvironment, IAttachmentCommentService attachmentCommentService, IConverter converter, IBOMService bOMService, IItemsService ItemsService)
        {

            this._hostingEnvironment = hostingEnvironment;
            this.attachmentCommentService = attachmentCommentService;
            this.bOMService = bOMService;
            this.ItemsService = ItemsService;
           

            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.masterId = id;
            var purchase = await bOMService.GetAllBOMMaster();
            int Cpurchase = 0;
            Cpurchase = purchase.Where(x => x.operationType == "Trade").Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "Trade" + '-' + idate + '-' + (Cpurchase + 1);


            if (Convert.ToInt32(id) != 0)
            {
                var data = await bOMService.GetBOMMasterById(id);
                issueNo = data.bomNo;
            }
            ViewBag.bomNo = issueNo;
            BOMViewModel model = new BOMViewModel
            {
                bOMMasters = await bOMService.GetAllBOMMaster(),
               
            };


            return View(model);

        }
        [Route("/api/global/GetTradeNo/")]
        public async Task<JsonResult> GetTradeNo()
        {
            var purchase = await bOMService.GetAllBOMMaster();
            int Cpurchase = 0;
            Cpurchase = purchase.Where(x => x.operationType == "Trade").Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "Trade" + '-' + idate + '-' + (Cpurchase + 1);
            return Json(issueNo);
        }
        [HttpPost]
        public async Task<JsonResult> Index([FromForm] BOMViewModel model)
        {
            
            string userName = User.Identity.Name;
            var purchase = await bOMService.GetAllBOMMaster();
            int Cpurchase = 0;
            Cpurchase = purchase.Where(x=>x.operationType== "Trade").Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "Trade" + '-' + idate + '-' + (Cpurchase + 1);
            if (model.bOMMasterId > 0)
            {
                issueNo = model.bomNo;
            }

            BOMMaster bOMMaster = new BOMMaster
            {

                Id = Convert.ToInt32(model.bOMMasterId),

                bomNo = issueNo,
                itemSpecificationId = model.bomitemSpecificationId,
                bomItemDescription = model.bomItemDescription,
                operationType= "Trade",
                quantity = model.bomQty,
                createdBy = userName
            };
            int BOMID = await bOMService.SaveBOMMaster(bOMMaster);

            if (model.bOMMasterId > 0)
            {
                await bOMService.DeleteBOMOverheadDetailByBOMMasterId(Convert.ToInt32(BOMID));
            }
            
            if (model.ledgerId != null)
            {
                if (model.ledgerId.Length > 0)
                {

                    for (int i = 0; i < model.ledgerId.Length; i++)
                    {
                        BOMOverheadDetail bOMOverheadDetail = new BOMOverheadDetail();

                        bOMOverheadDetail.Id = 0;
                        bOMOverheadDetail.bOMMasterId = BOMID;
                        bOMOverheadDetail.overheadCostId = Convert.ToInt32(model.ledgerId[i]);
                        bOMOverheadDetail.cost = model.cost[i];
                        bOMOverheadDetail.costType = model.costType[i];

                        await bOMService.SaveBOMOverheadDetail(bOMOverheadDetail);
                        bOMOverheadDetail = new BOMOverheadDetail();
                    }

                }
            }

            return Json(BOMID);

        }
        
        [HttpGet]
        public async Task<IActionResult> GetBOMMasterById(int BOMMasterId)
        {
            return Json(await bOMService.GetBOMMasterById(BOMMasterId));
        }
        [HttpGet]
        public async Task<IActionResult> GetBOMDetailByBOMMasterId(int BOMMasterId)
        {
            return Json(await bOMService.GetBOMDetailByBOMMasterId(BOMMasterId));
        }
        [HttpGet]
        public async Task<IActionResult> GetBOMOverheadDetailByBOMMasterId(int BOMMasterId)
        {
            return Json(await bOMService.GetBOMOverheadDetailByBOMMasterId(BOMMasterId));
        }

        public async Task<IActionResult> TradeList()
        {
            var model = new BOMViewModel
            {
                bOMMasters = await bOMService.GetAllBOMMaster(),
            };
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult TradePdfAction(int MasterId)
        {

            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Production/Trade/PurchaseInvoicePDF?MasterId=" + MasterId;

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
        public async Task<IActionResult> TradeInvoicePDF(int MasterId)
        {
            ViewBag.masterId = MasterId;
            var model = new BOMViewModel
            {
                //GetPurchaseOrderMasterById = await purchaseService.GetPurchaseOrderMasterById(MasterId),
                //purchaseOrderDetails = purchaseService.GetPurchaseOrderDetailByPOId(MasterId),
                //company = await ledgerService.Company(Convert.ToInt32(1)),

            };
           

            return PartialView(model);
        }
        public async Task<IActionResult> TradeDetail(int Id)
        {
            try
            {
                ViewBag.masterId = Id;
                var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(Id, "Production");
                if (CommentInfo == null)
                {
                    CommentInfo = new List<Comment>();
                }
                var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Production", "photo");
                if (photoInfo == null)
                {
                    photoInfo = new List<ProductionDocumentAttachment>();
                }
                var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Production", "Document");
                if (docInfo == null)
                {
                    docInfo = new List<ProductionDocumentAttachment>();
                }

                var model = new BOMViewModel
                {
                    GetBOMMasterById = await bOMService.GetBOMMasterById(Id),
                    bOMDetails = await bOMService.GetBOMDetailByBOMMasterId(Id),

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
        [HttpPost]
        public async Task<ActionResult> SaveComment([FromForm] CommentsViewModel model)
        {
            try
            {
                Comment entityComment = new Comment
                {
                    Id = Convert.ToInt32(model.commentsId),
                    actionType = "Production",
                    actionTypeId = model.actionTypeId,
                    title = model.title,
                    comment = model.comment,
                    createdBy = HttpContext.User.Identity.Name,
                };
                int commentsId = await attachmentCommentService.SaveComment(entityComment);
                return RedirectToAction("PurchaseDetail", "Trade", new { id = model.actionTypeId, Area = "Production" });

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
            return RedirectToAction("PurchaseDetail", "Trade", new { id = actionId, Area = "Production" });
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

                    ProductionDocumentAttachment data = new ProductionDocumentAttachment
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
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("PurchaseDetail", "Trade", new { id = model.actionTypeId, Area = "Production" });
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

                    ProductionDocumentAttachment data = new ProductionDocumentAttachment
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
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("PurchaseDetail", "Trade", new { id = model.actionTypeId, Area = "Production" });
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
            return RedirectToAction("PurchaseDetail", "Trade", new { id = actionId, Area = "Production" });
        }
    }
}