using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Accounting.Data.Entity.FDR;
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Accounting.Services.FDR.Interface;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.ERPService.AuthService.Interfaces;

namespace OPUSERP.Areas.Accounting.Controllers
{
    [Authorize]
    [Area("Accounting")]
    public class BankChargeMasterController : Controller
    {
        private readonly IBankBranchService bankBranchService;
        private readonly ILedgerService ledgerService;
        private readonly IBankChargeMasterService bankChargeMasterService;
        private readonly IUserInfoes userInfo;

        public BankChargeMasterController(ILedgerService ledgerService, IBankBranchService bankBranchService, IBankChargeMasterService bankChargeMasterService, IUserInfoes userInfo)
        {
            this.ledgerService = ledgerService;
            this.bankBranchService = bankBranchService;
            this.bankChargeMasterService = bankChargeMasterService;
            this.userInfo = userInfo;
        }
        public async Task<IActionResult> Index()
        {
            string username = HttpContext.User.Identity.Name;
            var userinfo = await userInfo.GetUserInfoByUser(username);
            var branchid = userinfo?.specialBranchUnitId;

            BankChargeMasterViewModel model = new BankChargeMasterViewModel
            {
                banks= await bankBranchService.GetAllBank(),
                ledgerRelations = await ledgerService.GetLedgerForPayment(Convert.ToInt32(branchid)),
                bankBranchDetails = await bankBranchService.GetAllBankBranchDetails(),
                bankChargeMasters = await bankBranchService.GetBankChargeMaster(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] BankChargeMasterViewModel model)
        {
            string username = HttpContext.User.Identity.Name;
            var userinfo = await userInfo.GetUserInfoByUser(username);
            var branchid = userinfo?.specialBranchUnitId;

            //return Json(model);
            if (!ModelState.IsValid)
            {
                model.ledgerRelations = await ledgerService.GetLedgerForPayment(Convert.ToInt32(branchid));
                model.bankBranchDetails = await bankBranchService.GetAllBankBranchDetails();
                model.bankChargeMasters = await bankBranchService.GetBankChargeMaster();
            }

            BankChargeMaster data = new BankChargeMaster
            {
                Id = model.Id,
                bankBranchDetailsId = model.bankBranchDetailsId,
                ledgerRelationId = model.ledgerRelationId,
                AccountName = model.AccountName,
                AccountNumber = model.AccountNumber,
                Status = model.Status,
                AccountType=model.AccountType
            };

            await bankBranchService.SaveBankChargeMaster(data);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetBranchByBankId(int bankId)
        {
            return Json(await bankBranchService.GetBranchByBankId(bankId));
        }
    }
}