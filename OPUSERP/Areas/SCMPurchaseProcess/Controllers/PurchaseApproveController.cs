using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.SCMPurchaseProcess.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.SCM.Helpers;
using OPUSERP.SCM.Services.Matrix.Interfaces;
using OPUSERP.SCM.Services.PurchaseProcess.Interfaces;
using OPUSERP.SCM.Services.Requisition.Interfaces;
using OPUSERP.SCM.Services.SCMMail.interfaces;

namespace OPUSERP.Areas.SCMPurchaseProcess.Controllers
{
    [Area("SCMPurchaseProcess")]
    public class PurchaseApproveController : Controller
    {
        private readonly IPurchaseProcessService purchaseProcessService;
        private readonly IProcurementService procurementService;
        private readonly IRequisitionService requisitionService;
        private readonly RequisitionStatusHistory requisitionStatusHistory;
        private readonly IUserInfoes userInfoes;
        private readonly IApprovalLogService approverLogService;
        private readonly ISCMMailService sCMMailService;
        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public PurchaseApproveController(IPurchaseProcessService purchaseProcessService, RequisitionStatusHistory requisitionStatusHistory, IUserInfoes userInfoes, IRequisitionService requisitionService, IProcurementService procurementService, IApprovalLogService approverLogService, ISCMMailService sCMMailService, IHostingEnvironment hostingEnvironment, IConverter converter)
        {
            this.purchaseProcessService = purchaseProcessService;
            this.procurementService = procurementService;
            this.requisitionService = requisitionService;
            this.requisitionStatusHistory = requisitionStatusHistory;
            this.userInfoes = userInfoes;
            this.approverLogService = approverLogService;
            this.sCMMailService = sCMMailService;
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        public async Task<IActionResult> Index()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfo = await userInfoes.GetUserInfoByUser(userName);
            var model = new CSRequisitionList
            {
                cSMasters = await purchaseProcessService.GetCSListForApprove(userInfo.UserId),
            };
            return View(model);
        }

        public async Task<IActionResult> CSApprove(int csId)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfo = await userInfoes.GetUserInfoByUser(userName);
            ViewBag.CSID = csId;
            var model = new CSRequisitionList
            {
                cSMaster = await purchaseProcessService.GetCSMasterById(csId),
                quotationDetailsVMs=await purchaseProcessService.GetQuotationDetailView(csId),
                cSDetailsVMs=await purchaseProcessService.GetCSDetailView(csId),
                justifications = await procurementService.GetJustificationListByCSMasterId(csId),
                approverPanel=await approverLogService.GetNextApproverInfo(userName,csId,2),
                approerPanelList=await approverLogService.GetApprovedListByApprovar(userInfo.UserId, csId,2),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> SaveCSApprove(int csId, string csNo, string appRemark, string approveType)
        {
            try
            {
                string actionNo = csNo;//reqNo
                string CCID = string.Empty;
                string _empCode = string.Empty;
                string mailTo = string.Empty;
                string userName = HttpContext.User.Identity.Name;
                var currUserInfo = await userInfoes.GetUserInfoByUser(userName);
                var currentStatus = await approverLogService.GetNextApproverInfo(userName, csId, 2);
                int statusId = 0;
                int logStatusId = 0;
                
                if (approveType == "-1")
                {
                    await approverLogService.DeleteApproverLogByMatrixTypeId(2,csId);
                    statusId = -1;
                    logStatusId = 13;
                }
                else if (approveType == "4")
                {
                    await approverLogService.DeleteApproverLogByMatrixTypeId(2, csId);
                    statusId = 4;
                    logStatusId = 12;
                }
                else
                {
                    if (currentStatus != null)
                    {
                        var userInfo = await approverLogService.UpdateApprovalLogStatus(userName, csId, 2, appRemark);
                        statusId = 2;
                        logStatusId = 10;
                    }
                    else
                    {
                        var userInfo = await approverLogService.UpdateApprovalLogStatus(userName, csId, 2, appRemark);
                        statusId = 3;
                        logStatusId = 11;
                    }                   
                }
                purchaseProcessService.UpdateCSMaster(csId, statusId);

                var cSMaster = await purchaseProcessService.GetCSMasterById(csId);
                string empNameCode = currUserInfo.EmpCode + "-" + currUserInfo.EmpName;
                string nextEmpNameCode = "";
                string nextEmail = string.Empty;
                if (logStatusId != 11)
                {
                    nextEmpNameCode = currentStatus.EmpCode + "-" + currentStatus.EmpName;
                }
                else
                {
                    var nextUserInfo = await userInfoes.GetUserInfoByUserId(cSMaster.userId);
                    nextEmpNameCode = nextUserInfo.EmpCode + "-" + nextUserInfo.EmpName;
                    nextEmail = nextUserInfo.Email;
                }
                await requisitionStatusHistory.SaveRequisitionStatusLog(Convert.ToInt32(cSMaster.requisitionId), 2, Convert.ToInt32(currUserInfo.UserTypeId), currUserInfo.UserId, empNameCode, nextEmpNameCode, appRemark, logStatusId, "CS", csId, actionNo);

                string host = HttpContext.Request.Host.ToString();
                string scheme = Request.Scheme;
                string baseUrl = $"" + scheme + "://" + host + "/Auth/Account/Login";
                //await sCMMailService.MailMessage(nextEmail, csNo, logStatusId, empNameCode, baseUrl);
                //await sCMMailService.MailMessage("suzauddaula103@gmail.com", csNo, logStatusId, empNameCode, baseUrl);
                //RedirectToAction(nameof(ReqApprovelist));
                return Json(true);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetSupplierWiseItemDetails(int reqDetailID)
        {
            return Json(await purchaseProcessService.GetSupplierWiseItemDetails(reqDetailID));
        }
        [HttpPost]
        public async Task<JsonResult> UpdateCSDetailApproval([FromForm] CSRequisitionList model)
        {
            if (model.csEId != null)
            {
                if (model.csEId.Length > 0)
                {

                    for (int i = 0; i < model.csEId.Length; i++)
                    {

                        await purchaseProcessService.UpdateCSDetailApproval(Convert.ToInt32(model.csEId[i]), model.csEqty[i], model.csErate[i]);


                    }

                }
            }


            return Json(1);

        }

    }
}