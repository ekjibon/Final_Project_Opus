using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.VMS.Models;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.VMS.Data.Entity.ServiceVehicle;
using OPUSERP.VMS.Data.Entity.VehicleInfo;
using OPUSERP.VMS.Services.CarManagement.Interfaces;
using OPUSERP.VMS.Services.VehicleInfo.Interfaces;
using OPUSERP.VMS.Services.VehicleService.Interfaces;

namespace OPUSERP.Areas.VMS.Controllers
{
    [Authorize]
    [Area("VMS")]
    public class VehicleServiceReminderController : Controller
    {
        private readonly IVMSVehicleInfoService vehicleInfoService;
        private readonly IVehicleServiceHistoryService vehicleServiceHistoryService;
        private readonly ICarInfo carInfo;
        public VehicleServiceReminderController(IHostingEnvironment hostingEnvironment, ICarInfo carInfo, IVMSVehicleInfoService vehicleInfoService, IVehicleServiceHistoryService vehicleServiceHistoryService)
        {
            this.vehicleInfoService = vehicleInfoService;
            this.vehicleServiceHistoryService = vehicleServiceHistoryService;
            this.carInfo = carInfo;
        }

        

        public async Task<IActionResult> ReminderList()
        {
            VehicleServiceReminderViewModel model = new VehicleServiceReminderViewModel
            {
                vehicleServiceReminders = await vehicleServiceHistoryService.GetVehicleServiceReminder(),                
            };
            return View(model);
        }

        public async Task<IActionResult> ReminderNew(int id)
        {
            ViewBag.vehicleId = id;
            var odomtr = await vehicleInfoService.GetOdometerByVehicleId(id);
            if (odomtr == null)
            {
                odomtr = new Odometer();
            }
            var photoInfo = await vehicleInfoService.GetDocumentAttachmentByActionId(id, "vehicleInfo", "photo");
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

            VehicleServiceReminderViewModel model = new VehicleServiceReminderViewModel
            {
                VehicleInformationbyid = await vehicleInfoService.GetVehicleInformationDetailsById(id),
                odometer = odomtr,
                vehicleStatuses = await vehicleInfoService.GetVehicleStatus(),
                photoes = photoInfo,
                documents = lstdocument,
                vehicleComments = lstcommets,
                serviceTasks= await vehicleServiceHistoryService.GetServiceTask(),
                spareParts=await carInfo.GetSpareParts(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReminderNew([FromForm] VehicleServiceReminderViewModel model)
        {
            var odomtr = await vehicleInfoService.GetOdometerByVehicleId(Convert.ToInt32(model.vehicleInformationId));
            if (odomtr == null)
            {
                odomtr = new Odometer();
            }
            var photoInfo = await vehicleInfoService.GetDocumentAttachmentByActionId(Convert.ToInt32(model.vehicleInformationId), "vehicleInfo", "photo");
            if (photoInfo == null)
            {
                photoInfo = new List<DocumentPhotoAttachment>();
            }
            var lstdocument = await vehicleInfoService.GetDocumentAttachmentByActionId(Convert.ToInt32(model.vehicleInformationId), "vehicleInfo", "document");
            if (lstdocument == null)
            {
                lstdocument = new List<DocumentPhotoAttachment>();
            }
            var lstcommets = await vehicleInfoService.GetCommentByVehicleId(Convert.ToInt32(model.vehicleInformationId));
            if (!ModelState.IsValid)
            {
                model.VehicleInformationbyid = await vehicleInfoService.GetVehicleInformationDetailsById(Convert.ToInt32(model.vehicleInformationId));
                model.odometer = odomtr;
                model.vehicleStatuses = await vehicleInfoService.GetVehicleStatus();
                model.photoes = photoInfo;
                model.documents = lstdocument;
                model.vehicleComments = lstcommets;
                model.serviceTasks = await vehicleServiceHistoryService.GetServiceTask();

                return View(model);
            }

            VehicleServiceReminder entity = new VehicleServiceReminder
            {
                Id = model.vehicleServiceReminderId,
                vehicleInformationId = model.vehicleInformationId,
                sparePartsId = model.serviceTaskId,

                meterInterval = model.meterInterval,
                timeInterval = model.timeInterval,
                timeIntervalType = model.timeIntervalType,
                dueMeterThreshold = model.dueMeterThreshold,
                dueTimeThreshold = model.dueTimeThreshold,
                dueTimeThresholdType = model.dueTimeThresholdType,                
                createdBy = HttpContext.User.Identity.Name,
            };
            var masterId = await vehicleServiceHistoryService.SaveVehicleServiceReminder(entity);

            //if (model.vehicleServiceHistoryId > 0)
            //{
            //    await vehicleServiceHistoryService.DeleteVehicleLineItemByserviceId(masterId);
            //}


            for (int i = 0; i < 5; i++)
            {
                ReminderSchedule detailEntity = new ReminderSchedule
                {
                    Id = 0,
                    vehicleServiceReminderId = masterId,
                    scheduleOdometer = model.scheduleOdometer[i],
                    scheduleDate =Convert.ToDateTime(model.scheduleDate[i])
                };
                await vehicleServiceHistoryService.SaveReminderSchedule(detailEntity);
            }



            return RedirectToAction(nameof(ReminderList));
            //return RedirectToAction("VehicleServiceDetails", "VehicleService", new { Id = masterId, vid = model.vehicleInformationId, Area = "VehicleService" });

        }


        public async Task<IActionResult> ReminderDetails(int id,int vehicleId)
        {
            ViewBag.vehicleId = vehicleId;
            var odomtr = await vehicleInfoService.GetOdometerByVehicleId(vehicleId);
            if (odomtr == null)
            {
                odomtr = new Odometer();
            }
            var photoInfo = await vehicleInfoService.GetDocumentAttachmentByActionId(vehicleId, "vehicleInfo", "photo");
            if (photoInfo == null)
            {
                photoInfo = new List<DocumentPhotoAttachment>();
            }
            var lstdocument = await vehicleInfoService.GetDocumentAttachmentByActionId(vehicleId, "vehicleInfo", "document");
            if (lstdocument == null)
            {
                lstdocument = new List<DocumentPhotoAttachment>();
            }
            var lstcommets = await vehicleInfoService.GetCommentByVehicleId(vehicleId);

            VehicleServiceReminderViewModel model = new VehicleServiceReminderViewModel
            {
                vehicleServiceReminder=await vehicleServiceHistoryService.GetVehicleServiceReminderById(id),
                VehicleInformationbyid = await vehicleInfoService.GetVehicleInformationDetailsById(vehicleId),
                odometer = odomtr,
                vehicleStatuses = await vehicleInfoService.GetVehicleStatus(),
                photoes = photoInfo,
                documents = lstdocument,
                vehicleComments = lstcommets,
                serviceTasks = await vehicleServiceHistoryService.GetServiceTask(),
                reminderSchedules=await vehicleServiceHistoryService.GetReminderScheduleByServiceId(id),
                spareParts=await carInfo.GetSpareParts()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReminderDetails([FromForm] VehicleServiceReminderViewModel model)
        {
            var odomtr = await vehicleInfoService.GetOdometerByVehicleId(Convert.ToInt32(model.vehicleInformationId));
            if (odomtr == null)
            {
                odomtr = new Odometer();
            }
            var photoInfo = await vehicleInfoService.GetDocumentAttachmentByActionId(Convert.ToInt32(model.vehicleInformationId), "vehicleInfo", "photo");
            if (photoInfo == null)
            {
                photoInfo = new List<DocumentPhotoAttachment>();
            }
            var lstdocument = await vehicleInfoService.GetDocumentAttachmentByActionId(Convert.ToInt32(model.vehicleInformationId), "vehicleInfo", "document");
            if (lstdocument == null)
            {
                lstdocument = new List<DocumentPhotoAttachment>();
            }
            var lstcommets = await vehicleInfoService.GetCommentByVehicleId(Convert.ToInt32(model.vehicleInformationId));
            if (!ModelState.IsValid)
            {

                model.vehicleServiceReminder = await vehicleServiceHistoryService.GetVehicleServiceReminderById(model.vehicleServiceReminderId);
                model.VehicleInformationbyid = await vehicleInfoService.GetVehicleInformationDetailsById(Convert.ToInt32(model.vehicleInformationId));
                model.odometer = odomtr;
                model.vehicleStatuses = await vehicleInfoService.GetVehicleStatus();
                model.photoes = photoInfo;
                model.documents = lstdocument;
                model.vehicleComments = lstcommets;
                model.serviceTasks = await vehicleServiceHistoryService.GetServiceTask();
                model.reminderSchedules = await vehicleServiceHistoryService.GetReminderScheduleByServiceId(model.vehicleServiceReminderId);

                return View(model);
            }

            VehicleServiceReminder entity = new VehicleServiceReminder
            {
                Id = model.vehicleServiceReminderId,
                vehicleInformationId = model.vehicleInformationId,
                sparePartsId = model.serviceTaskId,

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
                await vehicleServiceHistoryService.DeleteReminderScheduleByServiceId(masterId);
            //}


            for (int i = 0; i < 5; i++)
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
            return RedirectToAction(nameof(ReminderList));

        }

    }
}