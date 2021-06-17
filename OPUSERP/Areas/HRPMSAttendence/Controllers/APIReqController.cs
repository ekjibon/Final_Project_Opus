using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.SCMRequisition.Models;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.Requisition;
using OPUSERP.SCM.Helpers;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using OPUSERP.SCM.Services.Matrix.Interfaces;
using OPUSERP.SCM.Services.Requisition.Interfaces;
using OPUSERP.SCM.Services.SCMMail.interfaces;

namespace OPUSERP.Areas.HRPMSAttendence.Controllers
{
    public class APIReqController : Controller
    {
       

        private readonly IRequisitionService requisitionService;
        private readonly IItemsService itemsService;
        private readonly IApprovalLogService approvalLogService;
        private readonly IUserInfoes userInfoes;
     
        private readonly IProjectService projectService;
        private readonly IStatusLogService statusLogService;
        private readonly RequisitionStatusHistory requisitionStatusHistory;
        private readonly ISCMMailService sCMMailService;
        private readonly IApprovalMatrixService approvalMatrixService;
        public IActionResult Index()
        {
            return View();
        }

        public APIReqController(IApprovalMatrixService approvalMatrixService, IRequisitionService requisitionService, IItemsService itemsService, IApprovalLogService approvalLogService, IUserInfoes userInfoes, IProjectService projectService, IStatusLogService statusLogService, RequisitionStatusHistory requisitionStatusHistory, ISCMMailService sCMMailService)
        {
       
            this.approvalMatrixService = approvalMatrixService;
            this.requisitionService = requisitionService;
            this.itemsService = itemsService;
            this.approvalLogService = approvalLogService;
            this.userInfoes = userInfoes;
            this.projectService = projectService;
            this.statusLogService = statusLogService;
            this.requisitionStatusHistory = requisitionStatusHistory;
            this.sCMMailService = sCMMailService;
        }

        [Route("global/api/getRequisitionDetais/{name}/{reqId}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> getRequisitionDetais(string name,int Id)
        {
            string reqBy =name;

            var userInfo = await userInfoes.GetUserInfoByUser(reqBy);
            ViewBag.DepartmentName = userInfo.DivisionName;
            ViewBag.masterId = Id;
            var project = requisitionService.GetProjectByUser(reqBy);
            RequisitionViewModel model = new RequisitionViewModel
            {
                projects = await project,
                requisitionMaster = await requisitionService.GetRequisitionMasterList(userInfo.UserId),
                GetReqAutoNumberBySp = await requisitionService.GetReqAutoNumberBySp(Id,reqBy),
                    specificationCategories = await itemsService.GetAllSpecificationCategory(),
            };
            return Json(model);
        }
        [Route("global/api/getRequisitionMasterListForViewStatus/{fromDate}/{toDate}/{type}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetRequisitionMasterListForViewStatus(DateTime? fromDate, DateTime? toDate, string type)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfo = await userInfoes.GetUserInfoByUser(userName);
            return Json(await requisitionService.GetAllRequisitionMasterListForViewStatus(fromDate, toDate, type,userInfo.UserId));
        }

        [Route("global/api/getAllStatusLogListByReqId/{reqId}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllStatusLogListByReqId(int reqId)
        {
            return Json(await statusLogService.GetStatusLogListByReqId(reqId));
        }

        //[Route("global/api/getRequisitionMasterListForViewStatus/{fromDate}/{toDate}/{type}")]
        //[AllowAnonymous]
        //[HttpGet]
        //public async Task<ActionResult> ReturnRequisition()
        //{
        //    string userName = HttpContext.User.Identity.Name;
        //    var userInfo = await userInfoes.GetUserInfoByUser(userName);
        //    RequisitionViewModel model = new RequisitionViewModel
        //    {
        //        requisitionMaster = await requisitionService.GetRequisitionMasterListByPRStatus(userInfo.UserId, 4),
        //               };
        //    return View(model);
        //}


        //[Route("global/api/getRequisitionMasterListForViewStatus/{fromDate}/{toDate}/{type}")]
        //[AllowAnonymous]
        //[HttpGet]
        //public async Task<ActionResult> RejectRequisition()
        //{
        //    string userName = HttpContext.User.Identity.Name;
        //    var userInfo = await userInfoes.GetUserInfoByUser(userName);
        //    RequisitionViewModel model = new RequisitionViewModel
        //    {
        //        requisitionMaster = await requisitionService.GetRequisitionMasterListByPRStatus(userInfo.UserId, 4),
        //          };
        //    return Json(model);
        //}


        [Route("global/api/GetItemSpecificationByitemId/{ItemId}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetItemSpecificationByitemId(string ItemId)
        {
            return Json(await itemsService.GetAllItemSpecificationByitemName(ItemId));
        }
        [Route("global/api/GetAllRequisitionMasterByReqId/{reqId}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllRequisitionMasterByReqId(int reqId)
        {
            return Json(await requisitionService.GetRequisitionMasterByReqId(reqId));
        }

        [Route("global/api/GetAllItemListByRequisitionId/{reqId}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllItemListByRequisitionId(int reqId)
        {
            return Json(await requisitionService.GetItemListByRequisitionId(reqId));
        }

        [Route("global/api/GetAllPreferableVendorList/{reqId}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllPreferableVendorList(int reqId)
        {
            return Json(await requisitionService.GetPreferableVendorList(reqId));
        }

        [Route("global/api/GetAllMasterWiseRequisitionAttachment/{MasterId}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllMasterWiseRequisitionAttachment(string MasterId)
        {
            var Re_DetailList = await requisitionService.GetDocumentAttachmentList(Convert.ToInt32(MasterId));
            return Json(Re_DetailList);
        }

         [Route("global/api/GetItemForRequisition")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetItemForRequisition()
        {
            var Re_DetailList = await itemsService.GetAllItemForRequisition();
            return Json(Re_DetailList);
        }


        [Route("global/api/GetPRApproverPanelList/{userName}/{projectId}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetPRApproverPanelList(string userName,int projectId)
        {
            var result = await approvalMatrixService.GetPRApproverPanelList(userName, 1, projectId);
            return Json(result);
        }



       


        //POST

        [Route("api/SaveRequisitionDetails")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SaveRequisitionDetails([FromBody]RequisitionViewModel model)
        {
            try
            {
                string userName = model.userName;
            
           
                var userInfo = await userInfoes.GetUserInfoByUser(userName);
                var GetReqAutoNumberBySp = await requisitionService.GetReqAutoNumberBySp(model.projectId,userName);
                string _reqNo = GetReqAutoNumberBySp.AutoNumber;
                int proj = model.projectId;
                
     RequisitionMaster entity = new RequisitionMaster
                {
                    Id = model.reqMasterId,
                    reqNo = _reqNo,
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
                    createdBy = model.userName,
                   
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
                    //string itemSpecificationName = data.itemSpecificationId;
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
                    detailEntity.reqQty = data.reqQty;
                    detailEntity.reqRate = data.reqRate;
                    detailEntity.targetDate = data.targetDate;
                    detailEntity.justification = data.justification;
                    var detailId = await requisitionService.SaveRequisitionDetail(detailEntity);

                    detailEntity = new RequisitionDetail();
                }
              
                if (model.reqType == "Final" && model.reqMasterId == 0)
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
                            nextAppId = model.panels[i].userId;
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
                        var ReqDeails = await requisitionService.GetRequisitionDetailByitemIdandspecandReqId(model.preferableVendors[i].itemId, Convert.ToInt32(model.preferableVendors[i].itemSpecificationId), Convert.ToInt32(masterId));
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
                await requisitionStatusHistory.SaveRequisitionStatusLog(masterId, 1, Convert.ToInt32(userInfo.UserTypeId), userInfo.UserId, empNameCode, nextEmpNameCode, "", 1, "PR", masterId, _reqNo);
            
               


                return Json(masterId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}