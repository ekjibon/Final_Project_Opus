using OPUSERP.Areas.HRPMSAttendence.Models;
using OPUSERP.Areas.HRPMSAttendence.Models.Lang;
using OPUSERP.Areas.HRPMSMasterData.Models;
using OPUSERP.HRPMS.Data.Entity.Attendance;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OPUSERP.HRPMS.Data.Entity.Wages;
using OPUSERP.HRPMS.Services.Employee.Interfaces;

namespace OPUSERP.Areas.HRPMSAttendence.Controllers
{
    [Area("HRPMSAttendence")]
    public class EmployeePunchCardInfoController : Controller
    {
        private readonly LangGenerate<AttendanceLn> _lang;
        private readonly IEmployeePunchCardInfoService employeePunchCardInfoService;
        private readonly IShiftGroupMasterService shiftGroupMasterService;
        private readonly IWagesPunchCardInfoService wagesPunchCardInfoService;
        private readonly IPersonalInfoService personalInfoService;

        public EmployeePunchCardInfoController(IHostingEnvironment hostingEnvironment, IEmployeePunchCardInfoService employeePunchCardInfoService, IShiftGroupMasterService shiftGroupMasterService, IWagesPunchCardInfoService wagesPunchCardInfoService, IPersonalInfoService personalInfoService)
        {
            _lang = new LangGenerate<AttendanceLn>(hostingEnvironment.ContentRootPath);
            this.employeePunchCardInfoService = employeePunchCardInfoService;
            this.shiftGroupMasterService = shiftGroupMasterService;
            this.wagesPunchCardInfoService = wagesPunchCardInfoService;
            this.personalInfoService = personalInfoService;
        }

        // GET: EmployeePunchCardInfo
        public async Task<ActionResult> Index()
        {
            EmployeePunchCardInfoViewModel model = new EmployeePunchCardInfoViewModel
            {
                fLang = _lang.PerseLang("Attendance/PunchCardInfoEN.json", "Attendance/PunchCardInfoBN.json", Request.Cookies["lang"]),
                employeePunchCardInfoslist = await employeePunchCardInfoService.GetAllEmployeePunchCardInfo(),
                shiftGroupMasterslist = await shiftGroupMasterService.GetAllShiftGroupMaster(),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };
            return View(model);
        }

        // GET: EmployeePunchCardInfo
        public async Task<ActionResult> WagesIndex()
        {
            EmployeePunchCardInfoViewModel model = new EmployeePunchCardInfoViewModel
            {
                fLang = _lang.PerseLang("Attendance/PunchCardInfoEN.json", "Attendance/PunchCardInfoBN.json", Request.Cookies["lang"]),
                wagesPunchCardInfos = await wagesPunchCardInfoService.GetAllEmployeePunchCardInfo(),
            };
            return View(model);
        }

        // POST: EmployeePunchCardInfo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] EmployeePunchCardInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("Attendance/PunchCardInfoEN.json", "Attendance/PunchCardInfoBN.json", Request.Cookies["lang"]);
                model.employeePunchCardInfoslist = await employeePunchCardInfoService.GetAllEmployeePunchCardInfo();
                model.shiftGroupMasterslist = await shiftGroupMasterService.GetAllShiftGroupMaster();
                return View(model);
            }

            EmployeePunchCardInfo data = new EmployeePunchCardInfo
            {
                Id = model.editId,
                shiftGroupMasterId = (int)model.shiftGroupMasterId,
                punchCardNo = model.punchCardNo,
                employeeCode = model.employeeCode
            };

            await employeePunchCardInfoService.SaveEmployeePunchCardInfo(data);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WagesIndex([FromForm] EmployeePunchCardInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("Attendance/PunchCardInfoEN.json", "Attendance/PunchCardInfoBN.json", Request.Cookies["lang"]);
                model.wagesPunchCardInfos = await wagesPunchCardInfoService.GetAllEmployeePunchCardInfo();
                return View(model);
            }

            WagesPunchCardInfo data = new WagesPunchCardInfo
            {
                Id = model.editId,
                punchCardNo = model.punchCardNo,
                employeeId = (int)model.employeeId
            };

            await wagesPunchCardInfoService.SaveEmployeePunchCardInfo(data);

            return RedirectToAction(nameof(WagesIndex));
        }

        #region API Section
        [Route("global/api/shiftgroupmasters")]
        [HttpGet]
        public async Task<IActionResult> ProcessAttendance()
        {
            return Json(await shiftGroupMasterService.GetAllShiftGroupMaster());
        }
        #endregion
    }
}