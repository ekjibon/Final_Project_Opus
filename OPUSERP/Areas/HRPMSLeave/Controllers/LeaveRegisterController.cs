using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSLeave.Models;
using OPUSERP.Areas.HRPMSLeave.Models.Lang;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.EmailService.interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Data.Entity.Leave;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.Leave.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.Payroll.Services.Salary.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSLeave.Controllers
{
    [Authorize]
    [Area("HRPMSLeave")]
    public class LeaveRegisterController : Controller
    {
        private readonly LangGenerate<LeaveLn> _lang;
        // GET: LeaveRegister
        private readonly ILeaveRegisterService leaveRegisterService;
        private readonly IWagesLeaveRegisterService wagesLeaveRegisterService;
        private readonly ILeaveTypeService leaveTypeService;
        private readonly ILeaveRouteService leaveRouteService;
        private readonly IUserInfoes userInfo;
        private readonly IPersonalInfoService personalInfoService;
        private readonly ISupervisorService supervisorService;
        private readonly ILeaveStatusLogService leaveStatusLogService;
        private readonly IEmailSenderService emailSenderService;
        private readonly ILeavePolicyService leavePolicyService;
        private readonly IApprovalService approvalService;
        private readonly IYearCourseTitleService yearCourseTitleService;
        private readonly ISalaryService salaryService;
        private readonly IERPCompanyService eRPCompanyService;

        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public LeaveRegisterController(IHostingEnvironment hostingEnvironment, IERPCompanyService eRPCompanyService, IConverter converter, ILeaveRegisterService leaveRegisterService, ILeaveTypeService leaveTypeService, ILeaveRouteService leaveRouteService, IUserInfoes userInfo, IPersonalInfoService personalInfoService, ISupervisorService supervisorService, ILeaveStatusLogService leaveStatusLogService, IEmailSenderService emailSenderService, ILeavePolicyService leavePolicyService, IApprovalService approvalService, IYearCourseTitleService yearCourseTitleService, IWagesLeaveRegisterService wagesLeaveRegisterService, ISalaryService salaryService)
        {
            _lang = new LangGenerate<LeaveLn>(hostingEnvironment.ContentRootPath);
            this.leaveRegisterService = leaveRegisterService;
            this.leaveTypeService = leaveTypeService;
            this.leaveRouteService = leaveRouteService;
            this.userInfo = userInfo;
            this.personalInfoService = personalInfoService;
            this.supervisorService = supervisorService;
            this.leaveStatusLogService = leaveStatusLogService;
            this.emailSenderService = emailSenderService;
            this.leavePolicyService = leavePolicyService;
            this.approvalService = approvalService;
            this.yearCourseTitleService = yearCourseTitleService;
            this.wagesLeaveRegisterService = wagesLeaveRegisterService;
            this.eRPCompanyService = eRPCompanyService;
            this.salaryService = salaryService;
            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        #region Leave Apply

        public async Task<IActionResult> LeaveApply()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }
            ViewBag.employeeId = empId;
            LeaveRegisterViewModel model = new LeaveRegisterViewModel
            {
                fLang = _lang.PerseLang("Leave/LeaveApplyEN.json", "Leave/LeaveApplyBN.json", Request.Cookies["lang"]),
                leaveRegisterslist = await leaveRegisterService.GetAllLeaveRegisterByEmpId(empId),
                //leaveTypelist = await leaveTypeService.GetAllLeaveType(),
                leaveTypelist = await leaveTypeService.GetAllLeaveTypeBySP(empId),
                //supervisor = await supervisorService.GetFirstSupervisorByEmpId(empId),
                approvalDetail = await approvalService.GetSingleApprovalDetailByEmpAndType(empId, "Leave"),
                leaveDays = await leavePolicyService.GetAllLeaveDay()
                //leaveOpeningBalances = await leaveOpeningBalanceService.GetLeaveOpeningBalanceByEmpCode(employeeId)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LeaveApply([FromForm] LeaveRegisterViewModel model)
        {
            string userName = HttpContext.User.Identity.Name;
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
            ViewBag.employeeId = empId;
            var leaveCheck = await leaveRegisterService.GetAllLeaveRegisterByEmpIdAndDate(empId, model.leaveFrom, model.leaveTo);

            if (!ModelState.IsValid || leaveCheck.Count() > 0 || model.leaveBalance < model.daysLeave)
            {
                model.leaveRegisterslist = await leaveRegisterService.GetAllLeaveRegisterByEmpId(empId);
                model.leaveTypelist = await leaveTypeService.GetAllLeaveTypeBySP(empId);
                //model.supervisor = await supervisorService.GetFirstSupervisorByEmpId(EmpInfo.Id);
                model.approvalDetail = await approvalService.GetSingleApprovalDetailByEmpAndType(empId, "Leave");
                model.leaveDays = await leavePolicyService.GetAllLeaveDay();
                model.fLang = _lang.PerseLang("Leave/LeaveApplyEN.json", "Leave/LeaveApplyBN.json", Request.Cookies["lang"]);
                if (leaveCheck.Count() > 0)
                {
                    ModelState.AddModelError(string.Empty, "Another Leave has already applied in same Date !!");
                }
                if (model.leaveBalance < model.daysLeave)
                {
                    ModelState.AddModelError(string.Empty, "Sorry You Haven't Enough Leave Balance !!");
                }
                return View(model);
            }


            string fileName = String.Empty;
            string fileNameMain = String.Empty;
            if (model.fileUrl != null)
            {
                string message = FileSave.SaveEmpAttachment(out fileName, model.fileUrl);

                if (message == "success")
                {
                    fileNameMain = fileName;
                }
            }

            LeaveRegister data = new LeaveRegister
            {
                //Id = model.id,
                employeeId = EmpInfo.Id,
                leaveTypeId = model.leaveTypeId,
                whenLeave = model.whenLeave,
                leaveStatus = 1,
                leaveFrom = model.leaveFrom,
                leaveTo = model.leaveTo,
                daysLeave = model.daysLeave,
                purposeOfLeave = model.purposeOfLeave,
                emergencyContactNo = model.emergencyContactNo,
                address = model.address,
                substitutionUserId = model.substitutionUserId,
                leaveDayId = model.leaveDayId,
                fileUrl = fileNameMain
            };

            int id = await leaveRegisterService.SaveLeaveRegister(data);

            //IEnumerable<Supervisor> supervisors = await supervisorService.GetSupervisorByEmpId(EmpInfo.Id);
            IEnumerable<ApprovalDetail> approvalDetails = await approvalService.GetApprovalDetailByEmpAndType(EmpInfo.Id, "Leave");

            var i = 1;
            var Active = 1;
            foreach (ApprovalDetail supervisor in approvalDetails)
            {
                LeaveRoute leaveRoute = new LeaveRoute
                {
                    leaveRegisterId = id,
                    employeeId = supervisor.approverId,
                    routeOrder = i,
                    isActive = Active,
                };
                await leaveRouteService.SaveLeaveRoute(leaveRoute);
                i++;
                Active = 0;
            }

            LeaveStatusLog leaveStatusLog = new LeaveStatusLog
            {
                employeeId = EmpInfo.Id,
                leaveRegisterId = id,
                date = DateTime.Now,
                remarks = "On Approval",
                Status = 1
            };
            await leaveStatusLogService.SaveLeaveStatusLog(leaveStatusLog);

            var ActiveLeave = await leaveRouteService.GetLeaveRouteByLeaveRegisterId(id);

            string html1 = "<div><strong>Leave Application.</strong></div>"
                + " <br/>"
                + "<p>Dear Sir,</p>"
                + " <br/>"
                + " This is to inform you that you have sent one leave application to " + ActiveLeave?.employee?.nameEnglish + " just now."
                + "<br/>"

                + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Human Resource Department.</p></div>"
                + " <br/>";

            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string baseUrl = $"" + scheme + "://" + host + "/HRPMSLeave/LeaveApproval";

            string html = "<div><strong>Leave Recommendation/Approval.</strong></div>"
                    + " <br/>"
                    + "<p>Dear Sir,</p>"
                    + " <br/>"
                    + " This is to inform you that one leave application is waiting for your recommendation/approval."
                    + "<br/>"
                    + "<div><a href='" + baseUrl + "'><button>Login</button></a></div>"
                    + " <br/>"
                    + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Human Resource Department.</p></div>"
                    + " <br/>";

            if (email != null)
            {
                await emailSenderService.SendEmailWithFrom(email, name, "Leave Application", html1);
            }

            if (ActiveLeave?.employee?.emailAddress != null)
            {
                await emailSenderService.SendEmailWithFrom(ActiveLeave?.employee?.emailAddress, ActiveLeave?.employee?.nameEnglish, "Leave Application", html);
            }

            return RedirectToAction(nameof(LeaveApply));
        }

        #endregion

        #region Leave Cancel

        public async Task<IActionResult> LeaveCancel()
        {
            LeaveRegisterViewModel model = new LeaveRegisterViewModel
            {
                leaveRegisterslist = await leaveRegisterService.GetAllLeaveRegister()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LeaveCancel([FromForm] LeaveRegisterViewModel model)
        {
            //return Json(model);
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int? empId = null;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            LeaveStatusLog leaveStatusLog = new LeaveStatusLog
            {
                employeeId = empId,
                leaveRegisterId = model.id,
                date = DateTime.Now,
                remarks = model.txtRemarks,
                Status = 6
            };
            await leaveStatusLogService.SaveLeaveStatusLog(leaveStatusLog);

            await leaveRegisterService.UpdateLeaveApproval(model.id, 6);

            return RedirectToAction(nameof(LeaveCancel));
        }

        #endregion

        #region Leave Return
        public async Task<IActionResult> LeaveReturn()
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
                leaveRegisterslist = await leaveRegisterService.GetAllLeaveRegisterByEmpIdAndStatus(empId, 4)
            };
            return View(model);
        }
        #endregion

        #region LeaveReject
        public async Task<IActionResult> LeaveReject()
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
                leaveRegisterslist = await leaveRegisterService.GetAllLeaveRegisterByEmpIdAndStatus(empId, 5)
            };
            return View(model);
        }
        #endregion

        #region Manual Leave Apply
        public async Task<IActionResult> ManualLeaveApply()
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
                leaveRegisterslist = await leaveRegisterService.GetAllLeaveRegisterByEmpId(empId),
                leaveTypelist = await leaveTypeService.GetAllLeaveType(),
                leaveDays = await leavePolicyService.GetAllLeaveDay(),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManualLeaveApply([FromForm] LeaveRegisterViewModel model)
        {
            //return Json(model);
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            //var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = (int)model.employeeId;
            //if (EmpInfo != null)
            //{
            //    empId = EmpInfo.Id;
            //}

            var leaveCheck = await leaveRegisterService.GetAllLeaveRegisterByEmpIdAndDate(empId, model.leaveFrom, model.leaveTo);

            if (!ModelState.IsValid || leaveCheck.Count() > 0)
            {
                model.leaveRegisterslist = await leaveRegisterService.GetAllLeaveRegisterByEmpId(empId);
                model.leaveTypelist = await leaveTypeService.GetAllLeaveType();
                model.leaveDays = await leavePolicyService.GetAllLeaveDay();
                model.visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData();
                model.fLang = _lang.PerseLang("Leave/LeaveApplyEN.json", "Leave/LeaveApplyBN.json", Request.Cookies["lang"]);
                if (leaveCheck.Count() > 0)
                {
                    ModelState.AddModelError(string.Empty, "Another Leave has already applied in same Date !!");
                }
                if (model.leaveTypeId == 3 || model.leaveTypeId == 8)
                {
                }
                else
                {
                    if (model.leaveBalance < model.daysLeave)
                    {
                        ModelState.AddModelError(string.Empty, "Sorry You Haven't Enough Leave Balance !!");
                    }
                }
                return View(model);
            }

            LeaveRegister data = new LeaveRegister
            {
                Id = model.id,
                employeeId = model.employeeId,
                leaveTypeId = model.leaveTypeId,
                whenLeave = model.whenLeave,
                leaveStatus = 3,
                leaveFrom = model.leaveFrom,
                leaveTo = model.leaveTo,
                daysLeave = model.daysLeave,
                purposeOfLeave = model.purposeOfLeave,
                emergencyContactNo = model.emergencyContactNo,
                address = model.address,
                leaveDayId = model.leaveDayId,
                paymentOption = model.paymentType
            };

            int masterId = await leaveRegisterService.SaveLeaveRegister(data);

            LeaveStatusLog leaveStatusLog = new LeaveStatusLog
            {
                employeeId = model.employeeId,
                leaveRegisterId = masterId,
                date = DateTime.Now,
                remarks = "Leave Applied Manually by Admin",
                Status = 3
            };
            await leaveStatusLogService.SaveLeaveStatusLog(leaveStatusLog);

            //return RedirectToAction(nameof(ManualLeaveApply));
            return RedirectToAction(nameof(ManualLeaveApplyList), new { employeeId = empId });
        }

        public async Task<IActionResult> ManualLeaveApplyList(int employeeId)
        {
            LeaveRegisterViewModel model = new LeaveRegisterViewModel
            {
                fLang = _lang.PerseLang("Leave/LeaveApplyEN.json", "Leave/LeaveApplyBN.json", Request.Cookies["lang"]),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData(),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(employeeId),
            };
            ViewBag.empId = employeeId;
            return View(model);
        }

        [HttpGet]
        public async Task<JsonResult> GetTotalLeaveDaysBetweenDate(DateTime? frmDate, DateTime? toDate, int leaveType)
        {
            var daysLeave = await leaveRegisterService.GetTotalLeaveDaysBetweenDate(frmDate, toDate, leaveType);
            return Json(daysLeave);
        }

        [HttpGet]
        public async Task<JsonResult> GetLeaveBalanceSummaryByEmpId(int empId)
        {
            var daysLeave = await leaveRegisterService.GetLeaveBalanceSummaryByEmpId(empId);
            return Json(daysLeave);
        }

        [HttpGet]
        public async Task<JsonResult> GetAllLeaveApplicationByEmpId(int empId)
        {
            var daysLeave = await leaveRegisterService.GetAllLeaveRegisterByEmpId(empId);
            return Json(daysLeave);
        }

        #endregion

        #region Wages Leave Apply
        public async Task<IActionResult> WagesLeaveApply()
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
                wagesLeaveRegisters = await wagesLeaveRegisterService.GetAllLeaveRegister(),
                leaveTypelist = await leaveTypeService.GetAllLeaveType(),
                //leaveOpeningBalances = await leaveOpeningBalanceService.GetLeaveOpeningBalanceByEmpCode(employeeId)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WagesLeaveApply([FromForm] LeaveRegisterViewModel model)
        {
            //return Json(model);
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            if (!ModelState.IsValid)
            {
                model.wagesLeaveRegisters = await wagesLeaveRegisterService.GetAllLeaveRegister();
                model.leaveTypelist = await leaveTypeService.GetAllLeaveType();
                model.fLang = _lang.PerseLang("Leave/LeaveApplyEN.json", "Leave/LeaveApplyBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            WagesLeaveRegister data = new WagesLeaveRegister
            {
                Id = model.id,
                employeeId = model.employeeId,
                leaveTypeId = model.leaveTypeId,
                whenLeave = model.whenLeave,
                leaveStatus = 1,
                leaveFrom = model.leaveFrom,
                leaveTo = model.leaveTo,
                daysLeave = model.daysLeave,
                purposeOfLeave = model.purposeOfLeave,
                emergencyContactNo = model.emergencyContactNo,
                address = model.address,
            };

            int masterId = await wagesLeaveRegisterService.SaveLeaveRegister(data);

            return RedirectToAction(nameof(WagesLeaveApply));
        }

        #endregion

        #region Leave Report

        public async Task<IActionResult> LeaveReportView()
        {
            LeaveRegisterViewModel model = new LeaveRegisterViewModel
            {
                years = await yearCourseTitleService.GetYear()
            };
            return View(model);
        }    

        [AllowAnonymous]
        public async Task<IActionResult> LeaveReportSummaryPdf(string rptType, string year)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;
            string url = "";

            if (rptType == "LEAVESUMMARY")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/HRPMSLeave/LeaveRegister/" + data.FirstOrDefault().reportFormat + "?year=" + year;
            }
            string status = myPDF.GenerateLandscapePDF(out fileName, url);
            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> LeaveReportPdf(string rptType, int empId, string fdate, string tdate)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;
            string url = "";

            if (rptType == "LEAVEINDIVL")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/HRPMSLeave/LeaveRegister/" + data.FirstOrDefault().reportFormat + "?empId=" + empId + "&&fdate=" + fdate + "&&tdate=" + tdate;                
            }
            string status = myPDF.GeneratePDF(out fileName, url);
            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> LeaveReport(int year, int empId)
        {
            var EmpInfo = await personalInfoService.GetEmployeeInfoById(empId);
            ViewBag.name = EmpInfo.nameEnglish;
            ViewBag.year = year.ToString();
            LeaveRegisterViewModel model = new LeaveRegisterViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                leaveReportModels = await leaveRegisterService.GetLeaveReport(year, empId)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> LeaveReportIndividual_CBF(int empId, string fdate, string tdate)
        {           
            LeaveRegisterViewModel model = new LeaveRegisterViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                leaveIndividualViewModels = await leaveRegisterService.GetIndividualLeaveReport(empId, fdate, tdate),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(empId),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> LeaveReportSummary(string year)
        {
            ViewBag.year = year.ToString();
            LeaveRegisterViewModel model = new LeaveRegisterViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                leaveSummaryReports = await leaveRegisterService.GetAllLeaveSummaryReport(year)
            };
            return View(model);
        }

        public async Task<IActionResult> Preview(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }
            ViewBag.employeeId = empId;

            var leaveRegList = await leaveRegisterService.GetLeaveRegisterById(id);
            DateTime leaveYear = Convert.ToDateTime(leaveRegList.leaveFrom);
            var year = leaveYear.Year;
            LeaveRegisterViewModel model = new LeaveRegisterViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                leaveRegister = await leaveRegisterService.GetLeaveRegisterById(id),
                leaveReportModels = await leaveRegisterService.GetLeaveReport(year, EmpInfo.Id),
                leaveSupervisorRecomViewModels = await leaveRegisterService.GetSupervisorRecomForLeaveByEmpId(id, EmpInfo.Id),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()

            };
            return View(model);
        }

        //PrintPDF
        [AllowAnonymous]
        public IActionResult LeaveViewPdf(int id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;

            string url = scheme + "://" + host + "/HRPMSLeave/LeaveRegister/LeavePrintView/" + id;
            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> LeavePrintView(int id)
        {
            //string userName = HttpContext.User.Identity.Name;
            //var userInfos = await userInfo.GetUserInfoByUser(userName);
            //var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            //int empId = 0;
            //if (EmpInfo != null)
            //{
            //    empId = EmpInfo.Id;
            //}


            var leaveRegList = await leaveRegisterService.GetLeaveRegisterById(id);
            DateTime leaveYear = Convert.ToDateTime(leaveRegList.leaveFrom);
            var year = leaveYear.Year;
            LeaveRegisterViewModel model = new LeaveRegisterViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                leaveRegister = await leaveRegisterService.GetLeaveRegisterById(id),
                leaveReportModels = await leaveRegisterService.GetLeaveReport(year, Convert.ToInt32(leaveRegList.employeeId)),
                leaveSupervisorRecomViewModels = await leaveRegisterService.GetSupervisorRecomForLeaveByEmpId(id, Convert.ToInt32(leaveRegList.employeeId))

            };
            return View(model);
        }

        #endregion

        #region API Section
        [Route("global/api/GetLeaveBalanceByempId/{id}/{statusId}")]
        [HttpGet]
        public async Task<IActionResult> GetLeaveBalanceByempId(int id, int statusId)
        {
            return Json(await leaveRegisterService.GetLeaveBalanceByempId(id, statusId));
        }

        [Route("global/api/GetLeaveReport/{year}/{empId}")]
        [HttpGet]
        public async Task<IActionResult> GetLeaveReport(int year, int empId)
        {
            return Json(await leaveRegisterService.GetLeaveReport(year, empId));
        }

        [Route("global/api/GetLeaveOpeningBalanceByEmpAndYear/{id}/{yearId}")]
        [HttpGet]
        public async Task<IActionResult> GetLeaveOpeningBalanceByEmpAndYear(int id, int yearId)
        {
            return Json(await leavePolicyService.GetLeaveOpeningBalanceByEmpAndYear(id, yearId));
        }

        [Route("global/api/GetAllLeaveStatusLogByLeaveId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAllLeaveStatusLogByLeaveId(int id)
        {
            return Json(await leaveStatusLogService.GetAllLeaveStatusLogByLeaveId(id));
        }

        [Route("global/api/GetAllLeaveRegisterByEmpId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAllLeaveRegisterByEmpId(int id)
        {
            return Json(await leaveRegisterService.GetAllLeaveRegisterByEmpId(id));
        }

        [Route("global/api/GetAllLeaveRegisterByEmpIdAndDateRange/{id}/{from}/{to}")]
        [HttpGet]
        public async Task<IActionResult> GetAllLeaveRegisterByEmpIdAndDateRange(int id, DateTime from, DateTime to)
        {
            return Json(await leaveRegisterService.GetAllLeaveRegisterByEmpIdAndDateRange(id, from, to));
        }

        [AllowAnonymous]
        [Route("global/api/SendEmailWithFrom/{email}/{from}/{subject}/{body}")]
        [HttpGet]
        public IActionResult SendEmailWithFrom(string email, string from, string subject, string body)
        {
            emailSenderService.SendEmailWithFrom(email, from, subject, body);
            return Ok("SuccessFull");
        }

        [Route("global/api/GetLeavePolicyByTypeandYear/{leaveTypeId}")]
        [HttpGet]
        public async Task<IActionResult> GetLeavePolicyByTypeandYear(int leaveTypeId)
        {
            var year = DateTime.Now.Year;
            var yearList = await yearCourseTitleService.GetYearByName(year.ToString());
            int yearId = yearList.Id;
            return Json(await leavePolicyService.GetLeavePolicyByTypeandYear(leaveTypeId, yearId));
        }

        #endregion

        #region MobileAPI Section

        [Route("global/api/GetUserAllInfo")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserAllInfo()
        {
            var userInfos = await userInfo.GetUserInfo();

            return Json(userInfos);
        }

        [Route("global/api/GetUserInfoByUser/{userName}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserInfoByUser(string userName)
        {
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            return Json(EmpInfo);
        }


        [Route("global/api/GetLeaveBalanceById/{id}/{statusId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetLeaveBalanceById(int id, int statusId)
        {
            return Json(await leaveRegisterService.GetLeaveBalanceByempId(id, statusId));
        }

        [Route("global/api/GetLeaveOpeningBalanceEmpIDAndYear/{id}/{yearId}")]
        [HttpGet]
        public async Task<IActionResult> GetLeaveOpeningBalanceByEmpIDAndYear(int id, int yearId)
        {
            return Json(await leavePolicyService.GetLeaveOpeningBalanceByEmpAndYear(id, yearId));
        }

        [Route("global/api/GetAllLeaveStatusByLeaveid/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllLeaveStatusLogByLeaveid(int id)
        {
            return Json(await leaveStatusLogService.GetAllLeaveStatusLogByLeaveId(id));
        }

        [Route("global/api/GetAllLeaveApplyEmpid/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllLeaveRegisterByEmpid(int id)
        {
            return Json(await leaveRegisterService.GetAllLeaveRegisterByEmpId(id));
        }

        [Route("global/api/GetAllLeaveRegisterEmpIdandDate/{id}/{from}/{to}")]
        [HttpGet]
        public async Task<IActionResult> GetAllLeaveRegisterByEmpIdandDateRange(int id, DateTime from, DateTime to)
        {
            return Json(await leaveRegisterService.GetAllLeaveRegisterByEmpIdAndDateRange(id, from, to));
        }


        [Route("global/api/GetApprovarByEmpId/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetSupervisorByEmpId(int id)
        {
            return Json(await approvalService.GetApprovalDetailByEmpAndType(id, "Leave"));
        }




        [Route("global/api/SendEmailWithFromBody/{email}/{from}/{subject}/{body}")]
        [HttpGet]
        public IActionResult SendEmailWithFromBody(string email, string from, string subject, string body)
        {
            emailSenderService.SendEmailWithFrom(email, from, subject, body);
            return Ok("SuccessFull");
        }

        //Leave Status

        [Route("global/api/GetAllLeaveReject/{empId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllLeaveReject(int empId)
        {
            LeaveRegisterViewModel model = new LeaveRegisterViewModel
            {
                leaveRegisterslist = await leaveRegisterService.GetAllLeaveRegisterByEmpIdAndStatus(empId, 5)
            };
            return Json(model.leaveRegisterslist);
        }

        [Route("global/api/GetAllLeaveReturn/{empId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllLeaveReturn(int empId)
        {
            LeaveRegisterViewModel model = new LeaveRegisterViewModel
            {

                leaveRegisterslist = await leaveRegisterService.GetAllLeaveRegisterByEmpIdAndStatus(empId, 4)
            };
            return Json(model.leaveRegisterslist);
        }


        [Route("global/api/GetAllLeaveCancle/{empId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllLeaveCancle(int empId)
        {
            LeaveRegisterViewModel model = new LeaveRegisterViewModel
            {
                leaveRegisterslist = await leaveRegisterService.GetAllLeaveRegisterByEmpIdAndStatus(empId, 6)
            };
            return Json(model.leaveRegisterslist);
        }

        // POST: LeaveRegister/Edit
        [HttpPost]
        [AllowAnonymous]
        [Route("api/LeaveApplybyEmo")]
        public async Task<IActionResult> LeaveApplybyEmo([FromBody] LeaveRegisterViewModel model)
        {
            string userName = model.userName;
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
            //return Json(model);
            if (empId == 0)
            {
                return Json("Invalid Employee !! please check your employeeId..");
            }
            var leaveCheck = await leaveRegisterService.GetAllLeaveRegisterByEmpIdAndDate(empId, model.leaveFrom, model.leaveTo);
            //return Json(leaveCheck);

            if (!ModelState.IsValid || leaveCheck.Count() >= 0)
            {
                model.leaveRegisterslist = await leaveRegisterService.GetAllLeaveRegisterByEmpId(empId);
                model.leaveTypelist = await leaveTypeService.GetAllLeaveType();
                model.supervisor = await supervisorService.GetFirstSupervisorByEmpId(empId);
                model.fLang = _lang.PerseLang("Leave/LeaveApplyEN.json", "Leave/LeaveApplyBN.json", Request.Cookies["lang"]);
                if (leaveCheck.Count() > 0)
                {
                    ModelState.AddModelError(string.Empty, "Another Leave has already applied in same Date !!");
                }
                // return Json("Dont left any leave !!");
            }

            LeaveRegister data = new LeaveRegister
            {
                //Id = model.id,
                employeeId = EmpInfo.Id,
                leaveTypeId = model.leaveTypeId,
                whenLeave = model.whenLeave,
                leaveStatus = 1,
                leaveFrom = model.leaveFrom,
                leaveTo = model.leaveTo,
                daysLeave = model.daysLeave,
                purposeOfLeave = model.purposeOfLeave,
                emergencyContactNo = model.emergencyContactNo,
                address = model.address,
                substitutionUserId = model.substitutionUserId
            };

            int id = await leaveRegisterService.SaveLeaveRegister(data);

            IEnumerable<ApprovalDetail> approvalDetails = await approvalService.GetApprovalDetailByEmpAndType(EmpInfo.Id, "Leave");

            var i = 1;
            var Active = 1;
            foreach (ApprovalDetail supervisor in approvalDetails)
            {
                LeaveRoute leaveRoute = new LeaveRoute
                {
                    leaveRegisterId = id,
                    employeeId = supervisor.approverId,
                    routeOrder = i,
                    isActive = Active,
                };
                await leaveRouteService.SaveLeaveRoute(leaveRoute);
                i++;
                Active = 0;
            }

            LeaveStatusLog leaveStatusLog = new LeaveStatusLog
            {
                employeeId = EmpInfo.Id,
                leaveRegisterId = id,
                date = DateTime.Now,
                remarks = "On Approval",
                Status = 1
            };
            await leaveStatusLogService.SaveLeaveStatusLog(leaveStatusLog);

            var ActiveLeave = await leaveRouteService.GetLeaveRouteByLeaveRegisterId(id);

            string html1 = "<div><strong>Leave Application.</strong></div>"
                + " <br/>"
                + "<p>Dear Sir,</p>"
                + " <br/>"
                + " This is to inform you that you have sent one leave application to " + ActiveLeave?.employee?.nameEnglish + " just now."
                + "<br/>"

                + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Human Resource Department.</p></div>"
                + " <br/>";

            string htmlShow = ActiveLeave?.employee?.nameEnglish;

            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string baseUrl = $"" + scheme + "://" + host + "/HRPMSLeave/LeaveApproval";

            string html = "<div><strong>Leave Recommendation/Approval.</strong></div>"
                    + " <br/>"
                    + "<p>Dear Sir,</p>"
                    + " <br/>"
                    + " This is to inform you that one leave application is waiting for your recommendation/approval."
                    + "<br/>"
                    + "<div><a href='" + baseUrl + "'><button>Login</button></a></div>"
                    + " <br/>"
                    + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Human Resource Department.</p></div>"
                    + " <br/>";

            //  await emailSenderService.SendEmailWithFrom(email, name, "Leave Application", html1);
            await emailSenderService.SendEmailWithFrom(ActiveLeave?.employee?.emailAddress, ActiveLeave?.employee?.nameEnglish, "Leave Application", html);

            return Json(htmlShow);
        }
        #endregion
    }
}