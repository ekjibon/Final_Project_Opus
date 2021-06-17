using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSTravle.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.EmailService.interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Travel;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.HRPMS.Services.Travel.Interface;

namespace OPUSERP.Areas.HRPMSTravle.Controllers
{
    [Authorize]
    [Area("HRPMSTravle")]
    public class TravelMasterController : Controller
    {
        private readonly IApprovalService approvalService;
        private readonly IHRDonerSevice travelDonerSevice;
        private readonly IHRActivityService hRActivityService;
        private readonly ITravelVehicleTypeService travelVehicleTypeService;
        private readonly IHRProjectService hRProjectService;
        private readonly ITravellerInfoService travellerInfoService;
        private readonly ITravellingInfoService travellingInfoService;
        private readonly ITravelMasterService travelMasterService;
        private readonly IUserInfoes userInfo;
        private readonly IPersonalInfoService personalInfoService;
        private readonly ISupervisorService supervisorService;
        private readonly ITravelRouteService travelRouteService;
        private readonly ITravelStatusLogService travelStatusLogService;
        private readonly IEmailSenderService emailSenderService;
        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public TravelMasterController(IHostingEnvironment hostingEnvironment, IConverter converter, IHRDonerSevice travelDonerSevice, IHRActivityService hRActivityService, ITravelVehicleTypeService travelVehicleTypeService, IHRProjectService hRProjectService, ITravellerInfoService travellerInfoService, ITravellingInfoService travellingInfoService, ITravelMasterService travelMasterService, IUserInfoes userInfo, IPersonalInfoService personalInfoService, ISupervisorService supervisorService, ITravelRouteService travelRouteService, ITravelStatusLogService travelStatusLogService, IEmailSenderService emailSenderService, IApprovalService approvalService)
        {
            this.travelDonerSevice = travelDonerSevice;
            this.hRActivityService = hRActivityService;
            this.travelVehicleTypeService = travelVehicleTypeService;
            this.hRProjectService = hRProjectService;
            this.travellerInfoService = travellerInfoService;
            this.travellingInfoService = travellingInfoService;
            this.travelMasterService = travelMasterService;
            this.userInfo = userInfo;
            this.personalInfoService = personalInfoService;
            this.supervisorService = supervisorService;
            this.travelRouteService = travelRouteService;
            this.travelStatusLogService = travelStatusLogService;
            this.emailSenderService = emailSenderService;
            this.approvalService = approvalService;

            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
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
            var master = await travelMasterService.GetTravelMaster();
            int count = master.Count();
            ViewBag.travelNumber = "T-00" + count.ToString();

            TravelMasterViewModel model = new TravelMasterViewModel
            {
                travelDoners = await travelDonerSevice.GetHRDoner(),
                travelActivities = await hRActivityService.GetHRActivity(),
                hRProjects = await hRProjectService.GetHRProject(),
                travelVehicleTypes = await travelVehicleTypeService.GetTravelVehicleType(),
                travelMasters = await travelMasterService.GetTravelMasterByEmpId(empId)
            };
            return View(model);
        }

        public async Task<IActionResult> TravelApprove()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }
            TravelMasterViewModel model = new TravelMasterViewModel
            {
                travelDoners = await travelDonerSevice.GetHRDoner(),
                travelActivities = await hRActivityService.GetHRActivity(),
                hRProjects = await hRProjectService.GetHRProject(),
                travelVehicleTypes = await travelVehicleTypeService.GetTravelVehicleType(),
                travelRoutes = await travelRouteService.GetTravelRouteByEmpId(empId)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] TravelMasterViewModel model)
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
            if (!ModelState.IsValid || model.travellerEmployeeList == null)
            {
                model.travelDoners = await travelDonerSevice.GetHRDoner();
                model.travelActivities = await hRActivityService.GetHRActivity();
                model.hRProjects = await hRProjectService.GetHRProject();
                model.travelVehicleTypes = await travelVehicleTypeService.GetTravelVehicleType();
                model.travelMasters = await travelMasterService.GetTravelMasterByEmpId(empId);
                if (model.travellerEmployeeList == null)
                {
                    ModelState.AddModelError(string.Empty, "Have To add minimum 1 Traveller!!");
                }
                return View(model);
            }
            var master = await travelMasterService.GetTravelMaster();
            int count = master.Count();

            TravelMaster data = new TravelMaster
            {
                Id = (int)model.travelID,
                travelNumber = "T-00" + count.ToString(),
                employeeID = EmpInfo.Id,
                accountNumber = model.accountNumber,
                date = model.date,
                travelProjectId = model.travelProjectId,
                hRActivityId = model.travelActivityId,
                hRDonerId = model.travelDonerId,
                purpose = model.purpose,
                status = 1
            };

            int masterId = await travelMasterService.SaveTravelMaster(data);


            foreach (var emplist in model.travellerEmployeeList)
            {
                TravellerInfo travellerInfo = new TravellerInfo
                {
                    travelMasterId = masterId,
                    employeeID = (int)emplist
                };
                await travellerInfoService.SaveTravellerInfo(travellerInfo);
            }
            if (model.travellingFrom != null)
            {
                int x = 0;
                foreach (var travelFrom in model.travellingFrom)
                {
                    TravellingInfo travellingInfo = new TravellingInfo
                    {
                        travellingFrom = travelFrom.Replace("`", ""),
                        travellingTo = model.travellingTo[x].Replace("`", ""),
                        startDate = model.startDate[x],
                        arrivalDate = model.arrivalDate[x],
                        startTime = model.startTime[x],
                        arrivalTime = model.arrivalTime[x],
                        travelMasterId = masterId,
                        travelVehicleTypeId = model.travelVehicleTypeId[x],
                        vehicleNumber = model.vehicleNumber[x],
                        driverName = model.driverName[x],
                        driverContactNumber = model.driverContactNumber[x],
                        accommodationDaaress = model.accommodationDaaress[x].Replace("`", ""),
                        bookingRequird = model.bookingRequird[x]
                    };
                    await travellingInfoService.SaveTravellingInfo(travellingInfo);
                    x++;
                }
            }

            IEnumerable<ApprovalDetail> approvalDetails = await approvalService.GetApprovalDetailByEmpAndType(EmpInfo.Id, "Travel");

            var i = 1;
            var Active = 1;
            foreach (ApprovalDetail supervisor in approvalDetails)
            {
                TravelRoute travelRoute = new TravelRoute
                {
                    travelMasterId = masterId,
                    employeeId = supervisor.approverId,
                    routeOrder = i,
                    isActive = Active,
                };
                await travelRouteService.SaveTravelRoute(travelRoute);
                i++;
                Active = 0;
            }

            TravelStatusLog travelStatusLog = new TravelStatusLog
            {
                employeeId = EmpInfo.Id,
                travelMasterId = masterId,
                date = DateTime.Now,
                remarks = "On Approval",                
                Status = 1
            };
            await travelStatusLogService.SaveTravelStatusLog(travelStatusLog);

            var ActiveLeave = await travelRouteService.GetTravelRouteByTravelMasterId(masterId);

            string html1 = "<div><strong>Travel Application.</strong></div>"
                + " <br/>"
                + "<p>Dear Sir,</p>"
                + " <br/>"
                + " This is to inform you that you have sent one travel application to " + ActiveLeave?.employee?.nameEnglish + " just now."
                + "<br/>"

                + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Human Resource Department.</p></div>"
                + " <br/>";

            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string baseUrl = $"" + scheme + "://" + host + "/HRPMSTravle/TravelMaster/TravelApprove";

            string html = "<div><strong>Travel Recommendation/Approval.</strong></div>"
                    + " <br/>"
                    + "<p>Dear Sir,</p>"
                    + " <br/>"
                    + " This is to inform you that one leave application is waiting for your recommendation/approval."
                    + "<br/>"
                    + "<div><a href='" + baseUrl + "'><button>Login</button></a></div>"
                    + " <br/>"
                    + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Human Resource Department.</p></div>"
                    + " <br/>";
            if (EmpInfo?.emailAddress != null)
            {
                await emailSenderService.SendEmailWithFrom(EmpInfo?.emailAddress, EmpInfo?.nameEnglish, "Travel Application", html1);
            }

            if (ActiveLeave?.employee?.emailAddress != null)
            {
                await emailSenderService.SendEmailWithFrom(ActiveLeave?.employee?.emailAddress, ActiveLeave?.employee?.nameEnglish, "Travel Application", html);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TravelApprove([FromForm] TravelMasterViewModel model)
        {
           //return Json(model);
            TravelRoute leaveRoute = await travelRouteService.GetTravelRouteById((int)model.id);
            int? nextOrder = leaveRoute.routeOrder + 1;
            int Status = 1;
            string mailtext = "";
            if (model.travelApprove == "Approve")
            {
                Status = 2;
                mailtext = "recommended";
            }
            else if (model.travelApprove == "Reject")
            {
                Status = 5;
                mailtext = "rejected";
            }
            else
            {
                Status = 4;
                mailtext = "returned";
            }

            await travelRouteService.UpdateTravelRoute(leaveRoute.Id, 0);

            TravelStatusLog travelStatusLog = new TravelStatusLog
            {
                employeeId = (int)model.employeeID,
                travelMasterId = model.travelID,
                date = DateTime.Now,
                remarks = model.travelApprove,
                Status = Status
            };
            await travelStatusLogService.SaveTravelStatusLog(travelStatusLog);

            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string baseUrl = $"" + scheme + "://" + host + "/HRPMSTravle/TravelMaster/TravelApprove";

            string htmlApprove = "<div><strong>Travel Recommendation/Approval.</strong></div>"
                    + " <br/>"
                    + "<p>Dear Sir,</p>"
                    + " <br/>"
                    + " This is to inform you that one travel application is waiting for your recommendation/approval."
                    + "<br/>"
                    + "<div><a href='" + baseUrl + "'><button>Login</button></a></div>"
                    + " <br/>"
                    + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Human Resource Department.</p></div>"
                    + " <br/>";

            var employee = await personalInfoService.GetEmployeeInfoById((int)model.employeeID);
            var leavRegister = await travelMasterService.GetTravelMasterById((int)model.travelID);
            var leaveEmployee = await personalInfoService.GetEmployeeInfoById((int)leavRegister.employeeID);

            string html1 = "<div><strong>Travel Application.</strong></div>"
                        + " <br/>"
                        + "<p>Dear Sir,</p>"
                        + " <br/>"
                        + " This is to inform you that your travel application has " + mailtext + " by " + employee.nameEnglish + " just now."
                        + "<br/>"

                        + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Human Resource Department.</p></div>"
                        + " <br/>";

            if (Status == 5)
            {
                await travelMasterService.UpdateTravelApproval((int)model.travelID, Status);
                if (leaveEmployee.emailAddress != null)
                {
                    await emailSenderService.SendEmailWithFrom(leaveEmployee.emailAddress, employee.nameEnglish, "Travel Application", html1);
                }
                return RedirectToAction(nameof(TravelApprove));
            }
            else if (Status == 4)
            {
                await travelMasterService.UpdateTravelApproval((int)model.travelID, Status);
                if (leaveEmployee.emailAddress != null)
                {
                    await emailSenderService.SendEmailWithFrom(leaveEmployee.emailAddress, employee.nameEnglish, "Travel Application", html1);
                }
                return RedirectToAction(nameof(TravelApprove));
            }

            TravelRoute leaveRouteNew = await travelRouteService.GetTravelRouteByRouteOrder((int)leaveRoute.travelMasterId, (int)nextOrder);

            if (leaveRouteNew != null)
            {
                var leaveFutureEmployee = await personalInfoService.GetEmployeeInfoById((int)leaveRouteNew?.employeeId);
                await travelRouteService.UpdateTravelRoute(leaveRouteNew.Id, 1);
                await travelMasterService.UpdateTravelApproval((int)model.travelID, Status);
                if (leaveEmployee.emailAddress != null)
                {
                    await emailSenderService.SendEmailWithFrom(leaveEmployee.emailAddress, employee.nameEnglish, "Travel Application", html1);
                }

                if (leaveFutureEmployee.emailAddress != null)
                {
                    await emailSenderService.SendEmailWithFrom(leaveFutureEmployee.emailAddress, employee.nameEnglish, "Travel Application", htmlApprove);
                }
                
            }
            else
            {
                await travelMasterService.UpdateTravelApproval((int)model.travelID, 3);
                string html = "<div><strong>Travel Application.</strong></div>"
                            + " <br/>"
                            + "<p>Dear Sir,</p>"
                            + " <br/>"
                            + " This is to inform you that "
                            + "travel application no '" + leavRegister.travelNumber + "' "
                            + " <br/>"
                            + "Employee Name : '" + leaveEmployee.nameEnglish + "' "
                            + " <br/>"
                            + "Travel Register Date : '" + leavRegister.date?.ToString("dd-MMM-yyyy") + "'"
                            + " <br/>"
                            + "has approved by " + employee.nameEnglish + " just now."
                            + "<br/>"
                            + "<br/>"

                            + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Human Resource Department.</p></div>"
                            + " <br/>";

                if (leaveEmployee?.emailAddress != null)
                {
                    await emailSenderService.SendEmailWithFrom(leaveEmployee?.emailAddress, employee.nameEnglish, "Leave Application", html);
                }
            }

            return RedirectToAction(nameof(TravelApprove));

        }

        // GET: TrainerEntry
        [AllowAnonymous]
        public async Task<IActionResult> TravelReport(int id)
        {
            TravelReportModel travelReportModel = await travelMasterService.GetTravelReportByMasterId(id);
            return View(travelReportModel);
        }

        public async Task<IActionResult> TravelList()
        {
            TravelMasterViewModel model = new TravelMasterViewModel
            {
                travelMasters = await travelMasterService.GetTravelMaster()
            };
            return View(model);
        }

        //PrintPDF
        [AllowAnonymous]
        public IActionResult TravelReportPdf(int id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;

            string url = scheme + "://" + host + "/HRPMSTravle/TravelMaster/TravelReport/" + id;
            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        //PrintPDF
        [AllowAnonymous]
        public IActionResult TravelApproveReportPdf(int id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;

            string url = scheme + "://" + host + "/HRPMSTravle/TravelMaster/TravelApproveReport/" + id;
            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> TravelApproveReport(int id)
        {
            TravelReportModel travelReportModel = await travelMasterService.GetTravelReportByMasterId(id);
            return View(travelReportModel);
        }

        #region API Section
        [Route("global/api/GetAllTravelStatusLogByTravelId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAllTravelStatusLogByTravelId(int id)
        {
            return Json(await travelStatusLogService.GetAllTravelStatusLogByTravelId(id));
        }
        #endregion
    }
}