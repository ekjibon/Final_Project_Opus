using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Accounting.Services.MasterData.Interfaces;
using OPUSERP.Areas.SCMMatrix.Models;
using OPUSERP.Areas.SCMRequisition.Models;
using OPUSERP.Areas.SCMRequisition.Models.Lang;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.Requisition;
using OPUSERP.SCM.Helpers;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using OPUSERP.SCM.Services.Matrix.Interfaces;
using OPUSERP.SCM.Services.PurchaseOrder.Interface;
using OPUSERP.SCM.Services.Requisition.Interfaces;
using OPUSERP.SCM.Services.SCMMail.interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMRequisition.Controllers
{
    [Area("SCMRequisition")]
    public class RequisitionMasterController : Controller
    {
        private readonly IRequisitionService requisitionService;
        private readonly IItemsService itemsService;
        private readonly LangGenerate<RequisitionLN> _lang;
        private readonly IApprovalLogService approvalLogService;
        private readonly IUserInfoes userInfoes;
        private readonly IItemsService ItemsService;
        private readonly IProjectService projectService;
        private readonly IStatusLogService statusLogService;
        private readonly RequisitionStatusHistory requisitionStatusHistory;
        private readonly ISCMMailService sCMMailService;
        private readonly IFundSourceService fundSourceService;
        private readonly IEmployeeInfoService employeeInfoService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IUserTypeService userTypeService;
        private readonly IPurchaseOrderService purchaseOrderService;
        private readonly IApprovalMatrixService approvalMatrixService;

        public RequisitionMasterController(IHostingEnvironment hostingEnvironment, IPurchaseOrderService purchaseOrderService, IPersonalInfoService personalInfoService, IEmployeeInfoService employeeInfoService, IFundSourceService fundSourceService, IStatusLogService statusLogService, RequisitionStatusHistory requisitionStatusHistory,
            IRequisitionService requisitionService, IItemsService itemsService, IApprovalLogService approvalLogService, IUserInfoes userInfoes, IItemsService ItemsService,
            IProjectService projectService, ISCMMailService sCMMailService, IUserTypeService userTypeService, IApprovalMatrixService approvalMatrixService)
        {
            this.purchaseOrderService = purchaseOrderService;
            this.requisitionService = requisitionService;
            this.fundSourceService = fundSourceService;
            this.itemsService = itemsService;
            this.employeeInfoService = employeeInfoService;
            this.personalInfoService = personalInfoService;
            this.approvalLogService = approvalLogService;
            this.userInfoes = userInfoes;
            this.ItemsService = ItemsService;
            this.projectService = projectService;
            this.userTypeService = userTypeService;
            this.statusLogService = statusLogService;
            this.requisitionStatusHistory = requisitionStatusHistory;
            this.sCMMailService = sCMMailService;
            this.approvalMatrixService = approvalMatrixService;
            _lang = new LangGenerate<RequisitionLN>(hostingEnvironment.ContentRootPath);
        }

        public async Task<IActionResult> ViewRequisitionStatus()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfo = await userInfoes.GetUserInfoByUser(userName);
          
            RequisitionViewModel model = new RequisitionViewModel
            {
                //requisitionMaster = await requisitionService.GetRequisitionMasterList(userInfo.UserId),
                //fLang = _lang.PerseLang("Requisition/RequisitionEN.json", "Requisition/RequisitionBN.json", Request.Cookies["lang"]),
            };
            return View(model);
        }

        public async Task<IActionResult> Index()
        {
            string reqBy = HttpContext.User.Identity.Name;
            var userInfo = await userInfoes.GetUserInfoByUser(reqBy);
            RequisitionViewModel model = new RequisitionViewModel
            {
                requisitionMaster = await requisitionService.GetRequisitionMasterList(userInfo.UserId),
                fLang = _lang.PerseLang("Requisition/RequisitionEN.json", "Requisition/RequisitionBN.json", Request.Cookies["lang"]),
            };
            return View(model);
        }
        [Route("api/RequisitionMaster/GetRequisitionNo/{projecId}")]
        public async Task<JsonResult> GetRequisitionNo(int projecId)
        {
            string userName = User.Identity.Name;
           
            var reqno = "";
            var country = await requisitionService.GetAllRequisitionMasterList();
            int count = country.Count();
            var autonumber = await userTypeService.GetAutonumberingInfoById("Requisition");
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
                        ymd = ymd+DateTime.Now.Month.ToString();
                    }
                    if (autonumber.mseparator != null)
                    {
                        ymd = ymd + autonumber.mseparator;
                    }
                    if (autonumber.isdate == 1)
                    {
                        ymd = ymd+DateTime.Now.Day.ToString();
                    }
                    if (autonumber.dseparator != null)
                    {
                        ymd = ymd + autonumber.dseparator;
                    }
                    var sep = "";
                    sep = autonumber.separator;
                    code = autonumber.prefix+ sep+ymd + code;
                }
                reqno = code;

            }
            else
            {
                var GetReqAutoNumberBySp = await requisitionService.GetReqAutoNumberBySp(projecId, userName);
                reqno = GetReqAutoNumberBySp.AutoNumber;
            }
           




            //return Json(GetReqAutoNumberBySp);
            return Json(reqno);
        }

        [HttpGet]
        public async Task<IActionResult> GetCumulativeGRQtyBySpecId(int itemspecId)
        {
            return Json(await requisitionService.GetCumulativeGRQtyBySpecId(itemspecId));
        }

        [HttpGet]
        public async Task<IActionResult> CreateRequisition(int Id)
        {
            string reqBy = HttpContext.User.Identity.Name;

            var userInfo = await userInfoes.GetUserInfoByUser(reqBy);
            ViewBag.DepartmentName = userInfo.DivisionName;
            ViewBag.masterId = Id;
            ViewBag.remarks = "";
            var project =await requisitionService.GetProjectByUser(reqBy);
            var data = await statusLogService.GetStatusLogListbyreqsid(Id);
            if (data.Count()>0)
            {
                ViewBag.remarks = data.OrderByDescending(x => x.Id).FirstOrDefault().remarks;
            }
            RequisitionViewModel model = new RequisitionViewModel
            {
                projects = project,
                requisitionMaster = await requisitionService.GetRequisitionMasterList(userInfo.UserId),
                //GetReqAutoNumberBySp = await requisitionService.GetReqAutoNumberBySp(project.FirstOrDefault().Id, reqBy),
                fLang = _lang.PerseLang("Requisition/RequisitionEN.json", "Requisition/RequisitionBN.json", Request.Cookies["lang"]),
                specificationCategories = await ItemsService.GetAllSpecificationCategory(),
                fundSources = await fundSourceService.GetFundSource(),
                employeeInfos=await personalInfoService.GetEmployeeInfo()
            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> PriceComparison(int Id)
        {
            string reqBy = HttpContext.User.Identity.Name;

           
            RequisitionViewModel model = new RequisitionViewModel
            {
            
                //GetReqAutoNumberBySp = await requisitionService.GetReqAutoNumberBySp(project.FirstOrDefault().Id, reqBy),
                fLang = _lang.PerseLang("Requisition/RequisitionEN.json", "Requisition/RequisitionBN.json", Request.Cookies["lang"]),
              
            };
            return View(model);
        }
        public async Task<ActionResult> RequisitionListForInActive()
        {
            RequisitionViewModel model = new RequisitionViewModel
            {
                activePR = await requisitionService.GetRequisitionMasterListByActivityStatus(1),
                inActivePR = await requisitionService.GetRequisitionMasterListByActivityStatus(0),
            };
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> SaveMasterDetails(RequisitionViewModel model)
        {
            try
            {
                //string userName = HttpContext.User.Identity.Name;
                //var userInfo = await userInfoes.GetUserInfoByUser(userName);
                //var GetReqAutoNumberBySp = await requisitionService.GetReqAutoNumberBySp(model.projectId,userName);
                //string _reqNo = GetReqAutoNumberBySp.AutoNumber;
                string userName = User.Identity.Name;
                var userInfo = await userInfoes.GetUserInfoByUser(userName);
                var autonumber = await userTypeService.GetAutonumberingInfoById("Requisition");
               
               
                var reqno = "";
                var country = await requisitionService.GetAllRequisitionMasterList();
                int count = country.Count();
               
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
                else
                {
                    var GetReqAutoNumberBySp = await requisitionService.GetReqAutoNumberBySp(model.projectId, userName);
                    reqno = GetReqAutoNumberBySp.AutoNumber;
                }

                RequisitionMaster entity = new RequisitionMaster
                {
                    Id = model.reqMasterId,
                    reqNo = reqno,
                    reqDate = model.reqDate,
                    targetDate = model.targetDate,
                    subject = model.subject,
                    reqBy = userInfo.UserId,
                    reqDept = model.reqDept,
                    projectId = model.projectId,
                    reqType = model.reqType,
                    statusInfoId = 1,
                    isActive = 1,
                    isPostPR = model.isPostPR,
                    createdBy = HttpContext.User.Identity.Name,
                    fundSourceId=model.foundsourceId,
                    //deliveryaddress=model.deliveryaddress,
                    //employeeinfoId=model.employeeId
                };
                var masterId = await requisitionService.SaveRequisitionMaster(entity);

                if (model.reqMasterId > 0)
                {
                    await requisitionService.DeleteRequisitionDetailByreqId(masterId);
                }

                RequisitionDetail detailEntity = new RequisitionDetail();
                foreach (var data in model.Details)
                {
                    detailEntity.Id = 0;
                    detailEntity.requisitionMasterId = Convert.ToInt32(masterId);

                    #region ItemSpecification
                    //string itemSpecificationName = data.itemSpecificationName;
                    //var Itemspec = await ItemsService.GetAllItemSpecificationByitemIdandspec(data.itemId, itemSpecificationName);
                    //int itemspecid = 0;
                    //if (Itemspec.Count() > 0)
                    //{
                    //    itemspecid = Itemspec.FirstOrDefault().Id;
                    //}

                    //ItemSpecification itemspec = new ItemSpecification
                    //{
                    //    Id = itemspecid,
                    //    itemId = Convert.ToInt32(data.itemId),
                    //    specificationName = itemSpecificationName,
                    //    isDelete = 0,
                    //    createdBy = HttpContext.User.Identity.Name,
                    //    createdAt = DateTime.Now
                    //};
                    //itemspecid = await ItemsService.SaveItemSpecification(itemspec);
                    #endregion

                    detailEntity.itemSpecificationId = Convert.ToInt32(data.itemSpecificationId);
                    //detailEntity.itemSpecificationId = Convert.ToInt32(itemspecid);
                    detailEntity.reqQty = data.reqQty;
                    detailEntity.reqRate = data.reqRate;
                    detailEntity.targetDate = data.targetDate;
                    detailEntity.justification = data.justification;
                    detailEntity.budgetCode = data.budgetCode;
                    detailEntity.employeeId = data.employeeId;
                    detailEntity.deliveryLocation = data.deliveryLocation;

                    var detailId = await requisitionService.SaveRequisitionDetail(detailEntity);

                    detailEntity = new RequisitionDetail();
                }

                //if (model.reqType == "Final" && model.reqMasterId == 0)
                if (model.reqType == "Final")
                {
                    for (int i = 0; i < model.panels.Count(); i++)
                    {
                        int isactive = 0;
                        int nextAppId = 0;
                        if (i == 1)
                        {
                            isactive = 1;
                            nextAppId = 0;
                        }
                        else if (i == 0)
                        {
                            isactive = 0;
                            nextAppId = model.panels[1].userId;
                        }
                        else
                        {
                            isactive = 0;
                            nextAppId = 0;
                        }

                        ApprovalLog appLog = new ApprovalLog
                        {
                            masterId = masterId,
                            matrixTypeId = 1,
                            userId = model.panels[i].userId,
                            sequenceNo = model.panels[i].sequenceNo,
                            isActive = isactive,
                            nextApproverId = nextAppId
                        };
                        await approvalLogService.SaveApproverLog(appLog);
                    }
                }
                if (model.preferableVendors != null)
                {
                    if (model.reqMasterId > 0)
                    {
                        await requisitionService.DeletePreferableVendorByreqId(masterId);
                    }
                    for (int i = 0; i < model.preferableVendors.Count(); i++)
                    {
                        //var Itemspec = await ItemsService.GetAllItemSpecificationByitemIdandspec(model.preferableVendors[i].itemId, model.preferableVendors[i].itemSpecificationId);
                        //int itemspecid = 0;
                        //if (Itemspec.Count() > 0)
                        //{
                        //    itemspecid = Itemspec.FirstOrDefault().Id;
                        //}
                        var ReqDeails = await requisitionService.GetRequisitionDetailByitemIdandspecandReqId(model.preferableVendors[i].itemId, Convert.ToInt32(model.preferableVendors[i].itemSpecificationId),Convert.ToInt32(masterId));
                        int reqDeatailId = 0;
                        if (ReqDeails.Count() > 0)
                        {
                            reqDeatailId = ReqDeails.FirstOrDefault().Id;

                            PreferableVendor List = new PreferableVendor
                            {
                                Id = 0,
                                requisitionDetailId = reqDeatailId,
                                organizationId = model.preferableVendors[i].supplierId,
                                isDelete = 0,
                                createdBy = HttpContext.User.Identity.Name,
                                createdAt = DateTime.Now
                            };
                            await requisitionService.SavePreferableVendor(List);
                        }                        
                    }
                }

                var nextUserInfo = await userInfoes.GetUserInfoByUserId(model.panels[1].userId);
                string empNameCode = userInfo.EmpCode + "-" + userInfo.EmpName;
                string nextEmpNameCode = nextUserInfo.EmpCode + "-" + nextUserInfo.EmpName;
                await requisitionStatusHistory.SaveRequisitionStatusLog(masterId, 1, Convert.ToInt32(userInfo.UserTypeId), userInfo.UserId, empNameCode, nextEmpNameCode, "", 1, "PR", masterId, reqno);
                string host = HttpContext.Request.Host.ToString();
                string scheme = Request.Scheme;
                string baseUrl = $"" + scheme + "://" + host + "/Auth/Account/Login";

                //Mail service off for BnB
                //if (model.reqType == "Final")
                //{
                //    await sCMMailService.MailMessage(nextUserInfo.Email, _reqNo, 1, empNameCode, baseUrl);
                //}


                  return Json(masterId);

                //return Json(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<JsonResult> UploadAttachment(string id, string[] arrayFileAtach)
        {
            string userName = HttpContext.User.Identity.Name;

            if (arrayFileAtach.Length > 0)
            {
                for (int i = 0; i < arrayFileAtach.Length; i++)
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
                    filePath = "wwwroot/Upload/" + fileName;
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
                        createdBy = userName,
                        subject = arrayFileAtach[i],
                        matrixTypeId = 1
                    };
                    await requisitionService.SaveDocumentAttachment(attachment);
                }
            }
            return Json(true);
        }
        [HttpPost]
        public async Task<JsonResult> UploadAttachmentp(string id, IFormFile[] arrayFileAtach, int[] arrayFileAtachs)
        {
            string userName = HttpContext.User.Identity.Name;

            if (Request.Form.Files.Count > 0)
            {
                int jj = Request.Form.Files.Count() - arrayFileAtachs.Length;
                for (int i = Request.Form.Files.Count()- arrayFileAtachs.Length; i < Request.Form.Files.Count; i++)
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
                    var details = await requisitionService.GetRequisitionDetailListByReqId(Convert.ToInt32(id));
                    int j = 0;
                    j = i - jj;
                    //details = details.Where(x => x.itemSpecificationId == arrayFileAtachs[i - arrayFileAtachs.Length]).ToList();
                    details = details.Where(x => x.itemSpecificationId == arrayFileAtachs[j]).ToList();
                    DocumentAttachmentDetail attachments = new DocumentAttachmentDetail
                    {
                        Id = 0,
                       // requisitionDetailId = Convert.ToInt32(id),
                        filePath = filePath,
                        fileName = fileName,
                        fileType = fileType,
                        createdBy = userName,
                        requisitionDetailId = (int)details.FirstOrDefault().Id,
                        itemSpecificationId = arrayFileAtachs[j]
                    };
                    await requisitionService.SaveDocumentAttachmentDetails(attachments);
                }
            }
            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> SaveMasterDetailsNew(RequisitionViewModel model)
        {
            try
            {
                //string userName = HttpContext.User.Identity.Name;
                //var userInfo = await userInfoes.GetUserInfoByUser(userName);
                //var GetReqAutoNumberBySp = await requisitionService.GetReqAutoNumberBySp(model.projectId,userName);
                //string _reqNo = GetReqAutoNumberBySp.AutoNumber;
                string userName = User.Identity.Name;
                var userInfo = await userInfoes.GetUserInfoByUser(userName);
                var autonumber = await userTypeService.GetAutonumberingInfoById("Requisition");


                var reqno = "";
                var country = await requisitionService.GetAllRequisitionMasterList();
                int count = country.Count();

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
                else
                {
                    var GetReqAutoNumberBySp = await requisitionService.GetReqAutoNumberBySp(model.projectId, userName);
                    reqno = GetReqAutoNumberBySp.AutoNumber;
                }

                RequisitionMaster entity = new RequisitionMaster
                {
                    Id = model.reqMasterId,
                    reqNo = reqno,
                    reqDate = model.reqDate,
                    targetDate = model.targetDate,
                    subject = model.subject,
                    reqBy = userInfo.UserId,
                    reqDept = model.reqDept,
                    projectId = model.projectId,
                    reqType = model.reqType,
                    statusInfoId = 1,
                    isActive = 1,
                    isPostPR = model.isPostPR,
                    createdBy = HttpContext.User.Identity.Name,
                    //deliveryaddress=model.deliveryaddress,
                    //employeeinfoId=model.employeeId
                };
                var masterId = await requisitionService.SaveRequisitionMaster(entity);

                if (model.reqMasterId > 0)
                {
                    await requisitionService.DeleteRequisitionDetailByreqId(masterId);
                }

                RequisitionDetail detailEntity = new RequisitionDetail();
                foreach (var data in model.Details)
                {
                    detailEntity.Id = 0;
                    detailEntity.requisitionMasterId = Convert.ToInt32(masterId);

                    #region ItemSpecification
                    //string itemSpecificationName = data.itemSpecificationName;
                    //var Itemspec = await ItemsService.GetAllItemSpecificationByitemIdandspec(data.itemId, itemSpecificationName);
                    //int itemspecid = 0;
                    //if (Itemspec.Count() > 0)
                    //{
                    //    itemspecid = Itemspec.FirstOrDefault().Id;
                    //}

                    //ItemSpecification itemspec = new ItemSpecification
                    //{
                    //    Id = itemspecid,
                    //    itemId = Convert.ToInt32(data.itemId),
                    //    specificationName = itemSpecificationName,
                    //    isDelete = 0,
                    //    createdBy = HttpContext.User.Identity.Name,
                    //    createdAt = DateTime.Now
                    //};
                    //itemspecid = await ItemsService.SaveItemSpecification(itemspec);
                    #endregion

                    detailEntity.itemSpecificationId = Convert.ToInt32(data.itemSpecificationId);
                    //detailEntity.itemSpecificationId = Convert.ToInt32(itemspecid);
                    detailEntity.reqQty = data.reqQty;
                    detailEntity.reqRate = data.reqRate;
                    detailEntity.targetDate = data.targetDate;
                    detailEntity.justification = data.justification;
                    detailEntity.budgetCode = data.budgetCode;
                    detailEntity.employeeId = data.employeeId;
                    detailEntity.deliveryLocation = data.deliveryLocation;

                    var detailId = await requisitionService.SaveRequisitionDetail(detailEntity);

                    detailEntity = new RequisitionDetail();
                }
                var panels = await approvalMatrixService.GetPRApproverPanelList(userName, 1, 2);
                var res = panels.ToArray();
                //if (model.reqType == "Final" && model.reqMasterId == 0)
                if (model.reqType == "Final")
                {
                    
                    for (int i = 0; i < res.Count(); i++)
                    {
                        int isactive = 0;
                        int nextAppId = 0;
                        if (i == 1)
                        {
                            isactive = 1;
                            nextAppId = 0;
                        }
                        else if (i == 0)
                        {
                            isactive = 0;
                            nextAppId = (int)res[i].nextApproverId;
                        }
                        else
                        {
                            isactive = 0;
                            nextAppId = 0;
                        }

                        ApprovalLog appLog = new ApprovalLog
                        {
                            masterId = masterId,
                            matrixTypeId = 1,
                            userId = (int)res[i].nextApproverId,
                            sequenceNo = (int)res[i].sequenceNo,
                            isActive = isactive,
                            nextApproverId = nextAppId
                        };
                        await approvalLogService.SaveApproverLog(appLog);
                    }
                }
                //if (model.preferableVendors != null)
                //{
                //    if (model.reqMasterId > 0)
                //    {
                //        await requisitionService.DeletePreferableVendorByreqId(masterId);
                //    }
                //    for (int i = 0; i < model.preferableVendors.Count(); i++)
                //    {
                //        //var Itemspec = await ItemsService.GetAllItemSpecificationByitemIdandspec(model.preferableVendors[i].itemId, model.preferableVendors[i].itemSpecificationId);
                //        //int itemspecid = 0;
                //        //if (Itemspec.Count() > 0)
                //        //{
                //        //    itemspecid = Itemspec.FirstOrDefault().Id;
                //        //}
                //        var ReqDeails = await requisitionService.GetRequisitionDetailByitemIdandspecandReqId(model.preferableVendors[i].itemId, Convert.ToInt32(model.preferableVendors[i].itemSpecificationId), Convert.ToInt32(masterId));
                //        int reqDeatailId = 0;
                //        if (ReqDeails.Count() > 0)
                //        {
                //            reqDeatailId = ReqDeails.FirstOrDefault().Id;

                //            PreferableVendor List = new PreferableVendor
                //            {
                //                Id = 0,
                //                requisitionDetailId = reqDeatailId,
                //                organizationId = model.preferableVendors[i].supplierId,
                //                isDelete = 0,
                //                createdBy = HttpContext.User.Identity.Name,
                //                createdAt = DateTime.Now
                //            };
                //            await requisitionService.SavePreferableVendor(List);
                //        }
                //    }
                //}

                var nextUserInfo = await userInfoes.GetUserInfoByUserId(res[1].nextApproverId);
                string empNameCode = userInfo.EmpCode + "-" + userInfo.EmpName;
                string nextEmpNameCode = nextUserInfo.EmpCode + "-" + nextUserInfo.EmpName;
                await requisitionStatusHistory.SaveRequisitionStatusLog(masterId, 1, Convert.ToInt32(userInfo.UserTypeId), userInfo.UserId, empNameCode, nextEmpNameCode, "", 1, "PR", masterId, reqno);
                string host = HttpContext.Request.Host.ToString();
                string scheme = Request.Scheme;
                string baseUrl = $"" + scheme + "://" + host + "/Auth/Account/Login";

                //Mail service off for BnB
                //if (model.reqType == "Final")
                //{
                //    await sCMMailService.MailMessage(nextUserInfo.Email, _reqNo, 1, empNameCode, baseUrl);
                //}


                return Json(masterId);

                //return Json(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> ReturnRequisition()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfo = await userInfoes.GetUserInfoByUser(userName);
            RequisitionViewModel model = new RequisitionViewModel
            {
                requisitionMaster = await requisitionService.GetRequisitionMasterListByPRStatus(userInfo.UserId, 4),
                fLang = _lang.PerseLang("Requisition/RequisitionEN.json", "Requisition/RequisitionBN.json", Request.Cookies["lang"]),
            };
            return View(model);
        }

        public async Task<ActionResult> RejectRequisition()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfo = await userInfoes.GetUserInfoByUser(userName);
            RequisitionViewModel model = new RequisitionViewModel
            {
                requisitionMaster = await requisitionService.GetRequisitionMasterListByPRStatus(userInfo.UserId, 4),
                fLang = _lang.PerseLang("Requisition/RequisitionEN.json", "Requisition/RequisitionBN.json", Request.Cookies["lang"]),
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItemSpecificationByitemId(int ItemId)
        {
            return Json(await itemsService.GetAllItemSpecificationByitemId(ItemId));
        }

        [HttpGet]
        public async Task<IActionResult> GetRequisitionMasterByReqId(int reqId)
        {
            return Json(await requisitionService.GetRequisitionMasterByReqId(reqId));
        }

        [HttpGet]
        public async Task<IActionResult> GetItemListByRequisitionId(int reqId)
        {
            return Json(await requisitionService.GetItemListByRequisitionId(reqId));
        }

        [HttpGet]
        public async Task<IActionResult> GetPreferableVendorList(int reqId)
        {
            return Json(await requisitionService.GetPreferableVendorList(reqId));
        }

        [HttpGet]
        public async Task<IActionResult> GetMasterWiseRequisitionAttachment(string MasterId)
        {
            var Re_DetailList = await requisitionService.GetDocumentAttachmentList(Convert.ToInt32(MasterId));
            return Json(Re_DetailList);
        }


        #region API Section

        [Route("global/api/getAllRequisitionMasterListForViewStatus/{fromDate}/{toDate}/{type}")]
        [HttpGet]
        public async Task<IActionResult> GetAllRequisitionMasterListForViewStatus(DateTime? fromDate, DateTime? toDate, string type)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfo = await userInfoes.GetUserInfoByUser(userName);
            return Json(await requisitionService.GetAllRequisitionMasterListForViewStatus(fromDate, toDate, type,userInfo.UserId));
        }

        [Route("global/api/getStatusLogListByReqId/{reqId}")]
        [HttpGet]
        public async Task<IActionResult> GetStatusLogListByReqId(int reqId)
        {
            return Json(await statusLogService.GetStatusLogListByReqId(reqId));
        }
        [Route("global/api/getitempricecomarison/{Id}")]
        [HttpGet]
        public async Task<IActionResult> getitempricecomarison(int Id)
        {
            return Json(await purchaseOrderService.GetPurchaseOrderDetailsbyspecId(Id));
        }
        [Route("global/api/getitempricecomarisonA/{Id}")]
        [HttpGet]
        public async Task<IActionResult> getitempricecomarisonA(int Id)
        {
            return Json(await purchaseOrderService.GetPurchaseOrderDetailsbyspecIdA(Id));
        }

        [Route("SCMMasterData/Item/getItemSpecificationByItem/{Id}")]
        [HttpGet]
        public async Task<IActionResult> getItemSpecificationByItem(int Id)
        {
            return Json(await itemsService.GetAllItemSpecificationbyitemidnew(Id));
        }

        [Route("global/api/GetRequisitionMasterListForAssign/")]
        [HttpGet]
        public async Task<IActionResult> GetRequisitionMasterListForAssign()
        {
            return Json(await requisitionService.GetRequisitionMasterListForAssign());
        }
        #endregion
    }
}