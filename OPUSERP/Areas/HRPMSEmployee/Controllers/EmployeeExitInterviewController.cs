using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Data.Entity.Recruitment.ExitInterview;
//using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.HRPMS.Services.Recruitment.Interfaces;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Area("HRPMSEmployee")]
    public class EmployeeExitInterviewController : Controller
    {
        private readonly IEmployeeExitInterviewService employeeExitInterviewService;
        private readonly IUserInfoes userInfo;
        private readonly IPersonalInfoService personalInfoService;
        private readonly ISupervisorService supervisorService;
        private readonly IEmployeeProjectActivityService employeeProjectActivityService;
        private readonly IExitInterviewService exitInterviewService;


        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public EmployeeExitInterviewController(IHostingEnvironment hostingEnvironment, IConverter converter, IEmployeeExitInterviewService employeeExitInterviewService, IUserInfoes userInfo, IPersonalInfoService personalInfoService, ISupervisorService supervisorService, IEmployeeProjectActivityService employeeProjectActivityService, IExitInterviewService exitInterviewService)
        {
            this.employeeExitInterviewService = employeeExitInterviewService;
            this.userInfo = userInfo;
            this.personalInfoService = personalInfoService;
            this.supervisorService = supervisorService;
            this.employeeProjectActivityService = employeeProjectActivityService;
            this.exitInterviewService = exitInterviewService;

            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);

            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            EmployeeExitInterviewViewModel employeeExitInterviewViewModel = new EmployeeExitInterviewViewModel
            {

                employeeInfo = await personalInfoService.GetEmployeeInfoById(empId),
                supervisor = await supervisorService.GetFirstSupervisorByEmpId(empId),
                employeeProjectActivities = await employeeProjectActivityService.GetEmployeeProjectActivityByEmpId(empId),
                reasonForLeavings = await employeeExitInterviewService.GetAllReasonForLeaving(),
                commentORSuggestions = await employeeExitInterviewService.GetAllCommentORSuggestion(),
                payAndBenefits = await employeeExitInterviewService.GetAllPayAndBenefits(),
                feelAboutTheFollowings = await employeeExitInterviewService.GetAllFeelAboutTheFollowing(),
                interviewComments = await employeeExitInterviewService.GetAllInterviewComment()

            };

            return View(employeeExitInterviewViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(EmployeeExitInterviewViewModel model)
        {

            // employee info
            ExitInterviewMaster exitInterviewMaster = new ExitInterviewMaster
            {
                employeeId = model.employeeId,
                startDate = model.startDate,
                lastWithdrawnSalary = model.lastWithdrawBlance
            };

            int masterId =  await exitInterviewService.SaveExitInterviewMaster(exitInterviewMaster);


            // Save ExitInterviewReasonOfLeaving
            if(model.reason != null)
            {
                for(int i=0; i<model.reason.Length; i++)
                {

                    ExitInterviewReasonOfLeaving exitInterviewReasonOfLeaving = new ExitInterviewReasonOfLeaving
                    {
                        exitInterviewMasterId = masterId,
                        reasonForLeavingId = model.reason[i]
                    };

                    await exitInterviewService.SaveExitInterviewReasonOfLeaving(exitInterviewReasonOfLeaving);
                }
            }

            // Save ExitInterviewSuggestion
            for (int i = 0; i < model.suggestionId.Length; i++)
            {

                ExitInterviewSuggestion exitInterviewSuggestion = new ExitInterviewSuggestion
                {
                    exitInterviewMasterId = masterId,
                    commentORSuggestionId = model.suggestionId[i],
                    suggestionText = model.suggestion[i]
                };

                await exitInterviewService.SaveExitInterviewSuggestion(exitInterviewSuggestion);
            }

            // Save ExitInterviewPayNBenefits
            for (int i = 0; i < model.payAndBenefitsId.Length; i++)
            {

                ExitInterviewPayNBenefits exitInterviewPayNBenefits = new ExitInterviewPayNBenefits
                {
                    exitInterviewMasterId = masterId,
                    payAndBenefitsId = model.payAndBenefitsId[i],
                    payAndBenefitsText = model.payAndBenefitsAns[i]
                };

                await exitInterviewService.SaveExitInterviewPayNBenefits(exitInterviewPayNBenefits);
            }

            // Save ExitInterviewFeelAndFollowings
            for (int i = 0; i < model.payAndBenefitsId.Length; i++)
            {

                ExitInterviewFeelAndFollowings exitInterviewFeelAndFollowings = new ExitInterviewFeelAndFollowings
                {
                    exitInterviewMasterId = masterId,
                    feelAboutTheFollowingId = model.feelAboutFollowingId[i],
                    feelAboutFollowinsText = model.feelAboutFollowingAns[i]
                };

                await exitInterviewService.SaveExitInterviewFeelAndFollowings(exitInterviewFeelAndFollowings);
            }

            // Save ExitInterviewComment
            for (int i = 0; i < model.interviewCommentId.Length; i++)
            {

                ExitInterviewComment exitInterviewComment = new ExitInterviewComment
                {
                    exitInterviewMasterId = masterId,
                    interviewCommentId = model.interviewCommentId[i],
                    commentText = model.interviewComment[i]
                };

                await exitInterviewService.SaveExitInterviewComment(exitInterviewComment);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ExitInterViewList()
        {

            EmployeeExitInterviewViewModel model = new EmployeeExitInterviewViewModel
            {
                exitInterviewMasters = await exitInterviewService.GetAllExitInterviewMaster()
            };
            return View(model);
        }

        public async Task<IActionResult> ExitInterViewReport(int id, int code)
        {

            EmployeeExitInterviewViewModel model = new EmployeeExitInterviewViewModel
            {
                exitInterviewMaster = await exitInterviewService.GetExitInterviewMasterById(id),

                employeeInfo = await personalInfoService.GetEmployeeInfoById(code),
                supervisor = await supervisorService.GetFirstSupervisorByEmpId(code),
                employeeProjectActivities = await employeeProjectActivityService.GetEmployeeProjectActivityByEmpId(code),

                exitInterviewReasonOfLeaving = await exitInterviewService.GetExitInterviewReasonOfLeavingByMasterId(id),
                exitInterviewSuggestion = await exitInterviewService.GetExitInterviewSuggestionByMasterId(id),
                exitInterviewPayNBenefits = await exitInterviewService.GetExitInterviewPayNBenefitsByMasterId(id),
                exitInterviewFeelAndFollowings = await exitInterviewService.GetExitInterviewFeelAndFollowingsByMasterId(id),
                exitInterviewComment = await exitInterviewService.GetExitInterviewCommentByMasterId(id),
                reasonForLeavings = await employeeExitInterviewService.GetAllReasonForLeaving(),


            };
            return View(model);
        }


        [AllowAnonymous]
        public IActionResult ExitInterViewReportPdf(int id, int code)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;

            string url = scheme + "://" + host + "/HRPMSEmployee/EmployeeExitInterview/ExitInterViewReport?id=" + id + "&&code=" + code;
            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }


    }
}