using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRO.Models;
using OPUSERP.CRO.Data.Entity.DistributeJob;
using OPUSERP.CRO.Services.DistributeJob.Interfaces;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPService.MasterData.Interfaces;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.Helpers;

namespace OPUSERP.Areas.CRO.Controllers
{
    [Area("CRO")]
    public class TLAssignmentController : Controller
    {
        private readonly IDistributeJobService distributeJobService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserInfoes userInfo;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly ITeamService teamService;
        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public TLAssignmentController(IHostingEnvironment hostingEnvironment, ITeamService teamService, IDistributeJobService distributeJobService, IUserInfoes userInfo, IAttachmentCommentService attachmentCommentService, IConverter converter)
        {
            this.distributeJobService = distributeJobService;
            this._hostingEnvironment = hostingEnvironment;
            this.userInfo = userInfo;
            this.teamService = teamService;
            this.attachmentCommentService = attachmentCommentService;
            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        public async Task<IActionResult> EthicalPreview(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            LeadInCroViewModel model = new LeadInCroViewModel
            {
                statusLogs = await distributeJobService.GetStatusLogByOperationMasterId(id),
                receiveDocuments = await distributeJobService.GetAllReceiveDocumentByOperMstid(id),
                croAttachmentViewModels = await attachmentCommentService.GetCroDocumentAttachment(id, "Document", 13),
                getLeadInfoInCroViewModels = await distributeJobService.GetLeadInfoInCroByOPMstId(id)
            };
            return View(model);
        }

        public IActionResult EthicalPreviewPdf(int Id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            url = scheme + "://" + host + "/CRO/TLAssignment/EthicalPrintview?Id=" + Id;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        public async Task<IActionResult> EthicalPrintview(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            LeadInCroViewModel model = new LeadInCroViewModel
            {
                statusLogs = await distributeJobService.GetStatusLogByOperationMasterId(id),
                receiveDocuments = await distributeJobService.GetAllReceiveDocumentByOperMstid(id),
                croAttachmentViewModels = await attachmentCommentService.GetCroDocumentAttachment(id, "Document", 13),
                getLeadInfoInCroViewModels = await distributeJobService.GetLeadInfoInCroByOPMstId(id)
            };
            return View(model);
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
          
            OperationMasterViewModel model = new OperationMasterViewModel()
            {
                //operationMasters = await distributeJobService.GetOperationMasterByTeamLeader(empCode),
                getOMByassignToReviewerViewModels= await distributeJobService.GetOperationMasterByTeamLeaderSp(empCode),

            };
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> Index([FromForm] OperationMasterViewModel model)
        {

            string userName = User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            string empCode = "";
            if (empCode != null)
            {
                empCode = userInfos.aspnetId;
            }

            var team = await teamService.GetTeamByaspnetuserId(empCode);

            int jobStatusId = 1053;
            int declaration = 3;
            if (team.teamId != null)
            {
               jobStatusId = 1054;
               declaration = 5;
      
            }
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