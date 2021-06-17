using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSMasterData.Models;
using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.HRPMSMasterData.Controllers
{
    [Authorize]
    [Area("HRPMSMasterData")]
    public class OrganizationController : Controller
    {
        private readonly LangGenerate<OrganizationLn> _lang;
        private readonly IOrganizationService organizationService;

        public OrganizationController(IHostingEnvironment hostingEnvironment, IOrganizationService organizationService)
        {
            _lang = new LangGenerate<OrganizationLn>(hostingEnvironment.ContentRootPath);
            this.organizationService = organizationService;
        }
        // GET: Organization
        public async Task<IActionResult> Index()
        {
            OrganizationViewModel model = new OrganizationViewModel
            {
                fLang = _lang.PerseLang("MasterData/OrganizationEN.json", "MasterData/OrganizationBN.json", Request.Cookies["lang"]),
                organizations = await organizationService.GetAllOrganization()
            };
            return View(model);
        }

        // POST: Organization/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] OrganizationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/OrganizationEN.json", "MasterData/OrganizationBN.json", Request.Cookies["lang"]);
                model.organizations = await organizationService.GetAllOrganization();
                return View(model);
            }

            Organization data = new Organization
            {
                Id = model.organizationId,
                organizationType = model.organizationType,
                organizationName = model.organizationName,
                organizationNameBn = model.organizationNameBn
            };

            await organizationService.SaveOrganization(data);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteOrganizationById(int Id)
        {
            await organizationService.DeleteOrganizationById(Id);
            return Json(true);
        }

        #region API Section
        [Route("global/api/organizations")]
        [HttpGet]
        public async Task<IActionResult> Organizations()
        {
            return Json(await organizationService.GetAllOrganization());
        }
        #endregion
    }
}