using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSAttendence.Models;
using OPUSERP.HRPMS.Data.Entity.Attendance;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.Attendance.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.HRPMS.Services.Movement;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSAttendence.Controllers
{
    public class ApiController : Controller
    {
        private readonly ILeaveRegisterService leaveRegisterService;
        private readonly IAttendanceProcessService attendanceProcessService;
        private readonly IMovementService movementService;
        //private readonly IMovementService movementService;

        public ApiController(ILeaveRegisterService leaveRegisterService, IAttendanceProcessService attendanceProcessService, IMovementService movementService)
        {
            this.leaveRegisterService = leaveRegisterService;
            this.attendanceProcessService = attendanceProcessService;
            this.movementService = movementService;
        }

        #region Attendance Api

        // POST: api/saveAttendence
        [HttpPost]
        [AllowAnonymous]
        [Route("api/saveAttendence")]
        public async Task<IActionResult> saveAttendence([FromBody] AttendenceApi model)
        {
            //return Json(model);
            string empCode = model.empCode;
            var chk = await attendanceProcessService.GetAllAttendence(empCode);
            //   return Json(chk);
            if (!ModelState.IsValid || chk.Count() >= 0)
            {
                for (int i = 0; i < chk.Count(); i++)
                {
                    if (chk.ElementAt(i).entryDate == model.entryDate && chk.ElementAt(i).status == 1)
                    {
                        int id = i + 1;
                        AttendenceApi attendenceup = new AttendenceApi
                        {
                            Id = chk.ElementAt(i).Id,
                            empCode = model.empCode,
                            EmpName = chk.ElementAt(i).EmpName,
                            entryDate = chk.ElementAt(i).entryDate,
                            //Date = model.Date,
                            LoginTime = chk.ElementAt(i).LoginTime,
                            LogoutTime = model.LogoutTime,
                            Latitudein = chk.ElementAt(i).Latitudein,
                            Longitudein = chk.ElementAt(i).Longitudein,

                            Latitudeout = model.Latitudeout,
                            Longitudeout = model.Longitudeout,
                            Reason = model.Reason,
                            status = model.status
                        };
                        await attendanceProcessService.UpdateAttendence(id, attendenceup);
                        return Json("Update");
                    }
                    else if (chk.ElementAt(i).entryDate == model.entryDate && chk.ElementAt(i).status == 2)
                    {
                        return Json("Already Update");
                    }
                }

                AttendenceApi attendence = new AttendenceApi
                {
                    empCode = model.empCode,
                    EmpName = model.EmpName,
                    entryDate = model.entryDate,
                    //Date = model.Date,
                    LoginTime = model.LoginTime,
                    LogoutTime = model.LogoutTime,
                    Latitudein = model.Latitudein,
                    Longitudein = model.Longitudein,
                    Latitudeout = model.Latitudeout,
                    Longitudeout = model.Longitudeout,
                    Reason = model.Reason,
                    status = model.status
                };
                await attendanceProcessService.SaveAttendence(attendence);
            }
            return Json("Done");
        }

        // POST: api/updateAttendence
        [HttpPost]
        [AllowAnonymous]
        [Route("api/updateAttendence")]
        public async Task<IActionResult> updateAttendence([FromBody] AttendenceApi model)
        {
            //return Json(model);
            string empCode = model.empCode;
            var chk = await attendanceProcessService.GetAllAttendence(empCode);
            if (!ModelState.IsValid || chk.Count() >= 0)
            {
                for (int i = 0; i < chk.Count(); i++)
                {
                    if (Convert.ToDateTime(chk.ElementAt(i).entryDate).ToString("yyyyMMdd") == Convert.ToDateTime(model.entryDate).ToString("yyyyMMdd") && chk.ElementAt(i).status == 1)
                    {
                        AttendenceApi attendence = new AttendenceApi
                        {
                            empCode = model.empCode,
                            entryDate = DateTime.Now,
                            Latitudein = model.Latitudein,
                            Longitudein = model.Longitudein,

                            //LogoutTime = model.LogoutTime,
                            //Latitudeout = model.Latitudeout,
                            //Longitudeout = model.Longitudeout,
                            Reason = model.Reason,
                            status = 1
                        };
                        await attendanceProcessService.SaveAttendence(attendence);
                        return Json("Done");
                    }
                }
            }
            return Json("Done");
        }

        [Route("global/api/GetAllAttendence/{empcode}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAttendence(string empcode)
        {
            return Json(await attendanceProcessService.GetAllAttendence(empcode));
        }

        [AllowAnonymous]
        [Route("global/api/GetAllAttendenceByEmpIdAndDateRange/{empcode}/{from}/{to}")]
        [HttpGet]
        public async Task<IActionResult> GetAllAttendenceByEmpIdAndDateRange(string empcode, string from, string to)
        {
            return Json(await attendanceProcessService.GetAllAttendenceByEmpIdAndDateRange(empcode, from, to));
        }

        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region Movement Api

        // POST: api/saveMovementTracking
        [HttpPost]
        [AllowAnonymous]
        [Route("api/saveMovementTracking")]
        public async Task<IActionResult> saveMovementTracking([FromBody] MovementTracking model)
        {
            //return Json(model);
            MovementTracking movementTracking = new MovementTracking
            {
                empCode = model.empCode,
                EmpName = model.EmpName,
                entryDate = DateTime.Now, //model.entryDate,
                LoginTime = model.LoginTime,

                Latitude = model.Latitude,
                Longitude = model.Longitude,

                CompanyName=model.CompanyName,
                Contact=model.Contact,
                DesignationName=model.DesignationName,
                Reason = model.Reason,
                Status = model.Status
            };
            await movementService.SaveMovement(movementTracking);
            return Json("Done");
        }

        // POST: api/updateMovement
        [HttpPost]
        [AllowAnonymous]
        [Route("api/updateMovement")]
        public async Task<IActionResult> updateMovement([FromBody] MovementTrackingViewModel model)
        {
            string empCode = model.empCode;
            var chk = await movementService.GetAllMovementTracking(empCode);
            //   return Json(chk);
            if (!ModelState.IsValid || chk.Count() >= 0)
            {
                for (int i = 0; i < chk.Count(); i++)
                {
                    if (chk.ElementAt(i).Id == model.Id && chk.ElementAt(i).Status == 1)
                    {
                        int id = i + 1;
                        MovementTracking attendenceup = new MovementTracking
                        {
                            Id = chk.ElementAt(i).Id,
                            LogoutTime = model.LogoutTime,
                        };
                        await movementService.UpdateMovement(id, attendenceup);
                        return Json("Checked Out Susseccfully");
                    }
                    else if (chk.ElementAt(i).entryDate == model.entryDate && chk.ElementAt(i).Status == 2)
                    {
                        return Json("Already Checked Out");
                    }
                }
            }

            return Json("Update");
        }

        [Route("global/api/GetAllMovement/{empcode}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllMovement(string empcode)
        {
            return Json(await movementService.GetAllMovementTracking(empcode));
        }

        [AllowAnonymous]
        [Route("global/api/GetAllMovementByEmpIdAndDateRange/{empcode}/{from}/{to}")]
        [HttpGet]
        public async Task<IActionResult> GetAllMovementByEmpIdAndDateRange(string empcode, DateTime from, DateTime to)
        {
            return Json(await movementService.GetAllMovementByEmpIdAndDateRange(empcode, from, to));
        }

        #endregion
    }
}