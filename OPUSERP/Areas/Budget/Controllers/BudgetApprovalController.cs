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
    public class BudgetApprovalController : Controller
    {
        private readonly LangGenerate<BudgetRequisitionLn> _lang;
        private readonly LangGenerate<BudgetRequisitionExcelLn> _lang1;
        private readonly IBudgetRequsitionMasterService budgetRequsitionMasterService;
        private readonly IBudgetHeadService budgetHeadService;
        private readonly IUserInfoes userInfo;
        private readonly IApprovalLogService approvalLogService;
        private readonly RequisitionStatusHistory requisitionStatusHistory;

        public BudgetApprovalController(IHostingEnvironment hostingEnvironment, IBudgetRequsitionMasterService budgetRequsitionMasterService, IUserInfoes userInfo, IBudgetHeadService budgetHeadService, RequisitionStatusHistory requisitionStatusHistory, IApprovalLogService approvalLogService)
        {
            _lang = new LangGenerate<BudgetRequisitionLn>(hostingEnvironment.ContentRootPath);
            _lang1 = new LangGenerate<BudgetRequisitionExcelLn>(hostingEnvironment.ContentRootPath);
            this.budgetRequsitionMasterService = budgetRequsitionMasterService;
            this.budgetHeadService = budgetHeadService;
            this.userInfo = userInfo;
            this.approvalLogService = approvalLogService;
            this.requisitionStatusHistory = requisitionStatusHistory;
        }

        public async Task<IActionResult> BudgetApprovalList()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            BudgetRequisitionViewModel model = new BudgetRequisitionViewModel
            {
                budgetRequisitionApprovedListViewModels = await budgetRequsitionMasterService.GetBudgetRequsitionMasterForApproval(userInfos.UserId)
            };
            return View(model);
        }
        public async Task<IActionResult> BudgetApprovalListFin()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            BudgetRequisitionViewModel model = new BudgetRequisitionViewModel
            {
                budgetRequisitionApprovedListViewModels = await budgetRequsitionMasterService.GetBudgetRequsitionMasterFinForApproval(userInfos.UserId)
            };
            return View(model);
        }

        public async Task<IActionResult> Index(int? id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            var plan = await budgetRequsitionMasterService.GetBudgetRequsitionMaster();

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
                approerPanelList = await approvalLogService.GetApprovedListByApprovar(userInfos.UserId, Convert.ToInt32(id), 1),
            };
            return View(model);
        }

        public async Task<IActionResult> IndexFin(int? id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            var plan = await budgetRequsitionMasterService.GetBudgetRequsitionMasterFin();

            IEnumerable<BudgetRequsitionDetailFin> details = new List<BudgetRequsitionDetailFin>();
            BudgetRequsitionMasterFin masterInfoes = new BudgetRequsitionMasterFin();
            FiscalYear fiscalYear = new FiscalYear();
            if (id > 0)
            {
                var master = await budgetRequsitionMasterService.GetBudgetRequsitionMasterFinById(Convert.ToInt32(id));
                masterInfoes.Id = master.Id;
                masterInfoes.requsitionNo = master.requsitionNo;
                masterInfoes.budgetBranchId = master.budgetBranchId;
                masterInfoes.requsitionDate = master.requsitionDate;
                masterInfoes.fiscalYearId = master.fiscalYearId;
                masterInfoes.status = master.status;
                details = await budgetRequsitionMasterService.GetBudgetRequsitionDetailFinBymasterId(Convert.ToInt32(id));
                fiscalYear = await budgetRequsitionMasterService.GetFiscalYearById(Convert.ToInt32(masterInfoes.fiscalYearId));
            }


            BudgetRequisitionViewModel model = new BudgetRequisitionViewModel
            {
                flang = _lang.PerseLang("Budget/BudgetRequisitionEN.json", "Budget/BudgetRequisitionBN.json", Request.Cookies["lang"]),
                reqId = masterInfoes.Id,
                Number = masterInfoes.requsitionNo,
                Date = masterInfoes.requsitionDate,
                year = masterInfoes.fiscalYearId,
                fiscalYears = await budgetRequsitionMasterService.GetFiscalYear(),
                budgetRequsitionDetailFins = details,
                fiscalYear = fiscalYear,
                approerPanelList = await approvalLogService.GetApprovedListByApprovar(userInfos.UserId, Convert.ToInt32(id), 9),
            };
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> SaveBudgetApproval(int reqId, string reqNo, string appType, string remark)
        {
            try
            {
                string actionNo = reqNo;//reqNo
                string CCID = string.Empty;
                string _empCode = string.Empty;
                string mailTo = string.Empty;
                string userName = HttpContext.User.Identity.Name;
                var currUserInfo = await userInfo.GetUserInfoByUser(userName);
                var currentStatus = await approvalLogService.GetNextApproverInfo(userName, reqId, 1);
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
                        var userInfo = await approvalLogService.UpdateApprovalLogStatus(userName, reqId, 1, remark);
                        statusId = 2;
                    }
                    else
                    {
                        statusId = 3;
                    }
                }
                budgetRequsitionMasterService.UpdateBudgetRequsitionStatusById(reqId, statusId);

                //string empNameCode = currUserInfo.EmpCode + "-" + currUserInfo.EmpName;
                //string nextEmpNameCode = "";
                //if (statusId != 3)
                //{
                //    nextEmpNameCode = currentStatus.EmpCode + "-" + currentStatus.EmpName;

                //    string host = HttpContext.Request.Host.ToString();
                //    string scheme = Request.Scheme;
                //    string baseUrl = $"" + scheme + "://" + host + "/Auth/Account/Login";
                //await sCMMailService.MailMessage(currentStatus.EmailID, reqNo, 2, empNameCode, baseUrl);
                //await sCMMailService.MailMessage("suzauddaula103@gmail.com", reqNo, 2, empNameCode, baseUrl);
                //}

                //await requisitionStatusHistory.SaveRequisitionStatusLog(reqId, 1, Convert.ToInt32(currUserInfo.UserTypeId), currUserInfo.UserId, empNameCode, nextEmpNameCode, remark, statusId, "Budget", reqId, actionNo);



                return Json(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<JsonResult> SaveBudgetApprovalFin(int reqId, string reqNo, string appType, string remark)
        {
            try
            {
                string actionNo = reqNo;//reqNo
                string CCID = string.Empty;
                string _empCode = string.Empty;
                string mailTo = string.Empty;
                string userName = HttpContext.User.Identity.Name;
                var currUserInfo = await userInfo.GetUserInfoByUser(userName);
                var currentStatus = await approvalLogService.GetNextApproverInfo(userName, reqId, 9);
                int statusId = 0;

                if (appType == "5")
                {
                    await approvalLogService.DeleteApproverLogByMatrixTypeId(9, reqId);
                    statusId = 5;
                }
                else if (appType == "4")
                {
                    await approvalLogService.DeleteApproverLogByMatrixTypeId(9, reqId);
                    statusId = 4;
                }
                else
                {
                    if (currentStatus != null)
                    {
                        var userInfo = await approvalLogService.UpdateApprovalLogStatus(userName, reqId, 9, remark);
                        statusId = 2;
                    }
                    else
                    {
                        statusId = 3;
                    }
                }
                budgetRequsitionMasterService.UpdateBudgetRequsitionStatusFinById(reqId, statusId);

                //string empNameCode = currUserInfo.EmpCode + "-" + currUserInfo.EmpName;
                //string nextEmpNameCode = "";
                //if (statusId != 3)
                //{
                //    nextEmpNameCode = currentStatus.EmpCode + "-" + currentStatus.EmpName;

                //    string host = HttpContext.Request.Host.ToString();
                //    string scheme = Request.Scheme;
                //    string baseUrl = $"" + scheme + "://" + host + "/Auth/Account/Login";
                //await sCMMailService.MailMessage(currentStatus.EmailID, reqNo, 2, empNameCode, baseUrl);
                //await sCMMailService.MailMessage("suzauddaula103@gmail.com", reqNo, 2, empNameCode, baseUrl);
                //}

                //await requisitionStatusHistory.SaveRequisitionStatusLog(reqId, 1, Convert.ToInt32(currUserInfo.UserTypeId), currUserInfo.UserId, empNameCode, nextEmpNameCode, remark, statusId, "Budget", reqId, actionNo);



                return Json(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}