using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.SCMRequisition.Models;
using OPUSERP.Areas.SCMRequisition.Models.Lang;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.SCM.Helpers;
using OPUSERP.SCM.Services.Matrix.Interfaces;
using OPUSERP.SCM.Services.Requisition.Interfaces;
using OPUSERP.SCM.Services.SCMMail.interfaces;

namespace OPUSERP.Areas.SCMRequisition.Controllers
{
    [Area("SCMRequisition")]
    public class RequisitionApproveController : Controller
    {
        private readonly IRequisitionService requisitionService;
        private readonly IApprovalMatrixService approvalMatrixService;
        private readonly IUserInfoes userInfoes;
        private readonly IApprovalLogService approvalLogService;
        private readonly RequisitionStatusHistory requisitionStatusHistory;
        private readonly LangGenerate<RequisitionApproveLN> _lang;
        private readonly ISCMMailService sCMMailService;
        private readonly IStatusLogService statusLogService;
        public RequisitionApproveController(IHostingEnvironment hostingEnvironment, RequisitionStatusHistory requisitionStatusHistory
            , IRequisitionService requisitionService, IStatusLogService statusLogService, IApprovalMatrixService approvalMatrixService, IUserInfoes userInfoes, IApprovalLogService approvalLogService, ISCMMailService sCMMailService)
        {
            this.approvalLogService = approvalLogService;
            this.requisitionService = requisitionService;
            this.requisitionStatusHistory = requisitionStatusHistory;
            this.userInfoes = userInfoes;
            this.statusLogService = statusLogService;
            this.approvalMatrixService = approvalMatrixService;
            this.sCMMailService = sCMMailService;
            _lang = new LangGenerate<RequisitionApproveLN>(hostingEnvironment.ContentRootPath);
        }

        #region Requisition Approve List
        public async Task<IActionResult> ReqApprovelist()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfo = await userInfoes.GetUserInfoByUser(userName);
            ApprovalLogViewModel model = new ApprovalLogViewModel
            {
                getRequisitionListForApprovedViewModels = await requisitionService.GetRequisitionApproveList(userInfo.UserId),
                fLang = _lang.PerseLang("Requisition/RequisitionApproveEN.json", "Requisition/RequisitionApproveBN.json", Request.Cookies["lang"]),
            };
            if (model.approvalLog == null) model.approvalLog = new ApprovalLog();
            return View(model);
        }
        #endregion

        #region Requisition Approve
        public async Task<IActionResult> RequisitionApprove(string id)
        {
            if (id == null || id == "")
            {
                ViewBag.reqMasterId = "0";
            }
            else
            {
                ViewBag.reqMasterId = id.ToString();
            }
            string userName = HttpContext.User.Identity.Name;
            var userInfo = await userInfoes.GetUserInfoByUser(userName);
            ViewBag.ApproverName = userInfo.EmpName;
            ViewBag.DepartmentName = userInfo.DivisionName;
            ViewBag.FinancialValue = userInfo.FinancialValue;
            var data = await approvalMatrixService.GetPRApproverPanelFListFromApprovalLogs(userName, 0, Convert.ToInt32(id));
           var  dataxx = data.Where(x => x.isActive == 1).ToList();
            int? secuence = 0;
            if (dataxx.Count()>0)
            {
               secuence = dataxx.LastOrDefault().sequenceNo;
            }

            data = data.Where(x => x.sequenceNo < secuence);

            var datax = await statusLogService.GetStatusLogListbyreqsid(Convert.ToInt32(id));
            if (datax != null)
            {
                ViewBag.remarks = datax.OrderByDescending(x => x.Id).FirstOrDefault().remarks;
            }
            var model = new ApprovalLogViewModel()
            {
                //requisitionDetailsList = await requisitionService.GetItemListByRequisitionId(Convert.ToInt32(id)),
                requisitionDetailVMs=await requisitionService.GetAllRequisitionDetailListByReqId(Convert.ToInt32(id)),
                approerPanelList = await approvalLogService.GetApprovedListByApprovar(userInfo.UserId, Convert.ToInt32(id), 1),
                approverPanelFViewModels=data,
                documentAttachmentDetails=await requisitionService.GetDocumentAttachmentDetailListreqid(Convert.ToInt32(id))
            };
            if (model.approvalLog == null) model.approvalLog = new ApprovalLog();
            return View(model);
        }
        [AllowAnonymous] 
        public async Task<IActionResult> RequisitionApprove2()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> SaveRequisitionApprovalLog(int reqId, string reqNo, string appType, string remark,int?appId)
        {
            try
            {
                string actionNo = reqNo;//reqNo
                string CCID = string.Empty;
                string _empCode = string.Empty;
                string mailTo = string.Empty;
                string userName = HttpContext.User.Identity.Name;
                //var nextUserInfo = await approvalLogService.GetNextApproverInfo(userName, reqId, 1);
                var currUserInfo = await userInfoes.GetUserInfoByUser(userName);
                var currentStatus = await approvalLogService.GetNextApproverInfo(userName, reqId, 1);
                int statusId = 0;
                
                if (appType == "5")
                {
                    await approvalLogService.DeleteApproverLogByMatrixTypeId(1, reqId);
                    statusId = 5;
                }
                else if (appType == "4")
                {
                    var appdata = await approvalLogService.GetApproverLogById((int)appId);
                    if (appdata.sequenceNo == 0)
                    {
                        await approvalLogService.DeleteApproverLogByMatrixTypeId(1, reqId);
                        statusId = 4;
                    }
                    else
                    {
                        var userInfo = await approvalLogService.UpdateApproverReLog(appId, userName);
                        statusId = 2;
                    }
                   
                   
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
                requisitionService.UpdateRequisitionMasterStatusById(reqId, statusId);

                string empNameCode = currUserInfo.EmpCode + "-" + currUserInfo.EmpName;
                string nextEmpNameCode = "";
                if (statusId != 3)
                {
                    if(currentStatus!=null)
                    {
                        nextEmpNameCode = currentStatus.EmpCode + "-" + currentStatus.EmpName;
                    }
                   

                    string host = HttpContext.Request.Host.ToString();
                    string scheme = Request.Scheme;
                    string baseUrl = $"" + scheme + "://" + host + "/Auth/Account/Login";
                    //await sCMMailService.MailMessage(currentStatus.EmailID, reqNo, 2, empNameCode, baseUrl);
                    //await sCMMailService.MailMessage("suzauddaula103@gmail.com", reqNo, 2, empNameCode, baseUrl);
                }
                
                await requisitionStatusHistory.SaveRequisitionStatusLog(reqId, 1, Convert.ToInt32(currUserInfo.UserTypeId), currUserInfo.UserId, empNameCode, nextEmpNameCode, remark, statusId, "PR", reqId, actionNo);

                

                return Json(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAttachmentListByMatrixType(string masterId)
        {
            var Re_DetailList = await requisitionService.GetAttachmentListByMatrixType(Convert.ToInt32(masterId), 1);
            return Json(Re_DetailList);
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> GetAllRequisitionDetailListByReqId(int reqId)
        {
            var data = await requisitionService.GetAllRequisitionDetailListByReqId(reqId);
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetRequisitionMasterById(int reqId)
        {
            var data = await requisitionService.GetRequisitionMasterById(reqId);
            return Json(data);
        }

        #region API
        [Route("global/api/GetApprovalLogByRequisitionId/{reqId}")]
        [HttpGet]
        public async Task<IActionResult> GetApprovalLogByRequisitionId(int reqId)
        {
            return Json(await requisitionService.GetApprovalLogByRequisitionId(reqId));
        }
        [Route("global/api/GetRequisitionbyuserId/{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetRequisitionbyuserId(int Id)
        {
           
            return Json(await requisitionService.GetRequisitionApproveList(Id));
        }

        [Route("global/api/GetAttachmentListByMatrixType/{masterId}/{matrixTypeId}")]
        [HttpGet]
        public async Task<JsonResult> GetAttachmentListByMatrixType(string masterId, int matrixTypeId)
        {
            var Re_DetailList = await requisitionService.GetAttachmentListByMatrixType(Convert.ToInt32(masterId), matrixTypeId);
            return Json(Re_DetailList);
        }

        #endregion
    }
}