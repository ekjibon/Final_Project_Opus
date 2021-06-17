using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Event.Models;
using OPUSERP.CLUB.Data.Event;
using OPUSERP.CLUB.Services.Event.Interfaces;
using System;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Event.Controllers
{
    [Area("Event")]
    public class EventTypeController : Controller
    {
        private readonly IEventCategoryService eventCategoryService;

        public EventTypeController(IEventCategoryService eventCategoryService)
        {
            this.eventCategoryService = eventCategoryService;
            this.eventCategoryService = eventCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> EventCategory()
        {
            EventCategoryViewModel model = new EventCategoryViewModel()
            {
                eventCategories = await eventCategoryService.GetAllEventCategory()
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EventCategory([FromForm] EventCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.eventCategories = await eventCategoryService.GetAllEventCategory();
                return View(model);
            }

            EventCategory data = new EventCategory
            {
                Id = model.id,
                name = model.name,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            await eventCategoryService.SaveEventCategory(data);
       
            return RedirectToAction(nameof(EventCategory));
        }

        public async Task<IActionResult> DeleteEventTypeById(int id)
        {
            try
            {
                await eventCategoryService.DeleteEventCategoryById(id);
                return RedirectToAction("EventCategory", "EventType", new { Area = "Event" });
            }

            catch

            {
                return RedirectToAction("EventCategory", "EventType", new { Area = "Event" });
            }

        }

    }

}