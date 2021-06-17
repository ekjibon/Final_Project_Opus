using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.SCM.Data.Entity.Supplier;
using OPUSERP.SCM.Services.Supplier.Interface;

namespace OPUSERP.Areas.Accounting.Controllers
{
    [Area("Accounting")]
    public class MigrationController : Controller
    {
        private readonly IOrganizationService organizationService;
        private readonly ILedgerService ledgerService;
        private readonly IUserInfoes userInfoes;
        private readonly IERPCompanyService companyService; 

        public MigrationController(IOrganizationService organizationService, IERPCompanyService companyService, ILedgerService ledgerService, IUserInfoes userInfoes)
        {
            this.organizationService = organizationService;
            this.ledgerService = ledgerService;
            this.userInfoes = userInfoes;
            this.companyService = companyService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> VendorConvertion()
        {
            VendorViewModel model = new VendorViewModel
            {
                organizations=await organizationService.GetOrganizationByLedgerId()
            };
            return View(model);
        }
        // POST: Migration/VendorConvertion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VendorConvertion([FromForm] VendorViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string username = HttpContext.User.Identity.Name;
            var userinfo = await userInfoes.GetUserInfoByUser(username);
            var company = await companyService.GetAllCompany();
            Ledger data = new Ledger
            {
                Id =0,
                accountName = model.accountName,
                accountCode = model.accountCode,
                //aliasName = model.aliasName,
                companyId = company.FirstOrDefault().Id,
                isActive = 1,
                currencyId = 1,
                haveSubLedger = 2,
                specialBranchUnitId = userinfo.specialBranchUnitId
            };
            int ledgerId = await ledgerService.Saveledger(data);
            if (ledgerId != 0)
            {
                if (model.ids != null)
                {
                    for (var i = 0; i < model.ids.Length; i++)
                    {
                        LedgerRelation data1 = new LedgerRelation
                        {
                            ledgerId = model.ids[i],
                            subLedgerId = ledgerId,
                        };
                        await ledgerService.SaveLedgerRelation(data1);
                    }
                }
            }
            await organizationService.UpdateVendorStatus(model.vendorId, ledgerId);

            return RedirectToAction(nameof(VendorConvertion));
        }

    }
}