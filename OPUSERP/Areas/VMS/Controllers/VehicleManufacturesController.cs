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
    public class VehicleManufacturesController : Controller
    {
        // GET: /<controller>/
        private readonly IVMSVehicleInfoService vehicleInfoService;

        public VehicleManufacturesController(IHostingEnvironment hostingEnvironment, IVMSVehicleInfoService vehicleInfoService)
        {
            //_lang = new LangGenerate<AwardLn>(hostingEnvironment.ContentRootPath);
            this.vehicleInfoService = vehicleInfoService;
        }
        public async Task<IActionResult> Index()
        {
            VehicleManufacturesViewModel model = new VehicleManufacturesViewModel
            {
                //fLang = _lang.PerseLang("MasterData/AwardEN.json", "MasterData/AwardBN.json", Request.Cookies["lang"]),
                vehicleManufactures = await vehicleInfoService.GetVehicleManufacture()
            };
            return View(model);
         
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] VehicleManufacturesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //model.fLang = _lang.PerseLang("MasterData/AwardEN.json", "MasterData/AwardBN.json", Request.Cookies["lang"]);
                model.vehicleManufactures = await vehicleInfoService.GetVehicleManufacture();
                return View(model);
            }

            VehicleManufacture data = new VehicleManufacture
            {
                Id = Convert.ToInt32(model.vehicleManufactureId),
                vehicleManufactureName = model.vehicleManufactureName,
                
            };

            await vehicleInfoService.SaveVehicleManufacture(data);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await vehicleInfoService.DeleteVehicleManufactureById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
