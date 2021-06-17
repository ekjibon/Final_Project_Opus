using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRMMasterData.Models;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using System;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMMasterData.Controllers
{
    [Area("CRMMasterData")]
    public class ActivityCategoryController : Controller
    {
        private readonly IActivityCategoryService activityCategoryService;
        private readonly IActivityTypeService activityTypeService;
        private readonly IActivitySessionService activitySessionService;
        private readonly IActivityNatureService activityNatureService;

        public ActivityCategoryController(IActivityCategoryService activityCategoryService, IActivityNatureService activityNatureService, IActivityTypeService activityTypeService, IActivitySessionService activitySessionService)
        {
            this.activityCategoryService = activityCategoryService;
            this.activityTypeService = activityTypeService;
            this.activitySessionService = activitySessionService;
            this.activityNatureService = activityNatureService;
        }

        public async Task<IActionResult> ActivityCategory()
        {
            ActivityCategoryViewModel model = new ActivityCategoryViewModel()
            {
                activityCategories = await activityCategoryService.GetAllActivityCategory()
            };
            return View(model);
        }
        public async Task<IActionResult> ActivityType()
        {
            ActivityTypeViewModel model = new ActivityTypeViewModel()
            {
                activityTypes = await activityTypeService.GetAllActivityType()
            };
            return View(model);
        }
        public async Task<IActionResult> ActivitySession()
        {
            ActivitySessionViewModel model = new ActivitySessionViewModel()
            {
                activitySessions = await activitySessionService.GetAllActivitySession()
            };
            return View(model);
        }
        public async Task<IActionResult> ActivityNature()
        {
            ActivityNatureViewModel model = new ActivityNatureViewModel()
            {
                activityNatures = await activityNatureService.GetAllActivityNature()
            };
            return View(model);
        }

        // POST: ActivityCategory/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivityCategory([FromForm] ActivityCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.activityCategories = await activityCategoryService.GetAllActivityCategory();
                return View(model);
            }
           

            ActivityCategory data = new ActivityCategory
            {
                Id = model.Id,
                activityCategoryName = model.activityCategoryName,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
           };

            await activityCategoryService.SaveActivityCategory(data);

            return RedirectToAction(nameof(ActivityCategory));
        }
        // POST: ActivityType/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivityType([FromForm] ActivityTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.activityTypes = await activityTypeService.GetAllActivityType();
                return View(model);
            }


            ActivityType data = new ActivityType
            {
                Id = model.Id,
                activityTypeName = model.activityTypeName,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            await activityTypeService.SaveActivityType(data);

            return RedirectToAction(nameof(ActivityType));
        }
        // POST: ActivitySession/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivitySession([FromForm] ActivitySessionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.activitySessions = await activitySessionService.GetAllActivitySession();
                return View(model);
            }


            ActivitySession data = new ActivitySession
            {
                Id = model.Id,
                activitySessionName = model.activitySessionName,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            await activitySessionService.SaveActivitySession(data);

            return RedirectToAction(nameof(ActivitySession));
        }
        // POST: ActivitySession/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivityNature([FromForm] ActivityNatureViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.activityNatures = await activityNatureService.GetAllActivityNature();
                return View(model);
            }


            ActivityNature data = new ActivityNature
            {
                Id = model.Id,
                activityNatureName = model.activityNatureName,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            await activityNatureService.SaveActivityNature(data);

            return RedirectToAction(nameof(ActivityNature));
        }


        public async Task<IActionResult> DeleteActivityCategoryById(int id)
        {
            try
            {
                await activityCategoryService.DeleteActivityCategoryById(id);
                return RedirectToAction("ActivityCategory", "ActivityCategory", new { Area = "CRMMasterData" });
            }
            catch
            {
                return RedirectToAction("ActivityCategory", "ActivityCategory", new { Area = "CRMMasterData" });
            }
           
        }

        public async Task<IActionResult> DeleteActivityTypeById(int id)
        {
            try
            {
                await activityTypeService.DeleteActivityTypeById(id);
                return RedirectToAction("ActivityType", "ActivityCategory", new { Area = "CRMMasterData" });
            }
            catch
            {
                return RedirectToAction("ActivityType", "ActivityCategory", new { Area = "CRMMasterData" });
            }
           
        }

        public async Task<IActionResult> DeleteActivitySessionById(int id)
        {
            try
            {
                await activitySessionService.DeleteActivitySessionById(id);

                return RedirectToAction("ActivitySession", "ActivityCategory", new { Area = "CRMMasterData" });
            }
            catch
            {
                return RedirectToAction("ActivitySession", "ActivityCategory", new { Area = "CRMMasterData" });
            }
        }

        public async Task<IActionResult> DeleteActivityNatureById(int id)
        {
            try
            {
                await activityNatureService.DeleteActivityNatureById(id);
                return RedirectToAction("ActivityNature", "ActivityCategory", new { Area = "CRMMasterData" });
            }
            catch
            {
                return RedirectToAction("ActivityNature", "ActivityCategory", new { Area = "CRMMasterData" });
            }
        }

    }
}