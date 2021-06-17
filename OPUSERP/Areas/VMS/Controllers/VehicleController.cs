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
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;
using OPUSERP.VMS.Services.MasterData.Interfaces;
using OPUSERP.VMS.Services.VehicleInfo.Interfaces;

namespace OPUSERP.Areas.VMS.Controllers
{
    [Authorize]
    [Area("Vehicles")]
    public class VehicleController : Controller
    {
        private readonly IVMSVehicleInfoService vehicleInfoService;
        //private readonly LangGenerate<VehicleLN> _lang;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserInfoes userInfoes;

        public VehicleController(IHostingEnvironment hostingEnvironment, IUserInfoes userInfoes, IVMSVehicleInfoService vehicleInfoService)
        {
            this.vehicleInfoService = vehicleInfoService;
            this.userInfoes = userInfoes;
            //_lang = new LangGenerate<VehicleLN>(hostingEnvironment.ContentRootPath);
            this._hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> GetFluidDetailByvehicleinfoId(int fuelId)
        {
            return Json(await vehicleInfoService.GetVehicleFluidByInfoId(fuelId));
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id,string actionType)
        {
            if (actionType == string.Empty || actionType == null)
            {
                actionType = "defaultOpen";
            }
            int viid = Convert.ToInt32(id);
            try
            {
                VehicleInformationViewModel model = new VehicleInformationViewModel
                {
                    vehicleTypes = await vehicleInfoService.GetVehicleType(),
                    vehicleModels=await vehicleInfoService.GetVehicleModel(),
                    vehicleManufactures=await vehicleInfoService.GetVehicleManufacture(),
                    vehicleStatuses=await vehicleInfoService.GetVehicleStatus(),
                    vehicleGroups=await vehicleInfoService.GetVehicleGroup(),
                    vehicleOwnerships=await vehicleInfoService.GetVehicleOwnership(),
                    resources=new List<VMSResource>(),
                    vehicleBodyTypes=await vehicleInfoService.GetVehicleBodyType(),
                    vehicleBodySubTypes=await vehicleInfoService.GetVehicleBodySubType(),
                    aspirations=await vehicleInfoService.GetAspiration(),
                    blockTypes=await vehicleInfoService.GetBlockType(),
                    camTypes=await vehicleInfoService.GetCamType(),
                    fuelInductions=await vehicleInfoService.GetFuelInduction(),
                    driveTypes=await vehicleInfoService.GetDriveType(),
                    brakeSystems=await vehicleInfoService.GetBrakeSystem(),
                    fuelTypes=await vehicleInfoService.GetFuelType(),
                    transmissionTypes=await vehicleInfoService.GetTransmissionType(),
                    vehicleInformation=await vehicleInfoService.GetVehicleInformation(),
                    VehicleInformationbyid=await vehicleInfoService.GetVehicleInformationById(viid),
                    vehicleSpecification = await vehicleInfoService.GetVehicleSpecificationByInfoId(viid),
                    vehicleWheelTire=await vehicleInfoService.GetVehicleWheelTireByInfoId(viid),
                    vehicleFluid=await vehicleInfoService.GetVehicleFluidByInfoId(viid),
                    vehicleEngineTransmission=await vehicleInfoService.GetVehicleEngineTransmissionByInfoId(viid),
                    aspNetUsersViewModels = await userInfoes.GetUserInfo(),

                };
                model.actionType = actionType;
                if (model.VehicleInformationbyid == null) model.VehicleInformationbyid = new VehicleInformation();
                if (model.vehicleSpecification == null) model.vehicleSpecification = new VehicleSpecification();
                if (model.vehicleWheelTire == null) model.vehicleWheelTire = new VehicleWheelTire();
                if (model.vehicleFluid == null) model.vehicleFluid = new List<VehicleFluid>();
                if (model.vehicleEngineTransmission == null) model.vehicleEngineTransmission = new VehicleEngineTransmission();
                
                return View(model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] VehicleInformationViewModel model,IFormFile formFile)
        {
            try
            {
                string userId = HttpContext.User.Identity.Name;
                VehicleInformation vehicle = new VehicleInformation
                {
                    Id = Convert.ToInt32(model.vehicleId),
                    vehicleTypeId = model.vehicleTypeId != 0 ? model.vehicleTypeId : null,
                    vehicleManufactureId = model.vehicleManufactureId != 0 ? model.vehicleManufactureId : null,
                    vehicleModelId = model.vehicleModelId != 0 ? model.vehicleModelId : null,
                    vehicleStatusId=model.vehicleStatusId != 0 ? model.vehicleStatusId : null,
                    vehicleGroupId=model.vehicleGroupId != 0 ? model.vehicleGroupId : null,
                    vehicleBodyTypeId=model.vehicleBodyTypeId!=0? model.vehicleBodyTypeId:null,
                    vehicleBodySubTypeId=model.vehicleBodySubTypeId != 0 ? model.vehicleBodySubTypeId : null,
                    vehicleOwnershipId=model.vehicleOwnershipId != 0 ? model.vehicleOwnershipId : null,
                    vehicleLinkedLId=model.vehicleLinkedLId,
                    operatorId=model.operatorId,
                    vehicleName=model.vehicleName,
                    vinNo=model.vinNo,
                    licensePlate=model.licensePlate,
                    vehicleYear=model.vehicleYear,
                    trim=model.trim,
                    registrationStateArea=model.registrationStateArea,
                    color=model.color,
                    msrPrice=model.msrPrice,
                    createdBy = userId
                };
                int vid = await vehicleInfoService.SaveVehicleInformation(vehicle);
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

                    string NewFileName = vid + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = 0,
                        actionType = "vehicleInfo",
                        actionTypeId =vid,
                        subject = "photo",
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description ="default photo description",
                        documentType = documentType,
                        moduleId = 9,
                        createdBy = userName
                    };
                    await vehicleInfoService.SaveDocumentAttachment(data);
                }
                VehicleSpecification vehicleSpecification = new VehicleSpecification
                {
                    Id = Convert.ToInt32(model.vehicleSpecificationId),
                    vehicleInformationId=vid,
                    width=model.width,
                    height=model.height,
                    length=model.length,
                    interiorVolume=model.interiorVolume,
                    passengerVolume=model.passengerVolume,
                    cargoVolume=model.cargoVolume,
                    groundClearance=model.groundClearance,
                    bedLength=model.bedLength,
                    curbWeight=model.curbWeight,
                    grossVehicleWeightRating=model.grossVehicleWeightRating,
                    towingCapacity=model.towingCapacity,
                    maxPayload=model.maxPayload,
                    epaCity=model.epaCity,
                    epaHighway=model.epaHighway,
                    epaCombined=model.epaCombined
                };
                
                if (model.width != null)
                {
                    int specid = await vehicleInfoService.SaveVehicleSpecification(vehicleSpecification);
                }
                else
                {

                }
                
                VehicleEngineTransmission vehicleEngineTransmission = new VehicleEngineTransmission
                {
                    Id = Convert.ToInt32(model.vehicleenginetransmissionId),
                    vehicleInformationId = vid,
                    engineSummary = model.engineSummary,
                    engineBrand=model.engineBrand,
                    aspirationId=model.aspirationId,
                    blockTypeId=model.blockTypeId,
                    bore=model.bore,
                    compression=model.compression,
                    cylinders=model.cylinders,
                    displacement=model.displacement,
                    fuelInductionId=model.fuelInductionId,
                    fuelQuality=model.fuelQuality,
                    maxHp=model.maxHp,
                    maxTorque=model.maxTorque,
                    redLineRpm=model.redLineRpm,
                    stroke=model.stroke,
                    valves=model.valves,
                    transmissionSummary=model.transmissionSummary,
                    transmissionBrand=model.transmissionBrand,
                    transmissionTypeId=model.transmissionTypeId,
                    transmissionGears=model.transmissionGears


                };
                if (model.engineSummary != null)
                {
                    int vetId = await vehicleInfoService.SaveVehicleEngineTransmission(vehicleEngineTransmission);
                }
                else
                {

                }
                
                VehicleWheelTire vehicleWheelTire = new VehicleWheelTire
                {
                    Id=Convert.ToInt32(model.vehicleWheelTireId),
                    vehicleInformationId=vid,
                    driveTypeId=model.driveTypeId,
                    brakeSystemId = model.brakeSystemId,
                    frontTireType=model.frontTireType,
                    frontTirePsi=model.frontTirePsi,
                    frontTrackWidth=model.frontTrackWidth,
                    rearAxle=model.rearAxle,
                    rearTirePsi=model.rearTirePsi,
                    rearTireType=model.rearTireType,
                    rearTrackWidth=model.rearTrackWidth,
                    rearWheelDiameter=model.rearWheelDiameter,
                    frontWheelDiameter=model.frontWheelDiameter,
                    wheelBase=model.wheelBase
                    
                };
                if (model.driveTypeId != null)
                {
                    int vwtId = await vehicleInfoService.SaveVehicleWheelTire(vehicleWheelTire);
                }
                else
                {

                }
                if (model.fuelTypeId != null)
                {
                    await vehicleInfoService.DeleteVehicleFluidByInfoId(vid);
                    for (int i = 0; i < model.fuelTypeId.Count(); i++)
                    {
                        VehicleFluid vehicleFluid = new VehicleFluid
                        {
                            Id = Convert.ToInt32(model.vehiclefluidId[i]),
                            vehicleInformationId = vid,
                            fuelTypeId = model.fuelTypeId[i],
                            fuelQuality = model.fuelQualityfluid[i],
                            fuelTankNo = model.fuelTankNo[i],
                            //fuelTank1Capacity=model.fuelTank1Capacity,
                            //fuelTank2Capacity=model.fuelTank2Capacity,
                            capacity = model.Capacity[i]
                        };
                        int vfId = await vehicleInfoService.SaveVehicleFluid(vehicleFluid);

                    }
                }

                //return RedirectToAction(nameof(VehicleList));
                return RedirectToAction("VehicleDetails", "Vehicle", new { id = vid, Area = "Vehicles" });
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> VehicleList()
        {
            try
            {
                var vehicleList = await vehicleInfoService.GetFullVehicleInformation();
                if (vehicleList == null)
                {
                    vehicleList = new List<VehicleInformation>();
                }
               

                VehicleInformationViewModel model = new VehicleInformationViewModel
                {
                    vehicleInformation = vehicleList
                };
                return View(model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> VehicleListWithStatus(int statusId,string status)
        {
            try
            {
                ViewBag.Status = status;
                VehicleInformationViewModel model = new VehicleInformationViewModel
                {
                    vehicleInformation = await vehicleInfoService.GetFullVehicleInformationWithStatus(statusId)
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IActionResult> VehicleDetails(int id)
        {
            var odomtr = await vehicleInfoService.GetOdometerByVehicleId(id);
            if (odomtr == null)
            {
                odomtr = new Odometer();
            }
            var photoInfo = await vehicleInfoService.GetDocumentAttachmentByActionId(id, "vehicleInfo","photo");
            if (photoInfo == null)
            {
                photoInfo = new List<DocumentPhotoAttachment>();
            }
            var lstdocument = await vehicleInfoService.GetDocumentAttachmentByActionId(id, "vehicleInfo", "document");
            if (lstdocument == null)
            {
                lstdocument = new List<DocumentPhotoAttachment>();
            }
            var lstcommets = await vehicleInfoService.GetCommentByVehicleId(id);
            VehicleInformationViewModel model = new VehicleInformationViewModel
            {
                VehicleInformationbyid = await vehicleInfoService.GetVehicleInformationDetailsById(id),
                vehicleSpecification = await vehicleInfoService.GetVehicleSpecificationByInfoId(id),
                vehicleWheelTire = await vehicleInfoService.GetVehicleWheelTireByInfoId(id),
                vehicleEngineTransmission = await vehicleInfoService.GetVehicleEngineTransmissionByInfoId(id),
                vehicleFluid = await vehicleInfoService.GetVehicleFluidByInfoId(id),
                odometer = odomtr,
                vehicleStatuses = await vehicleInfoService.GetVehicleStatus(),
                photoes = photoInfo,
                documents= lstdocument,
                vehicleComments=lstcommets
            };
            model.odometer.odometerValue = model.odometer == null ? "0" : model.odometer.odometerValue;
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
            return RedirectToAction("VehicleDetails", "Vehicle", new { id = actionId, Area = "Vehicles" });
        }



        [HttpPost]
        public async Task<JsonResult> VehicleComment([FromForm] VehicleCommentsViewModel model)
        {
            try
            {
                string userName = User.Identity.Name;
                VehicleComment data = new VehicleComment
                {
                    Id = Convert.ToInt32(model.commentsId),
                    vehicleInformationId = model.vehicleInformationId,
                    titles = model.titles,
                    comments = model.comments,
                    createdBy = userName
                };
               int commentsId= await vehicleInfoService.SaveVehicleComment(data);
                
                return Json(commentsId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetCommentsByVehicleId(int vehicleId)
        {
            var records = await vehicleInfoService.GetCommentByVehicleId(vehicleId);
            return Json(records);
        }

        [HttpPost]
        public async Task<IActionResult> VehiclePhoto([FromForm] DocumentAttachmentViewModel model,IFormFile formFile)
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
                
                return RedirectToAction("VehicleDetails", "Vehicle", new { id = model.actionTypeId, Area = "Vehicles" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> VehicleDoc([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
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

                return RedirectToAction("VehicleDetails", "Vehicle", new { id = model.actionTypeId, Area = "Vehicles" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> VehicleDocument(string id, string subjects)
        {
            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    List<string> subject= subjects.Split(',').ToList<string>();
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
                            actionType = "vehicleInfo",
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

                return RedirectToAction("VehicleDetails", "Vehicle", new { id = Convert.ToInt32(id), Area = "Vehicles" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("/api/vehicles/deletefile/{fileId}/{fileName}")]
        public JsonResult DeleteDocumentInfo(int fileId, string fileName)
        {
            vehicleInfoService.DeleteDocumentAttachmentById(fileId);
            var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
            
            var fullFilePath = Path.Combine(filesPath, fileName);
            if (System.IO.File.Exists(fullFilePath))
                System.IO.File.Delete(fullFilePath);

            
            return Json(true);
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

        [HttpGet]
        public async Task<JsonResult> GetPhotoByVehicleId(int vehicleId,string actionType,string documentType)
        {
            var records = await vehicleInfoService.GetDocumentAttachmentByActionId(vehicleId, actionType, documentType);
            return Json(records);
        }

        [Route("/api/vehicles/statuschange/{vehicleId}/{statusId}")]
        public JsonResult ChangeVehicleStatus(int vehicleId, int statusId)
        {
            vehicleInfoService.UpdateVehicleStatusVehicleById(vehicleId, statusId);
            return Json(true);
        }
        
        [Route("/api/vehicles/GetOdometerByVehicleId/{vid}")]
        [HttpGet]
        public async Task<IActionResult> GetOdometerByVehicleId(int vid)
        {
            return Json(await vehicleInfoService.GetOdometerByVehicleId(vid));
        }
        [Route("/api/vehicles/GetOdometerBySourceTypeId/{vid}/{sourceType}/{soruceId}")]
        [HttpGet]
        public async Task<IActionResult> GetOdometerBySourceTypeId(int vid, string sourceType, int soruceId)
        {
            return Json(await vehicleInfoService.GetOdometerBySourceTypeId( vid,  sourceType, soruceId));
        }

        [HttpGet]
        [Route("/api/vehicles/GetLastOdometer/{id}")]
        public async Task<JsonResult> GetLastOdometer(int id)
        {
            var odomtr = await vehicleInfoService.GetOdometerByVehicleId(id);
            return Json(odomtr);
        }
        
        public IActionResult VehicleInfo()
        {
            return View();
        }
    }
}