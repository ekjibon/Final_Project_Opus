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
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.CRO.Data.Entity.DistributeJob;
using OPUSERP.CRO.Services.DistributeJob.Interfaces;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.Helpers;

namespace OPUSERP.Areas.CRO.Controllers
{
    [Area("CRO")]
    public class JobConversionController : Controller
    {
        private readonly IDistributeJobService distributeJobService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserInfoes userInfo;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IBankBranchService bankBranchService;
        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public JobConversionController(IHostingEnvironment hostingEnvironment, IBankBranchService bankBranchService, IDistributeJobService distributeJobService, IUserInfoes userInfo, IAttachmentCommentService attachmentCommentService, IConverter converter)
        {
            this.distributeJobService = distributeJobService;
            this._hostingEnvironment = hostingEnvironment;
            this.userInfo = userInfo;
            this.attachmentCommentService = attachmentCommentService;
            this.bankBranchService = bankBranchService;
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
                //operationMasters = await distributeJobService.GetOperationMasterByassignToConverter(empCode),
                getOMAssignToConverterViewModels= await distributeJobService.GetOperationMasterByassignToConverterBySp(empCode),
                jobStatuses = await distributeJobService.GetAllJobStatus(),
                archives = await distributeJobService.GetAllArchive(),
                leadsBankInfos = await bankBranchService.GetLeadsBankInfo()
                
                

            };
            ViewBag.opMstId = mstid;
            return View(model);
        }
        public async Task<IActionResult> ConversionList(int? opMstId)
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
                //operationMasters = await distributeJobService.GetOperationMasterByassignToConverter(empCode),
                getOMAssignToConverterdViewModels= await distributeJobService.GetOperationMasterByassignToConverterdBySp(empCode),
                jobStatuses = await distributeJobService.GetAllJobStatus(),
                archives = await distributeJobService.GetAllArchive(),
                leadsBankInfos = await bankBranchService.GetLeadsBankInfo()
                
                

            };
            ViewBag.opMstId = mstid;
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> Index([FromForm] AttachmentStatusViewModel model)
        {

            string userName = User.Identity.Name;
            int declaration = 19;

            if (model.operationMasterIdJob != null)
            {
                await distributeJobService.UpdateOperationMasterConversionByConverter(Convert.ToInt32(model.operationMasterIdJob), model.jobStatusId, declaration);

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
                if (model.jobStatusId==26)
                {
                    Archive archive = new Archive();
                    archive.Id = 0;
                    archive.operationMasterId = Convert.ToInt32(model.operationMasterIdJob);
                    archive.convertDate = model.convertDate;
                    archive.reportPrintDate = model.reportPrintDate;
                    archive.ratingValidDays = model.ratingValidDays;
                    archive.ratingValidTillDate = model.ratingValidTillDate;
                    archive.ratingDate = model.ratingDate;
                  //  archive.reportPrintDate = DateTime.Now;
                    log.createdAt = DateTime.Now;
                    log.createdBy = userName;
                    await distributeJobService.SaveArchive(archive);
                    archive = new Archive();
                }
               
            }


            return Json(1);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveUploadAttachment([FromForm] AttachmentStatusViewModel model, IFormFile imagePath)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            string empCode = "";
            if (empCode != null)
            {
                empCode = userInfos.EmpCode;
            }
            if (!ModelState.IsValid)
            {
                model.operationMasters = await distributeJobService.GetOperationMasterByassignToConverter(empCode);
                model.jobStatuses = await distributeJobService.GetAllJobStatus();

                return View(model);
            }

            AttachmentStatus data = new AttachmentStatus
            {
                Id = model.attachmentStatusId,
                operationMasterId = model.operationMasterId,
                attachmentTypeId = 3,
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

                string NewFileName = attachId + "_" + documentType + "_" + fileName;
                string fileLocation = "/Upload/Photo/" + NewFileName;
                var fullFilePath = Path.Combine(filesPath, NewFileName);

                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    await imagePath.CopyToAsync(stream);
                }

                DocumentPhotoAttachment dataPH = new DocumentPhotoAttachment
                {
                    Id = 0,
                    actionType = "CRO Converter",
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
            return RedirectToAction("Index", "JobConversion", new { opMstId = actionId, Area = "CRO" });
        }

        [HttpGet]
        public async Task<IActionResult> GetOperationMasterByassignToConverterByMstId(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            string empCode = "";
            if (empCode != null)
            {
                empCode = userInfos.EmpCode;
            }
            var data = await distributeJobService.GetOperationMasterByassignToConverterBySp(empCode);
            return Json(data.Where(a => a.Id == id).FirstOrDefault());
        }

    }
}