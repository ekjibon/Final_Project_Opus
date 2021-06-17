using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRMMasterData.Models;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using System;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMMasterData.Controllers
{
    [Area("CRMMasterData")]
    public class AgreementCategoryController : Controller
    {
        private readonly IAgreementCategoryService agreementCategoryService;

        public AgreementCategoryController(IAgreementCategoryService agreementCategoryService)
        {
            this.agreementCategoryService = agreementCategoryService;
        }

        public async Task<IActionResult> AgreementCategory()
        {
            AgreementCategoryViewModel model = new AgreementCategoryViewModel()
            {
                agreementCategories = await agreementCategoryService.GetAllAgreementCategory()
            };
            return View(model);
        }

        // POST: AgreementCategory/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgreementCategory([FromForm] AgreementCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.agreementCategories = await agreementCategoryService.GetAllAgreementCategory();
                return View(model);
            }


            AgreementCategory data = new AgreementCategory
            {
                Id = model.Id,
                agreementCategoryName = model.agreementCategoryName,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            await agreementCategoryService.SaveAgreementCategory(data);

            return RedirectToAction(nameof(AgreementCategory));
        }

    }
}