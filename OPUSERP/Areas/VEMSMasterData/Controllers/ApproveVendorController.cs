using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.VEMSMasterData.Models;
using OPUSERP.SCM.Data.Entity.Supplier;
using OPUSERP.SCM.Services.Supplier.Interface;
using OPUSERP.VEMS.Data.Entity.RegistrationForm;
using OPUSERP.VEMS.Services.Registration.Interface;

namespace OPUSERP.Areas.VEMSMasterData.Controllers
{
    [Area("VEMSMasterData")]
    public class ApproveVendorController : Controller
    {
        private readonly IRegistrationService registrationService;
        private readonly IOrganizationService organizationService;

        public ApproveVendorController(IRegistrationService registrationService, IOrganizationService organizationService)
        {
            this.registrationService = registrationService;
            this.organizationService = organizationService;
        }

        public async Task<ActionResult> Index()
        {
            ApproveViewModel model = new ApproveViewModel
            {
                pendingRegistrationForms = await registrationService.GetRegistrationFormByApproveType(0),
                approvedRegistrationForms = await registrationService.GetRegistrationFormByApproveType(1),
                rejectedRegistrationForms = await registrationService.GetRegistrationFormByApproveType(2),
            };
            return View(model);
        }

        public async Task<ActionResult> Approve(int id)
        {
            ApproveViewModel model = new ApproveViewModel
            {
                registrationForm = await registrationService.GetRegistrationFormById(id),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Approve([FromForm] ApproveViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //return Json(model);

            var masterId = await registrationService.UpdateRegistrationForm(model.companyId, model.approveType);
            RegistrationForm registrationForm = await registrationService.GetRegistrationFormById((int)model.companyId);

            if (model.approveType == 1)
            {
                Organization organization = new Organization
                {
                    organizationName = registrationForm.companyName,
                    LicenseNumber = registrationForm.tradeLicenseNumber,
                    eTinNumber =registrationForm.eTinNumber,
                    VATNumber = registrationForm.vatNumber,
                    email = registrationForm.companyEmail,
                    alternativeEmail = registrationForm.alternativeEmail,
                    mobile = registrationForm.contactNumber,
                    phone =registrationForm.contactNumber,
                };
                int id = await organizationService.SaveOrganization(organization);
                await registrationService.UpdateRegistrationFormSupplierId(model.companyId, id);
            }

            return RedirectToAction(nameof(Index));
        }



    }
}