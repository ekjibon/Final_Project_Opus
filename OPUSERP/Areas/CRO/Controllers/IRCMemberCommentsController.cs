using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRO.Models;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.CRM.Services.Proposal.Interfaces;
using OPUSERP.CRO.Data.Entity.DistributeJob;
using OPUSERP.CRO.Services.DistributeJob.Interfaces;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;

namespace OPUSERP.Areas.CRO.Controllers
{
    [Area("CRO")]
    public class IRCMemberCommentsController : Controller
    {
        private readonly IDistributeJobService distributeJobService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserInfoes userInfo;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IBankBranchService bankBranchService;
        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public IRCMemberCommentsController(IHostingEnvironment hostingEnvironment, IBankBranchService bankBranchService, IDistributeJobService distributeJobService, IUserInfoes userInfo, IPersonalInfoService personalInfoService, IAttachmentCommentService attachmentCommentService, IConverter converter)
        {
            this.distributeJobService = distributeJobService;
            this._hostingEnvironment = hostingEnvironment;
            this.userInfo = userInfo;
            this.attachmentCommentService = attachmentCommentService;
            this.personalInfoService = personalInfoService;
            this.bankBranchService = bankBranchService;
            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        public async Task<IActionResult> Index()
        { 
            IRCMemberCommentsViewModel model = new IRCMemberCommentsViewModel()
            {
                operationMasters = await distributeJobService.GetAllOperationMaster(),
                LeadsBankInfos = await bankBranchService.GetLeadsBankInfo(),
                employeeInfos=await personalInfoService.GetEmployeeInfo()

            };           
            return View(model);
        }

        public async Task<IActionResult> IRCMember()
        {
            DistributeJobAdminViewModel model = new DistributeJobAdminViewModel()
            {

                GetRCSignatories=await distributeJobService.GetAllIRCSignatory()

            };
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> IRCMember([FromForm] DistributeJobAdminViewModel model)
        {
            
           

            try
            {
                IRCSignatory data = new IRCSignatory
                {
                    Id = Convert.ToInt32(model.ircSignatoryId),
                   
                    employeeInfoId = model.employeeId,
                    serialNo = model.serialNo
                };

                int DocumentId = await distributeJobService.SaveIRCSignatory(data);

                return Json(1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<JsonResult> SaveComments([FromForm] IRCMemberCommentsViewModel model)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos.EmpCode);

            int? empId = 0;
            if (empId != null)
            {
                empId = EmpInfo.Id;
            }

            try
            {
                IRCMemberComments data = new IRCMemberComments
                {
                    Id = Convert.ToInt32(model.ircMemberCommentsId),
                    operationMasterId = Convert.ToInt32(model.operationMasterId),
                    employeeInfoId = Convert.ToInt32(empId),
                    comments = model.comments
                };

                int DocumentId = await distributeJobService.SaveIRCMemberComments(data);

                return Json(DocumentId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateJobStatusAfterSaveComments([FromForm] IRCMemberCommentsViewModel model)
        {           
            try
            {
                StatusLog status = new StatusLog
                {
                    Id = 0,
                    operationMasterId =Convert.ToInt32(model.operationMasterId),
                    jobStatusId = 27,
                    currentUser = HttpContext.User.Identity.Name
                };
                int statusId = await distributeJobService.SaveStatusLog(status);

                var masterId = await distributeJobService.UpdateOperationMasterArchieved(model.operationMasterId, 27);
                return Json(masterId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIRCMemberCommentsByOperMstid(int Id)
        {
            return Json(await distributeJobService.GetAllIRCMemberCommentsByOperMstid(Id));
        }
       

        [HttpPost]
        public async Task<JsonResult> DeleteIRCMemberComments(int Id)
        {
            await distributeJobService.DeleteIRCMemberCommentsById(Id);
            return Json(true);
        }
    }
}