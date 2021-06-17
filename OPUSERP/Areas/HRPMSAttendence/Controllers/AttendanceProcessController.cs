using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSAttendence.Models;
using OPUSERP.Areas.HRPMSAttendence.Models.Lang;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Data.Entity.Attendance;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.Attendance.Interfaces;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.HRPMS.Services.Report;
using OPUSERP.Payroll.Services.Salary.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSAttendence.Controllers
{
    [Area("HRPMSAttendence")]
    public class AttendanceProcessController : Controller
    {
        private readonly LangGenerate<AttendanceLn> _lang;
        private readonly IEmployeeInfoService employeeInfoService;
        private readonly IPersonalInfoService personalInfoService;
        private IAttendanceProcessService attendanceProcessService;
        private readonly IERPCompanyService eRPCompanyService;
        private readonly IUserInfoes userInfo;
        private readonly IShiftGroupMasterService shiftGroupMasterService;
        private readonly HRPMS.Services.MasterData.Interfaces.IDesignationDepartmentService designationDepartmentService;
        private readonly IApprovalService approvalService;
        private readonly ISalaryService salaryService;

        private IReports reports;
        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public AttendanceProcessController(IHostingEnvironment hostingEnvironment, IConverter converter, IUserInfoes userInfo, IPersonalInfoService personalInfoService, IEmployeeInfoService employeeInfoService, IAttendanceProcessService attendanceProcessService, IReports reports, IERPCompanyService eRPCompanyService, IShiftGroupMasterService shiftGroupMasterService, HRPMS.Services.MasterData.Interfaces.IDesignationDepartmentService designationDepartmentService, IApprovalService approvalService, ISalaryService salaryService)
        {
            _lang = new LangGenerate<AttendanceLn>(hostingEnvironment.ContentRootPath);
            this.attendanceProcessService = attendanceProcessService;
            this.employeeInfoService = employeeInfoService;
            this.personalInfoService = personalInfoService;
            this.reports = reports;
            this.eRPCompanyService = eRPCompanyService;
            this.userInfo = userInfo;
            this.shiftGroupMasterService = shiftGroupMasterService;
            this.designationDepartmentService = designationDepartmentService;
            this.approvalService = approvalService;
            this.salaryService = salaryService;

            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        #region Process Attendance

        public ActionResult Index()
        {
            EmployeePunchCardInfoViewModel model = new EmployeePunchCardInfoViewModel
            {
                fLang = _lang.PerseLang("Attendance/AttendanceProcessEN.json", "Attendance/AttendanceProcessBN.json", Request.Cookies["lang"])

            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<JsonResult> ProcessAttendance(DateTime startDate, int sbuId, int processType)
        {
            if (processType == 1)
            {
                await attendanceProcessService.ProcessAttendanceFromMachine(startDate, sbuId);
            }
            else if (processType == 2)
            {
                await attendanceProcessService.ProcessAttendance(startDate, sbuId);
            }
            else
            {
                await attendanceProcessService.ProcessAttendanceFromExcel(startDate, sbuId);
            }

            return Json(true);
        }
       
        public ActionResult WagesIndex()
        {
            EmployeePunchCardInfoViewModel model = new EmployeePunchCardInfoViewModel
            {
                fLang = _lang.PerseLang("Attendance/AttendanceProcessEN.json", "Attendance/AttendanceProcessBN.json", Request.Cookies["lang"])

            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<JsonResult> WagesProcessAttendance(string startDate)
        {
            await attendanceProcessService.WagesProcessAttendance(Convert.ToDateTime(startDate).ToString("yyyyMMdd"), 1);
            return Json(true);
        }

        #endregion

        #region Attendance Update Apply

        public async Task<ActionResult> AttendanceApply()
        {
            var plan = await attendanceProcessService.GetAllAttendanceUpdateApply();
            string applicationNo = (plan.Count() + 1).ToString();
            ViewBag.applicationNo = applicationNo;

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            AttendanceUpdateApplyViewModel model = new AttendanceUpdateApplyViewModel
            {
                attendanceUpdateApplies = await attendanceProcessService.GetAttendanceUpdateApplyByEmpId(empId),             
                approvalDetail = await approvalService.GetSingleApprovalDetailByEmpAndType(empId, "Attendance"),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AttendanceApply([FromForm] AttendanceUpdateApplyViewModel model)
        {

            AttendanceUpdateApply data = new AttendanceUpdateApply
            {
                Id = model.editId,
                employeeInfoId = model.employeeInfoId,
                applicationNo = model.applicationNo,
                applyDateTime = model.applyDateTime,
                notes = model.notes,
                approvalStatus = 0
            };

            int masterId = await attendanceProcessService.SaveAttendanceUpdateApply(data);

            await attendanceProcessService.DeleteAttendanceRouteByMasterId(masterId);
            IEnumerable<ApprovalDetail> approvalDetails = await approvalService.GetApprovalDetailByEmpAndType(model.employeeInfoId, "Attendance");

            var j = 1;
            var Active = 1;
            foreach (ApprovalDetail supervisor in approvalDetails)
            {
                AttendanceRoute loanRoute = new AttendanceRoute
                {
                    attendanceUpdateApplyId = masterId,
                    employeeId = supervisor.approverId,
                    routeOrder = j,
                    isActive = Active,
                };
                await attendanceProcessService.SaveAttendanceRoute(loanRoute);
                j++;
                Active = 0;
            }

            return RedirectToAction(nameof(AttendanceApply));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteAttendanceUpdateApplyById(int Id)
        {
            await attendanceProcessService.DeleteAttendanceUpdateApplyById(Id);
            return Json(true);
        }

        #endregion

        #region Attendance Apply Approve

        public async Task<IActionResult> AttendanceApprove()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }
            AttendanceUpdateApplyViewModel model = new AttendanceUpdateApplyViewModel
            {
                attendanceRoutes = await attendanceProcessService.GetAttendanceRouteByEmpId(empId),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AttendanceApproveAll([FromForm] AttendanceUpdateApplyViewModel model)
        {
            int Status = 1;
            if (model.registerids != null)
            {
                for (int i = 0; i < model.registerids.Count(); i++)
                {
                    AttendanceRoute leaveRoute = await attendanceProcessService.GetAttendanceRouteById((int)model.ids[i]);
                    int? nextOrder = leaveRoute.routeOrder + 1;
                    await attendanceProcessService.UpdateAttendanceRoute(leaveRoute.Id, 0);

                    AttendanceRoute leaveRouteNew = await attendanceProcessService.GetAttendanceRouteByRouteOrder((int)leaveRoute.attendanceUpdateApplyId, (int)nextOrder);

                    if (leaveRouteNew != null)
                    {
                        var leaveFutureEmployee = await personalInfoService.GetEmployeeInfoById((int)leaveRouteNew?.employeeId);
                        await attendanceProcessService.UpdateAttendanceRoute(leaveRouteNew.Id, 1);
                        await attendanceProcessService.UpdateAttendanceUpdateApply((int)model.registerids[i], Status);
                    }
                    else
                    {
                        await attendanceProcessService.UpdateAttendanceUpdateApply((int)model.registerids[0], Status);
                    }
                }
            }

            return RedirectToAction(nameof(AttendanceApprove));
        }


        #endregion

        #region Attendance Report

        public async Task<ActionResult> AttendanceReport()
        {
            EmployeePunchCardInfoViewModel model = new EmployeePunchCardInfoViewModel
            {
                fLang = _lang.PerseLang("Attendance/AttendanceReportEN.json", "Attendance/AttendanceReportBN.json", Request.Cookies["lang"]),
                employeeInfos = await personalInfoService.GetEmployeeInfo(),
                shiftGroupMasterlist = await shiftGroupMasterService.GetAllShiftGroupMaster(),
                departments = await designationDepartmentService.GetDepartment(),

            };
            return View(model);
        }

        public async Task<ActionResult> AttendanceView()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            ViewBag.EmployeeId = userInfos.EmployeeId;
            EmployeePunchCardInfoViewModel model = new EmployeePunchCardInfoViewModel
            {
                fLang = _lang.PerseLang("Attendance/AttendanceReportEN.json", "Attendance/AttendanceReportBN.json", Request.Cookies["lang"]),
                employeeInfos = await personalInfoService.GetEmployeeInfo()

            };
            return View(model);
        }       

        [AllowAnonymous]
        public async Task<IActionResult> IndividualJobCardReportPdf(string rptType, DateTime startDate, DateTime endDate, int EmployeeId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;
            string url = "";

            if (rptType == "ATTNINDV")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/HRPMSAttendence/AttendanceProcess/" + data.FirstOrDefault().reportFormat + "?startDate=" + startDate + "&&endDate=" + endDate + "&&EmployeeId=" + EmployeeId;                
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
        public async Task<IActionResult> IndividualJobCardReportExcel(string rptType, DateTime startDate, DateTime endDate, int EmployeeId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;
            string url = "";

            if (rptType == "ATTNINDV")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/HRPMSAttendence/AttendanceProcess/" + data.FirstOrDefault().reportFormat + "?startDate=" + startDate + "&&endDate=" + endDate + "&&EmployeeId=" + EmployeeId;
            }
            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            string pdfpath = rootPath + @"\wwwroot\pdf\" + fileName;
            string wordpath = fileName.Replace(".pdf", ".xls");
            SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
            f.OpenPdf(rootPath + @"\wwwroot\pdf\" + fileName);
            f.ToExcel(rootPath + @"\wwwroot\pdf\" + wordpath);
            var model = wordpath;
            return Json(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> JobCardReport(DateTime startDate, DateTime endDate, int EmployeeId)
        {

            EmployeePunchCardInfoViewModel model = new EmployeePunchCardInfoViewModel
            {
                individualAttendanceReportViewModels = await attendanceProcessService.GetIndividualAttendanceReport(EmployeeId, startDate.ToString("yyyyMMdd"), endDate.ToString("yyyyMMdd")),
                companies = await eRPCompanyService.GetAllCompany(),
                individualAttendanceSummaryReportViewModels = await attendanceProcessService.GetIndividualAttendanceSummaryReport(EmployeeId, startDate.ToString("yyyyMMdd"), endDate.ToString("yyyyMMdd"), 0, 0),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(EmployeeId),
            };
            ViewBag.startDate = startDate.ToString("dd/MM/yyyy");
            ViewBag.endDate = endDate.ToString("dd/MM/yyyy");
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> JobCardReportCBF(DateTime startDate, DateTime endDate, int EmployeeId)
        {

            EmployeePunchCardInfoViewModel model = new EmployeePunchCardInfoViewModel
            {
                individualAttendanceReportViewModels = await attendanceProcessService.GetIndividualAttendanceReport(EmployeeId, startDate.ToString("yyyyMMdd"), endDate.ToString("yyyyMMdd")),
                companies = await eRPCompanyService.GetAllCompany(),
                individualAttendanceSummaryReportViewModels = await attendanceProcessService.GetIndividualAttendanceSummaryReport(EmployeeId, startDate.ToString("yyyyMMdd"), endDate.ToString("yyyyMMdd"), 0, 0),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(EmployeeId),
            };
            ViewBag.startDate = startDate.ToString("dd/MM/yyyy");
            ViewBag.endDate = endDate.ToString("dd/MM/yyyy");
            return View(model);
        }
       

        [AllowAnonymous]
        public async Task<IActionResult> DailyReportPdf(string rptType, DateTime startDate, DateTime endDate, int summaryId, int? empId, int? deptId, int? shiftId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;
            string url = "";

            if (rptType == "ATTNDTWISE" || rptType == "ATTNLATE" || rptType == "ATTNCONSIDER" || rptType == "ATTNABSENT")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/HRPMSAttendence/AttendanceProcess/" + data.FirstOrDefault().reportFormat + "?startDate=" + startDate + "&&endDate=" + endDate + "&&summaryId=" + summaryId + "&&empId=" + empId + "&&deptId=" + deptId + "&&shiftId=" + shiftId;
            }
            else if (rptType == "ATTNDEPT")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/HRPMSAttendence/AttendanceProcess/" + data.FirstOrDefault().reportFormat + "?startDate=" + startDate + "&&endDate=" + endDate + "&&summaryId=" + summaryId + "&&empId=" + empId + "&&deptId=" + deptId + "&&shiftId=" + shiftId;
            }
            else if (rptType == "ATTNSHIFT")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/HRPMSAttendence/AttendanceProcess/" + data.FirstOrDefault().reportFormat + "?startDate=" + startDate + "&&endDate=" + endDate + "&&summaryId=" + summaryId + "&&empId=" + empId + "&&deptId=" + deptId + "&&shiftId=" + shiftId;
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
        public async Task<IActionResult> DailyReportExcel(string rptType, DateTime startDate, DateTime endDate, int summaryId, int? empId, int? deptId, int? shiftId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;
            string url = "";

            if (rptType == "ATTNDTWISE" || rptType == "ATTNLATE" || rptType == "ATTNCONSIDER" || rptType == "ATTNABSENT")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/HRPMSAttendence/AttendanceProcess/" + data.FirstOrDefault().reportFormat + "?startDate=" + startDate + "&&endDate=" + endDate + "&&summaryId=" + summaryId + "&&empId=" + empId + "&&deptId=" + deptId + "&&shiftId=" + shiftId;
            }
            else if (rptType == "ATTNDEPT")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/HRPMSAttendence/AttendanceProcess/" + data.FirstOrDefault().reportFormat + "?startDate=" + startDate + "&&endDate=" + endDate + "&&summaryId=" + summaryId + "&&empId=" + empId + "&&deptId=" + deptId + "&&shiftId=" + shiftId;
            }
            else if (rptType == "ATTNSHIFT")
            {
                var data = await salaryService.GetReportFormatByReportType(rptType);
                url = scheme + "://" + host + "/HRPMSAttendence/AttendanceProcess/" + data.FirstOrDefault().reportFormat + "?startDate=" + startDate + "&&endDate=" + endDate + "&&summaryId=" + summaryId + "&&empId=" + empId + "&&deptId=" + deptId + "&&shiftId=" + shiftId;
            }
            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            string pdfpath = rootPath + @"\wwwroot\pdf\" + fileName;
            string wordpath = fileName.Replace(".pdf", ".xls");
            SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
            f.OpenPdf(rootPath + @"\wwwroot\pdf\" + fileName);
            f.ToExcel(rootPath + @"\wwwroot\pdf\" + wordpath);
            var model = wordpath;
            return Json(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> DailyReport(DateTime startDate, DateTime endDate, int summaryId, int? empId, int? deptId, int? shiftId)
        {
            EmployeePunchCardInfoViewModel model = new EmployeePunchCardInfoViewModel
            {
                jobCardReportViewModels = await attendanceProcessService.GetDailyJobCardReport(deptId, startDate.ToString("yyyyMMdd"), endDate.ToString("yyyyMMdd"), shiftId, empId, summaryId),
                companies = await eRPCompanyService.GetAllCompany(),
                individualAttendanceSummaryReportViewModels = await attendanceProcessService.GetIndividualAttendanceSummaryReport(Convert.ToInt32(empId), startDate.ToString("yyyyMMdd"), endDate.ToString("yyyyMMdd"), 0, 0),
            };
            ViewBag.startDate = startDate.ToString("dd/MM/yyyy");
            ViewBag.endDate = endDate.ToString("dd/MM/yyyy");
            string rptName = "";
            if (summaryId == -2)
            {
                rptName = "";
            }
            else if (summaryId == 0)
            {
                rptName = "Late";
            }
            else if (summaryId == 2)
            {
                rptName = "Consider";
            }
            else if (summaryId == 3)
            {
                rptName = "Absent";
            }
            ViewBag.rptName = rptName;

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> DailyReportByDepartmentPdf(DateTime startDate, DateTime endDate, int summaryId, int? empId, int? deptId, int? shiftId)
        {
            EmployeePunchCardInfoViewModel model = new EmployeePunchCardInfoViewModel
            {
                jobCardReportViewModels = await attendanceProcessService.GetDailyJobCardReport(deptId, startDate.ToString("yyyyMMdd"), endDate.ToString("yyyyMMdd"), shiftId, empId, summaryId),
                companies = await eRPCompanyService.GetAllCompany(),
                individualAttendanceSummaryReportViewModels = await attendanceProcessService.GetIndividualAttendanceSummaryReport(Convert.ToInt32(empId), startDate.ToString("yyyyMMdd"), endDate.ToString("yyyyMMdd"), deptId, 0),
            };
            ViewBag.startDate = startDate.ToString("dd/MM/yyyy");
            ViewBag.endDate = endDate.ToString("dd/MM/yyyy");
            string rptName = "Department Wise";
            ViewBag.rptName = rptName;

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> DailyReportByShiftPdf(DateTime startDate, DateTime endDate, int summaryId, int? empId, int? deptId, int? shiftId)
        {
            EmployeePunchCardInfoViewModel model = new EmployeePunchCardInfoViewModel
            {
                jobCardReportViewModels = await attendanceProcessService.GetDailyJobCardReport(deptId, startDate.ToString("yyyyMMdd"), endDate.ToString("yyyyMMdd"), shiftId, empId, summaryId),
                companies = await eRPCompanyService.GetAllCompany(),
                individualAttendanceSummaryReportViewModels = await attendanceProcessService.GetIndividualAttendanceSummaryReport(Convert.ToInt32(empId), startDate.ToString("yyyyMMdd"), endDate.ToString("yyyyMMdd"), 0, shiftId),
            };
            ViewBag.startDate = startDate.ToString("dd/MM/yyyy");
            ViewBag.endDate = endDate.ToString("dd/MM/yyyy");
            string rptName = "Shift Wise";
            ViewBag.rptName = rptName;

            return View(model);
        }
        

       

        #endregion

        #region Wages

        public async Task<ActionResult> WagesAttendanceReport()
        {
            EmployeePunchCardInfoViewModel model = new EmployeePunchCardInfoViewModel
            {
                fLang = _lang.PerseLang("Attendance/AttendanceReportEN.json", "Attendance/AttendanceReportBN.json", Request.Cookies["lang"]),
                employeeInfos = await personalInfoService.GetEmployeeInfo()

            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> WagesDailyReport(DateTime startDate, DateTime endDate, int EmployeeId)
        {
            EmployeePunchCardInfoViewModel model = new EmployeePunchCardInfoViewModel
            {
                jobCardReportViewModels = await attendanceProcessService.GetDailyJobCardWagesReport("", startDate.ToString("yyyyMMdd"), endDate.ToString("yyyyMMdd"), 0, 0, 0),
                companies = await eRPCompanyService.GetAllCompany(),
            };
            ViewBag.startDate = startDate.ToShortDateString();
            ViewBag.endDate = endDate.ToShortDateString();
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult WagesDailyReportPdf(DateTime startDate, DateTime endDate, int EmployeeId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;

            string url = scheme + "://" + host + "/HRPMSAttendence/AttendanceProcess/WagesDailyReport?startDate=" + startDate + "&&endDate=" + endDate + "&&EmployeeId=" + EmployeeId;
            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public IActionResult IndividualJobCardWagesReportPdf(DateTime startDate, DateTime endDate, int EmployeeId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;

            string url = scheme + "://" + host + "/HRPMSAttendence/AttendanceProcess/JobCardWagesReport?startDate=" + startDate + "&&endDate=" + endDate + "&&EmployeeId=" + EmployeeId;
            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> JobCardWagesReport(DateTime startDate, DateTime endDate, int EmployeeId)
        {

            EmployeePunchCardInfoViewModel model = new EmployeePunchCardInfoViewModel
            {
                individualAttendanceReportViewModels = await attendanceProcessService.GetWagesIndividualAttendanceReport(EmployeeId, startDate.ToString("yyyyMMdd"), endDate.ToString("yyyyMMdd")),
                companies = await eRPCompanyService.GetAllCompany(),
                individualAttendanceSummaryReportViewModels = await attendanceProcessService.GetWagesIndividualAttendanceSummaryReport(EmployeeId, startDate.ToString("yyyyMMdd"), endDate.ToString("yyyyMMdd"), 0, 0),
            };
            ViewBag.startDate = startDate.ToShortDateString();
            ViewBag.endDate = endDate.ToShortDateString();
            return View(model);
        }


        #endregion
    }
}