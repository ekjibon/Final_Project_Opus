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
    public class LimitPeriodTypeController : Controller
    {
        // GET: /<controller>/
        private readonly IVehicleServiceHistoryService vehicleServiceHistoryService;

        public LimitPeriodTypeController(IHostingEnvironment hostingEnvironment, IVehicleServiceHistoryService vehicleServiceHistoryService)
        {
            //_lang = new LangGenerate<AwardLn>(hostingEnvironment.ContentRootPath);
            this.vehicleServiceHistoryService = vehicleServiceHistoryService;
        }
        public async Task<IActionResult> Index()
        {
            LimitPeriodTypeViewModel model = new LimitPeriodTypeViewModel
            {
                //fLang = _lang.PerseLang("MasterData/AwardEN.json", "MasterData/AwardBN.json", Request.Cookies["lang"]),
                limitPeriodTypes = await vehicleServiceHistoryService.GetlimitPeriodType()
            };
            return View(model);
         
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] LimitPeriodTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //model.fLang = _lang.PerseLang("MasterData/AwardEN.json", "MasterData/AwardBN.json", Request.Cookies["lang"]);
                model.limitPeriodTypes = await vehicleServiceHistoryService.GetlimitPeriodType();
                return View(model);
            }

            LimitPeriodType data = new LimitPeriodType
            {
                Id = Convert.ToInt32(model.limitPeriodTypeId),
                limitPeriodTypeName = model.limitPeriodTypeName,
                
            };

            await vehicleServiceHistoryService.SavelimitPeriodType(data);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await vehicleServiceHistoryService.DeletelimitPeriodTypeById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
