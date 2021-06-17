using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Budget.Models;
using OPUSERP.Areas.Budget.Models.Lang;
using OPUSERP.Budget.Data.Entity;
using OPUSERP.Budget.Service.Interface;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.SCM.Helpers;
using OPUSERP.SCM.Services.Matrix.Interfaces;

namespace OPUSERP.Areas.Budget.Controllers
{
    [Authorize]
    [Area("Budget")]
    public class HOBudgetApprovalController : Controller
    {
        private readonly LangGenerate<BudgetRequisitionLn> _lang;
        private readonly LangGenerate<BudgetRequisitionExcelLn> _lang1;
        private readonly IBudgetRequsitionMasterService budgetRequsitionMasterService;
        private readonly IHOBudgetRequsitionService hOBudgetRequsitionService;
        private readonly IBudgetHeadService budgetHeadService;
        private readonly IUserInfoes userInfo;
        private readonly IApprovalLogService approvalLogService;
        private readonly RequisitionStatusHistory requisitionStatusHistory;

        public HOBudgetApprovalController(IHostingEnvironment hostingEnvironment, IBudgetRequsitionMasterService budgetRequsitionMasterService, IUserInfoes userInfo, IBudgetHeadService budgetHeadService, RequisitionStatusHistory requisitionStatusHistory, IApprovalLogService approvalLogService, IHOBudgetRequsitionService hOBudgetRequsitionService)
        {
            _lang = new LangGenerate<BudgetRequisitionLn>(hostingEnvironment.ContentRootPath);
            _lang1 = new LangGenerate<BudgetRequisitionExcelLn>(hostingEnvironment.ContentRootPath);
            this.budgetRequsitionMasterService = budgetRequsitionMasterService;
            this.budgetHeadService = budgetHeadService;
            this.userInfo = userInfo;
            this.approvalLogService = approvalLogService;
            this.requisitionStatusHistory = requisitionStatusHistory;
            this.hOBudgetRequsitionService = hOBudgetRequsitionService;
        }

        public async Task<IActionResult> BudgetApprovalList()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            HOBudgetRequsitionViewModel model = new HOBudgetRequsitionViewModel
            {
                budgetRequisitionApprovedListViewModels = await hOBudgetRequsitionService.GetBudgetRequsitionMasterForApproval(userInfos.UserId)
            };
            return View(model);
        }

        public async Task<IActionResult> Index(int? id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);


            HOBudgetRequsitionMaster masterInfoes = new HOBudgetRequsitionMaster();
            FiscalYear fiscalYear = new FiscalYear();
            if (id > 0)
            {
                var master = await hOBudgetRequsitionService.GetBudgetRequsitionMasterById(Convert.ToInt32(id)); 
                masterInfoes.Id = master.Id;
                masterInfoes.requsitionNo = master.requsitionNo;
                masterInfoes.requsitionDate = master.requsitionDate;
                masterInfoes.fiscalYearId = master.fiscalYearId;
                masterInfoes.status = master.status;
                
                fiscalYear = await budgetRequsitionMasterService.GetFiscalYearById(Convert.ToInt32(masterInfoes.fiscalYearId));
            }


            HOBudgetRequsitionViewModel model = new HOBudgetRequsitionViewModel
            {
                flang = _lang.PerseLang("Budget/BudgetRequisitionEN.json", "Budget/BudgetRequisitionBN.json", Request.Cookies["lang"]),
                reqId = masterInfoes.Id,
                Number = masterInfoes.requsitionNo,
                Date = masterInfoes.requsitionDate,
                year = masterInfoes.fiscalYearId,
                fiscalYears = await budgetRequsitionMasterService.GetFiscalYear(),
                hOBudgetRequsitionDetails = await hOBudgetRequsitionService.GetBudgetRequsitionDetailBymasterId(Convert.ToInt32(id)),
                fiscalYear = fiscalYear,
                approerPanelList = await approvalLogService.GetApprovedListByApprovar(userInfos.UserId, Convert.ToInt32(id), 2),
            };
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> SaveBudgetApproval(int reqId, string reqNo, string appType, string remark)
        {
            try
            {
                string actionNo = reqNo;
                string CCID = string.Empty;
                string _empCode = string.Empty;
                string mailTo = string.Empty;
                string userName = HttpContext.User.Identity.Name;
                var currUserInfo = await userInfo.GetUserInfoByUser(userName);
                var currentStatus = await approvalLogService.GetNextApproverInfo(userName, reqId, 2);
                int statusId = 0;

                if (appType == "5")
                {
                    await approvalLogService.DeleteApproverLogByMatrixTypeId(1, reqId);
                    statusId = 5;
                }
                else if (appType == "4")
                {
                    await approvalLogService.DeleteApproverLogByMatrixTypeId(1, reqId);
                    statusId = 4;
                }
                else
                {
                    if (currentStatus != null)
                    {
                        var userInfo = await approvalLogService.UpdateApprovalLogStatus(userName, reqId, 2, remark);
                        statusId = 2;
                    }
                    else
                    {
                        statusId = 3;
                    }
                }
                hOBudgetRequsitionService.UpdateBudgetRequsitionStatusById(reqId, statusId);
                
                return Json(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}