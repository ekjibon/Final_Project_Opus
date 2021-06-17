using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRMMasterData.Models;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data.Entity.Master;
using OPUSERP.ERPServices.MasterData.Interfaces;
using System;
using System.Threading.Tasks;
using ComTypeViewModel = OPUSERP.Areas.CRMMasterData.Models.ComTypeViewModel;

namespace OPUSERP.Areas.CRMMasterData.Controllers
{
    [Area("CRMMasterData")]
    public class CommunicationController : Controller
    {
        private readonly ICommunicationService communicationService;
        private readonly ICompanyGroupService companyGroupService;

        public CommunicationController(ICommunicationService  communicationService, ICompanyGroupService companyGroupService)
        {
            this.communicationService = communicationService;
            this.companyGroupService = companyGroupService;
        }

        public async Task<IActionResult> CommunicationType()
        {
            ComTypeViewModel model = new ComTypeViewModel
            {
                communicationTypes = await communicationService.GetAllCommunicationType()
            };
            return View(model);
        }

        // POST: Organization Status/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CommunicationType([FromForm] ComTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.communicationTypes= await communicationService.GetAllCommunicationType();
                return View(model);
            }

            CommunicationType data = new CommunicationType
            {
                Id = model.comTypeId,
                communicationTypeName = model.communicationTypeName,
            };

            await communicationService.SaveCommunicationType(data);

            return RedirectToAction(nameof(CommunicationType));
        }

        public async Task<IActionResult> Area()
        {
            AreaViewModel model = new AreaViewModel
            {
                areas = await communicationService.GetAllArea()
            };
            return View(model);
        }

        // POST: Organization Status/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Area([FromForm] AreaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.areas = await communicationService.GetAllArea();
                return View(model);
            }

            Area data = new Area
            {
                Id = model.areaId,
                areaCode = model.areaCode,
                areaName = model.areaName,
                areaLocation = model.areaLocation,
            };

            await communicationService.SaveArea(data);

            return RedirectToAction(nameof(Area));
        }

        public async Task<IActionResult> DeleteCommunicationType(int id)
        {
            await communicationService.DeleteCommunicationTypeById(id);
            return RedirectToAction("Communication", "CommunicationType", new { Area = "CRMMasterData" });
        }
        //public async Task<IActionResult> CompanyGroup()
        //{
        //    CompanyGroupViewModel model = new CompanyGroupViewModel
        //    {
        //        companyGroups = await companyGroupService.GetAllCompanyGroup()
        //    };
        //    return View(model);
        //}

        //// POST: Company Group/Edit
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CompanyGroup([FromForm] CompanyGroupViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        model.companyGroups = await companyGroupService.GetAllCompanyGroup();
        //        return View(model);
        //    }

        //    CompanyGroup data = new CompanyGroup
        //    {
        //        Id = model.companyGroupId,
        //        groupName = model.compGroupName,
        //        createdBy = HttpContext.User.Identity.Name,
        //        createdAt = DateTime.Now
        //    };

        //    await companyGroupService.SaveCompanyGroup(data);

        //    return RedirectToAction(nameof(CompanyGroup));
        //}
    }
}