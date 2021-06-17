using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.ERPSettings.Models;
using OPUSERP.Data.Entity.Auth;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using System;
using System.Threading.Tasks;

namespace OPUSERP.Areas.ERPSettings.Controllers
{
    [Area("ERPSettings")]
    public class ERPModuleController : Controller
    {
        private readonly IUserInfoes userInfoes;
        private readonly IERPModuleService eRPModuleService;

        public ERPModuleController(IUserInfoes userInfoes, IERPModuleService eRPModuleService)
        {
            this.userInfoes = userInfoes;
            this.eRPModuleService = eRPModuleService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ERPModuleViewModel model = new ERPModuleViewModel
            {
                eRPModules=await eRPModuleService.GetAllERPModule()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] ERPModuleViewModel model)
        {
            ERPModule eRPModule = new ERPModule
            {
                Id=Convert.ToInt32(model.moduleId),
                moduleName=model.moduleName
            };
            await eRPModuleService.SaveERPModule(eRPModule);
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteModule(int moduleId)
        {
            await eRPModuleService.DeleteERPModuleById(moduleId);
            return Json(true);
        }
    }
}