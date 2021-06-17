using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Budget.Models;
using OPUSERP.Budget.Data.Entity;
using OPUSERP.Budget.Service.Interface;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.Budget.Controllers
{
    [Authorize]
    [Area("Budget")]
    public class DisbursementController : Controller
    {
        private readonly IBudgetRequsitionMasterService budgetRequsitionMasterService;
        private readonly IUserInfoes userInfo;
        private readonly ISpecialBranchUnitService specialBranchUnitService;
        private readonly IBudgetDisbursmentMasterService budgetDisbursmentMasterService;

        public DisbursementController(IHostingEnvironment hostingEnvironment, IBudgetRequsitionMasterService budgetRequsitionMasterService, IUserInfoes userInfo, ISpecialBranchUnitService specialBranchUnitService, IBudgetDisbursmentMasterService budgetDisbursmentMasterService)
        {
            this.budgetRequsitionMasterService = budgetRequsitionMasterService;
            this.userInfo = userInfo;
            this.specialBranchUnitService = specialBranchUnitService;
            this.budgetDisbursmentMasterService = budgetDisbursmentMasterService;
        }

        public async Task<IActionResult> Index(int? id)
        {
            string userName = User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            ViewBag.sbuId = userInfos?.specialBranchUnitId;
            var plan = await budgetDisbursmentMasterService.GetBudgetDisbursementMaster();
            string productionNo = ("Disburse-BR/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + (plan.Count() + 1)).ToString();
            BudgetDisbursementMaster budgetDisbursementMaster = new BudgetDisbursementMaster();
            if (id > 0)
            {
                var master = await budgetDisbursmentMasterService.GetBudgetDisbursementMasterById((int)id);
                budgetDisbursementMaster.Id = master.Id;
                budgetDisbursementMaster.disburseNo = master.disburseNo;
                budgetDisbursementMaster.disburseDate = master.disburseDate;
                budgetDisbursementMaster.fiscalYearId = master.fiscalYearId;
                budgetDisbursementMaster.budgetBranchId = master.budgetBranchId;
            }
            else
            {
                budgetDisbursementMaster.Id = 0;
                budgetDisbursementMaster.disburseNo = productionNo;
                budgetDisbursementMaster.disburseDate = DateTime.Now;
                budgetDisbursementMaster.fiscalYearId = 0;
                budgetDisbursementMaster.budgetBranchId = 0;
            }

            DisbursmentViewModel model = new DisbursmentViewModel
            {
                fiscalYears = await budgetRequsitionMasterService.GetFiscalYear(),
                specialBranchUnits = await specialBranchUnitService.GetSpecialBranchUnit(),
                number = productionNo,
                date = budgetDisbursementMaster.disburseDate,
                Id = budgetDisbursementMaster.Id,
                year = budgetDisbursementMaster.fiscalYearId,
                branch = budgetDisbursementMaster.budgetBranchId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] DisbursmentViewModel model)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            var plan = await budgetDisbursmentMasterService.GetBudgetDisbursementMaster();
            string productionNo = ("Disburse-BR/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + (plan.Count() + 1)).ToString();

            if (model.Id > 0)
            {
                productionNo = model.number;
            }

            if (model.headIdAll == null)
            {
                ModelState.AddModelError(string.Empty, "Have to Add minimum 1 Head Disburse");
                model.Id = 0;
                model.number = productionNo;
                model.date = DateTime.Now;
                return View(model);
            }
            decimal? sum = 0;
            if (model.amountAll!=null)
            {
                foreach(var data in model.amountAll)
                {
                    sum = sum + data;
                }
            }
            BudgetDisbursementMaster master = new BudgetDisbursementMaster
            {
                Id = Convert.ToInt32(model.Id),
                disburseNo = productionNo,
                disburseDate = model.date,
                fiscalYearId = model.year,
                budgetBranchId = model.branch,
                grandTotal = sum,
            };
            int masterId = await budgetDisbursmentMasterService.SaveBudgetDisbursementMaster(master);

            if (model.Id > 0)
            {
                await budgetDisbursmentMasterService.DeleteBudgetDisbursementDetailBymasterId(Convert.ToInt32(model.Id));
            }
            for (int i = 0; i < model.headIdAll.Length; i++)
            {
                BudgetDisbursementDetail details = new BudgetDisbursementDetail
                {
                    Id = 0,
                    budgetDisbursementMasterId = masterId,
                    budgetHeadId = model.headIdAll[i],
                    amount = model.amountAll[i]
                };
                await budgetDisbursmentMasterService.SaveBudgetDisbursementDetail(details);
            }

            return RedirectToAction(nameof(BudgetDisbursmentList));
        }

        public async Task<IActionResult> BudgetDisbursmentList()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            DisbursmentViewModel model = new DisbursmentViewModel
            {
                budgetDisbursementMasters = await budgetDisbursmentMasterService.GetBudgetDisbursementMaster()
            };
            return View(model);
        }

        [Route("global/api/GetBudgetDisbursementDetailBymasterId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetBudgetDisbursementDetailBymasterId(int id)
        {
            return Json(await budgetDisbursmentMasterService.GetBudgetDisbursementDetailBymasterId(id));
        }
    }
}