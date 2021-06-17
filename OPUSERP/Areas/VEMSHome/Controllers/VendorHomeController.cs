using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.VEMSHome.Models;
using OPUSERP.SCM.Services.PurchaseProcess.Interfaces;
using OPUSERP.VEMS.Data.Entity.RegistrationForm;
using OPUSERP.VEMS.Services.MasterData.Interface;
using OPUSERP.VEMS.Services.Registration.Interface;

namespace OPUSERP.Areas.VEMSHome.Controllers
{
    [Area("VEMSHome")]
    public class VendorHomeController : Controller
    {
        private readonly IMasterDataServices masterDataServices;
        private readonly IProcurementService procurementService;
        private readonly IRegistrationService registrationService;

        public VendorHomeController(IMasterDataServices masterDataServices, IRegistrationService registrationService, IProcurementService procurementService)
        {
            this.masterDataServices = masterDataServices;
            this.procurementService = procurementService;
            this.registrationService = registrationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> ReqDocList()
        {
            RegistrationViewModel model = new RegistrationViewModel
            {
                requiredDocuments = await masterDataServices.GetRequiredDocument(),
            };
            return View(model);
        }

        public async Task<ActionResult> CodeOfConduct()
        {
            RegistrationViewModel model = new RegistrationViewModel
            {
                codeOfContacts = await masterDataServices.GetCodeOfContact(),
            };
            return View(model);
        }

        public async Task<ActionResult> NDAView()
        {
            RegistrationViewModel model = new RegistrationViewModel
            {
                nDAFile = await masterDataServices.GetLastNDAFile(),
            };
            return View(model);
        }

        public async Task<ActionResult> Registration(int type)
        {
            ViewBag.type = type;
            RegistrationViewModel model = new RegistrationViewModel
            {
                cSItemCategories = await procurementService.GetCSItemCategoryList(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration([FromForm] RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //return Json(model);

            RegistrationForm data = new RegistrationForm
            {
                Id = 0,
                companyName = model.companyName,
                typeOfNDAAgree = model.typeOfNDAAgree,
                tradeLicenseNumber = model.tradeLicenseNumber,
                eTinNumber = model.eTinNumber,
                vatNumber = model.vatNumber,
                contactPersonName = model.contactPersonName,
                contactNumber = model.contactNumber,
                companyEmail = model.companyEmail,
                alternativeEmail = model.alternativeEmail,
                procurementCategoryId = model.procurementCategoryId,
                productServiceName = model.productServiceName,
                partialDisagreement = model.partialDisagreement,
                loginId = model.loginId,
                password = model.password,
                approveType = 0
            };
            var masterId = await registrationService.SaveRegistrationForm(data);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] RegistrationForm model)
        {
            //return Json(model);
            var result = await registrationService.GetLoginInfoByLoginIdAndPassword(model.loginId, model.password);
            if (result != null)
            {
                HttpContext.Session.SetString("userName", result.loginId);
                HttpContext.Session.SetInt32("Id", result.Id);

                return RedirectToAction("Index", "GeneralInformation", new { area = "VEMSInformation", @id = result.Id });
            }
            else
            {
                return RedirectToAction(nameof(Login));
            }
        }

        public IActionResult logout()
        {
            HttpContext.Session.Remove("userName");
            HttpContext.Session.Remove("Id");

            return RedirectToAction(nameof(Index));
        }

        //xxxxxxxxxxxxxxxxxxxxx
    }
}