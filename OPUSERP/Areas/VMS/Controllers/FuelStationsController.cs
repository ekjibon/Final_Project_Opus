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
using OPUSERP.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.FuelStation;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;
using OPUSERP.VMS.Services.FuelStation.Interfaces;
using OPUSERP.VMS.Services.MasterData.Interfaces;
using OPUSERP.VMS.Services.Resources.Interfaces;
using OPUSERP.VMS.Services.VehicleInfo.Interfaces;

namespace OPUSERP.Areas.VMS.Controllers
{
    [Authorize]
    [Area("VMS")]
    public class FuelStationsController : Controller
    {
        private readonly IFuelStationInfoService fuelStationInfoService;
        //private readonly LangGenerate<VehicleLN> _lang;
        private readonly IVMSAddressService addressService;
        private readonly IVMSVehicleInfoService vehicleInfoService;
        public readonly IDesignationService designationService;
        public readonly IVMSResourceService resourceService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public FuelStationsController(IHostingEnvironment hostingEnvironment, IVMSResourceService resourceService, IVMSAddressService addressService, IDesignationService designationService, IFuelStationInfoService fuelStationInfoService, IVMSVehicleInfoService vehicleInfoService)
        {
            this.fuelStationInfoService = fuelStationInfoService;
            this.vehicleInfoService = vehicleInfoService;
            this.designationService = designationService;
            this.addressService = addressService;
            this.resourceService = resourceService;
            //_lang = new LangGenerate<VehicleLN>(hostingEnvironment.ContentRootPath);
            this._hostingEnvironment = hostingEnvironment;
        }


        public async Task<IActionResult> FuelStationDetails(int? id)
        {
            int viid = Convert.ToInt32(id);
            var orgAddress = await addressService.GetAddressBystationId(Convert.ToInt32(viid));

            if (orgAddress == null)
            {
                orgAddress = new VMSAddress();
                orgAddress.division = new Division();
                orgAddress.district = new District();
                orgAddress.thana = new Thana();
            }
            ViewBag.addcount = orgAddress;
            //var odomtr = await vehicleInfoService.GetOdometerByVehicleId(id);
            //if (odomtr == null)
            //{
            //    odomtr = new Odometer();
            //}
            var photoInfo = await vehicleInfoService.GetDocumentAttachmentByActionId(viid, "FuelStationInfo", "photo");
            if (photoInfo == null)
            {
                photoInfo = new List<DocumentPhotoAttachment>();
            }
            var docInfo = await vehicleInfoService.GetDocumentAttachmentByActionId(viid, "FuelStationInfo", "Document");
            if (docInfo == null)
            {
                docInfo = new List<DocumentPhotoAttachment>();
            }
            var CommentInfo = await fuelStationInfoService.GetCommentByFuelStationId(viid);
            if (CommentInfo == null)
            {
                CommentInfo = new List<FuelStationComment>();
            }
           
            FuelStationsInfoViewModel model = new FuelStationsInfoViewModel
            {
                fuelTypes = await vehicleInfoService.GetFuelType(),
                designations = await designationService.GetAllDesignation(),
                GetFuelStationInfosbyId = await fuelStationInfoService.GetFuelStationInfoById(viid),
                GetAddress = orgAddress,
                //VehicleInformationbyid = await vehicleInfoService.GetVehicleInformationDetailsById(id),
                //vehicleSpecification = await vehicleInfoService.GetVehicleSpecificationByInfoId(id),
                //vehicleWheelTire = await vehicleInfoService.GetVehicleWheelTireByInfoId(id),
                //vehicleEngineTransmission = await vehicleInfoService.GetVehicleEngineTransmissionByInfoId(id),
                //vehicleFluid = await vehicleInfoService.GetVehicleFluidByInfoId(id),
                //odometer = odomtr,
                //vehicleStatuses = await vehicleInfoService.GetVehicleStatus(),
                photoes = photoInfo,
                documents = docInfo,
               fuelStationComments=CommentInfo
            };
            if (model.GetFuelStationInfosbyId == null) model.GetFuelStationInfosbyId = new FuelStationInfo();
        
            return View(model);
           

        }
        [HttpGet]
        public async Task<JsonResult> GetCommentsByFuelStationInfoId(int vehicleId)
        {
            var records = await fuelStationInfoService.GetCommentByFuelStationId(vehicleId);
            return Json(records);
        }
        [HttpPost]
        public async Task<IActionResult> FuelStationComment([FromForm] FuelStationsCommentsViewModel model)
        {
         
            try
            {
                string userName = User.Identity.Name;
                FuelStationComment data = new FuelStationComment
                {
                    Id = Convert.ToInt32(model.commentsId),
                    fuelStationInfoId = model.fuelstationInfoId,
                    titles = model.titles,
                    comments = model.comments,
                    createdBy = userName
                };
                  await fuelStationInfoService.SaveFuelStationComment(data);

              
                return RedirectToAction("FuelStationDetails", "FuelStations", new { id = model.fuelstationInfoId, Area = "FuelStations"});
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> FuelStationsDoc([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
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

                return RedirectToAction("FuelStationDetails", "FuelStations", new { id = model.actionTypeId, Area = "FuelStations" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> FuelStationsPhoto([FromForm] DocumentAttachmentViewModel model,IFormFile formFile)
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
                        description=model.description,
                        documentType = documentType,
                        moduleId =9,
                        createdBy = userName
                    };
                    await vehicleInfoService.SaveDocumentAttachment(data);
                }
                
                return RedirectToAction("FuelStationDetails", "FuelStations", new { id = model.actionTypeId, Area = "FuelStations" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetPhotoByVehicleId(int vehicleId, string actionType, string documentType)
        {
            var records = await vehicleInfoService.GetDocumentAttachmentByActionId(vehicleId, actionType, documentType);
            return Json(records);
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id, string actionType)
        {
            if (actionType == string.Empty||actionType==null)
            {
                actionType = "defaultOpen";
            }
            int viid = Convert.ToInt32(id);
            try
            {
                var orgAddress = await addressService.GetAddressBystationId(Convert.ToInt32(viid));
              
                if (orgAddress == null)
                {
                    orgAddress = new VMSAddress();
                    orgAddress.division = new Division();
                    orgAddress.district = new District();
                    orgAddress.thana = new Thana();
                }
                ViewBag.addcount = orgAddress;
                FuelStationsInfoViewModel model = new FuelStationsInfoViewModel
                {
                    fuelTypes = await vehicleInfoService.GetFuelType(),
                    designations=await designationService.GetAllDesignation(),


                    GetFuelStationInfosbyId = await fuelStationInfoService.GetFuelStationInfoById(viid),
                    GetAddress=orgAddress
                    //vehicleSpecification = await vehicleInfoService.GetVehicleSpecificationByInfoId(viid),
                    //vehicleWheelTire=await vehicleInfoService.GetVehicleWheelTireByInfoId(viid),
                    //vehicleFluid=await vehicleInfoService.GetVehicleFluidByInfoId(viid),
                    //vehicleEngineTransmission=await vehicleInfoService.GetVehicleEngineTransmissionByInfoId(viid)

                };
                model.actionType = actionType;
                if (model.GetFuelStationInfosbyId == null) model.GetFuelStationInfosbyId = new FuelStationInfo();
                //if (model.vehicleWheelTire == null) model.vehicleWheelTire = new VehicleWheelTire();
                //if (model.vehicleFluid == null) model.vehicleFluid = new VehicleFluid();
                //if (model.vehicleEngineTransmission == null) model.vehicleEngineTransmission = new VehicleEngineTransmission();

                return View(model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] FuelStationsInfoViewModel model,IFormFile formFile)
        {
            var Org = model.orgDivision;
            var Contactcount = model.contactall;
            var fuelcount = model.fuelType;
            //return Json(model);
            try
            {
                string userId = HttpContext.User.Identity.Name;
                FuelStationInfo fuelStationInfo  = new FuelStationInfo
                {
                    Id = Convert.ToInt32(model.fuelStationId),
                    fuelStationName=model.fuelStationName,
                    ownerName=model.ownerName,
                    managerName=model.managerName,
                    tradelicenseNo=model.tradelicenseNo,



                };
                int vid= await fuelStationInfoService.SaveFuelStationInfo(fuelStationInfo);
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
                        actionType = "FuelStationInfo",
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
                if (Contactcount != null)
                {
                    for (int i = 0; i < model.contactall.Count(); i++)
                    {
                        VMSResource resource = new VMSResource
                        {
                            Id = Convert.ToInt32(model.resouceidall[i]),
                            nameEnglish = model.contactall[i],
                            phone = model.phoneall[i],
                            mobile = model.mobileall[i],
                            designationID = Convert.ToInt32(model.designationIdall[i]),
                            departmentID = 1

                        };

                        int rid = await resourceService.SaveResource(resource);
                        VMSContact contact = new VMSContact
                        {
                            Id = Convert.ToInt32(model.idall[i]),
                            resourceID = rid,
                            fuelStationInfoId = vid



                        };
                        int specid = await fuelStationInfoService.Savecontact(contact);

                    }
                }
             if(Org != null)
                { 
                    VMSAddress address  = new VMSAddress
                    {
                   
                        Id = Convert.ToInt32(model.orgAddressID),
                        fuelStationInfoId = vid,
                        countryId = 5,
                        divisionId = Int32.Parse(model.orgDivision),
                        districtId = Int32.Parse(model.orgDistrict),
                        thanaId = Int32.Parse(model.orgUpazila),
                        union = model.orgUnion,
                        postOffice = model.orgPostOffice,
                        postCode = model.orgPostCode,
                        blockSector = model.orgBlockSector,
                        houseVillage = model.orgHouseVillage,
                  
                        type = "organization"


                    };
                    int vetId = await addressService.SaveAddress(address);
                }
             if(fuelcount!=null)
                {
                    await fuelStationInfoService.DeleteStationFuelInfoBystationId(vid);


                for (int i = 0; i < model.fuelType.Count(); i++)
                {
                    StationFuelInfo stationFuelInfo = new StationFuelInfo
                    {
                        Id = 0,
                        fuelStationInfoId = vid,
                        fuelTypeId = model.fuelType[i],
                        noOfNozlzle = model.fuelQty[i],
                       
                    };
                    int vwtId = await fuelStationInfoService.SavestationFuelInfo(stationFuelInfo);
                }
                }


                //return RedirectToAction(nameof(FuelStationsList));
                return RedirectToAction("FuelStationDetails", "FuelStations", new { id = vid, Area = "FuelStations" });
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        
        [HttpGet]
        public async Task<IActionResult> GetStationFuelInfobystationId(int Id)
        {


            var contactlist = await fuelStationInfoService.GetStationFuelInfobystationId(Id);

            return Json(contactlist);
        }
        [HttpGet]
        public async Task<IActionResult> Getcontactbystationid(int Id)
        {


            var contactlist = await fuelStationInfoService.Getcontactbystationid(Id);

            return Json(contactlist);
        }
        [HttpGet]
        public async Task<IActionResult> FuelStationsList()
        {
            FuelStationsInfoViewModel model = new FuelStationsInfoViewModel
            {
                GetFuelStationInfos=await fuelStationInfoService.GetFuelStationInfo()
                //vehicleInformation = await vehicleInfoService.GetFullVehicleInformation()
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
            return RedirectToAction("FuelStationDetails", "FuelStations", new { id = actionId, Area = "FuelStations" });
        }
        [HttpGet]
        public async Task<IActionResult> DeleteComments(int Id,int actionId)
        {
            await fuelStationInfoService.DeleteFuelStationCommentById(Id);
            return RedirectToAction("FuelStationDetails", "FuelStations", new { id = actionId, Area = "FuelStations" });
        }
        #region API Section
        //[Route("global/api/divisions/{id}")]
        //[HttpGet]
        //public async Task<IActionResult> Divisions(int Id)
        //{
        //    return Json(await addressService.GetDivisionsByCountryId(Id));
        //}
        //[Route("global/api/districts/{id}")]
        //[HttpGet]
        //public async Task<IActionResult> Districts(int Id)
        //{
        //    return Json(await addressService.GetDistrictsByDivisonId(Id));
        //}
        [Route("global/api/districtsI/")]
        [HttpGet]
        public async Task<IActionResult> GetAllDistrict()
        {
            return Json(await addressService.GetAllDistrict());
        }
        //[Route("global/api/thanas/{id}")]
        //[HttpGet]
        //public async Task<IActionResult> Thanas(int Id)
        //{
        //    return Json(await addressService.GetThanasByDistrictId(Id));
        //}
        #endregion
    }
}