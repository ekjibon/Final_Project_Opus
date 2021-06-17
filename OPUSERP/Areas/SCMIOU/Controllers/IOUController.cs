using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.SCMIOU.Models;
using OPUSERP.Areas.SCMMatrix.Models;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.SCM.Data.Entity.IOU;
using OPUSERP.SCM.Data.Entity.Requisition;
using OPUSERP.SCM.Helpers;
using OPUSERP.SCM.Services.IOU.Interface;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using OPUSERP.SCM.Services.Matrix.Interfaces;
using OPUSERP.SCM.Services.PurchaseOrder.Interface;
using OPUSERP.SCM.Services.PurchaseProcess.Interfaces;
using OPUSERP.SCM.Services.Requisition.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace OPUSERP.Areas.SCMIOU.Controllers
{
    [Area("SCMIOU")]

    public class IOUController : Controller
    {
        private readonly IAddressService addressService;
        private readonly IPurchaseProcessService purchaseProcessService;
        private readonly IPurchaseOrderService purchaseOrderService;
        private readonly IRequisitionService requisitionService;
        private readonly IApprovalMatrixService approvalMatrixService;
        private readonly IApprovalLogService approverLogService;
        private readonly RequisitionStatusHistory requisitionStatusHistory;
        private readonly IProjectService projectService;
        private readonly IIOUService iOUService;
        private readonly IUserInfoes userInfo;
        private readonly string rootPath;
        private readonly MyPDF myPDF;
        private readonly IUserTypeService userTypeService;

        public IOUController(IAddressService addressService, IProjectService projectService, IUserTypeService userTypeService, RequisitionStatusHistory requisitionStatusHistory, IApprovalLogService approverLogService, IApprovalMatrixService approvalMatrixService, IPurchaseOrderService purchaseOrderService, IRequisitionService requisitionService, IHostingEnvironment hostingEnvironment, IConverter converter, IPurchaseProcessService purchaseProcessService, IUserInfoes userInfo, IIOUService iOUService)
        {
            this.addressService = addressService;
            this.purchaseProcessService = purchaseProcessService;
            this.purchaseOrderService = purchaseOrderService;
            this.requisitionService = requisitionService;
            this.approvalMatrixService = approvalMatrixService;
            this.approverLogService = approverLogService;
            this.requisitionStatusHistory = requisitionStatusHistory;
            this.projectService = projectService;
            this.iOUService = iOUService;
            this.userInfo = userInfo;
            this.userTypeService = userTypeService;
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        // GET: PurchaseOrder

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            ViewBag.EmpName = userInfos.EmpName;
            //ViewBag.reqId = id;
            ViewBag.IOUNO = await iOUService.GetIOUNo();

            IOUViewModel model = new IOUViewModel
            {
                //requisitionMasters = await requisitionService.GetAllRequisitionMasterForIOUList(userName),
                requisitionForCSViewModels = await purchaseProcessService.GetRequisitionListForBuyer(userInfos.UserId),
            };
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> UpdateRFQMaster(int Id)
        {
            await iOUService.UpdateRFQMaster(Id,HttpContext.User.Identity.Name);
            return RedirectToAction(nameof(RFQListReceive));
        }

        [HttpGet]
        public async Task<ActionResult> RFQ()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            ViewBag.EmpName = userInfos.EmpName;
            //ViewBag.reqId = id;
            ViewBag.IOUNO = await iOUService.GetIOUNo();

            var reqno = "";
            var country = await iOUService.GetRFQMaster();
            int count = country.Count();
            var autonumber = await userTypeService.GetAutonumberingInfoById("RFQ");
            var total = 0;
            string code = "";
            if (autonumber != null)
            {
                if (autonumber.NumType == 1)
                {
                    total = count + (int)autonumber.defaultValue;
                    code = userTypeService.GetNumberCode(total.ToString(), (int)autonumber.NumValue);
                }
                else
                {
                    total = count + (int)autonumber.startValue;
                    code = userTypeService.GetNumberCode(total.ToString(), (int)autonumber.NumValue);
                    var ymd = "";
                    if (autonumber.isyear == 1)
                    {
                        ymd = DateTime.Now.Year.ToString();
                    }
                    if (autonumber.yseparator != null)
                    {
                        ymd = ymd + autonumber.yseparator;
                    }
                    if (autonumber.ismonth == 1)
                    {
                        ymd = ymd + DateTime.Now.Month.ToString();
                    }
                    if (autonumber.mseparator != null)
                    {
                        ymd = ymd + autonumber.mseparator;
                    }
                    if (autonumber.isdate == 1)
                    {
                        ymd = ymd + DateTime.Now.Day.ToString();
                    }
                    if (autonumber.dseparator != null)
                    {
                        ymd = ymd + autonumber.dseparator;
                    }
                    var sep = "";
                    sep = autonumber.separator;
                    code = autonumber.prefix + sep + ymd + code;
                }
                reqno = code;
            }

            ViewBag.IOUNO = reqno;

            var reqdata = await iOUService.GetRFQReqDetail();

            List<int?> reqids = reqdata.Select(x => x.requisitionMasterId).ToList();
            var requisitionmasters = await requisitionService.GetAllRequisitionMasterList();
            requisitionmasters = requisitionmasters.Where(x => !reqids.Contains(x.Id));
            RFQViewModel model = new RFQViewModel
            {
                requisitionMasters = requisitionmasters// await requisitionService.GetAllRequisitionMasterForIOUList(userName),
                //requisitionForCSViewModels = await purchaseProcessService.GetRequisitionListForBuyer(userInfos.UserId),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRFQ([FromForm] RFQViewModel model)
        {
            try
            {
                RFQMaster data = new RFQMaster
                {
                    Id = 0,
                    rfqNo = model.iouNo,
                    rfqDate = DateTime.Now,
                    rfqSubject = model.attentionTo,
                    termscondition = model.content
                };
                int masterId = await iOUService.SaveRFQMaster(data);

                for (int i = 0; i < model.reqDetailsall.Length; i++)
                {
                        RFQReqDetail details = new RFQReqDetail
                        {
                            Id = 0,
                            rFQMasterId = masterId,
                            requisitionMasterId = Convert.ToInt32(model.reqIdAll[i]),
                            requisitionDetailId= Convert.ToInt32(model.reqDetailsall[i]),
                            LotNo=model.lotNos[i]
                        };
                        await iOUService.SaveRFQReqDetail(details);
                }
                for (int i = 0; i < model.supsId.Length; i++)
                {
                    RFQSupDetail supdetails = new RFQSupDetail
                    {
                        Id = 0,
                        rFQMasterId = masterId,
                        supplierId = Convert.ToInt32(model.supsId[i]),
                    };
                    await iOUService.SaveRFQSupDetail(supdetails);
                }
                string userName = HttpContext.User.Identity.Name;
                IEnumerable<ApproverPanelViewModel> lstApproverPanel = await approvalMatrixService.GetPRApproverPanelList(userName, 10, 1);
                if (lstApproverPanel.Count() > 0)
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
                            matrixTypeId = 10,
                            userId = Convert.ToInt32(panels.nextApproverId),
                            sequenceNo = Convert.ToInt32(panels.sequenceNo),
                            isActive = isactive,
                            notes = notes,
                            nextApproverId = nextAppId
                        };
                        i = i + 1;
                        await approverLogService.SaveApproverLog(appLog);
                    }
                }
                return Json(masterId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        public async Task<IActionResult> CreateRFQReceive([FromForm] RFQViewModel model)
        {
            try
            {
                RFQPriceMaster data = new RFQPriceMaster
                {
                    Id = 0,
                    rFQMasterId = model.rfqId,
                    supplierId=model.supplierId,                   
                };
                int masterId = await iOUService.SaveRFQPriceMaster(data);

                for (int i = 0; i < model.reqDetailsall.Length; i++)
                {
                        RFQReqDetailPrice details = new RFQReqDetailPrice
                        {
                            Id = 0,
                            rFQPriceMasterId = masterId,
                            rFQReqDetailId= Convert.ToInt32(model.reqDetailsall[i]),
                           
                            rate=model.rate[i]

                        };
                        await iOUService.SaveRFQReqDetailPrice(details);
                }
                return Json(masterId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        public async Task<IActionResult> CreateRFQReceiveE([FromForm] RFQViewModel model)
        {
            try
            {
                var data = await iOUService.GetRFQPriceMasterbyrfqmasterId((int)model.rfqId);
                data = data.Where(x => x.supplierId == model.supplierId);

                //RFQPriceMaster data = new RFQPriceMaster
                //{
                //    Id = 0,
                //    rFQMasterId = model.rfqId,
                //    supplierId=model.supplierId,
                   
                //};
                int masterId = data.FirstOrDefault().Id;
                await iOUService.DeleteRFQPriceDetailsByMasterId(masterId);
                for (int i = 0; i < model.reqDetailsall.Length; i++)
                {                   
                        RFQReqDetailPrice details = new RFQReqDetailPrice
                        {
                            Id = 0,
                            rFQPriceMasterId = masterId,
                            rFQReqDetailId= Convert.ToInt32(model.reqDetailsall[i]),                           
                            rate=model.rate[i]
                        };
                        await iOUService.SaveRFQReqDetailPrice(details);
                }
                return Json(masterId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<JsonResult> UploadAttachmentp(string id, int[] arrayFileAtachs)
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
                    // IFormFile file = arrayFileAtach[i];
                    fileType = file.ContentType;
                    fileName = rnd + file.FileName;
                    filePath = "wwwroot/Upload/" + fileName;
                    //var fileD = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Attachments\\RoutingFile", fileName);
                    var fileD = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    using (var fileSrteam = new FileStream(fileD, FileMode.Create))
                    {
                        await file.CopyToAsync(fileSrteam);
                    }
             
                    RFQDocumentAttachmentDetail attachments = new RFQDocumentAttachmentDetail
                    {
                        Id = 0,
                 
                        filePath = filePath,
                        fileName = fileName,
                        fileType = fileType,
                        createdBy = userName,
                        rFQPriceMasterId= Convert.ToInt32(id)                   
                    };
                    await iOUService.SaveRFQDocumentAttachmentDetail(attachments);
                }
            }
            return Json(true);
        }

        [HttpGet]
        public async Task<JsonResult> GetRequisitionDetailsInfo(int id)
        {

            IOUViewModel model = new IOUViewModel
            {
                requisitionDetailViews = await requisitionService.GetLinqRequisitionDetailListByReqId(id),
                deliveryLocations = await purchaseOrderService.GetDeliveryLocation()
            };

            return Json(model);
        }

        [HttpGet]
        public async Task<JsonResult> GetRFQDetailsInfo(int id)
        {

            IOUViewModel model = new IOUViewModel
            {
                rFQReqDetails = await iOUService.GetRFQReqDetailbyreqId(id)
            };
          
            return Json(model);
        }
        [HttpGet]
        public async Task<JsonResult> GetRFQDetailsInfoE(int id,int supplierId)
        {
            var data = await iOUService.GetRFQReqDetailPricebyrfqId(id);
            data = data.Where(x => x.rFQPriceMaster.supplierId == supplierId);
            var doc = await iOUService.GetRFQDocumentAttachmentDetailbyrfqmasterid(id);
            doc = doc.Where(x => x.rFQPriceMaster.supplierId == supplierId);
            RFQViewModel model = new RFQViewModel
            {
                rFQReqDetailPrices= data,
                rFQDocumentAttachmentDetails = doc,
                
            };
          
            return Json(model);
        }
        [HttpGet]
        public async Task<JsonResult> GetRFQPriceMaster(int id)
        {
            RFQViewModel model = new RFQViewModel
            {
                rFQPriceMasters = await iOUService.GetRFQPriceMasterbyrfqmasterId(id),
                rFQDocumentAttachmentDetails=await iOUService.GetRFQDocumentAttachmentDetailbyrfqmasterid(id),
                rFQSupDetails=await iOUService.GetRFQSupDetailsbyrfqid(id),
                rFQReqDetailPrices=await iOUService.GetRFQReqDetailPricebyrfqId(id)
            };

            return Json(model);
        }


        [HttpGet]
        public async Task<JsonResult> GetEmployeeInfo(int id)
        {
            IOUViewModel model = new IOUViewModel
            {
                requisitionMaster = await requisitionService.GetRequisitionMasterById(id),
            };
            var reqByUserInfos = await userInfo.GetUserInfoByUser(model.requisitionMaster.createdBy);

            string empName = reqByUserInfos.EmpName;

            return Json(empName);
        }

        public async Task<ActionResult> CreateIOU(int reqId)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            ViewBag.EmpName = userInfos.EmpName;
            ViewBag.reqId = reqId;
            ViewBag.IOUNO = await iOUService.GetIOUNo();
            IOUViewModel model = new IOUViewModel
            {
                requisitionMaster = await requisitionService.GetRequisitionMasterById(reqId),
                requisitionDetails = await requisitionService.GetRequisitionDetailListByReqId(reqId),
                deliveryLocations = await purchaseOrderService.GetDeliveryLocation(),
                currencies = await purchaseOrderService.GetCurrency(),
            };
            var reqByUserInfos = await userInfo.GetUserInfoByUser(model.requisitionMaster.createdBy);
            ViewBag.AttentionTo = reqByUserInfos.EmpName;

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateIOU([FromForm] IOUViewModel model)
        {
            try
            {
                string userName = HttpContext.User.Identity.Name;
                var userInfos = await userInfo.GetUserInfoByUser(userName);

                var IOUNoInfo = await iOUService.GetIOUNo();

                IOUMaster data = new IOUMaster
                {
                    Id = 0,
                    iouNo = IOUNoInfo,
                    iouDate = DateTime.Now,
                    projectId = model.projectId,
                    attentionTo = model.attentionTo,
                    iouStatus = 1,
                    userId = userInfos.UserId,
                    expectedDeliveryDate = model.expectedDeliveryDate
                };
                int masterId = await iOUService.SaveIOUMaster(data);

                for (int i = 0; i < model.reqDetailsall.Length; i++)
                {
                    var vatCal = (model.poQntall[i] * model.txtUnitRateall[i]) * (model.txtVatall[i] / 100);
                    if (model.poQntall[i] != null)
                    {
                        if (model.txtUnitRateall[i] != 0 || model.txtUnitRateall[i] != null)
                        {
                            IOUDetails details = new IOUDetails
                            {
                                Id = 0,
                                IOUId = masterId,
                                requisitionDetailId = model.reqDetailsall[i],
                                requisitionId = Convert.ToInt32(model.reqIdAll[i]),
                                rate = model.txtUnitRateall[i],
                                qty = model.poQntall[i],
                                iou_DStatus = 1,
                                currencyId = 1,
                                currencyRate = 1,
                                deliveryLocationId = model.txtLocationall[i],
                                //description = model.txtDescriptionall[i],
                                //otherDeliveryLocation = model.txtOtherLocationall[i],
                                vatAmount = (model.poQntall[i] * model.txtUnitRateall[i]) + vatCal,
                                vatPercent = model.txtVatall[i],
                            };
                            await iOUService.SaveIOUDetails(details);
                        }

                    }
                }


                IEnumerable<ApprovalMatrixViewModel> approvarInfo = await approvalMatrixService.GetAllTypeApprovalMatrixByRaiserIdAndTypeId(Convert.ToInt32(model.projectId), 2, userInfos.UserId);
                List<ApprovalMatrixViewModel> lstApproval = approvarInfo.ToList();

                List<ApprovalLog> approvalLogs = new List<ApprovalLog>();
                if (lstApproval.Count() > 0)
                {
                    ApprovalLog approvalLog = new ApprovalLog
                    {
                        masterId = masterId,
                        matrixTypeId = 6,
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
                        log.matrixTypeId = 6;
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
                    for (int i = 0; i < model.reqDetailsall.Length; i++)
                    {
                        await requisitionStatusHistory.SaveRequisitionStatusLog(Convert.ToInt32(model.reqIdAll[i]), 6, Convert.ToInt32(userInfos.UserTypeId), userInfos.UserId, empNameCode, nextEmpNameCode, "", 15, "IOU", masterId, IOUNoInfo);
                    }
                }

                return Json(masterId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> IOUList()
        {
            string userName = HttpContext.User.Identity.Name;
            IOUViewModel model = new IOUViewModel
            {
                iOUMasters = await iOUService.GetIOUMasterByUserName(userName),
                issuedIOUMasters = await iOUService.GetIssuedIOUInformation(userName)
            };
            return View(model);
        }

        public async Task<IActionResult> IndexRFQ(int? id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            RFQViewModel model = new RFQViewModel
            {
              rFQMaster=await iOUService.GetRFQMasterbyId(Convert.ToInt32(id)),
               
                rFQSupDetails = await iOUService.GetRFQSupDetailsbyrfqid(Convert.ToInt32(id)),
               
                approerPanelList = await approverLogService.GetApprovedListByApprovar(userInfos.UserId, Convert.ToInt32(id), 10),

                rFQReqDetails=await iOUService.GetRFQReqDetailbyreqId(Convert.ToInt32(id))
            };
            return View(model);
        }
       
        [HttpPost]
        public async Task<JsonResult> SaveRFQApproval(int reqId, string reqNo, string appType, string remark)
        {
            try
            {
                string actionNo = reqNo;//reqNo
                string CCID = string.Empty;
                string _empCode = string.Empty;
                string mailTo = string.Empty;
                string userName = HttpContext.User.Identity.Name;
                var currUserInfo = await userInfo.GetUserInfoByUser(userName);
                var currentStatus = await approverLogService.GetNextApproverInfo(userName, reqId, 10);
                int statusId = 0;

                if (appType == "5")
                {
                    await approverLogService.DeleteApproverLogByMatrixTypeId(10, reqId);
                    statusId = 5;
                }
                else if (appType == "4")
                {
                    await approverLogService.DeleteApproverLogByMatrixTypeId(10, reqId);
                    statusId = 4;
                }
                else
                {
                    if (currentStatus != null)
                    {
                        var userInfo = await approverLogService.UpdateApprovalLogStatus(userName, reqId, 10, remark);
                        statusId = 2;
                    }
                    else
                    {
                        statusId = 3;
                    }
                }
                iOUService.UpdateRFQMasterById(reqId, statusId);

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


        public async Task<ActionResult> RFQList()
        {
            string userName = HttpContext.User.Identity.Name;
            RFQViewModel model = new RFQViewModel
            {
                rFQMasters = await iOUService.GetRFQMaster(),
               
            };
            return View(model);
        }
        public async Task<ActionResult> RFQApprovalList()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            RFQViewModel model = new RFQViewModel
            {
                rFQApprovedListViewModels = await iOUService.GetRFQApprovedListViewModels(userInfos.UserId),
               
            };
            return View(model);
        }
        public async Task<ActionResult> RFQListReceive()
        {
            string userName = HttpContext.User.Identity.Name;
            var data = await iOUService.GetRFQMaster();
            var pricemaster = await iOUService.GetRFQPriceMaster();
            List<int?> masterids = pricemaster.Select(x => x.rFQMasterId).ToList();
            data = data.Where(x => masterids.Contains(x.Id));
            RFQViewModel model = new RFQViewModel
            {
                rFQMasters = data,
                rFQPriceMasters = pricemaster,
                rFQDocumentAttachmentDetails = await iOUService.GetRFQDocumentAttachmentDetails(),
                rFQReqDetailPrices = await iOUService.GetRFQReqDetailPrice()
               
            };
            return View(model);
        }

        public async Task<ActionResult> RFQListReceiveClose()
        {
            string userName = HttpContext.User.Identity.Name;
            var data = await iOUService.GetRFQMaster();
            var pricemaster = await iOUService.GetRFQPriceMaster();
            List<int?> masterids = pricemaster.Select(x => x.rFQMasterId).ToList();
            data = data.Where(x => masterids.Contains(x.Id));
            RFQViewModel model = new RFQViewModel
            {
                rFQMasters = data,
                rFQPriceMasters = pricemaster,
                rFQDocumentAttachmentDetails = await iOUService.GetRFQDocumentAttachmentDetails(),
                rFQReqDetailPrices = await iOUService.GetRFQReqDetailPrice()

            };
            return View(model);
        }
        public async Task<ActionResult> RFQReceive()
        {
            string userName = HttpContext.User.Identity.Name;
            var RFQMaster = await iOUService.GetRFQMaster();
            var RFQSupp = await iOUService.GetRFQSupDetails();
            var RFQReceive = await iOUService.GetRFQPriceMaster();
            List<int?> ids = new List<int?>();
            foreach (var RM in RFQMaster)
            {
                int suppcount = RFQSupp.Where(x => x.rFQMasterId == RM.Id).Count();
                int receivecount= RFQReceive.Where(x => x.rFQMasterId == RM.Id).Count();
                if (suppcount == receivecount)
                {
                    ids.Add(RM.Id);
                }
            }
           
           // List<int?> rfqmasterid = RFQReceive.Select(x => x.rFQMasterId).ToList();
            RFQMaster = RFQMaster.Where(x => !ids.Contains(x.Id)).ToList();
            RFQViewModel model = new RFQViewModel
            {
                rFQMasters = RFQMaster,               
            };
            return View(model);
        }

        public async Task<ActionResult> IOUListForApprove()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            var user = userInfo.GetUserInfo();
            IOUViewModel model = new IOUViewModel
            {
                iOUMasters = await iOUService.GetIOUListForApprove(userInfos.UserId)
            };
            return View(model);
        }

        public async Task<ActionResult> IOUApprove(int iouMasterId)
        {
            string userName = HttpContext.User.Identity.Name;

            IOUViewModel model = new IOUViewModel();

            try
            {
                model.iOUMaster = await iOUService.GetIOUMasterById(iouMasterId);
                model.iOUDetails = await iOUService.GetIOUDetailsByMasterId(iouMasterId);
                model.approverPanel = await approverLogService.GetNextApproverInfo(userName, iouMasterId, 6);

                // username
                var userInfos = await userInfo.GetUserInfoByUser(model.iOUMaster.createdBy);

                ViewBag.EmpName = userInfos.EmpName;

                // project id find by requsitionId

                //var id = model.iOUDetails.Select(i => i.requisitionId).FirstOrDefault();

                //int RequisitionMaster = await requisitionService.GetRequisitionMasterById(id);

            }

            catch (Exception ex)
            {
                throw ex;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> IOUApprove(IOUViewModel model)
        {
            try
            {

                // quantity and unit rate update
                for (int i = 0; i < model.qty.Length; i++)
                {
                    var vatCal = (model.qty[i] * model.rate[i]) * (model.vat[i] / 100);

                    IOUDetails details = await iOUService.GetIOUDetailsById(model.detailsId[i]);

                    details.rate = model.rate[i];
                    details.qty = model.qty[i];
                    details.vatPercent = model.vat[i];
                    details.vatAmount = (model.qty[i] * model.rate[i]) + vatCal;

                    await iOUService.SaveIOUDetails(details);
                }


                string userName = HttpContext.User.Identity.Name;
                var currUserInfo = await userInfo.GetUserInfoByUser(userName);
                var currentStatus = await approverLogService.GetNextApproverInfo(userName, model.masterIdIOU, 6);
                var iOUMaster = await iOUService.GetIOUMasterById(model.masterIdIOU);
                int statusId = 0;
                int logStatusId = 0;

                if (model.approveType == -1)
                {
                    statusId = -1;
                    logStatusId = 19;
                }
                else if (model.approveType == 4)
                {
                    statusId = 4;
                    logStatusId = 18;
                }
                else
                {
                    if (currentStatus != null)
                    {
                        var userInfo = await approverLogService.UpdateApprovalLogStatus(userName, model.masterIdIOU, 6, model.Remark);
                        statusId = 2;
                        logStatusId = 16;
                    }
                    else
                    {
                        statusId = 3;
                        logStatusId = 17;
                    }
                }
                iOUService.UpdateIOUMaster(model.masterIdIOU, statusId);

                string empNameCode = currUserInfo.EmpCode + "-" + currUserInfo.EmpName;
                string nextEmpNameCode = "";
                if (logStatusId != 17)
                {
                    nextEmpNameCode = currentStatus.EmpCode + "-" + currentStatus.EmpName;
                }
                else
                {
                    var nextUserInfo = await userInfo.GetUserInfoByUserId(iOUMaster.userId);
                    nextEmpNameCode = nextUserInfo.EmpCode + "-" + nextUserInfo.EmpName;
                }
                for (int i = 0; i < model.qty.Length; i++)
                {
                    IOUDetails details = await iOUService.GetIOUDetailsById(model.detailsId[i]);
                    await requisitionStatusHistory.SaveRequisitionStatusLog(Convert.ToInt32(details.requisitionId), 6, Convert.ToInt32(currUserInfo.UserTypeId), currUserInfo.UserId, empNameCode, nextEmpNameCode, model.Remark, logStatusId, "IOU", model.masterIdIOU, iOUMaster.iouNo);
                }


                return Json(statusId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<ActionResult> IOUReport()
        {
            string userName = HttpContext.User.Identity.Name;
            IOUViewModel model = new IOUViewModel
            {
                iOUMasters = await iOUService.GetApprovedIOUMasterListByUserName(userName),
                issuedIOUMasters = await iOUService.GetIssuedIOUInformation(userName)
            };
            return View(model);
        }

        public async Task<ActionResult> IOUApprovedListForPayment()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            var user = userInfo.GetUserInfo();
            IOUViewModel model = new IOUViewModel
            {
                iOUMasters = await iOUService.GetIOUMasterForPayment(),
                iOUPayments = await iOUService.GetIOUPayment(),
            };
            return View(model);
        }

        public async Task<ActionResult> IOUPayment(int id)
        {
            IOUViewModel model = new IOUViewModel
            {
                iOUMaster = await iOUService.GetIOUMasterById(id),
                iOUDetails = await iOUService.GetIOUDetailsByMasterId(id),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> IOUPayment(int? IOUMasterId, decimal? IOUAmount, decimal? PaymentAmount, DateTime? PaymentDate)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            try
            {
                IOUPayment data = new IOUPayment
                {
                    Id = 0,
                    IOUId = IOUMasterId,
                    iouAmount = IOUAmount,
                    paymentAmount = PaymentAmount,
                    paymentDate = PaymentDate,
                    paymentBy = userInfos.UserId,
                    statusInfoId = 1
                };
                int masterId = await iOUService.SaveIOUPayment(data);

                return Json(masterId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> PaidIOUListForAdjust()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            var user = userInfo.GetUserInfo();
            IOUViewModel model = new IOUViewModel
            {
                iOUPayments = await iOUService.GetIOUPaymentByType(5),
                //iOUAdjustments = await iOUService.GetIOUPaymentByType(3),
            };
            return View(model);
        }

        public async Task<ActionResult> IOUAdjust(int id)
        {
            IOUViewModel model = new IOUViewModel
            {
                iOUPayment = await iOUService.GetIOUPaymentById(id),
            };
            model.iOUDetails = await iOUService.GetIOUDetailsByMasterId(Convert.ToInt32(model.iOUPayment.IOUId));
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> IOUAdjust(int? IOUPayId, decimal? ReturnAmount, decimal? PaymentAmount, decimal? AdjustmentAmount, DateTime? AdjustmentDate)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            try
            {
                IOUPayment data = new IOUPayment
                {
                    Id = Convert.ToInt32(IOUPayId),
                    adjustmentAmount = AdjustmentAmount,
                    adjustmentDate = AdjustmentDate,
                    balance = ReturnAmount,
                    adjustmentBy = userInfos.UserId,
                    statusInfoId = 6
                };
                int masterId = await iOUService.UpdateIOUPayment(data);

                return Json(masterId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> IOUPaymentReport()
        {
            //string userName = HttpContext.User.Identity.Name;
            //var userInfos = await userInfo.GetUserInfoByUser(userName);
            //var user = userInfo.GetUserInfo();
            IOUViewModel model = new IOUViewModel
            {
                projects = await projectService.GetProjectList(),
            };
            return View(model);
        }

        #region api
        [Route("global/api/getIOUPaymentStatus/{projectId}/{fromDate}/{toDate}")]
        [HttpGet]
        public async Task<IActionResult> GetIOUPaymentStatus(int projectId, DateTime? fromDate, DateTime? toDate)
        {
            string userName = HttpContext.User.Identity.Name;
            return Json(await iOUService.GetIOUPaymentStatus(projectId, fromDate, toDate));
        }

       // [Route("global/api/getsupp/{Id}")]
        [HttpGet]
        public async Task<IActionResult> getsupp(int Id)
        {
          
            return Json(await iOUService.GetRFQSupDetailsbyrfqid(Id));
        }
        [HttpGet]
        public async Task<IActionResult> getsuppR(int Id)
        {
            var supplierdata = await iOUService.GetRFQPriceMasterbyrfqmasterId(Id);
            List<int?> supplierIds = supplierdata.Select(x => x.supplierId).ToList();
            var suppdata = await iOUService.GetRFQSupDetailsbyrfqid(Id);
            suppdata = suppdata.Where(x => !supplierIds.Contains(x.supplierId));
            return Json(suppdata);
        }
        #endregion

    }
}