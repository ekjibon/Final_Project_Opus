using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Budget.Models;
using OPUSERP.Areas.Budget.Models.Lang;
using OPUSERP.Areas.SCMMatrix.Models;
using OPUSERP.Budget.Data.Entity;
using OPUSERP.Budget.Service.Interface;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.SCM.Services.Matrix.Interfaces;

namespace OPUSERP.Areas.Budget.Controllers
{
    [Authorize]
    [Area("Budget")]
    public class ReviseRequisitionController : Controller
    {
        private readonly LangGenerate<BudgetRequisitionLn> _lang;
        private readonly LangGenerate<BudgetRequisitionExcelLn> _lang1;
        private readonly IBudgetRequsitionMasterService budgetRequsitionMasterService;
        private readonly IBudgetHeadService budgetHeadService;
        private readonly IUserInfoes userInfo;

        private readonly IApprovalLogService approvalLogService;
        private readonly IApprovalMatrixService approvalMatrixService;

        public ReviseRequisitionController(IHostingEnvironment hostingEnvironment, IBudgetRequsitionMasterService budgetRequsitionMasterService, IUserInfoes userInfo, IBudgetHeadService budgetHeadService, IApprovalLogService approvalLogService, IApprovalMatrixService approvalMatrixService)
        {
            _lang = new LangGenerate<BudgetRequisitionLn>(hostingEnvironment.ContentRootPath);
            _lang1 = new LangGenerate<BudgetRequisitionExcelLn>(hostingEnvironment.ContentRootPath);
            this.budgetRequsitionMasterService = budgetRequsitionMasterService;
            this.budgetHeadService = budgetHeadService;
            this.userInfo = userInfo;

            this.approvalLogService = approvalLogService;
            this.approvalMatrixService = approvalMatrixService;
        }

        public async Task<IActionResult> Index(int? id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            var plan = await budgetRequsitionMasterService.GetBudgetRequsitionMaster();
            string productionNo = ("Revise-BR/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + (plan.Count() + 1)).ToString();
            IEnumerable<BudgetRequsitionDetail> details = new List<BudgetRequsitionDetail>();
            BudgetRequsitionMaster masterInfoes = new BudgetRequsitionMaster();
            FiscalYear fiscalYear = new FiscalYear();
            if (id > 0)
            {
                var master = await budgetRequsitionMasterService.GetBudgetRequsitionMasterById(Convert.ToInt32(id));
                masterInfoes.Id = master.Id;
                masterInfoes.requsitionNo = master.requsitionNo;
                masterInfoes.budgetBranchId = master.budgetBranchId;
                masterInfoes.requsitionDate = master.requsitionDate;
                masterInfoes.fiscalYearId = master.fiscalYearId;
                masterInfoes.status = master.status;
                details = await budgetRequsitionMasterService.GetBudgetRequsitionDetailBymasterId(Convert.ToInt32(id));
                fiscalYear = await budgetRequsitionMasterService.GetFiscalYearById(Convert.ToInt32(masterInfoes.fiscalYearId));
            }
            else
            {
                masterInfoes.Id = 0;
                masterInfoes.requsitionNo = productionNo;
                masterInfoes.requsitionDate = DateTime.Now;
                details = new List<BudgetRequsitionDetail>();
                fiscalYear = new FiscalYear();
            }

            BudgetRequisitionViewModel model = new BudgetRequisitionViewModel
            {
                flang = _lang.PerseLang("Budget/BudgetRequisitionEN.json", "Budget/BudgetRequisitionBN.json", Request.Cookies["lang"]),
                reqId = masterInfoes.Id,
                Number = masterInfoes.requsitionNo,
                Date = masterInfoes.requsitionDate,
                year = masterInfoes.fiscalYearId,
                fiscalYears = await budgetRequsitionMasterService.GetFiscalYear(),
                budgetRequsitionDetails = details,
                fiscalYear = fiscalYear,
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Index([FromForm] BudgetRequisitionViewModel model)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            var plan = await budgetRequsitionMasterService.GetBudgetRequsitionMaster();
            string productionNo = ("Revise-BR/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + (plan.Count() + 1)).ToString();

            if (model.reqId > 0)
            {
                productionNo = model.Number;
            }

            if (model.heads == null)
            {
                ModelState.AddModelError(string.Empty, "Have to Add minimum 1 Budget Head");
                model.reqId = 0;
                model.Number = productionNo;
                model.Date = DateTime.Now;
                return View(model);
            }
            BudgetRequsitionMaster master = new BudgetRequsitionMaster
            {
                Id = Convert.ToInt32(model.reqId),
                requsitionNo = productionNo,
                requsitionDate = model.Date,
                fiscalYearId = model.year,
                budgetBranchId = userInfos.projectId,
                status = 1,
                RequsitionBy = userInfos.UserId,
                grandTotal = model.amounts.Sum(),
                type = 1 //1 for revise budget;
            };
            int masterId = await budgetRequsitionMasterService.SaveBudgetRequsitionMaster(master);

            if (model.reqId > 0)
            {
                await budgetRequsitionMasterService.DeleteBudgetRequsitionDetailBymasterId(Convert.ToInt32(model.reqId));
            }
            for (int i = 0; i < model.heads.Length; i++)
            {
                BudgetRequsitionDetail details = new BudgetRequsitionDetail
                {
                    Id = 0,
                    budgetRequsitionMasterId = masterId,
                    budgetHeadId = model.heads[i],
                    firstMonth = model.col1[i],
                    secondMonth = model.col2[i],
                    thirdMonth = model.col3[i],
                    fourthMonth = model.col4[i],
                    fifthMonth = model.col5[i],
                    sixthMonth = model.col6[i],
                    seventhMonth = model.col7[i],
                    eighthMonth = model.col8[i],
                    ninethMonth = model.col9[i],
                    tenthMonth = model.col10[i],
                    eleventhMonth = model.col11[i],
                    twelvethMonth = model.col12[i],
                    subTotal = model.amounts[i]
                };
                await budgetRequsitionMasterService.SaveBudgetRequsitionDetail(details);
            }

            IEnumerable<ApproverPanelViewModel> lstApproverPanel = await approvalMatrixService.GetPRApproverPanelList(userName, 1, 1);
            if (model.reqId == 0 && lstApproverPanel.Count() > 0)
            {
                int i = 0;
                string notes = "";
                foreach (ApproverPanelViewModel panels in lstApproverPanel)
                {
                    int isactive = 0;
                    int? nextAppId = 0;
                    if (i == 1)
                    {
                        isactive = 1;
                        nextAppId = 0;
                        notes = "";
                    }
                    else if (i == 0)
                    {
                        isactive = 0;
                        nextAppId = panels.nextApproverId;
                        notes = "Created";
                    }
                    else
                    {
                        isactive = 0;
                        nextAppId = 0;
                        notes = "";
                    }
                    ApprovalLog appLog = new ApprovalLog
                    {
                        masterId = masterId,
                        matrixTypeId = 1,
                        userId = Convert.ToInt32(panels.nextApproverId),
                        sequenceNo = Convert.ToInt32(panels.sequenceNo),
                        isActive = isactive,
                        notes = notes,
                        nextApproverId = nextAppId
                    };
                    i = i + 1;
                    await approvalLogService.SaveApproverLog(appLog);
                }
            }

            return RedirectToAction(nameof(BudgetRequisitionList));
        }

        public async Task<IActionResult> BudgetRequisitionList()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            BudgetRequisitionViewModel model = new BudgetRequisitionViewModel
            {
                budgetRequsitionMasters = await budgetRequsitionMasterService.GetBudgetRequsitionMasterByBranchIdAndType((int)userInfos.projectId,1)
            };
            return View(model);
        }
    }
}