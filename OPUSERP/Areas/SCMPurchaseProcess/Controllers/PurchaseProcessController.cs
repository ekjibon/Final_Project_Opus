using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.SCMMatrix.Models;
using OPUSERP.Areas.SCMPurchaseProcess.Models;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.SCM.Data.Entity.PurchaseProcess;
using OPUSERP.SCM.Data.Entity.Requisition;
using OPUSERP.SCM.Helpers;
using OPUSERP.SCM.Services.Matrix.Interfaces;
using OPUSERP.SCM.Services.PurchaseProcess.Interfaces;
using OPUSERP.SCM.Services.Requisition.Interfaces;
using OPUSERP.SCM.Services.SCMMail.interfaces;

namespace OPUSERP.Areas.SCMPurchaseProcess.Controllers
{
    [Area("SCMPurchaseProcess")]
    public class PurchaseProcessController : Controller
    {
        private readonly IPurchaseProcessService purchaseProcessService;
        private readonly IProcurementService procurementService;
        private readonly IRequisitionService requisitionService;
        private readonly IUserInfoes userInfoes;
        private readonly IApprovalMatrixService approvalMatrixService;
        private readonly IApprovalLogService approverLogService;
        private readonly RequisitionStatusHistory requisitionStatusHistory;
        private readonly ISCMMailService sCMMailService;
        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public PurchaseProcessController(IPurchaseProcessService purchaseProcessService, RequisitionStatusHistory requisitionStatusHistory
            , IUserInfoes userInfoes, IRequisitionService requisitionService, IProcurementService procurementService, IApprovalMatrixService approvalMatrixService
            , IApprovalLogService approverLogService, ISCMMailService sCMMailService, IHostingEnvironment hostingEnvironment, IConverter converter)
        {
            this.purchaseProcessService = purchaseProcessService;
            this.procurementService = procurementService;
            this.requisitionService = requisitionService;
            this.userInfoes = userInfoes;
            this.approvalMatrixService = approvalMatrixService;
            this.requisitionStatusHistory = requisitionStatusHistory;
            this.approverLogService = approverLogService;
            this.sCMMailService = sCMMailService;
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfo = await userInfoes.GetUserInfoByUser(userName);
            var model = new CSRequisitionList
            {
                requisitionForCSViewModels = await purchaseProcessService.GetRequisitionListForBuyer(userInfo.UserId),
                cSMasters = await purchaseProcessService.GetCSMasterList(userInfo.UserId),
            };
            return View(model);
        }


        // Get: PurchaseProcess
        [HttpGet]
        public async Task<IActionResult> QutaionProcess(int reqId,int projeectId)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfo = await userInfoes.GetUserInfoByUser(userName);
            ViewBag.reqMasterId = reqId;
            ViewBag.CSNumber = await purchaseProcessService.GetCSNumber();
            var model = new PurchaseProcessViewModel
            {
                projectId = projeectId,
                reqId=reqId,
                requisitionMaster = await requisitionService.GetRequisitionMasterById(reqId),
                requisitionDetails = await requisitionService.GetRequisitionDetailListByReqId(reqId),
                procurementTypes=await procurementService.GetProcurementTypeList(),
                procurementValues=await procurementService.GetProcurementValueList(),
                justificationTypes=await procurementService.GetJustificationTypeList(),
            };
            return View(model);
        }

        // POST: PurchaseProcess
        [HttpPost]
        public async Task<JsonResult> QutaionProcess([FromForm] PurchaseProcessViewModel model)
        {
            try
            {
                string userName = HttpContext.User.Identity.Name;
                var userInfo = await userInfoes.GetUserInfoByUser(userName);

                IEnumerable<ApprovalMatrixViewModel> approvarInfo = await approvalMatrixService.GetAllTypeApprovalMatrixByRaiserIdAndTypeId(Convert.ToInt32(model.projectId), 2, userInfo.UserId);
                List<ApprovalMatrixViewModel> lstApproval = approvarInfo.ToList();
                string csNo = await purchaseProcessService.GetCSNumber();
                CSMaster cSMaster = new CSMaster
                {
                    csNo = csNo,
                    csDate = DateTime.Now.Date,
                    requisitionId = model.reqId,
                    csStatus = 1,
                    expectedDeliveryDate = model.deliveryDate,
                    rfqRefNo = model.rfqReference,
                    userId = userInfo.UserId,
                    procurementTypeId = model.procurementTypeId,
                    procurementValueId = model.procurementValueId
                };
                int csMasterId = await purchaseProcessService.SaveCSMaster(cSMaster);

                if (model.reqDetailsId != null)
                {
                    int index = 0;
                    List<CSDetail> lstDetails = new List<CSDetail>();
                    for (int i = 0; i < model.reqDetailsId.Length; i++)
                    {
                        for (int j = 0; j < model.supplierId.Length; j++)
                        {
                            CSDetail detail = new CSDetail
                            {
                                Id = 0,
                                cSMasterId = csMasterId,
                                requisitionDetailId = model.reqDetailsId[i],
                                itemCategoryId = model.itemCatId[i],
                                currentStatus = 1,
                                supplierId = model.supplierId[j],
                                qty = model.csQty[index]??0,
                                rate = model.csRate[index],
                            };
                            lstDetails.Add(detail);
                            index++;
                            //int csDetailsId = await purchaseProcessService.SaveCSDetailsSingle(detail);
                        }
                    }
                    purchaseProcessService.SaveCSDetails(lstDetails);
                }

                List<ApprovalLog> approvalLogs = new List<ApprovalLog>();
                ApprovalLog approvalLog = new ApprovalLog
                {
                    masterId = csMasterId,
                    matrixTypeId = 2,
                    isActive = 0,
                    nextApproverId = lstApproval[0].nextApproverId,
                    userId = userInfo.UserId,
                    sequenceNo = 0
                };
                approvalLogs.Add(approvalLog);

                for (int i = 0; i < lstApproval.Count(); i++)
                {
                    ApprovalLog log = new ApprovalLog();
                    log.masterId = csMasterId;
                    log.matrixTypeId = 2;
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

                if (model.justifyTypeId.Length > 0)
                {
                    List<Justification> lstJustify = new List<Justification>();
                    for (int i = 0; i < model.justifyTypeId.Length; i++)
                    {
                        Justification justification = new Justification
                        {
                            cSMasterId = csMasterId,
                            justificationTypeId = model.justifyTypeId[i],
                            isJustify = model.isJustify[i],
                            justificationValue = model.justifyValue[i],
                        };
                        lstJustify.Add(justification);
                    }
                    procurementService.SaveJustificationList(lstJustify);
                }

                var nextUserInfo = await userInfoes.GetUserInfoByUserId(lstApproval[0].nextApproverId);
                string empNameCode = userInfo.EmpCode + "-" + userInfo.EmpName;
                string nextEmpNameCode = nextUserInfo.EmpCode + "-" + nextUserInfo.EmpName;
                await requisitionStatusHistory.SaveRequisitionStatusLog(Convert.ToInt32(model.reqId), 2, Convert.ToInt32(userInfo.UserTypeId), userInfo.UserId, empNameCode, nextEmpNameCode, "", 9, "CS", csMasterId, csNo);

                string host = HttpContext.Request.Host.ToString();
                string scheme = Request.Scheme;
                string baseUrl = $"" + scheme + "://" + host + "/Auth/Account/Login";
                //await sCMMailService.MailMessage(nextUserInfo.Email, csNo, 9, empNameCode, baseUrl);

                return Json(csMasterId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public async Task<JsonResult> UploadCSAttachment(string id, string[] arrayFileAtach)
        {
            string userName = HttpContext.User.Identity.Name;

            if (Request.Form.Files.Count > 0)
            {
                for (int i = 0; i < Request.Form.Files.Count; i++)
                {
                    int _min = 10000;
                    int _max = 99999;
                    Random _rdm = new Random();
                    int rnd = _rdm.Next(_min, _max);

                    string filePath = string.Empty;
                    string fileName = string.Empty;
                    string fileType = string.Empty;

                    IFormFile file = Request.Form.Files[i];
                    fileType = file.ContentType;
                    fileName = rnd + file.FileName;
                    filePath = "wwwroot/Upload/CS/" + fileName;
                    //var fileD = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Attachments\\RoutingFile", fileName);
                    var fileD = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    using (var fileSrteam = new FileStream(fileD, FileMode.Create))
                    {
                        await file.CopyToAsync(fileSrteam);
                    }

                    DocumentAttachment attachment = new DocumentAttachment
                    {
                        Id = 0,
                        masterId = Convert.ToInt32(id),
                        filePath = filePath,
                        fileName = fileName,
                        fileType = fileType,
                        subject = arrayFileAtach[i],
                        matrixTypeId = 2
                    };
                    await requisitionService.SaveDocumentAttachment(attachment);
                }
            }
            return Json(true);
        }

        // Get: PurchaseProcess
        [HttpGet]
        public async Task<IActionResult> RFQQutaionProcess(int rfqId)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfo = await userInfoes.GetUserInfoByUser(userName);
            ViewBag.reqMasterId = rfqId;
            ViewBag.CSNumber = await purchaseProcessService.GetCSNumber();
            var model = new PurchaseProcessViewModel
            {
                //requisitionMaster = await requisitionService.GetRequisitionMasterById(reqId),
                //requisitionDetails = await requisitionService.GetRequisitionDetailListByReqId(reqId),
                procurementTypes = await procurementService.GetProcurementTypeList(),
                procurementValues = await procurementService.GetProcurementValueList(),
                justificationTypes = await procurementService.GetJustificationTypeList(),
            };
            return View(model);
        }

        [Route("global/api/GetRequisitionDetailListForBuyer/{reqId}")]
        [HttpGet]
        public async Task<IActionResult> GetRequisitionDetailListForBuyer(int reqId)
        {
           string userName = HttpContext.User.Identity.Name;
            var userInfo = await userInfoes.GetUserInfoByUser(userName);
            var result = await requisitionService.GetRequisitionDetailListForBuyer(reqId, userInfo.UserId);
            return Json(result);
        }

    }
}