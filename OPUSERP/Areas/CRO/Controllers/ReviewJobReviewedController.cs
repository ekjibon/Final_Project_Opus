using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRO.Models;
using OPUSERP.CRO.Data.Entity.DistributeJob;
using OPUSERP.CRO.Services.DistributeJob.Interfaces;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.Helpers;

namespace OPUSERP.Areas.CRO.Controllers
{
    [Area("CRO")]
    public class ReviewJobReviewedController : Controller
    {
        private readonly IDistributeJobService distributeJobService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserInfoes userInfo;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public ReviewJobReviewedController(IHostingEnvironment hostingEnvironment, IDistributeJobService distributeJobService, IUserInfoes userInfo, IAttachmentCommentService attachmentCommentService, IConverter converter)
        {
            this.distributeJobService = distributeJobService;
            this._hostingEnvironment = hostingEnvironment;
            this.userInfo = userInfo;
            this.attachmentCommentService = attachmentCommentService;
            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }
        public async Task<IActionResult> Index(int? opMstId)
        {
            int? mstid = 0;
            if (opMstId == null || opMstId == 0)
            {
                mstid = 0;
            }
            else
            {
                mstid = opMstId;
            }

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            string empCode = "";
            if (empCode != null)
            {
                empCode = userInfos.EmpCode;
            }

            AttachmentStatusViewModel model = new AttachmentStatusViewModel()
            {
                //operationMasters = await distributeJobService.GetOperationMasterByassignToReviewerTL(empCode),
                //operationMasters = await distributeJobService.GetOperationMasterByassignToReviewer(empCode),
                getOMByassignToReviewerViewModels = await distributeJobService.GetOperationMasterByassignToReviewerBySp(empCode),
                jobStatuses = await distributeJobService.GetAllJobStatus(),
                documentCharts = await distributeJobService.GetAllDocumentChart(),
                attachmentTypes = await distributeJobService.GetAllAttachmentType(),

            };
            ViewBag.opMstId = mstid;
            return View(model);
        }
        public async Task<IActionResult> ReviewList(int? opMstId)
        {
            int? mstid = 0;
            if (opMstId == null || opMstId == 0)
            {
                mstid = 0;
            }
            else
            {
                mstid = opMstId;
            }

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            string empCode = "";
            if (empCode != null)
            {
                empCode = userInfos.EmpCode;
            }

            AttachmentStatusViewModel model = new AttachmentStatusViewModel()
            {
                //operationMasters = await distributeJobService.GetOperationMasterByassignToReviewerTL(empCode),
                //operationMasters = await distributeJobService.GetOperationMasterByassignToReviewer(empCode),
                getOMReviewerListViewModels = await distributeJobService.GetOMReviewerListViewModels(empCode),
                jobStatuses = await distributeJobService.GetAllJobStatus(),
                documentCharts = await distributeJobService.GetAllDocumentChart(),
                attachmentTypes = await distributeJobService.GetAllAttachmentType(),

            };
            ViewBag.opMstId = mstid;
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> Index([FromForm] AttachmentStatusViewModel model)
        {

            string userName = User.Identity.Name;
            int declaration = 21;

            if (model.operationMasterIdJob != null)
            {
                await distributeJobService.UpdateOperationMasterReviewedByReviewer(Convert.ToInt32(model.operationMasterIdJob), model.jobStatusId, declaration,  model.remarks, model.reportNo);

                StatusLog log = new StatusLog();

                log.Id = 0;
                log.operationMasterId = Convert.ToInt32(model.operationMasterIdJob);
                log.jobStatusId = model.jobStatusId;
                log.remarks = model.remarks;
                log.currentUser = userName;
                log.createdAt = DateTime.Now;
                log.createdBy = userName;
                await distributeJobService.SaveStatusLog(log);
                log = new StatusLog();
            }


            return Json(1);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveUploadAttachment([FromForm] AttachmentStatusViewModel model, IFormFile imagePath)
        {
            string NewFileName = string.Empty;
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
           
            string empCode = "";
            if (empCode != null)
            {
                empCode = userInfos.EmpCode;
            }
            if (!ModelState.IsValid)
            {
                model.operationMasters = await distributeJobService.GetOperationMasterByassignToReviewer(empCode);
                model.documentCharts = await distributeJobService.GetAllDocumentChart();
                model.jobStatuses = await distributeJobService.GetAllJobStatus();

                return View(model);
            }

            AttachmentStatus data = new AttachmentStatus
            {
                Id = model.attachmentStatusId,
                operationMasterId = model.operationMasterId,
                attachmentTypeId = model.attachmentTypeId,
                fileTypeName = model.fileTypeName,
                subjectName = model.subjectName
            };

            int attachId = await distributeJobService.SaveAttachmentStatus(data);

            if (imagePath != null)
            {
                string documentType = "Document";
                var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                var fileName = ContentDispositionHeaderValue.Parse(imagePath.ContentDisposition).FileName;
                string fileType = Path.GetExtension(fileName);
                fileName = fileName.Contains("\\")
                    ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                    : fileName.Trim('"');

                NewFileName = attachId + "_" + documentType + "_" + fileName;
                string fileLocation = "/Upload/Photo/" + NewFileName;
                var fullFilePath = Path.Combine(filesPath, NewFileName);

                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    await imagePath.CopyToAsync(stream);
                }

                DocumentPhotoAttachment dataPH = new DocumentPhotoAttachment
                {
                    Id = 0,
                    actionType = "CRO Reviewer",
                    actionTypeId = model.operationMasterId,
                    subject = model.subjectName,
                    fileName = NewFileName,
                    filePath = fileLocation,
                    fileType = fileType,
                    description = attachId.ToString(),
                    documentType = documentType,
                    moduleId = 13,
                    //createdBy = userName
                };
                await attachmentCommentService.SaveDocumentAttachment(dataPH);
            }

            StatusLog statusdata = new StatusLog
            {
                Id = 0,
                operationMasterId = Convert.ToInt32(model.operationMasterId),
                jobStatusId = 2061,
                currentUser = HttpContext.User.Identity.Name,
                remarks = model.subjectName + "-" + NewFileName
            };

            int DocumentId = await distributeJobService.SaveStatusLog(statusdata);

            return RedirectToAction(nameof(Index), new
            {
                opMstId = model.operationMasterId
            });
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

            #region
            StatusLog statusdata = new StatusLog
            {
                Id = 0,
                operationMasterId = Convert.ToInt32(actionId),
                jobStatusId = 2062,
                currentUser = HttpContext.User.Identity.Name,
                remarks = data.fileName
            };
            int DocumentId = await distributeJobService.SaveStatusLog(statusdata);
            #endregion

            return RedirectToAction("Index", "ReviewJobReviewed", new { opMstId = actionId, Area = "CRO" });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteReceiveDocument(int operMstId, int Id)
        {
            await distributeJobService.DeleteReceiveDocumentById(Id);
            return RedirectToAction("Index", "ReviewJobReviewed", new { opMstId = operMstId, Area = "CRO" });
        }
    }
}