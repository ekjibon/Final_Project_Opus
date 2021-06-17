using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.VMS.Models;
using OPUSERP.VMS.Data.Entity.FuelInfo;
using OPUSERP.VMS.Data.Entity.VehicleInfo;
using OPUSERP.VMS.Services.FuelInfo.Interfaces;
using OPUSERP.VMS.Services.FuelStation.Interfaces;
using OPUSERP.VMS.Services.VehicleInfo.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.Data.Entity.AttachmentComment;

namespace OPUSERP.Areas.VMS.Controllers
{
    [Authorize]
    [Area("VMS")]
    public class FuelInfoController : Controller
    {
        //private readonly LangGenerate<FuelInfoLN> _lang;
        private readonly IFuelInfoService fuelInfoService;
        private readonly IVMSVehicleInfoService vehicleInfoService;
        private readonly IFuelStationInfoService fuelStationInfoService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string rootPath;
        private readonly MyPDF myPDF;
       
        public string FileName;
        public FuelInfoController(IHostingEnvironment hostingEnvironment, IFuelInfoService fuelInfoService, IVMSVehicleInfoService vehicleInfoService, IFuelStationInfoService fuelStationInfoService, IConverter converter)
        {
            this.fuelInfoService = fuelInfoService;
            this.vehicleInfoService = vehicleInfoService;
            this.fuelStationInfoService = fuelStationInfoService;
            //_lang = new LangGenerate<FuelInfoLN>(hostingEnvironment.ContentRootPath);
            this._hostingEnvironment = hostingEnvironment;
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var model = new FuelInfoViewModel
                {
                    fuelInformation = await fuelInfoService.GetFuelInformation(),
                    //flang = _lang.PerseLang("Fuel/FuelInfoEN.json", "Fuel/FuelInfoBN.json", Request.Cookies["lang"])
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<IActionResult> FuelEntry(int Id)
        {
            try
            {
                ViewBag.masterId = Id;
          
                var model = new FuelInfoViewModel
                {
                    fuelInformation = await fuelInfoService.GetFuelInformation(),
                    vehicleInformation = await vehicleInfoService.GetVehicleInformation(),
                    fuelStations = await fuelStationInfoService.GetFuelStationInfo(),
            
                   
                    fuelTypes = await vehicleInfoService.GetFuelType(),
                    //flang = _lang.PerseLang("Fuel/FuelInfoEN.json", "Fuel/FuelInfoBN.json", Request.Cookies["lang"])
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [AllowAnonymous]
        public IActionResult FuelReportPdfAction(string vehicleId, string fromDate, string toDate, string fuelStationId, string fuelTypeId)
        {

          


           
                if (vehicleId == null) vehicleId = "0";
                if (fromDate == null) fromDate = "";
                if (toDate == null) toDate = "";
                if (fuelStationId == null) fuelStationId = "0";
                if (fuelTypeId == null) fuelTypeId = "0";

                string scheme = Request.Scheme;
                var host = Request.Host;
              
                string url = scheme + "://" + host + "/Fuel/FuelInfo/FuelReportPdf?vehicleId=" + vehicleId + "&fromDate=" + fromDate + "&toDate=" + toDate + "&fuelStationId=" + fuelStationId + "&fuelTypeId=" + fuelTypeId;

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
        [AllowAnonymous]
        public async Task<IActionResult> FuelReportPdf(string vehicleId, string fromDate, string toDate, string fuelStationId, string fuelTypeId)
        {
            try
            {
                if (vehicleId == null) vehicleId = "0";
                if (fromDate == null) fromDate = "";
                if (toDate == null) toDate = "";
                if (fuelStationId == null) fuelStationId = "0";
                if (fuelTypeId == null) fuelTypeId = "0";
                var data = await fuelInfoService.GetFuelDetails(string.Empty);
                if (Convert.ToInt32(vehicleId) > 0)
                {
                    data = data.Where(x => x.vehicleId == Convert.ToInt32(vehicleId));
                    var vehicle = await vehicleInfoService.GetVehicleInformationById(Convert.ToInt32(vehicleId));
                    ViewBag.VehicleName = vehicle.vehicleName;
                }
                else
                {
                    ViewBag.VehicleName ="All";
                }
                if (Convert.ToInt32(fuelStationId) > 0)
                {
                    data = data.Where(x => x.stationId == Convert.ToInt32(fuelStationId));
                    var vehicle = await fuelStationInfoService.GetFuelStationInfoById(Convert.ToInt32(fuelStationId));
                    ViewBag.stationName = vehicle.fuelStationName;
                }
                else
                {
                    ViewBag.stationName = "All";
                }
                if (Convert.ToInt32(fuelTypeId) > 0)
                {
                    data = data.Where(x => x.fuelTypeId == Convert.ToInt32(fuelTypeId));
                    var vehicle = await vehicleInfoService.GetFuelTypeById(Convert.ToInt32(fuelTypeId));
                    ViewBag.fuelTypeName = vehicle.fuelTypeName;
                }
                else
                {
                    ViewBag.fuelTypeName = "All";
                }
                if (toDate != "")
                {
                    data = data.Where(x => x.date <= Convert.ToDateTime(toDate));
                    ViewBag.toDate = Convert.ToDateTime(toDate).ToString("dd-MM-yyyy");
                }
                else
                {
                    ViewBag.toDate = "All date";
                }
                if (fromDate != "")
                {
                    data = data.Where(x => x.date >= Convert.ToDateTime(fromDate));
                    ViewBag.fromDate = Convert.ToDateTime(fromDate).ToString("dd-MM-yyyy");
                }
                else
                {
                    ViewBag.fromDate = "All date";
                }

                var model = new FuelInfoViewModel
                {
                    //fuelStations = await fuelStationInfoService.GetFuelStationInfo(),
                    //vehicleInformation = await vehicleInfoService.GetVehicleInformation(),
                    fuelInfoReportViewModels =data,
                    //fuelTypes = await vehicleInfoService.GetFuelType()

                };
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<IActionResult> FuelReport(string vehicleId,string fromDate,string toDate,string fuelStationId,string fuelTypeId,string[] ColumnHead)
        {

            try
            {
                ViewBag.vehicleId = vehicleId;
                ViewBag.fromDate = fromDate;
                ViewBag.toDate = toDate;
                ViewBag.fuelStationId = fuelStationId;
                ViewBag.fuelTypeId = fuelTypeId;

                if (vehicleId == null) vehicleId = "0";
                if (fromDate == null) fromDate = "";
                if (toDate == null) toDate = "";
                if (fuelStationId == null) fuelStationId = "0";
                if (fuelTypeId == null) fuelTypeId = "0";
                var data = await fuelInfoService.GetFuelDetails(string.Empty);
                if (Convert.ToInt32(vehicleId) > 0)
                {
                    data = data.Where(x => x.vehicleId == Convert.ToInt32(vehicleId));
                }
                if (Convert.ToInt32(fuelStationId) > 0)
                {
                    data = data.Where(x => x.stationId == Convert.ToInt32(fuelStationId));
                }
                if (Convert.ToInt32(fuelTypeId) > 0)
                {
                    data = data.Where(x => x.fuelTypeId == Convert.ToInt32(fuelTypeId));
                }
                if (toDate !="")
                {
                    data = data.Where(x => x.date<= Convert.ToDateTime(toDate));
                }
                if (fromDate != "")
                {
                    data = data.Where(x => x.date >= Convert.ToDateTime(fromDate));
                }

                var model = new FuelInfoViewModel
                {
                    fuelStations = await fuelStationInfoService.GetFuelStationInfo(),
                    vehicleInformation = await vehicleInfoService.GetVehicleInformation(),
                    fuelInfoReportViewModels = data,
                    fuelTypes = await vehicleInfoService.GetFuelType()

                };
                List<string> Name= new List<string>();
              

                for(int i=0;i<ColumnHead.Length;i++)
                {
                    string ddd = ColumnHead[i].ToString();
                    string[] Words = ddd.Split(",");
                    foreach (var word in Words)
                    {

                        Name.Add(word);
                    }
                }


                ViewBag.properties = Name;
                ViewBag.NameCount = Name.Count();
                //if (Name.Count() <= 0)
                //{
                //    ViewBag.properties = typeof(FuelInfoReportViewModel).GetProperties().Select(p => p).Where(x=>x.Name!= "stationId" &&x.Name!= "fuelTypeId"&&x.Name!= "vehicleId");
                //}
                // typeof(Columns).GetProperties().Select(p => p);
                ViewBag.Columns = typeof(FuelInfoReportViewModel).GetProperties().Select(p => p);
                //if (Name.Length == 0)
                //{
                //    ViewBag.properties = ViewBag.Columns;
                //}
                //  ViewBag.properties = typeof(FuelInfoReportViewModel).GetProperties().Where(p => p.GetCustomAttributes(typeof(RequiredAttribute), false).Length == 1).Select(p => p);
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        // POST: FuelEntry/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> FuelEntry([FromForm] FuelInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {

                model.fuelInformation = await fuelInfoService.GetFuelInformation();
                model.vehicleInformation = await vehicleInfoService.GetVehicleInformation();
                model.fuelStations = await fuelStationInfoService.GetFuelStationInfo();
                model.fuelTypes = await vehicleInfoService.GetFuelType();
                //model.flang = _lang.PerseLang("Fuel/FuelInfoEN.json", "Fuel/FuelInfoBN.json", Request.Cookies["lang"]);

                //return View(model);
            }

            FuelInformation entity = new FuelInformation
            {
                Id = model.fuelInformationId,
                vehicleInformationId = model.vehicleInformationId,
                fuelStationInfoId = model.fuelStationId,
                odometer = model.odometer,
                fuelTakenDate = model.fuelTakenDate,
                referenceNo = model.referenceNo,
                createdBy = HttpContext.User.Identity.Name,
            };
            var masterId = await fuelInfoService.SaveFuelInformation(entity);
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
                    actionType = "FuelInfo",
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
                    actionType = "FuelInfo",
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
            Odometer Odometerentity = new Odometer
            {
                Id = 0,
                vehicleInformationId = model.vehicleInformationId,
                odometerValue = model.odometer,
                sortOrder = 1,
                sourceType = "Fuel",
                sourceTypeId = masterId,
                readingDate = DateTime.Now,
                createdBy = HttpContext.User.Identity.Name,
            };
            await vehicleInfoService.SaveOdometer(Odometerentity);

            if (model.fuelInformationId > 0)
            {
                await fuelInfoService.DeleteFuelDetailByfuelId(masterId);
            }

            if (model.fuelType != null)
            { 
                for (int i = 0; i < model.fuelType.Length; i++)
                {
               
                
                    FuelDetail detailEntity = new FuelDetail
                    {
                        Id = 0,
                        fuelInformationId = Convert.ToInt32(masterId),
                        fuelTypeId = model.fuelType[i],
                        quantity = model.fuelQty[i],
                        rate = model.fuelRate[i],
                    };
                    await fuelInfoService.SaveFuelDetail(detailEntity);
                }
            }
            if (model.comment!=null)
            { 
            FuelComment entityComment = new FuelComment
            {
                Id = model.fuelCommentId,
                fuelInformationId = Convert.ToInt32(masterId),
                comment = model.comment,
                createdBy = HttpContext.User.Identity.Name,
            };
                await fuelInfoService.SaveFuelComment(entityComment);
            }
            //return RedirectToAction(nameof(Index)); 
            return RedirectToAction("FuelEntryDetails", "FuelInfo", new { Id = masterId, Area = "Fuel" });
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
            return RedirectToAction("FuelEntryDetails", "FuelInfo", new { id = actionId, Area = "Fuel" });
        }
        [HttpGet]
        public async Task<IActionResult> GetFuelInformationByfuelId(int fuelId)
        {
            return Json(await fuelInfoService.GetFuelInformationByfuelId(fuelId));
        }
        [HttpGet]
        public async Task<IActionResult> GetFuelDetailByfuelId(int fuelId)
        {
            return Json(await fuelInfoService.GetFuelDetailByfuelId(fuelId));
        }
        [HttpGet]
        public async Task<JsonResult> GetFuelCommentByfuelId(int fuelId)
        {
            var records = await fuelInfoService.GetFuelCommentByfuelId(fuelId);
            return Json(records);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteComments(int Id,int actionId)
        {
            await fuelInfoService.DeleteFuelCommentById(Id);
            return RedirectToAction("FuelEntryDetails", "FuelInfo", new { id = actionId, Area = "Fuel" });
        }
        [HttpPost]
        public async Task<IActionResult> FuelDoc([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
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

                return RedirectToAction("FuelEntryDetails", "FuelInfo", new { id = model.actionTypeId, Area = "Fuel" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> FuelPhoto([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
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

                return RedirectToAction("FuelEntryDetails", "FuelInfo", new { id = model.actionTypeId, Area = "Fuel" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IActionResult> FuelEntryDetails(int Id)
        {
            try
            {
                ViewBag.masterId = Id;
                var CommentInfo = await fuelInfoService.GetFuelCommentByfuelId(Id);
                if (CommentInfo == null)
                {
                    CommentInfo = new List<FuelComment>();
                }
                var photoInfo = await vehicleInfoService.GetDocumentAttachmentByActionId(Id, "FuelInfo", "photo");
                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                var docInfo = await vehicleInfoService.GetDocumentAttachmentByActionId(Id, "FuelInfo", "Document");
                if (docInfo == null)
                {
                    docInfo = new List<DocumentPhotoAttachment>();
                }
                var data = await fuelInfoService.GetFuelInformationById(Id);
                var model = new FuelInfoViewModel
                {
                    fuelInformation = await fuelInfoService.GetFuelInformationByfuelId(Id),
                    fuelDetails = await fuelInfoService.GetFuelDetailByfuelId(Id),
                    vehicleInformationbyId = await vehicleInfoService.GetVehicleInformationById(Convert.ToInt32(data.vehicleInformationId)),
                    fuelComments =CommentInfo,
                    photoes = photoInfo,
                    documents = docInfo,

                    //fuelStations = await fuelInfoService.GetFuelStation(),
                    //fuelTypes = await vehicleInfoService.GetFuelType(),
                    //flang = _lang.PerseLang("Fuel/FuelInfoEN.json", "Fuel/FuelInfoBN.json", Request.Cookies["lang"])
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        public async Task<ActionResult> FuelComment([FromForm] FuelCommentsViewModel model)
        {
            try
            {
                FuelComment entityComment = new FuelComment
                {
                    Id = Convert.ToInt32(model.commentsId),
                    fuelInformationId = model.fuelInformationId,
                    title = model.titles,
                    comment = model.comments,
                    createdBy = HttpContext.User.Identity.Name,
                };
                int commentsId = await fuelInfoService.SaveFuelComment(entityComment);
                return RedirectToAction("FuelEntryDetails", "FuelInfo", new { id = model.fuelInformationId, Area = "Fuel" });
                //  return Json(commentsId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}