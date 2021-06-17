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
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.Requisition;
using OPUSERP.VMS.Services.Requisition.Interfaces;
using OPUSERP.VMS.Services.VehicleInfo.Interfaces;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.Data.Entity.AttachmentComment;

namespace OPUSERP.Areas.VMS.Controllers
{
    [Authorize]
    [Area("VMS")]
    public class RequisitionController : Controller
    {
        private readonly IVMSVehicleInfoService vehicleInfoService;
        //private readonly LangGenerate<VehicleLN> _lang;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserInfoes userInfoes;
        private readonly IVMSRequisitionService requisitionService;

        public RequisitionController(IHostingEnvironment hostingEnvironment, IVMSRequisitionService requisitionService, IUserInfoes userInfoes, IVMSVehicleInfoService vehicleInfoService)
        {
            this.vehicleInfoService = vehicleInfoService;
            this.userInfoes = userInfoes;
            this.requisitionService = requisitionService;
            //_lang = new LangGenerate<VehicleLN>(hostingEnvironment.ContentRootPath);
            this._hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> GetFluidDetailByvehicleinfoId(int fuelId)
        {
            return Json(await vehicleInfoService.GetVehicleFluidByInfoId(fuelId));
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id, string actionType)
        {
            ViewBag.masterId = Convert.ToInt32(id);
            if (actionType == string.Empty || actionType == null)
            {
                actionType = "defaultOpen";
            }
            var Requisitions = await requisitionService.GetrequisitionMaster();
            int CRequisiton = 0;
            CRequisiton = Requisitions.Where(x => Convert.ToDateTime(x.createdAt).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = idate + '/' + (CRequisiton + 1);

            int viid = Convert.ToInt32(id);
            if (Convert.ToInt32(id) != 0)
            {
                var data = await requisitionService.GetrequisitionMasterById(viid);
                issueNo = data.reqNo;
            }
            ViewBag.ReqNo = issueNo;
            try
            {
                RequisitionViewModel model = new RequisitionViewModel
                {
                    vehicleTypes = await vehicleInfoService.GetVehicleType(),
                    vehicleModels = await vehicleInfoService.GetVehicleModel(),
                    vehicleManufactures = await vehicleInfoService.GetVehicleManufacture(),
                    vehicleStatuses = await vehicleInfoService.GetVehicleStatus(),
                    vehicleGroups = await vehicleInfoService.GetVehicleGroup(),
                    vehicleOwnerships = await vehicleInfoService.GetVehicleOwnership(),
                    resources = new List<VMSResource>(),
                    vehicleBodyTypes = await vehicleInfoService.GetVehicleBodyType(),
                    vehicleBodySubTypes = await vehicleInfoService.GetVehicleBodySubType(),
                    requisitionMasters = await requisitionService.GetrequisitionMaster(),
                    RequisitionMasterbyId = await requisitionService.GetrequisitionMasterById(Convert.ToInt32(id)),
                    RequisitionDetails = await requisitionService.GetrequisitionDetailsbyMasterId(Convert.ToInt32(id)),


                    aspNetUsersViewModels = await userInfoes.GetUserInfo(),

                };
                model.actionType = actionType;
                if (model.RequisitionMasterbyId == null) model.RequisitionMasterbyId = new VMSRequisitionMaster();


                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] RequisitionViewModel model)
        {
            try
            {
                // string userId = HttpContext.User.Identity.Name;
                string createby = string.Empty;
                DateTime createAt = DateTime.Now;
                if (model.requisitionId != 0)
                {
                    var data = await requisitionService.GetrequisitionMasterById(Convert.ToInt32(model.requisitionId));
                    createby = data.createdBy;
                    createAt = data.createdAt != null ? (DateTime)data.createdAt : DateTime.Now;

                }
                else
                {
                    createby = HttpContext.User.Identity.Name;
                }
                VMSRequisitionMaster masterdata = new VMSRequisitionMaster
                {
                    Id = Convert.ToInt32(model.requisitionId),
                    vehicleTypeId = Convert.ToInt32(model.vehicleTypeId),
                    reqNo = model.reqNo,
                    reqBy = model.reqBy,
                    reqDate = model.reqDate,
                    reqTime = model.reqTime,
                    vehicleCondition = model.vehicleCondition,
                    vehicleSeat = model.vehicleSeat,

                    createdBy = createby,
                    createdAt = createAt
                };
                int vid = await requisitionService.SaverequisitionMaster(masterdata);
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

                    string NewFileName = vid + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await model.formFileDoc.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = 0,
                        actionType = "Requisition",
                        actionTypeId = vid,
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

                    string NewFileName = vid + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await model.formFileImage.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = 0,
                        actionType = "Requisition",
                        actionTypeId = vid,
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



                if (model.routingPlace != null)
                {
                    await requisitionService.DeleterequisitionDetailsByMasterId(vid);
                    for (int i = 0; i < model.routingPlace.Count(); i++)
                    {

                        VMSRequisitionDetails detaildata = new VMSRequisitionDetails
                        {
                            Id = 0,
                            requisitionId = vid,
                            routingPlace = model.routingPlace[i],
                            sequenseNo = i + 1

                        };
                        int vfId = await requisitionService.SaverequisitionDetail(detaildata);
                        if (i + 1 == model.routingPlace.Count())
                        {
                            VMSRequisitionDetails detaildataa = new VMSRequisitionDetails
                            {
                                Id = 0,
                                requisitionId = vid,
                                routingPlace = model.routingPlace[0],
                                sequenseNo = i + 1 + 1

                            };
                            vfId = await requisitionService.SaverequisitionDetail(detaildata);
                        }

                    }
                }

                //return RedirectToAction(nameof(VehicleList));
                return RedirectToAction("RequisitionList", "Requisition", new { id = vid, Area = "Requisition" });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public async Task<IActionResult> RequisitionList()
        {
            RequisitionViewModel model = new RequisitionViewModel
            {
                requisitionMasters = await requisitionService.GetrequisitionMaster()
            };
            return View(model);
        }

        public async Task<IActionResult> RequisitionDetails(int id)
        {
            ViewBag.masterId = id;

            var photoInfo = await vehicleInfoService.GetDocumentAttachmentByActionId(id, "Requisition", "photo");
            if (photoInfo == null)
            {
                photoInfo = new List<DocumentPhotoAttachment>();
            }
            var lstdocument = await vehicleInfoService.GetDocumentAttachmentByActionId(id, "Requisition", "document");
            if (lstdocument == null)
            {
                lstdocument = new List<DocumentPhotoAttachment>();
            }
            var lstcommets = await requisitionService.GetCommentByRequisitionId(id);
            RequisitionViewModel model = new RequisitionViewModel
            {
                vehicleTypes = await vehicleInfoService.GetVehicleType(),
                RequisitionMasterbyId = await requisitionService.GetrequisitionMasterById(id),
                RequisitionDetails = await requisitionService.GetrequisitionDetailsbyMasterId(id),

                vehicleStatuses = await vehicleInfoService.GetVehicleStatus(),
                photoes = photoInfo,
                documents = lstdocument,
                requisitionComments = lstcommets
            };
            return View(model);
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
            return RedirectToAction("RequisitionDetails", "Requisition", new { id = actionId, Area = "Requisition" });
        }



        [HttpPost]
        public async Task<IActionResult> RequisitionComment([FromForm] RequisitionCommentsViewModel model)
        {
            try
            {
                string userName = User.Identity.Name;
                RequisitionComment data = new RequisitionComment
                {
                    Id = Convert.ToInt32(model.commentsId),
                    requisitionMasterId = model.requisitionMasterId,
                    titles = model.titles,
                    comments = model.comments,
                    createdBy = userName
                };
                int commentsId = await requisitionService.SaveRequisitionComment(data);

                return RedirectToAction("RequisitionDetails", "Requisition", new { id = model.requisitionMasterId, Area = "Requisition" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetCommentsByRequisitionId(int Id)
        {
            var records = await requisitionService.GetCommentByRequisitionId(Id);
            return Json(records);
        }

        [HttpPost]
        public async Task<IActionResult> RequisitionPhoto([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
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

                return RedirectToAction("RequisitionDetails", "Requisition", new { id = model.actionTypeId, Area = "Requisition" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> RequisitionDoc([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
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

                return RedirectToAction("RequisitionDetails", "Requisition", new { id = model.actionTypeId, Area = "Requisition" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> RequisitionDocument(string id, string subjects)
        {
            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    List<string> subject = subjects.Split(',').ToList<string>();
                    for (int i = 0; i < Request.Form.Files.Count; i++)
                    {
                        string userName = User.Identity.Name;
                        string documentType = "document";
                        var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Document";
                        IFormFile formFile = Request.Form.Files[i];
                        var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName;
                        string fileType = Path.GetExtension(fileName);
                        fileName = fileName.Contains("\\")
                            ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                            : fileName.Trim('"');

                        string NewFileName = id + "_" + documentType + "_" + fileName;
                        string fileLocation = "/Upload/Document/" + NewFileName;
                        var fullFilePath = Path.Combine(filesPath, NewFileName);

                        using (var stream = new FileStream(fullFilePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }

                        DocumentPhotoAttachment data = new DocumentPhotoAttachment
                        {
                            Id = Convert.ToInt32(0),
                            actionType = "Requisition",
                            actionTypeId = Convert.ToInt32(id),
                            subject = subject[i],
                            fileName = NewFileName,
                            filePath = fileLocation,
                            fileType = fileType,
                            documentType = documentType,
                            moduleId = 9,
                            createdBy = userName
                        };
                        await vehicleInfoService.SaveDocumentAttachment(data);
                    }
                }

                return RedirectToAction("RequisitionDetails", "Requisition", new { id = Convert.ToInt32(id), Area = "Requisition" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("/api/Requisition/deletefile/{fileId}/{fileName}")]
        public JsonResult DeleteDocumentInfo(int fileId, string fileName)
        {
            vehicleInfoService.DeleteDocumentAttachmentById(fileId);
            var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";

            var fullFilePath = Path.Combine(filesPath, fileName);
            if (System.IO.File.Exists(fullFilePath))
                System.IO.File.Delete(fullFilePath);


            return Json(true);
        }


        [HttpGet]
        public async Task<IActionResult> GetRequisitionDetailbyMasterId(int fuelId)
        {


            var ReqDetails = await requisitionService.GetrequisitionDetailsbyMasterId(fuelId);

            return Json(ReqDetails);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteComments(int Id, int actionId)
        {
            await requisitionService.DeleteRequisitionCommentById(Id);
            return RedirectToAction("Requisition", "Requisition", new { id = actionId, Area = "Requisition" });
        }
        public IActionResult VehicleInfo()
        {
            return View();
        }
    }
}