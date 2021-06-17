using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSMasterData.Models;
using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSMasterData.Controllers
{
    [Authorize]
    [Area("HRPMSMasterData")]
    public class ServiceStatusController : Controller
    {
        private readonly LangGenerate<ServiceStatusLn> _lang;
        // GET: ServiceStatus
        private readonly IStatusService statusService;

        public ServiceStatusController(IHostingEnvironment hostingEnvironment, IStatusService statusService)
        {
            _lang = new LangGenerate<ServiceStatusLn>(hostingEnvironment.ContentRootPath);
            this.statusService = statusService;
        }

        #region ServiceStatus
        public async Task<IActionResult> Index()
        {
            ServiceStatusViewModel model = new ServiceStatusViewModel
            {
                fLang = _lang.PerseLang("MasterData/ServiceStatusEN.json", "MasterData/ServiceStatusBN.json", Request.Cookies["lang"]),
                serviceStatus = await statusService.GetServiceStatus()
            };
            return View(model);
        }

        // POST: ServiceStatus/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ServiceStatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/ServiceStatusEN.json", "MasterData/ServiceStatusBN.json", Request.Cookies["lang"]);
                model.serviceStatus = await statusService.GetServiceStatus();
                return View(model);
            }

            ServiceStatus data = new ServiceStatus
            {
                Id = model.serviceStatusId,
                statusName = model.statusName,
                statusNameBn = model.statusNameBn,
                statusShortName = model.statusShortName
            };

            await statusService.SaveServiceStatus(data);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteServiceStatusById(int Id)
        {
            await statusService.DeleteServiceStatusById(Id);
            return Json(true);
        }

        #endregion

        #region Hr Program
        public async Task<IActionResult> HrProgram()
        {
            HrProgramViewModel model = new HrProgramViewModel
            {
                hrPrograms = await statusService.GetHrProgram()
            };
            return View(model);
        }

        // POST: ServiceStatus/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HrProgram([FromForm] HrProgramViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.hrPrograms = await statusService.GetHrProgram();
                return View(model);
            }

            HrProgram data = new HrProgram
            {
                Id = model.hrProgramId,
                programName = model.programName,
                programNameBn = model.programNameBn,
                shortName = model.shortName,
                startDate = model.startDate,
                endDate = model.endDate
            };

            await statusService.SaveHrProgram(data);

            return RedirectToAction(nameof(HrProgram));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteHrProgramById(int Id)
        {
            await statusService.DeleteHrProgramById(Id);
            return Json(true);
        }

        #endregion

        #region Hr Unit
        public async Task<IActionResult> HrUnit()
        {
            HrUnitViewModel model = new HrUnitViewModel
            {
                hrUnits = await statusService.GetHrUnit()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HrUnit([FromForm] HrUnitViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.hrUnits = await statusService.GetHrUnit();
                return View(model);
            }

            HrUnit data = new HrUnit
            {
                Id = model.hrUnitId,
                unitName = model.unitName,
                unitNameBn = model.unitNameBn,
                shortName = model.shortName
            };

            await statusService.SaveHrUnit(data);

            return RedirectToAction(nameof(HrUnit));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteHrUnitById(int Id)
        {
            await statusService.DeleteHrUnitById(Id);
            return Json(true);
        }

        #endregion


    }
}