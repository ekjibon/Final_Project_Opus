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
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Distribution.Data.Entity.MasterData;
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


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OPUSERP.Areas.Distribution.Controllers
{
    [Authorize]
    [Area("Distribution")]
    public class PackageController : Controller
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
        private readonly IDistributorTypeService distributorTypeService;


        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;


        public PackageController(IHostingEnvironment hostingEnvironment, IDistributorTypeService distributorTypeService, IDisItemPriceFixingService disItemPriceFixingService, ICustomerService customerService, IItemsService itemsService, ILedgerService ledgerService, IUserInfoes userInfoes, IRequsitionMasterService requsitionMasterService, IAttachmentCommentService attachmentCommentService, IRequisitionDetailService requisitionDetailService, IConverter converter)
        {
            this._hostingEnvironment = hostingEnvironment;
            this.attachmentCommentService = attachmentCommentService;
            this.requsitionMasterService = requsitionMasterService;
            this.requisitionDetailService = requisitionDetailService;
            this.ledgerService = ledgerService;
            this.disItemPriceFixingService = disItemPriceFixingService;

            this.userInfoes = userInfoes;
            this.itemsService = itemsService;
            this.customerService = customerService;
            this.distributorTypeService = distributorTypeService;


            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        // GET: SalesInvoice
        public async Task<IActionResult> Index(int id)
        {
            var sale = await disItemPriceFixingService.GetAllpackageMasters();
            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.packageDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "Package" + '-' + idate + '-' + (Cpurchase + 1);
            ViewBag.SaleNo = issueNo;


            var MasterInfo = await disItemPriceFixingService.GetpackageMasterById(id);
            if (MasterInfo == null)
            {
                MasterInfo = new PackageMaster();
            }
            string user = HttpContext.User.Identity.Name;
            var userinfo = await userInfoes.GetUserInfoByUser(user);
            PackagingViewModel model = new PackagingViewModel
            {
                packagemasterId = id,
                packageDetails = await disItemPriceFixingService.GetAllPackageDetailByMasterId(id),
                packageMaster = MasterInfo,
                distributorTypes=await distributorTypeService.GetAllDistributorType()
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
        public async Task<IActionResult> PackageList()
        {
            PackagingViewModel model = new PackagingViewModel
            {
                packageMasters = await disItemPriceFixingService.GetAllpackageMasters(),
            };
            return View(model);
        }


        public async Task<IActionResult> PackageDetails(int id)
        {
            ViewBag.masterId = id;
            var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(id, "Package", 15);
            if (CommentInfo == null)
            {
                CommentInfo = new List<Comment>();
            }
            var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "Package", "photo", 15);
            if (photoInfo == null)
            {
                photoInfo = new List<DocumentPhotoAttachment>();
            }
            var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "Package", "Document", 15);
            if (docInfo == null)
            {
                docInfo = new List<DocumentPhotoAttachment>();
            }


            PackagingViewModel model = new PackagingViewModel
            {
                packageMaster = await disItemPriceFixingService.GetpackageMasterById(id),
                packageDetails = await disItemPriceFixingService.GetAllPackageDetailByMasterId(id),
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
            var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(id, "Package", 15);
            if (CommentInfo == null)
            {
                CommentInfo = new List<Comment>();
            }
            var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "Package", "photo", 15);
            if (photoInfo == null)
            {
                photoInfo = new List<DocumentPhotoAttachment>();
            }
            var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "Package", "Document", 15);
            if (docInfo == null)
            {
                docInfo = new List<DocumentPhotoAttachment>();
            }



            PackagingViewModel model = new PackagingViewModel
            {
                packageMaster = await disItemPriceFixingService.GetpackageMasterById(id),
                packageDetails = await disItemPriceFixingService.GetAllPackageDetailByMasterId(id),
                comments = CommentInfo,
                photoes = photoInfo,
                documents = docInfo,
                company = await ledgerService.Company(3),
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

            string url = scheme + "://" + host + "/Distribution/Package/InvoicePDF?id=" + id + "&userName=" + userName;

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

        [Route("/api/global/getPackageNo/{packageDate}")]
        public async Task<JsonResult> getPackageNo(string requisitionDate)
        {
            var sale = await disItemPriceFixingService.GetAllpackageMasters();
            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.packageDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "Package" + '-' + idate + '-' + (Cpurchase + 1);
            return Json(issueNo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] PackagingViewModel model)
        {
            var sale = await disItemPriceFixingService.GetAllpackageMasters();


            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.packageDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "Package" + '-' + idate + '-' + (Cpurchase + 1);
            if (model.packagemasterId > 0)
            {
                issueNo = model.packageNo;
            }
            if (model.packagemasterId > 0)
            {
                try
                {
                    await disItemPriceFixingService.DeletePackageDetailByMasterId(Convert.ToInt32(model.packagemasterId));
                }
                catch
                {
                    return RedirectToAction(nameof(PackageDetails), new { id = model.packagemasterId });
                }
            }

            PackageMaster data = new PackageMaster
            {
                Id = Convert.ToInt32(model.packagemasterId),
                packageDate = model.packageDate,
                packageNo = model.packageNo,
                packageName = model.packageName,
                startDate = model.startDate,
                endDate = model.endDate,
                description=model.description,
                distributorTypeId=model.distributorTypeId


            };
            var masterId = await disItemPriceFixingService.SavePackageMaster(data);

            for (int i = 0; i < model.itemSpecificationIdall.Length; i++)
            {
                int isf = 0;
                if (model.isfreeall[i] == "true")
                {
                    isf = 1;
                }
                PackageDetail data1 = new PackageDetail
                {
                    Id = 0,
                    itemSpecificationId = model.itemSpecificationIdall[i],
                    quantity = model.quantityall[i],
                    isfree = isf,
                    discountPersent=model.discountPersentall[i],
                    packageMasterId=masterId

                };
                await disItemPriceFixingService.SavePackageDetail(data1);
            }

            return RedirectToAction(nameof(PackageDetails), new { id = masterId });
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

                return RedirectToAction("PackageDetails", "Package", new { id = model.actionTypeId, Area = "Distribution" });
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

                return RedirectToAction("PackageDetails", "Package", new { id = model.actionTypeId, Area = "Distribution" });
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
                    actionType = "Package",
                    actionTypeId = model.actionTypeId,
                    title = model.title,
                    comment = model.comment,
                    createdBy = HttpContext.User.Identity.Name,
                };
                int commentsId = await attachmentCommentService.SaveComment(entityComment);

                return RedirectToAction("PackageDetails", "Package", new { id = model.actionTypeId, Area = "Distribution" });
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
            return RedirectToAction("PackageDetails", "Package", new { id = actionId, Area = "Distribution" });
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
            return RedirectToAction("PackageDetails", "Package", new { id = actionId, Area = "Distribution" });
        }


        #region API Section

        [Route("global/api/getPackageDetailByMasterId/{masterId}")]
        [HttpGet]
        public async Task<IActionResult> getPackageDetailByMasterId(int masterId)
        {
            return Json(await disItemPriceFixingService.GetAllPackageDetailByMasterId(masterId));
        }

        [Route("global/api/getAllPackageMaster/")]
        [HttpGet]
        public async Task<IActionResult> getAllPackageMaster()
        {
            return Json(await disItemPriceFixingService.GetAllpackageMasters());
        }
        
        //[Route("global/api/getAllItemReq/")]
        //[HttpGet]
        //public async Task<IActionResult> getAllItemReq()
        //{
        //    return Json(await itemsService.GetAllItem());
        //}
        //[Route("global/api/getAllSpacificationByItemId/{Id}")]
        //[HttpGet]
        //public async Task<IActionResult> getAllSpacificationByItemId(int Id)
        //{
        //    return Json(await itemsService.GetAllItemSpecificationByitemId(Id));
        //}

        //[Route("global/api/getAllOpusRelSupplierCustomerResourseByRoleId/")]
        //[HttpGet]
        //public async Task<IActionResult> getAllOpusRelSupplierCustomerResourseByRoleId()
        //{
        //    var data = await customerService.GetAllRelSupplierCustomerResourseByRoleId(3);
        //    return Json(data);
        //}

        #endregion

        //xxxxxxxxxxxxxxxxxxxxx
    }
}
