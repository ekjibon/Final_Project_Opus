using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
//using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Accounting.Controllers
{
    [Authorize]
    [Area("Accounting")]
    public class SubLedgerController : Controller
    {
        private readonly ILedgerService ledgerService;
        private readonly IAccountGroupService accountGroupService;
        private readonly ISpecialBranchUnitService specialBranchUnitService;
        private readonly IUserInfoes userInfoes;
        private readonly IERPCompanyService companyService;

        public SubLedgerController(ILedgerService ledgerService, IUserInfoes userInfoes, IERPCompanyService companyService, IAccountGroupService accountGroupService, ISpecialBranchUnitService specialBranchUnitService)
        {
            this.ledgerService = ledgerService;
            this.accountGroupService = accountGroupService;
            this.specialBranchUnitService = specialBranchUnitService;
            this.userInfoes = userInfoes;
            this.companyService = companyService;
        }

        // GET: SubLedger
        public async Task<IActionResult> Index()
        {
            string username = HttpContext.User.Identity.Name;
            var userinfo = await userInfoes.GetUserInfoByUser(username);
            //ViewBag.branchid = userinfo?.specialBranchUnitId;
            string userName = userinfo?.UserName;
            int? branchid = 0;
            if (userName == "biplab" || userName == "suza")
            {
                ViewBag.branchid = 0;
                branchid = 0;
            }
            else
            {
                ViewBag.branchid = userinfo?.specialBranchUnitId;
                branchid = userinfo?.specialBranchUnitId;
            }

            var subLedger = await ledgerService.GetLedgerWithBigSubLedger();
            string accTemp = string.Empty;
            if (subLedger != null)
            {
                accTemp = subLedger.accountCode;
            }
            else
            {
                accTemp = 0.ToString();
            }

            //var accTemp = subLedger.accountCode;
            var accWithOutS = accTemp.Replace('S', '0');
            int newAcc = Int32.Parse(accWithOutS) + 1;
            string newCode = "S" + newAcc;

            LedgerViewModel model = new LedgerViewModel
            {
                accountCode = newCode,
                //ledgers = await ledgerService.GetSubLedger(),
                ledgers = await ledgerService.GetSubLedgerBySbuId(branchid),
                specialBranchUnits =await specialBranchUnitService.GetSpecialBranchUnit()
            };
            return View(model);
        }        

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] LedgerViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                return View(model);
            }

           
            var company = await companyService.GetAllCompany();
            string newCode = "";
            if (model.subLedgerId != 0)
            {
                newCode = model.accountCode;
            }
            else
            {
                string accTemp = string.Empty;
                var subLedger = await ledgerService.GetLedgerWithBigSubLedger();
                if (subLedger != null)
                {
                    accTemp = subLedger.accountCode;
                }
                else
                {
                    accTemp = 0.ToString();
                }
                //var accTemp = subLedger.accountCode;
                var accWithOutS = accTemp.Replace('S', '0');
                int newAcc = Int32.Parse(accWithOutS) + 1;
                newCode = "S" + newAcc;
            }
            Ledger data = new Ledger
            {
                Id = model.subLedgerId,
                accountName = model.accountName,
                accountCode = newCode,
                aliasName = model.aliasName,
                companyId = company.FirstOrDefault().Id,
                isActive = 1,
                currencyId = 1,
                haveSubLedger = 2,
                specialBranchUnitId=model.specialBranchUnitId
            };

            int ledgerId = await ledgerService.Saveledger(data);

            if (ledgerId != 0)
            {
                if(model.ids!=null)
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

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteSubLedger(int Id)
        {
            await ledgerService.DeleteledgerRelationBySubLedgerId(Id);
            await ledgerService.DeleteledgerById(Id);
            return Json(true);
        }

        #region API Section 
        [Route("global/api/GetLedgerRelationBySubLedgerId/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetLedgerRelationBySubLedgerId(int Id)
        {
            return Json(await ledgerService.GetLedgerRelationBySubLedgerId(Id));
        }
        
        #endregion
    }
}