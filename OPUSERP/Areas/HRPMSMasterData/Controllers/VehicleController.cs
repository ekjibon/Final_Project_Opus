using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSMasterData.Models;
using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.HRPMSMasterData.Controllers
{
    [Authorize]
    [Area("HRPMSMasterData")]    
    public class VehicleController : Controller
    {
        private readonly LangGenerate<VehicleInfoLn> _lang;
        private readonly ITravelService travelService;

        public VehicleController(IHostingEnvironment hostingEnvironment, ITravelService travelService)
        {
            _lang = new LangGenerate<VehicleInfoLn>(hostingEnvironment.ContentRootPath);
            this.travelService = travelService;
        }

        // GET: Vehicle
        public async Task<IActionResult> Index()
        {
            VehicleViewModel model = new VehicleViewModel
            {
                fLang = _lang.PerseLang("MasterData/VehicleTypeEN.json", "MasterData/VehicleTypeBN.json", Request.Cookies["lang"]),
                vehicles = await travelService.GetVehicleType()
            };
            return View(model);
        }

        // POST: Vehicle/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] VehicleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/VehicleTypeEN.json", "MasterData/VehicleTypeBN.json", Request.Cookies["lang"]);
                model.vehicles = await travelService.GetVehicleType();
                return View(model);
            }

            Vehicle data = new Vehicle
            {
                Id = model.vehicleId,
                vehicleType = model.vehicleType,
                vehicleTypeBn = model.vehicleTypeBn,
                vehicleShortName = model.vehicleShortName
            };

            await travelService.SaveVehicleType(data);

            return RedirectToAction(nameof(Index));
        }

        #region API Section
        [Route("global/api/vehicleType")]
        [HttpGet]
        public async Task<IActionResult> VehicleType()
        {
            return Json(await travelService.GetVehicleType());
        }
        #endregion
    }
}