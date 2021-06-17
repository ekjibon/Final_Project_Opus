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
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Areas.Distribution.Models;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.Areas.SCMMatrix.Models;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.Distribution.Data.Entity.Requisition;
using OPUSERP.Distribution.Services.MasterData.Interfaces;
using OPUSERP.Distribution.Services.Requisition.Interfaces;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.OtherSales.Services.Sales.Interfaces;
using OPUSERP.SCM.Data.Entity.Requisition;
using OPUSERP.SCM.Services.MasterData;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using OPUSERP.SCM.Services.Matrix.Interfaces;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OPUSERP.Areas.Distribution.Controllers
{
    [Authorize]
    [Area("Distribution")]
    public class RequisitionController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
       // private readonly IItemPriceFixingService itemPriceFixingService;
        private readonly IRequsitionMasterService requsitionMasterService;
        private readonly IRequisitionDetailService requisitionDetailService;
        private readonly IAttachmentCommentService attachmentCommentService;
       private readonly ILedgerService ledgerService;
        private readonly IItemsService itemsService;
        //private readonly IStoreService storeService;
        private readonly IUserInfoes userInfoes;
        private readonly ICustomerService customerService;
        private readonly IDisItemPriceFixingService disItemPriceFixingService;
        private readonly IUserInfoes userInfo;
        private readonly IApprovalLogService approvalLogService;
        private readonly IApprovalMatrixService approvalMatrixService;
        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;


        public RequisitionController(IHostingEnvironment hostingEnvironment, IApprovalLogService approvalLogService, IApprovalMatrixService approvalMatrixService, IUserInfoes userInfo, IDisItemPriceFixingService disItemPriceFixingService, ICustomerService customerService, IItemsService itemsService, ILedgerService ledgerService, IUserInfoes userInfoes, IRequsitionMasterService requsitionMasterService, IAttachmentCommentService attachmentCommentService, IRequisitionDetailService requisitionDetailService, IConverter converter)
        {
            this._hostingEnvironment = hostingEnvironment;
            this.attachmentCommentService = attachmentCommentService;
            this.requsitionMasterService = requsitionMasterService;
            this.requisitionDetailService = requisitionDetailService;
            this.ledgerService = ledgerService;
            this.userInfo = userInfo;
            this.userInfoes = userInfoes;
            this.itemsService = itemsService;
            this.customerService = customerService;
            this.disItemPriceFixingService = disItemPriceFixingService;
            this.approvalLogService = approvalLogService;
            this.approvalMatrixService = approvalMatrixService;


            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        // GET: SalesInvoice
        public async Task<IActionResult> Index(int id)
        {
            var sale = await requsitionMasterService.GetAllRequistionMaster();
            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.requisitionDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "Req" + '-' + idate + '-' + (Cpurchase + 1);
            ViewBag.SaleNo = issueNo;
            ViewBag.Ispackage = 0;


            var MasterInfo = await requsitionMasterService.GetRequistionMasterById(id);
            if (MasterInfo == null)
            {
                MasterInfo = new OPUSERP.Distribution.Data.Entity.Requisition.RequisitionMaster();
            }
            string user = HttpContext.User.Identity.Name;
            var userinfo = await userInfoes.GetUserInfoByUser(user);
            RequisitionViewModel model = new RequisitionViewModel
            {
                masterId = id,
              requisitionDetails=await requisitionDetailService.GetAllRequisitionDetailsByMasterId(id),
                requisitionMaster = MasterInfo,
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
        public async Task<IActionResult> IndexAPP(int id)
        {
            var sale = await requsitionMasterService.GetAllRequistionMaster();
            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.requisitionDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "Req" + '-' + idate + '-' + (Cpurchase + 1);
            ViewBag.SaleNo = issueNo;
            ViewBag.Ispackage = 0;


            var MasterInfo = await requsitionMasterService.GetRequistionMasterById(id);
            if (MasterInfo == null)
            {
                MasterInfo = new OPUSERP.Distribution.Data.Entity.Requisition.RequisitionMaster();
            }
            string user = HttpContext.User.Identity.Name;
            var userinfo = await userInfoes.GetUserInfoByUser(user);
            RequisitionViewModel model = new RequisitionViewModel
            {
                masterId = id,
                requisitionDetails = await requisitionDetailService.GetAllRequisitionDetailsByMasterId(id),
                requisitionMaster = MasterInfo,
                
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
        // GET: SalesInvoice
        public async Task<IActionResult> RequisitionList()
        {
            RequisitionViewModel model = new RequisitionViewModel
            {
                requisitionMasters = await requsitionMasterService.GetAllRequistionMaster(),
            };
            return View(model);
        }
       

        public async Task<IActionResult> RequisitionDetails(int id)
        {
            ViewBag.masterId = id;
            var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(id, "Requisition", 15);
            if (CommentInfo == null)
            {
                CommentInfo = new List<Comment>();
            }
            var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "Requisition", "photo", 15);
            if (photoInfo == null)
            {
                photoInfo = new List<DocumentPhotoAttachment>();
            }
            var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "Requisition", "Document", 15);
            if (docInfo == null)
            {
                docInfo = new List<DocumentPhotoAttachment>();
            }


            RequisitionViewModel model = new RequisitionViewModel
            {
                requisitionMaster = await requsitionMasterService.GetRequistionMasterById(id),
                requisitionDetails = await requisitionDetailService.GetAllRequisitionDetailsByMasterId(id),
                comments = CommentInfo,
                photoes = photoInfo,
                documents = docInfo,
            };
           // ViewBag.InWord = AmountInWord.ConvertToWords(model.salesInvoiceMaster.totalAmount.ToString());

            return View(model);
        }

       

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> InvoicePDF(int id, string userName)
        {
            ViewBag.masterId = id;
            var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(id, "Requisition", 15);
            if (CommentInfo == null)
            {
                CommentInfo = new List<Comment>();
            }
            var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "Requisition", "photo", 15);
            if (photoInfo == null)
            {
                photoInfo = new List<DocumentPhotoAttachment>();
            }
            var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "Requisition", "Document", 15);
            if (docInfo == null)
            {
                docInfo = new List<DocumentPhotoAttachment>();
            }


            RequisitionViewModel model = new RequisitionViewModel
            {
                requisitionMaster = await requsitionMasterService.GetRequistionMasterById(id),
                requisitionDetails = await requisitionDetailService.GetAllRequisitionDetailsByMasterId(id),
                company = await ledgerService.Company(3),
                comments = CommentInfo,
                photoes = photoInfo,
                documents = docInfo,
            };
        //    ViewBag.InWord = AmountInWord.ConvertToWords(model.salesInvoiceMaster.NetTotal.ToString());

            return View(model);
        }
      

        [AllowAnonymous]
        public IActionResult InvoicePDFAction(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Distribution/Requisition/InvoicePDF?id=" + id + "&userName=" + userName;

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
        
        [Route("/api/global/getRequisitionNo/{requisitionDate}")]
        public async Task<JsonResult> getRequisitionNo(string requisitionDate)
        {
            var sale = await requsitionMasterService.GetAllRequistionMaster();
            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.requisitionDate).ToString("yyyyMMdd") == Convert.ToDateTime(requisitionDate).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(requisitionDate).ToString("yyyy-MM-dd");
            string issueNo = "Req" + '-' + idate + '-' + (Cpurchase + 1);
            return Json(issueNo);
        }
        public async Task<IActionResult> RequisitionApprovalList()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            RequisitionViewModel model = new RequisitionViewModel
            {
                requisitionApprovalViewModels = await requsitionMasterService.GetBudgetRequsitionMasterForApproval(userInfos.UserId)
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] RequisitionViewModel model)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            var sale = await requsitionMasterService.GetAllRequistionMaster();
            //return Json(model);
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}
            //if (model.customerId <= 0 || model.customerId == null)
            //{
            //    sale = await requsitionMasterService.GetAllRequistionMaster();
            //    int Cpurchase1 = 0;
            //    Cpurchase1 = sale.Where(x => Convert.ToDateTime(x.requisitionDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            //    string idate1 = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            //    string issueNo1 = "Req" + '-' + idate1 + '-' + (Cpurchase1 + 1);
            //    ViewBag.SaleNo = issueNo1;


            //    var MasterInfo = await requsitionMasterService.GetRequistionMasterById(0);
            //    if (MasterInfo == null)
            //    {
            //        MasterInfo = new OPUSERP.Distribution.Data.Entity.Requisition.RequisitionMaster();
            //    }

            //    model = new RequisitionViewModel
            //    {
            //        masterId = 0,
                   
            //        requisitionMaster = MasterInfo,
            //    };
            //    ModelState.AddModelError(string.Empty, "Distributor is not properly selected");
            //    return View(model);
            //}

            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.requisitionDate).ToString("yyyyMMdd") == Convert.ToDateTime(model.requisitionDate).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(model.requisitionDate).ToString("yyyy-MM-dd");
            string issueNo = "Req" + '-' + idate + '-' + (Cpurchase + 1);
            if (model.masterId > 0)
            {
                issueNo = model.requisitionNo;
            }
            if (model.masterId > 0)
            {
                try
                {
                    await requisitionDetailService.DeleteRequisitionDetailByMasterId(Convert.ToInt32(model.masterId));
                }
                catch
                {
                    return RedirectToAction(nameof(RequisitionDetails), new { id = model.masterId });
                }
            }

            OPUSERP.Distribution.Data.Entity.Requisition.RequisitionMaster data = new OPUSERP.Distribution.Data.Entity.Requisition.RequisitionMaster
            {
                Id = Convert.ToInt32(model.masterId),
                relSupplierCustomerResourseId = model.customerId,
                requisitionDate = model.requisitionDate,
                
                requisitionNumber = issueNo,//model.salesInvoiceNo,
                totalAmount = model.grossTotal,
                VATOnTotal = model.grossVAT,
                
                DiscountOnTotal = model.grossDiscount,
                //DiscountOnTotal = model.grossDiscount,
                NetTotal = model.netTotal,
                isApproved =0
               
            };
            var masterId = await requsitionMasterService.SaveRequisitionMaster(data);

            for (int i = 0; i < model.itemPriceFixingId.Length; i++)
            {
                int? packagedetalId = null;
                if (model.packageDetailIds[i] != 0)
                {
                    packagedetalId = model.packageDetailIds[i];
                }
                OPUSERP.Distribution.Data.Entity.Requisition.RequisitionDetail data1 = new OPUSERP.Distribution.Data.Entity.Requisition.RequisitionDetail
                {
                    Id = 0,
                   // itemSpecificationId = model.itemspecIds[i],
                    disItemPriceFixingId = model.itemPriceFixingId[i],
                    quantity = model.tblQuantity[i],
                    vatAmount = model.vattotal[i],
                
                    lineTotal = model.lineTotal[i],
                    discountAmount=model.discountTotal[i],
                    requisitionMasterId =masterId,
                    packageDetailId= packagedetalId

                };
                await requisitionDetailService.SaveRequisitionDetail(data1);
            }
            IEnumerable<ApproverPanelViewModel> lstApproverPanel = await approvalMatrixService.GetPRApproverPanelList(userName, 1, 2);
            if (model.masterId == 0 && lstApproverPanel.Count() > 0)
            {
                int i = 0;
                string notes = "";
                foreach (ApproverPanelViewModel panels in lstApproverPanel)
                {
                    int isactive = 0;
                    int? nextAppId = 0;
                    if (i == 1)
                    {
                        isactive = 1;
                        nextAppId = 0;
                        notes = "";
                    }
                    else if (i == 0)
                    {
                        isactive = 0;
                        nextAppId = panels.nextApproverId;
                        notes = "Created";
                    }
                    else
                    {
                        isactive = 0;
                        nextAppId = 0;
                        notes = "";
                    }
                    ApprovalLog appLog = new ApprovalLog
                    {
                        masterId = masterId,
                        matrixTypeId = 1,
                        userId = Convert.ToInt32(panels.nextApproverId),
                        sequenceNo = Convert.ToInt32(panels.sequenceNo),
                        isActive = isactive,
                        notes = notes,
                        nextApproverId = nextAppId
                    };
                    i = i + 1;
                    await approvalLogService.SaveApproverLog(appLog);
                }
            }

            return RedirectToAction(nameof(RequisitionDetails), new { id = masterId });
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
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("RequisitionDetails", "Requisition", new { id = model.actionTypeId, Area = "Distribution" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("RequisitionDetails", "Requisition", new { id = model.actionTypeId, Area = "Distribution" });
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
                    actionType = "Requisition",
                    actionTypeId = model.actionTypeId,
                    title = model.title,
                    comment = model.comment,
                    createdBy = HttpContext.User.Identity.Name,
                };
                int commentsId = await attachmentCommentService.SaveComment(entityComment);

                return RedirectToAction("RequisitionDetails", "Requisition", new { id = model.actionTypeId, Area = "Distribution" });
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
            return RedirectToAction("RequisitionDetails", "Requisition", new { id = actionId, Area = "Distribution" });
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
            return RedirectToAction("RequisitionDetails", "Requisition", new { id = actionId, Area = "Distribution" });
        }
        

        #region API Section

        [Route("global/api/getRequisitionDetailByMasterId/{masterId}")]
        [HttpGet]
        public async Task<IActionResult> getRequisitionDetailByMasterId(int masterId)
        {
            return Json(await requisitionDetailService.GetAllRequisitionDetailsByMasterId(masterId));
        }
        
        [Route("global/api/getpackageInfo/{masterId}")]
        [HttpGet]
        public async Task<IActionResult> getpackageInfo(int masterId)
        {
            return Json(await disItemPriceFixingService.GetAllPriceDetailsBydistributortypedate(masterId,DateTime.Now.Date));
        }

        [Route("global/api/getpackageInfodetail/{masterId}/{typeId}")]
        [HttpGet]
        public async Task<IActionResult> getpackageInfodetail(int masterId,int typeId)
        {
            return Json(await disItemPriceFixingService.GetAllPriceFixedItemSpacificationByItempackageId(masterId,typeId));
        }

        [Route("global/api/getAllRequisitionMaster/")]
        [HttpGet]
        public async Task<IActionResult> getAllRequisitionMaster()
        {
            return Json(await requsitionMasterService.GetAllRequistionMaster());
        }
        [Route("global/api/getAllResource/")]
        [HttpGet]
        public async Task<IActionResult> getAllResource()
        {
            return Json(await  requsitionMasterService.GetAllRequistionMaster());
        }
        [Route("global/api/getAllItemReq/")]
        [HttpGet]
        public async Task<IActionResult> getAllItemReq()
        {
            return Json(await itemsService.GetAllItem());
        }
        //[Route("global/api/getDisAllActiveItemFromItemPrice/")]
        //[HttpGet]
        //public async Task<IActionResult> getDisAllActiveItemFromItemPrice()
        //{
        //    return Json(await disItemPriceFixingService.GetAllActiveItemFromItemPrice());
        //}
        [Route("global/api/getAllSpacificationByItemId/{Id}")]
        [HttpGet]
        public async Task<IActionResult> getAllSpacificationByItemId(int Id)
        {
            return Json(await itemsService.GetAllItemSpecificationByitemId(Id));
        }

        [Route("global/api/getAllOpusRelSupplierCustomerResourseByRoleId/")]
        [HttpGet]
        public async Task<IActionResult> getAllOpusRelSupplierCustomerResourseByRoleId()
        {
            var data = await customerService.GetAllRelSupplierCustomerResourseByRoleDisId(3);
            return Json(data);
        }

        #endregion

        //xxxxxxxxxxxxxxxxxxxxx
    }
}
