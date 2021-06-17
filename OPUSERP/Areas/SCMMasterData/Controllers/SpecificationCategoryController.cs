using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.SCMMasterData.Models;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMMasterData.Controllers
{
    [Authorize]
    [Area("SCMMasterData")]
    public class SpecificationCategoryController : Controller
    {
        private readonly IItemsService itemsService;

        public SpecificationCategoryController(IItemsService itemsService)
        {
            this.itemsService = itemsService;
        }

        // GET: EventCategory
        public async Task<IActionResult> Index()
        {
            SpecificationCategoryViewModel model = new SpecificationCategoryViewModel
            {
                specificationCategories = await itemsService.GetAllSpecificationCategory(),
            };
            return View(model);
        }

        // POST: EventCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] SpecificationCategoryViewModel model)
        {
            var validation = await itemsService.GetSpecificationCategoryByname(model.specificationCategoryName);
            
            //return Json(model);
            if (!ModelState.IsValid)
            {
                model.specificationCategories = await itemsService.GetAllSpecificationCategory();
               
                return View(model);
            }
            if (validation != null&&model.specificationCategoryId==0&&validation.itemCategoryId==model.parentId)
            {
                model.specificationCategories = await itemsService.GetAllSpecificationCategory();
                if (validation.Id != 0)
                {
                    ModelState.AddModelError(string.Empty, "Specification Category Name '" + model.specificationCategoryName + "' is already exists.");
                }
                return View(model);
            }

            SpecificationCategory data = new SpecificationCategory
            {
                Id = model.specificationCategoryId,
                specificationCategoryName = model.specificationCategoryName,
                itemCategoryId=model.parentId
            };

            await itemsService.SaveSpecificationCategory(data);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await itemsService.DeleteSpecificationCategoryById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}