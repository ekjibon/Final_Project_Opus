using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.VMS.Models;
using OPUSERP.VMS.Data.Entity.VehicleInfo;
using OPUSERP.VMS.Services.VehicleInfo.Interfaces;
using OPUSERP.VMS.Services.VehicleService.Interfaces;

namespace OPUSERP.Areas.VMS.Controllers
{
    [Authorize]
    [Area("VMS")]
    public class VehicleRenewalReminderController : Controller
    {
        private readonly IVMSVehicleInfoService vehicleInfoService;
        private readonly IVehicleServiceHistoryService vehicleServiceHistoryService;
        public VehicleRenewalReminderController(IVMSVehicleInfoService vehicleInfoService, IVehicleServiceHistoryService vehicleServiceHistoryService)
        {
            this.vehicleInfoService = vehicleInfoService;
            this.vehicleServiceHistoryService = vehicleServiceHistoryService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var model = new VehicleRenewalReminderViewModel
                {
                    vehicleRenewalReminders = await vehicleServiceHistoryService.GetVehicleRenewalReminder(),

                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
            public async Task<IActionResult> VehicleRenewalReminderEntry(int Id)
            {
                try
                {
                    ViewBag.masterId = Id;

                    var model = new VehicleRenewalReminderViewModel
                    {
                        vehicleInformation = await vehicleInfoService.GetVehicleInformation(),
                        renewalTypes = await vehicleServiceHistoryService.GetRenewalType(),   
                    };
                    return View(model);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        // POST: VehicleRenewalReminder/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VehicleRenewalReminderEntry([FromForm] VehicleRenewalReminderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.vehicleInformation = await vehicleInfoService.GetVehicleInformation();
                model.renewalTypes = await vehicleServiceHistoryService.GetRenewalType();

                return View(model);
            }

            VehicleRenewalReminder entity = new VehicleRenewalReminder
            {
                Id = model.vehicleRenewalReminderId,
                vehicleInformationId = model.vehicleInformationId,
                renewalTypeId = model.renewalTypeId,
                issueDate = model.issueDate,
                expireDate = model.expireDate,
                reminderType = model.reminderType,
                reminderValue = model.reminderValue,
                emailNotifications = model.emailNotifications,
                emailId = model.emailId,
                createdBy = HttpContext.User.Identity.Name,
            };
            var masterId = await vehicleServiceHistoryService.SaveVehicleRenewalReminder(entity);
            

            //return RedirectToAction(nameof(Index)); 
            return RedirectToAction("VehicleRenewalReminderDetails", "VehicleRenewalReminder", new { Id = masterId, Area = "VehicleService" });
        }
        public async Task<IActionResult> VehicleRenewalReminderDetails(int Id)
        {
            try
            {
                ViewBag.masterId = Id;
                
                var model = new VehicleRenewalReminderViewModel
                {
                    vehicleRenewalReminders = await vehicleServiceHistoryService.GetVehicleRenewalReminderById(Id),
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetVehicleRenewalReminderById(int Id)
        {
            return Json(await vehicleServiceHistoryService.GetVehicleRenewalReminderById(Id));
        }

    }
}
