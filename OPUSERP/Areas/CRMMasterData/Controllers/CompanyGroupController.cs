using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRMMasterData.Models;
using OPUSERP.Data.Entity.Master;
using OPUSERP.ERPServices.MasterData.Interfaces;

namespace OPUSERP.Areas.CRMMasterData.Controllers
{
   
   [Area("CRMMasterData")]
    public class CompanyGroupController : Controller
    {
        private readonly ICompanyGroupService companyGroupService;

        public CompanyGroupController(ICompanyGroupService companyGroupService)
        {
            this.companyGroupService = companyGroupService;
        }

        public async Task<IActionResult> CompanyGroup()
        {
            CompanyGroupViewModel model = new CompanyGroupViewModel()
            {
                companyGroups = await companyGroupService.GetAllCompanyGroup()
            };
            return View(model);
        }
        

        // POST: ActivityCategory/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompanyGroup([FromForm] CompanyGroupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.companyGroups = await companyGroupService.GetAllCompanyGroup();
                return View(model);
            }


            CompanyGroup data = new CompanyGroup
            {
                Id = model.Id,
                groupName = model.companyGroupName,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            await companyGroupService.SaveCompanyGroup(data);

            return RedirectToAction(nameof(CompanyGroup));
        }
        
        public async Task<IActionResult> DeleteCompanyGroupById(int id)
        {
            try
            {
                await companyGroupService.DeleteCompanyGroupById(id);
                return RedirectToAction("CompanyGroup", "CompanyGroup", new { Area = "CRMMasterData" });
            }
            catch
            {
                return RedirectToAction("CompanyGroup", "CompanyGroup", new { Area = "CRMMasterData" });
            }

        }

    }
}