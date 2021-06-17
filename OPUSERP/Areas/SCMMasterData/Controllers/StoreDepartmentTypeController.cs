using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.SCMMasterData.Models;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMMasterData.Controllers
{
    [Area("SCMMasterData")]
    public class StoreDepartmentTypeController : Controller
    {
        private readonly IStoreDepartmentService storeDepartmentService;

        public StoreDepartmentTypeController(IStoreDepartmentService storeDepartmentService)
        {
            this.storeDepartmentService = storeDepartmentService;
        }
        public async Task<IActionResult> Index()
        {
            StoreDepartmentTypeViewModel model = new StoreDepartmentTypeViewModel
            {
                StoreDepartmentTypes = await storeDepartmentService.GetDeartmentTypeList()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] StoreDepartmentTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.StoreDepartmentTypes = await storeDepartmentService.GetDeartmentTypeList();
                return View(model);
            }

            StoreDepartmentType data = new StoreDepartmentType
            {
                Id = model.id,
                deptName = model.deptName,
                deptNameBn = model.deptNameBn
            };

            await storeDepartmentService.SaveDepartmentType(data);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var item = storeDepartmentService.GetDeartmentTypeById(id);
            return View(item);
        }

        [HttpPost]
        public IActionResult Update(StoreDepartmentType storeDepartmentType)
        {
            storeDepartmentService.UpdateDepartmentType(storeDepartmentType);
            return View(storeDepartmentType);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await storeDepartmentService.DeleteDeartmentType(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
