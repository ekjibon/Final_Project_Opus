using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.VMS.Models;
using OPUSERP.VMS.Data.Entity.ServiceVehicle;
using OPUSERP.VMS.Data.Entity.VehicleInfo;
using OPUSERP.VMS.Services.VehicleInfo.Interfaces;
using OPUSERP.VMS.Services.VehicleService.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OPUSERP.Areas.VMS.Controllers
{
    [Authorize]
    [Area("VMS")]
    public class LabelTypeController : Controller
    {
        // GET: /<controller>/
        private readonly IVehicleServiceHistoryService vehicleServiceHistoryService;

        public LabelTypeController(IHostingEnvironment hostingEnvironment, IVehicleServiceHistoryService vehicleServiceHistoryService)
        {
            //_lang = new LangGenerate<AwardLn>(hostingEnvironment.ContentRootPath);
            this.vehicleServiceHistoryService = vehicleServiceHistoryService;
        }
        public async Task<IActionResult> Index()
        {
            LabelTypeViewModel model = new LabelTypeViewModel
            {
                //fLang = _lang.PerseLang("MasterData/AwardEN.json", "MasterData/AwardBN.json", Request.Cookies["lang"]),
                labelTypes = await vehicleServiceHistoryService.GetLabelType()
            };
            return View(model);
         
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] LabelTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //model.fLang = _lang.PerseLang("MasterData/AwardEN.json", "MasterData/AwardBN.json", Request.Cookies["lang"]);
                model.labelTypes = await vehicleServiceHistoryService.GetLabelType();
                return View(model);
            }

            LabelType data = new LabelType
            {
                Id = Convert.ToInt32(model.labelTypeId),
                labelTypeName = model.labelTypeName,
                
            };

            await vehicleServiceHistoryService.SaveLabelType(data);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await vehicleServiceHistoryService.DeleteLabelTypeById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
