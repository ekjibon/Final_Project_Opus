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
    public class ServiceTaskController : Controller
    {
        // GET: /<controller>/
        private readonly IVehicleServiceHistoryService vehicleServiceHistoryService;

        public ServiceTaskController(IHostingEnvironment hostingEnvironment, IVehicleServiceHistoryService vehicleServiceHistoryService)
        {
            //_lang = new LangGenerate<AwardLn>(hostingEnvironment.ContentRootPath);
            this.vehicleServiceHistoryService = vehicleServiceHistoryService;
        }
        public async Task<IActionResult> Index()
        {
            ServiceTaskViewModel model = new ServiceTaskViewModel
            {
                //fLang = _lang.PerseLang("MasterData/AwardEN.json", "MasterData/AwardBN.json", Request.Cookies["lang"]),
                serviceTasks = await vehicleServiceHistoryService.GetServiceTask()
            };
            return View(model);
         
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ServiceTaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //model.fLang = _lang.PerseLang("MasterData/AwardEN.json", "MasterData/AwardBN.json", Request.Cookies["lang"]);
                model.serviceTasks = await vehicleServiceHistoryService.GetServiceTask();
                return View(model);
            }

            ServiceTask data = new ServiceTask
            {
                Id = Convert.ToInt32(model.serviceTaskNameId),
                serviceTaskName = model.serviceTaskName,
                description=model.description,
                subTaskName=model.subTaskName
                
            };

            await vehicleServiceHistoryService.SaveServiceTask(data);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await vehicleServiceHistoryService.DeleteServiceTaskById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
