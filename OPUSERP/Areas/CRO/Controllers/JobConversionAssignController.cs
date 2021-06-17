using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRO.Models;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.CRO.Data.Entity.DistributeJob;
using OPUSERP.CRO.Services.DistributeJob.Interfaces;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.ERPServices.EmailService.interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;

namespace OPUSERP.Areas.CRO.Controllers
{
    [Area("CRO")]
    public class JobConversionAssignController : Controller
    {
        private readonly IDistributeJobService distributeJobService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserInfoes userInfo;
        private readonly IAttachmentCommentService attachmentCommentService;

        private readonly IEmailSenderService emailSenderService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IAgreementService agreementService;

        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public JobConversionAssignController(IHostingEnvironment hostingEnvironment, IEmailSenderService emailSenderService, IAgreementService agreementService, IPersonalInfoService personalInfoService, IDistributeJobService distributeJobService, IUserInfoes userInfo, IAttachmentCommentService attachmentCommentService, IConverter converter)
        {
            this.distributeJobService = distributeJobService;
            this._hostingEnvironment = hostingEnvironment;
            this.userInfo = userInfo;
            this.attachmentCommentService = attachmentCommentService;
            this.emailSenderService = emailSenderService;
            this.personalInfoService = personalInfoService;
            this.agreementService = agreementService;
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
            int jobStatusId = 2058;
            int declaration = 7;

            if (model.opMasterId != null)
            {
                if (model.opMasterId.Length > 0)
                {

                    for (int i = 0; i < model.opMasterId.Length; i++)
                    {

                        await distributeJobService.UpdateOperationMasterConversionAssign(Convert.ToInt32(model.opMasterId[i]), jobStatusId, declaration, model.assignDate, model.remarks[i], model.assignTo);

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

                        try
                        {
                            var userInfos = await userInfo.GetUserInfoByUser(userName);
                            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
                            int empId = 0;
                            string email = "";
                            string name = "";
                            if (EmpInfo != null)
                            {
                                empId = EmpInfo.Id;
                                email = EmpInfo.emailAddress;
                                name = EmpInfo.nameEnglish;
                            }
                            var aggremetn = await distributeJobService.GetOperationMasterById((int)model.opMasterId[i]);
                            var aggrementDetails = await agreementService.GetAgreementDetailsByaggId((int)aggremetn.agreementId);

                            string html = "<div><strong>Assigned to you for conversion</strong></div>"
                                    + " <br/>"
                                    + "<p>Dear Sir,</p>"
                                    + " <br/>"
                                    + " This is to inform you that " + aggrementDetails.ratingYear.ratingTypeName + " of lead : " + aggrementDetails.agreement.leads.leadName + " is Assigned to you for conversion."
                                    + "<br/>"
                                    + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' >  " + email + " </p></div>"
                                    + " <br/>";

                            if (model.assignTo != null)
                            {
                                var emp = await personalInfoService.GetEmployeeInfoByCode(model.assignTo);
                                if (emp.emailAddress != null)
                                {
                                    await emailSenderService.SendEmailWithFrom(emp.emailAddress, email, "Job Assign for conversion " + aggrementDetails.ratingYear.ratingTypeName + " of lead : " + aggrementDetails.agreement.leads.leadName, html);
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            return Json(1);
                        }

                    }

                }
            }


            return Json(1);

        }

    }
}