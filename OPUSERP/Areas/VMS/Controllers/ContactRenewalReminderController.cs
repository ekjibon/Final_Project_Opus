using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.VMS.Models;
using OPUSERP.VMS.Data.Entity.VehicleInfo;
using OPUSERP.VMS.Services.VehicleService.Interfaces;

namespace OPUSERP.Areas.VMS.Controllers
{
    [Authorize]
    [Area("VMS")]
    public class ContactRenewalReminderController : Controller
    {
       
        private readonly IVehicleServiceHistoryService vehicleServiceHistoryService;
        public ContactRenewalReminderController( IVehicleServiceHistoryService vehicleServiceHistoryService)
        {
            this.vehicleServiceHistoryService = vehicleServiceHistoryService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var model = new ContactRenewalReminderViewModel
                {
                    contactRenewalReminders = await vehicleServiceHistoryService.GetContactRenewalReminder(),

                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IActionResult> ContactRenewalReminderEntry(int Id)
        {
            try
            {
                ViewBag.masterId = Id;

                var model = new ContactRenewalReminderViewModel
                {
                    suppliers = await vehicleServiceHistoryService.GetSupplier(),
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
        public async Task<IActionResult> ContactRenewalReminderEntry([FromForm] ContactRenewalReminderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.suppliers = await vehicleServiceHistoryService.GetSupplier();
                model.renewalTypes = await vehicleServiceHistoryService.GetRenewalType();

                return View(model);
            }

            ContactRenewalReminder entity = new ContactRenewalReminder
            {
                Id = model.contactRenewalReminderId,
                supplierId = model.supplierId,
                renewalTypeId = model.renewalTypeId,
                issueDate = model.issueDate,
                expireDate = model.expireDate,
                reminderType = model.reminderType,
                reminderValue = model.reminderValue,
                emailNotifications = model.emailNotifications,
                emailId = model.emailId,
                createdBy = HttpContext.User.Identity.Name,
            };
            var masterId = await vehicleServiceHistoryService.SaveContactRenewalReminder(entity);


            //return RedirectToAction(nameof(Index)); 
            return RedirectToAction("ContactRenewalReminderDetails", "ContactRenewalReminder", new { Id = masterId, Area = "VehicleService" });
        }
        public async Task<IActionResult> ContactRenewalReminderDetails(int Id)
        {
            try
            {
                ViewBag.masterId = Id;

                var model = new ContactRenewalReminderViewModel
                {
                    contactRenewalReminders = await vehicleServiceHistoryService.GetContactRenewalReminderById(Id),
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetContactRenewalReminderById(int Id)
        {
            return Json(await vehicleServiceHistoryService.GetContactRenewalReminderById(Id));
        }

    }
}
