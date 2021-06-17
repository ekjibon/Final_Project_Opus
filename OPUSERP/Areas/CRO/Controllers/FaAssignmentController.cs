using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRO.Models;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.CRO.Data.Entity.DistributeJob;
using OPUSERP.CRO.Services.DistributeJob.Interfaces;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.Helpers;

namespace OPUSERP.Areas.CRO.Controllers
{
    [Area("CRO")]
    public class FaAssignmentController : Controller
    {
        private readonly IDistributeJobService distributeJobService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserInfoes userInfo;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IBankBranchService bankBranchService;
        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public FaAssignmentController(IHostingEnvironment hostingEnvironment, IBankBranchService bankBranchService, IDistributeJobService distributeJobService, IUserInfoes userInfo, IAttachmentCommentService attachmentCommentService, IConverter converter)
        {
            this.distributeJobService = distributeJobService;
            this._hostingEnvironment = hostingEnvironment;
            this.userInfo = userInfo;
            this.attachmentCommentService = attachmentCommentService;
            this.bankBranchService = bankBranchService;
            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }
        public async Task<IActionResult> Index()
        {

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            string empCode = "";
            if (empCode != null)
            {
                empCode = userInfos.EmpCode;
            }
            var opmaster = await distributeJobService.GetOperationMasterByFAEDeclaration(empCode);
            OperationMasterViewModel model = new OperationMasterViewModel()
            {
                operationMasters = opmaster,
                LeadsBankInfos = await bankBranchService.GetLeadsBankInfo()

            };
            return View(model);
        }
        public async Task<IActionResult> DeclaredList(int? opMstId)
        {
            int? mstid = 0;
            if (opMstId == null || opMstId == 0)
            {
                mstid = 0;
            }
            else
            {
                mstid = opMstId;
            }
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            //var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos.EmpCode);
            string empCode = "";
            if (empCode != null)
            {
                empCode = userInfos.EmpCode;
            }

            AttachmentStatusViewModel model = new AttachmentStatusViewModel()
            {
                //operationMasters = await distributeJobService.GetOperationMasterByAssignTo(empCode),
                getOMByassignToDeclareViewModels = await distributeJobService.GetOMByassignToDeclareViewModels(empCode),
                attachmentTypes = await distributeJobService.GetAllAttachmentType(),
                documentCharts = await distributeJobService.GetAllDocumentChart(),
                jobStatuses = await distributeJobService.GetAllJobStatus(),
                ratingValues = await distributeJobService.GetAllRatingValue()
            };

            ViewBag.opMstId = mstid;
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> Index([FromForm] OperationMasterViewModel model)
        {

            string userName = User.Identity.Name;
            int jobStatusId = 1054;
            int declaration = 5;

            if (model.opMasterId != null)
            {
                if (model.opMasterId.Length > 0)
                {

                    for (int i = 0; i < model.opMasterId.Length; i++)
                    {

                        await distributeJobService.UpdateOperationMasterEthicalDeclaration(Convert.ToInt32(model.opMasterId[i]), jobStatusId, declaration, model.tLDeclarationDate, model.remarks[i]);

                        StatusLog log = new StatusLog();

                        log.Id = 0;
                        log.operationMasterId = Convert.ToInt32(model.opMasterId[i]);
                        log.jobStatusId = jobStatusId;
                        log.remarks = model.remarks[i];
                        log.currentUser = userName;
                        log.createdAt = DateTime.Now;
                        log.createdBy = userName;
                        await distributeJobService.SaveStatusLog(log);
                        log = new StatusLog();

                    }

                }
            }


            return Json(1);

        }
        [HttpPost]
        public async Task<JsonResult> Decline([FromForm] OperationMasterViewModel model)
        {

            string userName = User.Identity.Name;
            if (model.opMasterId != null)
            {
                if (model.opMasterId.Length > 0)
                {

                    for (int i = 0; i < model.opMasterId.Length; i++)
                    {
                        await distributeJobService.DeleteStatusLogByOperationMasterId(Convert.ToInt32(model.opMasterId[i]));
                        await distributeJobService.DeleteOperationMasterById(Convert.ToInt32(model.opMasterId[i]));

                    }

                }
            }
            return Json(1);

        }
    }
}