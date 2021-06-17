using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSTravle.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Travel;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.HRPMS.Services.Travel.Interface;

namespace OPUSERP.Areas.HRPMSTravle.Controllers
{
    public class APITravelController : Controller
    {

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

        public APITravelController(IHRDonerSevice travelDonerSevice, IHRActivityService hRActivityService, ITravelVehicleTypeService travelVehicleTypeService, IHRProjectService hRProjectService, ITravellerInfoService travellerInfoService, ITravellingInfoService travellingInfoService, ITravelMasterService travelMasterService, IUserInfoes userInfo, IPersonalInfoService personalInfoService, ISupervisorService supervisorService, ITravelRouteService travelRouteService, ITravelStatusLogService travelStatusLogService)
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
        }

        [Route("global/api/GetAllTravelDetails/{empId}")]
        [HttpGet]
        [AllowAnonymous]


        public async Task<IActionResult> GetAllTravelDetails(int empId)
        {
        
            TravelMasterViewModel model = new TravelMasterViewModel
            {
                travelDoners = await travelDonerSevice.GetHRDoner(),
                travelActivities = await hRActivityService.GetHRActivity(),
                hRProjects = await hRProjectService.GetHRProject(),
                travelVehicleTypes = await travelVehicleTypeService.GetTravelVehicleType(),
                travelMasters = await travelMasterService.GetTravelMasterByEmpId(empId)
            };
            return Json(model);
        }

        [Route("global/api/GetAllTravelApprovalDetails/{empId}")]
        [HttpGet]
        [AllowAnonymous]


        public async Task<IActionResult> GetAllTravelApprovalDetails(int empId)
        {
        
            TravelMasterViewModel model = new TravelMasterViewModel
            {
                travelDoners = await travelDonerSevice.GetHRDoner(),
                travelActivities = await hRActivityService.GetHRActivity(),
                hRProjects = await hRProjectService.GetHRProject(),
                travelVehicleTypes = await travelVehicleTypeService.GetTravelVehicleType(),
                travelRoutes = await travelRouteService.GetTravelRouteByEmpId(empId)
            };
            return Json(model);
        }




        [HttpPost]
        [AllowAnonymous]
        [Route("api/TravelPost")]
        public async Task<IActionResult> TravelPost([FromBody] TravelMasterViewModel model)
        {
            //return Json(model);
            string userName = model.userName;
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

            IEnumerable<Supervisor> supervisors = await supervisorService.GetSupervisorByEmpId(EmpInfo.Id);

            var i = 1;
            var Active = 1;
            foreach (Supervisor supervisor in supervisors)
            {
                TravelRoute travelRoute = new TravelRoute
                {
                    travelMasterId = masterId,
                    employeeId = supervisor.supervisorId,
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

           

            return Json("Successfully Add ");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/POstTravelApprove")]
        public async Task<IActionResult> POstTravelApprove([FromBody] TravelMasterViewModel model)
        {
            //return Json(model);
            TravelRoute leaveRoute = await travelRouteService.GetTravelRouteById((int)model.id);
            int? nextOrder = leaveRoute.routeOrder + 1;
            int Status = 1;
      
            if (model.travelApprove == "Approve")
            {
                Status = 2;
               
            }
            else if (model.travelApprove == "Reject")
            {
                Status = 5;
                
            }
            else
            {
                Status = 4;
               
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

           
            var employee = await personalInfoService.GetEmployeeInfoById((int)model.employeeID);
            var leavRegister = await travelMasterService.GetTravelMasterById((int)model.travelID);
            var leaveEmployee = await personalInfoService.GetEmployeeInfoById((int)leavRegister.employeeID);

          
           
          

            TravelRoute leaveRouteNew = await travelRouteService.GetTravelRouteByRouteOrder((int)leaveRoute.travelMasterId, (int)nextOrder);

            if (leaveRouteNew != null)
            {
                var leaveFutureEmployee = await personalInfoService.GetEmployeeInfoById((int)leaveRouteNew?.employeeId);
                await travelRouteService.UpdateTravelRoute(leaveRouteNew.Id, 1);
                await travelMasterService.UpdateTravelApproval((int)model.travelID, Status);
               

            }
          

            return Json("Approved");

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}