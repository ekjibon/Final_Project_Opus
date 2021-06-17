using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.Budget.Data.Entity;
using OPUSERP.Budget.Service.Interface;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;

namespace OPUSERP.Areas.Accounting.Controllers
{
    [Authorize]
    [Area("Accounting")]
    public class FiscalYearController : Controller
    {
        private readonly IBudgetRequsitionMasterService budgetRequsitionMasterService;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public FiscalYearController(IBudgetRequsitionMasterService budgetRequsitionMasterService, IAttachmentCommentService attachmentCommentService, IHostingEnvironment _hostingEnvironment)
        {
            this.budgetRequsitionMasterService = budgetRequsitionMasterService;
            this.attachmentCommentService = attachmentCommentService;
            this._hostingEnvironment = _hostingEnvironment;
        }

        public async Task<IActionResult> Index(int? Id)
        {
            if (Id==null)
            {
                Id = 0;
            }
            var model = new FiscalYearViewModel
            {
                fiscalYear = await budgetRequsitionMasterService.GetFiscalYearById((int)Id),
            }; if (model.fiscalYear == null) model.fiscalYear = new FiscalYear();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> FiscalYearList()
        {
            var model = new FiscalYearViewModel
            {
                fiscalYears = await budgetRequsitionMasterService.GetFiscalYear(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] FiscalYearViewModel model)
        {
            //return Json(model);

            FiscalYear data = new FiscalYear
            {
                Id = (int)model.Id,
                yearName = model.yearName,
                yearCaption = model.yearCaption,
                aliasName = model.aliasName,
                yearStatus = model.yearStatus,
                yearStartFrom = model.yearStartFrom,
                yearEndOn = model.yearEndOn,
                bookStartFrom = model.bookStartFrom,
                lockMonth = model.lockMonth
            };

            int yearId = await budgetRequsitionMasterService.SaveFiscalYear(data);

            return RedirectToAction("FiscalYearDetails", "FiscalYear", new { id = yearId, Area = "Accounting" });
        }


        [HttpPost]
        public async Task<JsonResult> DeleteFiscalYearById(int Id)
        {
            await budgetRequsitionMasterService.DeleteFiscalYearById(Id);
            return Json(true);
        }

        public async Task<IActionResult> FiscalYearDetails(int id)
        {
            try
            {
                ViewBag.masterId = id;
                var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(id, "FiscalYear", 4);
                if (CommentInfo == null)
                {
                    CommentInfo = new List<Comment>();
                }
                var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "FiscalYear", "photo", 4);
                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(id, "FiscalYear", "Document", 4);
                if (docInfo == null)
                {
                    docInfo = new List<DocumentPhotoAttachment>();
                }

                var model = new FiscalYearViewModel
                {
                    fiscalYear = await budgetRequsitionMasterService.GetFiscalYearById(id),
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
                    actionType = "FiscalYear",
                    actionTypeId = model.actionTypeId,
                    title = model.title,
                    comment = model.comment,
                    moduleId = 4,
                };
                int commentsId = await attachmentCommentService.SaveComment(entityComment);
                return RedirectToAction("FiscalYearDetails", "FiscalYear", new { id = model.actionTypeId, Area = "Accounting" });

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
            return RedirectToAction("FiscalYearDetails", "FiscalYear", new { id = actionId, Area = "Accounting" });
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
                        moduleId = 4
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("FiscalYearDetails", "FiscalYear", new { id = model.actionTypeId, Area = "Accounting" });
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
                        moduleId = 4
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("FiscalYearDetails", "FiscalYear", new { id = model.actionTypeId, Area = "Accounting" });
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
            return RedirectToAction("FiscalYearDetails", "FiscalYear", new { id = actionId, Area = "Accounting" });
        }

    }
}