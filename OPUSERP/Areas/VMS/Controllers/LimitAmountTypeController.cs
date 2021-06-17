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
    public class LimitAmountTypeController : Controller
    {
        // GET: /<controller>/
        private readonly IVehicleServiceHistoryService vehicleServiceHistoryService;

        public LimitAmountTypeController(IHostingEnvironment hostingEnvironment, IVehicleServiceHistoryService vehicleServiceHistoryService)
        {
            //_lang = new LangGenerate<AwardLn>(hostingEnvironment.ContentRootPath);
            this.vehicleServiceHistoryService = vehicleServiceHistoryService;
        }
        public async Task<IActionResult> Index()
        {
            LimitAmountTypeViewModel model = new LimitAmountTypeViewModel
            {
                //fLang = _lang.PerseLang("MasterData/AwardEN.json", "MasterData/AwardBN.json", Request.Cookies["lang"]),
                limitAmountTypes = await vehicleServiceHistoryService.GetlimitAmountType()
            };
            return View(model);
         
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] LimitAmountTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //model.fLang = _lang.PerseLang("MasterData/AwardEN.json", "MasterData/AwardBN.json", Request.Cookies["lang"]);
                model.limitAmountTypes = await vehicleServiceHistoryService.GetlimitAmountType();
                return View(model);
            }

            LimitAmountType data = new LimitAmountType
            {
                Id = Convert.ToInt32(model.limitAmountTypeId),
                limitAmountTypeName = model.limitAmountTypeName,
                
            };

            await vehicleServiceHistoryService.SavelimitAmountType(data);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await vehicleServiceHistoryService.DeletelimitAmountTypeById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
