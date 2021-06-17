using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRO.Models;
using OPUSERP.CRM.Services.Client.Interfaces;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.CRO.Data.Entity.DistributeJob;
using OPUSERP.CRO.Services.DistributeJob.Interfaces;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPService.MasterData.Interfaces;
using OPUSERP.ERPServices.EmailService.interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;

namespace OPUSERP.Areas.CRO.Controllers
{
    [Area("CRO")]
    public class AssignController : Controller
    {
        private readonly IAgreementService agreementService;
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly IDistributeJobService distributeJobService;
        private readonly IUserInfoes userInfoes;
        private readonly IEmailSenderService emailSenderService;
        private readonly IERPCompanyService eRPCompanyService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly ITeamService teamService;
        private readonly IUserInfoes userInfo;
        private readonly ILeadsService leadsService;
        private readonly IClientService clientService;

        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public AssignController(IAgreementService agreementService, IEmailSenderService emailSenderService,IClientService clientService, ILeadsService leadsService, IHostingEnvironment hostingEnvironment, IContactsService contactsService, IDistributeJobService distributeJobService, IUserInfoes userInfoes, IConverter converter, IERPCompanyService eRPCompanyService, IPersonalInfoService personalInfoService, ITeamService teamService, IUserInfoes userInfo)
        {
            this.agreementService = agreementService;
            this.distributeJobService = distributeJobService;
            this._hostingEnvironment = hostingEnvironment;
            this.userInfoes = userInfoes;
            this.eRPCompanyService = eRPCompanyService;
            this.personalInfoService = personalInfoService;
            this.teamService = teamService;
            this.userInfo = userInfo;
            this.leadsService = leadsService;
            this.clientService = clientService;
            this.emailSenderService = emailSenderService;

            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }
        public ActionResult AssignList()
        {
            return View();
        }
        public ActionResult ReAssignList()
        {
            return View();
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
            DistributeJobAdminViewModel model = new DistributeJobAdminViewModel()
            {
                getAgreementForCROratingViewModels = await agreementService.GetAgreementForCROrating(empCode),
                aspNetUsers = await userInfoes.GetUserInfoByModule(2),
            };
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> Index([FromForm] DistributeJobAdminViewModel model)
        {

            string userName = User.Identity.Name;
            int jobStatusId = 0;
            int declaration = 0;
            DateTime? TLassignDate = DateTime.Now;
            DateTime? FAassignDate = DateTime.Now;


            if (model.assignTo != null)
            {
                jobStatusId = 50;
                declaration = 4;
                TLassignDate = null;
                FAassignDate = model.assignDate;
            }
            else
            {
                jobStatusId = 49;
                declaration = 2;
                TLassignDate = model.assignDate;
                FAassignDate = null;
            }


            if (model.agreementId != null)
            {
                if (model.agreementId.Length > 0)
                {

                    for (int i = 0; i < model.agreementId.Length; i++)
                    {
                        OperationMaster OM = new OperationMaster();

                        OM.Id = 0;
                        OM.agreementId = Convert.ToInt32(model.agreementId[i]);
                        OM.approvedforCroId = Convert.ToInt32(model.approvedforCroId[i]);
                        OM.jobStatusId = jobStatusId;
                        OM.declaration = declaration;
                        OM.assignTeam = model.assignTeam;
                        OM.assignTeamLeader = model.assignTeamLeader;
                        OM.assignTeamDate = TLassignDate;
                        OM.assignDate = FAassignDate;
                        OM.assignTo = model.assignTo;
                        OM.comments = model.remarks[i];
                        int OPMID = await distributeJobService.SaveOperationMaster(OM);

                        StatusLog log = new StatusLog();

                        log.Id = 0;
                        log.operationMasterId = OPMID;
                        log.jobStatusId = jobStatusId;
                        log.remarks =model.remarks[i];
                        log.currentUser = userName;
                        log.createdAt = DateTime.Now;
                        log.createdBy = userName;
                        await distributeJobService.SaveStatusLog(log);
                        OM = new OperationMaster();
                        log = new StatusLog();

                        try
                        {
                            var userInfos = await userInfoes.GetUserInfoByUser(userName);
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

                            var aggrementDetails = await agreementService.GetAgreementDetailsByaggId((int)model.agreementId[i]);
                            
                            string html = "<div><strong>Assigned to you for Rating</strong></div>"
                                    + " <br/>"
                                    + "<p>Dear Sir,</p>"
                                    + " <br/>"
                                    + " This is to inform you that " + aggrementDetails.ratingYear.ratingTypeName + " of lead : " + aggrementDetails.agreement.leads.leadName + " is Assigned to you for rating."
                                    + "<br/>"
                                    + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' >  " + email + " </p></div>"
                                    + " <br/>";

                            if (model.assignTeamLeader != null)
                            {
                                var emp = await personalInfoService.GetEmployeeInfoByCode(model.assignTeamLeader);
                                if (emp.emailAddress != null)
                                {
                                        await emailSenderService.SendEmailWithFrom(emp.emailAddress, email, "Job Assign for Rating " + aggrementDetails.ratingYear.ratingTypeName + " of lead : " + aggrementDetails.agreement.leads.leadName, html);
                                }
                            }

                            if (model.assignTo != null)
                            {
                                var emp = await personalInfoService.GetEmployeeInfoByCode(model.assignTo);
                                if (emp.emailAddress != null)
                                {
                                    await emailSenderService.SendEmailWithFrom(emp.emailAddress, email, "Job Assign for Rating " + aggrementDetails.ratingYear.ratingTypeName + " of lead : " + aggrementDetails.agreement.leads.leadName, html);
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
        [HttpGet]
        public async Task<JsonResult> HoldJob(int Id,int agreementId,string remarks)
        {

            string userName = User.Identity.Name;
          
            DateTime? TLassignDate = DateTime.Now;
            DateTime? FAassignDate = DateTime.Now;
          
            FAassignDate = null;

            OperationMaster OM = new OperationMaster();

            OM.Id = 0;
            OM.agreementId = agreementId;
            OM.approvedforCroId = Id;
          
           OM.comments = remarks;
            int OPMID = await distributeJobService.SaveOperationMaster(OM);

            await distributeJobService.UpdateOperationMaster(OPMID, 35, userName);

            StatusLog log = new StatusLog();

            log.Id = 0;
            log.operationMasterId = OPMID;
            log.jobStatusId = 35;
            log.remarks = "Hold";
            log.currentUser = userName;
            log.createdAt = DateTime.Now;
            log.createdBy = userName;
            await distributeJobService.SaveStatusLog(log);
            OM = new OperationMaster();
            log = new StatusLog();

            return Json(1);

        }

        [HttpGet]
        public async Task<JsonResult> BackJob(int Id,int agreementId,string remarks)
        {
            string userName = User.Identity.Name;          
            DateTime? TLassignDate = DateTime.Now;
            DateTime? FAassignDate = DateTime.Now;
            FAassignDate = null;
            var agreement = await agreementService.GetAgreementById(agreementId);
            await clientService.DeletClientsByleadId(agreementId);
            await agreementService.UpdateForAgreementVerification(agreementId, 1, remarks);          
            return Json(1);

        }
        #region API Section

        [Route("global/api/GetEmployeeInfoCROTeamLeader")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeeInfoCROTeamLeader()
        {
            return Json(await personalInfoService.GetEmployeeInfoCROTeamLeader());
        }
        [Route("global/api/GetEmployeeInfoCROAnalyst")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeeInfoCROAnalyst()
        {
            return Json(await personalInfoService.GetEmployeeInfoCROAnalyst());
        }
        [Route("global/api/GetEmployeeInfoCRMAnalyst")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeeInfoCRMAnalyst()
        {
            return Json(await personalInfoService.GetEmployeeInfoCRMAnalyst());
        }
        [Route("global/api/GetEmployeeInfoCROAnalystByTeamId")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeeInfoCROAnalystByTeamId()
        {
            string username = HttpContext.User.Identity.Name;
            var userinfo = await userInfoes.GetUserInfoByUser(username);
            var team = await teamService.GetTeamByaspnetuserId(userinfo.aspnetId);
            int teamId = 0;
            if (team !=null)
            {
                teamId = team.Id;
            }
            return Json(await personalInfoService.GetEmployeeInfoCROAnalystByTeamId(teamId));
        }
        [Route("global/api/GetEmployeeInfoCROReview")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeeInfoCROReview()
        {
            return Json(await personalInfoService.GetEmployeeInfoCROReview());
        }

        [Route("global/api/GetOperationmasterjadata/{fromDate}/{toDate}")]
        [HttpGet]
        public async Task<IActionResult> GetOperationmasterjadata(string fromdate, string todate)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            string empCode = "";
            if (empCode != null)
            {
                empCode = userInfos.EmpCode;
            }
            if (fromdate != null)
            {
                fromdate = Convert.ToDateTime(fromdate).ToString("yyyyMMdd");
                todate = Convert.ToDateTime(todate).ToString("yyyyMMdd");
            }
            else
            {
                fromdate = string.Empty;
                todate = string.Empty;
            }
            return Json(await distributeJobService.OperationMasterSPJAViewModels(fromdate, todate, empCode));
        }

        [HttpGet]
        public async Task<IActionResult> GetOperationmasterjaudata()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            string empCode = "";
            if (empCode != null)
            {
                empCode = userInfos.EmpCode;
            }
            string fromdate = string.Empty;
            string todate = string.Empty;
            return Json(await distributeJobService.OperationMasterSPJAViewModels(fromdate, todate, empCode));
        }

        [HttpGet]
        public async Task<IActionResult> GetOperationMasterByMstId(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            string empCode = "";
            if (empCode != null)
            {
                empCode = userInfos.EmpCode;
            }

            var data = await  distributeJobService.GetAgreementInfoViewModels(id);
            return Json(data.FirstOrDefault());
        }
        #endregion
    }
}