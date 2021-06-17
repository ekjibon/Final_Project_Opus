using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.SCMIOU.Models;
using OPUSERP.Areas.SCMMatrix.Models;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.SCM.Data.Entity.IOU;
using OPUSERP.SCM.Helpers;
using OPUSERP.SCM.Services.IOU.Interface;
using OPUSERP.SCM.Services.Matrix.Interfaces;

namespace OPUSERP.Areas.SCMIOU.Controllers
{
    [Area("SCMIOU")]
    public class DisbarseController : Controller
    {
        private readonly IIOUService iOUService;
        private readonly IUserInfoes userInfo;
        private readonly IApprovalLogService approverLogService;
        private readonly RequisitionStatusHistory requisitionStatusHistory;
        private readonly IApprovalMatrixService approvalMatrixService;

        public DisbarseController(IIOUService iOUService, IUserInfoes userInfo, IApprovalLogService approverLogService, RequisitionStatusHistory requisitionStatusHistory, IApprovalMatrixService approvalMatrixService)
        {
            this.iOUService = iOUService;
            this.userInfo = userInfo;
            this.approverLogService = approverLogService;
            this.requisitionStatusHistory = requisitionStatusHistory;
            this.approvalMatrixService = approvalMatrixService;
        }

       
        public async Task<IActionResult> Index()
        {
            string userName = HttpContext.User.Identity.Name;

            var userInfos = await userInfo.GetUserInfoByUser(userName);
            var user = userInfo.GetUserInfo();

            IOUPaymentMasterViewModel model = new IOUPaymentMasterViewModel
            {
                //iOUMasters = await iOUService.GetIOUMasterForPayment(),
                iOUMasters = await iOUService.GetIOUMasterByUserNameNDateTime(userName, DateTime.Now.ToString(), DateTime.Now.ToString())
            };
            ViewBag.IOUNO = await iOUService.GetIOUPaymentNo();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveIOUPayment([FromForm] IOUPaymentMasterViewModel model)
        {
            try
            {
                string userName = HttpContext.User.Identity.Name;
                var userInfos = await userInfo.GetUserInfoByUser(userName);

                var IOUNoInfo = await iOUService.GetIOUPaymentNo();

                IOUPaymentMaster data = new IOUPaymentMaster
                {
                    Id = 0,
                    iouPaymentNo = IOUNoInfo,
                    iouPaymentDate = model.iouPaymentDate,
                    //projectId = model.projectId[0],
                    //attentionTo = model.attentionTo,
                    //attentionToId = model.attentionToId,
                    iouPaymentStatus = 1,
                    userId = userInfos.UserId,
                    paymentBy = userName,
                    remarks = model.remarks
                };
                int masterId = await iOUService.SaveIOUPaymentMaster(data);

                for (int i = 0; i < model.ioumasterId.Length; i++)
                {
                    IOUPayment details = new IOUPayment
                    {
                        Id = 0,
                        iOUPaymentMasterId = masterId,
                        IOUId = model.ioumasterId[i],
                        iouAmount = model.iouValue[i],
                        paymentAmount = model.subTotal[i],
                        paymentBy = userInfos.UserId,
                        paymentDate = model.iouPaymentDate,
                        statusInfoId = 1,
                    };
                    await iOUService.SaveIOUPayment(details);
                }


                IEnumerable<ApprovalMatrixViewModel> approvarInfo = await approvalMatrixService.GetAllTypeApprovalMatrixByRaiserIdAndTypeId(Convert.ToInt32(model.projectId[0]), 7, userInfos.UserId);
                List<ApprovalMatrixViewModel> lstApproval = approvarInfo.ToList();

                List<ApprovalLog> approvalLogs = new List<ApprovalLog>();

                ApprovalLog approvalLog = new ApprovalLog
                {
                    masterId = masterId,
                    matrixTypeId = 7,
                    isActive = 0,
                    nextApproverId = lstApproval[0].nextApproverId,
                    userId = userInfos.UserId,
                    sequenceNo = 0
                };
                approvalLogs.Add(approvalLog);

                for (int i = 0; i < lstApproval.Count(); i++)
                {
                    ApprovalLog log = new ApprovalLog();
                    log.masterId = masterId;
                    log.matrixTypeId = 7;
                    log.isActive = 0;
                    log.nextApproverId = 0;
                    log.userId = (int)lstApproval[i].nextApproverId;
                    log.sequenceNo = i + 1;
                    if (i == 0)
                    {
                        log.isActive = 1;
                    }
                    approvalLogs.Add(log);
                    log = new ApprovalLog();
                }
                approverLogService.SaveApproverLogList(approvalLogs);

                var nextUserInfo = await userInfo.GetUserInfoByUserId(lstApproval[0].nextApproverId);
                string empNameCode = userInfos.EmpCode + "-" + userInfos.EmpName;
                string nextEmpNameCode = nextUserInfo.EmpCode + "-" + nextUserInfo.EmpName;
                int reqId = 0;
                for (int i = 0; i < model.ioumasterId.Length; i++)
                {
                    var req = await iOUService.GetIOUDetailsByMasterId(Convert.ToInt32(model.ioumasterId[i]));
                    reqId = req.FirstOrDefault().requisitionDetail.requisitionMasterId;
                    await requisitionStatusHistory.SaveRequisitionStatusLog(Convert.ToInt32(reqId), 7, Convert.ToInt32(userInfos.UserTypeId), userInfos.UserId, empNameCode, nextEmpNameCode, "", 21, "IOUPayment", masterId, IOUNoInfo);
                }

                return Json(masterId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<ActionResult> IOUDisburseList()
        {
            string userName = HttpContext.User.Identity.Name;
            IOUPaymentMasterViewModel model = new IOUPaymentMasterViewModel
            {
                iOUPaymentMasters = await iOUService.GetIOUPaymentMasterByUserName(userName),
                issuedIOUPaymentMasters = await iOUService.GetIssuedIOUPaymentMasterByUserName(userName)
            };
            return View(model);
        }

        public async Task<ActionResult> IOUDisburseListForApprove()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            var user = userInfo.GetUserInfo();
          
            IOUPaymentMasterViewModel model = new IOUPaymentMasterViewModel
            {
                iOUPaymentMasters = await iOUService.GetIOUPaymentMasterListForApprove(userInfos.UserId)
            };
            return View(model);
        }

        public async Task<ActionResult> IOUDisburseApprove(int iOUPaymentMasterId)
        {
            string userName = HttpContext.User.Identity.Name;

            IOUPaymentMasterViewModel model = new IOUPaymentMasterViewModel();

            try
            {
                model.IOUPaymentMaster = await iOUService.GetIOUPaymentMasterById(Convert.ToInt32(iOUPaymentMasterId));
                model.iOUPayments = await iOUService.GetIOUPaymentByiOUPaymentMasterId(iOUPaymentMasterId);
                //model.approverPanel = await approverLogService.GetNextApproverInfo(userName, iOUPaymentMasterId, 7);

                // username
                var userInfos = await userInfo.GetUserInfoByUser(model.IOUPaymentMaster.createdBy);

                ViewBag.EmpName = userInfos.EmpName;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveIOUDisburseApprove(IOUPaymentMasterViewModel model)
        {
           
            try
            {
                string userName = HttpContext.User.Identity.Name;
                var userInfos = await userInfo.GetUserInfoByUser(userName);
                var status = 0;

                if(userInfos.UserId==7)
                {
                    status = 2;
                }
                else
                {
                    status = 3;
                }
                // quantity and unit rate update
                //string userName = HttpContext.User.Identity.Name;
                //var currUserInfo = await userInfo.GetUserInfoByUser(userName);
                //var currentStatus = await approverLogService.GetNextApproverInfo(userName, Convert.ToInt32(model.iOUPaymentMasterId), 7);
                //var iOUMaster = await iOUService.GetIOUPaymentMasterById(Convert.ToInt32(model.iOUPaymentMasterId));
                int statusId = 0;
                //int logStatusId = 0;

                if (model.approveType == -1)
                {
                    statusId = -1;
                    //logStatusId = 25;
                }
                else if (model.approveType == 4)
                {
                    statusId = 4;
                    //logStatusId = 24;
                }
                else
                {
                    //if (currentStatus != null)
                    //{
                    //    var userInfo = await approverLogService.UpdateApprovalLogStatus(userName, Convert.ToInt32(model.iOUPaymentMasterId), 7, model.remarks);
                    //    statusId = 2;
                    //    logStatusId = 22;
                    //}
                    //else
                    //{
                    //statusId = 3;
                    statusId = status;
                    //logStatusId = 23;
                    //}
                }
                for (int i = 0; i < model.ioumasterId.Length; i++)
                {
                    await iOUService.UpdateIOUPaymentForApprove(Convert.ToInt32(model.ioumasterId[i]), model.iouValue[i], statusId);

                }
                iOUService.UpdateIOUPaymentMaster(Convert.ToInt32(model.iOUPaymentMasterId), statusId);

                //string empNameCode = currUserInfo.EmpCode + "-" + currUserInfo.EmpName;
                //string nextEmpNameCode = "";
                //if (logStatusId != 23)
                //{
                //    nextEmpNameCode = currentStatus.EmpCode + "-" + currentStatus.EmpName;
                //}
                //else
                //{
                //    var nextUserInfo = await userInfo.GetUserInfoByUserId(iOUMaster.userId);
                //    nextEmpNameCode = nextUserInfo.EmpCode + "-" + nextUserInfo.EmpName;
                //}
                //int reqId = 0;
                //for (int i = 0; i < model.ioumasterId.Length; i++)
                //{
                //    var req = await iOUService.GetIOUDetailsByMasterId(Convert.ToInt32(model.ioumasterId[i]));
                //    reqId = req.FirstOrDefault().requisitionDetail.requisitionMasterId;
                //    await requisitionStatusHistory.SaveRequisitionStatusLog(Convert.ToInt32(reqId), 7, Convert.ToInt32(currUserInfo.UserTypeId), currUserInfo.UserId, empNameCode, nextEmpNameCode, model.remarks, logStatusId, "IOUPayment", model.iOUPaymentMasterId, iOUMaster.iouPaymentNo);
                //}


                return Json(statusId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> IOUListForPayment()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            var user = userInfo.GetUserInfo();
            IOUViewModel model = new IOUViewModel
            {
                iOUPayments = await iOUService.GetIOUPaymentByType(3),
                //iOUAdjustments = await iOUService.GetIOUPaymentByType(3),
            };
            return View(model);
        }

        public async Task<ActionResult> IOUPaymentEntry(int id)
        {
            IOUViewModel model = new IOUViewModel
            {
                iOUPayment = await iOUService.GetIOUPaymentById(id),
            };
            model.iOUDetails = await iOUService.GetIOUDetailsByMasterId(Convert.ToInt32(model.iOUPayment.IOUId));
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> SaveIOUPaymentStatus(int? IOUPayId,  decimal? paymentAmount, int? receivebyId, DateTime? ReceiveDate,string PaymentMode,string ChequeNo,string bankName)
        {
            try
            {
                await iOUService.UpdateIOUPaymentForReceivedPayment(Convert.ToInt32(IOUPayId), paymentAmount, receivebyId, ReceiveDate, PaymentMode, ChequeNo, bankName);
                return Json(IOUPayId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}