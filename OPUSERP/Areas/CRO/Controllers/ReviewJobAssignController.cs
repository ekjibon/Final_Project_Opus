using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRO.Models;
using OPUSERP.CRO.Data.Entity.DistributeJob;
using OPUSERP.CRO.Services.DistributeJob.Interfaces;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.Helpers;

namespace OPUSERP.Areas.CRO.Controllers
{
    [Area("CRO")]
    public class ReviewJobAssignController : Controller
    {
        private readonly IDistributeJobService distributeJobService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserInfoes userInfo;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public ReviewJobAssignController(IHostingEnvironment hostingEnvironment, IDistributeJobService distributeJobService, IUserInfoes userInfo, IAttachmentCommentService attachmentCommentService, IConverter converter)
        {
            this.distributeJobService = distributeJobService;
            this._hostingEnvironment = hostingEnvironment;
            this.userInfo = userInfo;
            this.attachmentCommentService = attachmentCommentService;
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

            OperationMasterViewModel model = new OperationMasterViewModel()
            {
                //operationMasters = await distributeJobService.GetAllOperationMaster(),
                getOMByassignToReviewerViewModels = await distributeJobService.GetAllOperationMasterBySp(),
            };
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> Index([FromForm] OperationMasterViewModel model)
        {

            string userName = User.Identity.Name;
            int jobStatusId = 2057;
            int declaration = 15;

            if (model.opMasterId != null)
            {
                if (model.opMasterId.Length > 0)
                {

                    for (int i = 0; i < model.opMasterId.Length; i++)
                    {

                        await distributeJobService.UpdateOperationMasterReviewAssign(Convert.ToInt32(model.opMasterId[i]), jobStatusId, declaration, model.assignDate, model.remarks[i], model.assignTo);

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

    }
}