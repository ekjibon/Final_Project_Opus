using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSLeave.Models.Lang;
using OPUSERP.Areas.HRPMSMasterData.Models;
using OPUSERP.HRPMS.Data.Entity.Leave;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Leave.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.HRPMSLeave.Controllers
{
    [Authorize]
    [Area("HRPMSLeave")]
    public class LeaveTypeController : Controller
    {
        private readonly LangGenerate<LeaveLn> _lang;
        private readonly ILeaveTypeService leaveTypeService;

        public LeaveTypeController(IHostingEnvironment hostingEnvironment, ILeaveTypeService leaveTypeService)
        {
            _lang = new LangGenerate<LeaveLn>(hostingEnvironment.ContentRootPath);
            this.leaveTypeService = leaveTypeService;
        }
        // GET: LevelofEducation
        public async Task<IActionResult> Index()
        {
            LeaveTypeViewModel model = new LeaveTypeViewModel
            {
                fLang = _lang.PerseLang("Leave/LeaveTypeEN.json", "Leave/LeaveTypeBN.json", Request.Cookies["lang"]),
                leaveTypes = await leaveTypeService.GetAllLeaveType()
            };
            return View(model);
        }

        // POST: LevelofEducation/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] LeaveTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.leaveTypes = await leaveTypeService.GetAllLeaveType();
                model.fLang = _lang.PerseLang("Leave/LeaveTypeEN.json", "Leave/LeaveTypeBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            LeaveType data = new LeaveType
            {
                Id = model.id,
                nameEn = model.leaveTypeNameEn,
                nameBn = model.leaveTypeNameBn,
                description = model.description,
            };

            await leaveTypeService.SaveLeaveType(data);

            return RedirectToAction(nameof(Index));
        }

        [Route("global/api/GetAllLeaveType")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllLeaveType()
        {
            return Json(await leaveTypeService.GetAllLeaveType());
        }
    }
}