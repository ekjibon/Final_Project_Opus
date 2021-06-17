using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.VMS.Models;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;
using OPUSERP.VMS.Services.VehicleInfo.Interfaces;
using OPUSERP.VMS.Services.VehicleService.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OPUSERP.Areas.VMS.Controllers
{
    [Authorize]
    [Area("VMS")]
    public class InspectionCheckLIstCategoryController : Controller
    {
        // GET: /<controller>/
        private readonly IVehicleServiceHistoryService vehicleServiceHistoryService;

        public InspectionCheckLIstCategoryController(IHostingEnvironment hostingEnvironment, IVehicleServiceHistoryService vehicleServiceHistoryService)
        {
            //_lang = new LangGenerate<AwardLn>(hostingEnvironment.ContentRootPath);
            this.vehicleServiceHistoryService = vehicleServiceHistoryService;
        }
        public async Task<IActionResult> Index()
        {
            InspectionCheckListCategoryViewModel model = new InspectionCheckListCategoryViewModel
            {
                //fLang = _lang.PerseLang("MasterData/AwardEN.json", "MasterData/AwardBN.json", Request.Cookies["lang"]),
                inspectionCheckLIstCategories = await vehicleServiceHistoryService.GetInspectionCheckLIstCategory()
            };
            return View(model);
         
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] InspectionCheckListCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //model.fLang = _lang.PerseLang("MasterData/AwardEN.json", "MasterData/AwardBN.json", Request.Cookies["lang"]);
                model.inspectionCheckLIstCategories = await vehicleServiceHistoryService.GetInspectionCheckLIstCategory();
                return View(model);
            }

            InspectionCheckLIstCategory data = new InspectionCheckLIstCategory
            {
                Id = Convert.ToInt32(model.inspectionCheckListCategoryId),
                checkListCategoryName = model.inspectionCheckListCategoryName,
                
            };

            await vehicleServiceHistoryService.SaveinspectionCheckLIstCategory(data);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await vehicleServiceHistoryService.DeleteInspectionCheckLIstCategoriesById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
