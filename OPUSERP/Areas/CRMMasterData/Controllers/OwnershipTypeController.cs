using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRMMasterData.Models;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using System;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMMasterData.Controllers
{
    [Area("CRMMasterData")]
    public class OwnershipTypeController : Controller
    {
        private readonly IOwnershipService ownershipService;

        public OwnershipTypeController(IOwnershipService ownershipService)
        {
            this.ownershipService = ownershipService;
        }

        public async Task<IActionResult> OwnershipType()
        {
            OwnershipViewModel model = new OwnershipViewModel()
            {
                ownerShipTypes= await ownershipService.GetAllOwnership()
            };
            return View(model);
        }

        // POST: ActivityCategory/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OwnershipType([FromForm] OwnershipViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ownerShipTypes = await ownershipService.GetAllOwnership();
                return View(model);
            }

            OwnerShipType data = new OwnerShipType
            {
                Id = model.ownershipTypeId,
                ownerShipTypeName = model.OwnershipType,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            await ownershipService.SaveOwnership(data);

            return RedirectToAction(nameof(OwnershipType));
        }
        public async Task<IActionResult> DeleteOwnershipTypeById(int id)
        {
            try
            {
                await ownershipService.DeleteOwnershipById(id);
                return RedirectToAction("OwnershipType", "OwnershipType", new { Area = "CRMMasterData" });
            }

            catch

            {
                return RedirectToAction("OwnershipType", "OwnershipType", new { Area = "CRMMasterData" });
            }
           
        }
    }
}