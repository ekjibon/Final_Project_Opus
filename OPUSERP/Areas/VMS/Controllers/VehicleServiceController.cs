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
using OPUSERP.Areas.VMS.Models;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.VMS.Data.Entity.Inspection;
using OPUSERP.VMS.Data.Entity.ServiceVehicle;
using OPUSERP.VMS.Data.Entity.VehicleInfo;
using OPUSERP.VMS.Services.CarManagement.Interfaces;
using OPUSERP.VMS.Services.FuelInfo.Interfaces;
using OPUSERP.VMS.Services.FuelStation.Interfaces;
using OPUSERP.VMS.Services.MasterData.Interfaces;
using OPUSERP.VMS.Services.SOR.Interfaces;
using OPUSERP.VMS.Services.VehicleInfo.Interfaces;
using OPUSERP.VMS.Services.VehicleService.Interfaces;

namespace OPUSERP.Areas.VMS.Controllers
{
    [Authorize]
    [Area("VMS")]
    public class VehicleServiceController : Controller
    {
        private readonly IVMSVehicleInfoService vehicleInfoService;
        private readonly IVehicleServiceHistoryService  vehicleServiceHistoryService;
        private readonly IUserInfoes userInfoes;
        private readonly ISORService sORService;
        private readonly IFuelInfoService fuelInfoService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ICarInfo carInfo;
        private readonly IItemStoreInServiceCenterService itemStoreInServiceCenterService;
        public VehicleServiceController(IHostingEnvironment hostingEnvironment, ISORService sORService, ICarInfo carInfo, IFuelInfoService fuelInfoService, IUserInfoes userInfoes, IVMSVehicleInfoService vehicleInfoService, IVehicleServiceHistoryService vehicleServiceHistoryService, IItemStoreInServiceCenterService itemStoreInServiceCenterService)
        {
            this.vehicleInfoService = vehicleInfoService;
            this.vehicleServiceHistoryService = vehicleServiceHistoryService;
            this.userInfoes = userInfoes;
            this.carInfo = carInfo;
            this.sORService = sORService;
            this.fuelInfoService = fuelInfoService;
            this.itemStoreInServiceCenterService = itemStoreInServiceCenterService;
            this._hostingEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var model = new VehicleServiceHistoryViewModel
                {
                    vehicleServiceHistories = await vehicleServiceHistoryService.GetVehicleServiceHistory(),
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        public async Task<ActionResult> IssueComment([FromForm] VehicleServiceIssueCommentsViewModel model)
        {
            try
            {
                VehicleServiceIssueComment entityComment = new VehicleServiceIssueComment
                {
                    Id = Convert.ToInt32(model.commentsId),
                    vehicleServiceIssueId=model.vehicleServiceIssueId,
                  
                    title = model.titles,
                    comment = model.comments,
                    createdBy = HttpContext.User.Identity.Name,
                };
                int commentsId = await vehicleServiceHistoryService.SaveVehicleServiceIssueComment(entityComment);
                return RedirectToAction("VehicleServiceIssueDetails", "VehicleService", new { id = model.vehicleServiceIssueId, Area = "VehicleService" });
                //  return Json(commentsId);
            }
            catch (Exception ex)
            {
                throw ex;
                //return RedirectToAction("VehicleServiceIssueDetails", "VehicleService", new { id = model.vehicleServiceIssueId, Area = "VehicleService" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> VehicleServiceIssueDoc([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
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
                        moduleId =9,
                        createdBy = userName
                    };
                    await vehicleInfoService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("VehicleServiceIssueDetails", "VehicleService", new { id = model.actionTypeId, Area = "VehicleService" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> VehicleServiceIssuePhoto([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
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
                        moduleId = 9,
                        createdBy = userName
                    };
                    await vehicleInfoService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("VehicleServiceIssueDetails", "VehicleService", new { id = model.actionTypeId, Area = "VehicleService" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteComments(int Id, int actionId)
        {
            await vehicleServiceHistoryService.DeleteVehicleServiceIssueCommentById(Id);
            return RedirectToAction("VehicleServiceIssueDetails", "VehicleService", new { id = actionId, Area = "VehicleService" });
        }
        [HttpGet]
        public async Task<IActionResult> DeletePhoto(int actionId, int photoId)
        {
            var data = await vehicleInfoService.GetDocumentAttachmentById(photoId);
            var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";

            var fullFilePath = Path.Combine(filesPath, data.fileName);
            if (System.IO.File.Exists(fullFilePath))
                System.IO.File.Delete(fullFilePath);
            await vehicleInfoService.DeleteDocumentAttachmentById(photoId);
            return RedirectToAction("VehicleServiceIssueDetails", "VehicleService", new { id = actionId, Area = "VehicleService" });
        }
        

        public async Task<IActionResult> InspectionDetails(int Id)
        {
            try
            {
                ViewBag.masterId = Id;
              
              

                var model = new VehicleInspectionViewModel
                {
                    inspectionMastersbyId=await vehicleServiceHistoryService.GetinspectionMasterById(Id),
                    inspectionDetails=await vehicleServiceHistoryService.GetinspectionDetailByMasterId(Id),
                   
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<IActionResult> VehicleMaintenanceDetails(int Id)
        {
            try
            {
                ViewBag.masterId = Id;



                var model = new VehicleMaintenanceViewModel
                {
                    vehicleMaintenanceLimitbyId = await vehicleServiceHistoryService.GetvehicleMaintenanceLimitById(Id),
                    vehicleMaintenanceLimitDetails = await vehicleServiceHistoryService.GetvehicleMaintenanceLimitDetailByMasterId(Id),

                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<IActionResult> VehicleServiceIssueDetails(int Id)
        {
            try
            {
                ViewBag.masterId = Id;
                var CommentInfo = await vehicleServiceHistoryService.GetVehicleServiceIssueCommentByIssueId(Id);
                if (CommentInfo == null)
                {
                    CommentInfo = new List<VehicleServiceIssueComment>();
                }
                var photoInfo = await vehicleInfoService.GetDocumentAttachmentByActionId(Id, "ServiceIssue", "photo");
                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                var docInfo = await vehicleInfoService.GetDocumentAttachmentByActionId(Id, "ServiceIssue", "Document");
                if (docInfo == null)
                {
                    docInfo = new List<DocumentPhotoAttachment>();
                }
               
                var model = new VehicleServiceIssuesViewModel
                {

                    vehicleServiceIssuebyId = await vehicleServiceHistoryService.GetVehicleServiceIssueById(Id),
                    vehicleServiceIssueComments = CommentInfo,
                    photoes = photoInfo,
                    documents = docInfo
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetMaintenancebyId(int fuelId)
        {
            return Json(await vehicleServiceHistoryService.GetvehicleMaintenanceLimitById(fuelId));
        }
        [HttpGet]
        public async Task<IActionResult> GetMaintenanceDetailBymasterId(int fuelId)
        {
            return Json(await vehicleServiceHistoryService.GetvehicleMaintenanceLimitDetailByMasterId(fuelId));
        }

        [HttpGet]
        public async Task<IActionResult> VehicleMaintenanceEntry(int Id)
        {
            try
            {
                ViewBag.masterId = Id;
                var model = new VehicleMaintenanceViewModel
                {
                    vehicleInformation = await vehicleInfoService.GetVehicleInformation(),
                    fuelTypes = await vehicleInfoService.GetFuelType(),
                    vehicleMaintenanceLimitbyId = await vehicleServiceHistoryService.GetvehicleMaintenanceLimitById(Id),
                    vehicleMaintenanceLimitDetails = await vehicleServiceHistoryService.GetvehicleMaintenanceLimitDetailByMasterId(Id),
                    limitPeriodTypes=await vehicleServiceHistoryService.GetlimitPeriodType(),
                    limitAmountTypes=await vehicleServiceHistoryService.GetlimitAmountType()
                };
                if (model.vehicleMaintenanceLimitbyId == null) model.vehicleMaintenanceLimitbyId = new VehicleMaintenanceLimit();

                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VehicleMaintenanceEntry([FromForm] VehicleMaintenanceViewModel model)
        {

            string createby = string.Empty;
            DateTime createAt = DateTime.Now;
            if (model.vehicleMaintenanceLimitId != 0)
            {
                var data = await vehicleServiceHistoryService.GetvehicleMaintenanceLimitById(Convert.ToInt32(model.vehicleMaintenanceLimitId));
                createby = data.createdBy;
                createAt = data.createdAt != null ? (DateTime)data.createdAt : DateTime.Now;

            }
            else
            {
                createby = HttpContext.User.Identity.Name;
            }
            VehicleMaintenanceLimit entity = new VehicleMaintenanceLimit
            {
                Id =Convert.ToInt32(model.vehicleMaintenanceLimitId),
                vehicleInformationId = model.vehicleInformationId,
              
                createdBy = createby,
                createdAt= createAt
            };
            var masterId = await vehicleServiceHistoryService.SavevehicleMaintenanceLimit(entity);
         
          

            if (model.vehicleMaintenanceLimitId > 0)
            {
                await vehicleServiceHistoryService.DeletevehicleMaintenanceLimitDetailBymasterId(masterId);
            }


            for (int i = 0; i < model.fuelTypeId.Count(); i++)
            {
                VehicleMaintenanceLimitDetail detailEntity = new VehicleMaintenanceLimitDetail
                {
                    Id = 0,
                    vehicleMaintenanceLimitId = Convert.ToInt32(masterId),
                    fuelTypeId = model.fuelTypeId[i],
                    limitPeriodTypeId=model.limitPeriodTypeId[i],
                    limitAmountTypeId=model.limitAmountTypeId[i],
                    limitValue=model.limitValue[i],
                    fromDate=model.fromDate[i],
                    toDate=model.toDate[i]
                   
                };
                await vehicleServiceHistoryService.SavevehicleMaintenanceLimitDetail(detailEntity);
            }

           

            //return RedirectToAction(nameof(Index)); 
            return RedirectToAction("VehicleMaintenanceDetails", "VehicleService", new { Id = masterId, Area = "VehicleService" });
        }


        [HttpGet]
        public async Task<IActionResult> Inspection(int Id)
        {
            try
            {
                ViewBag.masterId = Id;
                var model = new VehicleInspectionViewModel
                {
                    vehicleInformation = await vehicleInfoService.GetVehicleInformation(),
                    inspectionCheckLIstCategories = await vehicleServiceHistoryService.GetInspectionCheckLIstCategory(),
                    inspectionMastersbyId = await vehicleServiceHistoryService.GetinspectionMasterById(Id),
                    inspectionDetails = await vehicleServiceHistoryService.GetinspectionDetailByMasterId(Id),
                };
                if (model.inspectionMastersbyId == null) model.inspectionMastersbyId = new InspectionMaster();
                
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Inspection([FromForm] VehicleInspectionViewModel model, List<IFormFile> formFiles)
        {
          
            List<int?> fileCount = new List<int?>();
            foreach(var data in model.files)
            {
                if (data != null)
                {
                    fileCount.Add(data);
                }
            }
            string createby = string.Empty;
            DateTime createAt = DateTime.Now ;
            if (model.inspectionMasterId != 0)
            {
                var data = await vehicleServiceHistoryService.GetinspectionMasterById(Convert.ToInt32(model.inspectionMasterId));
                createby = data.createdBy;
                createAt = data.createdAt != null ? (DateTime)data.createdAt : DateTime.Now;

            }
            else
            {
                createby = HttpContext.User.Identity.Name;
            }
            InspectionMaster entity = new InspectionMaster
            {
                Id = Convert.ToInt32(model.inspectionMasterId),
                vehicleInformationId = model.vehicleInformationId,
                odometer = model.odometer,
                signature = model.signature,
                inspectionDate=model.inspectionDate,
                createdBy = createby,
                createdAt= createAt
            };
            var masterId = await vehicleServiceHistoryService.SaveinspectionMaster(entity);
            if (model.inspectionMasterId > 0)
            {

                var data = await vehicleServiceHistoryService.GetinspectionDetailByMasterId(masterId);
                foreach(var item in data.Where(x=>x.filename!=""))
                { 
                var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Inspection";

                var fullFilePath = Path.Combine(filesPath, item.filename);
                if (System.IO.File.Exists(fullFilePath))
                    System.IO.File.Delete(fullFilePath);
                }
                await vehicleServiceHistoryService.DeleteInspectionDetailBymasterId(masterId);
            }
            for (int i = 0; i < model.inspectionCategoryId.Count(); i++)
            {
                bool data = true;
                if (model.result[i] == 1)
                {
                    data = true;
                }
                else
                {
                    data = false;
                }

                string NewFileName = string.Empty;
                string fileLocation = string.Empty;
               
                int? index = fileCount.IndexOf(model.inspectionCategoryId[i]);
                if (index >=0)
                {
                    string documentType = "Document";
                    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Inspection";
                    var fileName = ContentDispositionHeaderValue.Parse(formFiles[Convert.ToInt32(index)].ContentDisposition).FileName;
                    string fileType = Path.GetExtension(fileName);
                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');

                    NewFileName = masterId+"_"+ model.inspectionCategoryId[i] + "_" + documentType + "_" + fileName;
                    fileLocation = "/Upload/Inspection/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await formFiles[Convert.ToInt32(index)].CopyToAsync(stream);
                    }
                }

                InspectionDetail detailEntity = new InspectionDetail
                {
                    Id = 0,
                    inspectionMasterId = Convert.ToInt32(masterId),
                    result = data,
                    inspectionCheckListCategoryId = model.inspectionCategoryId[i],
                    comments=model.description[i],
                    filename= NewFileName,
                    filePath=fileLocation
                };
                await vehicleServiceHistoryService.SaveinspectionDetail(detailEntity);
            }

          
            
            if (model.odometer > 0)
            {
                Odometer Odometerentity = new Odometer
                {
                    Id = 0,
                    vehicleInformationId = model.vehicleInformationId,
                    odometerValue = model.odometer.ToString(),
                    sortOrder = 1,
                    sourceType = "Inspection",
                    sourceTypeId = masterId,
                    readingDate = DateTime.Now,
                    createdBy = HttpContext.User.Identity.Name,
                };
                await vehicleInfoService.SaveOdometer(Odometerentity);
            }
          








            return RedirectToAction("InspectionDetails", "VehicleService", new { Id = masterId, Area = "VehicleService" });
        }
        [HttpPost]
        public async Task<ActionResult> SaveInspection(int[] AllResultIds,int[] AllCatIds,int vehicleinfoId,int inspectionMasterId, decimal odometer,string signature,string[] AllDescs,IFormFile[]formFiles )
        {
            
            InspectionMaster entity = new InspectionMaster
            {
                Id = Convert.ToInt32(inspectionMasterId),
                vehicleInformationId = vehicleinfoId,
                odometer = odometer,
                signature = signature,
                createdBy = HttpContext.User.Identity.Name,
            };
            var masterId = await vehicleServiceHistoryService.SaveinspectionMaster(entity);
            if (masterId > 0)
            {
                await vehicleServiceHistoryService.DeleteInspectionDetailBymasterId(masterId);
            }
            for (int i = 0; i < AllCatIds.Length; i++)
            {
                bool data = true;
                if (AllResultIds[i] == 1)
                {
                    data = true;
                }
                else
                {
                    data = false;
                }
                string NewFileName = "";
              

                InspectionDetail detailEntity = new InspectionDetail
                {
                    Id = 0,
                    inspectionMasterId = Convert.ToInt32(masterId),
                    result = data,
                    comments = AllDescs[i],
                    inspectionCheckListCategoryId = i + 1,
                    filename = NewFileName,







                };


                await vehicleServiceHistoryService.SaveinspectionDetail(detailEntity);

            }   

            if (odometer > 0)
            {
                Odometer Odometerentity = new Odometer
                {
                    Id = 0,
                    vehicleInformationId = vehicleinfoId,
                    odometerValue = odometer.ToString(),
                    sortOrder = 1,
                    sourceType = "Inspection",
                    sourceTypeId = masterId,
                    readingDate = DateTime.Now,
                    createdBy = HttpContext.User.Identity.Name,
                };
                await vehicleInfoService.SaveOdometer(Odometerentity);
            }
            return RedirectToAction("Inspection", "VehicleService", new { Id = masterId, Area = "VehicleService" });
        }
        public async Task<IActionResult> ServiceIssues(int Id)
        {
            try
            {
                ViewBag.masterId = Id;
                var model = new VehicleServiceIssuesViewModel
                {

                    vehicleInformation = await vehicleInfoService.GetVehicleInformation(),
                    aspNetUsersViewModels = await userInfoes.GetUserInfo(),
                    vehicleServiceIssuebyId = await vehicleServiceHistoryService.GetVehicleServiceIssueById(Id)



                };
                if (model.vehicleServiceIssuebyId == null) model.vehicleServiceIssuebyId = new VehicleServiceIssue();
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<IActionResult> VehicleMaintenanceList()
        {
            try
            {

                var model = new VehicleMaintenanceViewModel
                {

                    vehicleMaintenanceLimits = await vehicleServiceHistoryService.GetvehicleMaintenanceLimit(),
                    vehicleMaintenanceLimitDetails=await vehicleServiceHistoryService.GetvehicleMaintenanceLimitDetail()



                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<IActionResult> InspectionList()
        {
            try
            {
                var inspectionInfo = await vehicleServiceHistoryService.GetinspectionMaster();
                if (inspectionInfo == null)
                {
                    inspectionInfo = new List<InspectionMaster>();
                }
                var model = new VehicleInspectionViewModel
                {
                    inspectionMasters = inspectionInfo
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<IActionResult> ServiceIssuesList()
        {
            try
            {

                var model = new VehicleServiceIssuesViewModel
                {

                    vehicleServiceIssues = await vehicleServiceHistoryService.GetVehicleServiceIssue()



                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ServiceIssues([FromForm] VehicleServiceIssuesViewModel model)
        {


            VehicleServiceIssue entity = new VehicleServiceIssue
            {
                Id = Convert.ToInt32(model.vehicleserviceissueId),
                vehicleInformationId = model.vehicleInformationId,
                reportedDate = model.reportedDate,
                summary = model.summary,
                description = model.description,
                odometerValue = model.odometerValue,
                reportedBy = model.reportedBy,
                assignedTo = model.assignedTo,
                dueDate=model.dueDate,
                overdueOdometerValue=model.overdueOdometerValue,
                createdBy = HttpContext.User.Identity.Name,
            };
            var masterId = await vehicleServiceHistoryService.SaveVehicleServiceIssue(entity);
            if (model.formFileDoc != null)
            {
                string userName = User.Identity.Name;
                string documentType = "Document";
                var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                var fileName = ContentDispositionHeaderValue.Parse(model.formFileDoc.ContentDisposition).FileName;
                string fileType = Path.GetExtension(fileName);
                fileName = fileName.Contains("\\")
                    ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                    : fileName.Trim('"');

                string NewFileName = masterId + "_" + documentType + "_" + fileName;
                string fileLocation = "/Upload/Photo/" + NewFileName;
                var fullFilePath = Path.Combine(filesPath, NewFileName);

                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    await model.formFileDoc.CopyToAsync(stream);
                }

                DocumentPhotoAttachment data = new DocumentPhotoAttachment
                {
                    Id = 0,
                    actionType = "ServiceIssue",
                    actionTypeId = masterId,
                    subject = "Document",
                    fileName = NewFileName,
                    filePath = fileLocation,
                    fileType = fileType,
                    description = "default Document description",
                    documentType = documentType,
                    moduleId = 9,
                    createdBy = userName
                };
                await vehicleInfoService.SaveDocumentAttachment(data);
            }
            if (model.formFileImage != null)
            {
                string userName = User.Identity.Name;
                string documentType = "Photo";
                var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                var fileName = ContentDispositionHeaderValue.Parse(model.formFileImage.ContentDisposition).FileName;
                string fileType = Path.GetExtension(fileName);
                fileName = fileName.Contains("\\")
                    ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                    : fileName.Trim('"');

                string NewFileName = masterId + "_" + documentType + "_" + fileName;
                string fileLocation = "/Upload/Photo/" + NewFileName;
                var fullFilePath = Path.Combine(filesPath, NewFileName);

                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    await model.formFileImage.CopyToAsync(stream);
                }

                DocumentPhotoAttachment data = new DocumentPhotoAttachment
                {
                    Id = 0,
                    actionType = "ServiceIssue",
                    actionTypeId = masterId,
                    subject = "Photo",
                    fileName = NewFileName,
                    filePath = fileLocation,
                    fileType = fileType,
                    description = "default Photo description",
                    documentType = documentType,
                    moduleId = 9,
                    createdBy = userName
                };
                await vehicleInfoService.SaveDocumentAttachment(data);
            }
            if(model.odometerValue>0)
            { 
                Odometer Odometerentity = new Odometer
                {
                    Id = 0,
                    vehicleInformationId = model.vehicleInformationId,
                    odometerValue =model.odometerValue.ToString(),
                    sortOrder = 1,
                    sourceType = "ServiceIssue",
                    sourceTypeId = masterId,
                    readingDate = DateTime.Now,
                    createdBy = HttpContext.User.Identity.Name,
                };
                await vehicleInfoService.SaveOdometer(Odometerentity);
            }
            if (model.overdueOdometerValue > 0)
            {
                Odometer Odometerentityoverdue = new Odometer
                {
                    Id = 0,
                    vehicleInformationId = model.vehicleInformationId,
                    odometerValue = model.overdueOdometerValue.ToString(),
                    sortOrder = 1,
                    sourceType = "ServiceIssue",
                    sourceTypeId = masterId,
                    readingDate = DateTime.Now,
                    createdBy = HttpContext.User.Identity.Name,
                };
                await vehicleInfoService.SaveOdometer(Odometerentityoverdue);
            }


          

           
          

       
            return RedirectToAction("VehicleServiceIssueDetails", "VehicleService", new { Id = masterId, Area = "VehicleService" });
        }

        public async Task<IActionResult> VehicleServiceEntry(int Id)
        {
            try
            {
                ViewBag.masterId = Id;
                var model = new VehicleServiceHistoryViewModel
                {
                    spareParts=await carInfo.GetSpareParts(),
                    vehicleInformation = await vehicleInfoService.GetVehicleInformation(),
                    suppliers = await vehicleServiceHistoryService.GetSupplier(),
                    labelTypes = await vehicleServiceHistoryService.GetLabelType(),
                    serviceTasks = await vehicleServiceHistoryService.GetServiceTask(),
                    aspNetUsersViewModels = await userInfoes.GetUserInfo(),
                    

                };
               
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        // POST: VehicleServiceEntry/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VehicleServiceEntry([FromForm] VehicleServiceHistoryViewModel model, IFormFile formFile, IFormFile formFiledoc)
        {
            if (!ModelState.IsValid)
            {
                model.vehicleInformation = await vehicleInfoService.GetVehicleInformation();
                model.suppliers = await vehicleServiceHistoryService.GetSupplier();
                model.labelTypes = await vehicleServiceHistoryService.GetLabelType();
                model.serviceTasks = await vehicleServiceHistoryService.GetServiceTask();
                model.spareParts = await carInfo.GetSpareParts();
                
                return View(model);
            }

            //return Json(model);

            VehicleServiceHistory entity = new VehicleServiceHistory
            {
                Id = model.vehicleServiceHistoryId,
                vehicleInformationId = model.vehicleInformationId,
                completionDate = model.completionDate,
                startDate = model.startDate,
                supplierId = model.supplierId,
                referenceNo = model.referenceNo,
                subTotal = model.subTotalSummary,
                laborTotal = model.laborTotal,
                partsTotal = model.partsTotal,
                discountType = model.discountType,
                discount = model.discount,
                taxType = model.taxType,
                tax = model.tax,
                total = model.total,
                generalNotes = model.generalNotes,
                createdBy = HttpContext.User.Identity.Name,
            };
            var masterId = await vehicleServiceHistoryService.SaveVehicleServiceHistory(entity);

            if (model.vehicleServiceHistoryId > 0)
            {
                await vehicleServiceHistoryService.DeleteVehicleLineItemByserviceId(masterId);
                await itemStoreInServiceCenterService.DeleteItemStoreInServiceCenterByHistoryId(model.vehicleServiceHistoryId);
            }

            if (model.serviceTaskId != null)
            {
                for (int i = 0; i < model.serviceTaskId.Length; i++)
                {
                    VehicleLineItem detailEntity = new VehicleLineItem
                    {
                        Id = 0,
                        vehicleServiceHistoryId = Convert.ToInt32(masterId),
                        sparePartsId = model.serviceTaskId[i],
                        labor = model.labor[i],
                        parts = model.parts[i],
                        subTotal = model.subtotal[i],
                    };
                    await vehicleServiceHistoryService.SaveVehicleLineItem(detailEntity);
                }
            }
            Odometer Odometerentity = new Odometer
            {
                Id = 0,
                vehicleInformationId = model.vehicleInformationId,
                odometerValue = model.odometer,
                sortOrder = 1,
                sourceType = "Service",
                sourceTypeId = masterId,
                readingDate = DateTime.Now,
                createdBy = HttpContext.User.Identity.Name,
            };
            await vehicleInfoService.SaveOdometer(Odometerentity);

            ServiceLabel ServiceLabelentity = new ServiceLabel
            {
                Id = model.serviceLabelId,
                vehicleServiceHistoryId = Convert.ToInt32(masterId),
                vehicleInformationId = model.vehicleInformationId,
                labelTypeId = model.labelTypeId,
                createdBy = HttpContext.User.Identity.Name,
            };
            await vehicleServiceHistoryService.SaveServiceLabel(ServiceLabelentity);

            if (model.comment!=null)
            { 
            VehicleServiceComment entityComment = new VehicleServiceComment
            {
                Id = 0,
                vehicleServiceHistoryId = Convert.ToInt32(masterId),
                comments = model.comment,
                createdBy = HttpContext.User.Identity.Name,
            };
            await vehicleServiceHistoryService.SaveVehicleServiceComment(entityComment);
            }

            if (formFiledoc != null)
            {
                string userName = User.Identity.Name;
                string documentType = "Document";
                var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName;
                string fileType = Path.GetExtension(fileName);
                fileName = fileName.Contains("\\")
                    ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                    : fileName.Trim('"');

                string NewFileName = masterId + "_" + documentType + "_" + fileName;
                string fileLocation = "/Upload/Photo/" + NewFileName;
                var fullFilePath = Path.Combine(filesPath, NewFileName);

                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                DocumentPhotoAttachment data = new DocumentPhotoAttachment
                {
                    Id = 0,
                    actionType = "Service",
                    actionTypeId = masterId,
                    subject = "Document",
                    fileName = NewFileName,
                    filePath = fileLocation,
                    fileType = fileType,
                    description = "default Document description",
                    documentType = documentType,
                    moduleId = 9,
                    createdBy = userName
                };
                await vehicleInfoService.SaveDocumentAttachment(data);
            }
            if (formFile != null)
            {
                string userName = User.Identity.Name;
                string documentType = "Photo";
                var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName;
                string fileType = Path.GetExtension(fileName);
                fileName = fileName.Contains("\\")
                    ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                    : fileName.Trim('"');

                string NewFileName = masterId + "_" + documentType + "_" + fileName;
                string fileLocation = "/Upload/Photo/" + NewFileName;
                var fullFilePath = Path.Combine(filesPath, NewFileName);

                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                DocumentPhotoAttachment data = new DocumentPhotoAttachment
                {
                    Id = 0,
                    actionType = "Service",
                    actionTypeId = masterId,
                    subject = "Photo",
                    fileName = NewFileName,
                    filePath = fileLocation,
                    fileType = fileType,
                    description = "default Photo description",
                    documentType = documentType,
                    moduleId = 9,
                    createdBy = userName
                };
                await vehicleInfoService.SaveDocumentAttachment(data);
            }
            if (model.spearIds != null)
            {
                for(int i = 0; i < model.spearIds.Length; i++)
                {
                    ItemStoreInServiceCenter itemStoreInServiceCenter = new ItemStoreInServiceCenter
                    {
                        vehicleServiceHistoryId = masterId,
                        sparePartsId = model.spearIds[i],
                        quantity = model.quantitys[i]
                    };
                    await itemStoreInServiceCenterService.SaveItemStoreInServiceCenter(itemStoreInServiceCenter);
                }
            }

            //return RedirectToAction(nameof(Index));
            return RedirectToAction("VehicleServiceDetails", "VehicleService", new { Id = masterId, Area = "VehicleService" });

        }
        

        public async Task<IActionResult> VehicleServiceDetails(int Id)
        {
            try
            {
                ViewBag.masterId = Id;
                var vehicleInformation = await vehicleServiceHistoryService.GetVehicleServiceHistoryById(Id);
                int vid = vehicleInformation.vehicleInformationId;
                var odomtr = await vehicleInfoService.GetOdometerByVehicleId(vid);
                if (odomtr == null)
                {
                    odomtr = new Odometer();
                }
                var servicemeter= await vehicleInfoService.GetOdometerBySourceTypeId(vid, "Service", Id);
                if (servicemeter == null)
                {
                    servicemeter = new Odometer();
                }

                var CommentInfo = await vehicleServiceHistoryService.GetVehicleServiceCommentByserviceId(Id);
                if (CommentInfo == null)
                {
                    CommentInfo = new List<VehicleServiceComment>();
                }
                var photoInfo = await vehicleInfoService.GetDocumentAttachmentByActionId(Id, "Service", "photo");
                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                var docInfo = await vehicleInfoService.GetDocumentAttachmentByActionId(Id, "Service", "Document");
                if (docInfo == null)
                {
                    docInfo = new List<DocumentPhotoAttachment>();
                }

                IEnumerable<ItemStoreInServiceCenter> itemstoreInServiceCenter = await itemStoreInServiceCenterService.GetItemStoreInServiceCenterByServiceHistoryId(Id);
                if (itemstoreInServiceCenter == null)
                {
                    itemstoreInServiceCenter = new List<ItemStoreInServiceCenter>();
                }

                var model = new VehicleServiceHistoryViewModel
                {
                    vehicleServiceHistory=await vehicleServiceHistoryService.GetVehicleServiceHistoryById(Id),
                    VehicleInformationbyid = await vehicleInfoService.GetVehicleInformationDetailsById(vid),
                    vehicleStatuses = await vehicleInfoService.GetVehicleStatus(),
                    vehicleLineItems= await vehicleServiceHistoryService.GetVehicleLineItemByserviceId(Id),
                    serviceLabels = await vehicleServiceHistoryService.GetServiceLabelByvehicleServiceHistoryId(Id),
                    odometers = odomtr,
                    vehicleServiceComments = CommentInfo,
                    photoes = photoInfo,
                    itemStoreInServiceCenters = itemstoreInServiceCenter,
                    documents = docInfo
                };
                model.odometer = servicemeter.odometerValue;
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        public async Task<ActionResult> VehicleServiceComment([FromForm] VehicleServiceCommentsViewModel model)
        {
            try
            {
                VehicleServiceComment entityComment = new VehicleServiceComment
                {
                    Id = 0,
                    vehicleServiceHistoryId = Convert.ToInt32(model.vehicleServiceHistoryId),
                    titles = model.titles,
                    comments = model.comments,
                    createdBy = HttpContext.User.Identity.Name,
                };
                int commentsId = await vehicleServiceHistoryService.SaveVehicleServiceComment(entityComment);
                
                return RedirectToAction("VehicleServiceDetails", "VehicleService", new { id = model.vehicleServiceHistoryId, Area = "VehicleService" });
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> VehicleServiceDoc([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
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
                        moduleId = 9,
                        createdBy = userName
                    };
                    await vehicleInfoService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("VehicleServiceDetails", "VehicleService", new { id = model.actionTypeId, Area = "VehicleService" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> VehicleServicePhoto([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
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
                        moduleId = 9,
                        createdBy = userName
                    };
                    await vehicleInfoService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("VehicleServiceDetails", "VehicleService", new { id = model.actionTypeId, Area = "VehicleService" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteServiceComments(int Id, int actionId)
        {
            await vehicleServiceHistoryService.DeleteVehicleServiceIssueCommentById(Id);
            return RedirectToAction("VehicleServiceDetails", "VehicleService", new { id = actionId, Area = "VehicleService" });
        }
        [HttpGet]
        public async Task<IActionResult> DeleteServicePhoto(int actionId, int photoId)
        {
            var data = await vehicleInfoService.GetDocumentAttachmentById(photoId);
            var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";

            var fullFilePath = Path.Combine(filesPath, data.fileName);
            if (System.IO.File.Exists(fullFilePath))
                System.IO.File.Delete(fullFilePath);
            await vehicleInfoService.DeleteDocumentAttachmentById(photoId);
            return RedirectToAction("VehicleServiceDetails", "VehicleService", new { id = actionId, Area = "VehicleService" });
        }
        [HttpGet]
        public async Task<IActionResult> GetVehicleServiceHistoryByserviceId(int serviceId)
        {
            return Json(await vehicleServiceHistoryService.GetVehicleServiceHistoryByserviceId(serviceId));
        }
        [HttpGet]
        public async Task<IActionResult> GetVehicleLineItemByserviceId(int serviceId)
        {
            return Json(await vehicleServiceHistoryService.GetVehicleLineItemByserviceId(serviceId));
        }
        [HttpGet]
        public async Task<IActionResult> GetSorDatabyVendorId(int vid)
        {
            return Json(await sORService.GetSORDetailListByVendor(vid));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetServiceLabelByvehicleServiceHistoryId(int vehicleServiceHistoryId)
        {
            return Json(await vehicleServiceHistoryService.GetServiceLabelByvehicleServiceHistoryId( vehicleServiceHistoryId));
        }
        [HttpPost]
        public async Task<JsonResult> ChangeOdometerReading([FromForm] OdometerViewModel model)
        {
            try
            {
                string userName = User.Identity.Name;
                Odometer data = new Odometer
                {
                    Id = 0,
                    vehicleInformationId = model.vehicleInformationId,
                    odometerValue = model.odometerValue,
                    readingDate = model.readingDate,
                    createdBy = userName
                };
                int commentsId = await vehicleInfoService.SaveOdometer(data);

                return Json(commentsId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<JsonResult> SaveServiceIssue([FromForm] VehicleServiceIssuesViewModel model)
        {
            try
            {


                VehicleServiceIssue entity = new VehicleServiceIssue
                {
                    Id = Convert.ToInt32(model.vehicleserviceissueId),
                    vehicleInformationId = model.vehicleInformationIdIssue,
                    reportedDate = model.reportedDate,
                    summary = model.summary,
                    description = model.description,
                    odometerValue = model.odometerValue,
                    reportedBy = model.reportedBy,
                    assignedTo = model.assignedTo,
                    dueDate = model.dueDate,
                    overdueOdometerValue = model.overdueOdometerValue,
                    createdBy = HttpContext.User.Identity.Name,
                };
                var masterId = await vehicleServiceHistoryService.SaveVehicleServiceIssue(entity);
                if (model.formFileDoc != null)
                {
                    string userName = User.Identity.Name;
                    string documentType = "Document";
                    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                    var fileName = ContentDispositionHeaderValue.Parse(model.formFileDoc.ContentDisposition).FileName;
                    string fileType = Path.GetExtension(fileName);
                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');

                    string NewFileName = masterId + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await model.formFileDoc.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = 0,
                        actionType = "ServiceIssue",
                        actionTypeId = masterId,
                        subject = "Document",
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description = "default Document description",
                        documentType = documentType,
                        moduleId = 9,
                        createdBy = userName
                    };
                    await vehicleInfoService.SaveDocumentAttachment(data);
                }
                if (model.formFileImage != null)
                {
                    string userName = User.Identity.Name;
                    string documentType = "Photo";
                    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                    var fileName = ContentDispositionHeaderValue.Parse(model.formFileImage.ContentDisposition).FileName;
                    string fileType = Path.GetExtension(fileName);
                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');

                    string NewFileName = masterId + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await model.formFileImage.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = 0,
                        actionType = "ServiceIssue",
                        actionTypeId = masterId,
                        subject = "Photo",
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description = "default Photo description",
                        documentType = documentType,
                        moduleId = 9,
                        createdBy = userName
                    };
                    await vehicleInfoService.SaveDocumentAttachment(data);
                }
                if (model.odometerValue > 0)
                {
                    Odometer Odometerentity = new Odometer
                    {
                        Id = 0,
                        vehicleInformationId = model.vehicleInformationId,
                        odometerValue = model.odometerValue.ToString(),
                        sortOrder = 1,
                        sourceType = "ServiceIssue",
                        sourceTypeId = masterId,
                        readingDate = DateTime.Now,
                        createdBy = HttpContext.User.Identity.Name,
                    };
                    await vehicleInfoService.SaveOdometer(Odometerentity);
                }
                if (model.overdueOdometerValue > 0)
                {
                    Odometer Odometerentityoverdue = new Odometer
                    {
                        Id = 0,
                        vehicleInformationId = model.vehicleInformationId,
                        odometerValue = model.overdueOdometerValue.ToString(),
                        sortOrder = 1,
                        sourceType = "ServiceIssue",
                        sourceTypeId = masterId,
                        readingDate = DateTime.Now,
                        createdBy = HttpContext.User.Identity.Name,
                    };
                    await vehicleInfoService.SaveOdometer(Odometerentityoverdue);
                }
                return Json(masterId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetVehicleServiceIssueByVehicleId(int vid)
        {
            return Json(await vehicleServiceHistoryService.GetVehicleServiceIssueByVehicleId(vid));
        }
        [HttpPost]
        public async Task<JsonResult> SaveServiceReminder([FromForm] VehicleServiceReminderViewModel model)
        {
            try
            {


                VehicleServiceReminder entity = new VehicleServiceReminder
                {
                    Id = model.vehicleServiceReminderId,
                    vehicleInformationId = model.vehicleInformationIdReminder,
                    sparePartsId = model.serviceTaskIdReminder,

                    meterInterval = model.meterInterval,
                    timeInterval = model.timeInterval,
                    timeIntervalType = model.timeIntervalType,
                    dueMeterThreshold = model.dueMeterThreshold,
                    dueTimeThreshold = model.dueTimeThreshold,
                    dueTimeThresholdType = model.dueTimeThresholdType,
                    createdBy = HttpContext.User.Identity.Name,
                };
                var masterId = await vehicleServiceHistoryService.SaveVehicleServiceReminder(entity);

                //if (model.vehicleServiceReminderId > 0)
                //{
                //    await vehicleServiceHistoryService.DeleteVehicleLineItemByserviceId(masterId);
                //}

               
                for (int i = 0; i < model.scheduleDate.Length; i++)
                {
                    ReminderSchedule detailEntity = new ReminderSchedule
                    {
                        Id = 0,
                        vehicleServiceReminderId = masterId,
                        scheduleOdometer = model.scheduleOdometer[i],
                        scheduleDate = Convert.ToDateTime(model.scheduleDate[i])
                    };
                    await vehicleServiceHistoryService.SaveReminderSchedule(detailEntity);
                }
                return Json(masterId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetVehicleServiceReminderByVehicleId(int vid)
        {
            return Json(await vehicleServiceHistoryService.GetVehicleServiceReminderByVehicleId(vid));
        }
        #region API
        
        [HttpGet]
        [Route("/global/api/GetSupplierforParts")]
        public async Task<JsonResult> GetSupplierforParts()
        {
            return Json(await vehicleServiceHistoryService.GetSupplier());
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/global/api/GetItemStoreInServiceCenterByServiceHistoryId/{Id}")]
        public async Task<JsonResult> GetItemStoreInServiceCenterByServiceHistoryId(int Id)
        {
            return Json(await itemStoreInServiceCenterService.GetItemStoreInServiceCenterByServiceHistoryId(Id));
        }

        #endregion
    }
}