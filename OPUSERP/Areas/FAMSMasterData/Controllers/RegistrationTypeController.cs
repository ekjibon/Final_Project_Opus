using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRMMasterData.Models;
using OPUSERP.Areas.FAMSMasterData.Models;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.FixedAsset.Data.Entity.MasterData;
using OPUSERP.FixedAsset.Services.MasterData.Interfaces;
using System;
using System.Threading.Tasks;

namespace OPUSERP.Areas.FAMSMasterData.Controllers
{
    [Area("FAMSMasterData")]
    public class RegistrationTypeController : Controller
    {
        private readonly IRegistrationTypeService registrationTypeService;
 

        public RegistrationTypeController(IRegistrationTypeService registrationTypeService)
        {
            this.registrationTypeService = registrationTypeService;
          
        }

        public async Task<IActionResult> Index()
        {
            RegistrationTypeViewModel model = new RegistrationTypeViewModel()
            {
                registrationTypes = await registrationTypeService.GetAllRegistrationType()
            };
            return View(model);
        }
       
       
        // POST: ActivityCategory/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] RegistrationTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.registrationTypes = await registrationTypeService.GetAllRegistrationType();
                return View(model);
            }
           

            RegistrationType data = new RegistrationType
            {
                Id = model.Id,
                RegistrationTypeName = model.RegistrationTypeName,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
           };

            await registrationTypeService.SaveRegistrationType(data);

            return RedirectToAction(nameof(Index));
        }
        // POST: ActivityType/save/Edit
      


        public async Task<IActionResult> DeleteRegistrationTypeById(int id)
        {
            try
            {
                await registrationTypeService.DeleteRegistrationTypeById(id);
                return RedirectToAction("Index", "RegistrationType", new { Area = "FAMSMasterData" });
            }
            catch
            {
                return RedirectToAction("Index", "RegistrationType", new { Area = "CRMMasterData" });
            }
           
        }

     

    }
}