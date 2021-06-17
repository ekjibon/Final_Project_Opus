using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSLeave.Models;
using OPUSERP.Areas.HRPMSLeave.Models.Lang;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Leave.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.HRPMS.Services.Recruitment.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Data.Entity.Leave;
using OPUSERP.ERPServices.EmailService.interfaces;
using OPUSERP.Areas.Auth.Models;

namespace OPUSERP.Areas.HRPMSLeave.Controllers
{
    [Authorize]
    [Area("HRPMSLeave")]
    public class LeaveApprovalController : Controller
    {
        private readonly LangGenerate<LeaveLn> _lang;
        private readonly ILeaveRegisterService leaveRegisterService;
        private readonly IWagesLeaveRegisterService wagesLeaveRegisterService;
        private readonly ILeaveRouteService leaveRouteService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IUserInfoes userInfo;
        private readonly ILeaveStatusLogService leaveStatusLogService;
        private readonly IEmailSenderService emailSenderService;
        private readonly ILeavePolicyService leavePolicyService;

        public LeaveApprovalController(IHostingEnvironment hostingEnvironment, ILeaveRegisterService leaveRegisterService, ILeaveRouteService leaveRouteService, IUserInfoes userInfo, IPersonalInfoService personalInfoService, ILeaveStatusLogService leaveStatusLogService, IEmailSenderService emailSenderService, IWagesLeaveRegisterService wagesLeaveRegisterService, ILeavePolicyService leavePolicyService)
        {
            _lang = new LangGenerate<LeaveLn>(hostingEnvironment.ContentRootPath);
            this.leaveRegisterService = leaveRegisterService;
            this.leaveRouteService = leaveRouteService;
            this.personalInfoService = personalInfoService;
            this.userInfo = userInfo;
            this.leaveStatusLogService = leaveStatusLogService;
            this.emailSenderService = emailSenderService;
            this.wagesLeaveRegisterService = wagesLeaveRegisterService;
            this.leavePolicyService = leavePolicyService;
        }
        // GET: LeaveApproval
        public async Task<IActionResult> WagesIndex()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }
            LeaveRegisterViewModel model = new LeaveRegisterViewModel
            {
                fLang = _lang.PerseLang("Leave/LeaveApplyEN.json", "Leave/LeaveApplyBN.json", Request.Cookies["lang"]),
                wagesLeaveRegisters = await wagesLeaveRegisterService.GetAllLeaveRegisterPending(),
            };
            return View(model);
        }

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
            LeaveRegisterViewModel model = new LeaveRegisterViewModel
            {
                fLang = _lang.PerseLang("Leave/LeaveApplyEN.json", "Leave/LeaveApplyBN.json", Request.Cookies["lang"]),
                leaveRoutes = await leaveRouteService.GetLeaveRouteByEmpId(empId),
                leaveDays = await leavePolicyService.GetAllLeaveDay(),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WagesAction([FromForm] LeaveApprovalViewModel model)
        {
            await wagesLeaveRegisterService.UpdateLeaveApproval((int)model.leaveId, Convert.ToInt32(model.leaveApprove));
            return RedirectToAction(nameof(WagesIndex));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Action([FromForm] LeaveApprovalViewModel model)
        {
          //  return Json(model);
            LeaveRoute leaveRoute = await leaveRouteService.GetLeaveRouteById((int)model.id);
            int? nextOrder = leaveRoute.routeOrder + 1;
            int Status = 1;
            string mailtext = "";
            if (model.leaveApprove == "Approve")
            {
                Status = 2;
                mailtext = "Approved";
            }
            else if (model.leaveApprove == "Reject")
            {
                Status = 5;
                mailtext = "rejected";
            }
            else
            {
                Status = 4;
                mailtext = "returned";
            }

            await leaveRouteService.UpdateLeaveRoute(leaveRoute.Id, 0);

            LeaveStatusLog leaveStatusLog = new LeaveStatusLog
            {
                employeeId = model.employeeId,
                leaveRegisterId = model.leaveId,
                date = DateTime.Now,
                remarks = model.leaveApprove,
                Status = Status
            };
            await leaveStatusLogService.SaveLeaveStatusLog(leaveStatusLog);

            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string baseUrl = $"" + scheme + "://" + host + "/HRPMSLeave/LeaveApproval";

            string htmlApprove = "<div><strong>Leave Recommendation/Approval.</strong></div>"
                    + " <br/>"
                    + "<p>Dear Sir,</p>"
                    + " <br/>"
                    + " This is to inform you that one leave application is waiting for your recommendation/approval."
                    + "<br/>"
                    + "<div><a href='" + baseUrl + "'><button>Login</button></a></div>"
                    + " <br/>"
                    + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Human Resource Department.</p></div>"
                    + " <br/>";

            var employee = await personalInfoService.GetEmployeeInfoById((int)model.employeeId);
            var leavRegister = await leaveRegisterService.GetLeaveRegisterById((int)model.leaveId);
            var leaveEmployee = await personalInfoService.GetEmployeeInfoById((int)leavRegister.employeeId);

            string html1 = "<div><strong>Leave Application.</strong></div>"
                        + " <br/>"
                        + "<p>Dear Sir,</p>"
                        + " <br/>"
                        + " This is to inform you that your leave application has " + mailtext + " by " + employee.nameEnglish + " just now."
                        + "<br/>"

                        + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Human Resource Department.</p></div>"
                        + " <br/>";

            if (Status == 5)
            {
                await leaveRegisterService.UpdateLeaveApproval((int)model.leaveId, Status);

                if (leaveEmployee.emailAddress != null)
                {
                    await emailSenderService.SendEmailWithFrom(leaveEmployee.emailAddress, employee.nameEnglish, "Leave Application", html1);
                }
                
                return RedirectToAction(nameof(Index));  
            }
            else if (Status == 4)
            {
                await leaveRegisterService.UpdateLeaveApproval((int)model.leaveId, Status);
                if (leaveEmployee.emailAddress != null)
                {
                    await emailSenderService.SendEmailWithFrom(leaveEmployee.emailAddress, employee.nameEnglish, "Leave Application", html1);
                }
                return RedirectToAction(nameof(Index));
            }

            LeaveRoute leaveRouteNew = await leaveRouteService.GetLeaveRouteByRouteOrder((int)leaveRoute.leaveRegisterId, (int)nextOrder);

            if (leaveRouteNew != null)
            {
                var leaveFutureEmployee = await personalInfoService.GetEmployeeInfoById((int)leaveRouteNew?.employeeId);
                await leaveRouteService.UpdateLeaveRoute(leaveRouteNew.Id, 1);
                await leaveRegisterService.UpdateLeaveApproval((int)model.leaveId, Status);
                if (leaveEmployee.emailAddress != null)
                {
                    await emailSenderService.SendEmailWithFrom(leaveEmployee.emailAddress, employee.nameEnglish, "Leave Application", html1);
                }

                if (leaveFutureEmployee.emailAddress != null)
                {
                    await emailSenderService.SendEmailWithFrom(leaveFutureEmployee.emailAddress, employee.nameEnglish, "Leave Application", htmlApprove);
                }
                
            }
            else
            {
                await leaveRegisterService.UpdateLeaveApproval((int)model.leaveId, 3);
                //string html = "<div><strong>Leave Application.</strong></div>"
                //            + " <br/>"
                //            + "<p>Dear Sir,</p>"
                //            + " <br/>"
                //            + " This is to inform you that "
                //            + "leave application"
                //            + " <br/>"
                //            + "Employee Name : '" + leaveEmployee.nameEnglish + "' "
                //            + " <br/>"
                //            + "Leave From : '" + leavRegister.leaveFrom?.ToString("dd-MMM-yyyy") + "' " + " To : '" + leavRegister.leaveTo?.ToString("dd-MMM-yyyy") + "' "
                //            + " <br/>"
                //            + "has approved by " + employee.nameEnglish + " just now."
                //            + "<br/>"
                //            + "<br/>"

                //            + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Human Resource Department.</p></div>"
                //            + " <br/>";
                string html = "<div><strong>Leave application finally approved.</strong></div>"
                                  + " <br/>"
                                  + "Your requested leave application (Start date:" + leavRegister.leaveFrom?.ToString("dd-MMM-yyyy") + ") has been finally approved."
                                  + "<br/><br/>"
                                  + "<b><u>Application Details</u></b>"
                                  + " <br/>"
                                  + "Leave Type : " + leavRegister.leaveType?.nameEn + ""
                                  + " <br/>"
                                  + "Leave Duration :" + " From " + leavRegister.leaveFrom?.ToString("dd-MMM-yyyy") + " " + " To " + leavRegister.leaveTo?.ToString("dd-MMM-yyyy") + " "
                                  + " <br/>"
                                  + "Approver Remarks : "
                                  + "<br/>";
                if (leaveEmployee.emailAddress != null)
                {
                    await emailSenderService.SendEmailWithFrom(leaveEmployee.emailAddress, employee.nameEnglish, "Leave Application", html);
                }
            }

            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ActionAll([FromForm] LeaveApprovalViewModel model)
        {
            //  return Json(model);
          
         
           
            int Status = 2;
            string mailtext = "recommended";

            if (model.registerids != null )
            {
                for (int i = 0; i < model.registerids.Count(); i++)
                {
                    LeaveRoute leaveRoute = await leaveRouteService.GetLeaveRouteById((int)model.ids[i]);
                    LeaveRegister leaveRegister = await leaveRegisterService.GetLeaveRegisterById((int)model.registerids[0]);
                    int? nextOrder = leaveRoute.routeOrder + 1;
                    await leaveRouteService.UpdateLeaveRoute(leaveRoute.Id, 0);

                    LeaveStatusLog leaveStatusLog = new LeaveStatusLog
                    {
                        employeeId = leaveRegister.employeeId,
                        leaveRegisterId = model.registerids[i],
                        date = DateTime.Now,
                        remarks = "Approved",
                        Status = Status
                    };
                    await leaveStatusLogService.SaveLeaveStatusLog(leaveStatusLog);

                    string host = HttpContext.Request.Host.ToString();
                    string scheme = Request.Scheme;
                    string baseUrl = $"" + scheme + "://" + host + "/HRPMSLeave/LeaveApproval";

                    string htmlApprove = "<div><strong>Leave Recommendation/Approval.</strong></div>"
                            + " <br/>"
                            + "<p>Dear Sir,</p>"
                            + " <br/>"
                            + " This is to inform you that one leave application is waiting for your recommendation/approval."
                            + "<br/>"
                            + "<div><a href='" + baseUrl + "'><button>Login</button></a></div>"
                            + " <br/>"
                            + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Human Resource Department.</p></div>"
                            + " <br/>";

                    var employee = await personalInfoService.GetEmployeeInfoById((int)leaveRegister.employeeId);
                   // var leavRegister = await leaveRegisterService.GetLeaveRegisterById((int)model.leaveId);
                    var leaveEmployee = await personalInfoService.GetEmployeeInfoById((int)leaveRegister.employeeId);

                    string html1 = "<div><strong>Leave Application.</strong></div>"
                                + " <br/>"
                                + "<p>Dear Sir,</p>"
                                + " <br/>"
                                + " This is to inform you that your leave application has " + mailtext + " by " + employee.nameEnglish + " just now."
                                + "<br/>"

                                + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Human Resource Department.</p></div>"
                                + " <br/>";


                   

                    LeaveRoute leaveRouteNew = await leaveRouteService.GetLeaveRouteByRouteOrder((int)leaveRoute.leaveRegisterId, (int)nextOrder);

                    if (leaveRouteNew != null)
                    {
                        var leaveFutureEmployee = await personalInfoService.GetEmployeeInfoById((int)leaveRouteNew?.employeeId);
                        await leaveRouteService.UpdateLeaveRoute(leaveRouteNew.Id, 1);
                        await leaveRegisterService.UpdateLeaveApproval((int)model.registerids[i], Status);
                        if (leaveEmployee.emailAddress != null)
                        {
                            await emailSenderService.SendEmailWithFrom(leaveEmployee.emailAddress, employee.nameEnglish, "Leave Application", html1);
                        }

                        if (leaveFutureEmployee.emailAddress != null)
                        {
                            await emailSenderService.SendEmailWithFrom(leaveFutureEmployee.emailAddress, employee.nameEnglish, "Leave Application", htmlApprove);
                        }

                    }
                    else
                    {
                        await leaveRegisterService.UpdateLeaveApproval((int)model.registerids[0], 3);
                        //string html = "<div><strong>Leave Application</strong></div>"
                        //            + " <br/>"
                        //            + "<p>Dear Sir,</p>"
                        //            + " <br/>"
                        //            + " This is to inform you that "
                        //            + "leave application"
                        //            + " <br/>"
                        //            + "Employee Name : '" + leaveEmployee.nameEnglish + "' "
                        //            + " <br/>"
                        //            + "Leave From : '" + leaveRegister.leaveFrom?.ToString("dd-MMM-yyyy") + "' " + " To : '" + leaveRegister.leaveTo?.ToString("dd-MMM-yyyy") + "' "
                        //            + " <br/>"
                        //            + "has approved by " + employee.nameEnglish + " just now."
                        //            + "<br/>"
                        //            + "<br/>"

                        //            + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Human Resource Department.</p></div>"
                        //            + " <br/>";
                        string html = "<div><strong>Leave application finally approved.</strong></div>"
                                    + " <br/>"                                   
                                    + "Your requested leave application (Start date:" + leaveRegister.leaveFrom?.ToString("dd-MMM-yyyy") + ") has been finally approved."                                   
                                    + "<br/><br/>"
                                    + "<b><u>Application Details</u></b>"
                                    + " <br/>"
                                    + "Leave Type : " + leaveRegister.leaveType?.nameEn + ""
                                    + " <br/>"
                                    + "Leave Duration :" + " From " + leaveRegister.leaveFrom?.ToString("dd-MMM-yyyy") + " " + " To " + leaveRegister.leaveTo?.ToString("dd-MMM-yyyy") + " "
                                    + " <br/>"
                                    + "Approver Remarks : "
                                    + "<br/>";
                        if (leaveEmployee.emailAddress != null)
                        {
                            await emailSenderService.SendEmailWithFrom(leaveEmployee.emailAddress, employee.nameEnglish, "Leave Application", html);
                        }
                    }

                }
            }
            
            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ActionAllReject([FromForm] LeaveApprovalViewModel model)
        {
            //  return Json(model);



            int Status = 5;
            string mailtext = "Rejected";

            if (model.registerids != null)
            {
                for (int i = 0; i < model.registerids.Count(); i++)
                {
                    LeaveRoute leaveRoute = await leaveRouteService.GetLeaveRouteById((int)model.ids[i]);
                    LeaveRegister leaveRegister = await leaveRegisterService.GetLeaveRegisterById((int)model.registerids[0]);
                    int? nextOrder = leaveRoute.routeOrder + 1;
                    await leaveRouteService.UpdateLeaveRoute(leaveRoute.Id, 0);

                    LeaveStatusLog leaveStatusLog = new LeaveStatusLog
                    {
                        employeeId = leaveRegister.employeeId,
                        leaveRegisterId = model.registerids[i],
                        date = DateTime.Now,
                        remarks = "Approved",
                        Status = Status
                    };
                    await leaveStatusLogService.SaveLeaveStatusLog(leaveStatusLog);

                    string host = HttpContext.Request.Host.ToString();
                    string scheme = Request.Scheme;
                    string baseUrl = $"" + scheme + "://" + host + "/HRPMSLeave/LeaveApproval";

                    string htmlApprove = "<div><strong>Leave Recommendation/Approval.</strong></div>"
                            + " <br/>"
                            + "<p>Dear Sir,</p>"
                            + " <br/>"
                            + " This is to inform you that one leave application is waiting for your recommendation/approval."
                            + "<br/>"
                            + "<div><a href='" + baseUrl + "'><button>Login</button></a></div>"
                            + " <br/>"
                            + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Human Resource Department.</p></div>"
                            + " <br/>";

                    var employee = await personalInfoService.GetEmployeeInfoById((int)leaveRegister.employeeId);
                    // var leavRegister = await leaveRegisterService.GetLeaveRegisterById((int)model.leaveId);
                    var leaveEmployee = await personalInfoService.GetEmployeeInfoById((int)leaveRegister.employeeId);

                    string html1 = "<div><strong>Leave Application.</strong></div>"
                                + " <br/>"
                                + "<p>Dear Sir,</p>"
                                + " <br/>"
                                + " This is to inform you that your leave application has " + mailtext + " by " + employee.nameEnglish + " just now."
                                + "<br/>"

                                + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Human Resource Department.</p></div>"
                                + " <br/>";

                    if (Status == 5)
                    {
                        await leaveRegisterService.UpdateLeaveApproval((int)model.registerids[i], Status);

                        if (leaveEmployee.emailAddress != null)
                        {
                            await emailSenderService.SendEmailWithFrom(leaveEmployee.emailAddress, employee.nameEnglish, "Leave Application", html1);
                        }

                        return RedirectToAction(nameof(Index));
                    }

                    LeaveRoute leaveRouteNew = await leaveRouteService.GetLeaveRouteByRouteOrder((int)leaveRoute.leaveRegisterId, (int)nextOrder);

                    if (leaveRouteNew != null)
                    {
                        var leaveFutureEmployee = await personalInfoService.GetEmployeeInfoById((int)leaveRouteNew?.employeeId);
                        await leaveRouteService.UpdateLeaveRoute(leaveRouteNew.Id, 1);
                        await leaveRegisterService.UpdateLeaveApproval((int)model.registerids[i], Status);
                        if (leaveEmployee.emailAddress != null)
                        {
                            await emailSenderService.SendEmailWithFrom(leaveEmployee.emailAddress, employee.nameEnglish, "Leave Application", html1);
                        }

                        if (leaveFutureEmployee.emailAddress != null)
                        {
                            await emailSenderService.SendEmailWithFrom(leaveFutureEmployee.emailAddress, employee.nameEnglish, "Leave Application", htmlApprove);
                        }

                    }
                    else
                    {
                        await leaveRegisterService.UpdateLeaveApproval((int)model.registerids[0], 3);
                        string html = "<div><strong>Leave Application.</strong></div>"
                                    + " <br/>"
                                    + "<p>Dear Sir,</p>"
                                    + " <br/>"
                                    + " This is to inform you that "
                                    + "leave application"
                                    + " <br/>"
                                    + "Employee Name : '" + leaveEmployee.nameEnglish + "' "
                                    + " <br/>"
                                    + "Leave From : '" + leaveRegister.leaveFrom?.ToString("dd-MMM-yyyy") + "' " + " To : '" + leaveRegister.leaveTo?.ToString("dd-MMM-yyyy") + "' "
                                    + " <br/>"
                                    + "has approved by " + employee.nameEnglish + " just now."
                                    + "<br/>"
                                    + "<br/>"

                                    + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Human Resource Department.</p></div>"
                                    + " <br/>";
                        if (leaveEmployee.emailAddress != null)
                        {
                            await emailSenderService.SendEmailWithFrom(leaveEmployee.emailAddress, employee.nameEnglish, "Leave Application", html);
                        }
                    }

                }
            }

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ActionAllReturn([FromForm] LeaveApprovalViewModel model)
        {
            //  return Json(model);



            int Status = 4;
            string mailtext = "Returned";

            if (model.registerids!=null)
            {
                for (int i = 0; i < model.registerids.Count(); i++)
                {
                    LeaveRoute leaveRoute = await leaveRouteService.GetLeaveRouteById((int)model.ids[i]);
                    LeaveRegister leaveRegister = await leaveRegisterService.GetLeaveRegisterById((int)model.registerids[0]);
                    int? nextOrder = leaveRoute.routeOrder + 1;
                    await leaveRouteService.UpdateLeaveRoute(leaveRoute.Id, 0);

                    LeaveStatusLog leaveStatusLog = new LeaveStatusLog
                    {
                        employeeId = leaveRegister.employeeId,
                        leaveRegisterId = model.registerids[i],
                        date = DateTime.Now,
                        remarks = "Approved",
                        Status = Status
                    };
                    await leaveStatusLogService.SaveLeaveStatusLog(leaveStatusLog);

                    string host = HttpContext.Request.Host.ToString();
                    string scheme = Request.Scheme;
                    string baseUrl = $"" + scheme + "://" + host + "/HRPMSLeave/LeaveApproval";

                    string htmlApprove = "<div><strong>Leave Recommendation/Approval.</strong></div>"
                            + " <br/>"
                            + "<p>Dear Sir,</p>"
                            + " <br/>"
                            + " This is to inform you that one leave application is waiting for your recommendation/approval."
                            + "<br/>"
                            + "<div><a href='" + baseUrl + "'><button>Login</button></a></div>"
                            + " <br/>"
                            + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Human Resource Department.</p></div>"
                            + " <br/>";

                    var employee = await personalInfoService.GetEmployeeInfoById((int)leaveRegister.employeeId);
                    // var leavRegister = await leaveRegisterService.GetLeaveRegisterById((int)model.leaveId);
                    var leaveEmployee = await personalInfoService.GetEmployeeInfoById((int)leaveRegister.employeeId);

                    string html1 = "<div><strong>Leave Application.</strong></div>"
                                + " <br/>"
                                + "<p>Dear Sir,</p>"
                                + " <br/>"
                                + " This is to inform you that your leave application has " + mailtext + " by " + employee.nameEnglish + " just now."
                                + "<br/>"

                                + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Human Resource Department.</p></div>"
                                + " <br/>";

                    if (Status == 4)
                    {
                        await leaveRegisterService.UpdateLeaveApproval((int)model.registerids[i], Status);
                        if (leaveEmployee.emailAddress != null)
                        {
                            await emailSenderService.SendEmailWithFrom(leaveEmployee.emailAddress, employee.nameEnglish, "Leave Application", html1);
                        }
                        return RedirectToAction(nameof(Index));
                    }

                    LeaveRoute leaveRouteNew = await leaveRouteService.GetLeaveRouteByRouteOrder((int)leaveRoute.leaveRegisterId, (int)nextOrder);

                    if (leaveRouteNew != null)
                    {
                        var leaveFutureEmployee = await personalInfoService.GetEmployeeInfoById((int)leaveRouteNew?.employeeId);
                        await leaveRouteService.UpdateLeaveRoute(leaveRouteNew.Id, 1);
                        await leaveRegisterService.UpdateLeaveApproval((int)model.registerids[i], Status);
                        if (leaveEmployee.emailAddress != null)
                        {
                            await emailSenderService.SendEmailWithFrom(leaveEmployee.emailAddress, employee.nameEnglish, "Leave Application", html1);
                        }

                        if (leaveFutureEmployee.emailAddress != null)
                        {
                            await emailSenderService.SendEmailWithFrom(leaveFutureEmployee.emailAddress, employee.nameEnglish, "Leave Application", htmlApprove);
                        }

                    }
                    else
                    {
                        await leaveRegisterService.UpdateLeaveApproval((int)model.registerids[0], 3);
                        string html = "<div><strong>Leave Application.</strong></div>"
                                    + " <br/>"
                                    + "<p>Dear Sir,</p>"
                                    + " <br/>"
                                    + " This is to inform you that "
                                    + "leave application"
                                    + " <br/>"
                                    + "Employee Name : '" + leaveEmployee.nameEnglish + "' "
                                    + " <br/>"
                                    + "Leave From : '" + leaveRegister.leaveFrom?.ToString("dd-MMM-yyyy") + "' " + " To : '" + leaveRegister.leaveTo?.ToString("dd-MMM-yyyy") + "' "
                                    + " <br/>"
                                    + "has approved by " + employee.nameEnglish + " just now."
                                    + "<br/>"
                                    + "<br/>"

                                    + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Human Resource Department.</p></div>"
                                    + " <br/>";
                        if (leaveEmployee.emailAddress != null)
                        {
                            await emailSenderService.SendEmailWithFrom(leaveEmployee.emailAddress, employee.nameEnglish, "Leave Application", html);
                        }
                    }

                }
            }

            return RedirectToAction(nameof(Index));

        }


        #region MobileAPI Section

        [Route("global/api/GetLeaveRouteByEmpId/{empId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetLeaveRouteByEmpId(int empId)
        {
          
          
            LeaveRegisterViewModel model = new LeaveRegisterViewModel
            {
             
                leaveRoutes = await leaveRouteService.GetLeaveRouteByEmpId(empId),
            };
            return Json(model);
        }


        #endregion


        [HttpPost]
        [AllowAnonymous]
        [Route("api/LeaveApprovalAction")]
        public async Task<IActionResult> LeaveApprovalAction([FromBody] LeaveApprovalViewModel model)
        {
            //return Json(model);
            LeaveRoute leaveRoute = await leaveRouteService.GetLeaveRouteById((int)model.id);
            int? nextOrder = leaveRoute.routeOrder + 1;
            int Status = 1;
            string mailtext = "";
            if (model.leaveApprove == "Approve")
            {
                Status = 2;
                mailtext = "Approved";
            }
            else if (model.leaveApprove == "Reject")
            {
                Status = 5;
                mailtext = "rejected";
            }
            else if (model.leaveApprove == "Return")
            {
                Status = 4;
                mailtext = "rejected";
            }
            else
            {
                Status = 4;
                mailtext = "returned";
            }

            await leaveRouteService.UpdateLeaveRoute(leaveRoute.Id, 0);

            LeaveStatusLog leaveStatusLog = new LeaveStatusLog
            {
                employeeId = model.employeeId,
                leaveRegisterId = model.leaveId,
                date = DateTime.Now,
                remarks = model.leaveApprove,
                Status = Status
            };
            await leaveStatusLogService.SaveLeaveStatusLog(leaveStatusLog);

            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string baseUrl = $"" + scheme + "://" + host + "/HRPMSLeave/LeaveApproval";

            string htmlApprove = "<div><strong>Leave Recommendation/Approval.</strong></div>"
                    + " <br/>"
                    + "<p>Dear Sir,</p>"
                    + " <br/>"
                    + " This is to inform you that one leave application is waiting for your recommendation/approval."
                    + "<br/>"
                    + "<div><a href='" + baseUrl + "'><button>Login</button></a></div>"
                    + " <br/>"
                    + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Human Resource Department.</p></div>"
                    + " <br/>";

            var employee = await personalInfoService.GetEmployeeInfoById((int)model.employeeId);
            var leavRegister = await leaveRegisterService.GetLeaveRegisterById((int)model.leaveId);
            var leaveEmployee = await personalInfoService.GetEmployeeInfoById((int)leavRegister.employeeId);

            string html1 = "<div><strong>Leave Application.</strong></div>"
                        + " <br/>"
                        + "<p>Dear Sir,</p>"
                        + " <br/>"
                        + " This is to inform you that your leave application has " + mailtext + " by " + employee.nameEnglish + " just now."
                        + "<br/>"

                        + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Human Resource Department.</p></div>"
                        + " <br/>";

            if (Status == 5)
            {
          await leaveRegisterService.UpdateLeaveApproval((int)model.leaveId, Status);
                //await emailSenderService.SendEmailWithFrom(leaveEmployee.emailAddress, employee.nameEnglish, "Leave Application", html1);
               
            }
            else if (Status == 4)
            {
               await leaveRegisterService.UpdateLeaveApproval((int)model.leaveId, Status);
                //await emailSenderService.SendEmailWithFrom(leaveEmployee.emailAddress, employee.nameEnglish, "Leave Application", html1);
               
            }

            LeaveRoute leaveRouteNew = await leaveRouteService.GetLeaveRouteByRouteOrder((int)leaveRoute.leaveRegisterId, (int)nextOrder);

            if (leaveRouteNew != null)
            {
                var leaveFutureEmployee = await personalInfoService.GetEmployeeInfoById((int)leaveRouteNew?.employeeId);
                await leaveRouteService.UpdateLeaveRoute(leaveRouteNew.Id, 1);
                await leaveRegisterService.UpdateLeaveApproval((int)model.leaveId, Status);
                //await emailSenderService.SendEmailWithFrom(leaveEmployee.emailAddress, employee.nameEnglish, "Leave Application", html1);
                //await emailSenderService.SendEmailWithFrom(leaveFutureEmployee.emailAddress, employee.nameEnglish, "Leave Application", htmlApprove);
            }
            else
            {
              await leaveRegisterService.UpdateLeaveApproval((int)model.leaveId, Status);
                string html = "<div><strong>Leave Application.</strong></div>"
                            + " <br/>"
                            + "<p>Dear Sir,</p>"                                                                                                                     
                            + " <br/>"
                            + " This is to inform you that "
                            + "leave application"
                            + " <br/>"
                            + "Employee Name : '" + leaveEmployee.nameEnglish + "' "
                            + " <br/>"
                            + "Leave From : '" + leavRegister.leaveFrom?.ToString("dd-MMM-yyyy") + "' " + " To : '" + leavRegister.leaveTo?.ToString("dd-MMM-yyyy") + "' "
                            + " <br/>"
                            + "has approved by " + employee.nameEnglish + " just now."
                            + "<br/>"
                            + "<br/>"

                            + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Human Resource Department.</p></div>"
                            + " <br/>";
             //  await emailSenderService.SendEmailWithFrom(leaveEmployee.emailAddress, employee.nameEnglish, "Leave Application", html);
            }

            return Json(leavRegister);

        }

    }


   
}