using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Payroll.Models;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.EmailService.interfaces;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.Payroll.Data.Entity.Salary;
using OPUSERP.Payroll.Services.Salary.Interfaces;

namespace OPUSERP.Areas.Payroll.Controllers
{
    [Area("Payroll")]
    public class EmailingController : Controller
    {
        private readonly ISalaryService salaryService;
        private readonly IUserInfoes userInfoes;
        private readonly IEmailSenderService emailSenderService;
        private readonly IPersonalInfoService employeeInfoService;

        public EmailingController(ISalaryService salaryService, IUserInfoes userInfoes, IEmailSenderService emailSenderService, IPersonalInfoService employeeInfoService)
        {
            this.salaryService = salaryService;
            this.employeeInfoService = employeeInfoService;
            this.emailSenderService = emailSenderService;
            this.userInfoes = userInfoes;
        }

        public async Task<ActionResult> EmailPaySlip()
        {
            EmailingViewModel model = new EmailingViewModel
            {
                salaryPeriods = await salaryService.GetAllSalaryPeriod()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmailPaySlip([FromForm] EmailingViewModel model)
        {
            //return Json(model);
            try
            {
                if (model.All == 0)
                {
                    var emp = await employeeInfoService.GetEmployeeInfoById((int)model.employeeInfoId);
                    var email = emp.emailAddress;

                    string host = HttpContext.Request.Host.ToString();
                    string scheme = Request.Scheme;
                    string baseUrl = $"" + scheme + "://" + host + "/Payroll/Emailing/GetSalaryPaySlipSendEmailLogStatus?employeeInfoId=" + model.employeeInfoId + "&salaryPeriodId=" + model.salaryPeriodId;

                    string html = "<div><strong>Your PaySlip.</strong></div>"
                            + " <br/>"
                            + "<p>Dear Sir,</p>"
                            + " <br/>"
                            + model.mailText + " <br/>"
                            + " This is your payslip given by accounts department please download it by clicking below button"
                            + "<br/>"
                            + "<div><a href='" + baseUrl + "' download><button>PaySlip</button></a></div>"
                            + " <br/>"
                            + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Accounts Department.</p></div>"
                            + " <br/>";

                    if (email != null)
                    {
                        await emailSenderService.SendEmailWithFrom(email, "Accounts Department", model.mailSub, html);
                        SendEmailLogStatus data = new SendEmailLogStatus
                        {
                            sender = HttpContext.User.Identity.Name,
                            receiver = emp.nameEnglish,
                            receiverEmail = email,
                            employeeId = emp.Id,
                            date = DateTime.Now,
                            type = "Send",
                            module = "Payroll",
                            itemName = "PaySlip"
                        };
                        await salaryService.SaveSendEmailLogStatus(data);
                    }
                }
                else
                {
                    var empList = await employeeInfoService.GetEmployeeInfo();
                    foreach (var data in empList)
                    {
                        var email = data.emailAddress;

                        string host = HttpContext.Request.Host.ToString();
                        string scheme = Request.Scheme;
                        string baseUrl = $"" + scheme + "://" + host + "/Payroll/Emailing/GetSalaryPaySlipSendEmailLogStatus?employeeInfoId=" + data.Id + "&salaryPeriodId=" + model.salaryPeriodId;

                        string html = "<div><strong>Your PaySlip.</strong></div>"
                                + " <br/>"
                                + "<p>Dear Sir,</p>"
                                + model.mailText + " <br/>"
                                + " This is your payslip given by accounts department please download it by clicking below button"
                                + "<br/>"
                                + "<div><a href='" + baseUrl + "' download><button>PaySlip</button></a></div>"
                                + " <br/>"
                                + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Accounts Department.</p></div>"
                                + " <br/>";

                        if (email != null)
                        {
                            await emailSenderService.SendEmailWithFrom(email, "Accounts Department", model.mailSub, html);
                            SendEmailLogStatus data1 = new SendEmailLogStatus
                            {
                                sender = HttpContext.User.Identity.Name,
                                receiver = data.nameEnglish,
                                receiverEmail = email,
                                employeeId = data.Id,
                                date = DateTime.Now,
                                type = "Send",
                                module = "Payroll",
                                itemName = "PaySlip"
                            };
                            await salaryService.SaveSendEmailLogStatus(data1);
                        }
                    }
                }
                return RedirectToAction(nameof(EmailPaySlip));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(EmailPaySlip));
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetSalaryPeriodById(int periodId)
        {
            return Json(await salaryService.GetSalaryPeriodNameById(periodId));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetSalaryPaySlipSendEmailLogStatus(int employeeInfoId, int salaryPeriodId)
        {
            SendEmailLogStatus data1 = new SendEmailLogStatus
            {
                employeeId = employeeInfoId,
                date = DateTime.Now,
                type = "Download",
                module = "Payroll",
                itemName = "PaySlip"
            };
            await salaryService.SaveSendEmailLogStatus(data1);

            return RedirectToAction("SalaryReport", "PayrollReport", new
            {
                rptType = "PSLIP",
                employeeInfoId = employeeInfoId,
                salaryPeriodId = salaryPeriodId
            });
        }

    }
}